using PR2Macro.Properties;

namespace PR2Macro.Searches.Titles
{
    public partial class UpdateTitleForm : Form
    {
        public UpdateTitleForm()
        {
            InitializeComponent();
            Show();
        }

        private void UpdateTitleForm_Load(object sender, EventArgs e)
        {
            foreach (string? title in Settings.Default.TitleSearches)
            {
                if (title != null && title.Length > 0)
                {
                    _ = titles.Items.Add(title);
                }
            }
        }

        private void Titles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (titles.SelectedItem != null && titles.SelectedItem.ToString()?.Length > 0)
            {
                newTitle.Text = titles.SelectedItem.ToString();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newTitle.Text))
            {
                _ = MessageBox.Show("Please enter a title.");
            }
            else if (titles.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a title to update.");
            }
            else if (titles.SelectedItem.ToString() == newTitle.Text)
            {
                _ = MessageBox.Show("New title is the same as old title.");
            }
            else
            {
                bool exists = false;
                foreach (string? title in Settings.Default.TitleSearches)
                {
                    if (title != null && title.Length > 0)
                    {
                        if (title.Equals(newTitle.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            exists = true;
                        }
                    }
                }
                if (exists)
                {
                    _ = MessageBox.Show("That title already exists.");
                }
                else
                {
                    Settings.Default.TitleSearches.Remove(titles.SelectedItem.ToString());
                    _ = Settings.Default.TitleSearches.Add(newTitle.Text);
                    Settings.Default.Save();
                    _ = MessageBox.Show("Title was updated successfully.");
                    Close();
                }
            }
        }
    }
}
