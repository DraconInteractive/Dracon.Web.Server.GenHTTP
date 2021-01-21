using System;
using System.Collections.Generic;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Content.Templating;

using GenHTTP.Engine;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using GenHTTP.Modules.Practices;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Websites;
using GenHTTP.Modules.Placeholders;

using GenHTTP.Themes.AdminLTE;
using System.Diagnostics;
using GenHTTP.Api.Protocol;
using System.IO;
using GenHTTP.Modules.Controllers;

using GenHTTP_WebServer.Controllers;

namespace GenHTTP_WebServer
{
    public static class GameHandler
    {
        public class Game
        {
            public string Name;
            public string Path;

            public GenHTTP.Modules.Scriban.Providers.ScribanPageProviderBuilder<PageModel> GetPage()
            {
                return ModScriban.Page(Resource.FromAssembly(Path + ".html")).Title(Name);
            }
        }

        public static Game[] all = new Game[]
        {
            new Game ()
            {
                Name = "First Aid Skills",
                Path = "fas"
            },
            new Game ()
            {
                Name = "Eternal: Embers",
                Path = "eternal-ember"
            },
            new Game ()
            {
                Name = "Aonar - Alone",
                Path = "aonar"
            },
            new Game ()
            {
                Name = "SubterraNEON",
                Path = "subterraneon"
            },
            new Game ()
            {
                Name = "Dear God",
                Path = "deargod"
            }
        };
    }
    public static class SystemHandler
    {
        public class System
        {
            public string Name;
            public string Path;

            public GenHTTP.Modules.Scriban.Providers.ScribanPageProviderBuilder<PageModel> GetPage()
            {
                return ModScriban.Page(Resource.FromAssembly(Path + ".html")).Title(Name);
            }
        }

        public static System[] all = new System[]
        {
            new System ()
            {
                Name = "CGRS",
                Path = "cgrs"
            },
            new System ()
            {
                Name = "Draconic Link",
                Path = "draconic-link"
            }
        };
    }
    class Program
    {
        public static int Main(string[] args)
        {
            bool startTunnel = true;
            if (startTunnel)
            {
                StartTunnel();
            }
            Setup();
            return 0;
        }

        public static void StartTunnel ()
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c" + "ngrok http --region=au -hostname=connect.draconai.com.au 8080");
            processInfo.UseShellExecute = true;
            processInfo.CreateNoWindow = false;
            process = Process.Start(processInfo);
        }

        private static void Setup()
        {
            var resources = new string[]
            {
                "avatar.png",
                "logo.png"
            };

            var main = Layout.Create()
                .Add("models", Controller.From<AIController>())
                .Add("games", Controller.From<GameController>())
                .Add("reports", Controller.From<ReportController>())
                .Add("user", ModScriban.Page(Resource.FromAssembly("user.html")).Title("Internal Systems"))
                .Index(ModScriban.Page(Resource.FromAssembly("index.html")).Title("Home"));
            //To be added
            //.Add("games", Controller.From<GameController>()) -- not currently working
            //.Add("systems", Controller.From<SystemController>())


            foreach (var resource in resources)
            {
                main.Add(resource, Download.From(Resource.FromAssembly(resource)));
            }

            var menu = Menu.Empty()
                    .Add("{website}", "Home")
                    .Add("/games/", "Games", GameController.Links())
                    .Add("/models/", "AI Models", AIController.Links())
                    .Add("/systems/", "Systems");

            var website = Website.Create()
                .Theme(GetAdminLTE())
                .Menu(menu)
                .Content(main);

            Host.Create()
                .Defaults()
                .Console()
                .Handler(website)
                .Run();
        }

        private static ITheme GetAdminLTE()
        {
            var menu = Menu.Empty()
                           .Add("{website}/", "Home")
                           .Add("{website}/reports", "Reports");

            var notifications = ModScriban.Template<IBaseModel>(Resource.FromAssembly("Notifications.html"))
                                          .Build();

            
            return new AdminLteBuilder().Title("Dracon Interactive")
                                        .Logo(Download.From(Resource.FromAssembly("logo.png")))
                                        .UserProfile((r, h) => new UserProfile("User A-01", "/avatar.png", "/user"))
                                        .FooterLeft((r, h) => Helpers.Version)
                                        .FooterRight((r, h) => "Peter M Carey (2021)")
                                        .Sidebar((r, h) => SideBarConstructor())
                                        .Search((r, h) => new SearchBox(""))
                                        .Header(menu)
                                        .Notifications((r, h) => notifications.RenderAsync(new ViewModel(r, h)).Result)
                                        .Build();
        }

        private static string SideBarConstructor ()
        {
            string s = "";
            s += "Networked Services: Active";
            s += "<br><br>";
            s += "Connection to AI units established.";
            s += "<br>";
            s += "<ul>";
            s += "<li>[0] C-1 | Idle |</li>";
            s += "<li>[1] C-1 | Idle |</li>";
            s += "<li>[2] C-2 | Idle |</li>";
            s += "<li>[3] .NR-1 | Responsive |</li>";
            s += "<li>[4] WS-1 | State 2 |</li>";
            s += "<li>[5] WS-1 | State 5 |</li>";
            s += "</ul>";
            s += "<br><br>";
            s += "Game Services Running. 52 users within the last hour";
            return s;
        }
    }


    public static class Helpers
    {
        public static string Version = "v0.0.5t";
    }
}
