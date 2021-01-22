using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Modules.Controllers;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using GenHTTP_WebServer.Models;

namespace GenHTTP_WebServer.Controllers
{
    class SystemController
    {
        private static List<Models.System> _Systems = new List<Models.System>()
        {
            new Models.System
            (
                0,
                "CGRS",
                "[Really need to remember what CGRS stands for]",
                "An integrated event-sharing SDK used to bind services together, allowing players to share data and achievements across video games.",
                new string[]{"Manage cross-platform events", "easily integratable via REST API", "Uses central auth/id"},
                "C#"
            ),
            new Models.System
            (
                1,
                "Draconic Link",
                "Central point of access and manipulation for Dracon services",
                "A single system that manages authentication, ID, player data, persistence and data repositories (e.g Item Types). By using the same system across the Dracon Interactive website, games and services, users get the most out of their usage, and are encouraged to interact and engage across the Dracon spectrum.",
                new string[]{"","",""},
                "C#, Python, SQL, F#"
            )
        };

        public IHandlerBuilder Index()
        {
            var model = new SystemModel(_Systems);

            return ModScriban.Page(Resource.FromAssembly("system_index.html"), (r, h) => new ViewModel<SystemModel>(r, h, model))
                            .Title("Systems");
        }

        public IHandlerBuilder Details([FromPath] int id)
        {
            var model = _Systems.Where(m => m.ID == id).First();

            return ModScriban.Page(Resource.FromAssembly("system.html"), (r, h) => new ViewModel<Models.System>(r, h, model))
                .Title($"{model.Name}");
        }

        public static List<(string, string)> Links()
        {
            var n = new List<(string, string)>();

            foreach (var a in _Systems)
            {
                n.Add(("./details/" + a.ID + "/", a.Name));
            }

            return n;
        }
    }
}
