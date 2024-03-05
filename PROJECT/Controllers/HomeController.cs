using Microsoft.AspNetCore.Mvc;
using PROJECT.Models;
using System.Diagnostics;

namespace PROJECT.Controllers
{
    public class HomeController : Controller
    {
        List<MCardModel> Movies = new List<MCardModel> {
                new MCardModel
                {
                    Id = "1",
                    ImgSrc = "https://sportshub.cbsistatic.com/i/2023/05/21/6feb1e78-1082-4b63-a4f4-f9fd6b917b5a/demon-slayer.jpg?auto=webp&width=849&height=1200&crop=0.708:1,smart",
                    Name = "Demon Slayer",
                    DateOfRelease = "18 August 2023",
                    Rating = "5.0",
                    VideoLink = "https://firebasestorage.googleapis.com/v0/b/fir-362d4.appspot.com/o/Hinokami%20Kagura%20Total%20Concentration%20Dancing%20Flash!%204K%2060FPS%20Demon%20Slayer%20Kimetsu%20no%20Yaiba%20%5BTubeRipper.com%5D.mp4?alt=media&token=7870e3f3-d134-4995-9a80-6b2399d21cbf",
                    Desc = "Ever since the death of his father, the burden of supporting the family has fallen upon Tanjirou Kamado's shoulders. Though living impoverished on a remote mountain, the Kamado family are able to enjoy a relatively peaceful and happy life. One day, Tanjirou decides to go down to the local village to make a little money selling charcoal. On his way back, night falls, forcing Tanjirou to take shelter in the house of a strange man, who warns him of the existence of flesh-eating demons that lurk in the woods at night. When he finally arrives back home the next day, he is met with a horrifying sight—his whole family has been slaughtered. Worse still, the sole survivor is his sister Nezuko, who has been turned into a bloodthirsty demon. Consumed by rage and hatred, Tanjirou swears to avenge his family and stay by his only remaining sibling. Alongside the mysterious group calling themselves the Demon Slayer Corps, Tanjirou will do whatever it takes to slay the demons and protect the remnants of his beloved sister's humanity.",
                    Cast = "Tanjiro, Rangoku, Nezuko, Zenitsu",
                    Director = "Muzan"
                },
                new MCardModel
                {
                    Id = "2",
                    ImgSrc = "https://m.media-amazon.com/images/M/MV5BODM0NmVjMzUtOTJhNi00N2ZhLWFkYmMtYmZmNjRiY2M1YWY4XkEyXkFqcGdeQXVyOTgxOTA5MDg@._V1_.jpg",
                    Name = "Jujutsu Kaisen 0",
                    DateOfRelease = "12 Feb 2024",
                    Rating = "4.0",
                    VideoLink = "https://firebasestorage.googleapis.com/v0/b/fir-362d4.appspot.com/o/Yuta%20vs%20Geto%204K%E2%A7%B88K%20-%20Jujutsu%20Kaisen%200%20Movie%20Bluray%20Release.webm?alt=media&token=f7eb3ae5-d664-4058-9a1a-ca54195370bd",
                    Desc = "The year is 2006, and the halls of Tokyo Prefectural Jujutsu High School echo with the endless bickering and intense debate between two inseparable best friends. Exuding unshakeable confidence, Satoru Gojou and Suguru Getou believe there is no challenge too great for young and powerful Special Grade sorcerers such as themselves. They are tasked with safely delivering a sensible girl named Riko Amanai to the entity whose existence is the very essence of the jujutsu world. However, the mission plunges them into an exhausting swirl of moral conflict that threatens to destroy the already feeble amity between sorcerers and ordinary humans. Twelve years later, students and sorcerers are the frontline defense against the rising number of high-level curses born from humans' negative emotions. As the entities grow in power, their self-awareness and ambition increase too. The curses unite for the common goal of eradicating humans and creating a world of only cursed energy users, led by a dangerous, ancient cursed spirit. To dispose of their greatest obstacle—the strongest sorcerer, Gojou—they orchestrate an attack at Shibuya Station on Halloween. Dividing into teams, the sorcerers enter the fight prepared to risk everything to protect the innocent and their own kind.",
                    Director = "",
                    Cast = "Yuta , Rika, Gojo Satoru"
                },
                new MCardModel
                {
                    Id = "3",
                    ImgSrc = "https://images-cdn.ubuy.co.in/64020c032b88b367ca7410e3-anime-one-piece-poster-the-straw-hat.jpg",
                    Name = "One Piece",
                    DateOfRelease = "21 May 2023",
                    Rating = "5.0",
                    VideoLink = "https://firebasestorage.googleapis.com/v0/b/fir-362d4.appspot.com/o/Zoro%20vs%20King%20%5B4K%E2%A7%B850fps%5D%20The%20King%20of%20Hell%20%EF%BD%9C%20One%20Piece%20Episode%201062.webm?alt=media&token=85168f71-c87a-4730-9b49-15e04090b1f5",
                    Desc = "Gold Roger was known as the Pirate King, the strongest and most infamous being to have sailed the Grand Line. The capture and execution of Roger by the World Government brought a change throughout the world. His last words before his death revealed the existence of the greatest treasure in the world, One Piece. It was this revelation that brought about the Grand Age of Pirates, men who dreamed of finding One Piece—which promises an unlimited amount of riches and fame—and quite possibly the pinnacle of glory and the title of the Pirate King. Enter Monkey Luffy, a 17-year-old boy who defies your standard definition of a pirate. Rather than the popular persona of a wicked, hardened, toothless pirate ransacking villages for fun, Luffy's reason for being a pirate is one of pure wonder: the thought of an exciting adventure that leads him to intriguing people and ultimately, the promised treasure. Following in the footsteps of his childhood hero, Luffy and his crew travel across the Grand Line, experiencing crazy adventures, unveiling dark mysteries and battling strong enemies, all in order to reach the most coveted of all fortunes—One Piece.",
                    Director = "Oda",
                    Cast = "Mayumi Tanaka, Kazuya Nakai,Akemi Okamura"
                }
            };

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {

            return View(Movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult TandC()
        {
            return View();
        }


        //Take movie info and create card 
        //taking input from admin
        public IActionResult AdminAddMovie()
        {
            return View();
        }
        //creating model of that info
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //adding that model to the Movie List
        [HttpPost]
        public IActionResult Create(MCardModel movie)
        {
            Movies.Add(movie);
            return View("Index");
        } 

        public IActionResult WatchMovie(string id)
        {
            MCardModel Movie = Movies.FirstOrDefault(m => m.Id == id);
            return View("MWatchScreen", Movie);
        }
    }
}
