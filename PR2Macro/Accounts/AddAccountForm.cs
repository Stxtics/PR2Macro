using PR2Macro.Properties;

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
                    _ = MessageBox.Show("Account successfully added.");
                    Close();
                }
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !password.UseSystemPasswordChar;
        }
    }
}
