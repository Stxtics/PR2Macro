using PR2Macro.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PR2Macro
{
    public partial class UpdateAccountForm : Form
    {
        public UpdateAccountForm()
        {
            InitializeComponent();
            Show();
        }

        private void UpdateAccountForm_Load(object sender, EventArgs e)
        {
            foreach (string acc in Settings.Default.Accounts)
            {
                account.Items.Add(acc.Split('|').First());
            }
        }

        private void Account_SelectedIndexChanged(object sender, EventArgs e)
        {
            username.Text = account.SelectedItem.ToString();
            foreach (string acc in Settings.Default.Accounts)
            {
                if (acc.Split('|').First().Equals(account.SelectedItem.ToString()))
                {
                    password.Text = acc.Split(new char[] { '|' }, 2).Last();
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                MessageBox.Show("Please enter a username.");
            }
            else if (username.Text.Length > 20)
            {
                MessageBox.Show("Maximum username length is 20 characters.");
            }
            else if (username.Text.Contains("|"))
            {
                MessageBox.Show("The | character is not allowed in account usernames.");
            }
            else if (password.Text == null || password.Text.Length < 1)
            {
                MessageBox.Show("Please enter a password.");
            }
            else
            {
                if (account.SelectedItem == null)
                {
                    MessageBox.Show("You need to select an account to update.");
                }
                else
                {
                    string oldUsername = account.SelectedItem.ToString();
                    string oldPassword;
                    foreach (string acc in Settings.Default.Accounts)
                    {
                        if (acc.Split('|').First().Equals(oldUsername))
                        {
                            oldPassword = acc.Split(new char[] { '|' }, 2).Last();
                            Settings.Default.Accounts.Remove(oldUsername + "|" + oldPassword);
                            break;
                        }
                    }
                    bool exists = false;
                    foreach (string acc in Settings.Default.Accounts)
                    {
                        if (acc.Split('|').First().Equals(username.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            exists = true;
                        }
                    }
                    if (exists)
                    {
                        MessageBox.Show("Account with that name already exists.");
                    }
                    else
                    {
                        Settings.Default.Accounts.Add(username.Text + "|" + password.Text);
                        Settings.Default.Save();
                        MessageBox.Show("Account successfully updated.");
                        Close();
                    }
                }
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (password.UseSystemPasswordChar)
            {
                password.UseSystemPasswordChar = false;
            }
            else
            {
                password.UseSystemPasswordChar = true;
            }
        }
    }
}
