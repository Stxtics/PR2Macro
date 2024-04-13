using Newtonsoft.Json;
using PR2Macro.Enums;
using PR2Macro.Interfaces;
using PR2Macro.Models;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace PR2Macro.Services;

public class SimmingService : ISimmingService
{
    private readonly List<MacroInfo> macroInfos = new();
    private bool _movementInProgress;

    private readonly IMacroService _macroService;
    private readonly IResourcesService _resourcesService;

    public SimmingService(IMacroService macroService, IResourcesService resourcesService)
    {
        _macroService = macroService;
        _resourcesService = resourcesService;

        _ = Task.Factory.StartNew(HappyHourCheck);
    }

    public async Task<StartMacroResult> Start(MacroInfo macroInfo)
    {
        if (macroInfos.Any(x => x.Account1.Split('|').First() == macroInfo.Account1.Split('|').First()
        || x.Account1.Split('|').First() == macroInfo.Account2.Split('|').First()
        || x.Account1.Split('|').First() == macroInfo.Account3.Split('|').First()
        || x.Account1.Split('|').First() == macroInfo.Account4.Split('|').First()
        || x.Account2.Split('|').First() == macroInfo.Account1.Split('|').First()
        || x.Account2.Split('|').First() == macroInfo.Account2.Split('|').First()
        || x.Account2.Split('|').First() == macroInfo.Account3.Split('|').First()
        || x.Account2.Split('|').First() == macroInfo.Account4.Split('|').First()
        || x.Account3.Split('|').First() == macroInfo.Account1.Split('|').First()
        || x.Account3.Split('|').First() == macroInfo.Account2.Split('|').First()
        || x.Account3.Split('|').First() == macroInfo.Account3.Split('|').First()
        || x.Account3.Split('|').First() == macroInfo.Account4.Split('|').First()
        || x.Account4.Split('|').First() == macroInfo.Account1.Split('|').First()
        || x.Account4.Split('|').First() == macroInfo.Account2.Split('|').First()
        || x.Account4.Split('|').First() == macroInfo.Account3.Split('|').First()
        || x.Account4.Split('|').First() == macroInfo.Account4.Split('|').First()))
        {
            return StartMacroResult.AccountInUse;
        }
        else if (macroInfos.Any(x => x.CurrentServer == macroInfo.Server))
        {
            return StartMacroResult.ServerInUse;
        }
        else if (macroInfos.Count + 1 > _macroService.GetWinHandles("4pr2").Where(x => x != IntPtr.Zero).Count())
        {
            return StartMacroResult.NotEnoughWindows;
        }

        macroInfo.HWnd = _macroService.GetWinHandles("4pr2").Where(x => x != IntPtr.Zero).First(x => !macroInfos.Select(x => x.HWnd).Contains(x));

        macroInfo.HappyHourPriority = macroInfos.Count(x => x.UseHappyHourServer) + 1;
        macroInfo.StartTime = DateTime.UtcNow;

        _ = Task.Factory.StartNew(() => Sim(macroInfo));

        HttpClient web = new();

        string account1String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account1.Split('|').First());
        while (account1String.Contains("Slow down a bit, yo"))
        {
            account1String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account1.Split('|').First());
        }
        PR2User? account1 = JsonConvert.DeserializeObject<PR2User>(account1String);

        string account2String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account2.Split('|').First());
        while (account2String.Contains("Slow down a bit, yo"))
        {
            account2String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account2.Split('|').First());
        }
        PR2User? account2 = JsonConvert.DeserializeObject<PR2User>(account2String);

        string account3String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account3.Split('|').First());
        while (account3String.Contains("Slow down a bit, yo"))
        {
            account3String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account3.Split('|').First());
        }
        PR2User? account3 = JsonConvert.DeserializeObject<PR2User>(account3String);

        string account4String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account4.Split('|').First());
        while (account4String.Contains("Slow down a bit, yo"))
        {
            account4String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account4.Split('|').First());
        }
        PR2User? account4 = JsonConvert.DeserializeObject<PR2User>(account4String);

        macroInfo.Account1StartRank = account1?.ExpToRank != null ? (int)Math.Round(Math.Log(account1.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account1StartExp = account1?.ExpPoints ?? -1;

        macroInfo.Account2StartRank = account2?.ExpToRank != null ? (int)Math.Round(Math.Log(account2.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account2StartExp = account2?.ExpPoints ?? -1;

        macroInfo.Account3StartRank = account3?.ExpToRank != null ? (int)Math.Round(Math.Log(account3.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account3StartExp = account3?.ExpPoints ?? -1;

        macroInfo.Account4StartRank = account4?.ExpToRank != null ? (int)Math.Round(Math.Log(account4.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account4StartExp = account4?.ExpPoints ?? -1;

        web.Dispose();

        macroInfos.Add(macroInfo);

        return StartMacroResult.Success;
    }

    public async Task<SwitchServerResult> SwitchServerRequest(MacroInfo macroToControl, Server server)
    {
        MacroInfo macroInfo = macroInfos.First(x => x.Id == macroToControl.Id);

        if (macroInfos.Any(x => x.CurrentServer == server))
        {
            return macroInfos.First(x => x.CurrentServer == server).Id == macroInfo.Id
                ? SwitchServerResult.CurrentMacroUsingServer
                : SwitchServerResult.AnotherMacroUsingServer;
        }

        macroInfo.ReadyToSwitch = true;
        while (macroInfo.InLevel)
        {
            await Task.Delay(1000);
        }
        await SwitchServer(macroInfo, server);
        macroInfo.ReadyToSwitch = false;

        return SwitchServerResult.Success;
    }

    public PauseMacroResult PauseMacro(MacroInfo macroToControl)
    {
        MacroInfo macroInfo = macroInfos.First(x => x.Id == macroToControl.Id);

        if (macroInfo.IsPaused)
        {
            macroInfo.IsPaused = false;
            return PauseMacroResult.Resumed;
        }
        else
        {
            macroInfo.IsPaused = true;
            return PauseMacroResult.Paused;
        }
    }

    public bool IsMacroPaused(MacroInfo macroToControl)
    {
        MacroInfo macroInfo = macroInfos.First(x => x.Id == macroToControl.Id);

        return macroInfo.IsPaused;
    }

    public async Task<MacroInfo> StopMacro(MacroInfo macroToControl)
    {
        MacroInfo macroInfo = macroInfos.First(x => x.Id == macroToControl.Id);

        macroInfo.IsStopped = true;
        while (macroInfo.InLevel)
        {
            await Task.Delay(1000);
        }
        macroInfo.EndTime = DateTime.UtcNow;

        HttpClient web = new();

        string account1String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account1.Split('|').First());
        while (account1String.Contains("Slow down a bit, yo"))
        {
            account1String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account1.Split('|').First());
        }
        PR2User? account1 = JsonConvert.DeserializeObject<PR2User>(account1String);

        string account2String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account2.Split('|').First());
        while (account2String.Contains("Slow down a bit, yo"))
        {
            account2String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account2.Split('|').First());
        }
        PR2User? account2 = JsonConvert.DeserializeObject<PR2User>(account2String);

        string account3String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account3.Split('|').First());
        while (account3String.Contains("Slow down a bit, yo"))
        {
            account3String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account3.Split('|').First());
        }
        PR2User? account3 = JsonConvert.DeserializeObject<PR2User>(account3String);

        string account4String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account4.Split('|').First());
        while (account4String.Contains("Slow down a bit, yo"))
        {
            account4String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account4.Split('|').First());
        }
        PR2User? account4 = JsonConvert.DeserializeObject<PR2User>(account4String);

        macroInfo.Account1EndRank = account1?.ExpToRank != null ? (int)Math.Round(Math.Log(account1.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account1EndExp = account1?.ExpPoints ?? -1;

        macroInfo.Account2EndRank = account2?.ExpToRank != null ? (int)Math.Round(Math.Log(account2.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account2EndExp = account2?.ExpPoints ?? -1;

        macroInfo.Account3EndRank = account3?.ExpToRank != null ? (int)Math.Round(Math.Log(account3.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account3EndExp = account3?.ExpPoints ?? -1;

        macroInfo.Account4EndRank = account4?.ExpToRank != null ? (int)Math.Round(Math.Log(account4.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
        macroInfo.Account4EndExp = account4?.ExpPoints ?? -1;

        web.Dispose();

        _ = macroInfos.RemoveAll(x => x.Id == macroInfo.Id);

        return macroInfo;
    }

    public async Task<List<MacroInfo>> StopAllMacros()
    {
        macroInfos.ForEach(x => x.IsStopped = true);
        macroInfos.ForEach(x => x.EndTime = DateTime.UtcNow);

        HttpClient web = new();

        foreach (MacroInfo macroInfo in macroInfos)
        {
            if (macroInfo.InLevel)
            {
                macroInfo.LevelsEntered--;
            }

            string account1String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account1.Split('|').First());
            while (account1String.Contains("Slow down a bit, yo"))
            {
                account1String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account1.Split('|').First());
            }
            PR2User? account1 = JsonConvert.DeserializeObject<PR2User>(account1String);

            string account2String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account2.Split('|').First());
            while (account2String.Contains("Slow down a bit, yo"))
            {
                account2String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account2.Split('|').First());
            }
            PR2User? account2 = JsonConvert.DeserializeObject<PR2User>(account2String);

            string account3String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account3.Split('|').First());
            while (account3String.Contains("Slow down a bit, yo"))
            {
                account3String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account3.Split('|').First());
            }
            PR2User? account3 = JsonConvert.DeserializeObject<PR2User>(account3String);

            string account4String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account4.Split('|').First());
            while (account4String.Contains("Slow down a bit, yo"))
            {
                account4String = await web.GetStringAsync("https://pr2hub.com/get_player_info.php?name=" + macroInfo.Account4.Split('|').First());
            }
            PR2User? account4 = JsonConvert.DeserializeObject<PR2User>(account4String);

            macroInfo.Account1EndRank = account1?.ExpToRank != null ? (int)Math.Round(Math.Log(account1.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
            macroInfo.Account1EndExp = account1?.ExpPoints ?? -1;

            macroInfo.Account2EndRank = account2?.ExpToRank != null ? (int)Math.Round(Math.Log(account2.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
            macroInfo.Account2EndExp = account2?.ExpPoints ?? -1;

            macroInfo.Account3EndRank = account3?.ExpToRank != null ? (int)Math.Round(Math.Log(account3.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
            macroInfo.Account3EndExp = account3?.ExpPoints ?? -1;

            macroInfo.Account4EndRank = account4?.ExpToRank != null ? (int)Math.Round(Math.Log(account4.ExpToRank / 30) / Math.Log(1.25), 0) : -1;
            macroInfo.Account4EndExp = account4?.ExpPoints ?? -1;
        }

        web.Dispose();

        List<MacroInfo> clone = new(macroInfos);
        macroInfos.Clear();

        return clone;
    }

    #region Private Methods

    private async Task Sim(MacroInfo macroInfo)
    {
        try
        {
            await WaitForResumedAndCompletedMovement(macroInfo, false);
            await Login(macroInfo);
            await WaitForResumedAndCompletedMovement(macroInfo);
            await SearchBy(macroInfo);
            await WaitForResumedAndCompletedMovement(macroInfo);
            await SortBy(macroInfo);
            await WaitForResumedAndCompletedMovement(macroInfo);
            await EnterSearch(macroInfo);
            await WaitForResumedAndCompletedMovement(macroInfo);
            await SelectPage(macroInfo);
            await WaitForResumedAndCompletedMovement(macroInfo);
            bool result = await DCCheck(macroInfo);
            await WaitForResumedAndCompletedMovement(macroInfo);
            await EnterLevel(macroInfo);
            macroInfo.InLevel = true;
            if (macroInfo.SimType == SimType.OnePlayer)
            {
                while (true)
                {
                    _movementInProgress = false;
                    await Task.Delay(6000);
                    await WaitForResumedAndCompletedMovement(macroInfo, false);
                    result = await DCCheck(macroInfo);
                    if (!result)
                    {
                        await WaitForResumedAndCompletedMovement(macroInfo);
                        await Quit(macroInfo);
                        await WaitForResumedAndCompletedMovement(macroInfo);
                        await ReturnToLobby(macroInfo, 2);
                        _movementInProgress = false;
                        await Task.Delay(116000);
                        await WaitForResumedAndCompletedMovement(macroInfo, false);
                        result = await DCCheck(macroInfo);
                        if (!result)
                        {
                            await WaitForResumedAndCompletedMovement(macroInfo);
                            await ReturnToLobby(macroInfo, 1);
                        }
                    }
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    macroInfo.InLevel = false;
                    if (macroInfo.IsStopped)
                    {
                        _movementInProgress = false;
                        break;
                    }
                    if (macroInfo.ReadyToSwitch)
                    {
                        _movementInProgress = false;

                        while (macroInfo.ReadyToSwitch)
                        {
                            await Task.Delay(1000);
                        }
                    }
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    _ = await DCCheck(macroInfo);
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    await EnterLevel(macroInfo);
                    macroInfo.InLevel = true;
                }
            }
            else if (macroInfo.SimType == SimType.FourPlayers)
            {
                while (true)
                {
                    _movementInProgress = false;
                    await Task.Delay(121000);
                    await WaitForResumedAndCompletedMovement(macroInfo, false);
                    result = await DCCheck(macroInfo);
                    if (!result)
                    {
                        await WaitForResumedAndCompletedMovement(macroInfo);
                        await ReturnToLobby(macroInfo);
                    }
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    macroInfo.InLevel = false;
                    if (macroInfo.IsStopped)
                    {
                        _movementInProgress = false;
                        break;
                    }
                    if (macroInfo.ReadyToSwitch)
                    {
                        _movementInProgress = false;

                        while (macroInfo.ReadyToSwitch)
                        {
                            await Task.Delay(1000);
                        }
                    }
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    _ = await DCCheck(macroInfo);
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    await EnterLevel(macroInfo);
                    macroInfo.InLevel = true;
                }
            }
            else if (macroInfo.SimType == SimType.Objective)
            {
                while (true)
                {
                    _movementInProgress = false;
                    await Task.Delay(121000);
                    await WaitForResumedAndCompletedMovement(macroInfo, false);
                    result = await DCCheck(macroInfo);
                    if (!result)
                    {
                        await WaitForResumedAndCompletedMovement(macroInfo);
                        await Quit(macroInfo);
                        await WaitForResumedAndCompletedMovement(macroInfo);
                        await ReturnToLobby(macroInfo);
                    }
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    macroInfo.InLevel = false;
                    if (macroInfo.IsStopped)
                    {
                        _movementInProgress = false;
                        break;
                    }
                    if (macroInfo.ReadyToSwitch)
                    {
                        _movementInProgress = false;

                        while (macroInfo.ReadyToSwitch)
                        {
                            await Task.Delay(1000);
                        }
                    }
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    _ = await DCCheck(macroInfo);
                    await WaitForResumedAndCompletedMovement(macroInfo);
                    await EnterLevel(macroInfo);
                    macroInfo.InLevel = true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task WaitForResumedAndCompletedMovement(MacroInfo macroInfo, bool wasMovementInProgress = true)
    {
        if (wasMovementInProgress && macroInfo.IsPaused)
        {
            _movementInProgress = false;
        }

        bool shouldWaitForCompletedMovement = macroInfo.IsPaused || !wasMovementInProgress;

        while (macroInfo.IsPaused)
        {
            await Task.Delay(1000);
        }

        if (shouldWaitForCompletedMovement)
        {
            while (_movementInProgress)
            {
                await Task.Delay(1000);
            }
        }

        _movementInProgress = true;
    }

    private async Task Login(MacroInfo macroInfo)
    {
        await Task.Delay(1000);
        //_macroService.Focus(macroInfo.HWnd);
        Focus();
        await Task.Delay(5000);
        //Bitmap screen = _macroService.CaptureWindow(macroInfo.HWnd);
        Bitmap screen = CaptureScreen(macroInfo);
        Bitmap download = _resourcesService.GetResource(Resource.Download, macroInfo);
        List<Point> points = _macroService.Find(screen, download, macroInfo);
        while (points.Count != 4 || PointsOverlapMacro(points, macroInfo))
        {
            screen = _macroService.CaptureWindow(macroInfo.HWnd);
            points = _macroService.Find(screen, download, macroInfo);
            await Task.Delay(250);
        }

        if (macroInfo.MacroSize == MacroSize.Big)
        {
            macroInfo.TopLeft = new Point(points[0].X - int.Parse(Math.Round(69 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[0].Y - int.Parse(Math.Round(335 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
            macroInfo.BottomRight = new Point(points[3].X + int.Parse(Math.Round(482 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[3].Y + int.Parse(Math.Ceiling(67 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        }
        else if (macroInfo.MacroSize == MacroSize.Medium)
        {
            macroInfo.TopLeft = new Point(points[0].X - int.Parse(Math.Round(43 / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), points[0].Y - int.Parse(Math.Round(212 / Constants.MediumPR2Height * macroInfo.PR2Height).ToString()));
            macroInfo.BottomRight = new Point(points[3].X + int.Parse(Math.Round(310 / Constants.MediumPR2Width * macroInfo.PR2Width).ToString()), points[3].Y + int.Parse(Math.Ceiling(45 / Constants.MediumPR2Height * macroInfo.PR2Height).ToString()));
        }
        else if (macroInfo.MacroSize == MacroSize.Small)
        {
            macroInfo.TopLeft = new Point(points[0].X - int.Parse(Math.Round(69 / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), points[0].Y - int.Parse(Math.Round(335 / Constants.SmallPR2Height * macroInfo.PR2Height).ToString()));
            macroInfo.BottomRight = new Point(points[3].X + int.Parse(Math.Round(482 / Constants.SmallPR2Width * macroInfo.PR2Width).ToString()), points[3].Y + int.Parse(Math.Ceiling(67 / Constants.SmallPR2Height * macroInfo.PR2Height).ToString()));
        }

        foreach (Point point in points)
        {
            _macroService.Click(macroInfo.HWnd, point);
            //_macroService.MouseMove(point.X, point.Y);
            //_macroService.LeftClick();
        }
        download.Dispose();
        screen = CaptureRegion(macroInfo);
        Bitmap mute = _resourcesService.GetResource(Resource.Mute, macroInfo);
        points = _macroService.Find(screen, mute, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, mute, macroInfo);
            await Task.Delay(250);
        }
        foreach (Point point in points)
        {
            _macroService.MouseMove(point.X, point.Y);
            _macroService.LeftClick();
        }
        mute.Dispose();
        Focus();
        await Task.Delay(1000);
        screen = CaptureRegion(macroInfo);
        Bitmap login = _resourcesService.GetResource(Resource.Login, macroInfo);
        points = _macroService.Find(screen, login, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, login, macroInfo);
            await Task.Delay(50);
        }
        foreach (Point point in points)
        {
            _macroService.MouseMove(point.X, point.Y);
            _macroService.LeftClick();
        }
        login.Dispose();
        Focus();
        await SelectServer(macroInfo, macroInfo.Server, points, false);
        Focus();
        screen = CaptureRegion(macroInfo);
        Bitmap username = _resourcesService.GetResource(Resource.Name, macroInfo);
        Bitmap password = _resourcesService.GetResource(Resource.Password, macroInfo);
        List<Point> usernamePoints = _macroService.Find(screen, username, macroInfo);
        List<Point> passwordPoints = _macroService.Find(screen, password, macroInfo);
        while (usernamePoints.Count != 4 || passwordPoints.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            if (usernamePoints.Count != 4)
            {
                usernamePoints = _macroService.Find(screen, username, macroInfo);
            }
            else if (passwordPoints.Count != 4)
            {
                passwordPoints = _macroService.Find(screen, password, macroInfo);
            }
            await Task.Delay(250);
        }
        username.Dispose();
        password.Dispose();
        _macroService.MouseMove(usernamePoints[0].X, usernamePoints[0].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account1.Split('|').First());
        _macroService.MouseMove(passwordPoints[0].X, passwordPoints[0].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account1.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(usernamePoints[0].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), usernamePoints[0].Y + int.Parse(Math.Round(160 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(usernamePoints[1].X, usernamePoints[1].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account2.Split('|').First());
        _macroService.MouseMove(passwordPoints[1].X, passwordPoints[1].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account2.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(usernamePoints[1].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), usernamePoints[1].Y + int.Parse(Math.Round(160 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(usernamePoints[2].X, usernamePoints[2].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account3.Split('|').First());
        _macroService.MouseMove(passwordPoints[2].X, passwordPoints[2].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account3.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(usernamePoints[2].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), usernamePoints[2].Y + int.Parse(Math.Round(160 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(usernamePoints[3].X, usernamePoints[3].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account4.Split('|').First());
        _macroService.MouseMove(passwordPoints[3].X, passwordPoints[3].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account4.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(usernamePoints[3].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), usernamePoints[3].Y + int.Parse(Math.Round(160 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Search(macroInfo);
    }

    private async Task SelectServer(MacroInfo macroInfo, Server server, List<Point> points, bool reconnect)
    {
        if (server == macroInfo.CurrentServer && !reconnect)
        {
            return;
        }

        if (server == Server.Derron)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(45 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (server == Server.Carina)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(65 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (server == Server.Grayan)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(85 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (server == Server.Fitz)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(105 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (server == Server.Tournament)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(125 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        macroInfo.CurrentServer = macroInfo.Server;
    }

    private async Task Search(MacroInfo macroInfo)
    {
        await Task.Delay(500);
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap searchTab = _resourcesService.GetResource(Resource.SearchTab, macroInfo);
        List<Point> points = _macroService.Find(screen, searchTab, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, searchTab, macroInfo);
            await Task.Delay(50);
        }
        searchTab.Dispose();
        foreach (Point point in points)
        {
            _macroService.MouseMove(point.X, point.Y);
            _macroService.LeftClick();
        }
    }

    private async Task SearchBy(MacroInfo macroInfo)
    {
        Focus();
        if (macroInfo.SearchBy == macroInfo.CurrentSearchBy)
        {
            return;
        }
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap search = _resourcesService.GetResource(Resource.SearchBy, macroInfo);
        List<Point> points = _macroService.Find(screen, search, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, search, macroInfo);
            await Task.Delay(50);
        }
        search.Dispose();
        if (macroInfo.SearchBy == Enums.SearchBy.UserName)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.SearchBy == Enums.SearchBy.LevelTitle)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.SearchBy == Enums.SearchBy.LevelId)
        {
            foreach (Point point in points)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(60 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        macroInfo.CurrentSearchBy = macroInfo.SearchBy;
    }

    private async Task SortBy(MacroInfo macroInfo)
    {
        Focus();
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap sort = _resourcesService.GetResource(Resource.SortBy, macroInfo);
        Bitmap order = _resourcesService.GetResource(Resource.SortOrder, macroInfo);
        List<Point> sortLocations = _macroService.Find(screen, sort, macroInfo);
        List<Point> orderLocations = _macroService.Find(screen, order, macroInfo);
        while (sortLocations.Count != 4 || orderLocations.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            if (sortLocations.Count != 4)
            {
                sortLocations = _macroService.Find(screen, sort, macroInfo);
            }
            if (orderLocations.Count != 4)
            {
                orderLocations = _macroService.Find(screen, order, macroInfo);
            }
            await Task.Delay(50);
        }
        sort.Dispose();
        order.Dispose();
        if (macroInfo.SortBy == Enums.SortBy.Date && macroInfo.CurrentSortBy != Enums.SortBy.Date)
        {
            foreach (Point point in sortLocations)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.SortBy == Enums.SortBy.Alphabetical && macroInfo.CurrentSortBy != Enums.SortBy.Alphabetical)
        {
            foreach (Point point in sortLocations)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.SortBy == Enums.SortBy.Rating && macroInfo.CurrentSortBy != Enums.SortBy.Rating)
        {
            foreach (Point point in sortLocations)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(60 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.SortBy == Enums.SortBy.Popularity && macroInfo.CurrentSortBy != Enums.SortBy.Popularity)
        {
            foreach (Point point in sortLocations)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(80 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        if (macroInfo.SortOrder == Enums.SortOrder.Descending && macroInfo.CurrentSortOrder != Enums.SortOrder.Descending)
        {
            foreach (Point point in orderLocations)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.SortOrder == Enums.SortOrder.Ascending && macroInfo.CurrentSortOrder != Enums.SortOrder.Ascending)
        {
            foreach (Point point in orderLocations)
            {
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
            }
        }
        macroInfo.CurrentSortBy = macroInfo.SortBy;
        macroInfo.CurrentSortOrder = macroInfo.SortOrder;
    }

    private async Task EnterSearch(MacroInfo macroInfo)
    {
        Focus();
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap searchBox = _resourcesService.GetResource(Resource.SearchBox, macroInfo);
        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
        List<Point> boxLocations = _macroService.Find(screen, searchBox, macroInfo);
        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
        while (boxLocations.Count != 4 || buttonLocations.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            if (boxLocations.Count != 4)
            {
                boxLocations = _macroService.Find(screen, searchBox, macroInfo);
            }
            if (buttonLocations.Count != 4)
            {
                buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
            }
            await Task.Delay(50);
        }
        searchBox.Dispose();
        searchButton.Dispose();
        for (int i = 0; i < 4; i++)
        {
            _macroService.MouseMove(boxLocations[i].X, boxLocations[i].Y);
            _macroService.LeftClick();
            await FixedSendKeys(macroInfo.Search);
            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
            _macroService.LeftClick();
        }
    }

    private async Task SelectPage(MacroInfo macroInfo)
    {
        Focus();
        Bitmap screen = CaptureRegion(macroInfo);
        if (macroInfo.Page == Page.One && macroInfo.CurrentPage != Page.One)
        {
            Bitmap page1 = _resourcesService.GetResource(Resource.Page1, macroInfo);
            List<Point> points = _macroService.Find(screen, page1, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page1, macroInfo);
                await Task.Delay(50);
            }
            page1.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Two && macroInfo.CurrentPage != Page.Two)
        {
            Bitmap page2 = _resourcesService.GetResource(Resource.Page2, macroInfo);
            List<Point> points = _macroService.Find(screen, page2, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page2, macroInfo);
                await Task.Delay(50);
            }
            page2.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Three && macroInfo.CurrentPage != Page.Three)
        {
            Bitmap page3 = _resourcesService.GetResource(Resource.Page3, macroInfo);
            List<Point> points = _macroService.Find(screen, page3, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page3, macroInfo);
                await Task.Delay(50);
            }
            page3.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Four && macroInfo.CurrentPage != Page.Four)
        {
            Bitmap page4 = _resourcesService.GetResource(Resource.Page4, macroInfo);
            List<Point> points = _macroService.Find(screen, page4, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page4, macroInfo);
                await Task.Delay(50);
            }
            page4.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Five && macroInfo.CurrentPage != Page.Five)
        {
            Bitmap page5 = _resourcesService.GetResource(Resource.Page5, macroInfo);
            List<Point> points = _macroService.Find(screen, page5, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page5, macroInfo);
                await Task.Delay(50);
            }
            page5.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Six && macroInfo.CurrentPage != Page.Six)
        {
            Bitmap page6 = _resourcesService.GetResource(Resource.Page6, macroInfo);
            List<Point> points = _macroService.Find(screen, page6, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page6, macroInfo);
                await Task.Delay(50);
            }
            page6.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Seven && macroInfo.CurrentPage != Page.Seven)
        {
            Bitmap page7 = _resourcesService.GetResource(Resource.Page7, macroInfo);
            List<Point> points = _macroService.Find(screen, page7, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page7, macroInfo);
                await Task.Delay(50);
            }
            page7.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Eight && macroInfo.CurrentPage != Page.Eight)
        {
            Bitmap page8 = _resourcesService.GetResource(Resource.Page8, macroInfo);
            List<Point> points = _macroService.Find(screen, page8, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page8, macroInfo);
                await Task.Delay(50);
            }
            page8.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        else if (macroInfo.Page == Page.Nine && macroInfo.CurrentPage != Page.Nine)
        {
            Bitmap page9 = _resourcesService.GetResource(Resource.Page9, macroInfo);
            List<Point> points = _macroService.Find(screen, page9, macroInfo);
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, page9, macroInfo);
                await Task.Delay(50);
            }
            page9.Dispose();
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X, point.Y);
                _macroService.LeftClick();
            }
        }
        macroInfo.CurrentPage = macroInfo.Page;
    }

    private async Task EnterLevel(MacroInfo macroInfo)
    {
        Focus();
        _ = CaptureRegion(macroInfo);
        await Task.Delay(500);
        Bitmap screen;
        int timeTaken = 0;
        if (macroInfo.SimType == SimType.Objective)
        {
            if (macroInfo.Level == Level.One)
            {
                Bitmap level1 = _resourcesService.GetResource(Resource.Level1Objective, macroInfo);
                while (macroInfo.Level1Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level1Points = _macroService.Find(screen, level1, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level1Points[0].X, macroInfo.Level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level1Points[1].X, macroInfo.Level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level1Points[2].X, macroInfo.Level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level1Points[3].X, macroInfo.Level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level1ObjectiveSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level1Points[0].X, macroInfo.Level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level1Points[1].X, macroInfo.Level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level1Points[2].X, macroInfo.Level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level1Points[3].X, macroInfo.Level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level1Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level1Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level1Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level1Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level1.Dispose();
            }
            else if (macroInfo.Level == Level.Two)
            {
                Bitmap level2 = _resourcesService.GetResource(Resource.Level2Objective, macroInfo);
                while (macroInfo.Level2Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level2Points = _macroService.Find(screen, level2, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level2Points[0].X, macroInfo.Level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level2Points[1].X, macroInfo.Level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level2Points[2].X, macroInfo.Level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level2Points[3].X, macroInfo.Level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level2ObjectiveSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level2Points[0].X, macroInfo.Level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level2Points[1].X, macroInfo.Level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level2Points[2].X, macroInfo.Level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level2Points[3].X, macroInfo.Level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level2Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level2Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level2Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level2Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level2.Dispose();
            }
            else if (macroInfo.Level == Level.Three)
            {
                Bitmap level3 = _resourcesService.GetResource(Resource.Level3Objective, macroInfo);
                while (macroInfo.Level3Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level3Points = _macroService.Find(screen, level3, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level3Points[0].X, macroInfo.Level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level3Points[1].X, macroInfo.Level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level3Points[2].X, macroInfo.Level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level3Points[3].X, macroInfo.Level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level3ObjectiveSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level3Points[0].X, macroInfo.Level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level3Points[1].X, macroInfo.Level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level3Points[2].X, macroInfo.Level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level3Points[3].X, macroInfo.Level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level3Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level3Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level3Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level3Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level3.Dispose();
            }
            else if (macroInfo.Level == Level.Four)
            {
                Bitmap level4 = _resourcesService.GetResource(Resource.Level4Objective, macroInfo);
                while (macroInfo.Level4Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level4Points = _macroService.Find(screen, level4, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level4Points[0].X, macroInfo.Level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level4Points[1].X, macroInfo.Level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level4Points[2].X, macroInfo.Level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level4Points[3].X, macroInfo.Level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level4ObjectiveSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level4Points[0].X, macroInfo.Level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level4Points[1].X, macroInfo.Level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level4Points[2].X, macroInfo.Level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level4Points[3].X, macroInfo.Level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level4Points[0].X - int.Parse(Math.Round(int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()) / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level4Points[1].X - int.Parse(Math.Round(int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()) / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level4Points[2].X - int.Parse(Math.Round(int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()) / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level4Points[3].X - int.Parse(Math.Round(int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()) / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level4.Dispose();
            }
            else if (macroInfo.Level == Level.Five)
            {
                Bitmap level5 = _resourcesService.GetResource(Resource.Level5Objective, macroInfo);
                while (macroInfo.Level5Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level5Points = _macroService.Find(screen, level5, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level5Points[0].X, macroInfo.Level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level5Points[1].X, macroInfo.Level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level5Points[2].X, macroInfo.Level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level5Points[3].X, macroInfo.Level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level5ObjectiveSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level5Points[0].X, macroInfo.Level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level5Points[1].X, macroInfo.Level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level5Points[2].X, macroInfo.Level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level5Points[3].X, macroInfo.Level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level5Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level5Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level5Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level5Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level5.Dispose();
            }
            else if (macroInfo.Level == Level.Six)
            {
                Bitmap level6 = _resourcesService.GetResource(Resource.Level6Objective, macroInfo);
                while (macroInfo.Level6Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level6Points = _macroService.Find(screen, level6, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level6Points[0].X, macroInfo.Level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level6Points[1].X, macroInfo.Level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level6Points[2].X, macroInfo.Level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level6Points[3].X, macroInfo.Level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level6ObjectiveSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level6Points[0].X, macroInfo.Level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level6Points[1].X, macroInfo.Level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level6Points[2].X, macroInfo.Level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level6Points[3].X, macroInfo.Level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level6Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level6Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level6Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level6Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level6.Dispose();
            }
        }
        else if (macroInfo.SimType is SimType.OnePlayer or SimType.FourPlayers)
        {
            if (macroInfo.Level == Level.One)
            {
                Bitmap level1 = _resourcesService.GetResource(Resource.Level1Race, macroInfo);
                while (macroInfo.Level1Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level1Points = _macroService.Find(screen, level1, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level1Points[0].X, macroInfo.Level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level1Points[1].X, macroInfo.Level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level1Points[2].X, macroInfo.Level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level1Points[3].X, macroInfo.Level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level1RaceSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level1Points[0].X, macroInfo.Level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level1Points[1].X, macroInfo.Level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level1Points[2].X, macroInfo.Level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level1Points[3].X, macroInfo.Level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level1Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level1Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level1Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level1Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level1.Dispose();
            }
            else if (macroInfo.Level == Level.Two)
            {
                Bitmap level2 = _resourcesService.GetResource(Resource.Level2Race, macroInfo);
                while (macroInfo.Level2Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level2Points = _macroService.Find(screen, level2, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level2Points[0].X, macroInfo.Level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level2Points[1].X, macroInfo.Level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level2Points[2].X, macroInfo.Level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level2Points[3].X, macroInfo.Level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level2RaceSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level2Points[0].X, macroInfo.Level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level2Points[1].X, macroInfo.Level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level2Points[2].X, macroInfo.Level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level2Points[3].X, macroInfo.Level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level2Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level2Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level2Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level2Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level2.Dispose();
            }
            else if (macroInfo.Level == Level.Three)
            {
                Bitmap level3 = _resourcesService.GetResource(Resource.Level3Race, macroInfo);
                while (macroInfo.Level3Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level3Points = _macroService.Find(screen, level3, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level3Points[0].X, macroInfo.Level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level3Points[1].X, macroInfo.Level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level3Points[2].X, macroInfo.Level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level3Points[3].X, macroInfo.Level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level3RaceSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level3Points[0].X, macroInfo.Level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level3Points[1].X, macroInfo.Level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level3Points[2].X, macroInfo.Level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level3Points[3].X, macroInfo.Level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level3Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level3Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level3Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level3Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level3.Dispose();
            }
            else if (macroInfo.Level == Level.Four)
            {
                Bitmap level4 = _resourcesService.GetResource(Resource.Level4Race, macroInfo);
                while (macroInfo.Level4Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level4Points = _macroService.Find(screen, level4, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level4Points[0].X, macroInfo.Level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level4Points[1].X, macroInfo.Level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level4Points[2].X, macroInfo.Level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level4Points[3].X, macroInfo.Level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level4RaceSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level4Points[0].X, macroInfo.Level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level4Points[1].X, macroInfo.Level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level4Points[2].X, macroInfo.Level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level4Points[3].X, macroInfo.Level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level4Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level4Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level4Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level4Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level4.Dispose();
            }
            else if (macroInfo.Level == Level.Five)
            {
                Bitmap level5 = _resourcesService.GetResource(Resource.Level5Race, macroInfo);
                while (macroInfo.Level5Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level5Points = _macroService.Find(screen, level5, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level5Points[0].X, macroInfo.Level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level5Points[1].X, macroInfo.Level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level5Points[2].X, macroInfo.Level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level5Points[3].X, macroInfo.Level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level5RaceSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level5Points[0].X, macroInfo.Level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level5Points[1].X, macroInfo.Level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level5Points[2].X, macroInfo.Level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level5Points[3].X, macroInfo.Level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level5Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level5Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level5Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level5Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level5.Dispose();
            }
            else if (macroInfo.Level == Level.Six)
            {
                Bitmap level6 = _resourcesService.GetResource(Resource.Level6Race, macroInfo);
                while (macroInfo.Level6Points.Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    macroInfo.Level6Points = _macroService.Find(screen, level6, macroInfo);
                    await Task.Delay(50);
                }
                _macroService.MouseMove(macroInfo.Level6Points[0].X, macroInfo.Level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level6Points[1].X, macroInfo.Level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level6Points[2].X, macroInfo.Level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(250);
                _macroService.MouseMove(macroInfo.Level6Points[3].X, macroInfo.Level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)));
                _macroService.LeftClick();
                await Task.Delay(100);
                Bitmap selected = _resourcesService.GetResource(Resource.Level6RaceSelected, macroInfo);
                screen = CaptureRegion(macroInfo);
                while (_macroService.Find(screen, selected, macroInfo).Count != 4)
                {
                    screen = CaptureRegion(macroInfo);
                    await Task.Delay(50);
                    timeTaken += 50;
                    if (timeTaken >= 4000) // more than 5ish seconds
                    {
                        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
                        List<Point> buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                        while (buttonLocations.Count != 4)
                        {
                            screen = CaptureRegion(macroInfo);
                            buttonLocations = _macroService.Find(screen, searchButton, macroInfo);
                            await Task.Delay(50);
                        }
                        searchButton.Dispose();
                        for (int i = 0; i < 4; i++)
                        {
                            _macroService.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                            _macroService.LeftClick();
                        }
                        _macroService.MouseMove(macroInfo.Level6Points[0].X, macroInfo.Level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level6Points[1].X, macroInfo.Level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level6Points[2].X, macroInfo.Level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(250);
                        _macroService.MouseMove(macroInfo.Level6Points[3].X, macroInfo.Level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)));
                        _macroService.LeftClick();
                        await Task.Delay(100);
                        screen = CaptureRegion(macroInfo);
                        timeTaken = 0;
                    }
                }
                selected.Dispose();
                _macroService.MouseMove(macroInfo.Level6Points[0].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level6Points[1].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level6Points[2].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                _macroService.MouseMove(macroInfo.Level6Points[3].X - int.Parse(Math.Round(90 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), macroInfo.Level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)) + int.Parse(Math.Round(10 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                level6.Dispose();
            }
        }

        macroInfo.LevelsEntered++;
    }

    private async Task Quit(MacroInfo macroInfo)
    {
        Focus();
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap chat = _resourcesService.GetResource(Resource.Chat, macroInfo);
        List<Point> points = _macroService.Find(screen, chat, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, chat, macroInfo);
            await Task.Delay(50);
        }
        chat.Dispose();
        if (macroInfo.SimType == SimType.Objective)
        {
            _macroService.MouseMove(points[0].X + int.Parse(Math.Round(360 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[0].Y);
            _macroService.LeftClick();
            await Task.Delay(100);
            _macroService.MouseMove(points[1].X, points[1].Y - 50);
            _macroService.LeftClick();
            await FixedSendKeys("                               ");
            await Task.Delay(3500);
            Focus();
            _macroService.MouseMove(points[1].X + int.Parse(Math.Round(360 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[1].Y);
            _macroService.LeftClick();
            await Task.Delay(100);
            _macroService.MouseMove(points[2].X, points[2].Y - 50);
            _macroService.LeftClick();
            await FixedSendKeys("                               ");
            await Task.Delay(3500);
            Focus();
            _macroService.MouseMove(points[2].X + int.Parse(Math.Round(360 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[2].Y);
            _macroService.LeftClick();
            await Task.Delay(100);
            _macroService.MouseMove(points[3].X, points[3].Y - 50);
            _macroService.LeftClick();
            await FixedSendKeys("                               ");
            await Task.Delay(3000);
            Focus();
            _macroService.MouseMove(points[3].X + int.Parse(Math.Round(360 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[3].Y);
            _macroService.LeftClick();
        }
        else if (macroInfo.SimType == SimType.OnePlayer)
        {
            for (int i = 1; i < 4; i++)
            {
                _macroService.MouseMove(points[i].X + int.Parse(Math.Round(360 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[i].Y);
                _macroService.LeftClick();
            }
        }
    }

    private async Task ReturnToLobby(MacroInfo macroInfo, int? option = null)
    {
        Focus();
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap chatGrey = _resourcesService.GetResource(Resource.ChatGrey, macroInfo);
        List<Point> points = _macroService.Find(screen, chatGrey, macroInfo);
        if (option == null)
        {
            while (points.Count != 4)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, chatGrey, macroInfo);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X + int.Parse(Math.Round(235 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), point.Y - int.Parse(Math.Round(55 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (option == 1)
        {
            while (points.Count != 1)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, chatGrey, macroInfo);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X + int.Parse(Math.Round(235 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), point.Y - int.Parse(Math.Round(55 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()));
                _macroService.LeftClick();
            }
        }
        else if (option == 2)
        {
            while (points.Count != 3)
            {
                screen = CaptureRegion(macroInfo);
                points = _macroService.Find(screen, chatGrey, macroInfo);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                _macroService.MouseMove(point.X + int.Parse(Math.Round(235 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), point.Y - int.Parse(Math.Round(55 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()));
                _macroService.LeftClick();
            }
        }
        chatGrey.Dispose();
    }

    private async Task SwitchServer(MacroInfo macroInfo, Server server)
    {
        if (server == macroInfo.CurrentServer)
        {
            return;
        }
        Focus();
        await Task.Delay(1000);
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
        List<Point> points = _macroService.Find(screen, searchButton, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, searchButton, macroInfo);
            await Task.Delay(50);
        }
        foreach (Point point in points)
        {
            await Task.Delay(3000);
            _macroService.MouseMove(point.X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), point.Y + int.Parse(Math.Round(280 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
            _macroService.LeftClick();
        }
        searchButton.Dispose();
        Bitmap login = _resourcesService.GetResource(Resource.Login, macroInfo);
        points = _macroService.Find(screen, login, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, login, macroInfo);
            await Task.Delay(50);
        }
        foreach (Point point in points)
        {
            _macroService.MouseMove(point.X, point.Y);
            _macroService.LeftClick();
        }
        login.Dispose();
        Focus();
        await SelectServer(macroInfo, server, points, true);
        Focus();
        screen = CaptureRegion(macroInfo);
        Bitmap password = _resourcesService.GetResource(Resource.Password, macroInfo);
        List<Point> passwordPoints = _macroService.Find(screen, password, macroInfo);
        while (passwordPoints.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            passwordPoints = _macroService.Find(screen, password, macroInfo);
            await Task.Delay(50);
        }
        password.Dispose();
        _macroService.MouseMove(passwordPoints[0].X, passwordPoints[0].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account1.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[0].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[0].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(passwordPoints[1].X, passwordPoints[1].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account2.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[1].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[1].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(passwordPoints[2].X, passwordPoints[2].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account3.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[2].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[2].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(passwordPoints[3].X, passwordPoints[3].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account4.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[3].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[3].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        macroInfo.CurrentServer = server;
        await Task.Delay(2000);
    }

    private async Task ReLogin(MacroInfo macroInfo, Server server)
    {
        if (server == macroInfo.CurrentServer)
        {
            return;
        }
        Focus();
        Bitmap login = _resourcesService.GetResource(Resource.Login, macroInfo);
        Bitmap screen = CaptureRegion(macroInfo);
        List<Point> points = _macroService.Find(screen, login, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, login, macroInfo);
            await Task.Delay(50);
        }
        foreach (Point point in points)
        {
            _macroService.MouseMove(point.X, point.Y);
            _macroService.LeftClick();
        }
        login.Dispose();
        Focus();
        await SelectServer(macroInfo, server, points, true);
        Focus();
        screen = CaptureRegion(macroInfo);
        Bitmap password = _resourcesService.GetResource(Resource.Password, macroInfo);
        List<Point> passwordPoints = _macroService.Find(screen, password, macroInfo);
        while (passwordPoints.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            passwordPoints = _macroService.Find(screen, password, macroInfo);
            await Task.Delay(50);
        }
        password.Dispose();
        _macroService.MouseMove(passwordPoints[0].X, passwordPoints[0].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account1.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[0].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[0].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(passwordPoints[1].X, passwordPoints[1].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account2.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[1].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[1].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(passwordPoints[2].X, passwordPoints[2].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account3.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[2].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[2].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        await Task.Delay(2200);
        Focus();
        _macroService.MouseMove(passwordPoints[3].X, passwordPoints[3].Y);
        _macroService.LeftClick();
        await FixedSendKeys(macroInfo.Account4.Split(new char[] { '|' }, 2).Last());
        _macroService.MouseMove(passwordPoints[3].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoints[3].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
        _macroService.LeftClick();
        macroInfo.CurrentServer = server;
        await Task.Delay(2000);
    }

    private async Task Logout(MacroInfo macroInfo)
    {
        Focus();
        await Task.Delay(1000);
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap searchButton = _resourcesService.GetResource(Resource.SearchButton, macroInfo);
        List<Point> points = _macroService.Find(screen, searchButton, macroInfo);
        while (points.Count != 4)
        {
            screen = CaptureRegion(macroInfo);
            points = _macroService.Find(screen, searchButton, macroInfo);
            await Task.Delay(50);
        }
        foreach (Point point in points)
        {
            await Task.Delay(3000);
            _macroService.MouseMove(point.X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), point.Y + int.Parse(Math.Round(280 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
            _macroService.LeftClick();
        }
        searchButton.Dispose();
    }

    private async Task<bool> DCCheck(MacroInfo macroInfo)
    {
        bool result = false;
        Focus();
        Bitmap screen = CaptureRegion(macroInfo);
        Bitmap disconnected = _resourcesService.GetResource(Resource.Disconnected, macroInfo);
        List<Point> points = _macroService.Find(screen, disconnected, macroInfo);
        while (points.Count > 0)
        {
            result = true;

            foreach (Point point in points)
            {
                Focus();
                _macroService.MouseMove(point.X, point.Y + int.Parse(Math.Round(20 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                screen = CaptureRegion(macroInfo);
                Bitmap loggedInElsewhere = _resourcesService.GetResource(Resource.LoggedInElsewhere, macroInfo);
                List<Point> loggedInElsewherePoint = _macroService.Find(screen, loggedInElsewhere, macroInfo);
                if (loggedInElsewherePoint.Count == 1)
                {
                    _macroService.MouseMove(loggedInElsewherePoint[0].X, loggedInElsewherePoint[0].Y);
                    _macroService.LeftClick();
                }
                loggedInElsewhere.Dispose();
                if (macroInfo.InLevel)
                {
                    Bitmap returnToLobby = _resourcesService.GetResource(Resource.ChatGrey, macroInfo);
                    List<Point> returnToLobbyPoint = _macroService.Find(screen, returnToLobby, macroInfo);
                    if (returnToLobbyPoint.Count == 1)
                    {
                        _macroService.MouseMove(returnToLobbyPoint[0].X + int.Parse(Math.Round(235 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), returnToLobbyPoint[0].Y - int.Parse(Math.Round(55 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()));
                        _macroService.LeftClick();
                    }
                    returnToLobby.Dispose();
                }
                Bitmap login = _resourcesService.GetResource(Resource.Login, macroInfo);
                List<Point> loginPoint = _macroService.Find(screen, login, macroInfo);
                while (loginPoint.Count != 1)
                {
                    screen = CaptureRegion(macroInfo);
                    loginPoint = _macroService.Find(screen, login, macroInfo);
                    await Task.Delay(50);
                }
                login.Dispose();
                foreach (Point lPoint in loginPoint)
                {
                    _macroService.MouseMove(lPoint.X, lPoint.Y);
                    _macroService.LeftClick();
                }
                Focus();
                await SelectServer(macroInfo, macroInfo.CurrentServer, points, true);
                Focus();
                screen = CaptureRegion(macroInfo);
                Bitmap password = _resourcesService.GetResource(Resource.Password, macroInfo);
                List<Point> passwordPoint = _macroService.Find(screen, password, macroInfo);
                while (passwordPoint.Count != 1)
                {
                    screen = CaptureRegion(macroInfo);
                    passwordPoint = _macroService.Find(screen, password, macroInfo);
                    await Task.Delay(50);
                }
                password.Dispose();
                _macroService.MouseMove(passwordPoint.First().X, passwordPoint.First().Y);
                _macroService.LeftClick();
                int index = -1;
                if (macroInfo.Level1Points.Count == 4)
                {
                    Point closestPoint = _macroService.GetClosestPoint(macroInfo.Level1Points, point);
                    index = macroInfo.Level1Points.IndexOf(closestPoint);
                }
                else if (macroInfo.Level2Points.Count == 4)
                {
                    Point closestPoint = _macroService.GetClosestPoint(macroInfo.Level2Points, point);
                    index = macroInfo.Level2Points.IndexOf(closestPoint);
                }
                else if (macroInfo.Level3Points.Count == 4)
                {
                    Point closestPoint = _macroService.GetClosestPoint(macroInfo.Level3Points, point);
                    index = macroInfo.Level3Points.IndexOf(closestPoint);
                }
                else if (macroInfo.Level4Points.Count == 4)
                {
                    Point closestPoint = _macroService.GetClosestPoint(macroInfo.Level4Points, point);
                    index = macroInfo.Level4Points.IndexOf(closestPoint);
                }
                else if (macroInfo.Level5Points.Count == 4)
                {
                    Point closestPoint = _macroService.GetClosestPoint(macroInfo.Level5Points, point);
                    index = macroInfo.Level5Points.IndexOf(closestPoint);
                }
                else if (macroInfo.Level6Points.Count == 4)
                {
                    Point closestPoint = _macroService.GetClosestPoint(macroInfo.Level6Points, point);
                    index = macroInfo.Level6Points.IndexOf(closestPoint);
                }
                if (index == 0)
                {
                    await FixedSendKeys(macroInfo.Account1.Split(new char[] { '|' }, 2).Last());
                }
                else if (index == 1)
                {
                    await FixedSendKeys(macroInfo.Account2.Split(new char[] { '|' }, 2).Last());
                }
                else if (index == 2)
                {
                    await FixedSendKeys(macroInfo.Account3.Split(new char[] { '|' }, 2).Last());
                }
                else if (index == 3)
                {
                    await FixedSendKeys(macroInfo.Account4.Split(new char[] { '|' }, 2).Last());
                }
                _macroService.MouseMove(passwordPoint[0].X - int.Parse(Math.Round(50 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), passwordPoint[0].Y + int.Parse(Math.Round(130 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
                _macroService.LeftClick();
                await Task.Delay(2200);
            }
            if (macroInfo.InLevel)
            {
                Focus();
                screen = CaptureRegion(macroInfo);
                Bitmap chat = _resourcesService.GetResource(Resource.Chat, macroInfo);
                points = _macroService.Find(screen, chat, macroInfo);
                if (points.Count > 0)
                {
                    foreach (Point point in points)
                    {
                        _macroService.MouseMove(point.X + int.Parse(Math.Round(360 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), point.Y);
                        _macroService.LeftClick();
                        await ReturnToLobby(macroInfo, 1);
                    }
                }
                chat.Dispose();
            }

            points = _macroService.Find(screen, disconnected, macroInfo);
        }
        disconnected.Dispose();

        return result;
    }

    private static Bitmap CaptureScreen(MacroInfo macroInfo)
    {
        Bitmap image = new(Screen.AllScreens[macroInfo.Monitor].Bounds.Width, Screen.AllScreens[macroInfo.Monitor].Bounds.Height, PixelFormat.Format32bppArgb);
        Graphics gfx = Graphics.FromImage(image);
        gfx.CopyFromScreen(Screen.AllScreens[macroInfo.Monitor].Bounds.X, Screen.AllScreens[macroInfo.Monitor].Bounds.Y, 0, 0, Screen.AllScreens[macroInfo.Monitor].Bounds.Size, CopyPixelOperation.SourceCopy);
        return image;
    }

    private static Bitmap CaptureRegion(MacroInfo macroInfo)
    {
        Bitmap image = new(macroInfo.BottomRight.X - macroInfo.TopLeft.X, macroInfo.BottomRight.Y - macroInfo.TopLeft.Y, PixelFormat.Format32bppArgb);
        Graphics gfx = Graphics.FromImage(image);
        Size size = new(macroInfo.BottomRight.X - macroInfo.TopLeft.X, macroInfo.BottomRight.Y - macroInfo.TopLeft.Y);
        gfx.CopyFromScreen(macroInfo.TopLeft.X, macroInfo.TopLeft.Y, 0, 0, size, CopyPixelOperation.SourceCopy);
        return image;
    }

    private void Focus()
    {
        IEnumerable<bool> didFocus = _macroService.Focus("4pr2");

        if (didFocus.Any(x => !x))
        {

        }
    }

    private static async Task FixedSendKeys(string keys)
    {
        SendKeys.SendWait(keys.Replace("+", "{+}").Replace("^", "{^}").Replace("%", "{%}").Replace("~", "{~}").Replace("(", "{(}").Replace(")", "{)}"));
        await Task.Delay(1000);
    }

    private bool PointsOverlapMacro(List<Point> points, MacroInfo macroInfo)
    {
        // make sure we have 4 points
        if (points.Count != 4)
        {
            return true;
        }
        else
        {
            Point topLeft = new(points[0].X - int.Parse(Math.Round(69 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[0].Y - int.Parse(Math.Round(335 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));
            Point bottomRight = new(points[3].X + int.Parse(Math.Round(482 / Constants.BigPR2Width * macroInfo.PR2Width).ToString()), points[3].Y + int.Parse(Math.Round(66 / Constants.BigPR2Height * macroInfo.PR2Height).ToString()));

            bool result = macroInfos.Any(
                x => !x.TopLeft.IsEmpty && !x.BottomRight.IsEmpty
                && x.TopLeft.X <= topLeft.X && bottomRight.X <= x.BottomRight.X
                && x.TopLeft.Y <= topLeft.Y && bottomRight.Y <= x.BottomRight.Y);

            return result;
        }
    }

    #region Happy Hour Check

    private async Task HappyHourCheck()
    {
        while (true)
        {
            await Task.Delay(90000);
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
                PR2ServerList? pr2ServerList = JsonConvert.DeserializeObject<PR2ServerList>(text);
                if (pr2ServerList?.Servers != null)
                {
                    // get a list of the servers with happy hours (hh)
                    // and where they are not private servers or Tournament (12)
                    IEnumerable<PR2Server> hhServers = pr2ServerList.Servers.Where(x => x.HappyHour == 1 && x.Id < 12);

                    foreach (PR2Server server in hhServers)
                    {
                        Server serverEnum = server.Name switch
                        {
                            "Derron" => Server.Derron,
                            "Carina" => Server.Carina,
                            "Grayan" => Server.Grayan,
                            "Fitz" => Server.Fitz,
                            "Tournament" => Server.Tournament,
                            _ => throw new InvalidOperationException($"Invalid server name '{server.Name}'")
                        };

                        foreach (MacroInfo macroInfo in macroInfos.Where(x => x.UseHappyHourServer).OrderBy(x => x.HappyHourPriority))
                        {
                            // if current macro is not on happy hour server
                            if (macroInfo.CurrentServer != serverEnum)
                            {
                                // if there is more than 1 happy hour server
                                if (hhServers.Count() > 1)
                                {
                                    List<Server> hhServerEnums = new();

                                    // get a list of all the hhServer enums
                                    foreach (PR2Server hhServer in hhServers)
                                    {
                                        hhServerEnums.Add(hhServer.Name switch
                                        {
                                            "Derron" => Server.Derron,
                                            "Carina" => Server.Carina,
                                            "Grayan" => Server.Grayan,
                                            "Fitz" => Server.Fitz,
                                            "Tournament" => Server.Tournament,
                                            _ => throw new InvalidOperationException($"Invalid server name '{server.Name}'")
                                        });
                                    }

                                    // if macro is already on a happy hour server we don't want to switch
                                    if (hhServerEnums.Contains(macroInfo.CurrentServer))
                                    {
                                        // continue looping to let the next priority macro (if available) use this server
                                        continue;
                                    }
                                }

                                // let the macro know this macro is ready to switch server
                                macroInfo.ReadyToSwitch = true;
                                // wait for the macro to no longer be in a level
                                while (macroInfo.InLevel)
                                {
                                    await Task.Delay(1000);
                                }

                                // check if any other macros are using this server
                                if (macroInfos.FirstOrDefault(x => x.CurrentServer == serverEnum) == null)
                                {
                                    // no macros using this server so wait for completed movement then switch
                                    await WaitForResumedAndCompletedMovement(macroInfo, false);
                                    await SwitchServer(macroInfo, serverEnum);
                                    macroInfo.ReadyToSwitch = false;
                                    _movementInProgress = false;
                                }
                                else
                                {
                                    // get the other macro that is using this server
                                    MacroInfo otherMacro = macroInfos.First(x => x.CurrentServer == serverEnum);

                                    // let the macro know this macro is ready to switch server
                                    otherMacro.ReadyToSwitch = true;
                                    // wait for the macro to no longer be in a level
                                    while (otherMacro.InLevel)
                                    {
                                        await Task.Delay(1000);
                                    }

                                    // wait for completed movement
                                    await WaitForResumedAndCompletedMovement(macroInfo, false);

                                    // check if all servers are in use
                                    if (macroInfos.Count >= 4)
                                    {
                                        // all servers in use so we logout the macro on hh server
                                        await Logout(otherMacro);
                                        // get the current server of the macro we want to switch
                                        Server oldServer = macroInfo.CurrentServer;
                                        // move macro we want to switch to hh server
                                        await SwitchServer(macroInfo, serverEnum);
                                        // this macro is no longer ready to switch
                                        macroInfo.ReadyToSwitch = false;
                                        await WaitForResumedAndCompletedMovement(otherMacro, false);
                                        // log the macro we logged out into the server the macro we switched was on
                                        await ReLogin(otherMacro, oldServer);
                                        _movementInProgress = false;
                                        otherMacro.ReadyToSwitch = false;
                                    }
                                    else
                                    {
                                        // we have a free server so we workout which server it is
                                        // and switch the macro using the hh server to that
                                        if (!macroInfos.Any(x => x.CurrentServer == Server.Derron))
                                        {
                                            await SwitchServer(otherMacro, Server.Derron);
                                        }
                                        else if (!macroInfos.Any(x => x.CurrentServer == Server.Carina))
                                        {
                                            await SwitchServer(otherMacro, Server.Carina);
                                        }
                                        else if (!macroInfos.Any(x => x.CurrentServer == Server.Grayan))
                                        {
                                            await SwitchServer(otherMacro, Server.Grayan);
                                        }
                                        else
                                        {
                                            await SwitchServer(otherMacro, Server.Fitz);
                                        }

                                        otherMacro.ReadyToSwitch = false;
                                        await WaitForResumedAndCompletedMovement(macroInfo, false);
                                        // hh server is free so switch the macro to it
                                        await SwitchServer(macroInfo, serverEnum);
                                        _movementInProgress = false;
                                        macroInfo.ReadyToSwitch = false;
                                    }
                                }

                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    #endregion

    #endregion
}
