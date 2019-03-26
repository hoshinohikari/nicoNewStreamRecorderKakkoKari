﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/07/26
 * Time: 16:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using namaichi.info;

namespace namaichi
{
	/// <summary>
	/// Description of TimeShiftOptionForm.
	/// </summary>
	public partial class TimeShiftOptionForm : Form
	{
		private string[] lastFileTime;
		private string segmentSaveType;
		public TimeShiftConfig ret = null;
		private config.config config;
		public TimeShiftOptionForm(string[] lastFileTime, 
				string segmentSaveType, config.config config)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.lastFileTime = lastFileTime;
			this.segmentSaveType = segmentSaveType;
			
			if (lastFileTime != null)
				lastFileInfoLabel.Text = "(" + lastFileTime[0] + 
					"時間" + lastFileTime[1] + "分" + lastFileTime[2] + "秒まで録画済み)";
			else {
				lastFileInfoLabel.Text = "(前回の録画ファイルが見つかりませんでした)";
				isRenketuLastFile.Enabled = false;
				isFromLastTimeRadioBtn.Enabled = false;
			}
			isRenketuLastFile.Visible = (segmentSaveType == "0");
			
			
			var isUrlList = bool.Parse(config.get("IsUrlList"));
			var openListCommand = config.get("openUrlListCommand");
			
			isUrlListChkBox.Checked = isUrlList;
			openListCommandText.Text = openListCommand;
			openListCommandText.Enabled = isUrlList;
			
