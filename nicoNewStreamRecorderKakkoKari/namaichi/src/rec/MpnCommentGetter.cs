﻿/*
 * Created by SharpDevelop.
 * User: ajkkh
 * Date: 2024/08/05
 * Time: 17:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using namaichi.info;
using namaichi.utility;
using Newtonsoft.Json;
using ProtoBuf;

namespace namaichi.rec
{
	/// <summary>
	/// Description of MpnCommentGetter.
	/// </summary>
	public class MpnCommentGetter
	{
		RecordingManager rm;
		RecordFromUrl rfu;
		WebSocketRecorder wr;
		
		string mpnViewUri = null;
		int vposBaseTimeUnix = 0;
		string mpnHashedUserId = null;
		List<string> gotCommentIDList = new List<string>();
		bool isCatchUpChase = false;
		
		string at = "now";
		bool isEnd = false;
		object messageLock = new object();
		
		public MpnCommentGetter(RecordingManager rm, RecordFromUrl rfu, WebSocketRecorder wr)
		{
			this.rm = rm;
			this.rfu = rfu;
			this.wr = wr;
			
			var isSaveFromBiginning = 
					wr.ri.si.isTimeShift && !wr.ri.isRealtimeChase;
			at = !isSaveFromBiginning ? "now" : 
					wr.ri.si._openTime.ToString();
		}
		public void get(string wsMsg) {
			util.debugWriteLine("mpn comment getter get " + wsMsg);
			if (!setMpnInfo(wsMsg)) return;
			
			wr.commentFileInit();
			
			if (rfu == rm.rfu && wr.IsRetry) {
				wr.addDebugBuf("connect message server mpn");
		    	wr.addDebugBuf("isretry " + wr.IsRetry + " isend " + wr.isEndProgram);
		    	try {
		    		viewProtoProcess();
		    	} catch (Exception e) {
		    		util.debugWriteLine("connectMessageServerMpn error " + e.Message + e.Source + e.StackTrace);
		    	}
			}
		}
		public bool setMpnInfo(string message) {
			mpnViewUri = util.getRegGroup(message, "\"viewUri\":\"(.+?)\"");
			if (mpnViewUri == null) {
				rm.form.addLogText("view uriが見つかりませんでした");
				return false;
			}
			var _vposBaseTime = util.getRegGroup(message, "\"vposBaseTime\":\"(.+?)\"");
			if (_vposBaseTime == null) {
				rm.form.addLogText("vposBaseTimeが見つかりませんでした");
				//return false;
				_vposBaseTime = wr.ri.si.vposBaseTime.ToString();
				util.debugWriteLine("not found vposBaseTime mpn");
			}
			vposBaseTimeUnix = util.getUnixTime(DateTime.Parse(_vposBaseTime));
			
			mpnHashedUserId = util.getRegGroup(message, "\"hashedUserId\":\"(.+?)\"");
			if (mpnHashedUserId == null) {
				util.debugWriteLine("not found hashedUserId mpn");
			}
			wr.serverTime = util.getUnixTime() - 3600 * 9;
			return true;
		}
		private bool receiveFromProtoUri<T>(string uri) {
			byte[] r = new Byte[1000000];
			var ms = new MemoryStream();
			
			var isUsingStream = !wr.ri.si.isTimeShift || (wr.ri.isRealtimeChase) || isCatchUpChase;
			if (isUsingStream) {
				HttpWebRequest req = null;
				try {
					HttpWebResponse res = null;
					for (var i = 0; i < 3; i++) {
						res = util.sendRequest(uri, null, null, "GET", null, out req);
						if (res == null) {
							Thread.Sleep(1000);
							continue;
						}
						if (i != 0) {
							util.debugWriteLine("get after retry uri " + uri);
						}
						break;
					}
					if (res == null) {
						util.debugWriteLine("not get after retry uri " + uri);
		    			rm.form.addLogText("コメント情報のuriに接続できませんでした。" + uri);
		    			return false;
		    		}
					
					using (var rs = res.GetResponseStream()) {
						var readNum = rs.Read(r, 0, r.Length);
						while (readNum != 0) {
							ms.Write(r, 0, readNum);
							ms.Flush();
							
							var a = ms.ToArray();
							var protoList = getDataToProtoList<T>(ms.ToArray(), typeof(T));
							if (protoList != null && protoList.Count > 0) {
								passToHandler<T>(protoList);
								ms.SetLength(0);
							}
							
							readNum = rs.Read(r, 0, r.Length);
						}
					}
				} catch (Exception e) {
					util.debugWriteLine("protoUri read error " + e.Message + e.Source + e.StackTrace);
				} finally {
					try {
						if (req != null) req.Abort();
					} catch (Exception e) {
						util.debugWriteLine(e.Message + e.Source + e.StackTrace);
					}
				}
			} else {
				var beforeT = DateTime.Now;
				var res = util.getFileBytes(uri, null);
				var afterT = DateTime.Now;
				if (res == null) return false;
				var protoList = getDataToProtoList<T>(res, typeof(T));
				if (protoList != null && protoList.Count > 0) {
					passToHandler(protoList);
				}
				if (wr.ri.isChase && afterT - beforeT > TimeSpan.FromSeconds(10)) {
					isCatchUpChase = true;
				}
			}
    		
			return true;
		}
		void passToHandler<T>(List<T> protoList) {
			if (protoList is List<ChunkedEntry>)
				onChunkedEntryReceived(protoList as List<ChunkedEntry>);
			else if (protoList is List<ChunkedMessage>)
				onChunkedMessageReceived(protoList as List<ChunkedMessage>);
		}
		private List<T> getDataToProtoList<T>(byte[] b, Type t) {
			var protoList = new List<T>();
			
			var rList = new List<byte>(b);
			var dataLen = 0;
			ulong len;
			List<byte> rrr = null;
			for (var i = 0; i < b.Length && rList.Count != 0; i++) {
				dataLen = 0;
				len = 0;
				try {
					len = VarintBitConverter.ToUInt64(rList.ToArray(), out dataLen);
					if (len == 0) break;
					
	    			util.debugWriteLine("view res len " + b.Length + " rlistLen " + rList.Count + " datalen " + dataLen + " len " + len);
					
					rrr = rList.GetRange(dataLen, (int)len);
					var _ms = new MemoryStream(rrr.ToArray());
					
					var cee = Serializer.Deserialize<T>(_ms);
					
					if (t == typeof(ChunkedMessage)) {
						util.debugWriteLine("des ok " + cee);
						
						rrr = rList.GetRange(dataLen, (int)len);
						_ms = new MemoryStream(rrr.ToArray());
					}
					
					i += dataLen + (int)len;
					rList.RemoveRange(0, dataLen + (int)len);
					protoList.Add(cee);
					
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + " " + t.FullName);
					if (dataLen != 0 || len != 0) {
						i += dataLen + (int)len;
						rList.RemoveRange(0, dataLen + (int)len);
					}
				}
			}
			if (t == typeof(ChunkedEntry)) {
				util.debugWriteLine("entry ret ");
			}
			return protoList;
		}
		bool des<T>(MemoryStream ms) {
			try {
				var cee2 = Serializer.Deserialize<T>(ms);
					return true;
			} catch (Exception e) {
				util.debugWriteLine("deserialize error " + e.Message + e.Source + e.StackTrace);
				return false;
			}
		}
		void viewProtoProcess() {
			while (rm.rfu == rfu && wr.IsRetry && !isEnd) {
				Action<string> a = (d) => {util.debugWriteLine(d);};
				var isOk = receiveFromProtoUri<ChunkedEntry>(mpnViewUri + "?at=" + at);
	    		if (!isOk) {
	    			rm.form.addLogText("コメントのView Uriから正常に情報を取得できませんでした");
	    			Thread.Sleep(1000);
	    			continue;
	    		}
			}
		}
		void onChunkedEntryReceived(List<ChunkedEntry> l) {
			foreach (var ce in l) {
				if (ce.Next != null) {
					var newAt = ce.Next.At.ToString();
					if (at == newAt) Thread.Sleep(1000);
					at = newAt;
					util.debugWriteLine("view at change " + at);
				}
				if (ce.Segment != null) {
					messageSegmentProcess<ChunkedMessage>(ce.Segment.Uri);
				}
				if (ce.Previous != null) {
					//messageSegmentProcess<ChunkedMessage>(ce.Previous.Uri);
				}
				if (wr.ri.si.isTimeShift && !wr.ri.isChase && 
				    	ce.Segment != null && ce.Segment.Until.Seconds > 
				    	wr.ri.si.endTime + 120 && !isEnd) {
					isEnd = true;
					wr.closeWscProcess();
					rm.form.addLogText("コメントの保存を完了しました");
					
				}
			}
		}
		void onChunkedMessageReceived(List<ChunkedMessage> l) {
			foreach (var cm in l) {
				lock(messageLock) {
					if (cm.Message != null || cm.State != null) {
						if (gotCommentIDList.IndexOf(cm.meta.Id) > -1)
						    continue;
						
						var chatXml = getMsgProtoToXML(cm);
						var json = JsonConvert.SerializeXNode(chatXml);
						json = json.Replace("\"#text\"", "\"content\"");
						wr.onCommentMessageReceiveCore(json);
						gotCommentIDList.Add(cm.meta.Id);
						
						var isTimeshiftRec = wr.ri.si.isTimeShift && !wr.ri.isChase; 
						if (isTimeshiftRec && gotCommentIDList.Count % 100 == 0)
							rm.form.addLogText(gotCommentIDList.Count + "件のコメントを取得しました");
					}
				}
			}
		}
		void messageSegmentProcess<T>(string segmentUri) {
			var isOk = false;
			for (var i = 0; i < 5; i++) {
				isOk = receiveFromProtoUri<T>(segmentUri);
				if (!isOk) {
					Thread.Sleep(1000);
					continue;
				}
				break;
			}
			util.debugWriteLine("messageSegmentProcess " + isOk);
			if (!isOk) {
    			rm.form.addLogText("コメントのSegment Uriから正常に情報を取得できませんでした");
    			return;
    		}
		}
		XDocument getMsgProtoToXML(ChunkedMessage msgProto) {
			var _xml = new XDocument();
			_xml.Add(new XElement("chat"));
			try {
				if (msgProto.Message != null && 
				    	msgProto.Message.Chat != null) {
					var chat = msgProto.Message.Chat;
					var content = chat.Content != null ? chat.Content : "";
					var premium = ((int)chat.account_status).ToString();
					var modifier = chat.modifier != null ? getModifier(chat.modifier) : "";
					msgProto.Message.Chat.Vpos -= ((int)wr.ri.si._openTime - vposBaseTimeUnix + 32400) * 100;
					
					_xml = getXDocument(content, premium,
					        chat.HashedUserId, modifier, chat.Name,
					        chat.No.ToString(), chat.RawUserId.ToString(), 
					        chat.Vpos.ToString(), msgProto.meta.At.Seconds,
					        msgProto.meta.At.Nanos);
				} else if (msgProto.Message != null && msgProto.Message.SimpleNotification != null) {
					var vpos = (msgProto.meta.At.Seconds - wr.ri.si._openTime) * 100;
					var notification = getNotification(msgProto.Message.SimpleNotification);
					_xml = getXDocument(notification, "3",
							"", "", "",
							"", "", vpos.ToString(), 
							msgProto.meta.At.Seconds, 
							msgProto.meta.At.Nanos);
				} else if (msgProto.State != null) {
					var vpos = (msgProto.meta.At.Seconds - wr.ri.si._openTime) * 100;
					var content = getStateComment(msgProto.State);
					_xml = getXDocument(content, "3",
							"", "", "",
							"", "", vpos.ToString(), 
							msgProto.meta.At.Seconds, 
							msgProto.meta.At.Nanos);
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return null;
			}
			util.debugWriteLine(_xml);
			return _xml;
			
		}
		XDocument getXDocument(string content, string premium, 
				string userId, string modifier, string name, string no,
				string raw_user_id, string vpos, long date, int date_usec) {
			var _xml = new XDocument();
			_xml.Add(new XElement("chat"));
			
			if (string.IsNullOrEmpty(userId) && 
			    	!string.IsNullOrEmpty(raw_user_id)) {
				userId = raw_user_id;
				raw_user_id = "";
			}
			_xml.Root.Add(content);
			if (!string.IsNullOrEmpty(premium))
				_xml.Root.SetAttributeValue("premium", premium);
			if (!string.IsNullOrEmpty(userId) && userId != "0")
				_xml.Root.SetAttributeValue("user_id", userId);
			if (!string.IsNullOrEmpty(modifier))
				_xml.Root.SetAttributeValue("modifier", modifier);
			if (!string.IsNullOrEmpty(name))
				_xml.Root.SetAttributeValue("name", name);
			if (!string.IsNullOrEmpty(no))
				_xml.Root.SetAttributeValue("no", no);
			if (!string.IsNullOrEmpty(raw_user_id) && raw_user_id != "0")
				_xml.Root.SetAttributeValue("raw_user_id", raw_user_id);
			if (!string.IsNullOrEmpty(vpos))
				_xml.Root.SetAttributeValue("vpos", vpos);
			_xml.Root.SetAttributeValue("date", date.ToString());
			_xml.Root.SetAttributeValue("date_usec", date_usec.ToString());
			return _xml;
		}
		string getModifier(Chat.Modifier modifier) {
			var m = new List<string>();
			if (modifier.font.ToString() != "Defont") m.Add(modifier.font.ToString());
			if (modifier.full_color != null) m.Add(modifier.full_color.ToString());
			if (modifier.NamedColor.ToString() != "White") m.Add(modifier.NamedColor.ToString());
			if (modifier.opacity.ToString() != "Normal") m.Add(modifier.opacity.ToString());
			if (modifier.Position.ToString() != "Naka") m.Add(modifier.Position.ToString());
			if (modifier.size.ToString() != "Medium") m.Add(modifier.size.ToString());
			return string.Join(" ", m.ToArray());
		}
		string getNotification(SimpleNotification n) {
			var m = new List<string>();
			if (n.Cruise != null) m.Add(n.Cruise);
			if (n.Emotion != null) m.Add(n.Emotion);
			if (n.Ichiba != null) m.Add(n.Ichiba);
			if (n.ProgramExtended != null) m.Add(n.ProgramExtended);
			if (n.Quote != null) m.Add(n.Quote);
			if (n.RankingIn != null) m.Add(n.RankingIn);
			if (n.RankingUpdated != null) m.Add(n.RankingUpdated);
			if (n.Visited != null) m.Add(n.Visited);
			return string.Join(" ", m.ToArray());
		}
		string getStateComment(NicoliveState s) {
			var l = new List<string>();
			if (s.Marquee != null) {
				var opComment = s.Marquee.display.OperatorComment;
				l.Add(getOperatorCommentStr(opComment));
			}
			if (s.Enquete != null) {
				if (!string.IsNullOrEmpty(s.Enquete.Question)) {
					l.Add(s.Enquete.Question);
					l.Add(string.Join(" ", s.Enquete.Choices.Select(x => x.Description).ToArray()));
					util.debugWriteLine(l);
				}
				
			}
			if (s.ProgramStatus != null) {
				l.Add(s.ProgramStatus.state.ToString());
			}
			if (s.CommentLock != null) {
				l.Add(s.CommentLock.status.ToString());
			}
			if (s.CommentMode != null) {
				l.Add(s.CommentMode.layout.ToString());
			}
			if (s.ModerationAnnouncement != null)
				l.Add(s.ModerationAnnouncement.Message);
			if (s.MoveOrder != null) {
				var m = s.MoveOrder;
				if (m.Jump != null)
					l.Add(m.Jump.Message + " " + m.Jump.Content);
				if (m.Redirect != null)
					l.Add(m.Redirect.Message + " " + m.Redirect.Uri);
			}
			if (s.Statistics != null) {
				var st = s.Statistics;
				//l.Add("来場者:" + st.Viewers);
				//l.Add("コメント:" + st.Comments);
				if (st.AdPoints != 0) l.Add("広告:" + st.AdPoints);
				if (st.GiftPoints != 0) l.Add("ギフト:" + st.GiftPoints);
			}
			if (s.TrialPanel != null) {
				l.Add(s.TrialPanel.panel.ToString());
				l.Add(s.TrialPanel.UnqualifiedUser.ToString());
			}
			return string.Join(" ", l.ToArray());
		}
		string getOperatorCommentStr(OperatorComment c) {
			var m = new List<string>();
			if (!string.IsNullOrEmpty(c.Name)) m.Add(c.Name);
			if (!string.IsNullOrEmpty(c.Content)) m.Add(c.Content);
			if (!string.IsNullOrEmpty(c.Link)) m.Add("(" + c.Link + ")");
			if (c.Modifier != null) m.Add(getModifier(c.Modifier));
			return string.Join(" ", m.ToArray());
		}
	}
}
