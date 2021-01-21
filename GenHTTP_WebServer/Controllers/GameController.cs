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
    class GameController
    {
        private static List<Game> _Games = new List<Game>()
        {
            new Game
            (
                0,
                true,
                "Eternal: Ember",
                "Fantasy RPG",
                "The first of the Eternal series, Ember represents the culmination of my game development knowledge. Continually being reworked, not even I know when it will be finished, but I continually return to its development as my ultimate passion project.",
                "In Development",
                "Unknown",
                "C# (Unity)",
                "PC - Windows",
                new string[] {"Third Person", "Deep and immersive story", "'Eternal Network' integration"},
                "http://www.draconinteractive.com.au/eternal-ember/",
                "Website",
                "",
                ""
            ),
            new Game
            (
                1,
                true,
                "Eternal: Conflict",
                "Scifi FPS",
                "Set a millenium after the events of Eternal: Ember, Conflict is an intense shooter that explores the evolution of the grand themes of Ember; Politics, religion, art and society being but a few.",
                "In Development",
                "Unknown",
                "C# (Unity)",
                "PC - Windows, XBox Series X",
                new string [] {"First Person scifi combat", "A variety of weapons and character enhancements", "'Eternal Network' integration"},
                "http://www.draconinteractive.com.au/eternal-conflict/",
                "Website",
                "",
                ""
            ),
            new Game
            (
                2,
                false,
                "First Aid Skills",
                "VR Training Simulator",
                "[Description]",
                "Released -> In Development",
                "2019",
                "C# Unity, C++, Python",
                "Oculus Quest, iOS, Android, WebGL",
                new string[] {},
                "http://www.stjohnwa.com.au/my-account/first-aid-skills-web",
                "Website",
                "",
                ""
            ),
            new Game
            (
                3,
                false,
                "Aonar - Alone",
                "Ethereal Experience",
                "Originally developed as part of my Master of Creative Industries, Aonar is a game that explores the nature of distance, in terms of my own personal progression through my life. The game consists of a set of environments, each symbolic to a time period.<br />I experimented a lot with shaders designed to allow me to make large levels with limited visibilty but okay framerate. I could definitely do better now days, but I am pretty happy with this as a first attempt. ",
                "Released",
                "October 2019",
                "C# (Unity)",
                "PC - Windows, Android (14+)",
                new string[] {"A set of visually distinct environments", "Head bob!", "Small puzzles (because why not)"},
                "http://www.draconinteractive.com.au/aonar",
                "Website",
                "",
                "https://www.youtube.com/embed/8L7eyBdHymA"
            ),
            new Game
            (
                4,
                false,
                "Dear God",
                "VR God Puzzle",
                "[Description]",
                "In Development",
                "Unknown",
                "C# Unity",
                "Oculus Quest 1/2",
                new string[] {"Intuitive diagetic UI", "Hand controlled interaction", "Immersive story"},
                "http://www.draconinteractive.com.au/dear-god",
                "Website",
                "",
                "https://www.youtube.com/embed/4KzpXrHUzFU"
            )
        };
    
        public IHandlerBuilder Index ()
        {
            var model = new GameModel(_Games);

            return ModScriban.Page(Resource.FromAssembly("game_index.html"), (r, h) => new ViewModel<GameModel>(r, h, model))
                            .Title("Games");
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
                if (a.Eternal)
                {
                    n.Add(("./details/" + a.ID + "/", a.Name));//Want to add a sub type (e.g /eternal/) in the future
                } 
                else
                {
                    n.Add(("./details/" + a.ID + "/", a.Name));
                }
                
            }

            return n;
        }
    }
}
