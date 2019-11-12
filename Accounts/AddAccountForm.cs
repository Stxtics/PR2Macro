using PR2Macro.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PR2Macro
{
    public partial class AddAccountForm : Form
    {
        public AddAccountForm()
        {
            InitializeComponent();
            Show();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
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
                bool exists = false;
                foreach (string acc in Settings.Default.Accounts)
                {
                    if (acc != null && acc.Length > 0)
                    {
                        if (acc.Split('|').First().Equals(username.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            exists = true;
                        }
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
                    MessageBox.Show("Account successfully added.");
                    Close();
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
