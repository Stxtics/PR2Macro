using PR2Macro.Properties;

namespace PR2Macro.Searches.Usernames
{
    public partial class RemoveUsernameForm : Form
    {
        public RemoveUsernameForm()
        {
            InitializeComponent();
            Show();
        }

        private void RemoveUsernameForm_Load(object sender, EventArgs e)
        {
            foreach (string? user in Settings.Default.UserSearches)
            {
                if (user != null && user.Length > 0)
                {
                    _ = username.Items.Add(user);
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (username.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a username to remove.");
            }
            else
            {
                Settings.Default.UserSearches.Remove(username.SelectedItem.ToString());
                Settings.Default.Save();
                _ = MessageBox.Show("Username was removed successfully.");
                Close();
            }
        }
    }
}
