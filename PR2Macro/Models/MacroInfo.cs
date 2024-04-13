using PR2Macro.Enums;
using PR2Macro.Properties;
using SortOrder = PR2Macro.Enums.SortOrder;

namespace PR2Macro.Models;

public class MacroInfo
{
    public MacroInfo(string account1, string account2, string account3, string account4, SearchBy searchBy, SortBy sortBy, SortOrder sortOrder, Level level, Page page, SimType simType, string search, Server server, int monitor, bool useHappyHourServer, MacroSize macroSize, double pr2Width, double pr2Height)
    {
        Id = Guid.NewGuid().ToString();

        Account1 = Settings.Default.Accounts.Cast<string>().First(x => x != null && x.Split('|').First() == account1);
        Account2 = Settings.Default.Accounts.Cast<string>().First(x => x != null && x.Split('|').First() == account2);
        Account3 = Settings.Default.Accounts.Cast<string>().First(x => x != null && x.Split('|').First() == account3);
        Account4 = Settings.Default.Accounts.Cast<string>().First(x => x != null && x.Split('|').First() == account4);

        SearchBy = searchBy;
        SortBy = sortBy;
        SortOrder = sortOrder;
        Level = level;
        Page = page;
        SimType = simType;
        Search = search;
        Server = server;
        Monitor = monitor;
        UseHappyHourServer = useHappyHourServer;

        MacroSize = macroSize;
        PR2Width = pr2Width;
        PR2Height = pr2Height;
    }

    public string Id { get; set; }
    public string Account1 { get; set; }
    public string Account2 { get; set; }
    public string Account3 { get; set; }
    public string Account4 { get; set; }
    public SearchBy SearchBy { get; set; }
    public SearchBy CurrentSearchBy { get; set; }
    public SortBy SortBy { get; set; }
    public SortBy CurrentSortBy { get; set; }
    public SortOrder SortOrder { get; set; }
    public SortOrder CurrentSortOrder { get; set; }
    public Page Page { get; set; }
    public Page CurrentPage { get; set; }
    public Level Level { get; set; }
    public SimType SimType { get; set; }
    public string Search { get; set; }
    public Server Server { get; set; }
    public Server CurrentServer { get; set; }
    public int Monitor { get; set; }
    public Point TopLeft { get; set; }
    public Point BottomRight { get; set; }
    public bool UseHappyHourServer { get; set; }
    public int HappyHourPriority { get; set; }
    public MacroSize MacroSize { get; set; }
    public double PR2Width { get; set; }
    public double PR2Height { get; set; }
    public bool InLevel { get; set; }
    public bool ReadyToSwitch { get; set; }
    public bool IsPaused { get; set; }
    public bool IsStopped { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; }
    public int Account1StartRank { get; set; }
    public int Account1StartExp { get; set; }
    public int Account1EndRank { get; set; }
    public int Account1EndExp { get; set; }
    public int Account2StartRank { get; set; }
    public int Account2StartExp { get; set; }
    public int Account2EndRank { get; set; }
    public int Account2EndExp { get; set; }
    public int Account3StartRank { get; set; }
    public int Account3StartExp { get; set; }
    public int Account3EndRank { get; set; }
    public int Account3EndExp { get; set; }
    public int Account4StartRank { get; set; }
    public int Account4StartExp { get; set; }
    public int Account4EndRank { get; set; }
    public int Account4EndExp { get; set; }
    public int LevelsEntered { get; set; }
    public int DisconnectedAccounts { get; set; } = 0;
    public List<Point> Level1Points { get; set; } = new();
    public List<Point> Level2Points { get; set; } = new();
    public List<Point> Level3Points { get; set; } = new();
    public List<Point> Level4Points { get; set; } = new();
    public List<Point> Level5Points { get; set; } = new();
    public List<Point> Level6Points { get; set; } = new();
    public IntPtr HWnd { get; set; }

    public override string ToString()
    {
        return Account1.Split('|').First();
    }
}
