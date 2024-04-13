using Newtonsoft.Json;
using PR2Macro.Enums;
using PR2Macro.Interfaces;
using PR2Macro.Models;
using PR2Macro.Properties;
using PR2Macro.Searches.Titles;
using PR2Macro.Searches.Usernames;
using System.Diagnostics;
using System.Text;
using SortOrder = PR2Macro.Enums.SortOrder;

namespace PR2Macro;

public partial class MainForm : Form
{
    private readonly IHotkeyService _hotkeyService;
    private readonly ISimmingService _simmingService;

    public MainForm(IHotkeyService hotkeyService, ISimmingService simmingService)
    {
        InitializeComponent();

        _hotkeyService = hotkeyService;
        _simmingService = simmingService;

        levels.SelectedIndex = 0;
        pages.SelectedIndex = 0;
        searchType.SelectedIndex = 0;
        sortOrder.SelectedIndex = 0;
        sortType.SelectedIndex = 0;

        _ = _hotkeyService.Register(Keys.F1, this);
        _ = _hotkeyService.Register(Keys.F2, this);
        _ = _hotkeyService.Unregister(Keys.F1, this);
        _ = _hotkeyService.Unregister(Keys.F2, this);
    }

    private async void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            search.Items.Clear();
            if (searchType.SelectedItem.ToString() == "User Name")
            {
                foreach (string? user in Settings.Default.UserSearches)
                {
                    if (user != null && user.Length > 0)
                    {
                        _ = search.Items.Add(user);
                    }
                }
            }
            else
            {
                foreach (string? title in Settings.Default.TitleSearches)
                {
                    if (title != null && title.Length > 0)
                    {
                        _ = search.Items.Add(title);
                    }
                }
            }

            account1.Items.Clear();
            account2.Items.Clear();
            account3.Items.Clear();
            account4.Items.Clear();
            foreach (string? acc in Settings.Default.Accounts)
            {
                if (acc != null && acc.Length > 0)
                {
                    _ = account1.Items.Add(acc.Split('|').First());
                    _ = account2.Items.Add(acc.Split('|').First());
                    _ = account3.Items.Add(acc.Split('|').First());
                    _ = account4.Items.Add(acc.Split('|').First());
                }
            }

            monitors.Items.Clear();
            for (int i = 1; i <= Screen.AllScreens.Length; i++)
            {
                _ = monitors.Items.Add(i.ToString());
            }
            monitors.SelectedItem = monitors.Items[0];

            sizes.Items.Clear();
            foreach (string size in Enum.GetNames(typeof(MacroSize)))
            {
                _ = sizes.Items.Add(size);
            }
            sizes.SelectedItem = sizes.Items[0];

