/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/08/21
 * Time: 4:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Diagnostics;
using namaichi.play;
using namaichi.rec;

namespace namaichi.gui;

/// <summary>
///     Description of commentForm.
/// </summary>
public partial class commentForm : Form
{
    private readonly config.Config config;
    private int commentListclickedRowIndex = -1;
    private CommentPlayerWebSocket2 cpws = null!;
    public readonly MainForm form;
    private int nowVpos;

    public commentForm(config.Config config, MainForm form)
    {
        this.config = config;
        this.form = form;
        //
        // The InitializeComponent() call is required for Windows Forms designer support.
        //
        InitializeComponent();
        try
        {
            StartPosition = FormStartPosition.Manual;

            Location = new Point(int.Parse(config.get("defaultCommentFormX")),
                int.Parse(config.get("defaultCommentFormY")));
            Width = int.Parse(config.get("defaultCommentFormWidth"));
            Height = int.Parse(config.get("defaultCommentFormHeight"));
        }
        catch (Exception e)
        {
            util.debugWriteLine("setComment foerm init " + e.Message + " " + e.StackTrace + " " + e.Source + " " +
                                e.TargetSite);
        }

        commentList.DefaultCellStyle.SelectionBackColor = commentList.DefaultCellStyle.BackColor;
        commentList.DefaultCellStyle.SelectionForeColor = Color.FromArgb(114, 114, 114);

        visitLabel.Text = form.visitLabel.Text;
        commentLabel.Text = form.commentLabel.Text;
        communityLabel.Text = form.rec.communityNum;

        is184ChkBox.Checked = bool.Parse(config.get("Is184"));

        Task.Run(connectMessageServer);
        //
        // TODO: Add constructor code after the InitializeComponent() call.
        //
        util.setFontSize(int.Parse(config.get("fontSize")), this, false);
    }

    private void commentPanelBorderPaint(object sender, PaintEventArgs e)
    {
        var fromArgb = Color.FromArgb(229, 229, 229);
        ControlPaint.DrawBorder(e.Graphics, commentBorderPanel.ClientRectangle, fromArgb, ButtonBorderStyle.Solid);
    }

    private void commentTextPaint(object sender, PaintEventArgs e)
    {
        Debug.WriteLine("paint");
        //commentText.displayPlaceHolder();

        var fromArgb = Color.FromArgb(118, 118, 118);
//			ControlPaint.DrawBorder(e.Graphics, commentBorderPanel.ClientRectangle, addColor, ButtonBorderStyle.Solid);
        e.Graphics.DrawString("aコメント（75文字以内）", commentPlaceHolderText.Font, new SolidBrush(fromArgb), 0, 0);
        var g = e.Graphics;
        g.FillRectangle(new SolidBrush(Color.Green), 0, 0, 100, 100);
        fromArgb = Color.FromArgb(229, 229, 229);
        g.DrawString("aコメント（75文字以内）", commentPlaceHolderText.Font, new SolidBrush(fromArgb), 1, 1);
        ControlPaint.DrawStringDisabled(g, "aaaaa", commentPlaceHolderText.Font, fromArgb,
            commentPlaceHolderText.ClientRectangle, StringFormat.GenericDefault);
    }

    private void commentText_KeyDown(object sender, KeyEventArgs e)
    {
        Debug.WriteLine(commentPlaceHolderText.Text);
    }

    private void commentPanel_MouseDown(object sender, MouseEventArgs e)
    {
        commentPlaceHolderText.Focus();
    }


    private void guide_focusget(object sender, EventArgs e)
    {
        commentText.Focus();
    }

    private void infoButton_Click(object sender, EventArgs e)
    {
        Height = Height == 300 ? 130 : 300;
    }

