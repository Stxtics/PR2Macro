namespace PR2Macro.Searches.Titles
{
    partial class UpdateTitleForm
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
            this.titles = new System.Windows.Forms.ComboBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.newTitle = new System.Windows.Forms.TextBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.newTitleLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titles
            // 
            this.titles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.titles.FormattingEnabled = true;
            this.titles.Location = new System.Drawing.Point(87, 12);
            this.titles.Name = "titles";
            this.titles.Size = new System.Drawing.Size(121, 21);
            this.titles.TabIndex = 0;
            this.titles.SelectedIndexChanged += new System.EventHandler(this.Titles_SelectedIndexChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(47, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(30, 13);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Title:";
            // 
            // newTitle
            // 
            this.newTitle.Location = new System.Drawing.Point(108, 51);
            this.newTitle.Name = "newTitle";
            this.newTitle.Size = new System.Drawing.Size(100, 20);
            this.newTitle.TabIndex = 2;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(133, 97);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 3;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // newTitleLabel
            // 
            this.newTitleLabel.AutoSize = true;
            this.newTitleLabel.Location = new System.Drawing.Point(47, 54);
            this.newTitleLabel.Name = "newTitleLabel";
            this.newTitleLabel.Size = new System.Drawing.Size(55, 13);
            this.newTitleLabel.TabIndex = 4;
            this.newTitleLabel.Text = "New Title:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(50, 97);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // UpdateTitleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 142);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.newTitleLabel);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.newTitle);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titles);
            this.Name = "UpdateTitleForm";
            this.Text = "Update Title";
            this.Load += new System.EventHandler(this.UpdateTitleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox titles;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox newTitle;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label newTitleLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}