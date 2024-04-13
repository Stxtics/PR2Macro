using PR2Macro.Properties;

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
                _ = MessageBox.Show("Please enter a title.");
            }
            else
            {
                bool exists = false;
                foreach (string? search in Settings.Default.TitleSearches)
                {
                    if (search != null && search.Length > 0)
                    {
                        if (search.Equals(title.Text, StringComparison.InvariantCultureIgnoreCase))
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
                    _ = Settings.Default.TitleSearches.Add(title.Text);
                    Settings.Default.Save();
                    _ = MessageBox.Show("Title was added successfully.");
                    Close();
                }
            }
        }
    }
}
