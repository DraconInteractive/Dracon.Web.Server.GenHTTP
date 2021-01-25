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
    class GGJController
    {
        private static List<Game> _Games = new List<Game>()
        {
            new Game
            (
                0, 
                false,
                "Entropy",
                "First Person Physics Puzzle",
                "[Description]",
                "Released",
                "2019",
                "C# (Unity)",
                "PC",
                new string[] {"","",""},
                "http://www.google.com.au/",
                "Google",
                "",
                new List<(string,string)> { },
                "logo.png",
                new GameComponent[]{ }
            ),
            new Game
            (
                1,
                false,
                "Wreck'em Mech'em Robots",
                "Destroy Mechs in VR",
                "[Description]",
                "Released",
                "2020",
                "C# (Unity)",
                "PC",
                new string[] {"","",""},
                "",
                "Link",
                "",
                new List<(string,string)> { },
                "logo.png",
                new GameComponent[]{ }
            )
        };
    
        public IHandlerBuilder Index ()
        {
            var model = new GameModel(_Games);

            return ModScriban.Page(Resource.FromAssembly("ggj_index.html"), (r, h) => new ViewModel<GameModel>(r, h, model))
                            .Title("Global Game Jam");
        }
        /*
        public IHandlerBuilder Eternal_Index()
        {
            var model = new GameModel(_Games);

            return ModScriban.Page(Resource.FromAssembly("eternal_index.html"), (r, h) => new ViewModel<GameModel>(r, h, model))
                            .Title("The Eternal Series");
        }*/

        public IHandlerBuilder Details([FromPath] int id)
        {
            var model = _Games.Where(m => m.ID == id).First();

            return ModScriban.Page(Resource.FromAssembly("game.html"), (r, h) => new ViewModel<Game>(r, h, model))
                .Title($"{model.Name}");
        }

        public static List<(string, string)> Links()
        {
            var n = new List<(string, string)>();

            foreach (var a in _Games)
            {
                n.Add(("./details/" + a.ID + "/", a.Name));
            }

            return n;
        }
    }
}