			if (bool.Parse(config.get("IsM3u8List")))
				isM3u8RadioBtn.Checked = true;
			updateListSecondText.Text = config.get("M3u8UpdateSeconds");
			isOpenListCommandChkBox.Checked = bool.Parse(config.get("IsOpenUrlList"));
			isSetVposStartTime.Checked = bool.Parse(config.get("IsVposStartTime"));
			updateTimeShiftStartTimeChkBox();
			updateIsM3u8RadioBtn_CheckedChanged();
			updateIsOpenListCommandChkBoxCheckedChanged();
			updateUrlListChkBoxCheckedChanged();
			updateIsManualEndTimeRadioBtn();
			this.config = config;
		}
		private void updateTimeShiftStartTimeChkBox() {
			//isRenketuLastFile.Enabled = !isStartTimeRadioBtn.Checked;
			hText.Enabled = isStartTimeRadioBtn.Checked;
			//hLabel.Enabled = isStartTimeRadioBtn.Checked;
			mText.Enabled = isStartTimeRadioBtn.Checked;
			//mLabel.Enabled = isStartTimeRadioBtn.Checked;
			sText.Enabled = isStartTimeRadioBtn.Checked;
			//sLabel.Enabled = isStartTimeRadioBtn.Checked;
		}	
		void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void okBtn_Click(object sender, EventArgs e)
		{
			var startType = (isStartTimeRadioBtn.Checked || isMostStartTimeRadioBtn.Checked) ? 0 : 1;
			var startTimeMode = (isMostStartTimeRadioBtn.Checked ? 0 :((isStartTimeRadioBtn.Checked) ? 1 : 2));
			var endTimeMode = isEndTimeRadioBtn.Checked ? 0 : 1;
			
			
			var _h = (startType == 0) ? hText.Text : lastFileTime[0];
			var _m = (startType == 0) ? mText.Text : lastFileTime[1];
			var _s = (startType == 0) ? sText.Text : lastFileTime[2];
			if (startTimeMode == 0) _h = _m = _s = "0";
			int h, formH;
			int m, formM;
			int s, formS;
			if (!int.TryParse(hText.Text, out formH) ||
			   	!int.TryParse(mText.Text, out formM) ||
			   	!int.TryParse(sText.Text, out formS) ||
			   	!int.TryParse(_h, out h) ||
			   	!int.TryParse(_m, out m) ||
			   	!int.TryParse(_s, out s)) {
				MessageBox.Show("開始時間に数字以外が指定されています");
				return;
			}
			
			var _endH = endHText.Text;
			var _endM = endMText.Text;
			var _endS = endSText.Text;
			if (endTimeMode == 0) _endH = _endM = _endS = "0";
			int endH, formEndH;
			int endM, formEndM;
			int endS, formEndS;
			if (!int.TryParse(endHText.Text, out formEndH) ||
			   	!int.TryParse(endMText.Text, out formEndM) ||
			   	!int.TryParse(endSText.Text, out formEndS) ||
			   	!int.TryParse(_endH, out endH) ||
			   	!int.TryParse(_endM, out endM) ||
			   	!int.TryParse(_endS, out endS)) {
				MessageBox.Show("終了時間に数字以外が指定されています");
				return;
			}
			
			var timeSeconds = (startTimeMode == 1) ? (h * 3600 + m * 60 + s) : 0;
			var endTimeSeconds = endH * 3600 + endM * 60 + endS;
			if (endTimeMode == 0) endTimeSeconds = 0;
			if (endTimeMode == 1 && (endH != 0 || endM != 0 || endS != 0) && 
			    	endTimeSeconds < timeSeconds) {
				MessageBox.Show("終了時間が開始時間より前に設定されています");
				return;
			}
			
			double updateSecond;
			if (!double.TryParse(updateListSecondText.Text, out updateSecond)) {
				MessageBox.Show("M3U8の更新間隔に数字以外が指定されています");
				return;
			}
			if (updateSecond <= 0.5) {
				MessageBox.Show("M3U8の更新間隔に0.5以下を指定することはできません");
				return;
			}
			/*
			var isUrlList = isUrlListChkBox.Checked;
			var openListCommand = openListCommandText.Text;
			var isM3u8List = isM3u8RadioBtn.Checked;
			var m3u8UpdateSeconds = updateSecond;
			var isOpenUrlList = isOpenListCommandChkBox.Checked;
			*/
			var isUrlList = false;
			var openListCommand = "";
			var isM3u8List = false;
			var m3u8UpdateSeconds = 5.1;
			var isOpenUrlList = false;
			
			ret = new TimeShiftConfig(startType, 
				h, m, s, endH, endM, endS, isRenketuLastFile.Checked, isUrlList, 
				openListCommand, isM3u8List, m3u8UpdateSeconds, isOpenUrlList,
				isSetVposStartTime.Checked, startTimeMode, endTimeMode);
			
			var l = new List<KeyValuePair<string, string>>();
			l.Add(new KeyValuePair<string, string>("IsUrlList", isUrlList.ToString().ToLower()));
			l.Add(new KeyValuePair<string, string>("IsM3u8List", isM3u8List.ToString().ToLower()));
			l.Add(new KeyValuePair<string, string>("M3u8UpdateSeconds", m3u8UpdateSeconds.ToString()));
			l.Add(new KeyValuePair<string, string>("IsOpenUrlList", isOpenUrlList.ToString().ToLower()));
			l.Add(new KeyValuePair<string, string>("openUrlListCommand", openListCommand));
			
			l.Add(new KeyValuePair<string, string>("tsStartTimeMode", startTimeMode.ToString()));
			l.Add(new KeyValuePair<string, string>("tsEndTimeMode", endTimeMode.ToString()));
			l.Add(new KeyValuePair<string, string>("tsStartSecond", (formH * 3600 + formM * 60 + formS).ToString()));
			l.Add(new KeyValuePair<string, string>("tsEndSecond", (formEndH * 3600 + formEndM * 60 + formEndS).ToString()));
			l.Add(new KeyValuePair<string, string>("tsIsRenketu", isRenketuLastFile.Checked.ToString().ToLower()));
			l.Add(new KeyValuePair<string, string>("IsVposStartTime", isSetVposStartTime.Checked.ToString().ToLower()));
			config.set(l);
			/*
			config.set("IsUrlList", isUrlList.ToString().ToLower());
			config.set("IsM3u8List", isM3u8List.ToString().ToLower());
			config.set("M3u8UpdateSeconds", m3u8UpdateSeconds.ToString());
			config.set("IsOpenUrlList", isOpenUrlList.ToString().ToLower());
			config.set("openUrlListCommand", openListCommand);
			config.set("IsVposStartTime", isSetVposStartTime.Checked.ToString().ToLower());
			*/
			Close();
		}
		
		void isStartTimeRadioBtn_CheckedChanged(object sender, EventArgs e)
		{
			updateTimeShiftStartTimeChkBox();
		}
		
