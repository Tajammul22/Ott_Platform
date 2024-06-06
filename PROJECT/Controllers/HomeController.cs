using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PROJECT.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Firebase.Storage;
using System.Xml.Linq;
using Firebase.Auth;
using NuGet.Common;

namespace PROJECT.Controllers
{
    public class HomeController : Controller
    {
        List<MCardModel> Movies = new List<MCardModel> {
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
                    Cast = "Mayumi Tanaka, Kazuya Nakai,Akemi Okamura",
                    Genre = "Action"
                }
            };

        List<SCardModel> Series = new List<SCardModel> {
                 new SCardModel{
                    Id = 2,
                    Name = "The Family Man",
                    ImgSrc  = "https://firebasestorage.googleapis.com/v0/b/storage1-452d7.appspot.com/o/familyman.jpg?alt=media&token=a75b041f-dbdd-4c9c-baf8-b2e522a4ae77",
                    Director = "me again",
                    Genre =     "Advanture,Comady",
                    Cast = "sanji",
                    Desc = "Shanks Is Good Or Bad",
                    Episodes = new List<Episode>
                            {
                                new Episode { Id = 1, EpisodeName = "Episode 1", VideoSource = "https://firebasestorage.googleapis.com/v0/b/fir-362d4.appspot.com/o/ep1.mp4?alt=media&token=b7fc508a-4691-4fa4-b164-ad0139f95dde" },
                                new Episode { Id = 2, EpisodeName = "Episode 2", VideoSource = "https://firebasestorage.googleapis.com/v0/b/fir-362d4.appspot.com/o/ep2.mp4?alt=media&token=a92ef8f2-8bd6-4b3d-a77f-5cd829276869" }
                            }
                    },
                 new SCardModel{
                    Id = 3,
                    Name = "Richer",
                    ImgSrc  = "https://firebasestorage.googleapis.com/v0/b/storage1-452d7.appspot.com/o/richer.jpg?alt=media&token=0c4065a2-074d-4d0d-a63e-00f2912aaae2",
                    Director = "me again for third Time",
                    Genre = "Action,Drama",
                    Cast = "Robin",
                    Desc = "Shanks Is Good Or Bad",
                    Episodes = new List<Episode>
                            {
                                new Episode { Id = 1, EpisodeName = "Episode 1", VideoSource = "" },
                                new Episode { Id = 2, EpisodeName = "Episode 2", VideoSource = "" }
                            }
                    }
         };
        List<Models.Query> Querys = new List<Models.Query>();
        private readonly ILogger<HomeController> _logger;
        FirebaseAuthProvider auth;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
                _env = env;
                string authSecret = "AIzaSyCDmMGUWzT-thkcfxevhQk4tnpJinvoeh4";
                string basePath = "https://fir-362d4-default-rtdb.firebaseio.com";
                auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig("AIzaSyCDmMGUWzT-thkcfxevhQk4tnpJinvoeh4"));
                IFirebaseClient client;
                IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
                {
                    AuthSecret = authSecret,
                    BasePath = basePath,
                };

                client = new FireSharp.FirebaseClient(config);

                if (client != null && !string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
                {
                    FirebaseResponse response = client.Get("Movies/");
                    if (response != null && response.Body != "null")
                    {
                    Dictionary<string, MCardModel> Movie = response.ResultAs<Dictionary<string, MCardModel>>();

                        // Iterate through the dictionary and add users to the list
                        foreach (var i in Movie)
                        {
                            Movies.Add(i.Value);
                        }
                    }
                }

                if (client != null && !string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
                {
                    FirebaseResponse response = client.Get("Series/");
                    if (response != null && response.Body != "null")
                    {
                        Dictionary<string, SCardModel> series = response.ResultAs<Dictionary<string, SCardModel>>();
                        foreach (var i in series){
                            List<Episode> newepList = new List<Episode>();
                            List<Episode> epList = i.Value.Episodes;
                            foreach(Episode ep in epList){
                                if(ep != null){
                                    newepList.Add(ep);
                                }
                            }
                            i.Value.Episodes = newepList;
                            Series.Add(i.Value);
                        }
                    }

                }

                if (client != null && !string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(authSecret))
                {
                    FirebaseResponse response = client.Get("Querys/");
                    if (response != null && response.Body != "null")
                    {
                        Dictionary<string, Models.Query> querys = response.ResultAs<Dictionary<string, Models.Query>>();
                        foreach (var i in querys)
                        {
                            Querys.Add(i.Value);
                        }
                    }
                }
                _logger = logger;

        }     
        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "Uc3ZsYldvZy4c2cE7vr371TYGHbzAtLBSQj7FFD1",
            BasePath = "https://fir-362d4-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public IActionResult Index()
        {
            CombinedViewModel combinedViewModel = new CombinedViewModel();
            combinedViewModel.MCardModels = Movies;
            combinedViewModel.SCardModels = Series;
            return View(combinedViewModel);
        }

        //series operation
        public Episode GetEpisode(SCardModel obj,int id)
        {
            try
            {
                foreach (Episode i in obj.Episodes)
                {
                    if (i.Id == id && i !=null)
                    {
                        return i;
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return obj.Episodes[0];
        }
        public IActionResult WatchSeries(int id = 1)
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                SCardModel Ser = Series.FirstOrDefault(m => m.Id == id);
                MyViewModel model = new MyViewModel();
                model.Series = Ser;

                model.Episode = GetEpisode(Ser, id);
                return View("SWatchScreen", model);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public IActionResult ChangeEpisode(int seriesId, int episodeId)
        {
            SCardModel Ser = Series.FirstOrDefault(m => m.Id == seriesId);
            MyViewModel model = new MyViewModel();
            model.Series = Ser;
            model.Episode = GetEpisode(Ser, episodeId);
            return View("SWatchScreen", model);
        }
        //end of series operation

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult TandC()
        {
            return View();
        }

        public IActionResult WatchMovie(string id = "1")
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (token != null)
            {
                MCardModel Movie = Movies.FirstOrDefault(m => m.Id == id);
                return View("MWatchScreen", Movie);
            }
            else
            {
                return RedirectToAction("SignIn");
            }
           
        }

        ////New Movie Add to Cloud 
        //Take movie info and create card 
        //taking input from admin

        //admin section
        public IActionResult Admin()
        {
            if(HttpContext.Session.GetString("Admin")!= null)
            {
                return View("Admin");
            }
            return View("Index");
        }
        public IActionResult AdminAddMovie()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MCardModel obj,IFormFile myImg,IFormFile myVideo)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                obj.ImgSrc = await UploadImage(myImg, 0);
                obj.VideoLink = await UploadImage(myVideo, 1);
                var data = obj;
                PushResponse response = client.Push("Movies/", data);
            }
            catch (NullReferenceException ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View("Admin");
        }

        //Upload file to Firebase Storage // Very IMP
        private readonly IWebHostEnvironment _env;
        //firebase storage
        [HttpPost]
        public async Task<string> UploadImage(IFormFile imageFile, int no)
        {
            string url = "--none--";
            if (imageFile != null && imageFile.Length > 0)
            {
                Debug.WriteLine("working--------------------->");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // Upload the image to Firebase Storage
                    var imageUrl = await UploadImageToFirebase(fileName, filePath,no);
                    url = Convert.ToString(imageUrl);
                    ViewBag.ImageUrl = imageUrl;

                    // Delayed clean up: delete the local file after a short delay
                    Task.Delay(1000).ContinueWith(t =>
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    });
                    return Convert.ToString(imageUrl);
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    // For demonstration purposes, printing the exception message
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                return url;
            }
            else
            {
                Debug.Write(url);
                Debug.WriteLine("Not Working ======================>");
            }

            ViewBag.ImageUrl = null;
            return url;
        }


        private async Task<string> UploadImageToFirebase(string fileName, string filePath,int no)
        {
            var stream = new FileStream(filePath, FileMode.Open);

            var auth = "Uc3ZsYldvZy4c2cE7vr371TYGHbzAtLBSQj7FFD1";
            var bucket = "fir-362d4.appspot.com";
            var storage = new FirebaseStorage(bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(auth)
            });
            if(no == 0)
            {
                var imageUrl = await storage.Child("Movies").Child(fileName).PutAsync(stream);
                return imageUrl;
            } else
            {
                var imageUrl = await storage.Child("Series").Child(fileName).PutAsync(stream);
                return imageUrl;
            }

           
        }

        //end of admin section
        public SCardModel GetSeriesById(int id)
        {
            // Assuming you have a data context or repository to interact with the database
            // This is just a placeholder implementation, replace it with your actual data access logic
            var series = Series.FirstOrDefault(s => s.Id == id);
            return series;
        }

        public IActionResult Advance()
        {
            return View();
        }

        public IActionResult AllMovies()
        {
            return View("Movies",Movies);
        }

        public IActionResult AllSeries()
        {
            return View("Series",Series);
        }

        public ActionResult MSort(string genre)
        {
            ViewBag.Query = genre;
            List<MCardModel> temp = Movies.Where(movie => movie.Genre.Contains(genre)).ToList();

            return View("Movies", temp);
        }

        public ActionResult MSearch(string query)
        {
            ViewBag.Query = query;
            try
            {
                List<MCardModel> temp = Movies.Where(movie => movie.Name.ToLower().Contains(query.ToLower())).ToList();
                return View("Movies", temp);
            } catch (Exception ex){
                Console.Write(ex.ToString());
                return View("Movies");
            }
            
        }

        public ActionResult SSort(string genre)
        {
            ViewBag.Query = genre;
            List<SCardModel> temp = Series.Where(series => series.Genre.Contains(genre)).ToList();
            return View("Series", temp);
        }

        public ActionResult SSearch(string query)
        {
            ViewBag.Query = query;
            List<SCardModel> temp = Series.Where(series => series.Name.ToLower().Contains(query.ToLower())).ToList();
            return View("Series", temp);
        }

        //Prototype Search{

        //public actionresult msearch(string query)
        //{
        //    viewbag.query = query;
        //    list<mcardmodel> temp = new list<mcardmodel>();
        //    foreach (mcardmodel i in movies)
        //    {
        //        if (i.name.tolower().contains(query.tolower()))
        //        {
        //            temp.add(i);
        //        }
        //    }
        //    return view("movies", temp);
        //}

        //}

        //Prototype Sort{

        //public ActionResult MSort(string genre)
        //{
        //    ViewBag.Query = genre;
        //    List<MCardModel> temp = new List<MCardModel>();
        //    foreach (var i in Movies)
        //    {
        //        String[] Genres = i.Genre.Split(',');
        //        foreach (var g in Genres)
        //        {
        //            if (g.ToLower() == genre.ToLower())
        //            {
        //                temp.Add(i);
        //            }
        //        }
        //    }

        //    return View("Movies", temp);
        //}

        //}

        //admin section
        //Series Upload
        public IActionResult Add()
        {
            return View("Tryle");
        }

        //still hava error
        [HttpPost]
        public async Task<IActionResult> CreateSeries(SCardModel obj,int EpId,string Epname,IFormFile imgsrc,IFormFile vidsrc)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                var data = obj;
                string imglink = await UploadImage(imgsrc,1);
                string vidlink = await UploadImage(vidsrc, 1);
                data.Episodes = new List<Episode>()
                {
                     new Episode { Id = EpId, EpisodeName = Epname, VideoSource = vidlink }
                };
                data.ImgSrc = imglink;
                PushResponse response = client.Push("Series/", data);
            } catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View("Admin");
        } 

        public async Task<IActionResult> AddEpisodes()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = await client.GetAsync("Series/");
                var series = response.ResultAs<Dictionary<string, SCardModel>>();
                return View("AddEpisodes", series);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
            return View("AddEpisodes",Series);
        }

        [HttpPost]
        public async Task<IActionResult> addEp(string seriesId, int EpId, string EpName, IFormFile EpVideo)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = await client.GetAsync("Series/"+seriesId+"/Episodes");
            List<Episode> Eplist = response.ResultAs<List<Episode>>();
            if (EpVideo != null) {
                client = new FireSharp.FirebaseClient(config);
                string vidlink = await UploadImage(EpVideo, 1);
                if (EpVideo == null)
                {
                    Debug.Write("File not Found ++++++++++++++++++++++++>");
                }
                Debug.WriteLine(seriesId + "=========================>");
                Episode temp = new Episode()
                {
                    Id = EpId,
                    EpisodeName = EpName,
                    VideoSource = vidlink,
                };
                var data = temp;
                await client.SetAsync("Series/" + seriesId + "/Episodes/" + Eplist.Count(), data);
            }
            return View("Admin");
        }

        //Movie Delete by Admin
        public async Task<IActionResult> AdminDelete()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = await client.GetAsync("Movies/");
                var movies = response.ResultAs<Dictionary<string, MCardModel>>();
                return View("DeleteMovie", movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> MDelete(string movieId)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = await client.DeleteAsync("Movies/" + movieId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)//OK -> notFound (debugging)
                {
                    return View("Admin");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "Failed to delete movie" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
        }

        public async Task<IActionResult> DeleteEpisodes()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = await client.GetAsync("Series/");
                var series = response.ResultAs<Dictionary<string, SCardModel>>();
                return View("DeleteEp", series);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
        }
        public async Task<IActionResult> DeleteEpisode(string seriesId)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = await client.GetAsync("Series/"+seriesId+"/Episodes");
                Debug.WriteLine("Series/" + seriesId + "/Episodes============?");
                var Episodes = response.ResultAs<List<Episode>>();
                ViewBag.SeriesId = seriesId;
                return View("DelEp", Episodes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
        }

        public async Task<IActionResult> DelEpi(string SerId,int EpId)
        {
            try
            {
                Debug.WriteLine("Series/" + SerId + "/Episodes/" + EpId + "...............................>");
               if(EpId != 0)
                {
                    client = new FireSharp.FirebaseClient(config);
                    FirebaseResponse response = await client.DeleteAsync("Series/" + SerId + "/Episodes/" + EpId);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return View("Admin");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "Failed to delete movie" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
            Debug.WriteLine("Series/" + SerId + "/Episodes/" + EpId + "...............................>");
            return View("Admin");
        }
        public async Task<IActionResult> AdminDeleteSeries()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = await client.GetAsync("Series/");
                var series = response.ResultAs<Dictionary<string, SCardModel>>();
                return View("DeleteSeries", series);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SDelete(string seriesId)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse response = await client.DeleteAsync("Series/" + seriesId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return View("Admin");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "Failed to delete movie" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = ex.Message });
            }
        }
        //sending mail
        [HttpPost]
		public ActionResult SendEmail(string mail,string message)
		{
			try
			{
				if (ModelState.IsValid)
				{
                    string subject = "Query Registraion";
                    var senderEmail = new MailAddress("natalijay2515@gmail.com", "JKT Movie Streeming");
					var receiverEmail = new MailAddress(mail, "Receiver");
					var password = "yqdglmhlmnqrcghw";
					var sub = subject;
					var body = "Your Query has been Submited Successfuly.\nWe will Proceed your Qeury and Answer you Shortly\n\nReview Your Query : \n" + message;
					var smtp = new SmtpClient
					{
						Host = "smtp.gmail.com",
						Port = 587,
						EnableSsl = true,
						DeliveryMethod = SmtpDeliveryMethod.Network,
						UseDefaultCredentials = false,
						Credentials = new NetworkCredential(senderEmail.Address, password)
					};
					using (var mess = new MailMessage(senderEmail, receiverEmail)
					{
						Subject = subject,
						Body = body
					})
					{
						smtp.Send(mess);
					}
                    try
                    {
                        //adding query to our realtime database
                        client = new FireSharp.FirebaseClient(config);
                        Models.Query data = new Models.Query();
                        data.email = mail;
                        data.message = message;
                        PushResponse response = client.Push("Querys/", data);
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    ViewBag.EmailSent = true;
					return View("Help");
				}
			}
			catch (Exception e)
			{
				ViewBag.Error = "Some Error";
                Debug.WriteLine(e.Message + "-----------------<>");
			}
			return View("Help");
		}

        public IActionResult WatchQuery()
        {
            return View("Querys",Querys);
        }

        public IActionResult Tryle(MCardModel obj,IFormFile myImg,IFormFile myVideo)
        {
            if(obj != null)
            {
                Debug.WriteLine(obj.Name + " Working ===============================================>");
            } else
            {
                Debug.WriteLine("Not Working ==============================>");
            }
            if(myImg != null)
            {
                Debug.WriteLine("Image Working ===============================================>");
            }
            else
            {
                Debug.WriteLine("Not Working ==============================>");
            }
            if(myVideo != null)
            {
                Debug.WriteLine("Video Working ===============================================>");
            }
            else
            {
                Debug.WriteLine("Video Not Working ==============================>");
            }
            Debug.WriteLine(obj + "===============================================>");
            return View("Admin");
        }

        //Authentication Start
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(LoginModel loginModel)
        {
            try
            {
                //create the user
                await auth.CreateUserWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                //log in the new user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();

        }


        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel loginModel)
        {
            try
            {
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);
                    if(loginModel.Email == "admin@gmail.com")
                    {
                        HttpContext.Session.SetString("Admin", token);
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("SignIn");
        }

    }
}
