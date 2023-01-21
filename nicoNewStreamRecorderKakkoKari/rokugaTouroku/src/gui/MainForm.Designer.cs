/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/09/10
 * Time: 16:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace rokugaTouroku
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            afterConvertModeList = new ComboBox();
            recList = new DataGridView();
            放送ID = new DataGridViewTextBoxColumn();
            形式 = new DataGridViewTextBoxColumn();
            画質 = new DataGridViewTextBoxColumn();
            タイムシフト設定 = new DataGridViewTextBoxColumn();
            chaseColumn = new DataGridViewTextBoxColumn();
            状態 = new DataGridViewTextBoxColumn();
            recComment = new DataGridViewTextBoxColumn();
            accountColumn = new DataGridViewTextBoxColumn();
            タイトル = new DataGridViewTextBoxColumn();
            放送者 = new DataGridViewTextBoxColumn();
            コミュニティ名 = new DataGridViewTextBoxColumn();
            開始時刻 = new DataGridViewTextBoxColumn();
            終了時刻 = new DataGridViewTextBoxColumn();
            ログ = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            openWatchUrlMenu = new ToolStripMenuItem();
            openCommunityUrlMenu = new ToolStripMenuItem();
            copyWatchUrlMenu = new ToolStripMenuItem();
            copyCommunityUrlMenu = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            openRecFolderMenu = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            reAddRowMenu = new ToolStripMenuItem();
            reAddNewConfigRowMenu = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            deleteRowMenu = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            録画フォルダを開くToolStripMenuItem = new ToolStripMenuItem();
            openSettingFolderMenu = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            openRecExeMenu = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            urlBulkRegist = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            終了ToolStripMenuItem = new ToolStripMenuItem();
            toolMenuItem = new ToolStripMenuItem();
            optionMenuItem = new ToolStripMenuItem();
            visualMenuItem = new ToolStripMenuItem();
            displayRecListMenu = new ToolStripMenuItem();
            isDisplayRecLvidMenu = new ToolStripMenuItem();
            isDisplayRecConvertTypeMenu = new ToolStripMenuItem();
            isDisplayRecQualityMenu = new ToolStripMenuItem();
            isDisplayRecTimeShiftMenu = new ToolStripMenuItem();
            isDisplayRecChaseMenu = new ToolStripMenuItem();
            isDisplayRecCommentMenu = new ToolStripMenuItem();
            isDisplayRecStateMenu = new ToolStripMenuItem();
            isDisplayRecAccountMenu = new ToolStripMenuItem();
            isDisplayRecTitleMenu = new ToolStripMenuItem();
            isDisplayRecHostNameMenu = new ToolStripMenuItem();
            isDisplayRecCommunityNameMenu = new ToolStripMenuItem();
            isDisplayRecStartTimeMenu = new ToolStripMenuItem();
            isDisplayRecEndTimMenu = new ToolStripMenuItem();
            isDisplayRecLogMenu = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            formColorMenuItem = new ToolStripMenuItem();
            characterColorMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            openReadmeMenu = new ToolStripMenuItem();
            updateMenu = new ToolStripMenuItem();
            バージョン情報VToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            isChaseChkBox = new CheckBox();
            label2 = new Label();
            recCommmentList = new ComboBox();
            label18 = new Label();
            label5 = new Label();
            label16 = new Label();
            label14 = new Label();
            accountBtn = new Button();
            setTimeshiftBtn = new Button();
            qualityBtn = new Button();
            addListBtn = new Button();
            downBtn = new Button();
            upBtn = new Button();
            deleteFinishedBtn = new Button();
            clearBtn = new Button();
            recBtn = new Button();
            urlText = new TextBox();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel3 = new Panel();
            foldBtnLabel = new Label();
            keikaTimeLabel = new Label();
            label7 = new Label();
            descriptLabel = new Label();
            label19 = new Label();
            communityUrlLabel = new Label();
            label15 = new Label();
            urlLabel = new Label();
            label17 = new Label();
            communityLabel = new Label();
            programTimeLabel = new Label();
            hostLabel = new Label();
            endTimeLabel = new Label();
            label11 = new Label();
            label6 = new Label();
            label10 = new Label();
            label4 = new Label();
            titleLabel = new Label();
            label8 = new Label();
            startTimeLabel = new Label();
            label3 = new Label();
            samuneBox = new PictureBox();
            logText = new TextBox();
            ((System.ComponentModel.ISupportInitialize)recList).BeginInit();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)samuneBox).BeginInit();
            SuspendLayout();
            // 
            // afterConvertModeList
            // 
            afterConvertModeList.DropDownStyle = ComboBoxStyle.DropDownList;
            afterConvertModeList.FormattingEnabled = true;
            afterConvertModeList.Items.AddRange(new object[] { "処理しない", "形式を変更せず処理する", "ts", "avi", "mp4", "flv", "mov", "wmv", "vob", "mkv", "mp3(音声)", "wav(音声)", "wma(音声)", "aac(音声)", "ogg(音声)" });
            afterConvertModeList.Location = new Point(391, 91);
            afterConvertModeList.Margin = new Padding(4);
            afterConvertModeList.Name = "afterConvertModeList";
            afterConvertModeList.Size = new Size(173, 25);
            afterConvertModeList.TabIndex = 5;
            // 
            // recList
            // 
            recList.AllowDrop = true;
            recList.AllowUserToAddRows = false;
            recList.AllowUserToDeleteRows = false;
            recList.AllowUserToResizeRows = false;
            recList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            recList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            recList.Columns.AddRange(new DataGridViewColumn[] { 放送ID, 形式, 画質, タイムシフト設定, chaseColumn, 状態, recComment, accountColumn, タイトル, 放送者, コミュニティ名, 開始時刻, 終了時刻, ログ });
            recList.ContextMenuStrip = contextMenuStrip1;
            recList.Location = new Point(16, 132);
            recList.Margin = new Padding(4);
            recList.Name = "recList";
            recList.ReadOnly = true;
            recList.RowHeadersVisible = false;
            recList.RowTemplate.Height = 21;
            recList.Size = new Size(1255, 231);
            recList.TabIndex = 6;
            recList.CellFormatting += RecListCellFormatting;
            recList.CellMouseDown += recListCell_MouseDown;
            recList.DataError += RecListDataError;
            recList.RowEnter += recList_FocusRowEnter;
            recList.RowsRemoved += RecListRowsRemoved;
            recList.DragDrop += RecListDragDrop;
            recList.DragEnter += RecListDragEnter;
            recList.KeyDown += RecListKeyDown;
            // 
            // 放送ID
            // 
            放送ID.DataPropertyName = "id";
            放送ID.HeaderText = "放送ID";
            放送ID.Name = "放送ID";
            放送ID.ReadOnly = true;
            // 
            // 形式
            // 
            形式.DataPropertyName = "afterConvertType";
            形式.HeaderText = "形式";
            形式.Name = "形式";
            形式.ReadOnly = true;
            形式.Resizable = DataGridViewTriState.True;
            // 
            // 画質
            // 
            画質.DataPropertyName = "quality";
            画質.HeaderText = "画質";
            画質.Name = "画質";
            画質.ReadOnly = true;
            画質.Width = 120;
            // 
            // タイムシフト設定
            // 
            タイムシフト設定.DataPropertyName = "timeShift";
            タイムシフト設定.HeaderText = "タイムシフト設定";
            タイムシフト設定.Name = "タイムシフト設定";
            タイムシフト設定.ReadOnly = true;
            タイムシフト設定.Resizable = DataGridViewTriState.True;
            タイムシフト設定.SortMode = DataGridViewColumnSortMode.NotSortable;
            タイムシフト設定.Width = 195;
            // 
            // chaseColumn
            // 
            chaseColumn.DataPropertyName = "chase";
            chaseColumn.HeaderText = "追っかけ録画";
            chaseColumn.Name = "chaseColumn";
            chaseColumn.ReadOnly = true;
            // 
            // 状態
            // 
            状態.DataPropertyName = "state";
            状態.HeaderText = "状態";
            状態.Name = "状態";
            状態.ReadOnly = true;
            // 
            // recComment
            // 
            recComment.DataPropertyName = "recComment";
            recComment.HeaderText = "映像・コメント";
            recComment.Name = "recComment";
            recComment.ReadOnly = true;
            recComment.SortMode = DataGridViewColumnSortMode.NotSortable;
            recComment.Width = 85;
            // 
            // accountColumn
            // 
            accountColumn.DataPropertyName = "account";
            accountColumn.HeaderText = "アカウント";
            accountColumn.Name = "accountColumn";
            accountColumn.ReadOnly = true;
            // 
            // タイトル
            // 
            タイトル.DataPropertyName = "title";
            タイトル.HeaderText = "タイトル";
            タイトル.Name = "タイトル";
            タイトル.ReadOnly = true;
            // 
            // 放送者
            // 
            放送者.DataPropertyName = "host";
            放送者.HeaderText = "放送者";
            放送者.Name = "放送者";
            放送者.ReadOnly = true;
            // 
            // コミュニティ名
            // 
            コミュニティ名.DataPropertyName = "communityName";
            コミュニティ名.HeaderText = "コミュニティ名";
            コミュニティ名.Name = "コミュニティ名";
            コミュニティ名.ReadOnly = true;
            // 
            // 開始時刻
            // 
            開始時刻.DataPropertyName = "startTime";
            開始時刻.HeaderText = "開始時刻";
            開始時刻.Name = "開始時刻";
            開始時刻.ReadOnly = true;
            // 
            // 終了時刻
            // 
            終了時刻.DataPropertyName = "endTime";
            終了時刻.HeaderText = "終了時刻";
            終了時刻.Name = "終了時刻";
            終了時刻.ReadOnly = true;
            // 
            // ログ
            // 
            ログ.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ログ.DataPropertyName = "log";
            ログ.HeaderText = "ログ";
            ログ.Name = "ログ";
            ログ.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { openWatchUrlMenu, openCommunityUrlMenu, copyWatchUrlMenu, copyCommunityUrlMenu, toolStripSeparator2, openRecFolderMenu, toolStripSeparator3, reAddRowMenu, reAddNewConfigRowMenu, toolStripSeparator4, deleteRowMenu });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(257, 198);
            // 
            // openWatchUrlMenu
            // 
            openWatchUrlMenu.Name = "openWatchUrlMenu";
            openWatchUrlMenu.Size = new Size(256, 22);
            openWatchUrlMenu.Text = "放送ページへ移動";
            openWatchUrlMenu.Click += openWatchUrlMenu_Click;
            // 
            // openCommunityUrlMenu
            // 
            openCommunityUrlMenu.Name = "openCommunityUrlMenu";
            openCommunityUrlMenu.Size = new Size(256, 22);
            openCommunityUrlMenu.Text = "コミュニティページへ移動";
            openCommunityUrlMenu.Click += openCommunityUrlMenu_Click;
            // 
            // copyWatchUrlMenu
            // 
            copyWatchUrlMenu.Name = "copyWatchUrlMenu";
            copyWatchUrlMenu.Size = new Size(256, 22);
            copyWatchUrlMenu.Text = "放送URLをコピー";
            copyWatchUrlMenu.Click += copyWatchUrlMenu_Click;
            // 
            // copyCommunityUrlMenu
            // 
            copyCommunityUrlMenu.Name = "copyCommunityUrlMenu";
            copyCommunityUrlMenu.Size = new Size(256, 22);
            copyCommunityUrlMenu.Text = "コミュニティURLをコピー";
            copyCommunityUrlMenu.Click += copyCommunityUrlMenu_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(253, 6);
            // 
            // openRecFolderMenu
            // 
            openRecFolderMenu.Name = "openRecFolderMenu";
            openRecFolderMenu.Size = new Size(256, 22);
            openRecFolderMenu.Text = "録画フォルダを開く";
            openRecFolderMenu.Click += openRecFolderMenu_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(253, 6);
            // 
            // reAddRowMenu
            // 
            reAddRowMenu.Name = "reAddRowMenu";
            reAddRowMenu.Size = new Size(256, 22);
            reAddRowMenu.Text = "この行を再登録する";
            reAddRowMenu.Click += reAddRowMenu_Click;
            // 
            // reAddNewConfigRowMenu
            // 
            reAddNewConfigRowMenu.Name = "reAddNewConfigRowMenu";
            reAddNewConfigRowMenu.Size = new Size(256, 22);
            reAddNewConfigRowMenu.Text = "この行を新しい設定で再登録する";
            reAddNewConfigRowMenu.Click += ReAddNewConfigRowMenuClick;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(253, 6);
            // 
            // deleteRowMenu
            // 
            deleteRowMenu.Name = "deleteRowMenu";
            deleteRowMenu.Size = new Size(256, 22);
            deleteRowMenu.Text = "この行を削除する";
            deleteRowMenu.Click += deleteRowMenu_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenuItem, toolMenuItem, visualMenuItem, helpMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 3, 0, 3);
            menuStrip1.Size = new Size(1296, 27);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 録画フォルダを開くToolStripMenuItem, openSettingFolderMenu, toolStripSeparator7, openRecExeMenu, toolStripSeparator1, urlBulkRegist, toolStripMenuItem1, toolStripSeparator5, 終了ToolStripMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.ShowShortcutKeys = false;
            fileMenuItem.Size = new Size(82, 21);
            fileMenuItem.Text = "ファイル(&F)";
            // 
            // 録画フォルダを開くToolStripMenuItem
            // 
            録画フォルダを開くToolStripMenuItem.Name = "録画フォルダを開くToolStripMenuItem";
            録画フォルダを開くToolStripMenuItem.Size = new Size(283, 22);
            録画フォルダを開くToolStripMenuItem.Text = "録画フォルダを開く(&O)";
            録画フォルダを開くToolStripMenuItem.Click += openRecFolderMenu_Click;
            // 
            // openSettingFolderMenu
            // 
            openSettingFolderMenu.Name = "openSettingFolderMenu";
            openSettingFolderMenu.Size = new Size(283, 22);
            openSettingFolderMenu.Text = "設定ファイルフォルダーを開く(&F)";
            openSettingFolderMenu.Click += OpenSettingFolderMenuClick;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(280, 6);
            // 
            // openRecExeMenu
            // 
            openRecExeMenu.Name = "openRecExeMenu";
            openRecExeMenu.Size = new Size(283, 22);
            openRecExeMenu.Text = "ニコ生新配信録画ツールを起動する(&E)";
            openRecExeMenu.Click += OpenRecExeMenuClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(280, 6);
            // 
            // urlBulkRegist
            // 
            urlBulkRegist.Name = "urlBulkRegist";
            urlBulkRegist.Size = new Size(283, 22);
            urlBulkRegist.Text = "URL一括登録(&R)";
            urlBulkRegist.Click += urlBulkRegistMenu_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(283, 22);
            toolStripMenuItem1.Text = "URLリストの保存(&S)";
            toolStripMenuItem1.Click += urlListSaveMenu_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(280, 6);
            // 
            // 終了ToolStripMenuItem
            // 
            終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            終了ToolStripMenuItem.Size = new Size(283, 22);
            終了ToolStripMenuItem.Text = "終了(&X)";
            終了ToolStripMenuItem.Click += endMenu_Click;
            // 
            // toolMenuItem
            // 
            toolMenuItem.DropDownItems.AddRange(new ToolStripItem[] { optionMenuItem });
            toolMenuItem.Name = "toolMenuItem";
            toolMenuItem.ShowShortcutKeys = false;
            toolMenuItem.Size = new Size(71, 21);
            toolMenuItem.Text = "ツール(&T)";
            // 
            // optionMenuItem
            // 
            optionMenuItem.Name = "optionMenuItem";
            optionMenuItem.ShowShortcutKeys = false;
            optionMenuItem.Size = new Size(146, 22);
            optionMenuItem.Text = "オプション(&O)";
            optionMenuItem.Click += optionItem_Select;
            // 
            // visualMenuItem
            // 
            visualMenuItem.DropDownItems.AddRange(new ToolStripItem[] { displayRecListMenu, toolStripSeparator6, formColorMenuItem, characterColorMenuItem });
            visualMenuItem.Name = "visualMenuItem";
            visualMenuItem.Size = new Size(60, 21);
            visualMenuItem.Text = "表示(&V)";
            // 
            // displayRecListMenu
            // 
            displayRecListMenu.DropDownItems.AddRange(new ToolStripItem[] { isDisplayRecLvidMenu, isDisplayRecConvertTypeMenu, isDisplayRecQualityMenu, isDisplayRecTimeShiftMenu, isDisplayRecChaseMenu, isDisplayRecCommentMenu, isDisplayRecStateMenu, isDisplayRecAccountMenu, isDisplayRecTitleMenu, isDisplayRecHostNameMenu, isDisplayRecCommunityNameMenu, isDisplayRecStartTimeMenu, isDisplayRecEndTimMenu, isDisplayRecLogMenu });
            displayRecListMenu.Name = "displayRecListMenu";
            displayRecListMenu.Size = new Size(180, 22);
            displayRecListMenu.Text = "表示列(&D)";
            displayRecListMenu.DropDownOpened += DisplayRecListMenuDropDownOpened;
            displayRecListMenu.DropDownItemClicked += DisplayRecListMenuDropDownItemClicked;
            // 
            // isDisplayRecLvidMenu
            // 
            isDisplayRecLvidMenu.Name = "isDisplayRecLvidMenu";
            isDisplayRecLvidMenu.Size = new Size(172, 22);
            isDisplayRecLvidMenu.Text = "放送ID";
            // 
            // isDisplayRecConvertTypeMenu
            // 
            isDisplayRecConvertTypeMenu.Name = "isDisplayRecConvertTypeMenu";
            isDisplayRecConvertTypeMenu.Size = new Size(172, 22);
            isDisplayRecConvertTypeMenu.Text = "形式";
            // 
            // isDisplayRecQualityMenu
            // 
            isDisplayRecQualityMenu.Name = "isDisplayRecQualityMenu";
            isDisplayRecQualityMenu.Size = new Size(172, 22);
            isDisplayRecQualityMenu.Text = "画質";
            // 
            // isDisplayRecTimeShiftMenu
            // 
            isDisplayRecTimeShiftMenu.Name = "isDisplayRecTimeShiftMenu";
            isDisplayRecTimeShiftMenu.Size = new Size(172, 22);
            isDisplayRecTimeShiftMenu.Text = "タイムシフト設定";
            // 
            // isDisplayRecChaseMenu
            // 
            isDisplayRecChaseMenu.Name = "isDisplayRecChaseMenu";
            isDisplayRecChaseMenu.Size = new Size(172, 22);
            isDisplayRecChaseMenu.Text = "追っかけ録画";
            // 
            // isDisplayRecCommentMenu
            // 
            isDisplayRecCommentMenu.Name = "isDisplayRecCommentMenu";
            isDisplayRecCommentMenu.Size = new Size(172, 22);
            isDisplayRecCommentMenu.Text = "映像・コメント";
            // 
            // isDisplayRecStateMenu
            // 
            isDisplayRecStateMenu.Name = "isDisplayRecStateMenu";
            isDisplayRecStateMenu.Size = new Size(172, 22);
            isDisplayRecStateMenu.Text = "状態";
            // 
            // isDisplayRecAccountMenu
            // 
            isDisplayRecAccountMenu.Name = "isDisplayRecAccountMenu";
            isDisplayRecAccountMenu.Size = new Size(172, 22);
            isDisplayRecAccountMenu.Text = "アカウント";
            // 
            // isDisplayRecTitleMenu
            // 
            isDisplayRecTitleMenu.Name = "isDisplayRecTitleMenu";
            isDisplayRecTitleMenu.Size = new Size(172, 22);
            isDisplayRecTitleMenu.Text = "タイトル";
            // 
            // isDisplayRecHostNameMenu
            // 
            isDisplayRecHostNameMenu.Name = "isDisplayRecHostNameMenu";
            isDisplayRecHostNameMenu.Size = new Size(172, 22);
            isDisplayRecHostNameMenu.Text = "放送者";
            // 
            // isDisplayRecCommunityNameMenu
            // 
            isDisplayRecCommunityNameMenu.Name = "isDisplayRecCommunityNameMenu";
            isDisplayRecCommunityNameMenu.Size = new Size(172, 22);
            isDisplayRecCommunityNameMenu.Text = "コミュニティ名";
            // 
            // isDisplayRecStartTimeMenu
            // 
            isDisplayRecStartTimeMenu.Name = "isDisplayRecStartTimeMenu";
            isDisplayRecStartTimeMenu.Size = new Size(172, 22);
            isDisplayRecStartTimeMenu.Text = "開始時刻";
            // 
            // isDisplayRecEndTimMenu
            // 
            isDisplayRecEndTimMenu.Name = "isDisplayRecEndTimMenu";
            isDisplayRecEndTimMenu.Size = new Size(172, 22);
            isDisplayRecEndTimMenu.Text = "終了時刻";
            // 
            // isDisplayRecLogMenu
            // 
            isDisplayRecLogMenu.Name = "isDisplayRecLogMenu";
            isDisplayRecLogMenu.Size = new Size(172, 22);
            isDisplayRecLogMenu.Text = "ログ";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(177, 6);
            // 
            // formColorMenuItem
            // 
            formColorMenuItem.Name = "formColorMenuItem";
            formColorMenuItem.Size = new Size(180, 22);
            formColorMenuItem.Text = "ウィンドウの色(&W)";
            formColorMenuItem.Click += FormColorMenuItemClick;
            // 
            // characterColorMenuItem
            // 
            characterColorMenuItem.Name = "characterColorMenuItem";
            characterColorMenuItem.Size = new Size(180, 22);
            characterColorMenuItem.Text = "文字の色(&S)";
            characterColorMenuItem.Click += CharacterColorMenuItemClick;
            // 
            // helpMenuItem
            // 
            helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openReadmeMenu, updateMenu, バージョン情報VToolStripMenuItem });
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.ShowShortcutKeys = false;
            helpMenuItem.Size = new Size(73, 21);
            helpMenuItem.Text = "ヘルプ(&H)";
            // 
            // openReadmeMenu
            // 
            openReadmeMenu.Name = "openReadmeMenu";
            openReadmeMenu.Size = new Size(201, 22);
            openReadmeMenu.Text = "readme.htmlを開く(&V)";
            openReadmeMenu.Click += OpenReadmeMenuClick;
            // 
            // updateMenu
            // 
            updateMenu.Name = "updateMenu";
            updateMenu.Size = new Size(201, 22);
            updateMenu.Text = "更新方法(&U)";
            updateMenu.Click += UpdateMenuClick;
            // 
            // バージョン情報VToolStripMenuItem
            // 
            バージョン情報VToolStripMenuItem.Name = "バージョン情報VToolStripMenuItem";
            バージョン情報VToolStripMenuItem.Size = new Size(201, 22);
            バージョン情報VToolStripMenuItem.Text = "バージョン情報(&A)";
            バージョン情報VToolStripMenuItem.Click += versionMenu_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSize = true;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(isChaseChkBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(recCommmentList);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label16);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(accountBtn);
            panel1.Controls.Add(setTimeshiftBtn);
            panel1.Controls.Add(afterConvertModeList);
            panel1.Controls.Add(qualityBtn);
            panel1.Controls.Add(addListBtn);
            panel1.Controls.Add(downBtn);
            panel1.Controls.Add(upBtn);
            panel1.Controls.Add(deleteFinishedBtn);
            panel1.Controls.Add(clearBtn);
            panel1.Controls.Add(recBtn);
            panel1.Controls.Add(urlText);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(recList);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1296, 426);
            panel1.TabIndex = 13;
            // 
            // isChaseChkBox
            // 
            isChaseChkBox.Location = new Point(813, 88);
            isChaseChkBox.Margin = new Padding(4);
            isChaseChkBox.Name = "isChaseChkBox";
            isChaseChkBox.Size = new Size(121, 34);
            isChaseChkBox.TabIndex = 14;
            isChaseChkBox.Text = "追っかけ録画";
            isChaseChkBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Location = new Point(592, 92);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(88, 24);
            label2.TabIndex = 13;
            label2.Text = "映像・コメント：";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // recCommmentList
            // 
            recCommmentList.DropDownStyle = ComboBoxStyle.DropDownList;
            recCommmentList.FormattingEnabled = true;
            recCommmentList.Items.AddRange(new object[] { "コメントのみ", "映像のみ", "映像＋コメント" });
            recCommmentList.Location = new Point(687, 91);
            recCommmentList.Margin = new Padding(4);
            recCommmentList.Name = "recCommmentList";
            recCommmentList.Size = new Size(107, 25);
            recCommmentList.TabIndex = 12;
            // 
            // label18
            // 
            label18.Location = new Point(338, 92);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(44, 24);
            label18.TabIndex = 11;
            label18.Text = "形式：";
            label18.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Location = new Point(14, 92);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(96, 24);
            label5.TabIndex = 11;
            label5.Text = "アカウント設定：";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            label16.Location = new Point(14, 57);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(187, 24);
            label16.TabIndex = 11;
            label16.Text = "タイムシフト・追っかけ再生設定：";
            label16.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            label14.Location = new Point(454, 57);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(49, 24);
            label14.TabIndex = 9;
            label14.Text = "画質：";
            label14.TextAlign = ContentAlignment.MiddleRight;
            // 
            // accountBtn
            // 
            accountBtn.Location = new Point(117, 88);
            accountBtn.Margin = new Padding(4);
            accountBtn.Name = "accountBtn";
            accountBtn.Size = new Size(194, 33);
            accountBtn.TabIndex = 3;
            accountBtn.Text = "録画ツールの設定を使用";
            accountBtn.UseVisualStyleBackColor = true;
            accountBtn.Click += AccountBtnClick;
            // 
            // setTimeshiftBtn
            // 
            setTimeshiftBtn.Location = new Point(205, 52);
            setTimeshiftBtn.Margin = new Padding(4);
            setTimeshiftBtn.Name = "setTimeshiftBtn";
            setTimeshiftBtn.Size = new Size(232, 33);
            setTimeshiftBtn.TabIndex = 3;
            setTimeshiftBtn.Text = "最初から-最後まで";
            setTimeshiftBtn.UseVisualStyleBackColor = true;
            setTimeshiftBtn.Click += setTimeshiftBtn_Click;
            // 
            // qualityBtn
            // 
            qualityBtn.AutoSize = true;
            qualityBtn.Location = new Point(510, 52);
            qualityBtn.Margin = new Padding(4);
            qualityBtn.Name = "qualityBtn";
            qualityBtn.Size = new Size(227, 38);
            qualityBtn.TabIndex = 4;
            qualityBtn.Text = "超高,高,中,低,超低,音,6M,8M,4M";
            qualityBtn.UseVisualStyleBackColor = true;
            qualityBtn.Click += qualityBtn_Click;
            // 
            // addListBtn
            // 
            addListBtn.Location = new Point(502, 14);
            addListBtn.Margin = new Padding(4);
            addListBtn.Name = "addListBtn";
            addListBtn.Size = new Size(113, 33);
            addListBtn.TabIndex = 1;
            addListBtn.Text = "録画リスト追加";
            addListBtn.UseVisualStyleBackColor = true;
            addListBtn.Click += addListBtn_Click;
            // 
            // downBtn
            // 
            downBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            downBtn.Location = new Point(390, 375);
            downBtn.Margin = new Padding(4);
            downBtn.Name = "downBtn";
            downBtn.Size = new Size(49, 33);
            downBtn.TabIndex = 7;
            downBtn.Text = "下へ";
            downBtn.UseVisualStyleBackColor = true;
            downBtn.Click += DownBtnClick;
            // 
            // upBtn
            // 
            upBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            upBtn.Location = new Point(334, 375);
            upBtn.Margin = new Padding(4);
            upBtn.Name = "upBtn";
            upBtn.Size = new Size(49, 33);
            upBtn.TabIndex = 7;
            upBtn.Text = "上へ";
            upBtn.UseVisualStyleBackColor = true;
            upBtn.Click += UpBtnClick;
            // 
            // deleteFinishedBtn
            // 
            deleteFinishedBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deleteFinishedBtn.Location = new Point(159, 375);
            deleteFinishedBtn.Margin = new Padding(4);
            deleteFinishedBtn.Name = "deleteFinishedBtn";
            deleteFinishedBtn.Size = new Size(136, 33);
            deleteFinishedBtn.TabIndex = 7;
            deleteFinishedBtn.Text = "完了した行を削除";
            deleteFinishedBtn.UseVisualStyleBackColor = true;
            deleteFinishedBtn.Click += DeleteFinishedBtnClick;
            // 
            // clearBtn
            // 
            clearBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            clearBtn.Location = new Point(15, 375);
            clearBtn.Margin = new Padding(4);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(136, 33);
            clearBtn.TabIndex = 7;
            clearBtn.Text = "全ての行を削除";
            clearBtn.UseVisualStyleBackColor = true;
            clearBtn.Click += clearBtn_Click;
            // 
            // recBtn
            // 
            recBtn.Location = new Point(662, 14);
            recBtn.Margin = new Padding(4);
            recBtn.Name = "recBtn";
            recBtn.Size = new Size(88, 33);
            recBtn.TabIndex = 2;
            recBtn.Text = "録画開始";
            recBtn.UseVisualStyleBackColor = true;
            recBtn.Click += recBtn_Click;
            // 
            // urlText
            // 
            urlText.Location = new Point(88, 17);
            urlText.Margin = new Padding(4);
            urlText.Name = "urlText";
            urlText.Size = new Size(362, 23);
            urlText.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new Point(14, 21);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(99, 21);
            label1.TabIndex = 1;
            label1.Text = "放送URL：";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel3, 0, 1);
            tableLayoutPanel1.Location = new Point(0, 37);
            tableLayoutPanel1.Margin = new Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 224F));
            tableLayoutPanel1.Size = new Size(1296, 650);
            tableLayoutPanel1.TabIndex = 15;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.AutoSize = true;
            panel3.BackColor = Color.Transparent;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(foldBtnLabel);
            panel3.Controls.Add(keikaTimeLabel);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(descriptLabel);
            panel3.Controls.Add(label19);
            panel3.Controls.Add(communityUrlLabel);
            panel3.Controls.Add(label15);
            panel3.Controls.Add(urlLabel);
            panel3.Controls.Add(label17);
            panel3.Controls.Add(communityLabel);
            panel3.Controls.Add(programTimeLabel);
            panel3.Controls.Add(hostLabel);
            panel3.Controls.Add(endTimeLabel);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(titleLabel);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(startTimeLabel);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(samuneBox);
            panel3.Controls.Add(logText);
            panel3.Location = new Point(0, 428);
            panel3.Margin = new Padding(0);
            panel3.MinimumSize = new Size(4, 222);
            panel3.Name = "panel3";
            panel3.Size = new Size(1296, 222);
            panel3.TabIndex = 15;
            // 
            // foldBtnLabel
            // 
            foldBtnLabel.BackColor = SystemColors.ButtonHighlight;
            foldBtnLabel.BorderStyle = BorderStyle.FixedSingle;
            foldBtnLabel.Font = new Font("MS UI Gothic", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            foldBtnLabel.Location = new Point(4, 0);
            foldBtnLabel.Margin = new Padding(4, 0, 4, 0);
            foldBtnLabel.Name = "foldBtnLabel";
            foldBtnLabel.Size = new Size(60, 18);
            foldBtnLabel.TabIndex = 15;
            foldBtnLabel.Text = "折り畳む";
            foldBtnLabel.TextAlign = ContentAlignment.MiddleCenter;
            foldBtnLabel.Click += FoldBtnLabelClick;
            // 
            // keikaTimeLabel
            // 
            keikaTimeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            keikaTimeLabel.Location = new Point(184, 198);
            keikaTimeLabel.Margin = new Padding(4, 0, 4, 0);
            keikaTimeLabel.Name = "keikaTimeLabel";
            keikaTimeLabel.Size = new Size(475, 20);
            keikaTimeLabel.TabIndex = 17;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label7.Location = new Point(169, 178);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(475, 20);
            label7.TabIndex = 16;
            label7.Text = "経過時間";
            // 
            // descriptLabel
            // 
            descriptLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            descriptLabel.Location = new Point(736, 146);
            descriptLabel.Margin = new Padding(4, 0, 4, 0);
            descriptLabel.Name = "descriptLabel";
            descriptLabel.Size = new Size(317, 72);
            descriptLabel.TabIndex = 7;
            // 
            // label19
            // 
            label19.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label19.Location = new Point(721, 126);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(115, 20);
            label19.TabIndex = 8;
            label19.Text = "説明";
            // 
            // communityUrlLabel
            // 
            communityUrlLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            communityUrlLabel.Location = new Point(736, 94);
            communityUrlLabel.Margin = new Padding(4, 0, 4, 0);
            communityUrlLabel.Name = "communityUrlLabel";
            communityUrlLabel.Size = new Size(317, 20);
            communityUrlLabel.TabIndex = 5;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label15.Location = new Point(721, 74);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(115, 20);
            label15.TabIndex = 6;
            label15.Text = "コミュニティURL";
            // 
            // urlLabel
            // 
            urlLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            urlLabel.BackColor = Color.Transparent;
            urlLabel.Location = new Point(736, 41);
            urlLabel.Margin = new Padding(4, 0, 4, 0);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(317, 20);
            urlLabel.TabIndex = 3;
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label17.Location = new Point(721, 21);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(115, 20);
            label17.TabIndex = 4;
            label17.Text = "放送URL";
            // 
            // communityLabel
            // 
            communityLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            communityLabel.Location = new Point(334, 146);
            communityLabel.Margin = new Padding(4, 0, 4, 0);
            communityLabel.Name = "communityLabel";
            communityLabel.Size = new Size(380, 20);
            communityLabel.TabIndex = 2;
            // 
            // programTimeLabel
            // 
            programTimeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            programTimeLabel.Location = new Point(184, 146);
            programTimeLabel.Margin = new Padding(4, 0, 4, 0);
            programTimeLabel.Name = "programTimeLabel";
            programTimeLabel.Size = new Size(475, 20);
            programTimeLabel.TabIndex = 2;
            // 
            // hostLabel
            // 
            hostLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            hostLabel.Location = new Point(334, 94);
            hostLabel.Margin = new Padding(4, 0, 4, 0);
            hostLabel.Name = "hostLabel";
            hostLabel.Size = new Size(380, 20);
            hostLabel.TabIndex = 2;
            // 
            // endTimeLabel
            // 
            endTimeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            endTimeLabel.Location = new Point(184, 94);
            endTimeLabel.Margin = new Padding(4, 0, 4, 0);
            endTimeLabel.Name = "endTimeLabel";
            endTimeLabel.Size = new Size(475, 20);
            endTimeLabel.TabIndex = 2;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label11.Location = new Point(318, 126);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(475, 20);
            label11.TabIndex = 2;
            label11.Text = "コミュニティ";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.Location = new Point(169, 126);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(475, 20);
            label6.TabIndex = 2;
            label6.Text = "放送時間";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label10.Location = new Point(318, 74);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(475, 20);
            label10.TabIndex = 2;
            label10.Text = "放送者";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Location = new Point(169, 74);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(475, 20);
            label4.TabIndex = 2;
            label4.Text = "放送終了日時";
            // 
            // titleLabel
            // 
            titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Location = new Point(334, 41);
            titleLabel.Margin = new Padding(4, 0, 4, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(380, 20);
            titleLabel.TabIndex = 2;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label8.Location = new Point(318, 21);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(475, 20);
            label8.TabIndex = 2;
            label8.Text = "タイトル";
            // 
            // startTimeLabel
            // 
            startTimeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            startTimeLabel.Location = new Point(184, 41);
            startTimeLabel.Margin = new Padding(4, 0, 4, 0);
            startTimeLabel.Name = "startTimeLabel";
            startTimeLabel.Size = new Size(475, 20);
            startTimeLabel.TabIndex = 2;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(169, 21);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(475, 20);
            label3.TabIndex = 2;
            label3.Text = "放送開始日時";
            // 
            // samuneBox
            // 
            samuneBox.BorderStyle = BorderStyle.FixedSingle;
            samuneBox.ErrorImage = null;
            samuneBox.Image = (Image)resources.GetObject("samuneBox.Image");
            samuneBox.InitialImage = (Image)resources.GetObject("samuneBox.InitialImage");
            samuneBox.Location = new Point(14, 21);
            samuneBox.Margin = new Padding(2, 3, 2, 3);
            samuneBox.Name = "samuneBox";
            samuneBox.Size = new Size(149, 180);
            samuneBox.SizeMode = PictureBoxSizeMode.Zoom;
            samuneBox.TabIndex = 1;
            samuneBox.TabStop = false;
            // 
            // logText
            // 
            logText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            logText.Location = new Point(1073, 0);
            logText.Margin = new Padding(4);
            logText.Multiline = true;
            logText.Name = "logText";
            logText.ScrollBars = ScrollBars.Vertical;
            logText.Size = new Size(218, 216);
            logText.TabIndex = 8;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1296, 687);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "rokugaTouroku";
            FormClosing += form_Close;
            Load += form_Load;
            ((System.ComponentModel.ISupportInitialize)recList).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)samuneBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label foldBtnLabel;
        private System.Windows.Forms.Button upBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecAccountMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountColumn;
        public System.Windows.Forms.Button accountBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem reAddNewConfigRowMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn chaseColumn;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecChaseMenu;
        public System.Windows.Forms.CheckBox isChaseChkBox;
        private System.Windows.Forms.ToolStripMenuItem openRecExeMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem openReadmeMenu;
        private System.Windows.Forms.ToolStripMenuItem openSettingFolderMenu;
        private System.Windows.Forms.Button deleteFinishedBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecLogMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecEndTimMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecStartTimeMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecCommunityNameMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecHostNameMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecTitleMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecTimeShiftMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecCommentMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecStateMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecQualityMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecConvertTypeMenu;
        private System.Windows.Forms.ToolStripMenuItem isDisplayRecLvidMenu;
        private System.Windows.Forms.ToolStripMenuItem displayRecListMenu;
        private System.Windows.Forms.ToolStripMenuItem characterColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem urlBulkRegist;
        private System.Windows.Forms.DataGridViewTextBoxColumn recComment;
        public System.Windows.Forms.ComboBox recCommmentList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem deleteRowMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem reAddRowMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem openRecFolderMenu;
        private System.Windows.Forms.ToolStripMenuItem copyCommunityUrlMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyWatchUrlMenu;
        private System.Windows.Forms.ToolStripMenuItem openCommunityUrlMenu;
        private System.Windows.Forms.ToolStripMenuItem openWatchUrlMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label keikaTimeLabel;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridViewTextBoxColumn 画質;
        public System.Windows.Forms.Button qualityBtn;
        public System.Windows.Forms.Button setTimeshiftBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn タイムシフト設定;
        public System.Windows.Forms.ComboBox afterConvertModeList;
        private System.Windows.Forms.DataGridViewTextBoxColumn 形式;
        public System.Windows.Forms.DataGridView recList;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label descriptLabel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label communityUrlLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Label communityLabel;
        private System.Windows.Forms.Label startTimeLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label endTimeLabel;
        private System.Windows.Forms.Label programTimeLabel;
        private System.Windows.Forms.TextBox logText;
        private System.Windows.Forms.PictureBox samuneBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ログ;
        private System.Windows.Forms.DataGridViewTextBoxColumn 終了時刻;
        private System.Windows.Forms.DataGridViewTextBoxColumn 開始時刻;
        private System.Windows.Forms.DataGridViewTextBoxColumn コミュニティ名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 放送者;
        private System.Windows.Forms.DataGridViewTextBoxColumn タイトル;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状態;
        private System.Windows.Forms.DataGridViewTextBoxColumn 放送ID;
        public System.Windows.Forms.Button addListBtn;
        public System.Windows.Forms.Button recBtn;
        public System.Windows.Forms.TextBox urlText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem バージョン情報VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        public System.Windows.Forms.ToolStripMenuItem optionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 録画フォルダを開くToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}
