using GenHTTP.Api.Content.Templating;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenHTTP_WebServer.Models;
using GenHTTP.Api.Content;
using GenHTTP.Modules.Controllers;


namespace GenHTTP_WebServer.Controllers
{
    public class AIController
    {
        private static List<AI> _AIs = new List<AI>()
        {
            new AI
            (
                0,
                "C-1",
                "c1",
                "Contextual Model C-1",
                "Intelligence utilising pre-scripted commands for action and response feedback.",
                "Idle",
                "C#",
                new string[] {"It works", "Maybe"},
                "http://www.google.com.au",
                "Google",
                ""
                ),
            new AI
            (
                1,
                "C-2",
                "c2",
                "Contextual Model C-2",
                "Intelligence utilising pre-scripted commands for action and response feedback.",
                "Idle",
                "C#",
                new string[] {"It works", "Maybe"},
                "http://www.google.com.au",
                "Google",
                ""
            ),
            new AI
            (
                2,
                ".NR-1",
                "nr1",
                "Hybrid .Net + RASA ML Agent",
                "description",
                "Halted",
                "C#, Python",
                new string[] {"It works", "Maybe"},
                "http://www.google.com.au",
                "Google",
                ""
            ),
            new AI
            (
                3,
                "MBF-1",
                "mbf1",
                "Microsoft Bot Framework - Medical",
                "description",
                "In Development",
                "C#",
                new string[] {"It works", "Maybe"},
                "http://www.google.com.au",
                "Google",
                ""
            )
        };

        public IHandlerBuilder Index ()
        {
            var model = new AIModel(_AIs);

            return ModScriban.Page(Resource.FromAssembly("ai_index.html"), (r, h) => new ViewModel<AIModel>(r, h, model))
                .Title("Models");
        }

        public IHandlerBuilder Details([FromPath] int id)
        {
            var model = _AIs.Where(m => m.ID == id).First();

            return ModScriban.Page(Resource.FromAssembly("ai.html"), (r, h) => new ViewModel<AI>(r, h, model))
                .Title($"Model {model.Name}");
        }

        public static List<(string,string)> Links ()
        {
            var n = new List<(string, string)>();

            foreach (var a in _AIs)
            {
                n.Add(("./details/" + a.ID, a.Name));
            }

            return n;
        }
    }
}
