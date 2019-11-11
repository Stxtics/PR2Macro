using PR2Macro.Macros;
using PR2Macro.Properties;
using PR2Macro.Searches.Titles;
using PR2Macro.Searches.Usernames;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR2Macro
{
    public partial class MainForm : Form
    {
        private readonly KeyHandler ghk;
        public MainForm()
        {
            InitializeComponent();
            levels.SelectedIndex = 0;
            pages.SelectedIndex = 0;
            searchType.SelectedIndex = 0;
            sortOrder.SelectedIndex = 0;
            sortType.SelectedIndex = 0;
            ghk = new KeyHandler(Keys.F1, this);
            ghk.Register();
            ghk = new KeyHandler(Keys.F2, this);
            ghk.Register();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            search.Items.Clear();
            if (searchType.SelectedItem.ToString().Equals("User Name"))
            {
                foreach (string user in Settings.Default.UserSearches)
                {
                    search.Items.Add(user);
                }
            }
            else
            {
                foreach (string title in Settings.Default.TitleSearches)
                {
                    search.Items.Add(title);
                }
            }

            account1.Items.Clear();
            account2.Items.Clear();
            account3.Items.Clear();
            account4.Items.Clear();
            foreach (string acc in Settings.Default.Accounts)
            {
                account1.Items.Add(acc.Split('|').First());
                account2.Items.Add(acc.Split('|').First());
                account3.Items.Add(acc.Split('|').First());
                account4.Items.Add(acc.Split('|').First());
            }

            foreach (Process proc in Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero).OrderBy(x => x.ProcessName))
            {
                if (!windows.Items.Contains(proc.ProcessName))
                {
                    windows.Items.Add(proc.ProcessName);
                }
            }

            await GetServers();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (searchType.SelectedItem.ToString().Equals("User Name"))
            {
                foreach (string user in Settings.Default.UserSearches)
                {
                    if (!search.Items.Contains(user))
                    {
                        search.Items.Add(user);
                    }
                }
                List<string> removeUsers = new List<string>();
                foreach (string user in search.Items)
                {
                    if (!Settings.Default.UserSearches.Contains(user))
                    {
                        removeUsers.Add(user);
                    }
                }
                foreach (string user in removeUsers)
                {
                    search.Items.Remove(user);
                }
            }
            else
            {
                foreach (string title in Settings.Default.TitleSearches)
                {
                    if (!search.Items.Contains(title))
                    {
                        search.Items.Add(title);
                    }
                }
                List<string> removeTitles = new List<string>();
                foreach (string title in search.Items)
                {
                    if (!Settings.Default.TitleSearches.Contains(title))
                    {
                        removeTitles.Add(title);
                    }
                }
                foreach (string title in removeTitles)
                {
                    search.Items.Remove(title);
                }
            }

            foreach (string acc in Settings.Default.Accounts)
            {
                string name = acc.Split('|').First();
                if (!account1.Items.Contains(name))
                {
                    account1.Items.Add(name);
                    account2.Items.Add(name);
                    account3.Items.Add(name);
                    account4.Items.Add(name);
                }
            }
            List<string> removeAccs = new List<string>();
            foreach (string acc in account1.Items)
            {
                if (Settings.Default.Accounts.Cast<string>().Where(x => x.Split('|').First().Equals(acc)).Count() < 1)
                {
                    removeAccs.Add(acc);
                }
            }

            foreach (string acc in removeAccs)
            {
                account1.Items.Remove(acc);
                account2.Items.Remove(acc);
                account3.Items.Remove(acc);
                account4.Items.Remove(acc);
            }
        }

        private void SearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            search.Items.Clear();
            if (searchType.SelectedItem.ToString().Equals("User Name"))
            {
                foreach (string user in Settings.Default.UserSearches)
                {
                    search.Items.Add(user);
                }
            }
            else
            {
                foreach (string title in Settings.Default.TitleSearches)
                {
                    search.Items.Add(title);
                }
            }
        }

        private void Account1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account1.SelectedItem != null)
            {
                if (account2.SelectedItem != null && account1.SelectedItem.ToString() == account2.SelectedItem.ToString())
                {
                    account2.SelectedItem = null;
                }
                else if (account3.SelectedItem != null && account1.SelectedItem.ToString() == account3.SelectedItem.ToString())
                {
                    account3.SelectedItem = null;
                }
                else if (account4.SelectedItem != null && account1.SelectedItem.ToString() == account4.SelectedItem.ToString())
                {
                    account4.SelectedItem = null;
                }
            }
        }

        private void Account2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account2.SelectedItem != null)
            {
                if (account1.SelectedItem != null && account2.SelectedItem.ToString() == account1.SelectedItem.ToString())
                {
                    account1.SelectedItem = null;
                }
                else if (account3.SelectedItem != null && account2.SelectedItem.ToString() == account3.SelectedItem.ToString())
                {
                    account3.SelectedItem = null;
                }
                else if (account4.SelectedItem != null && account2.SelectedItem.ToString() == account4.SelectedItem.ToString())
                {
                    account4.SelectedItem = null;
                }
            }
        }

        private void Account3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account3.SelectedItem != null)
            {
                if (account1.SelectedItem != null && account3.SelectedItem.ToString() == account1.SelectedItem.ToString())
                {
                    account1.SelectedItem = null;
                }
                else if (account2.SelectedItem != null && account3.SelectedItem.ToString() == account2.SelectedItem.ToString())
                {
                    account2.SelectedItem = null;
                }
                else if (account4.SelectedItem != null && account3.SelectedItem.ToString() == account4.SelectedItem.ToString())
                {
                    account4.SelectedItem = null;
                }
            }
        }

        private void Account4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account4.SelectedItem != null)
            {
                if (account1.SelectedItem != null && account4.SelectedItem.ToString() == account1.SelectedItem.ToString())
                {
                    account1.SelectedItem = null;
                }
                else if (account2.SelectedItem != null && account4.SelectedItem.ToString() == account2.SelectedItem.ToString())
                {
                    account2.SelectedItem = null;
                }
                else if (account3.SelectedItem != null && account4.SelectedItem.ToString() == account3.SelectedItem.ToString())
                {
                    account3.SelectedItem = null;
                }
            }
        }

        private void RandomButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            levels.SelectedIndex = rand.Next(levels.Items.Count);
            pages.SelectedIndex = rand.Next(pages.Items.Count);
        }

        private void RandomAllButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            levels.SelectedIndex = rand.Next(levels.Items.Count);
            pages.SelectedIndex = rand.Next(pages.Items.Count);
            searchType.SelectedIndex = rand.Next(searchType.Items.Count);
            sortOrder.SelectedIndex = rand.Next(sortOrder.Items.Count);
            sortType.SelectedIndex = rand.Next(sortType.Items.Count);
        }

        private void AddAccount_Click(object sender, EventArgs e)
        {
            _ = new AddAccountForm();
        }

        private void UpdateAccount_Click(object sender, EventArgs e)
        {
            _ = new UpdateAccountForm();
        }

        private void RemoveAccount_Click(object sender, EventArgs e)
        {
            _ = new RemoveAccountForm();
        }

        private void AddUsername_Click(object sender, EventArgs e)
        {
            _ = new AddUsernameForm();
        }

        private void UpdateUsername_Click(object sender, EventArgs e)
        {
            _ = new UpdateUsernameForm();
        }

        private void RemoveUsername_Click(object sender, EventArgs e)
        {
            _ = new RemoveUsernameForm();
        }

        private void AddTitle_Click(object sender, EventArgs e)
        {
            _ = new AddTitleForm();
        }

        private void UpdateTitle_Click(object sender, EventArgs e)
        {
            _ = new UpdateTitleForm();
        }

        private void RemoveTitle_Click(object sender, EventArgs e)
        {
            _ = new RemoveTitleForm();
        }

        private bool running = false;
        private async void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!running)
                {
                    if (simType.SelectedItem == null)
                    {
                        MessageBox.Show("You need to select a sim type.");
                    }
                    else if (search.SelectedItem == null)
                    {
                        MessageBox.Show("You need to select a search term.");
                    }
                    else if (account1.SelectedItem == null || account2.SelectedItem == null || account3.SelectedItem == null || account4.SelectedItem == null)
                    {
                        MessageBox.Show("You need to select 4 accounts to macro with.");
                    }
                    else if (servers.SelectedItem == null)
                    {
                        MessageBox.Show("You need to select a server to macro on.");
                    }
                    else if (windows.SelectedItem == null)
                    {
                        MessageBox.Show("You need to select a window where the sim page is.");
                    }
                    else if (pr2Width.Text.Length < 1 || pr2Height.Text.Length < 1)
                    {
                        MessageBox.Show("You need to enter the width and height of a PR2.");
                    }
                    else if (!int.TryParse(pr2Width.Text, out int width) || !int.TryParse(pr2Height.Text, out int height))
                    {
                        MessageBox.Show("The PR2 width and height both need to be positive whole numbers.");
                    }
                    else
                    {
                        running = true;
                        SimFunctions functions = new SimFunctions(account1.SelectedItem.ToString(), account2.SelectedItem.ToString(), account3.SelectedItem.ToString(), account4.SelectedItem.ToString(), servers.SelectedItem.ToString(), windows.SelectedItem.ToString(), width, height);

                        Task task1 = Sim(functions);
                        Task task2 = HappyHourCheck(functions);

                        await Task.WhenAll(task1, task2);
                    }
                }
                else
                {
                    MessageBox.Show("Macro is already running.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool inLevel = false;

        public async Task Sim(SimFunctions functions)
        {
            await functions.Login();
            await functions.SearchBy(searchType.SelectedItem.ToString());
            await functions.SortBy(sortType.SelectedItem.ToString(), sortOrder.SelectedItem.ToString());
            await functions.EnterSearch(search.SelectedItem.ToString());
            await functions.SelectPage(int.Parse(pages.SelectedItem.ToString()));
            await functions.EnterLevel(int.Parse(levels.SelectedItem.ToString()), simType.SelectedItem.ToString());
            inLevel = true;
            if (simType.SelectedItem.ToString() == "1p")
            {
                while (true)
                {
                    await Task.Delay(6000);
                    await functions.Quit(simType.SelectedItem.ToString());
                    await functions.ReturnToLobby(2);
                    await Task.Delay(115000);
                    await functions.DCCheck(inLevel);
                    await functions.ReturnToLobby(1);
                    inLevel = false;
                    while (readyToSwitch)
                    {
                        await Task.Delay(1000);
                    }
                    await functions.DCCheck(inLevel);
                    await functions.EnterLevel(int.Parse(levels.SelectedItem.ToString()), simType.SelectedItem.ToString());
                    inLevel = true;
                }
            }
            else if (simType.SelectedItem.ToString() == "4p")
            {
                while (true)
                {
                    await Task.Delay(125000);
                    await functions.DCCheck(inLevel);
                    await functions.ReturnToLobby();
                    inLevel = false;
                    while (readyToSwitch)
                    {
                        await Task.Delay(1000);
                    }
                    await functions.DCCheck(inLevel);
                    await functions.EnterLevel(int.Parse(levels.SelectedItem.ToString()), simType.SelectedItem.ToString());
                    inLevel = true;
                }
            }
            else if (simType.SelectedItem.ToString() == "Objective")
            {
                while (true)
                {
                    await Task.Delay(115000);
                    await functions.DCCheck(inLevel);
                    await functions.Quit(simType.SelectedItem.ToString());
                    await functions.ReturnToLobby();
                    inLevel = false;
                    while (readyToSwitch)
                    {
                        await Task.Delay(1000);
                    }
                    await functions.DCCheck(inLevel);
                    await functions.EnterLevel(int.Parse(levels.SelectedItem.ToString()), simType.SelectedItem.ToString());
                    inLevel = true;
                }
            }
        }

        private bool readyToSwitch = false;

        public async Task HappyHourCheck(SimFunctions functions)
        {
            while (true)
            {
                await Task.Delay(90000);
                HttpClient web = new HttpClient();
                string text = null;
                try
                {
                    text = await web.GetStringAsync("https://pr2hub.com/files/server_status_2.txt");
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("Connection to PR2 Hub was not successfull.");
                }
                web.Dispose();
                if (text != null)
                {
                    string[] serversinfo = text.Split('}');
                    foreach (string server_id in serversinfo)
                    {
                        string name = GetBetween(server_id, "\",\"server_name\":\"", "\",\"address\":\"");
                        if (name.Length <= 0)
                        {
                            break;
                        }
                        string happyHour = GetBetween(server_id + "}", "\",\"happy_hour\":", "}");
                        int serverId = int.Parse(GetBetween(server_id, "\"server_id\":\"", "\",\"server_name\":\""));
                        if (happyHour.Equals("1") && serverId < 12)
                        {
                            readyToSwitch = true;
                            while (inLevel)
                            {
                                await Task.Delay(1000);
                            }
                            await functions.SwitchServer("!! " + name);
                            readyToSwitch = false;
                            break;
                        }
                    }
                }
            }
        }

        public async Task GetServers()
        {
            HttpClient web = new HttpClient();
            string text = null;
            try
            {
                text = await web.GetStringAsync("https://pr2hub.com/files/server_status_2.txt");
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Connection to PR2 Hub was not successfull.");
            }
            web.Dispose();
            if (text != null)
            {
                servers.Items.Clear();
                string[] serversinfo = text.Split('}');
                foreach (string server_id in serversinfo)
                {
                    string name = GetBetween(server_id, "\",\"server_name\":\"", "\",\"address\":\"");
                    if (name.Length <= 0)
                    {
                        break;
                    }
                    string pop = GetBetween(server_id, "\",\"population\":\"", "\",\"status\":\"");
                    string status = GetBetween(server_id, "\",\"status\":\"", "\",\"guild_id\":\"");
                    string happyHour = GetBetween(server_id + "}", "\",\"happy_hour\":", "}");
                    int serverId = int.Parse(GetBetween(server_id, "\"server_id\":\"", "\",\"server_name\":\""));
                    if (status.Equals("down", StringComparison.InvariantCultureIgnoreCase) && serverId < 12)
                    {
                        servers.Items.Add(name + " (down)");
                    }
                    else if (happyHour.Equals("1") && serverId < 12)
                    {
                        servers.Items.Add("!! " + name + " (" + pop + " online)");
                    }
                    else if (happyHour.Equals("1") && serverId > 11)
                    {
                        //servers.Items.Add("* !! " + name + " (" + pop + " online)");
                    }
                    else if (status.Equals("down", StringComparison.InvariantCultureIgnoreCase) && serverId > 10)
                    {
                        //servers.Items.Add("* " + name + " (down)");
                    }
                    else if (serverId > 11)
                    {
                        //servers.Items.Add("* " + name + " (" + pop + " online)");
                    }
                    else
                    {
                        servers.Items.Add(name + " (" + pop + " online)");
                    }
                }
            }
        }

        public string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        private void HandleHotkey(IntPtr lParam)
        {
            switch (GetKey(lParam))
            {
                case Keys.F1:
                    {
                        Environment.Exit(0);
                        break;
                    }
                case Keys.F2:
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                HandleHotkey(m.LParam);
            }

            base.WndProc(ref m);
        }

        private Keys GetKey(IntPtr LParam)
        {
            return (Keys)((LParam.ToInt32()) >> 16);
        }

        private void Pr2Width_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Pr2Height_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
