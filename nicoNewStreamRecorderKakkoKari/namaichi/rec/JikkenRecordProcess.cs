﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/08/30
 * Time: 18:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.IO.Compression;
using WebSocket4Net;
using Newtonsoft.Json;
using namaichi.info;

namespace namaichi.rec
{
	/// <summary>
	/// Description of JikkenRecordProcess.
	/// </summary>
	public class JikkenRecordProcess : IRecorderProcess
	{
		//private string broadcastId;
		private string userId;
		private string lvid;
		private long releaseTime;
		private bool isPremium = false;
		private CookieContainer container;
		private string[] recFolderFile;
		private RecordingManager rm;
		private RecordFromUrl rfu;
		public JikkenRecorder jr;
		public WatchingInfo wi;
		private Record rec;
		private WebSocket wsc;
		private StreamWriter commentSW;
//		override public string msUri;
//		public string[] msReq;
		public long serverTime;
		private string ticket;
		private bool isRetry = true;
//		private string msThread;
//		private string sendCommentBuf = null;
//		private bool isSend184 = true;
		
		//private bool isNoPermission = false;
		//public long openTime;
		public bool isEndProgram = false;
		//public int lastSegmentNo = -1;
		//public bool isTimeShift = false;
		private TimeShiftConfig tsConfig = null;
		private bool isTimeShiftCommentGetEnd = false;
		private DateTime lastEndProgramCheckTime = DateTime.Now;
		private DateTime lastWebsocketConnectTime = DateTime.Now;
		
		public TimeSpan jisa = TimeSpan.MinValue;
		//private DateTime beginTime = null;
		//private DateTime endTime = null;
		private TimeSpan programTime = TimeSpan.Zero;
		
//		public DateTime tsHlsRequestTime = DateTime.MinValue;
//		public TimeSpan tsStartTime;
		
		private DeflateDecoder deflateDecoder = new DeflateDecoder();
		CancellationTokenSource wscPongCancelToken;
		
		private bool isGetCommentXml;
		private TimeShiftCommentGetter_jikken tscgChat;
		private TimeShiftCommentGetter_jikken tscgControl;
		
