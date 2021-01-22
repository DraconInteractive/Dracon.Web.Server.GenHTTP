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
    class Program
    {
        public static int Main(string[] args)
        {
            bool startTunnel = false;
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
                .Add("systems", Controller.From<SystemController>())
                .Add("reports", Controller.From<ReportController>())
                .Add("user", ModScriban.Page(Resource.FromAssembly("user.html")).Title("Internal Systems"))
                .Index(ModScriban.Page(Resource.FromFile("Pages/index.html")).Title("Home"));


            foreach (var resource in resources)
            {
                main.Add(resource, Download.From(Resource.FromAssembly(resource)));
            }

            var menu = Menu.Empty()
                    .Add("{website}", "Home")
                    .Add("/games/", "Games", GameController.Links())
                    .Add("/models/", "AI Models", AIController.Links())
                    .Add("/systems/", "Systems", SystemController.Links());

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
                           .Add("/reports/", "Reports");

            var notifications = ModScriban.Template<IBaseModel>(Resource.FromAssembly("Notifications.html"))
                                          .Build();

            
            return new AdminLteBuilder().Title("Dracon Interactive")
                                        .Logo(Download.From(Resource.FromAssembly("logo.png")))
                                        .UserProfile((r, h) => new UserProfile("User Page", "/avatar.png", "/user"))
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
            s += "Sidebar!";
            return s;
        }
    }


    public static class Helpers
    {
        public static string Version = "v0.0.7t";
    }
}