    public void addComment(string time, string contents, string userId, string addScore, string addColor)
    {
        try
        {
            if (!IsDisposed)
                Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        var isScroll = commentList.FirstDisplayedScrollingRowIndex +
                                       commentList.DisplayedRowCount(true)
                                       >= commentList.Rows.Count;
                        object[] rows = { time, contents, addColor, userId, addScore };
                        commentList.Rows.Add(rows);

                        if (isScroll) commentList.FirstDisplayedScrollingRowIndex = commentList.Rows.Count - 1;

                        while (commentList.Rows.Count > 20 && false) commentList.Rows.RemoveAt(0);
                    }
                    catch (Exception e)
                    {
                        util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
                    }
                });
        }
        catch (Exception e)
        {
            util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
        }
    }

    private void form_Close(object sender, FormClosingEventArgs e)
    {
        if (!kakuninClose()) e.Cancel = true;
        cpws.isEnd = true;
    }

    private bool kakuninClose()
    {
        try
        {
            util.debugWriteLine(" setComment x " + Location.X + " setComment y " + Location.Y + " setComment width " +
                                Width + " setComment height " + Height + " restore width " + RestoreBounds.Width +
                                " restore height " + RestoreBounds.Height);

            if (WindowState == FormWindowState.Normal)
            {
                config.set("defaultCommentFormWidth", Width.ToString());
                config.set("defaultCommentFormHeight", Height.ToString());
                config.set("defaultCommentFormX", Location.X.ToString());
                config.set("defaultCommentFormY", Location.Y.ToString());
            }
            else
            {
                config.set("defaultCommentFormWidth", RestoreBounds.Width.ToString());
                config.set("defaultCommentFormHeight", RestoreBounds.Height.ToString());
                config.set("defaultCommentFormX", RestoreBounds.X.ToString());
                config.set("defaultCommentFormY", RestoreBounds.Y.ToString());
            }

            config.set("Is184", is184ChkBox.Checked.ToString().ToLower());
        }
        catch (Exception e)
        {
            util.debugWriteLine(e.Message + " " + e.StackTrace);
        }

        return true;
    }

    private void commentList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
        var row = commentList.Rows[e.RowIndex];
        row.Cells[0].Style.ForeColor = Color.FromArgb(114, 114, 114);
        var _c = row.Cells[2].Value.ToString();
        Color c;
        if (_c == "red") c = Color.FromArgb(255, 0, 51);
        //else if (_c == "grey") c = Color.FromArgb(114, 114, 114);
        else if (_c == "blue") c = Color.FromArgb(0, 197, 204);
        else c = Color.Black;
        row.Cells[1].Style.ForeColor = c;
    }

    private void visitLabel_sizeChanged(object sender, EventArgs e)
    {
//			if (visitLabel.Text.Length == 1) {

//			}
        visitLabel.Left = 107 - visitLabel.Width;
        visitImage.Left = 89 - visitLabel.Width;
        updateVisitCommentBarWidth();
    }

    private void commentLabel_sizeChanged(object sender, EventArgs e)
    {
        updateVisitCommentBarWidth();
    }

    private void updateVisitCommentBarWidth()
    {
        visitCommentBar.Left = visitImage.Left - 2;
        visitCommentBar.Width = commentLabel.Right + 0 - visitCommentBar.Left;
        //visitCommentBar.Width = commentLabel.Left + commentLabel.Width + 4;
    }

    private void commentList_Click(object sender, EventArgs e)
    {
        var row = commentListclickedRowIndex;
        var a = (ContextMenuStrip)sender;
//			var c = a.Tag;
        var b = (ToolStripItemClickedEventArgs)e;
        //toolstrip
        if (b.ClickedItem?.Text == @"コメントをコピー" && row != -1)
            try
            {
                Clipboard.SetText((string)commentList.Rows[row].Cells[1].Value);
            }
            catch (Exception ee)
            {
                util.debugWriteLine("clipbord cell click exception " + ee.Message + ee.Source + ee.TargetSite +
                                    ee.StackTrace);
            }
    }

    private void commentList_cellClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        commentListclickedRowIndex = e.RowIndex;
    }

    public void setStatistics(string visit, string setComment)
    {
        try
        {
            if (!IsDisposed)
                Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        visitLabel.Text = visit;
                        commentLabel.Text = setComment;
                    }
                    catch (Exception e)
                    {
                        util.debugWriteLine("setComment form statistics exception " + e.Message + " " + e.StackTrace +
                                            " " + e.Source + " " + e.TargetSite);
                    }
                });
        }
        catch (Exception e)
        {
            util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
        }
    }

    private async void commentSendButtonPane_Clickl(object sender, EventArgs e)
    {
        await commentSend();
    }

    private async Task commentSend()
    {
        if (commentText.Text == "" || !commentText.Focused) return;
        await Task.Run(() =>
        {
            Invoke((MethodInvoker)delegate
            {
                commentButtonPanel.BackColor = Color.FromArgb(115, 179, 242);
                Thread.Sleep(100);
                commentButtonPanel.BackColor = Color.FromArgb(0, 128, 255);
            });
        });
        await Task.Run(() =>
        {
            form.rec.wsr.sendComment(commentText.Text, is184ChkBox.Checked);

            Invoke((MethodInvoker)delegate { commentText.Text = ""; });
        });
    }

    private async void secretCommentSendBtn_Click(object sender, EventArgs e)
    {
        await commentSend();
    }

    public void setTime(int h, int m, int s)
    {
        nowVpos = (h * 3600 + m * 60 + s) * 100;
    }

    private void connectMessageServer()
    {
        while (form.rec.wsr == null)
        {
            if (IsDisposed) return;
            Thread.Sleep(300);
        }

        if (form.rec.wsr.ri.si.isTimeShift && !((WebSocketRecorder)form.rec.wsr).ri.isRealtimeChase)
        {
            displayTsComment();
        }
        else
        {
            cpws = new CommentPlayerWebSocket2(form.rec.wsr, this);
            cpws.start();
        }
    }

    private void displayTsComment()
    {
        while (form.rec.wsr.gotTsCommentList == null &&
               !IsDisposed)
            Thread.Sleep(200);
        if (form.rec.wsr.gotTsCommentList == null) return;

        var chats = getChats(form.rec.wsr.gotTsCommentList);

        var lastCommentIndex = -1;
        while (!IsDisposed)
            try
            {
                for (var i = lastCommentIndex + 1; i < chats.Count; i++)
                {
                    //						util.debugWriteLine(string.Join(" ", chats[i]));
                    var vpos = int.Parse(chats[i][5]);
                    if (vpos > nowVpos + 200) break;

                    addComment(chats[i][0], chats[i][2], chats[i][1], chats[i][3], chats[i][4]);
                    lastCommentIndex = i;
                }

                Thread.Sleep(300);
            }
            catch (Exception e)
            {
                util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
            }
    }

    private List<string[]> getChats(string[] gotTsComments)
    {
        var isXml = bool.Parse(form.rec.cfg.get("IsgetcommentXml"));
        var ret = new List<string[]>();
        foreach (var c in gotTsComments)
        {
//				if (c.IndexOf(" />") > -1 || c.IndexOf("content
            var vpos = int.Parse(util.getRegGroup(c, "vpos.+?(\\d+)"));
            var regGroup = util.getRegGroup(c, "user_id.+?\"(.+?)\"");
            string content = null!;
            string premium = null!;
            string root = null!;
            string getRegGroup;
            if (isXml)
            {
                content = util.getRegGroup(c, ">(.+)</");
                premium = util.getRegGroup(c, "premium=\"(.)\"");
                root = c.IndexOf("type=\"", StringComparison.Ordinal) > -1 ? "control" : "chat";
                getRegGroup = util.getRegGroup(c, "addScore=\"(.+?)\"");
            }
            else
            {
                if (c.IndexOf("\"text\":", StringComparison.Ordinal) > -1) content = util.getRegGroup(c, "\"text\":[ ]*\"(.+?)\"");
                else if (c.IndexOf("\"content\":", StringComparison.Ordinal) > -1) content = util.getRegGroup(c, "\"content\":[ ]*\"(.+?)\"");
                premium = util.getRegGroup(c, "\"premium\":[ ]*(\\d)");
                root = c.IndexOf("\"type\":", StringComparison.Ordinal) > -1 ? "control" : "chat";
                getRegGroup = util.getRegGroup(c, "\"addScore\":[ ]*(-\\d+)");
            }

            var __time = vpos / 100;
            if (vpos < 0) __time = 0;

            var h = __time / (60 * 60);
            var m = __time % (60 * 60) / 60;
            var _m = m < 10 ? "0" + m : m.ToString();
            var s = __time % 60;
            var _s = s < 10 ? "0" + s : s.ToString();
            var keikaTime = h + ":" + _m + ":" + _s + "";

            var setColor = premium == "3" ? "red" :
                premium == "7" ? "blue" : "black";
            if (root == "control") setColor = "red";

            ret.Add(new[] { keikaTime, regGroup, content, getRegGroup, setColor, vpos.ToString() });
        }

        return ret;
    }
}