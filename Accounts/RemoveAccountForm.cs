using PR2Macro.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PR2Macro
{
    public partial class RemoveAccountForm : Form
    {
        public RemoveAccountForm()
        {
            InitializeComponent();
            Show();
        }

        private void RemoveAccountForm_Load(object sender, EventArgs e)
        {
            foreach (string acc in Settings.Default.Accounts)
            {
                account.Items.Add(acc.Split('|').First());
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (account.SelectedItem == null)
            {
                MessageBox.Show("You need to select an account to remove.");
            }
            else
            {
                string username = account.SelectedItem.ToString();
                foreach (string acc in Settings.Default.Accounts)
                {
                    if (acc.Split('|').First().Equals(username))
                    {
                        string password = acc.Split(new char[] { '|' }, 2).Last();
                        Settings.Default.Accounts.Remove(username + "|" + password);
                        Settings.Default.Save();
                        break;
                    }
                }
                MessageBox.Show("Account was removed successfully.");
                Close();
            }
        }
    }
}
