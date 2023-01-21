/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/04/11
 * Time: 0:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using namaichi.info;
using namaichi.utility;

//using Newtonsoft.Json;

namespace namaichi.rec;

/// <summary>
///     Description of Class1.
/// </summary>
public class RecordingManager
{
    //public bool isPlayOnlyMode = false;

    public TimeShiftConfig argTsConfig;

    //static readonly Uri TargetUrl = new Uri("https://live.nicovideo.jp/");
    public config.Config cfg;
    public string communityNum;
    public MainForm form;
    public string hlsUrl;

    public bool isClickedRecBtn = false;

//		public string commentWsUrl;
    public bool isJikken = false;
    public bool isRecording;

    public bool isTitleBarInfo = false;
    public string recordingUrl;
    public List<int> recordRunningList = new();
    public RegGetter regGetter = new();

    public RecordFromUrl rfu;

//		public JikkenRecorder jr = null;
//		public JikkenRecordProcess jrp = null;
    public RedistInfo ri;
    public Stream rtmpPipe = null;
    public IRecorderProcess wsr = null;

    public RecordingManager(MainForm form, config.Config cfg)
    {
        this.form = form;
        this.cfg = cfg;
    }

    public void rec(bool isPlayOnlyMode)
    {
        util.debugWriteLine("rm");

        if (rfu == null)
        {
            var lv = util.getRegGroup(form.urlText.Text, "(lv\\d+(,\\d+)*)");
            if (lv == null)
                lv = util.getRegGroup(form.urlText.Text, "https://nicochannel.jp/.+/(live|video)/([a-zA-Z0-9]+)",
                    2);
            util.setLog(cfg, lv);
            util.debugWriteLine(util.versionStr + " " + util.versionDayStr + " " + util.dotNetVer);

            var arr = form.urlText.Text.Split('|');
            try
            {
                if ((!arr[0].StartsWith("http") && File.Exists(arr[0])) ||
                    Directory.Exists(arr[0]))
                {
                    Task.Run(() => new ArgConcat(this, arr).concat());
                }
                else
                {
                    if (lv == null)
                    {
                        form.formAction(() => util.showMessageBoxCenterForm(form, "not found lvid"), false);
                        return;
                    }

                    startRecording(lv, isPlayOnlyMode);
                }
            }
            catch (Exception e)
            {
                util.debugWriteLine(e.Message + " " + e.Source + " " + e.StackTrace + " " + e.TargetSite);
            }
        }
        else
        {
            stopRecording(rfu.isPlayOnlyMode);
        }
    }

    private void startRecording(string lvid, bool isPlayOnlyMode)
    {
        util.setProxy(cfg, form);
        isRecording = true;
        form.formAction(() =>
        {
            form.urlText.Text = lvid.StartsWith("lv")
                ? "https://live.nicovideo.jp/watch/" + lvid
                : util.getRegGroup(form.urlText.Text, "(https://nicochannel.jp/.+/(live|video)/([a-zA-Z0-9]+))");
            setRecModeForm(true);

            form.resetDisplay();
            recordingUrl = form.urlText.Text;
        }, false);

        rfu = new RecordFromUrl(this, isPlayOnlyMode);
        Task.Run(() =>
        {
            try
            {
                var _rfu = rfu;
                util.debugWriteLine("rm rec 録画開始" + rfu);
                util.debugWriteLine(form.urlText.Text);

                var rfuCode = rfu.GetHashCode();
                recordRunningList.Add(rfuCode);
                //endcode 0-その他の理由 1-stop 2-最初に終了 3-始まった後に番組終了
                var endCode = rfu.rec(form.urlText.Text, lvid);
                util.debugWriteLine("endcode " + endCode);
                recordRunningList.Remove(rfuCode);

                endProcess(endCode, rfu == _rfu);
            }
            catch (Exception e)
            {
                util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
                rfu = null;
                setRecModeForm(false);
            }
        });
    }

    private void endProcess(int endCode, bool isSameRfu)
    {
        if (endCode == 3 && bool.Parse(cfg.get("IsSoundEnd")))
            util.soundEnd(cfg, form);

        if (isSameRfu)
        {
            isRecording = false;
            rfu = null;
            setRecModeForm(false);

            if (form.recEndProcess != null && endCode == 3)
            {
                Environment.ExitCode = 5;
                form.formAction(() =>
                    util.shutdown(form.recEndProcess, form));
            }

            util.debugWriteLine("end rec " + rfu);
            if (!isClickedRecBtn && endCode == 3)
                if (util.isShowWindow && bool.Parse(cfg.get("IscloseExit")))
                {
                    Environment.ExitCode = 5;
                    form.close();
                }

            hlsUrl = null;
            recordingUrl = null;
        }

        if (bool.Parse(cfg.get("IscloseExit")) && endCode == 3)
        {
            rfu = null;
            Environment.ExitCode = 5;
            form.close();
        }

        if (util.isStdIO)
        {
            // && (endCode == 0 || endCode == 1 || endCode == 2 || endCode == 3)) {
            if (endCode == 3) Environment.ExitCode = 5;
            form.close();
        }
    }

    public void setRedistInfo(string[] args)
    {
        ri = new RedistInfo(args);
    }

    public void stopRecording(bool isPlayOnlyMode)
    {
        setRecModeForm(false);
        var _m = isPlayOnlyMode ? "視聴" : "録画";
        form.addLogText(_m + "を中断しました");

        isRecording = false;
        rfu = null;
        hlsUrl = null;

        recordingUrl = null;
    }

    private void setRecModeForm(bool isRec)
    {
        form.formAction(() =>
        {
            try
            {
                form.recBtn.Text = isRec ? "中断" : "録画開始";
                form.urlText.Enabled = !isRec;
                form.optionMenuItem.Enabled = !isRec;
                form.isChaseChkBtn.Enabled = !isRec;
            }
            catch (Exception e)
            {
                util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
            }
        }, false);
    }
}