		void isUrlListChkBox_CheckedChanged(object sender, EventArgs e)
		{
			updateUrlListChkBoxCheckedChanged();
		}
		void updateUrlListChkBoxCheckedChanged() {
			openListCommandText.Enabled = (isUrlListChkBox.Checked && isOpenListCommandChkBox.Checked);
			isM3u8RadioBtn.Enabled = isUrlListChkBox.Checked;
			isSimpleListRadioBtn.Enabled = isUrlListChkBox.Checked;
			updateListSecondText.Enabled = isUrlListChkBox.Checked && isM3u8RadioBtn.Checked;
			isOpenListCommandChkBox.Enabled = isUrlListChkBox.Checked;
			
			isUrlListLabelText0.Enabled = isUrlListChkBox.Checked;
			isUrlListLabelText1.Enabled = isUrlListChkBox.Checked;
			isUrlListLabelText2.Enabled = isUrlListChkBox.Checked;
			isUrlListLabelText3.Enabled = isUrlListChkBox.Checked;
		}
		
		void isM3u8RadioBtn_CheckedChanged(object sender, EventArgs e)
		{
			updateIsM3u8RadioBtn_CheckedChanged();
		}
		void updateIsM3u8RadioBtn_CheckedChanged() {
			updateListSecondText.Enabled = isM3u8RadioBtn.Checked;
		}
			
		void isOpenListCommandChkBox_CheckedChanged(object sender, EventArgs e)
		{
			updateIsOpenListCommandChkBoxCheckedChanged();
		}
		void updateIsOpenListCommandChkBoxCheckedChanged() {
			openListCommandText.Enabled = isOpenListCommandChkBox.Checked;
		}
		
		void IsFromLastTimeRadioBtnCheckedChanged(object sender, EventArgs e)
		{
			updateIsFromLastTimeRadioBtn();
		}
		void updateIsFromLastTimeRadioBtn() {
			isRenketuLastFile.Enabled = isFromLastTimeRadioBtn.Checked;
		}
		
		void IsManualEndTimeRadioBtnCheckedChanged(object sender, EventArgs e)
		{
			updateIsManualEndTimeRadioBtn();
		}
		void updateIsManualEndTimeRadioBtn() {
			endHText.Enabled = endMText.Enabled = 
					endSText.Enabled = isManualEndTimeRadioBtn.Checked;
			
		}
		private void setFormFromConfig() {
			var startMode = config.get("tsStartTimeMode");
			if (startMode == "0") isMostStartTimeRadioBtn.Checked = true;
			else if (startMode == "1") isStartTimeRadioBtn.Checked = true;
			else isFromLastTimeRadioBtn.Checked = true;
			if (config.get("tsEndTimeMode") == "0") isEndTimeRadioBtn.Checked = true;
			else isManualEndTimeRadioBtn.Checked = true;
			
			var startSeconds = int.Parse(config.get("tsStartSecond"));
			hText.Text = ((int)(startSeconds / 3600)).ToString();
			mText.Text = ((int)((startSeconds % 3600) / 60)).ToString();
			sText.Text = ((int)((startSeconds % 60) / 1)).ToString();
			var endSeconds = int.Parse(config.get("tsEndSecond"));
			endHText.Text = ((int)(endSeconds / 3600)).ToString();
			endMText.Text = ((int)((endSeconds % 3600) / 60)).ToString();
			endSText.Text = ((int)((endSeconds % 60) / 1)).ToString();
			isRenketuLastFile.Checked = bool.Parse(config.get("tsIsRenketu"));
			isSetVposStartTime.Checked = bool.Parse(config.get("IsVposStartTime"));
			
		}
		
		void ResetBtnClick(object sender, EventArgs e)
		{
			isMostStartTimeRadioBtn.Checked = true;
			isEndTimeRadioBtn.Checked = true;
			hText.Text = "0";
			mText.Text = "0";
			sText.Text = "0";
			endHText.Text = "0";
			endMText.Text = "0";
			endSText.Text = "0";
			isRenketuLastFile.Checked = false;
			isSetVposStartTime.Checked = true;
		}
		
		void LastSettingBtnClick(object sender, EventArgs e)
		{
			setFormFromConfig();
		}
	}
}
