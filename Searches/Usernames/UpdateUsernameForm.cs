﻿using PR2Macro.Properties;
using System;
using System.Windows.Forms;

namespace PR2Macro.Searches.Usernames
{
    public partial class UpdateUsernameForm : Form
    {
        public UpdateUsernameForm()
        {
            InitializeComponent();
            Show();
        }

        private void UpdateUsernameForm_Load(object sender, EventArgs e)
        {
            foreach (string user in Settings.Default.UserSearches)
            {
                if (user != null && user.Length > 0)
                {
                    username.Items.Add(user);
                }
            }
        }

        private void Username_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (username.SelectedItem != null && username.SelectedItem.ToString().Length > 0)
            {
                newUsername.Text = username.SelectedItem.ToString();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateButtom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newUsername.Text))
            {
                MessageBox.Show("Please enter a username.");
            }
            else if (newUsername.Text.Length > 20)
            {
                MessageBox.Show("Maximum username length is 20 characters.");
            }
            else if (username.SelectedItem == null)
            {
                MessageBox.Show("You need to select a username to update.");
            }
            else if (username.SelectedItem.ToString().Equals(newUsername.Text))
            {
                MessageBox.Show("New username is the same as old username.");
            }
            else
            {
                bool exists = false;
                foreach (string user in Settings.Default.UserSearches)
                {
                    if (user != null && user.Length > 0)
                    {
                        if (user.Equals(newUsername.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            exists = true;
                        }
                    }
                }
                if (exists)
                {
                    MessageBox.Show("That username already exists.");
                }
                else
                {
                    Settings.Default.UserSearches.Remove(username.SelectedItem.ToString());
                    Settings.Default.UserSearches.Add(newUsername.Text);
                    Settings.Default.Save();
                    MessageBox.Show("Username was updated successfully.");
                    Close();
                }
            }
        }
    }
}