            await GetServers();
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.ToString());
        }
    }

    private void MainForm_Activated(object sender, EventArgs e)
    {
        if (searchType.SelectedItem.ToString() == "User Name")
        {
            foreach (string? user in Settings.Default.UserSearches)
            {
                if (user != null && user.Length > 0)
                {
                    if (!search.Items.Contains(user))
                    {
                        _ = search.Items.Add(user);
                    }
                }
            }
            List<string> removeUsers = new();
            foreach (string? user in search.Items)
            {
                if (user != null && user.Length > 0)
                {
                    if (!Settings.Default.UserSearches.Contains(user))
                    {
                        removeUsers.Add(user);
                    }
                }
            }
            foreach (string? user in removeUsers)
            {
                if (user != null && user.Length > 0)
                {
                    search.Items.Remove(user);
                }
            }
        }
        else
        {
            foreach (string? title in Settings.Default.TitleSearches)
            {
                if (title != null && title.Length > 0)
                {
                    if (!search.Items.Contains(title))
                    {
                        _ = search.Items.Add(title);
                    }
                }
            }
            List<string> removeTitles = new();
            foreach (string? title in search.Items)
            {
                if (title != null && title.Length > 0)
                {
                    if (!Settings.Default.TitleSearches.Contains(title))
                    {
                        removeTitles.Add(title);
                    }
                }
            }
            foreach (string? title in removeTitles)
            {
                if (title != null && title.Length > 0)
                {
                    search.Items.Remove(title);
                }
            }
        }
        foreach (string? acc in Settings.Default.Accounts)
        {
            if (acc != null && acc.Length > 0)
            {
                string name = acc.Split('|').First();
                if (name != null && name.Length > 0 && !account1.Items.Contains(name))
                {
                    _ = account1.Items.Add(name);
                    _ = account2.Items.Add(name);
                    _ = account3.Items.Add(name);
                    _ = account4.Items.Add(name);
                }
            }
        }
        List<string> removeAccs = new();
        foreach (string? acc in account1.Items)
        {
            if (acc != null && acc.Length > 0)
            {
                if (!Settings.Default.Accounts.Cast<string>().Where(x => x != null).Any(x => x.Split('|').First().Equals(acc)))
                {
                    removeAccs.Add(acc);
                }
            }
        }
        foreach (string? acc in removeAccs)
        {
            if (acc != null && acc.Length > 0)
            {
                account1.Items.Remove(acc);
                account2.Items.Remove(acc);
                account3.Items.Remove(acc);
                account4.Items.Remove(acc);
            }
        }
    }

    private void SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        search.Items.Clear();
        if (searchType.SelectedItem.ToString() == "User Name")
        {
            foreach (string? user in Settings.Default.UserSearches)
            {
                if (user != null && user.Length > 0)
                {
                    _ = search.Items.Add(user);
                }
            }
        }
        else
        {
            foreach (string? title in Settings.Default.TitleSearches)
            {
                if (title != null && title.Length > 0)
                {
                    _ = search.Items.Add(title);
                }
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
        Random rand = new();
        levels.SelectedIndex = rand.Next(levels.Items.Count);
        pages.SelectedIndex = rand.Next(pages.Items.Count);
    }

    private void RandomAllButton_Click(object sender, EventArgs e)
    {
        Random rand = new();
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

    private async void StartButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (simType.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a sim type.");
            }
            else if (search.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a search term.");
            }
            else if (account1.SelectedItem == null || account2.SelectedItem == null || account3.SelectedItem == null || account4.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select 4 accounts to macro with.");
            }
            else if (servers.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a server to macro on.");
            }
            else if (!double.TryParse(width.Text, out double pr2Width) || !double.TryParse(height.Text, out double pr2Height))
            {
                _ = MessageBox.Show("You need to enter a valid width and height");
            }
            else
            {
                SearchBy searchBy = searchType.SelectedItem.ToString() switch
                {
                    "User Name" => SearchBy.UserName,
                    "Course Title" => SearchBy.LevelTitle,
                    "Level ID" => SearchBy.LevelId,
                    _ => throw new InvalidOperationException("Invalid search by")
                };

                SortBy sortBy = sortType.SelectedItem.ToString() switch
                {
                    "Date" => SortBy.Date,
                    "Alphabetical" => SortBy.Alphabetical,
                    "Rating" => SortBy.Rating,
                    "Popularity" => SortBy.Popularity,
                    _ => throw new InvalidOperationException("Invalid sort by")
                };

                SortOrder sortOrderEnum = sortOrder.SelectedItem.ToString() switch
                {
                    "Descending" => SortOrder.Descending,
                    "Ascending" => SortOrder.Ascending,
                    _ => throw new InvalidOperationException("Invalid sort rrder")
                };

                Level level = levels.SelectedItem.ToString() switch
                {
                    "1" => Level.One,
                    "2" => Level.Two,
                    "3" => Level.Three,
                    "4" => Level.Four,
                    "5" => Level.Five,
                    "6" => Level.Six,
                    _ => throw new InvalidOperationException("Invalid level")
                };

                Page page = pages.SelectedItem.ToString() switch
                {
                    "1" => Page.One,
                    "2" => Page.Two,
                    "3" => Page.Three,
                    "4" => Page.Four,
                    "5" => Page.Five,
                    "6" => Page.Six,
                    "7" => Page.Seven,
                    "8" => Page.Eight,
                    "9" => Page.Nine,
                    _ => throw new InvalidOperationException("Invalid page")
                };

                SimType simTypeEnum = simType.SelectedItem.ToString() switch
                {
                    "1p" => SimType.OnePlayer,
                    "4p" => SimType.FourPlayers,
                    "Objective" => SimType.Objective,
                    _ => throw new InvalidOperationException("Invalid Sim Type")
                };

                Server server = servers.SelectedIndex switch
                {
                    0 => Server.Derron,
                    1 => Server.Carina,
                    2 => Server.Grayan,
                    3 => Server.Fitz,
                    4 => Server.Tournament,
                    _ => throw new InvalidOperationException("Invalid server")
                };

                MacroSize macroSize = sizes.SelectedIndex switch
                {
                    0 => MacroSize.Big,
                    1 => MacroSize.Medium,
                    2 => MacroSize.Small,
                    _ => throw new InvalidOperationException("Invalid macro size")
                };

                string acc1 = account1.SelectedItem.ToString() ?? throw new InvalidOperationException("Account 1 is empty");
                string acc2 = account2.SelectedItem.ToString() ?? throw new InvalidOperationException("Account 2 is empty");
                string acc3 = account3.SelectedItem.ToString() ?? throw new InvalidOperationException("Account 3 is empty");
                string acc4 = account4.SelectedItem.ToString() ?? throw new InvalidOperationException("Account 4 is empty");
                string searchString = search.SelectedItem.ToString() ?? throw new InvalidOperationException("Search is empty");

                MacroInfo macroInfo = new(acc1, acc2, acc3, acc4, searchBy, sortBy, sortOrderEnum, level, page, simTypeEnum, searchString, server, monitors.SelectedIndex, useHHServer.Checked, macroSize, pr2Width, pr2Height);

                StartMacroResult result = await _simmingService.Start(macroInfo);

                switch (result)
                {
                    case StartMacroResult.Success:
                        _ = macrosToControl.Items.Add(macroInfo);
                        break;
                    case StartMacroResult.AccountInUse:
                        _ = MessageBox.Show("An account in this macro is already in use.");
                        break;
                    case StartMacroResult.ServerInUse:
                        _ = MessageBox.Show("Another macro is already using this server.");
                        break;
                    case StartMacroResult.NotEnoughWindows:
                        _ = MessageBox.Show("There is not enough 4PR2 windows to use.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private void PauseButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (macrosToControl.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a macro to control.");
            }
            else
            {
                PauseMacroResult result = _simmingService.PauseMacro((MacroInfo)macrosToControl.SelectedItem);

                switch (result)
                {
                    case PauseMacroResult.Paused:
                        pauseButton.Text = "Resume";
                        break;
                    case PauseMacroResult.Resumed:
                        pauseButton.Text = "Pause";
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private void MacrosToControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (macrosToControl.SelectedItem != null)
            {
                bool result = _simmingService.IsMacroPaused((MacroInfo)macrosToControl.SelectedItem);

                pauseButton.Text = result ? "Resume" : "Pause";
            }
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private async void SwitchServerButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (macrosToControl.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a macro to control.");
            }
            else if (serversToSwitch.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a server to switch to.");
            }
            else
            {
                Server server = serversToSwitch.SelectedIndex switch
                {
                    0 => Server.Derron,
                    1 => Server.Carina,
                    2 => Server.Grayan,
                    3 => Server.Fitz,
                    4 => Server.Tournament,
                    _ => Server.Derron,
                };

                SwitchServerResult result = await _simmingService.SwitchServerRequest((MacroInfo)macrosToControl.SelectedItem, server);

                switch (result)
                {
                    case SwitchServerResult.CurrentMacroUsingServer:
                        _ = MessageBox.Show("This macro is already using this server.");
                        break;
                    case SwitchServerResult.AnotherMacroUsingServer:
                        _ = MessageBox.Show("Another macro is already using this server.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private async void ServersToSwitch_Enter(object sender, EventArgs e)
    {
        try
        {
            await GetServers();
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private async void StopButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (macrosToControl.SelectedItem == null)
            {
                _ = MessageBox.Show("You need to select a macro to control.");
            }
            else
            {
                var macroToStop = (MacroInfo)macrosToControl.SelectedItem;
                macrosToControl.Items.Remove(macrosToControl.SelectedItem);
                MacroInfo macroInfo = await _simmingService.StopMacro(macroToStop);

                int acc1Exp = CalculateExpGained(macroInfo.Account1StartRank, macroInfo.Account1StartExp, macroInfo.Account1EndRank, macroInfo.Account1EndExp);
                int acc2Exp = CalculateExpGained(macroInfo.Account2StartRank, macroInfo.Account2StartExp, macroInfo.Account2EndRank, macroInfo.Account2EndExp);
                int acc3Exp = CalculateExpGained(macroInfo.Account3StartRank, macroInfo.Account3StartExp, macroInfo.Account3EndRank, macroInfo.Account3EndExp);
                int acc4Exp = CalculateExpGained(macroInfo.Account4StartRank, macroInfo.Account4StartExp, macroInfo.Account4EndRank, macroInfo.Account4EndExp);

                TimeSpan ellapsed = macroInfo.EndTime - macroInfo.StartTime;
                string ellapsedString = TimeSpanToEllapsed(ellapsed);

                StringBuilder sb = new();
                _ = sb.AppendLine("Macro: " + ellapsedString);
                _ = sb.AppendLine(macroInfo.Account1.Split('|').First() + ": " + acc1Exp.ToString("N0") + " AVG: " + ((double)acc1Exp / macroInfo.LevelsEntered).ToString("N0") + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));
                _ = sb.AppendLine(macroInfo.Account2.Split('|').First() + ": " + acc2Exp.ToString("N0") + " AVG: " + ((double)acc2Exp / macroInfo.LevelsEntered).ToString("N0") + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));
                _ = sb.AppendLine(macroInfo.Account3.Split('|').First() + ": " + acc3Exp.ToString("N0") + " AVG: " + ((double)acc3Exp / macroInfo.LevelsEntered).ToString("N0") + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));
                _ = sb.AppendLine(macroInfo.Account4.Split('|').First() + ": " + acc4Exp.ToString("N0") + " AVG: " + ((double)acc4Exp / macroInfo.LevelsEntered).ToString("N0") + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));

                _ = MessageBox.Show(sb.ToString(), "EXP Gained");
            }
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private async void StopAllButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (macrosToControl.Items.Count == 0)
            {
                _ = MessageBox.Show("There are no macros to stop.");
            }
            else
            {
                List<MacroInfo> macroInfos = await _simmingService.StopAllMacros();

                StringBuilder sb = new();
                int counter = 0;

                foreach (MacroInfo macroInfo in macroInfos)
                {
                    counter++;

                    int acc1Exp = CalculateExpGained(macroInfo.Account1StartRank, macroInfo.Account1StartExp, macroInfo.Account1EndRank, macroInfo.Account1EndExp);
                    int acc2Exp = CalculateExpGained(macroInfo.Account2StartRank, macroInfo.Account2StartExp, macroInfo.Account2EndRank, macroInfo.Account2EndExp);
                    int acc3Exp = CalculateExpGained(macroInfo.Account3StartRank, macroInfo.Account3StartExp, macroInfo.Account3EndRank, macroInfo.Account3EndExp);
                    int acc4Exp = CalculateExpGained(macroInfo.Account4StartRank, macroInfo.Account4StartExp, macroInfo.Account4EndRank, macroInfo.Account4EndExp);

                    TimeSpan ellapsed = macroInfo.EndTime - macroInfo.StartTime;
                    string ellapseString = TimeSpanToEllapsed(ellapsed);

                    _ = sb.AppendLine("Macro " + counter + ": " + ellapseString);
                    _ = sb.AppendLine(macroInfo.Account1.Split('|').First() + ": " + acc1Exp.ToString("N0") + " AVG: " + (macroInfo.LevelsEntered > 0 ? ((double)acc1Exp / macroInfo.LevelsEntered).ToString("N0") : 0) + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));
                    _ = sb.AppendLine(macroInfo.Account2.Split('|').First() + ": " + acc2Exp.ToString("N0") + " AVG: " + (macroInfo.LevelsEntered > 0 ? ((double)acc2Exp / macroInfo.LevelsEntered).ToString("N0") : 0) + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));
                    _ = sb.AppendLine(macroInfo.Account3.Split('|').First() + ": " + acc3Exp.ToString("N0") + " AVG: " + (macroInfo.LevelsEntered > 0 ? ((double)acc3Exp / macroInfo.LevelsEntered).ToString("N0") : 0) + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));
                    _ = sb.AppendLine(macroInfo.Account4.Split('|').First() + ": " + acc4Exp.ToString("N0") + " AVG: " + (macroInfo.LevelsEntered > 0 ? ((double)acc4Exp / macroInfo.LevelsEntered).ToString("N0") : 0) + " Levels: " + macroInfo.LevelsEntered.ToString("N0"));
                    _ = sb.AppendLine();
                }

                macrosToControl.Items.Clear();

                _ = MessageBox.Show(sb.ToString(), "EXP Gained");
            }
        }
        catch (Exception ex)
        {
            _ = MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
        }
    }

    private async Task GetServers()
    {
        HttpClient web = new();
        string? text = null;
        try
        {
            text = await web.GetStringAsync("https://pr2hub.com/files/server_status_2.txt");
        }
        catch (HttpRequestException ex)
        {
            Debug.WriteLine(ex.ToString());
            _ = MessageBox.Show("Connection to PR2 Hub was not successfull.");
        }
        web.Dispose();
        if (text != null)
        {
            servers.Items.Clear();
            serversToSwitch.Items.Clear();
            PR2ServerList? pr2ServerList = JsonConvert.DeserializeObject<PR2ServerList>(text);
            if (pr2ServerList?.Servers != null)
            {
                foreach (PR2Server server in pr2ServerList.Servers)
                {
                    _ = servers.Items.Add(server.ToString());
                    _ = serversToSwitch.Items.Add(server.ToString());
                }
            }
        }
    }

    private static void HandleHotkey(IntPtr lParam)
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

    private static Keys GetKey(IntPtr LParam)
    {
        return (Keys)((LParam.ToInt32()) >> 16);
    }

    private static int CalculateExpGained(int accStartRank, int accStartExp, int accEndRank, int accEndExp)
    {
        int accExp = -1;

        if (accStartRank == accEndRank)
        {
            accExp = accEndExp - accStartExp;
        }
        else
        {
            for (int i = accStartRank; i <= accEndRank; i++)
            {
                if (i == accStartRank)
                {
                    accExp = (int)Math.Round(Math.Pow(1.25, i) * 30, 0, MidpointRounding.AwayFromZero) - accStartExp;
                }
                else if (i == accEndRank)
                {
                    accExp += accEndExp;
                }
                else
                {
                    accExp += (int)Math.Round(Math.Pow(1.25, i) * 30, 0, MidpointRounding.AwayFromZero);
                }
            }
        }

        return accExp;
    }

    public static string TimeSpanToEllapsed(TimeSpan ellapsed)
    {
        string ellapsedString = ellapsed.TotalMinutes < 1.0
            ? string.Format("{0}s", ellapsed.Seconds)
            : ellapsed.TotalHours < 1.0
                ? string.Format("{0}m:{1:D2}s", ellapsed.Minutes, ellapsed.Seconds)
                : string.Format("{0}h:{1:D2}m:{2:D2}s", (int)ellapsed.TotalHours, ellapsed.Minutes, ellapsed.Seconds);
        return ellapsedString;
    }

    private void Sizes_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (sizes.SelectedIndex)
        {
            case 0:
                width.Text = Constants.BigPR2Width.ToString();
                height.Text = Constants.BigPR2Height.ToString();
                break;
            case 1:
                width.Text = Constants.MediumPR2Width.ToString();
                height.Text = Constants.MediumPR2Height.ToString();
                break;
            case 2:
                width.Text = Constants.SmallPR2Width.ToString();
                height.Text = Constants.SmallPR2Height.ToString();
                break;

        }

    }
}
