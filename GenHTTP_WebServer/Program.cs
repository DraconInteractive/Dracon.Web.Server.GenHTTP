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

namespace GenHTTP_WebServer
{
    public static class AIHandler
    {
        public class AI
        {
            public string Name;
            public string Description;
            public string Path;

            public GenHTTP.Modules.Scriban.Providers.ScribanPageProviderBuilder<PageModel> GetPage ()
            {
                return ModScriban.Page(Resource.FromAssembly(Path + ".html")).Title(Name);
            }
        }

        public static AI[] all = new AI[]
        {
            new AI ()
            {
                Name = "C-1",
                Description = "Contextual AI - 1",
                Path = "c1"
            },
            new AI ()
            {
                Name = "C-2",
                Description = "Contextual AI - 2",
                Path = "c2"
            },
            new AI ()
            {
                Name = ".NR-1",
                Description = ".Net / Rasa Hybrid Model - 1",
                Path = "nr1"
            },
            new AI ()
            {
                Name = "WS-1",
                Description = "Web Server Model - 1",
                Path = "ws1"
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
            return Host.Create()
                .Defaults()
                .Console()
                .Handler(Setup())
                .Run();
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
        private static IHandlerBuilder Setup()
        {
            var resources = new string[]
            {
                "avatar.png",
                "logo.png"
            };

            var index = ModScriban.Page(Resource.FromAssembly("index.html")).Title("Home");

            var userPage = ModScriban.Page(Resource.FromAssembly("user.html")).Title("Internal Systems");

            var modelPage = Page.From("Content", String.Format("Models. We current are hosting {0} AI's", AIHandler.all.Length))
                .Description("Content Page");

            var reportsPage = Page.From("Reports", "0 New Reports")
                .Description("Reports Page");

            var models = Layout.Create()
                .Index(modelPage);

            foreach (var ai in AIHandler.all)
            {
                models.Add(ai.Path, ai.GetPage());
            }

            var root = Layout.Create()
                .Add("models", models)
                .Add("reports", reportsPage)
                .Add("user", userPage)
                .Index(index);

            foreach (var resource in resources)
            {
                root.Add(resource, Download.From(Resource.FromAssembly(resource)));
            }

            var modelLinks = new List<(string, string)>();
            foreach (var ai in AIHandler.all)
            {
                modelLinks.Add((ai.Path + "/", ai.Name));
            }
            var menu = Menu.Empty()
                    .Add("{website}", "Home")
                    .Add("models/", "Models", modelLinks);

            var website = Website.Create()
                .Theme(GetAdminLTE())
                .Menu(menu)
                .Content(root);

            return website;
        }

        private static ITheme GetAdminLTE()
        {
            var menu = Menu.Empty()
                           .Add("{website}/", "Home");

            var notifications = ModScriban.Template<IBaseModel>(Resource.FromAssembly("Notifications.html"))
                                          .Build();

            return new AdminLteBuilder().Title("Dracon AI")
                                        .Logo(Download.From(Resource.FromAssembly("logo.png")))
                                        .UserProfile((r, h) => new UserProfile("Dracon-A", "/avatar.png", "/user"))
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
        public static string Version = "v0.0.3t";
    }
}
