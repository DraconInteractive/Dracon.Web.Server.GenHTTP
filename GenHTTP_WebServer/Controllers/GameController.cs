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
                3,
                false,
                "First Aid Skills",
                "First Aid Training Simulator",
                "The focus of my work over the last three (3) years, First Aid Skills has taught me more than any other project, in both practical game development, and soft skills like communication, team management and leadership." +
                "<br>In 2017, St John created the Product Development team, my team. The purpose was to reinvent first aid training, and bring it into the new age. I joined this team in 2018, around 8 months after its conception. There was still plenty of work to be done, and I am still working on it to this day! " +
                "I am forever thankful for the amazing work done by my predecessor, Camille Woodthorpe, for the work she did on establishing such a solid foundation that I could grow upon." +
                "<br>Over the next year, I worked closely with my local team, a 3D Artist (Tayla James), another Developer (Ivan Ng) and our Project Manager (Jeremy Burton). I also worked with an outsourced game studio, Emerge Worlds. Collaborating with this team taught me a lot about coping with the challenges remote work can present." +
                "<br>Finally, late 2019, we released our project onto iOS and Android. Our WebGL platform was released a few months later, March 2020, to cope with the suspension of face-to-face training from COVID." +
                "<br>While we have not yet officially released our VR platform, this is due to a focus on an enterprise customer, and making sure we can provide features required as such. ",
                "Released -> In Development",
                "2019",
                "C# Unity, C++, Python",
                "Oculus Quest, iOS, Android, WebGL",
                new string[] {"Multi-Platform Support", "Linear scene flow", "6 distinct environments", "A fully voiced first aid instructor, the robot Mia"},
                "https://stjohnwa.com.au/first-aid-training/first-aid-skills",
                "Website",
                "",
                new List<(string,string)> { ("IMAGE", "mia.png"), ("VIDEO", "https://player.vimeo.com/video/349366560")},
                "mia.png",
                new GameComponent[]{ }
            ),
            new Game
            (
                4,
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
                new List<(string,string)> {("VIDEO", "https://www.youtube.com/embed/8L7eyBdHymA" )},
                "logo.png",
                new GameComponent[]
                { 
                    new GameComponent ("View", "The dissolving view shader was one of the largest education components of this project.", new List<(string, string)>(){("Shadergraph", "sgdkjhasg"), ("C# Integration", "afdsghassg")})
                }
            ),
            new Game
            (
                5,
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
                new List<(string,string)> {("VIDEO","https://www.youtube.com/embed/4KzpXrHUzFU" )},
                "logo.png",
                new GameComponent[]{ }
            ),
            new Game
            (
                6,
                false,
                "Earth and Lava",
                "Shadergraph Exposition",
                "This is a 'planet' shader that I made when shadergraph was first released. Its not the most realistic shader that I've ever made, but it was a lot of fun and I'm fond of how calming the final product was.",
                "Finished",
                "2019",
                "C#, HLSL",
                "PC",
                new string[] {"Unity LWRP", "Shadergraph", "Procedurally generated", "No models. No materials. Only code."},
                "",
                "",
                "",
               new List<(string,string)> {("VIDEO", "https://www.youtube.com/embed/29qf6VCEeps" )},
                "logo.png",
                new GameComponent[]{ }
            ), 
            new Game
            (
                7,
                false,
                "Hero Rising",
                "Simple Resource Management RPG",
                "I completed this project during my Bachelor degree, as part of a unit covering commercialisation of video games. I wanted to make a simple repetitive game loop that encouraged the player to grow and continue. I integrated In-App-Purchasing for if the player wanted to fast forward this process.<br />Needless to say, with my extra 5-ish years of experience, I would do a lot of things differently, but it was an educational project while it lasted.",
                "Finished",
                "2016",
                "C#",
                "PC",
                new string[] {"Early Unity version", "Featuring sick models from the animation class", "Who needs shadows? Apparently not me."},
                "",
                "",
                "",
                new List<(string,string)> {("VIDEO", "https://www.youtube.com/embed/29qf6VCEeps") },
                "logo.png",
                new GameComponent[]{ }
            ),
            new Game
            (
                8,
                false,
                "Draco Terrum",
                "World Building",
                "Over the years, I seem to return to world-building a lot. It's no secret that I am a more practical, mechanics based person, but narrative design is something I enjoy without being particularly skilled at it. Draco Terrum is the child of these two traits combining. A repository of my various thoughts and dreams around the Dracon universes. This has been specifically helpful regarding the Eternal series, and how the various games within it intertwine.",
                "Ongoing",
                "",
                "C#",
                "PC, WebGL",
                new string[] {"All the information", "Procedural planet shaders, including really obvious seams due to a lack of UV knowledge at the time", "Really bad UI design, that I'm totally going to refactor one day", "A procedural ... sun?"},
                "https://dracon-interactive.itch.io/draco-terrum",
                "Itch.IO",
                "",
                new List<(string,string)> {("VIDEO", "https://www.youtube.com/embed/29qf6VCEeps") },
                "logo.png",
                new GameComponent[]{ }
            )
        };
    
        public IHandlerBuilder Index ()
        {
            var model = new GameModel(_Games);

            return ModScriban.Page(Resource.FromAssembly("game_index.html"), (r, h) => new ViewModel<GameModel>(r, h, model))
                            .Title("Games");
        }

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
