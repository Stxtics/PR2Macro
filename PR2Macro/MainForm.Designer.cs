namespace PR2Macro
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.accounts = new System.Windows.Forms.ToolStripMenuItem();
            this.addAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.updateAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.searches = new System.Windows.Forms.ToolStripMenuItem();
            this.usernames = new System.Windows.Forms.ToolStripMenuItem();
            this.addUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.updateUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.removeUsername = new System.Windows.Forms.ToolStripMenuItem();
            this.titles = new System.Windows.Forms.ToolStripMenuItem();
            this.addTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.settings = new System.Windows.Forms.ToolStripMenuItem();
            this.keybinds = new System.Windows.Forms.ToolStripMenuItem();
            this.defaults = new System.Windows.Forms.ToolStripMenuItem();
            this.search = new System.Windows.Forms.ComboBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.sortType = new System.Windows.Forms.ComboBox();
            this.sortOrder = new System.Windows.Forms.ComboBox();
            this.sortLabel = new System.Windows.Forms.Label();
            this.searchTypeLabel = new System.Windows.Forms.Label();
            this.searchType = new System.Windows.Forms.ComboBox();
            this.pageLabel = new System.Windows.Forms.Label();
            this.pages = new System.Windows.Forms.ComboBox();
            this.levelLabel = new System.Windows.Forms.Label();
            this.levels = new System.Windows.Forms.ComboBox();
            this.randomButton = new System.Windows.Forms.Button();
            this.randomAllButton = new System.Windows.Forms.Button();
            this.simTypeLabel = new System.Windows.Forms.Label();
            this.simType = new System.Windows.Forms.ComboBox();
            this.account1Label = new System.Windows.Forms.Label();
            this.account2Label = new System.Windows.Forms.Label();
            this.account3Label = new System.Windows.Forms.Label();
            this.account4Label = new System.Windows.Forms.Label();
            this.account1 = new System.Windows.Forms.ComboBox();
            this.account2 = new System.Windows.Forms.ComboBox();
            this.account3 = new System.Windows.Forms.ComboBox();
            this.account4 = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.serverLabel = new System.Windows.Forms.Label();
            this.servers = new System.Windows.Forms.ComboBox();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowLabel = new System.Windows.Forms.Label();
            this.monitors = new System.Windows.Forms.ComboBox();
            this.monitorLabel = new System.Windows.Forms.Label();
            this.useHHServer = new System.Windows.Forms.CheckBox();
            this.macrosToControl = new System.Windows.Forms.ComboBox();
            this.macroToControlLabel = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.serversToSwitch = new System.Windows.Forms.ComboBox();
            this.switchServerButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.stopAllButton = new System.Windows.Forms.Button();
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.width = new System.Windows.Forms.TextBox();
            this.height = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sizes = new System.Windows.Forms.ComboBox();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accounts,
            this.searches,
            this.settings});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(560, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // accounts
            // 
            this.accounts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAccount,
            this.updateAccount,
            this.removeAccount});
            this.accounts.Name = "accounts";
            this.accounts.Size = new System.Drawing.Size(69, 20);
            this.accounts.Text = "Accounts";
            // 
            // addAccount
            // 
            this.addAccount.Name = "addAccount";
            this.addAccount.Size = new System.Drawing.Size(117, 22);
            this.addAccount.Text = "Add";
            this.addAccount.Click += new System.EventHandler(this.AddAccount_Click);
            // 
            // updateAccount
            // 
            this.updateAccount.Name = "updateAccount";
            this.updateAccount.Size = new System.Drawing.Size(117, 22);
            this.updateAccount.Text = "Update";
            this.updateAccount.Click += new System.EventHandler(this.UpdateAccount_Click);
            // 
            // removeAccount
            // 
            this.removeAccount.Name = "removeAccount";
            this.removeAccount.Size = new System.Drawing.Size(117, 22);
            this.removeAccount.Text = "Remove";
            this.removeAccount.Click += new System.EventHandler(this.RemoveAccount_Click);
            // 
            // searches
            // 
            this.searches.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usernames,
            this.titles});
            this.searches.Name = "searches";
            this.searches.Size = new System.Drawing.Size(65, 20);
            this.searches.Text = "Searches";
            // 
            // usernames
            // 
            this.usernames.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUsername,
            this.updateUsername,
            this.removeUsername});
            this.usernames.Name = "usernames";
            this.usernames.Size = new System.Drawing.Size(132, 22);
            this.usernames.Text = "Usernames";
            // 
            // addUsername
            // 
            this.addUsername.Name = "addUsername";
            this.addUsername.Size = new System.Drawing.Size(117, 22);
            this.addUsername.Text = "Add";
            this.addUsername.Click += new System.EventHandler(this.AddUsername_Click);
            // 
            // updateUsername
            // 
            this.updateUsername.Name = "updateUsername";
            this.updateUsername.Size = new System.Drawing.Size(117, 22);
            this.updateUsername.Text = "Update";
            this.updateUsername.Click += new System.EventHandler(this.UpdateUsername_Click);
            // 
            // removeUsername
            // 
            this.removeUsername.Name = "removeUsername";
            this.removeUsername.Size = new System.Drawing.Size(117, 22);
            this.removeUsername.Text = "Remove";
            this.removeUsername.Click += new System.EventHandler(this.RemoveUsername_Click);
            // 
            // titles
            // 
            this.titles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTitle,
            this.updateTitle,
            this.removeTitle});
            this.titles.Name = "titles";
            this.titles.Size = new System.Drawing.Size(132, 22);
            this.titles.Text = "Titles";
            // 
            // addTitle
            // 
            this.addTitle.Name = "addTitle";
            this.addTitle.Size = new System.Drawing.Size(117, 22);
            this.addTitle.Text = "Add";
            this.addTitle.Click += new System.EventHandler(this.AddTitle_Click);
            // 
            // updateTitle
            // 
            this.updateTitle.Name = "updateTitle";
            this.updateTitle.Size = new System.Drawing.Size(117, 22);
            this.updateTitle.Text = "Update";
            this.updateTitle.Click += new System.EventHandler(this.UpdateTitle_Click);
            // 
            // removeTitle
            // 
            this.removeTitle.Name = "removeTitle";
            this.removeTitle.Size = new System.Drawing.Size(117, 22);
            this.removeTitle.Text = "Remove";
            this.removeTitle.Click += new System.EventHandler(this.RemoveTitle_Click);
            // 
            // settings
            // 
            this.settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keybinds,
            this.defaults});
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(61, 20);
            this.settings.Text = "Settings";
            // 
            // keybinds
            // 
            this.keybinds.Name = "keybinds";
            this.keybinds.Size = new System.Drawing.Size(205, 22);
            this.keybinds.Text = "Keybinds (Coming soon)";
            // 
            // defaults
            // 
            this.defaults.Name = "defaults";
            this.defaults.Size = new System.Drawing.Size(205, 22);
            this.defaults.Text = "Defaults (Coming soon)";
            // 
            // search
            // 
            this.search.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.search.FormattingEnabled = true;
            this.search.Location = new System.Drawing.Point(315, 141);
            this.search.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(140, 23);
            this.search.TabIndex = 3;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(239, 144);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(45, 15);
            this.searchLabel.TabIndex = 6;
            this.searchLabel.Text = "Search:";
            // 
            // sortType
            // 
            this.sortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortType.FormattingEnabled = true;
            this.sortType.Items.AddRange(new object[] {
            "Date",
            "Alphabetical",
            "Rating",
            "Popularity"});
            this.sortType.Location = new System.Drawing.Point(90, 72);
            this.sortType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sortType.MaxDropDownItems = 4;
            this.sortType.Name = "sortType";
            this.sortType.Size = new System.Drawing.Size(108, 23);
            this.sortType.TabIndex = 1;
            // 
            // sortOrder
            // 
            this.sortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortOrder.FormattingEnabled = true;
            this.sortOrder.Items.AddRange(new object[] {
            "Descending",
            "Ascending"});
            this.sortOrder.Location = new System.Drawing.Point(205, 72);
            this.sortOrder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sortOrder.MaxDropDownItems = 2;
            this.sortOrder.Name = "sortOrder";
            this.sortOrder.Size = new System.Drawing.Size(140, 23);
            this.sortOrder.TabIndex = 2;
            // 
            // sortLabel
            // 
            this.sortLabel.AutoSize = true;
            this.sortLabel.Location = new System.Drawing.Point(14, 75);
            this.sortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sortLabel.Name = "sortLabel";
            this.sortLabel.Size = new System.Drawing.Size(47, 15);
            this.sortLabel.TabIndex = 9;
            this.sortLabel.Text = "Sort By:";
            // 
            // searchTypeLabel
            // 
            this.searchTypeLabel.AutoSize = true;
            this.searchTypeLabel.Location = new System.Drawing.Point(14, 40);
            this.searchTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.searchTypeLabel.Name = "searchTypeLabel";
            this.searchTypeLabel.Size = new System.Drawing.Size(61, 15);
            this.searchTypeLabel.TabIndex = 10;
            this.searchTypeLabel.Text = "Search By:";
            // 
            // searchType
            // 
            this.searchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchType.FormattingEnabled = true;
            this.searchType.Items.AddRange(new object[] {
            "User Name",
            "Course Title",
            "Level ID"});
            this.searchType.Location = new System.Drawing.Point(90, 37);
            this.searchType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.searchType.MaxDropDownItems = 2;
            this.searchType.Name = "searchType";
            this.searchType.Size = new System.Drawing.Size(140, 23);
            this.searchType.TabIndex = 0;
            this.searchType.SelectedIndexChanged += new System.EventHandler(this.SearchType_SelectedIndexChanged);
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Location = new System.Drawing.Point(14, 110);
            this.pageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(36, 15);
            this.pageLabel.TabIndex = 12;
            this.pageLabel.Text = "Page:";
            // 
            // pages
            // 
            this.pages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pages.FormattingEnabled = true;
            this.pages.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.pages.Location = new System.Drawing.Point(90, 106);
            this.pages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pages.MaxDropDownItems = 9;
            this.pages.MaxLength = 1;
            this.pages.Name = "pages";
            this.pages.Size = new System.Drawing.Size(47, 23);
            this.pages.TabIndex = 4;
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(239, 110);
            this.levelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(37, 15);
            this.levelLabel.TabIndex = 14;
            this.levelLabel.Text = "Level:";
            // 
            // levels
            // 
            this.levels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.levels.FormattingEnabled = true;
            this.levels.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.levels.Location = new System.Drawing.Point(315, 106);
            this.levels.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.levels.MaxDropDownItems = 6;
            this.levels.MaxLength = 1;
            this.levels.Name = "levels";
            this.levels.Size = new System.Drawing.Size(46, 23);
            this.levels.TabIndex = 5;
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(382, 106);
            this.randomButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(64, 27);
            this.randomButton.TabIndex = 6;
            this.randomButton.Text = "Random";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.RandomButton_Click);
            // 
            // randomAllButton
            // 
            this.randomAllButton.Location = new System.Drawing.Point(453, 106);
            this.randomAllButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.randomAllButton.Name = "randomAllButton";
            this.randomAllButton.Size = new System.Drawing.Size(86, 27);
            this.randomAllButton.TabIndex = 7;
            this.randomAllButton.Text = "Random All";
            this.randomAllButton.UseVisualStyleBackColor = true;
            this.randomAllButton.Click += new System.EventHandler(this.RandomAllButton_Click);
            // 
            // simTypeLabel
            // 
            this.simTypeLabel.AutoSize = true;
            this.simTypeLabel.Location = new System.Drawing.Point(14, 144);
            this.simTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.simTypeLabel.Name = "simTypeLabel";
            this.simTypeLabel.Size = new System.Drawing.Size(57, 15);
            this.simTypeLabel.TabIndex = 15;
            this.simTypeLabel.Text = "Sim Type:";
            // 
            // simType
            // 
            this.simType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simType.FormattingEnabled = true;
            this.simType.Items.AddRange(new object[] {
            "1p",
            "4p",
            "Objective"});
            this.simType.Location = new System.Drawing.Point(90, 141);
            this.simType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.simType.Name = "simType";
            this.simType.Size = new System.Drawing.Size(140, 23);
            this.simType.TabIndex = 16;
            // 
            // account1Label
            // 
            this.account1Label.AutoSize = true;
            this.account1Label.Location = new System.Drawing.Point(14, 178);
            this.account1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.account1Label.Name = "account1Label";
            this.account1Label.Size = new System.Drawing.Size(64, 15);
            this.account1Label.TabIndex = 17;
            this.account1Label.Text = "Account 1:";
            // 
            // account2Label
            // 
            this.account2Label.AutoSize = true;
            this.account2Label.Location = new System.Drawing.Point(239, 178);
            this.account2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.account2Label.Name = "account2Label";
            this.account2Label.Size = new System.Drawing.Size(64, 15);
            this.account2Label.TabIndex = 18;
            this.account2Label.Text = "Account 2:";
            // 
            // account3Label
            // 
            this.account3Label.AutoSize = true;
            this.account3Label.Location = new System.Drawing.Point(14, 212);
            this.account3Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.account3Label.Name = "account3Label";
            this.account3Label.Size = new System.Drawing.Size(64, 15);
            this.account3Label.TabIndex = 19;
            this.account3Label.Text = "Account 3:";
            // 
            // account4Label
            // 
            this.account4Label.AutoSize = true;
            this.account4Label.Location = new System.Drawing.Point(239, 212);
            this.account4Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.account4Label.Name = "account4Label";
            this.account4Label.Size = new System.Drawing.Size(64, 15);
            this.account4Label.TabIndex = 20;
            this.account4Label.Text = "Account 4:";
            // 
            // account1
            // 
            this.account1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account1.FormattingEnabled = true;
            this.account1.Location = new System.Drawing.Point(90, 174);
            this.account1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.account1.Name = "account1";
            this.account1.Size = new System.Drawing.Size(140, 23);
            this.account1.TabIndex = 21;
            this.account1.SelectedIndexChanged += new System.EventHandler(this.Account1_SelectedIndexChanged);
            // 
            // account2
            // 
            this.account2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account2.FormattingEnabled = true;
            this.account2.Location = new System.Drawing.Point(315, 174);
            this.account2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.account2.Name = "account2";
            this.account2.Size = new System.Drawing.Size(140, 23);
            this.account2.TabIndex = 22;
            this.account2.SelectedIndexChanged += new System.EventHandler(this.Account2_SelectedIndexChanged);
            // 
            // account3
            // 
            this.account3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account3.FormattingEnabled = true;
            this.account3.Location = new System.Drawing.Point(90, 209);
            this.account3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.account3.Name = "account3";
            this.account3.Size = new System.Drawing.Size(140, 23);
            this.account3.TabIndex = 23;
            this.account3.SelectedIndexChanged += new System.EventHandler(this.Account3_SelectedIndexChanged);
            // 
            // account4
            // 
            this.account4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account4.FormattingEnabled = true;
            this.account4.Location = new System.Drawing.Point(315, 209);
            this.account4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.account4.Name = "account4";
            this.account4.Size = new System.Drawing.Size(140, 23);
            this.account4.TabIndex = 24;
            this.account4.SelectedIndexChanged += new System.EventHandler(this.Account4_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(459, 281);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(88, 27);
            this.startButton.TabIndex = 27;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(14, 253);
            this.serverLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(42, 15);
            this.serverLabel.TabIndex = 30;
            this.serverLabel.Text = "Server:";
            // 
            // servers
            // 
            this.servers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.servers.FormattingEnabled = true;
            this.servers.Location = new System.Drawing.Point(90, 249);
            this.servers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.servers.Name = "servers";
            this.servers.Size = new System.Drawing.Size(140, 23);
            this.servers.TabIndex = 31;
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // windowLabel
            // 
            this.windowLabel.AutoSize = true;
            this.windowLabel.Location = new System.Drawing.Point(12, 265);
            this.windowLabel.Name = "windowLabel";
            this.windowLabel.Size = new System.Drawing.Size(49, 13);
            this.windowLabel.TabIndex = 28;
            this.windowLabel.Text = "Window:";
            // 
            // monitors
            // 
            this.monitors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monitors.FormattingEnabled = true;
            this.monitors.Location = new System.Drawing.Point(314, 248);
            this.monitors.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.monitors.Name = "monitors";
            this.monitors.Size = new System.Drawing.Size(140, 23);
            this.monitors.TabIndex = 33;
            // 
            // monitorLabel
            // 
            this.monitorLabel.AutoSize = true;
            this.monitorLabel.Location = new System.Drawing.Point(238, 252);
            this.monitorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.monitorLabel.Name = "monitorLabel";
            this.monitorLabel.Size = new System.Drawing.Size(53, 15);
            this.monitorLabel.TabIndex = 32;
            this.monitorLabel.Text = "Monitor:";
            // 
            // useHHServer
            // 
            this.useHHServer.AutoSize = true;
            this.useHHServer.Location = new System.Drawing.Point(345, 289);
            this.useHHServer.Name = "useHHServer";
            this.useHHServer.Size = new System.Drawing.Size(101, 19);
            this.useHHServer.TabIndex = 35;
            this.useHHServer.Text = "Use HH Server";
            this.useHHServer.UseVisualStyleBackColor = true;
            // 
            // macrosToControl
            // 
            this.macrosToControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.macrosToControl.FormattingEnabled = true;
            this.macrosToControl.Location = new System.Drawing.Point(114, 329);
            this.macrosToControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.macrosToControl.Name = "macrosToControl";
            this.macrosToControl.Size = new System.Drawing.Size(60, 23);
            this.macrosToControl.TabIndex = 37;
            this.macrosToControl.SelectedIndexChanged += new System.EventHandler(this.MacrosToControl_SelectedIndexChanged);
            // 
            // macroToControlLabel
            // 
            this.macroToControlLabel.AutoSize = true;
            this.macroToControlLabel.Location = new System.Drawing.Point(14, 332);
            this.macroToControlLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.macroToControlLabel.Name = "macroToControlLabel";
            this.macroToControlLabel.Size = new System.Drawing.Size(102, 15);
            this.macroToControlLabel.TabIndex = 36;
            this.macroToControlLabel.Text = "Macro To Control:";
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(175, 326);
            this.pauseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(58, 27);
            this.pauseButton.TabIndex = 38;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // serversToSwitch
            // 
            this.serversToSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serversToSwitch.FormattingEnabled = true;
            this.serversToSwitch.Location = new System.Drawing.Point(234, 329);
            this.serversToSwitch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.serversToSwitch.Name = "serversToSwitch";
            this.serversToSwitch.Size = new System.Drawing.Size(121, 23);
            this.serversToSwitch.TabIndex = 39;
            this.serversToSwitch.Enter += new System.EventHandler(this.ServersToSwitch_Enter);
            // 
            // switchServerButton
            // 
            this.switchServerButton.Location = new System.Drawing.Point(357, 326);
            this.switchServerButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.switchServerButton.Name = "switchServerButton";
            this.switchServerButton.Size = new System.Drawing.Size(85, 27);
            this.switchServerButton.TabIndex = 40;
            this.switchServerButton.Text = "Switch Server";
            this.switchServerButton.UseVisualStyleBackColor = true;
            this.switchServerButton.Click += new System.EventHandler(this.SwitchServerButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(442, 326);
            this.stopButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(45, 27);
            this.stopButton.TabIndex = 41;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // stopAllButton
            // 
            this.stopAllButton.Location = new System.Drawing.Point(487, 326);
            this.stopAllButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.stopAllButton.Name = "stopAllButton";
            this.stopAllButton.Size = new System.Drawing.Size(60, 27);
            this.stopAllButton.TabIndex = 42;
            this.stopAllButton.Text = "Stop All";
            this.stopAllButton.UseVisualStyleBackColor = true;
            this.stopAllButton.Click += new System.EventHandler(this.StopAllButton_Click);
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(120, 289);
            this.widthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(42, 15);
            this.widthLabel.TabIndex = 43;
            this.widthLabel.Text = "Width:";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(220, 289);
            this.heightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(46, 15);
            this.heightLabel.TabIndex = 44;
            this.heightLabel.Text = "Height:";
            // 
            // width
            // 
            this.width.Location = new System.Drawing.Point(166, 285);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(47, 23);
            this.width.TabIndex = 45;
            // 
            // height
            // 
            this.height.Location = new System.Drawing.Point(273, 285);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(47, 23);
            this.height.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 290);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "Size:";
            // 
            // sizes
            // 
            this.sizes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizes.FormattingEnabled = true;
            this.sizes.Location = new System.Drawing.Point(42, 286);
            this.sizes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sizes.Name = "sizes";
            this.sizes.Size = new System.Drawing.Size(74, 23);
            this.sizes.TabIndex = 48;
            this.sizes.SelectedIndexChanged += new System.EventHandler(this.Sizes_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 375);
            this.Controls.Add(this.sizes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.height);
            this.Controls.Add(this.width);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.stopAllButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.switchServerButton);
            this.Controls.Add(this.serversToSwitch);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.macrosToControl);
            this.Controls.Add(this.macroToControlLabel);
            this.Controls.Add(this.useHHServer);
            this.Controls.Add(this.monitors);
            this.Controls.Add(this.monitorLabel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.servers);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.account4);
            this.Controls.Add(this.account3);
            this.Controls.Add(this.account2);
            this.Controls.Add(this.account1);
            this.Controls.Add(this.account4Label);
            this.Controls.Add(this.account3Label);
            this.Controls.Add(this.account2Label);
            this.Controls.Add(this.account1Label);
            this.Controls.Add(this.simType);
            this.Controls.Add(this.simTypeLabel);
            this.Controls.Add(this.randomAllButton);
            this.Controls.Add(this.randomButton);
            this.Controls.Add(this.levels);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.pages);
            this.Controls.Add(this.pageLabel);
            this.Controls.Add(this.searchType);
            this.Controls.Add(this.searchTypeLabel);
            this.Controls.Add(this.sortLabel);
            this.Controls.Add(this.sortOrder);
            this.Controls.Add(this.sortType);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.search);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "PR2 Macro Tool By Stxtics";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem settings;
        private System.Windows.Forms.ComboBox search;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.ComboBox sortType;
        private System.Windows.Forms.ComboBox sortOrder;
        private System.Windows.Forms.Label sortLabel;
        private System.Windows.Forms.Label searchTypeLabel;
        private System.Windows.Forms.ComboBox searchType;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.ComboBox pages;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.ComboBox levels;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.Button randomAllButton;
        private System.Windows.Forms.ToolStripMenuItem accounts;
        private System.Windows.Forms.ToolStripMenuItem addAccount;
        private System.Windows.Forms.ToolStripMenuItem updateAccount;
        private System.Windows.Forms.ToolStripMenuItem removeAccount;
        private System.Windows.Forms.ToolStripMenuItem searches;
        private System.Windows.Forms.ToolStripMenuItem usernames;
        private System.Windows.Forms.ToolStripMenuItem addUsername;
        private System.Windows.Forms.ToolStripMenuItem updateUsername;
        private System.Windows.Forms.ToolStripMenuItem removeUsername;
        private System.Windows.Forms.ToolStripMenuItem titles;
        private System.Windows.Forms.ToolStripMenuItem addTitle;
        private System.Windows.Forms.ToolStripMenuItem updateTitle;
        private System.Windows.Forms.ToolStripMenuItem removeTitle;
        private System.Windows.Forms.ToolStripMenuItem keybinds;
        private System.Windows.Forms.ToolStripMenuItem defaults;
        private System.Windows.Forms.Label simTypeLabel;
        private System.Windows.Forms.ComboBox simType;
        private System.Windows.Forms.Label account1Label;
        private System.Windows.Forms.Label account2Label;
        private System.Windows.Forms.Label account3Label;
        private System.Windows.Forms.Label account4Label;
        private System.Windows.Forms.ComboBox account1;
        private System.Windows.Forms.ComboBox account2;
        private System.Windows.Forms.ComboBox account3;
        private System.Windows.Forms.ComboBox account4;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.ComboBox servers;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.Label windowLabel;
        private ComboBox monitors;
        private Label monitorLabel;
        private CheckBox useHHServer;
        private ComboBox macrosToControl;
        private Label macroToControlLabel;
        private Button pauseButton;
        private ComboBox serversToSwitch;
        private Button switchServerButton;
        private Button stopButton;
        private Button stopAllButton;
        private Label widthLabel;
        private Label heightLabel;
        private TextBox width;
        private TextBox height;
        private Label label1;
        private ComboBox sizes;
    }
}

