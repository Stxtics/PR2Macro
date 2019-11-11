namespace PR2Macro.Searches.Usernames
{
    partial class UpdateUsernameForm
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
            this.username = new System.Windows.Forms.ComboBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.updateButtom = new System.Windows.Forms.Button();
            this.newUsername = new System.Windows.Forms.TextBox();
            this.newUsernameLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.username.FormattingEnabled = true;
            this.username.Location = new System.Drawing.Point(132, 25);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(121, 21);
            this.username.TabIndex = 0;
            this.username.SelectedIndexChanged += new System.EventHandler(this.Username_SelectedIndexChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(68, 28);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "Username:";
            // 
            // updateButtom
            // 
            this.updateButtom.Location = new System.Drawing.Point(178, 107);
            this.updateButtom.Name = "updateButtom";
            this.updateButtom.Size = new System.Drawing.Size(75, 23);
            this.updateButtom.TabIndex = 2;
            this.updateButtom.Text = "Update";
            this.updateButtom.UseVisualStyleBackColor = true;
            this.updateButtom.Click += new System.EventHandler(this.UpdateButtom_Click);
            // 
            // newUsername
            // 
            this.newUsername.Location = new System.Drawing.Point(153, 64);
            this.newUsername.MaxLength = 20;
            this.newUsername.Name = "newUsername";
            this.newUsername.Size = new System.Drawing.Size(100, 20);
            this.newUsername.TabIndex = 3;
            // 
            // newUsernameLabel
            // 
            this.newUsernameLabel.AutoSize = true;
            this.newUsernameLabel.Location = new System.Drawing.Point(68, 67);
            this.newUsernameLabel.Name = "newUsernameLabel";
            this.newUsernameLabel.Size = new System.Drawing.Size(83, 13);
            this.newUsernameLabel.TabIndex = 4;
            this.newUsernameLabel.Text = "New Username:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(71, 107);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // UpdateUsernameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 151);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.newUsernameLabel);
            this.Controls.Add(this.newUsername);
            this.Controls.Add(this.updateButtom);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.username);
            this.Name = "UpdateUsernameForm";
            this.Text = "Update Username";
            this.Load += new System.EventHandler(this.UpdateUsernameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox username;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button updateButtom;
        private System.Windows.Forms.TextBox newUsername;
        private System.Windows.Forms.Label newUsernameLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}