using PR2Macro.Properties;
using System;
using System.Windows.Forms;

namespace PR2Macro.Searches.Titles
{
    public partial class AddTitleForm : Form
    {
        public AddTitleForm()
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
            if (string.IsNullOrWhiteSpace(title.Text))
            {
                MessageBox.Show("Please enter a title.");
            }
            else
            {
                bool exists = false;
                foreach (string search in Settings.Default.TitleSearches)
                {
                    if (search.Equals(title.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                    MessageBox.Show("That title already exists.");
                }
                else
                {
                    Settings.Default.TitleSearches.Add(title.Text);
                    Settings.Default.Save();
                    MessageBox.Show("Title was added successfully.");
                    Close();
                }
            }
        }
    }
}
