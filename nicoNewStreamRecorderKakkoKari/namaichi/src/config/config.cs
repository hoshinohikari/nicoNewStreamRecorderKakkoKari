﻿/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/04/29
 * Time: 20:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using namaichi.info;

namespace namaichi.config {
/// <summary>
/// Description of config.
/// </summary>
public class config
{
	private Configuration cfg;
	public Dictionary<string, string> defaultConfig;
	public Dictionary<string, string> argConfig = new Dictionary<string, string>();
	public string brokenCopyFile = null;
	public AccountInfo argAi = null;
	
	public config()
	{
		cfg = getConfig();
		defaultMergeFile();
        
		if (get("EngineMode") == "2")
			set("EngineMode", "0");
		//set("EngineMode", "2");
 	}
	public Configuration getConfig() {
		for (var i = 0; i < 5; i++) {
			try {
				var jarPath = util.getJarPath();
				var configFile = jarPath[0] + "\\" + jarPath[1] + ".config";
				if (i > 3) System.IO.File.Delete(configFile);
				//util.debugWriteLine(configFile);
		        var exeFileMap = new System.Configuration. ExeConfigurationFileMap { ExeConfigFilename = configFile };
		        var cfg     = ConfigurationManager.OpenMappedExeConfiguration(exeFileMap, ConfigurationUserLevel.None);
		        return cfg;
			} catch (Exception e) {
				util.debugWriteLine("getconfig " + e.Message + " " + e.StackTrace + " " + e.TargetSite);
				if (e.Message.IndexOf("レベルのデータ") > -1) break;
				Thread.Sleep(3000);
				continue;
				//ルート レベルのデータが無効です。
			}
		}
		resetConfig();
		return this.cfg;
	}
	public void set(string key, string value) {
		if (key.IndexOf("user_session") == -1 && 
				key.IndexOf("account") == -1)
			util.debugWriteLine("config set " + key + " " + value);
		else util.debugWriteLine("config set " + key);
		for (var i = 0; i < 100; i++) {
			cfg = getConfig();
			
			
			var keys = cfg.AppSettings.Settings.AllKeys;
			if (System.Array.IndexOf(keys, key) < 0)
				cfg.AppSettings.Settings.Add(key, value);
			else cfg.AppSettings.Settings[key].Value = value;
			try {
				//cfg.Save();
				var o = cfg.FilePath.TrimEnd(new char[]{'_'});
				cfg.SaveAs(o + "_", ConfigurationSaveMode.Modified, true);
				var exeFileMap = new System.Configuration. ExeConfigurationFileMap { ExeConfigFilename = o + "_" };
		        var cfg2 = ConfigurationManager.OpenMappedExeConfiguration(exeFileMap, ConfigurationUserLevel.None);
				File.Copy(o + "_", o, true);
				File.Delete(o + "_");
				return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
				System.Threading.Thread.Sleep(500);
				continue;
			}
		}
	}
	public void set(List<KeyValuePair<string, string>> l) {
		foreach (var _l in l) {
			if (_l.Key.IndexOf("user_session") == -1 && 
					_l.Key.IndexOf("account") == -1)
				util.debugWriteLine("config set " + _l.Key + " " + _l.Value);
			else util.debugWriteLine("config set " + _l.Key);
		}
		for (var i = 0; i < 100; i++) {
			cfg = getConfig();
			
			var keys = cfg.AppSettings.Settings.AllKeys;
			
			foreach (var _l in l) {
				if (System.Array.IndexOf(keys, _l.Key) < 0)
					cfg.AppSettings.Settings.Add(_l.Key, _l.Value);
				else cfg.AppSettings.Settings[_l.Key].Value = _l.Value;
			}
			try {
				var o = cfg.FilePath.TrimEnd(new char[]{'_'});
				cfg.SaveAs(o + "_", ConfigurationSaveMode.Modified, true);
				var exeFileMap = new System.Configuration. ExeConfigurationFileMap { ExeConfigFilename = o + "_" };
		        var cfg2 = ConfigurationManager.OpenMappedExeConfiguration(exeFileMap, ConfigurationUserLevel.None);
				File.Copy(o + "_", o, true);
				File.Delete(o + "_");
				return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
				System.Threading.Thread.Sleep(500);
				continue;
			}
		}
	}
	public string get(string key) {
		util.debugWriteLine("config get " + key);
		try {
			if (key.IndexOf("user_session") == -1 && 
			    	key.IndexOf("account") == -1) {
				util.debugWriteLine(key + " " + cfg.AppSettings.Settings[key].Value);
			} else util.debugWriteLine(key);
		} catch (Exception e) {
			util.debugWriteLine("config get exception " + key + " " + e.Message + e.Source + e.StackTrace + e.TargetSite);
			return null;
		}
		try {
			if (argConfig != null && argConfig.ContainsKey(key)) 
				return argConfig[key];
			return cfg.AppSettings.Settings[key].Value;
		} catch (Exception) {
			return null;
		}
	}
	private void defaultMergeFile() {
		defaultConfig = new Dictionary<string, string>(){
			{"accountId",""},
			{"accountPass",""},
			{"user_session",""},
			{"user_session_secure",""},
			{"browserNum","1"},
//			{"isAllBrowserMode","true"},
			{"issecondlogin","false"},
			{"age_auth","0"},
			
			{"IsHokan","true"},
			{"accountId2",""},
			{"accountPass2",""},
			{"user_session2",""},
			{"user_session_secure2",""},
			{"browserNum2","1"},
			{"issecondlogin2","false"},
			{"cookieFile2",""},
			{"iscookie2","false"},
			
			{"useProxy","false"},
			{"proxyAddress",""},
			{"proxyPort",""},
			{"localServerPortList","7997"},
			
			{"IsdefaultBrowserPath","true"},
			{"browserPath",""},
			{"Isminimized","false"},
			{"IscloseExit","true"},
			{"IsfailExit","false"},
			{"IsgetComment","true"},
			{"IsmessageBox","false"},
			{"IshosoInfo","false"},
			{"IsDescriptionTag","false"},
			{"Islog","false"},
			{"IstitlebarInfo","true"},
			{"Islimitpopup","true"},
			{"Isretry","true"},
			{"IsdeleteExit","false"},
			{"IsgetcommentXml","true"},
			{"IsgetcommentXmlInfo","false"},
			{"IsgetcommentStore","false"},
			{"IsCommentConvertSpace","false"},
			{"commentConvertStr"," "},
			{"IsSaveCommentOnlyRetryingRec","false"},
			{"IsDisplayComment","true"},
			{"IsNormalizeComment","false"},
			{"CommentReplaceText",""},
			
			{"IstitlebarSamune","true"},
			{"IsautoFollowComgen","false"},
			{"qualityRank","0,1,2,3,4,5,6,7,8,9"},
			{"qualityList","{\"0\":\"3Mbps(super_high)\",\"1\":\"2Mbps(high)\",\"2\":\"1Mbps(normal)\",\"3\":\"384kbps(low)\",\"4\":\"192kbps(super_low)\",\"5\":\"音声のみ(audio_high)\",\"6\":\"6Mbps(6Mbps1080p30fps)\",\"7\":\"8Mbps(8Mbps1080p60fps)\",\"8\":\"4Mbps(4Mbps720p60fps)\",\"9\":\"音声のみ(audio_only)\"}"},
			{"chPlusQualityRank","240p(426x240),1080p(1920x1080),360p(640x360),720p(1280x720)"},
			{"IsMiniStart","false"},
			{"IsConfirmCloseMsgBox","true"},
			{"IsRecBtnOnlyMouse","false"},
			{"reserveMessage","ダイアログで確認"},
			
			{"IsLogFile","false"},
			{"IsSegmentNukeInfo","true"},
			{"IsNotSleep","false"},
			{"IsRestoreLocation","false"},
			
			{"segmentSaveType","0"},
			{"IsRenketuAfter","true"},
//			{"IsAfterRenketuFFmpeg","false"},
//			{"IsDefaultEngine","true"},
			{"EngineMode","0"},
			{"anotherEngineCommand",""},
			{"IsDefaultRtmpPath","true"},
			{"rtmpPath",""},
			{"latency","3.0"},
			{"IsNoRecordRtmpBlackScreen","false"},
			{"IsChaseRecord","false"},
			{"IsArgChaseRecFromFirst","false"},
			{"IsOnlyTimeShiftChase","true"},
			{"IsChaseReserveRec","false"},
			
			{"IsUsePlayer","true"},
			{"IsDefaultPlayer","true"},
			{"IsUseCommentViewer","true"},
			{"IsDefaultCommentViewer","true"},
			{"anotherPlayerPath",""},
			{"anotherCommentViewerPath",""},
			{"Is184","true"},
			{"playerArgs",""},
			
			{"tsStartTimeMode","0"},
			{"tsEndTimeMode","0"},
			{"tsStartSecond","0"},
			{"tsEndSecond","0"},
			{"tsIsDeletePosTime","true"},
			{"tsBaseOpenTimeStart","false"},
			{"tsBaseOpenTimeEnd","false"},
			{"tsIsRenketu","false"},
			{"IsVposStartTime","true"},
			{"IsAfterStartTimeComment","false"},
			{"IsBeforeEndTimeComment","false"},
			
			//{"IsUrlList","false"},
			//{"IsM3u8List","false"},
			//{"M3u8UpdateSeconds","5"},
			//{"IsOpenUrlList","false"},
			//{"openUrlListCommand","notepad {i}"},
			{"afterConvertMode","0"},
			{"afterConvertModeCmd",""},
			{"IsSoundEnd","false"},
			{"soundPath",""},
			{"IsSoundDefault","true"},
			{"soundVolume","50"},
			
			{"cookieFile",""},
			{"iscookie","false"},
			{"IsBrowserShowAll","false"},
			{"recordDir",""},
			{"IsSecondRecordDir","false"},
			{"secondRecordDir",""},
			{"IsdefaultRecordDir","true"},
			{"IscreateSubfolder","true"},
			{"subFolderNameType","1"},
			{"fileNameType","1"},
			{"filenameformat","{Y}年{M}月{D}日{h}時{m}分{0}_{1}_{2}_{3}_{4}"},
			{"ffmpegopt",""},
			{"Height","400"},
			{"Width","735"},
			{"X",""},
			{"Y",""},
			{"fontSize","9"},
			{"IsTray","false"},
			
			{"chPlus_access_token",""},
			{"chPlus_refreshCookie", ""},
			
			{"defaultControllerX","100"},
			{"defaultControllerY","100"},
			{"volume","50"},
			{"defaultCommentFormX","100"},
			{"defaultCommentFormY","100"},
			{"defaultCommentFormWidth","500"},
			{"defaultCommentFormHeight","520"},
			
			{"rokugaTourokuWidth","950"},
			{"rokugaTourokuHeight","500"},
			{"rokugaTourokuMaxRecordingNum","10"},
			{"IsDuplicateConfirm","false"},
			{"rokugaTourokuQualityRank","0,1,2,3,4,5,6,7,8,9"},
			{"IsDeleteConfirmMessageRt","false"},
			
			{"recBackColor","-1"},
			{"recForeColor","-16777216"},
			{"recLinkColor","-16776961"},
			{"tourokuBackColor","-986896"},
			{"tourokuForeColor","-16777216"},
		};
		try {
			var buf = new Dictionary<string,string>();
			foreach (var k in cfg.AppSettings.Settings.AllKeys) {
				buf.Add(k, cfg.AppSettings.Settings[k].Value);
			}
			
			cfg.AppSettings.Settings.Clear();
			foreach (var k in defaultConfig.Keys) {
				var v = (buf.ContainsKey(k)) ? buf[k] : defaultConfig[k];
				cfg.AppSettings.Settings.Add(k, v);
			}
			try {
				cfg.Save();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
			}
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			resetConfig();
		}
		
		// Dictionary<string, string>
	}
	public void saveFromForm(Dictionary<string, string> formData) {
		cfg = getConfig();
		
		foreach (var k in formData.Keys) {
			cfg.AppSettings.Settings[k].Value = formData[k];
			//util.debugWriteLine(k + formData[k]);
		}		
		try {
			cfg.Save();
		} catch (Exception e) {
			util.debugWriteLine(e.Message + " " + e.StackTrace);
		}
	}
	private bool resetConfig() {
		var jarPath = util.getJarPath();
		var configFile = jarPath[0] + "\\" + jarPath[1] + ".config";
		if (File.Exists(configFile)) {
			var n = DateTime.Now;
			var fn = jarPath[0] + "\\" + n.ToString("yyyyMMddhhmmss") + "ニコ生新配信録画ツール（仮.config";
			try {
				File.Copy(configFile, fn);
				File.Delete(configFile);
				brokenCopyFile = fn;
				cfg = getConfig();
				defaultMergeFile();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				try {
					for (var i = 0; i < 10000; i++) {
						fn = configFile + i.ToString();
						if (File.Exists(fn)) continue;
						File.Copy(configFile, fn);
						File.Delete(configFile);
						brokenCopyFile = fn;
						cfg = getConfig();
						defaultMergeFile();
						break;
					}
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					return false;
				}
			}
		}
		return true;
	}
	public static Dictionary<int, string> qualityList = new Dictionary<int, string>() {
			{0, "3Mbps(super_high)"},
			{1, "2Mbps(high)"}, {2, "1Mbps(normal)"},
			{3, "384kbps(low)"}, {4, "192kbps(super_low)"},
			{5, "音声のみ(audio_high)"}, {6, "6Mbps(6Mbps1080p30fps)"},
			{7, "8Mbps(8Mbps1080p60fps)"}, {8, "4Mbps(4Mbps720p60fps)"},
			{9, "音声のみ(audio_only)"}
		};
	}
}