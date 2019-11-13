using PR2Macro.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR2Macro.Macros
{
    public class SimFunctions
    {
        private readonly string account1, account2, account3, account4, server;
        private static string window;
        private string currentServer = "Derron", currentSearchBy = "User Name", currentSortBy = "Date", currentSortOrder = "Descending";
        private int currentPage = 1;
        private readonly int pr2Width, pr2Height;
        private List<Point> level1Points = new List<Point>(), level2Points = new List<Point>(), level3Points = new List<Point>(), level4Points = new List<Point>(), level5Points = new List<Point>(), level6Points = new List<Point>();

        public SimFunctions(string acc1, string acc2, string acc3, string acc4, string svr, string wdw, int width, int height)
        {
            foreach (string acc in Settings.Default.Accounts)
            {
                if (acc != null && acc.Length > 0)
                {
                    if (acc1.Equals(acc.Split('|').FirstOrDefault()))
                    {
                        account1 = acc;
                    }
                    else if (acc2.Equals(acc.Split('|').FirstOrDefault()))
                    {
                        account2 = acc;
                    }
                    else if (acc3.Equals(acc.Split('|').FirstOrDefault()))
                    {
                        account3 = acc;
                    }
                    else if (acc4.Equals(acc.Split('|').FirstOrDefault()))
                    {
                        account4 = acc;
                    }
                }
            }
            if (svr.Contains("!!"))
            {
                server = "!! " + svr.Split(' ').ElementAt(1);
            }
            else
            {
                server = svr.Split(' ').FirstOrDefault();
            }
            window = wdw;
            pr2Width = width;
            pr2Height = height;
        }

        public void Focus()
        {
            if (!MacroController.Focus(window))
            {
                MessageBox.Show("Error: Could not focus the selected browser application.");
                Environment.Exit(0);
            }
        }

        private Bitmap CaptureScreen()
        {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(image);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return image;
        }

        public async Task Login()
        {
            await Task.Delay(1000);
            Focus();
            await Task.Delay(500);
            Bitmap screen = CaptureScreen();
            Bitmap download = new Bitmap(Resources.download, new Size(int.Parse(Math.Round(Resources.download.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.download.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, download);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, download);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                MacroController.MouseMove(point.X, point.Y);
                MacroController.LeftClick();
            }
            download.Dispose();
            screen = CaptureScreen();
            Bitmap mute = new Bitmap(Resources.mute, new Size(int.Parse(Math.Round(Resources.mute.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.mute.Height / 400.0 * pr2Height).ToString())));
            points = MacroController.Find(screen, mute);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, mute);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                MacroController.MouseMove(point.X, point.Y);
                MacroController.LeftClick();
            }
            mute.Dispose();
            Focus();
            await Task.Delay(1000);
            screen = CaptureScreen();
            Bitmap login = new Bitmap(Resources.login, new Size(int.Parse(Math.Round(Resources.login.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.login.Height / 400.0 * pr2Height).ToString())));
            points = MacroController.Find(screen, login);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, login);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                MacroController.MouseMove(point.X, point.Y);
                MacroController.LeftClick();
            }
            login.Dispose();
            Focus();
            await SelectServer(server, 4);
            Focus();
            screen = CaptureScreen();
            Bitmap username = new Bitmap(Resources.name, new Size(int.Parse(Math.Round(Resources.name.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.name.Height / 400.0 * pr2Height).ToString())));
            Bitmap password = new Bitmap(Resources.password, new Size(int.Parse(Math.Round(Resources.password.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.password.Height / 400.0 * pr2Height).ToString())));
            Bitmap connect = new Bitmap(Resources.connect, new Size(int.Parse(Math.Round(Resources.connect.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.connect.Height / 400.0 * pr2Height).ToString())));
            List<Point> usernamePoints = MacroController.Find(screen, username);
            List<Point> passwordPoints = MacroController.Find(screen, password);
            List<Point> connectPoints = MacroController.Find(screen, connect);
            while (usernamePoints.Count != 4 || passwordPoints.Count != 4 || connectPoints.Count != 4)
            {
                screen = CaptureScreen();
                if (usernamePoints.Count != 4)
                {
                    usernamePoints = MacroController.Find(screen, username);
                }
                else if (passwordPoints.Count != 4)
                {
                    passwordPoints = MacroController.Find(screen, password);
                }
                else if (connectPoints.Count != 4)
                {
                    connectPoints = MacroController.Find(screen, connect);
                }
                await Task.Delay(50);
            }
            username.Dispose();
            password.Dispose();
            connect.Dispose();
            MacroController.MouseMove(usernamePoints[0].X, usernamePoints[0].Y);
            MacroController.LeftClick();
            SendKeys.Send(account1.Split('|').FirstOrDefault());
            MacroController.MouseMove(passwordPoints[0].X, passwordPoints[0].Y);
            MacroController.LeftClick();
            SendKeys.Send(account1.Split(new char[] { '|' }, 2).LastOrDefault());
            MacroController.MouseMove(connectPoints[0].X, connectPoints[0].Y);
            MacroController.LeftClick();
            await Task.Delay(2200);
            Focus();
            MacroController.MouseMove(usernamePoints[1].X, usernamePoints[1].Y);
            MacroController.LeftClick();
            SendKeys.Send(account2.Split('|').FirstOrDefault());
            MacroController.MouseMove(passwordPoints[1].X, passwordPoints[1].Y);
            MacroController.LeftClick();
            SendKeys.Send(account2.Split(new char[] { '|' }, 2).LastOrDefault());
            MacroController.MouseMove(connectPoints[1].X, connectPoints[1].Y);
            MacroController.LeftClick();
            await Task.Delay(2200);
            Focus();
            MacroController.MouseMove(usernamePoints[2].X, usernamePoints[2].Y);
            MacroController.LeftClick();
            SendKeys.Send(account3.Split('|').FirstOrDefault());
            MacroController.MouseMove(passwordPoints[2].X, passwordPoints[2].Y);
            MacroController.LeftClick();
            SendKeys.Send(account3.Split(new char[] { '|' }, 2).LastOrDefault());
            MacroController.MouseMove(connectPoints[2].X, connectPoints[2].Y);
            MacroController.LeftClick();
            await Task.Delay(2200);
            Focus();
            MacroController.MouseMove(usernamePoints[3].X, usernamePoints[3].Y);
            MacroController.LeftClick();
            SendKeys.Send(account4.Split('|').FirstOrDefault());
            MacroController.MouseMove(passwordPoints[3].X, passwordPoints[3].Y);
            MacroController.LeftClick();
            SendKeys.Send(account4.Split(new char[] { '|' }, 2).LastOrDefault());
            MacroController.MouseMove(connectPoints[3].X, connectPoints[3].Y);
            MacroController.LeftClick();
            await Search();
        }

        public async Task SelectServer(string server, int count)
        {
            Bitmap screen = CaptureScreen();
            Bitmap rememberMe = new Bitmap(Resources.rememberMe, new Size(int.Parse(Math.Round(Resources.rememberMe.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.rememberMe.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, rememberMe);
            while (points.Count != count)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, rememberMe);
                await Task.Delay(50);
            }
            if ((server.Equals("Derron") || server.Equals("!! Derron")) && (currentServer != "Derron" || currentServer != "!! Derron"))
            {
                //Bitmap derron;
                //if (server.Contains("!! "))
                //{
                //    derron = new Bitmap(Resources.derron, new Size(int.Parse(Math.Round(Resources.derron.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.derron.Height / 400.0 * pr2Height).ToString())));
                //}
                //else
                //{
                //    derron = new Bitmap(Resources.derron, new Size(int.Parse(Math.Round(Resources.derron.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.derron.Height / 400.0 * pr2Height).ToString())));
                //}
                //foreach (Point point in points)
                //{
                //    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / 400.0 * pr2Height).ToString()));
                //    MacroController.LeftClick();
                //    screen = CaptureScreen();
                //    List<Point> serverLocation = MacroController.Find(screen, derron);
                //    while (serverLocation.Count != 1)
                //    {
                //        screen = CaptureScreen();
                //        serverLocation = MacroController.Find(screen, derron);
                //        await Task.Delay(50);
                //    }
                //    MacroController.MouseMove(serverLocation.First().X, serverLocation.First().Y);
                //    MacroController.LeftClick();
                //}
                //derron.Dispose();
            }
            else if ((server.Equals("Carina") || server.Equals("!! Carina")) && (currentServer != "Carina" || currentServer != "!! Carina"))
            {
                Bitmap carina;
                if (server.Contains("!! "))
                {
                    carina = new Bitmap(Resources.carinaHH, new Size(int.Parse(Math.Round(Resources.carinaHH.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.carinaHH.Height / 400.0 * pr2Height).ToString())));
                }
                else
                {
                    carina = new Bitmap(Resources.carina, new Size(int.Parse(Math.Round(Resources.carina.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.carina.Height / 400.0 * pr2Height).ToString())));
                }
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> serverLocation = MacroController.Find(screen, carina);
                    while (serverLocation.Count != 1)
                    {
                        screen = CaptureScreen();
                        serverLocation = MacroController.Find(screen, carina);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(serverLocation.First().X, serverLocation.First().Y);
                    MacroController.LeftClick();
                }
                carina.Dispose();
            }
            else if ((server.Equals("Grayan") || server.Equals("!! Grayan")) && (currentServer != "Grayan" || currentServer != "!! Grayan"))
            {
                Bitmap grayan;
                if (server.Contains("!! "))
                {
                    grayan = new Bitmap(Resources.grayanHH, new Size(int.Parse(Math.Round(Resources.grayanHH.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.grayanHH.Height / 400.0 * pr2Height).ToString())));
                }
                else
                {
                    grayan = new Bitmap(Resources.grayan, new Size(int.Parse(Math.Round(Resources.grayan.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.grayan.Height / 400.0 * pr2Height).ToString())));
                }
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> serverLocation = MacroController.Find(screen, grayan);
                    while (serverLocation.Count != 1)
                    {
                        screen = CaptureScreen();
                        serverLocation = MacroController.Find(screen, grayan);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(serverLocation.First().X, serverLocation.First().Y);
                    MacroController.LeftClick();
                }
                grayan.Dispose();
            }
            else if ((server.Equals("Fitz") || server.Equals("!! Fitz")) && (currentServer != "Fitz" || currentServer != "!! Fitz"))
            {
                Bitmap fitz;
                if (server.Contains("!! "))
                {
                    fitz = new Bitmap(Resources.fitzHH, new Size(int.Parse(Math.Round(Resources.fitzHH.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.fitzHH.Height / 400.0 * pr2Height).ToString())));
                }
                else
                {
                    fitz = new Bitmap(Resources.fitz, new Size(int.Parse(Math.Round(Resources.fitz.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.fitz.Height / 400.0 * pr2Height).ToString())));
                }
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> serverLocation = MacroController.Find(screen, fitz);
                    while (serverLocation.Count != 1)
                    {
                        screen = CaptureScreen();
                        serverLocation = MacroController.Find(screen, fitz);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(serverLocation.First().X, serverLocation.First().Y);
                    MacroController.LeftClick();
                }
                fitz.Dispose();
            }
            else if (server.Equals("Tournament") && currentServer != "Tournament")
            {
                Bitmap tournament = new Bitmap(Resources.tournament, new Size(int.Parse(Math.Round(Resources.tournament.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.tournament.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(40 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> serverLocation = MacroController.Find(screen, tournament);
                    while (serverLocation.Count != 1)
                    {
                        screen = CaptureScreen();
                        serverLocation = MacroController.Find(screen, tournament);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(serverLocation.First().X, serverLocation.First().Y);
                    MacroController.LeftClick();
                }
                tournament.Dispose();
            }
            currentServer = server;
            rememberMe.Dispose();
        }

        private async Task Search()
        {
            await Task.Delay(500);
            Bitmap screen = CaptureScreen();
            Bitmap searchTab = new Bitmap(Resources.searchTab, new Size(int.Parse(Math.Round(Resources.searchTab.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.searchTab.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, searchTab);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, searchTab);
                await Task.Delay(50);
            }
            searchTab.Dispose();
            foreach (Point point in points)
            {
                MacroController.MouseMove(point.X, point.Y);
                MacroController.LeftClick();
            }
        }

        public async Task SearchBy(string searchBy)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            Bitmap search = new Bitmap(Resources.searchBy, new Size(int.Parse(Math.Round(Resources.searchBy.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.searchBy.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, search);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, search);
                await Task.Delay(50);
            }
            search.Dispose();
            if (searchBy == "User Name" && currentSearchBy != "User Name")
            {
                Bitmap username = new Bitmap(Resources.username, new Size(int.Parse(Math.Round(Resources.username.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.username.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> usernamePoint = MacroController.Find(screen, username);
                    while (usernamePoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        usernamePoint = MacroController.Find(screen, username);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(usernamePoint.First().X, usernamePoint.First().Y);
                    MacroController.LeftClick();
                }
                username.Dispose();
            }
            else if (searchBy == "Course Title" && currentSearchBy != "Course Title")
            {
                Bitmap courseTitle = new Bitmap(Resources.courseTitle, new Size(int.Parse(Math.Round(Resources.courseTitle.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.courseTitle.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> coursePoint = MacroController.Find(screen, courseTitle);
                    while (coursePoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        coursePoint = MacroController.Find(screen, courseTitle);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(coursePoint.First().X, coursePoint.First().Y);
                    MacroController.LeftClick();
                }
                courseTitle.Dispose();
            }
            currentSearchBy = searchBy;
        }

        public async Task SortBy(string sortBy, string orderBy)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            Bitmap sort = new Bitmap(Resources.sortBy, new Size(int.Parse(Math.Round(Resources.sortBy.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.sortBy.Height / 400.0 * pr2Height).ToString())));
            Bitmap order = new Bitmap(Resources.sortOrder, new Size(int.Parse(Math.Round(Resources.sortOrder.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.sortOrder.Height / 400.0 * pr2Height).ToString())));
            List<Point> sortLocations = MacroController.Find(screen, sort);
            List<Point> orderLocations = MacroController.Find(screen, order);
            while (sortLocations.Count != 4 || orderLocations.Count != 4)
            {
                screen = CaptureScreen();
                if (sortLocations.Count != 4)
                {
                    sortLocations = MacroController.Find(screen, sort);
                }
                if (orderLocations.Count != 4)
                {
                    orderLocations = MacroController.Find(screen, sort);
                }
                await Task.Delay(50);
            }
            sort.Dispose();
            order.Dispose();
            if (sortBy.Equals("Date") && currentSortBy != "Date")
            {
                Bitmap date = new Bitmap(Resources.date, new Size(int.Parse(Math.Round(Resources.date.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.date.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in sortLocations)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> datePoint = MacroController.Find(screen, date);
                    while (datePoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        datePoint = MacroController.Find(screen, date);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(datePoint.First().X, datePoint.First().Y);
                    MacroController.LeftClick();
                }
                date.Dispose();
            }
            else if (sortBy.Equals("Alphabetical") && currentSortBy != "Alphabetical")
            {
                Bitmap alphabetical = new Bitmap(Resources.alphabetical, new Size(int.Parse(Math.Round(Resources.alphabetical.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.alphabetical.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in sortLocations)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> alphabeticalPoint = MacroController.Find(screen, alphabetical);
                    while (alphabeticalPoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        alphabeticalPoint = MacroController.Find(screen, alphabetical);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(alphabeticalPoint.First().X, alphabeticalPoint.First().Y);
                    MacroController.LeftClick();
                }
                alphabetical.Dispose();
            }
            else if (sortBy.Equals("Rating") && currentSortBy != "Rating")
            {
                Bitmap rating = new Bitmap(Resources.rating, new Size(int.Parse(Math.Round(Resources.rating.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.rating.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in sortLocations)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> ratingPoint = MacroController.Find(screen, rating);
                    while (ratingPoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        ratingPoint = MacroController.Find(screen, rating);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(ratingPoint.First().X, ratingPoint.First().Y);
                    MacroController.LeftClick();
                }
                rating.Dispose();
            }
            else if (sortBy.Equals("Popularity") && currentSortBy != "Popularity")
            {
                Bitmap popularity = new Bitmap(Resources.popularity, new Size(int.Parse(Math.Round(Resources.popularity.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.popularity.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in sortLocations)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> popularityPoint = MacroController.Find(screen, popularity);
                    while (popularityPoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        popularityPoint = MacroController.Find(screen, popularity);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(popularityPoint.First().X, popularityPoint.First().Y);
                    MacroController.LeftClick();
                }
                popularity.Dispose();
            }
            if (orderBy.Equals("Descending") && currentSortOrder != "Descending")
            {
                Bitmap descending = new Bitmap(Resources.descending, new Size(int.Parse(Math.Round(Resources.descending.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.descending.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in orderLocations)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> descendingPoint = MacroController.Find(screen, descending);
                    while (descendingPoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        descendingPoint = MacroController.Find(screen, descending);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(descendingPoint.First().X, descendingPoint.First().Y);
                    MacroController.LeftClick();
                }
                descending.Dispose();
            }
            else if (orderBy.Equals("Ascending") && currentSortOrder != "Ascending")
            {
                Bitmap ascending = new Bitmap(Resources.ascending, new Size(int.Parse(Math.Round(Resources.ascending.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.ascending.Height / 400.0 * pr2Height).ToString())));
                foreach (Point point in orderLocations)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    List<Point> ascendingPoint = MacroController.Find(screen, ascending);
                    while (ascendingPoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        ascendingPoint = MacroController.Find(screen, ascending);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(ascendingPoint.First().X, ascendingPoint.First().Y);
                    MacroController.LeftClick();
                }
                ascending.Dispose();
            }
            currentSortBy = sortBy;
            currentSortOrder = orderBy;
        }

        public async Task EnterSearch(string search)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            Bitmap searchBox = new Bitmap(Resources.searchBox, new Size(int.Parse(Math.Round(Resources.searchBox.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.searchBox.Height / 400.0 * pr2Height).ToString())));
            Bitmap searchButton = new Bitmap(Resources.search, new Size(int.Parse(Math.Round(Resources.search.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.search.Height / 400.0 * pr2Height).ToString())));
            List<Point> boxLocations = MacroController.Find(screen, searchBox);
            List<Point> buttonLocations = MacroController.Find(screen, searchButton);
            while (boxLocations.Count != 4 || buttonLocations.Count != 4)
            {
                screen = CaptureScreen();
                if (boxLocations.Count != 4)
                {
                    boxLocations = MacroController.Find(screen, searchBox);
                }
                if (buttonLocations.Count != 4)
                {
                    buttonLocations = MacroController.Find(screen, searchButton);
                }
                await Task.Delay(50);
            }
            searchBox.Dispose();
            searchButton.Dispose();
            for (int i = 0; i < 4; i++)
            {
                MacroController.MouseMove(boxLocations[i].X, boxLocations[i].Y);
                MacroController.LeftClick();
                SendKeys.Send(search);
                MacroController.MouseMove(buttonLocations[i].X, buttonLocations[i].Y);
                MacroController.LeftClick();
            }
        }

        public async Task SelectPage(int page)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            if (page == 1 && currentPage != 1)
            {
                Bitmap page1 = new Bitmap(Resources.page1, new Size(int.Parse(Math.Round(Resources.page1.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page1.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page1);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page1);
                    await Task.Delay(50);
                }
                page1.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 2 && currentPage != 2)
            {
                Bitmap page2 = new Bitmap(Resources.page2, new Size(int.Parse(Math.Round(Resources.page2.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page2.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page2);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page2);
                    await Task.Delay(50);
                }
                page2.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 3 && currentPage != 3)
            {
                Bitmap page3 = new Bitmap(Resources.page3, new Size(int.Parse(Math.Round(Resources.page3.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page3.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page3);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page3);
                    await Task.Delay(50);
                }
                page3.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 4 && currentPage != 4)
            {
                Bitmap page4 = new Bitmap(Resources.page4, new Size(int.Parse(Math.Round(Resources.page4.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page4.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page4);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page4);
                    await Task.Delay(50);
                }
                page4.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 5 && currentPage != 5)
            {
                Bitmap page5 = new Bitmap(Resources.page5, new Size(int.Parse(Math.Round(Resources.page5.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page5.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page5);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page5);
                    await Task.Delay(50);
                }
                page5.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 6 && currentPage != 6)
            {
                Bitmap page6 = new Bitmap(Resources.page6, new Size(int.Parse(Math.Round(Resources.page6.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page6.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page6);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page6);
                    await Task.Delay(50);
                }
                page6.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 7 && currentPage != 7)
            {
                Bitmap page7 = new Bitmap(Resources.page7, new Size(int.Parse(Math.Round(Resources.page7.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page7.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page7);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page7);
                    await Task.Delay(50);
                }
                page7.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 8 && currentPage != 8)
            {
                Bitmap page8 = new Bitmap(Resources.page8, new Size(int.Parse(Math.Round(Resources.page8.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page8.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page8);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page8);
                    await Task.Delay(50);
                }
                page8.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            else if (page == 9 && currentPage != 9)
            {
                Bitmap page9 = new Bitmap(Resources.page9, new Size(int.Parse(Math.Round(Resources.page9.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.page9.Height / 400.0 * pr2Height).ToString())));
                List<Point> points = MacroController.Find(screen, page9);
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, page9);
                    await Task.Delay(50);
                }
                page9.Dispose();
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y);
                    MacroController.LeftClick();
                }
            }
            currentPage = page;
        }

        public async Task EnterLevel(int level, string simType)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            Bitmap searchBy = new Bitmap(Resources.searchBy, new Size(int.Parse(Math.Round(Resources.searchBy.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.searchBy.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, searchBy);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, searchBy);
                await Task.Delay(50);
            }
            searchBy.Dispose();
            await Task.Delay(500);
            if (simType.Equals("Objective"))
            {
                if (level == 1)
                {
                    Bitmap level1 = new Bitmap(Resources.level1Obj, new Size(int.Parse(Math.Round(Resources.level1Obj.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level1Obj.Height / 400.0 * pr2Height).ToString())));
                    while (level1Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level1Points = MacroController.Find(screen, level1);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level1Points[0].X, level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[1].X, level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[2].X, level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[3].X, level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level1ObjSelected, new Size(int.Parse(Math.Round(Resources.level1ObjSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level1ObjSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level1Points[0].X - 90, level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[1].X - 90, level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[2].X - 90, level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[3].X - 90, level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level1.Dispose();
                }
                else if (level == 2)
                {
                    Bitmap level2 = new Bitmap(Resources.level2Obj, new Size(int.Parse(Math.Round(Resources.level2Obj.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level2Obj.Height / 400.0 * pr2Height).ToString())));
                    while (level2Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level2Points = MacroController.Find(screen, level2);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level2Points[0].X, level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[1].X, level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[2].X, level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[3].X, level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level2ObjSelected, new Size(int.Parse(Math.Round(Resources.level2ObjSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level2ObjSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level2Points[0].X - 90, level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[1].X - 90, level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[2].X - 90, level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[3].X - 90, level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level2.Dispose();
                }
                else if (level == 3)
                {
                    Bitmap level3 = new Bitmap(Resources.level3Obj, new Size(int.Parse(Math.Round(Resources.level3Obj.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level3Obj.Height / 400.0 * pr2Height).ToString())));
                    while (level3Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level3Points = MacroController.Find(screen, level3);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level3Points[0].X, level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[1].X, level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[2].X, level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[3].X, level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level3ObjSelected, new Size(int.Parse(Math.Round(Resources.level3ObjSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level3ObjSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level3Points[0].X - 90, level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[1].X - 90, level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[2].X - 90, level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[3].X - 90, level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level3.Dispose();
                }
                else if (level == 4)
                {
                    Bitmap level4 = new Bitmap(Resources.level4Obj, new Size(int.Parse(Math.Round(Resources.level4Obj.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level4Obj.Height / 400.0 * pr2Height).ToString())));
                    while (level4Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level4Points = MacroController.Find(screen, level4);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level4Points[0].X, level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[1].X, level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[2].X, level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[3].X, level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level4ObjSelected, new Size(int.Parse(Math.Round(Resources.level4ObjSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level4ObjSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level4Points[0].X - 90, level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[1].X - 90, level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[2].X - 90, level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[3].X - 90, level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level4.Dispose();
                }
                else if (level == 5)
                {
                    Bitmap level5 = new Bitmap(Resources.level5Obj, new Size(int.Parse(Math.Round(Resources.level5Obj.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level5Obj.Height / 400.0 * pr2Height).ToString())));
                    while (level5Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level5Points = MacroController.Find(screen, level5);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level5Points[0].X, level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[1].X, level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[2].X, level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[3].X, level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level5ObjSelected, new Size(int.Parse(Math.Round(Resources.level5ObjSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level5ObjSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level5Points[0].X - 90, level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[1].X - 90, level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[2].X - 90, level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[3].X - 90, level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level5.Dispose();
                }
                else if (level == 6)
                {
                    Bitmap level6 = new Bitmap(Resources.level6Obj, new Size(int.Parse(Math.Round(Resources.level6Obj.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level6Obj.Height / 400.0 * pr2Height).ToString())));
                    while (level6Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level6Points = MacroController.Find(screen, level6);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level6Points[0].X, level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[1].X, level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[2].X, level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[3].X, level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level6ObjSelected, new Size(int.Parse(Math.Round(Resources.level6ObjSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level6ObjSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level6Points[0].X - 90, level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[1].X - 90, level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[2].X - 90, level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[3].X - 90, level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level6.Dispose();
                }
            }
            else if (simType.Equals("1p") || simType.Equals("4p"))
            {
                if (level == 1)
                {
                    Bitmap level1 = new Bitmap(Resources.level1Race, new Size(int.Parse(Math.Round(Resources.level1Race.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level1Race.Height / 400.0 * pr2Height).ToString())));
                    while (level1Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level1Points = MacroController.Find(screen, level1);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level1Points[0].X, level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[1].X, level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[2].X, level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[3].X, level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level1RaceSelected, new Size(int.Parse(Math.Round(Resources.level1RaceSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level1RaceSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level1Points[0].X - 90, level1Points[0].Y - (level1.Height / 2) + (level1.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[1].X - 90, level1Points[1].Y - (level1.Height / 2) + (3 * (level1.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[2].X - 90, level1Points[2].Y - (level1.Height / 2) + (5 * (level1.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level1Points[3].X - 90, level1Points[3].Y - (level1.Height / 2) + (7 * (level1.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level1.Dispose();
                }
                else if (level == 2)
                {
                    Bitmap level2 = new Bitmap(Resources.level2Race, new Size(int.Parse(Math.Round(Resources.level2Race.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level2Race.Height / 400.0 * pr2Height).ToString())));
                    while (level2Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level2Points = MacroController.Find(screen, level2);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level2Points[0].X, level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[1].X, level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[2].X, level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[3].X, level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level2RaceSelected, new Size(int.Parse(Math.Round(Resources.level2RaceSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level2RaceSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level2Points[0].X - 90, level2Points[0].Y - (level2.Height / 2) + (level2.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[1].X - 90, level2Points[1].Y - (level2.Height / 2) + (3 * (level2.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[2].X - 90, level2Points[2].Y - (level2.Height / 2) + (5 * (level2.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level2Points[3].X - 90, level2Points[3].Y - (level2.Height / 2) + (7 * (level2.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level2.Dispose();
                }
                else if (level == 3)
                {
                    Bitmap level3 = new Bitmap(Resources.level3Race, new Size(int.Parse(Math.Round(Resources.level3Race.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level3Race.Height / 400.0 * pr2Height).ToString())));
                    while (level3Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level3Points = MacroController.Find(screen, level3);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level3Points[0].X, level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[1].X, level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[2].X, level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[3].X, level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level3RaceSelected, new Size(int.Parse(Math.Round(Resources.level3RaceSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level3RaceSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level3Points[0].X - 90, level3Points[0].Y - (level3.Height / 2) + (level3.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[1].X - 90, level3Points[1].Y - (level3.Height / 2) + (3 * (level3.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[2].X - 90, level3Points[2].Y - (level3.Height / 2) + (5 * (level3.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level3Points[3].X - 90, level3Points[3].Y - (level3.Height / 2) + (7 * (level3.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level3.Dispose();
                }
                else if (level == 4)
                {
                    Bitmap level4 = new Bitmap(Resources.level4Race, new Size(int.Parse(Math.Round(Resources.level4Race.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level4Race.Height / 400.0 * pr2Height).ToString())));
                    while (level4Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level4Points = MacroController.Find(screen, level4);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level4Points[0].X, level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[1].X, level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[2].X, level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[3].X, level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level4RaceSelected, new Size(int.Parse(Math.Round(Resources.level4RaceSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level4RaceSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level4Points[0].X - 90, level4Points[0].Y - (level4.Height / 2) + (level4.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[1].X - 90, level4Points[1].Y - (level4.Height / 2) + (3 * (level4.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[2].X - 90, level4Points[2].Y - (level4.Height / 2) + (5 * (level4.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level4Points[3].X - 90, level4Points[3].Y - (level4.Height / 2) + (7 * (level4.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level4.Dispose();
                }
                else if (level == 5)
                {
                    Bitmap level5 = new Bitmap(Resources.level5Race, new Size(int.Parse(Math.Round(Resources.level5Race.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level5Race.Height / 400.0 * pr2Height).ToString())));
                    while (level5Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level5Points = MacroController.Find(screen, level5);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level5Points[0].X, level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[1].X, level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[2].X, level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[3].X, level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level5RaceSelected, new Size(int.Parse(Math.Round(Resources.level5RaceSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level5RaceSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level5Points[0].X - 90, level5Points[0].Y - (level5.Height / 2) + (level5.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[1].X - 90, level5Points[1].Y - (level5.Height / 2) + (3 * (level5.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[2].X - 90, level5Points[2].Y - (level5.Height / 2) + (5 * (level5.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level5Points[3].X - 90, level5Points[3].Y - (level5.Height / 2) + (7 * (level5.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level5.Dispose();
                }
                else if (level == 6)
                {
                    Bitmap level6 = new Bitmap(Resources.level6Race, new Size(int.Parse(Math.Round(Resources.level6Race.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level6Race.Height / 400.0 * pr2Height).ToString())));
                    while (level6Points.Count != 4)
                    {
                        screen = CaptureScreen();
                        level6Points = MacroController.Find(screen, level6);
                        await Task.Delay(50);
                    }
                    MacroController.MouseMove(level6Points[0].X, level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[1].X, level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[2].X, level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[3].X, level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)));
                    MacroController.LeftClick();
                    await Task.Delay(100);
                    Bitmap selected = new Bitmap(Resources.level6RaceSelected, new Size(int.Parse(Math.Round(Resources.level6RaceSelected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.level6RaceSelected.Height / 400.0 * pr2Height).ToString())));
                    screen = CaptureScreen();
                    while (MacroController.Find(screen, selected).Count != 4)
                    {
                        screen = CaptureScreen();
                        await Task.Delay(50);
                    }
                    selected.Dispose();
                    MacroController.MouseMove(level6Points[0].X - 90, level6Points[0].Y - (level6.Height / 2) + (level6.Height / 8) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[1].X - 90, level6Points[1].Y - (level6.Height / 2) + (3 * (level6.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[2].X - 90, level6Points[2].Y - (level6.Height / 2) + (5 * (level6.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    MacroController.MouseMove(level6Points[3].X - 90, level6Points[3].Y - (level6.Height / 2) + (7 * (level6.Height / 8)) + int.Parse(Math.Round(10 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    level6.Dispose();
                }
            }
        }

        public async Task Quit(string simType)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            Bitmap chat = new Bitmap(Resources.chat, new Size(int.Parse(Math.Round(Resources.chat.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.chat.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, chat);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, chat);
                await Task.Delay(50);
            }
            chat.Dispose();
            if (simType.Equals("Objective"))
            {
                MacroController.MouseMove(points[0].X + int.Parse(Math.Round(360 / 550.0 * pr2Width).ToString()), points[0].Y);
                MacroController.LeftClick();
                await Task.Delay(100);
                MacroController.MouseMove(points[1].X, points[1].Y);
                MacroController.LeftClick();
                SendKeys.Send("                                                            ");
                await Task.Delay(4500);
                MacroController.MouseMove(points[1].X + int.Parse(Math.Round(360 / 550.0 * pr2Width).ToString()), points[1].Y);
                MacroController.LeftClick();
                await Task.Delay(100);
                MacroController.MouseMove(points[2].X, points[2].Y);
                MacroController.LeftClick();
                SendKeys.Send("                                                            ");
                await Task.Delay(4500);
                MacroController.MouseMove(points[2].X + int.Parse(Math.Round(360 / 550.0 * pr2Width).ToString()), points[2].Y);
                MacroController.LeftClick();
                await Task.Delay(100);
                MacroController.MouseMove(points[3].X, points[3].Y);
                MacroController.LeftClick();
                SendKeys.Send("                                                            ");
                await Task.Delay(4000);
                MacroController.MouseMove(points[3].X + int.Parse(Math.Round(360 / 550.0 * pr2Width).ToString()), points[3].Y);
                MacroController.LeftClick();
            }
            else if (simType.Equals("1p"))
            {
                for (int i = 1; i < 4; i++)
                {
                    MacroController.MouseMove(points[i].X + int.Parse(Math.Round(360 / 550.0 * pr2Width).ToString()), points[i].Y);
                    MacroController.LeftClick();
                }
            }
        }

        public async Task ReturnToLobby(int? option = null)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            Bitmap star = new Bitmap(Resources.star, new Size(int.Parse(Math.Round(Resources.star.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.star.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, star);
            if (option == null)
            {
                while (points.Count != 4)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, star);
                    await Task.Delay(50);
                }
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(30 / 550.0 * pr2Width).ToString()));
                    MacroController.LeftClick();
                }
            }
            else if (option == 1)
            {
                while (points.Count != 1)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, star);
                    await Task.Delay(50);
                }
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(30 / 550.0 * pr2Width).ToString()));
                    MacroController.LeftClick();
                }
            }
            else if (option == 2)
            {
                while (points.Count != 3)
                {
                    screen = CaptureScreen();
                    points = MacroController.Find(screen, star);
                    await Task.Delay(50);
                }
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(30 / 550.0 * pr2Width).ToString()));
                    MacroController.LeftClick();
                }
            }
            star.Dispose();
        }

        public async Task SwitchServer(string server)
        {
            if (server.Equals(currentServer))
            {
                return;
            }
            Focus();
            await Task.Delay(1000);
            Bitmap screen = CaptureScreen();
            Bitmap logout = new Bitmap(Resources.logout, new Size(int.Parse(Math.Round(Resources.logout.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.logout.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, logout);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, logout);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                await Task.Delay(2200);
                MacroController.MouseMove(point.X, point.Y);
                MacroController.LeftClick();
            }
            logout.Dispose();
            Bitmap login = new Bitmap(Resources.login, new Size(int.Parse(Math.Round(Resources.login.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.login.Height / 400.0 * pr2Height).ToString())));
            points = MacroController.Find(screen, login);
            while (points.Count != 4)
            {
                screen = CaptureScreen();
                points = MacroController.Find(screen, login);
                await Task.Delay(50);
            }
            foreach (Point point in points)
            {
                MacroController.MouseMove(point.X, point.Y);
                MacroController.LeftClick();
            }
            login.Dispose();
            Focus();
            await SelectServer(server, 4);
            Focus();
            screen = CaptureScreen();
            Bitmap password = new Bitmap(Resources.password, new Size(int.Parse(Math.Round(Resources.password.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.password.Height / 400.0 * pr2Height).ToString())));
            Bitmap connect = new Bitmap(Resources.connect, new Size(int.Parse(Math.Round(Resources.connect.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.connect.Height / 400.0 * pr2Height).ToString())));
            List<Point> passwordPoints = MacroController.Find(screen, password);
            List<Point> connectPoints = MacroController.Find(screen, connect);
            while (passwordPoints.Count != 4 || connectPoints.Count != 4)
            {
                screen = CaptureScreen();
                if (passwordPoints.Count != 4)
                {
                    passwordPoints = MacroController.Find(screen, password);
                }
                else if (connectPoints.Count != 4)
                {
                    connectPoints = MacroController.Find(screen, connect);
                }
                await Task.Delay(50);
            }
            password.Dispose();
            connect.Dispose();
            MacroController.MouseMove(passwordPoints[0].X, passwordPoints[0].Y);
            MacroController.LeftClick();
            SendKeys.Send(account1.Split(new char[] { '|' }, 2).Last());
            MacroController.MouseMove(connectPoints[0].X, connectPoints[0].Y);
            MacroController.LeftClick();
            await Task.Delay(2200);
            Focus();
            MacroController.MouseMove(passwordPoints[1].X, passwordPoints[1].Y);
            MacroController.LeftClick();
            SendKeys.Send(account2.Split(new char[] { '|' }, 2).Last());
            MacroController.MouseMove(connectPoints[1].X, connectPoints[1].Y);
            MacroController.LeftClick();
            await Task.Delay(2200);
            Focus();
            MacroController.MouseMove(passwordPoints[2].X, passwordPoints[2].Y);
            MacroController.LeftClick();
            SendKeys.Send(account3.Split(new char[] { '|' }, 2).Last());
            MacroController.MouseMove(connectPoints[2].X, connectPoints[2].Y);
            MacroController.LeftClick();
            await Task.Delay(2200);
            Focus();
            MacroController.MouseMove(passwordPoints[3].X, passwordPoints[3].Y);
            MacroController.LeftClick();
            SendKeys.Send(account4.Split(new char[] { '|' }, 2).Last());
            MacroController.MouseMove(connectPoints[3].X, connectPoints[3].Y);
            MacroController.LeftClick();
        }

        public async Task DCCheck(bool inLevel)
        {
            Focus();
            Bitmap screen = CaptureScreen();
            Bitmap disconnected = new Bitmap(Resources.disconnected, new Size(int.Parse(Math.Round(Resources.disconnected.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.disconnected.Height / 400.0 * pr2Height).ToString())));
            List<Point> points = MacroController.Find(screen, disconnected);
            if (points.Count > 0)
            {
                foreach (Point point in points)
                {
                    MacroController.MouseMove(point.X, point.Y + int.Parse(Math.Round(70 / 400.0 * pr2Height).ToString()));
                    MacroController.LeftClick();
                    screen = CaptureScreen();
                    Bitmap login = new Bitmap(Resources.login, new Size(int.Parse(Math.Round(Resources.login.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.login.Height / 400.0 * pr2Height).ToString())));
                    List<Point> loginPoint = MacroController.Find(screen, login);
                    while (loginPoint.Count != 1)
                    {
                        screen = CaptureScreen();
                        loginPoint = MacroController.Find(screen, login);
                        await Task.Delay(50);
                    }
                    login.Dispose();
                    foreach (Point lPoint in loginPoint)
                    {
                        MacroController.MouseMove(lPoint.X, lPoint.Y);
                        MacroController.LeftClick();
                    }
                    Focus();
                    await SelectServer(currentServer, 1);
                    Focus();
                    screen = CaptureScreen();
                    Bitmap password = new Bitmap(Resources.password, new Size(int.Parse(Math.Round(Resources.password.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.password.Height / 400.0 * pr2Height).ToString())));
                    Bitmap connect = new Bitmap(Resources.connect, new Size(int.Parse(Math.Round(Resources.connect.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.connect.Height / 400.0 * pr2Height).ToString())));
                    List<Point> passwordPoints = MacroController.Find(screen, password);
                    List<Point> connectPoints = MacroController.Find(screen, connect);
                    while (passwordPoints.Count != 1 || connectPoints.Count != 1)
                    {
                        screen = CaptureScreen();
                        if (passwordPoints.Count != 1)
                        {
                            passwordPoints = MacroController.Find(screen, password);
                        }
                        else if (connectPoints.Count != 1)
                        {
                            connectPoints = MacroController.Find(screen, connect);
                        }
                        await Task.Delay(50);
                    }
                    password.Dispose();
                    connect.Dispose();
                    MacroController.MouseMove(passwordPoints.First().X, passwordPoints.First().Y);
                    MacroController.LeftClick();
                    int index = -1;
                    if (level1Points.Count == 4)
                    {
                        Point closestPoint = MacroController.GetClosestPoint(level1Points, point);
                        index = level1Points.IndexOf(closestPoint);
                    }
                    else if (level2Points.Count == 4)
                    {
                        Point closestPoint = MacroController.GetClosestPoint(level2Points, point);
                        index = level2Points.IndexOf(closestPoint);
                    }
                    else if (level3Points.Count == 4)
                    {
                        Point closestPoint = MacroController.GetClosestPoint(level3Points, point);
                        index = level3Points.IndexOf(closestPoint);
                    }
                    else if (level4Points.Count == 4)
                    {
                        Point closestPoint = MacroController.GetClosestPoint(level4Points, point);
                        index = level4Points.IndexOf(closestPoint);
                    }
                    else if (level5Points.Count == 4)
                    {
                        Point closestPoint = MacroController.GetClosestPoint(level5Points, point);
                        index = level5Points.IndexOf(closestPoint);
                    }
                    else if (level6Points.Count == 4)
                    {
                        Point closestPoint = MacroController.GetClosestPoint(level6Points, point);
                        index = level6Points.IndexOf(closestPoint);
                    }
                    if (index == 0)
                    {
                        SendKeys.Send(account1.Split(new char[] { '|' }, 2).Last());
                    }
                    else if (index == 1)
                    {
                        SendKeys.Send(account2.Split(new char[] { '|' }, 2).Last());
                    }
                    else if (index == 2)
                    {
                        SendKeys.Send(account3.Split(new char[] { '|' }, 2).Last());
                    }
                    else if (index == 3)
                    {
                        SendKeys.Send(account4.Split(new char[] { '|' }, 2).Last());
                    }
                    MacroController.MouseMove(connectPoints.First().X, connectPoints.First().Y);
                    MacroController.LeftClick();
                    await Task.Delay(2200);
                }
                if (inLevel)
                {
                    screen = CaptureScreen();
                    Bitmap chat = new Bitmap(Resources.chat, new Size(int.Parse(Math.Round(Resources.chat.Width / 550.0 * pr2Width).ToString()), int.Parse(Math.Round(Resources.chat.Height / 400.0 * pr2Height).ToString())));
                    points = MacroController.Find(screen, chat);
                    if (points.Count > 0)
                    {
                        foreach (Point point in points)
                        {
                            MacroController.MouseMove(point.X + int.Parse(Math.Round(360 / 550.0 * pr2Width).ToString()), point.Y);
                            MacroController.LeftClick();
                            await ReturnToLobby(1);
                        }
                    }
                    chat.Dispose();
                }
            }
            disconnected.Dispose();
        }
    }
}
