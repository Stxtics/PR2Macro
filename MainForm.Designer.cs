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
            this.pr2WidthLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.windowLabel = new System.Windows.Forms.Label();
            this.windows = new System.Windows.Forms.ComboBox();
            this.serverLabel = new System.Windows.Forms.Label();
            this.servers = new System.Windows.Forms.ComboBox();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pr2HeightLabel = new System.Windows.Forms.Label();
            this.pr2Width = new System.Windows.Forms.TextBox();
            this.pr2Height = new System.Windows.Forms.TextBox();
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
            this.menuStrip.Size = new System.Drawing.Size(480, 24);
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
            this.search.Location = new System.Drawing.Point(270, 122);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(121, 21);
            this.search.TabIndex = 3;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(205, 125);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(44, 13);
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
            this.sortType.Location = new System.Drawing.Point(77, 62);
            this.sortType.MaxDropDownItems = 4;
            this.sortType.Name = "sortType";
            this.sortType.Size = new System.Drawing.Size(93, 21);
            this.sortType.TabIndex = 1;
            // 
            // sortOrder
            // 
            this.sortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortOrder.FormattingEnabled = true;
            this.sortOrder.Items.AddRange(new object[] {
            "Descending",
            "Ascending"});
            this.sortOrder.Location = new System.Drawing.Point(176, 62);
            this.sortOrder.MaxDropDownItems = 2;
            this.sortOrder.Name = "sortOrder";
            this.sortOrder.Size = new System.Drawing.Size(121, 21);
            this.sortOrder.TabIndex = 2;
            // 
            // sortLabel
            // 
            this.sortLabel.AutoSize = true;
            this.sortLabel.Location = new System.Drawing.Point(12, 65);
            this.sortLabel.Name = "sortLabel";
            this.sortLabel.Size = new System.Drawing.Size(44, 13);
            this.sortLabel.TabIndex = 9;
            this.sortLabel.Text = "Sort By:";
            // 
            // searchTypeLabel
            // 
            this.searchTypeLabel.AutoSize = true;
            this.searchTypeLabel.Location = new System.Drawing.Point(12, 35);
            this.searchTypeLabel.Name = "searchTypeLabel";
            this.searchTypeLabel.Size = new System.Drawing.Size(59, 13);
            this.searchTypeLabel.TabIndex = 10;
            this.searchTypeLabel.Text = "Search By:";
            // 
            // searchType
            // 
            this.searchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchType.FormattingEnabled = true;
            this.searchType.Items.AddRange(new object[] {
            "User Name",
            "Course Title"});
            this.searchType.Location = new System.Drawing.Point(77, 32);
            this.searchType.MaxDropDownItems = 2;
            this.searchType.Name = "searchType";
            this.searchType.Size = new System.Drawing.Size(121, 21);
            this.searchType.TabIndex = 0;
            this.searchType.SelectedIndexChanged += new System.EventHandler(this.SearchType_SelectedIndexChanged);
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Location = new System.Drawing.Point(12, 95);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(35, 13);
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
            this.pages.Location = new System.Drawing.Point(77, 92);
            this.pages.MaxDropDownItems = 9;
            this.pages.MaxLength = 1;
            this.pages.Name = "pages";
            this.pages.Size = new System.Drawing.Size(41, 21);
            this.pages.TabIndex = 4;
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(205, 95);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(36, 13);
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
            this.levels.Location = new System.Drawing.Point(270, 92);
            this.levels.MaxDropDownItems = 6;
            this.levels.MaxLength = 1;
            this.levels.Name = "levels";
            this.levels.Size = new System.Drawing.Size(40, 21);
            this.levels.TabIndex = 5;
            // 
            // randomButton
            // 
            this.randomButton.Location = new System.Drawing.Point(327, 92);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(55, 23);
            this.randomButton.TabIndex = 6;
            this.randomButton.Text = "Random";
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.Click += new System.EventHandler(this.RandomButton_Click);
            // 
            // randomAllButton
            // 
            this.randomAllButton.Location = new System.Drawing.Point(388, 92);
            this.randomAllButton.Name = "randomAllButton";
            this.randomAllButton.Size = new System.Drawing.Size(74, 23);
            this.randomAllButton.TabIndex = 7;
            this.randomAllButton.Text = "Random All";
            this.randomAllButton.UseVisualStyleBackColor = true;
            this.randomAllButton.Click += new System.EventHandler(this.RandomAllButton_Click);
            // 
            // simTypeLabel
            // 
            this.simTypeLabel.AutoSize = true;
            this.simTypeLabel.Location = new System.Drawing.Point(12, 125);
            this.simTypeLabel.Name = "simTypeLabel";
            this.simTypeLabel.Size = new System.Drawing.Size(54, 13);
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
            this.simType.Location = new System.Drawing.Point(77, 122);
            this.simType.Name = "simType";
            this.simType.Size = new System.Drawing.Size(121, 21);
            this.simType.TabIndex = 16;
            // 
            // account1Label
            // 
            this.account1Label.AutoSize = true;
            this.account1Label.Location = new System.Drawing.Point(12, 154);
            this.account1Label.Name = "account1Label";
            this.account1Label.Size = new System.Drawing.Size(59, 13);
            this.account1Label.TabIndex = 17;
            this.account1Label.Text = "Account 1:";
            // 
            // account2Label
            // 
            this.account2Label.AutoSize = true;
            this.account2Label.Location = new System.Drawing.Point(205, 154);
            this.account2Label.Name = "account2Label";
            this.account2Label.Size = new System.Drawing.Size(59, 13);
            this.account2Label.TabIndex = 18;
            this.account2Label.Text = "Account 2:";
            // 
            // account3Label
            // 
            this.account3Label.AutoSize = true;
            this.account3Label.Location = new System.Drawing.Point(12, 184);
            this.account3Label.Name = "account3Label";
            this.account3Label.Size = new System.Drawing.Size(59, 13);
            this.account3Label.TabIndex = 19;
            this.account3Label.Text = "Account 3:";
            // 
            // account4Label
            // 
            this.account4Label.AutoSize = true;
            this.account4Label.Location = new System.Drawing.Point(205, 184);
            this.account4Label.Name = "account4Label";
            this.account4Label.Size = new System.Drawing.Size(59, 13);
            this.account4Label.TabIndex = 20;
            this.account4Label.Text = "Account 4:";
            // 
            // account1
            // 
            this.account1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account1.FormattingEnabled = true;
            this.account1.Location = new System.Drawing.Point(77, 151);
            this.account1.Name = "account1";
            this.account1.Size = new System.Drawing.Size(121, 21);
            this.account1.TabIndex = 21;
            this.account1.SelectedIndexChanged += new System.EventHandler(this.Account1_SelectedIndexChanged);
            // 
            // account2
            // 
            this.account2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account2.FormattingEnabled = true;
            this.account2.Location = new System.Drawing.Point(270, 151);
            this.account2.Name = "account2";
            this.account2.Size = new System.Drawing.Size(121, 21);
            this.account2.TabIndex = 22;
            this.account2.SelectedIndexChanged += new System.EventHandler(this.Account2_SelectedIndexChanged);
            // 
            // account3
            // 
            this.account3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account3.FormattingEnabled = true;
            this.account3.Location = new System.Drawing.Point(77, 181);
            this.account3.Name = "account3";
            this.account3.Size = new System.Drawing.Size(121, 21);
            this.account3.TabIndex = 23;
            this.account3.SelectedIndexChanged += new System.EventHandler(this.Account3_SelectedIndexChanged);
            // 
            // account4
            // 
            this.account4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.account4.FormattingEnabled = true;
            this.account4.Location = new System.Drawing.Point(270, 181);
            this.account4.Name = "account4";
            this.account4.Size = new System.Drawing.Size(121, 21);
            this.account4.TabIndex = 24;
            this.account4.SelectedIndexChanged += new System.EventHandler(this.Account4_SelectedIndexChanged);
            // 
            // pr2WidthLabel
            // 
            this.pr2WidthLabel.AutoSize = true;
            this.pr2WidthLabel.Location = new System.Drawing.Point(205, 243);
            this.pr2WidthLabel.Name = "pr2WidthLabel";
            this.pr2WidthLabel.Size = new System.Drawing.Size(62, 13);
            this.pr2WidthLabel.TabIndex = 25;
            this.pr2WidthLabel.Text = "PR2 Width:";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(397, 262);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 27;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
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
            // windows
            // 
            this.windows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.windows.FormattingEnabled = true;
            this.windows.Location = new System.Drawing.Point(77, 262);
            this.windows.Name = "windows";
            this.windows.Size = new System.Drawing.Size(108, 21);
            this.windows.TabIndex = 29;
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(12, 219);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(41, 13);
            this.serverLabel.TabIndex = 30;
            this.serverLabel.Text = "Server:";
            // 
            // servers
            // 
            this.servers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.servers.FormattingEnabled = true;
            this.servers.Location = new System.Drawing.Point(77, 216);
            this.servers.Name = "servers";
            this.servers.Size = new System.Drawing.Size(121, 21);
            this.servers.TabIndex = 31;
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // pr2HeightLabel
            // 
            this.pr2HeightLabel.AutoSize = true;
            this.pr2HeightLabel.Location = new System.Drawing.Point(205, 267);
            this.pr2HeightLabel.Name = "pr2HeightLabel";
            this.pr2HeightLabel.Size = new System.Drawing.Size(65, 13);
            this.pr2HeightLabel.TabIndex = 32;
            this.pr2HeightLabel.Text = "PR2 Height:";
            // 
            // pr2Width
            // 
            this.pr2Width.Location = new System.Drawing.Point(270, 240);
            this.pr2Width.MaxLength = 8;
            this.pr2Width.Name = "pr2Width";
            this.pr2Width.Size = new System.Drawing.Size(100, 20);
            this.pr2Width.TabIndex = 33;
            this.pr2Width.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pr2Width_KeyPress);
            // 
            // pr2Height
            // 
            this.pr2Height.Location = new System.Drawing.Point(270, 262);
            this.pr2Height.MaxLength = 8;
            this.pr2Height.Name = "pr2Height";
            this.pr2Height.Size = new System.Drawing.Size(100, 20);
            this.pr2Height.TabIndex = 34;
            this.pr2Height.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Pr2Height_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 299);
            this.Controls.Add(this.pr2Height);
            this.Controls.Add(this.pr2Width);
            this.Controls.Add(this.pr2HeightLabel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.servers);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.windows);
            this.Controls.Add(this.windowLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pr2WidthLabel);
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
        private System.Windows.Forms.Label pr2WidthLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label windowLabel;
        private System.Windows.Forms.ComboBox windows;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.ComboBox servers;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.Label pr2HeightLabel;
        private System.Windows.Forms.TextBox pr2Width;
        private System.Windows.Forms.TextBox pr2Height;
    }
}

