/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2021/09/30
 * Time: 1:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using rokugaTouroku.info;
using rokugaTouroku.rec;
using SunokoLibrary.Application;

namespace rokugaTouroku.gui;

/// <summary>
///     Description of accountForm.
/// </summary>
public partial class accountForm : Form
{
    private readonly config.config cfg;
    public AccountInfo ai;

    //private config.config config = null;
    //public bool isBrowser = false;
    //public CookieSourceInfo si = null;
    //public string aus = null;
    public CookieSourceInfo si;

    public accountForm(config.config config, AccountInfo ai)
    {
        //
        // The InitializeComponent() call is required for Windows Forms designer support.
        //
        InitializeComponent();

        //
        // TODO: Add constructor code after the InitializeComponent() call.
        //
        this.ai = ai;

        checkBoxShowAll.Checked = bool.Parse(config.get("IsBrowserShowAll"));

        try
        {
            if (ai == null)
            {
                useRecorderSettingRadioBtn.Checked = true;
                //var bn = config.get("browserNum");
                //if (bn == "1") useAccountLoginRadioBtn.Checked = true;
                //if (bn == "2") useCookieRadioBtn.Checked = true;
                useSecondLoginChkBox.Checked = bool.Parse(config.get("issecondlogin"));
                isCookieFileSiteiChkBox.Checked = bool.Parse(config.get("iscookie"));
                cookieFileText.Text = config.get("cookieFile");

                mailText.Text = config.get("accountId");
                passText.Text = config.get("accountPass");
                return;
            }

            if (ai.isRecSetting)
                useRecorderSettingRadioBtn.Checked = true;
            else if (ai.isBrowser)
                useCookieRadioBtn.Checked = true;
            else
                useAccountLoginRadioBtn.Checked = true;

            mailText.Text = ai.accountId;
            passText.Text = ai.accountPass;
            useSecondLoginChkBox.Checked = ai.useSecondLogin;
        }
        catch (Exception e)
        {
            util.debugWriteLine(e.Message + e.Source + e.StackTrace);
        }

        cfg = config;
    }

    private void okBtnClick(object sender, EventArgs e)
    {
        try
        {
            ai = null;

            var importer = nicoSessionComboBox1.Selector.SelectedImporter;
            if (importer != null && importer.SourceInfo != null)
            {
                si = importer.SourceInfo;
                if (isCookieFileSiteiChkBox.Checked)
                    si = si.GenerateCopy(si.BrowserName, si.ProfileName, cookieFileText.Text);
            }

            ai = new AccountInfo(si, mailText.Text,
                passText.Text, useCookieRadioBtn.Checked,
                useSecondLoginChkBox.Checked,
                useRecorderSettingRadioBtn.Checked,
                cookieFileText.Text);

            DialogResult = DialogResult.OK;
        }
        catch (Exception ee)
        {
            util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
        }
    }

    private void CancelBtnClick(object sender, EventArgs e)
    {
        Close();
    }

    private void AccountFormLoad(object sender, EventArgs e)
    {
        try
        {
            if (ai == null || ai.si == null)
            {
                var si = SourceInfoSerialize.load(false);
                nicoSessionComboBox1.Selector.SetInfoAsync(si);
                isCookieFileSiteiChkBox.Checked = bool.Parse(cfg.get("iscookie"));
                cookieFileText.Text = cfg.get("cookieFile");
            }
            else
            {
                isCookieFileSiteiChkBox.Checked = ai.si.IsCustomized;
                cookieFileText.Text = ai.cookieFile;
                nicoSessionComboBox1.Selector.SetInfoAsync(ai.si);
            }
        }
        catch (Exception ee)
        {
            util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
        }
    }

    private async void LoginBtnClick(object sender, EventArgs e)
    {
        var TargetUrl = new Uri("https://live.nicovideo.jp/");
        var cg = new CookieGetter(cfg);
        var cc = await cg.getAccountCookie(mailText.Text, passText.Text);
        if (cc == null)
        {
            util.showMessageBoxCenterForm(this, "login error");
            return;
        }

        if (cc.GetCookies(TargetUrl)["user_session"] == null &&
            cc.GetCookies(TargetUrl)["user_session_secure"] == null)
            util.showMessageBoxCenterForm(this, "no login");
        else util.showMessageBoxCenterForm(this, "login ok");
    }
}