		public JikkenRecordProcess( 
				CookieContainer container, string[] recFolderFile, 
				RecordingManager rm, RecordFromUrl rfu, 
				JikkenRecorder jr, long openTime, 
				bool isTimeShift, string lvid, 
				TimeShiftConfig tsConfig, string userId, 
				bool isPremium, TimeSpan programTime, 
				WatchingInfo wi, long releaseTime)
		{
			this.container = container;
			this.recFolderFile = recFolderFile;
			this.rm = rm;
			this.rfu = rfu;
			this.jr = jr;
			this.openTime = openTime;
			this.isTimeShift = isTimeShift;
			this.lvid = lvid;
			this.tsConfig = tsConfig;
			this.userId = userId;
			this.isPremium = isPremium;
			this.programTime = programTime;
			this.wi = wi;
			this.msUri = wi.msUri;
			isGetCommentXml = bool.Parse(rm.cfg.get("IsgetcommentXml"));
			isJikken = true;
			this.releaseTime = releaseTime;
		}
		public void start() {
			util.debugWriteLine("jrp start");
			
			process();
			displaySchedule();
			rm.form.setStatistics(wi.visit, wi.comment);
			
			while (rm.rfu == rfu && isRetry) {
				//test
//				GC.Collect();
//				GC.WaitForPendingFinalizers();
				
				System.Threading.Thread.Sleep(100);
			}
			//while (isTimeShift && !isTimeShiftCommentGetEnd && rm.rfu == rfu) 
			//	System.Threading.Thread.Sleep(300);
			
			isRetry = false;
			
			if (rm.rfu != rfu) {
				if (tscgChat != null) tscgChat.setIsRetry(false);
				if (tscgControl != null) tscgControl.setIsRetry(false);
			}
			
			if (isTimeShift && rm.rfu == rfu) {
				while (rm.rfu == rfu && (!tscgChat.isEnd || !tscgControl.isEnd)) {
					Thread.Sleep(1000);
				}
			}
			
			stopRecording();
			if (rec != null) 
				rec.waitForEnd();			
			
			util.debugWriteLine("closed saikai");

		}
		private void process() {
			//util.debugWriteLine("testtest");
			/*
			try {var a = new WebSocket4Net.MessageReceivedEventArgs(null, null).Data;}
			catch (Exception e) {
				System.Windows.Forms.MessageBox.Show("websocket", "aa");
			}
			*/
			//util.debugWriteLine(System.Diagnostics.FileVersionInfo.GetVersionInfo("websocket4net.dll").
			Task.Run(() => {record();});
			Task.Run(() => {getMessage();});
			Task.Run(() => {connectKeeper();});
		}
		private void record() {
			Task.Run(() => {
				rec = new Record(rm, true, rfu, wi.hlsUrl, recFolderFile[1], -1, container, isTimeShift, this, lvid, tsConfig, releaseTime, null);
				
				rec.record(jr.requestQuality);
				if (rec.isEndProgram) {
					util.debugWriteLine("stop jrp recd");
					isRetry = false;
					isEndProgram = true;
				}
		    });
		}
		private void getMessage() {
			//if (rm.isPlayOnlyMode) return;
			
			if (isTimeShift) {
				tscgChat = new TimeShiftCommentGetter_jikken(this, userId, rm, rfu, rm.form, openTime, recFolderFile, lvid, container, wi.chatThread, wi.chatKey, true, wi);
				tscgControl = new TimeShiftCommentGetter_jikken(this, userId, rm, rfu, rm.form, openTime, recFolderFile, lvid, container, wi.controlThread, wi.controlKey, false, wi);
				tscgChat.save();
				tscgControl.save();
				if (rm.isPlayOnlyMode) return;
				
				while (rm.rfu == rfu && isRetry) {
					if (tscgChat.isEnd && tscgControl.isEnd) break;
					Thread.Sleep(500);
				}
				
				tscgChat.gotCommentList.AddRange(tscgControl.gotCommentList);
				var fName = util.getOkCommentFileName(rm.cfg, recFolderFile[1], lvid, true);
				TimeShiftCommentGetter_jikken.endProcess(tscgChat.gotCommentList, fName, rm.form, tscgChat.isGetXml, tscgChat.threadLine, tscgControl.threadLine);
			} else connectMessageServer();
		}
		private void messageServerPong() {
			while (rm.rfu == rfu && isRetry) {
				if (wsc == null) return;
				wscPongCancelToken = new CancellationTokenSource();
				try {
					var t = Task.Delay(TimeSpan.FromMinutes(1), wscPongCancelToken.Token);
					t.Wait();
					wsc.Send("");
				} catch (Exception e) {
					util.debugWriteLine("wsc pong cancel? " + e.Message + e.Source + e.StackTrace + e.TargetSite);
					break;
				}
			}
		}
		private void connectKeeper() {
			var start = DateTime.Now;
			while (rm.rfu == rfu && isRetry) {
				Thread.Sleep(500);
				if (DateTime.Now - start < 
				    TimeSpan.FromMilliseconds((double)wi.expireIn)) continue;
				start = DateTime.Now;
				
				var w = jr.getWatchingPut();
				w.Wait();
				if (w.Result == null) continue;
				wi.setPutWatching(w.Result);
				rm.form.setStatistics(wi.visit, wi.comment);
			}
		}
		public void displaySchedule() {
			Task.Run(() => {
				while (rm.rfu == rfu && isRetry) {
	         		if (isTimeShift && tsHlsRequestTime == DateTime.MinValue) {
	         			Thread.Sleep(300);
	         			continue;
	         		}
	         		//if (!isTimeShift && jisa == TimeSpan.MinValue) {
	         		if (jisa == TimeSpan.MinValue) {
	         			Thread.Sleep(300);
	         			continue;
	         		}
			        TimeSpan _keikaJikanDt;
			        if (!isTimeShift)
						_keikaJikanDt = (DateTime.Now - util.getUnixToDatetime(openTime) + jisa);
					else 
						_keikaJikanDt = (DateTime.Now - tsHlsRequestTime + tsStartTime + jisa);
			        var keikaJikanH = (int)(_keikaJikanDt.TotalHours);
					var keikaJikan = _keikaJikanDt.ToString("''mm'分'ss'秒'");
					if (keikaJikanH != 0) keikaJikan = keikaJikanH.ToString() + "時間" + keikaJikan;
					
					var timeLabelKeikaH = (int)(_keikaJikanDt.TotalHours);
					var timeLabelKeika = _keikaJikanDt.ToString("''mm':'ss''");
					if (timeLabelKeikaH != 0) timeLabelKeika = timeLabelKeikaH.ToString() + ":" + timeLabelKeika;
					
					var programTimeStrH = (int)(programTime.TotalHours);
					var programTimeStr = programTime.ToString("''mm':'ss''");
					if (programTimeStrH != 0) programTimeStr = programTimeStrH.ToString() + ":" + programTimeStr;
					
					//var keikaJikan = _keikaJikanDt.ToString("H'時間'm'分's'秒'");
					//var programTimeStr = programTime.ToString("h'時間'm'分's'秒'");
					rm.form.setKeikaJikan(keikaJikan, timeLabelKeika + "/" + programTimeStr);
					System.Threading.Thread.Sleep(100);
				}
			});
		}
		override public string[] getRecFilePath(long releaseTime) {
			return jr.getRecFilePath(releaseTime);
		}
		override public void reConnect() {
			util.debugWriteLine("reconnect");
			while (true) {
				if (rfu != rm.rfu || !isRetry) return;
				
//				var w = jr.getWatchingPut();
				var w = jr.getWatching();
				w.Wait();
				if (w.Result == null || w.Result.IndexOf("master.m3u8") == -1) {
					Thread.Sleep(3000);
					
					Task.Run(() => {
						if (!isTimeShift && isEndedProgram()) {
							isRetry = false;
							isEndProgram = true;
						}
					});
					continue;
				}
				wi.setPutWatching(w.Result);
				rm.form.setStatistics(wi.visit, wi.comment);
				break;
			}
			if (!isTimeShift && wi.hlsUrl.IndexOf("hlsarchive") > -1) {
				isRetry = false;
				isEndProgram = true;
				return;
			}
			rec.reSetHlsUrl(wi.hlsUrl, jr.requestQuality, null);
		}
		override public void sendComment(string s, bool is184) {
			//{"message":"大丈夫な範囲だと思います","command":"184 ","multi":[],"vpos":"151539"}
			//https://api.cas.nicovideo.jp/v1/services/live/programs/lv315399591/comments
			//post
			jr.sendComment(s, is184);
		}
		private void onWscOpen(object sender, EventArgs e) {
			util.debugWriteLine("ms open a");
			var res_from = (isTimeShift) ? 1 : -15;
			msReq = wi.getMessageRequest(userId, res_from);
			
			try {
				wsc.Send(msReq[0]);
				wsc.Send(msReq[1]);
			} catch (Exception ee) {
				util.debugWriteLine("on open wsc req send exception " + ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
			
			if (rm.rfu != rfu) {
				//stopRecording();
//				wsc.Close();
				return;
			}			
			try {
				if (bool.Parse(rm.cfg.get("IsgetComment")) && commentSW == null) {
					var commentFileName = util.getOkCommentFileName(rm.cfg, recFolderFile[1], lvid, isTimeShift);
					
					var isExists = File.Exists(commentFileName);
					commentSW = new StreamWriter(commentFileName, false, System.Text.Encoding.UTF8);
					if (isGetCommentXml && !isExists) {
						commentSW.WriteLine("<?xml version='1.0' encoding='UTF-8'?>");
				        commentSW.WriteLine("<packet>");
				        commentSW.Flush();
					} 
			       
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + " " + ee.StackTrace);
			}
			
			if (wscPongCancelToken == null) 
				Task.Run(() => {messageServerPong();});
		}
		
		private void onWscClose(object sender, EventArgs e) {
			util.debugWriteLine("ms onclose");
			closeWscProcess();
			//wsc = null;
			
			if (rm.rfu == rfu && !isEndProgram && isRetry && (WebSocket)sender == wsc && !isTimeShiftCommentGetEnd) {
				while (true) {
					try {
						if (!connectMessageServer()) continue;
						break;
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
//				ws.Open();
				/*
				Task.Run(() => {
					if (!isTimeShift && isEndedProgram()) {
						isRetry = false;
						isEndProgram = true;
					}
				});
				*/
			}
		}
		private void closeWscProcess() {
			util.debugWriteLine("close wsc process");
			if (isTimeShift && isTimeShiftCommentGetEnd) return;
			
			if (commentSW != null) {
				if (isGetCommentXml) {
					try {
						commentSW.WriteLine("</packet>");
						commentSW.Flush();
						
						commentSW.Close();
						commentSW = null;
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.TargetSite);
					}
				}
			}
			

			//stopRecording();
//			endStreamProcess();
		}
		private void onWscError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
			util.debugWriteLine("ms onerror");
			//stopRecording();
//			endStreamProcess();
		}
		private void onWscDataReceive(object sender, DataReceivedEventArgs e) {
			util.debugWriteLine("kiteru a");
			util.debugWriteLine(e.Data.ToString());
			int a = 3;
		}
		
		private void onWscMessageReceive(object sender, MessageReceivedEventArgs e) {
			util.debugWriteLine("on wsc message ");
			
			if (isTimeShift && e.Message.StartsWith("{\"ping\":{\"content\":\"rf:")) {
//				closeWscProcess();
//				try {commentSW.Close();}
//				catch (Exception eee) {util.debugWriteLine(eee.Message + eee.Source + eee.StackTrace + eee.TargetSite);}
				
				//isTimeShiftCommentGetEnd = true;
//				rm.form.addLogText("コメントの保存を完了しました");
			}
			
			if (rm.rfu != rfu || (!isTimeShift && !isRetry)) {
				try {
					if (wsc != null) wsc.Close();
				} catch (Exception ee) {
					util.debugWriteLine("wsc message receive exception " + ee.Source + " " + ee.StackTrace + " " + ee.TargetSite + " " + ee.Message);
				}
				//stopRecording();
				util.debugWriteLine("tigau rfu comment" + e.Message);
				return;
			}
			
//			var a = System.Text.Encoding.UTF8.GetBytes(e.Message);
//			util.debugWriteLine(string.Join(" ", a));
//			util.debugWriteLine(string.Join(" ", e.Data));
			
			var decodedMsg = deflateDecoder.decode(e.Data);
//			util.debugWriteLine("decoded " + decodedMsg);
 			
			
			var chatInfoList = getChatInfoList(decodedMsg);
			
			
			foreach (var chatinfo in chatInfoList) {
			
				XDocument chatXml;
				if (isTimeShift) chatXml = chatinfo.getFormatXml(openTime);
				else chatXml = chatinfo.getFormatXml(serverTime);
				util.debugWriteLine("xml " + chatXml.ToString());
				
				if (chatinfo.root == "chat" && (chatinfo.contents.IndexOf("/hb ifseetno") != -1 && 
						chatinfo.premium == "3")) continue;
				if (chatinfo.root != "chat" && chatinfo.root != "thread" 
				    	&& chatinfo.root != "control") 
					continue;
				
				if (chatinfo.root == "thread") {
					serverTime = chatinfo.serverTime;
					ticket = chatinfo.ticket;
					jisa = util.getUnixToDatetime(serverTime) - DateTime.Now;
				}
				
//				util.debugWriteLine("wsc message " + wsc);
				
	//			Newtonsoft.Json
				//if (e.Message.IndexOf("chat") < 0 &&
				//    	e.Message.IndexOf("thread") < 0) return;
				
				
	//            util.debugWriteLine(jsonCommentToXML(text));
				try {
					if (commentSW != null) {
						if (isGetCommentXml) {
							commentSW.WriteLine(chatXml.ToString());
						} else {
							var vposReplaced = Regex.Replace(chatinfo.json, 
				            	"\"vpos\"\\:(\\d+)", 
				            	"\"vpos\":" + chatinfo.vpos + "");
							commentSW.WriteLine(vposReplaced);
						}
			            commentSW.Flush();
					}
	           
				} catch (Exception ee) {util.debugWriteLine(ee.Message + " " + ee.StackTrace);}
				
				//if (!isTimeShift)
					addDisplayComment(chatinfo);
			}

		}
		public List<ChatInfo> getChatInfoList(string decodedMsg) {
			var ret = new List<ChatInfo>();
			if (decodedMsg.StartsWith("[")) {
				var b = decodedMsg.Trim(new char[]{'[', ']', '\0'});
//				util.debugWriteLine("b " + b);
				var jsonArr = b.Replace("}, ", "}\n").Split('\n');
				foreach (var j in jsonArr) {
//					util.debugWriteLine("j " + j);
					try {
						var xml = JsonConvert.DeserializeXNode(j);
						var chatinfo = new namaichi.info.ChatInfo(xml, j);
						ret.Add(chatinfo);
					} catch (Exception e) {
						util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					}
					
				}
			} else {
				try {
					var xml = JsonConvert.DeserializeXNode(decodedMsg);
					var chatinfo = new namaichi.info.ChatInfo(xml, decodedMsg);
					ret.Add(chatinfo);
				} catch (Exception e) {
					util.debugWriteLine("decode msg exception " + decodedMsg);
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			return ret;
		}

		private void addDisplayComment(namaichi.info.ChatInfo chat) {
			
			if (chat.root.Equals("thread")) return;
			if (chat.contents == "再読み込みを行いました<br>読み込み中のままの方はお手数ですがプレイヤー下の更新ボタンをお試し下さい") {
				util.debugWriteLine("chat 再読み込みを行いました");
				return;
			}
			if (chat.contents == null) return;
//			var time = util.getUnixToDatetime(chat.vpos / 100);
//			var unixKijunDt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			var __time = chat.date - openTime; //- (60 * 60 * 9);
			if (__time < 0) __time = 0;
			
//			var __timeDt = util.getUnixToDatetime(__time);
//			var openTimeDt = util.getUnixToDatetime(openTime);
//			var __timeDt0 = __timeDt - openTimeDt;
//			var __timeDt1 = __timeDt0. - new timespunixKijunDt;
			var h = (int)(__time / (60 * 60));
			var m = (int)((__time % (60 * 60)) / 60);
			var _m = (m < 10) ? ("0" + m.ToString()) : m.ToString();
			var s = __time % 60;
			var _s = (s < 10) ? ("0" + s.ToString()) : s.ToString();
			var keikaTime = h + ":" + _m + ":" + _s + "";
			/*
//			- unixKijunDt;
			
//			var __time = new TimeSpan(chat.vpos * 10000);
			var h = (int)(__timeSpan.TotalHours);
			var m = __timeSpan.Minutes;
			var s = __timeSpan.Seconds;
			*/
//			- new TimeSpan(9,0,0);
			var c = (chat.premium == "3") ? "red" :
				((chat.premium == "7") ? "blue" : "black");
			if (chat.root == "control") c = "red";
			
			rm.form.addComment(keikaTime, chat.contents, chat.userId, chat.score, isTimeShift, c);
			
		}
		private bool connectMessageServer() {
	    	util.debugWriteLine("connect message server");
	    	util.debugWriteLine("isretry " + isRetry + " isend " + isEndProgram);
	    	
	    	try {
		    	lock (this) {
					var  isPass = (TimeSpan.FromSeconds(3) > (DateTime.Now - lastWebsocketConnectTime));
					lastWebsocketConnectTime = DateTime.Now;
					if (isPass) 
						Thread.Sleep(3000);
				}
	//			msUri = util.getRegGroup(message, "(ws://.+?)\"");
	//			msThread = util.getRegGroup(message, "threadId\":\"(.+?)\"");
		//		String msReq = "[truncated][{\"ping\":{\"content\":\"rs:0\"}},{\"ping\":{\"content\":\"ps:0\"}},{\"thread\":{\"thread\":\"" + msThread + "\",\"version\":\"20061206\",\"fork\":0,\"user_id\":\"" + userId + "\",\"res_from\":-1000,\"with_global\":1,\"scores\":1,\"nicoru\":0}},{\"ping\":{\"content\":\"pf:0\"}},{\"ping\":{\"content\":\"rf:0\"}}]\0";
		
				
	//			msReq = "[{\"ping\":{\"content\":\"rs:0\"}},{\"ping\":{\"content\":\"ps:0\"}},{\"thread\":{\"thread\":\"" + msThread + "\",\"version\":\"20061206\",\"fork\":0,\"user_id\":\"" + userId + "\",\"res_from\":" + res_from + ",\"with_global\":1,\"scores\":0,\"nicoru\":0}},{\"ping\":{\"content\":\"pf:0\"}},{\"ping\":{\"content\":\"rf:0\"}}]";
	//			msReq = "[{\"ping\":{\"content\":\"rs:0\"}},{\"ping\":{\"content\":\"ps:0\"}},{\"thread\":{\"thread\":\"" + msThread + "\",\"version\":\"20061206\",\"fork\":0,\"user_id\":\"" + userId + "\",\"res_from\":1,\"with_global\":1,\"scores\":0,\"nicoru\":0}},{\"ping\":{\"content\":\"pf:0\"}},{\"ping\":{\"content\":\"rf:0\"}}]";
				
		//		String msReq = "{\"thread\":{\"thread\":\"" + msThread + "\",\"version\":\"20061206\",\"fork\":0,\"user_id\":\"" + userId + "\",\"res_from\":-100,\"with_global\":1,\"scores\":1,\"nicoru\":0}}";
		//		String msReq = "{\"thread\":{\"thread\":\"" + msThread + "\",\"version\":\"20061206\",\"res_from\":-1000}}";
		//		String msReq = "<thread thread=\"" + msThread + "\" version=\"20061206\" res_from=\"-100\" />\0";
//				string msPort = util.getRegGroup(message, "jp:(.+?)/");
//				string msAddr = util.getRegGroup(message, "://(.+?):");
				
//				util.debugWriteLine(msAddr + " " + wi.msPort);
				util.debugWriteLine("msuri " + wi.msUri);
//				util.debugWriteLine("msreq " + wi.msReq);
				
				var header =  new List<KeyValuePair<string, string>>();
				header.Add(new KeyValuePair<string,string>("Sec-WebSocket-Protocol", "msg.nicovideo.jp#json"));
				//header.Add(new KeyValuePair<string,string>("Sec-WebSocket-Extensions", "permessage-deflate; client_max_window_bits"));
				header.Add(new KeyValuePair<string,string>("Sec-WebSocket-Extensions", "permessage-deflate"));
				header.Add(new KeyValuePair<string,string>("Sec-WebSocket-Version", "13"));
//				header.Add(new KeyValuePair<string,string>("Sec-WebSocket-Protocol", "msg.nicovideo.jp#json"));
				var cookieKV = new List<KeyValuePair<string, string>>();
//				foreach (Cookie kv in container.GetCookies(new Uri(wi.msUri)))
//					cookieKV.Add(new KeyValuePair<string, string>(kv.Name, kv.Value));
//				msUri = "ws://nmsg.nicovideo.jp:2581/websocket";
				wsc = new WebSocket(msUri, "", null, header, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36", "https://cas.nicovideo.jp", WebSocketVersion.Rfc6455, null, SslProtocols.Tls12);
//				wsc = new WebSocket(msUri, "", null, header, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36", "https://cas.nicovideo.jp", WebSocketVersion.Rfc6455);
//				wsc.Security.AllowUnstrustedCertificate = true;
//				wsc.Security.
				wsc.Opened += onWscOpen;
				wsc.Closed += onWscClose;
				wsc.MessageReceived += onWscMessageReceive;
//				wsc.DataReceived += onWscDataReceive;
				wsc.Error += onWscError;
	//			himodukeWS[0] = _ws;
	//			himodukeWS[1] = wsc;
				
		        util.debugWriteLine("wi msuri " + wi.msUri);
		        
		        wsc.Open();
		        	        
		        
				util.debugWriteLine("ms start");
				var _ws = wsc; 
					Thread.Sleep(5000);
					if (_ws.State == WebSocketState.Connecting) 
						wsc.Close();
	    	} catch (Exception ee) {
	    		util.debugWriteLine("wsc connect exception " + ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
	    		return false;
	    	}
	    	return true;
		}
		public void stopRecording() {
			util.debugWriteLine("stop recording");
			try {
				if (wsc != null && wsc.State != WebSocketState.Closed && wsc.State != WebSocketState.Closing) {
					util.debugWriteLine("state close wsc " + WebSocketState.Closed + " " + wsc.State);					
					wsc.Close();
				}
			} catch (Exception e) {
				util.debugWriteLine("wsc close error");
				util.debugWriteLine(e.Message + e.StackTrace);
			}
			try {
				if (wscPongCancelToken != null) wscPongCancelToken.Cancel();
			} catch (Exception e) {
				util.debugWriteLine("comment sw close error");
				util.debugWriteLine(e.Message + e.StackTrace);
			}
			/*
			try {
				if (rec != null) rec.stopRecording();
			} catch (Exception e) {
				util.debugWriteLine("rec close error");
				util.debugWriteLine(e.Message + e.StackTrace);
			}
			isRetry = false;
			*/
		}
		private bool isEndedProgram() {
			var url = "http://live2.nicovideo.jp/watch/" + lvid;
			var isPass = (DateTime.Now - lastEndProgramCheckTime < TimeSpan.FromSeconds(5)); 
			if (isPass) return false;
			lastEndProgramCheckTime = DateTime.Now;
			
			var a = new System.Net.WebHeaderCollection();
			var res = util.getPageSource(url, ref a, container);
			util.debugWriteLine("isendedprogram url " + url + " res==null " + (res == null));
//			util.debugWriteLine("isendedprogram res " + res);
			if (res == null) return false;
			var isEnd = res.IndexOf("\"content_status\":\"closed\"") != -1; 
			util.debugWriteLine("is ended program " + isEnd);
			return isEnd; 
		}
	}
}

