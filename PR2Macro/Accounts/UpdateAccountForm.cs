using PR2Macro.Properties;

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
            foreach (string? acc in Settings.Default.Accounts)
            {
                if (acc != null && acc.Length > 0)
                {
                    _ = account.Items.Add(acc.Split('|').First());
                }
            }
        }

        private void Account_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account.SelectedItem != null && account.SelectedItem.ToString()?.Length > 0)
            {
                username.Text = account.SelectedItem.ToString();
                foreach (string? acc in Settings.Default.Accounts)
                {
                    if (acc != null && acc.Length > 0)
                    {
                        if (acc.Split('|').First().Equals(account.SelectedItem.ToString()))
                        {
                            password.Text = acc.Split(new char[] { '|' }, 2).Last();
                        }
                    }
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
                _ = MessageBox.Show("Please enter a username.");
            }
            else if (username.Text.Length > 20)
            {
                _ = MessageBox.Show("Maximum username length is 20 characters.");
            }
            else if (username.Text.Contains('|'))
            {
                _ = MessageBox.Show("The | character is not allowed in account usernames.");
            }
            else if (password.Text == null || password.Text.Length < 1)
            {
                _ = MessageBox.Show("Please enter a password.");
            }
            else
            {
                if (account.SelectedItem == null)
                {
                    _ = MessageBox.Show("You need to select an account to update.");
                }
                else
                {
                    string? oldUsername = account.SelectedItem.ToString();
                    string oldPassword;
                    foreach (string? acc in Settings.Default.Accounts)
                    {
                        if (acc != null && acc.Length > 0)
                        {
                            if (acc.Split('|').First().Equals(oldUsername))
                            {
                                oldPassword = acc.Split(new char[] { '|' }, 2).Last();
                                Settings.Default.Accounts.Remove(oldUsername + "|" + oldPassword);
                                break;
                            }
                        }
                    }
                    bool exists = false;
                    foreach (string? acc in Settings.Default.Accounts)
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
                        _ = MessageBox.Show("Account with that name already exists.");
                    }
                    else
                    {
                        _ = Settings.Default.Accounts.Add(username.Text + "|" + password.Text);
                        Settings.Default.Save();
                        _ = MessageBox.Show("Account successfully updated.");
                        Close();
                    }
                }
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !password.UseSystemPasswordChar;
        }
    }
}
