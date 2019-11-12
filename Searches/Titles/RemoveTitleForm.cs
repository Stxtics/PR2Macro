using PR2Macro.Properties;
using System;
using System.Windows.Forms;

namespace PR2Macro.Searches.Titles
{
    public partial class RemoveTitleForm : Form
    {
        public RemoveTitleForm()
        {
            InitializeComponent();
            Show();
        }

        private void RemoveTitleForm_Load(object sender, EventArgs e)
        {
            foreach (string title in Settings.Default.TitleSearches)
            {
                if (title != null && title.Length > 0)
                {
                    titles.Items.Add(title);
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (titles.SelectedItem == null)
            {
                MessageBox.Show("You need to select a title to remove.");
            }
            else
            {
                Settings.Default.TitleSearches.Remove(titles.SelectedItem.ToString());
                Settings.Default.Save();
                MessageBox.Show("Title was removed successfully.");
                Close();
            }
        }
    }
}
