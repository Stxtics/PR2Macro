using PR2Macro.Properties;
using System;
using System.Windows.Forms;

namespace PR2Macro.Searches.Usernames
{
    public partial class AddUsernameForm : Form
    {
        public AddUsernameForm()
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
            else
            {
                bool exists = false;
                foreach (string user in Settings.Default.UserSearches)
                {
                    if (user.Equals(username.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                    MessageBox.Show("That username already exists.");
                }
                else
                {
                    Settings.Default.UserSearches.Add(username.Text);
                    Settings.Default.Save();
                    MessageBox.Show("Username was added successfully.");
                    Close();
                }
            }
        }
    }
}
