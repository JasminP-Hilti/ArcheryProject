using ArcheryProject.Models;
using Microsoft.AspNetCore.Mvc;
using artaimusDBlib;
//Home
//Login

namespace ArcheryProject.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private ArtaimusContext dbCtx;

        public AccountController(ILogger<AccountController> logger, ArtaimusContext dbCtx)
        {
            _logger = logger;

            this.dbCtx = dbCtx;
        }

        public IActionResult LoginOrRegister()
        { 
            //if (ModelState.IsValid)
            //{
            //    School? tmpDBSchool = dbCtx.Schools.Where(x => x.Id == school.ID).FirstOrDefault();

            //    if (tmpDBSchool == null)
            //    {
            //        tmpDBSchool = new School { Id = school.ID.Value };
            //        dbCtx.Schools.Add(tmpDBSchool);
            //    }

            //    tmpDBSchool.Name = school.Name;
            //    dbCtx.SaveChanges();
            //}
            return View();

        }


        [HttpPost]
        public IActionResult Login(LoginOrRegisterModel LoginData)
        {
            var username = LoginData.loginName;
            var password = LoginData.loginPassword;
            bool loginSuccess = false;
            bool isAdmin = false;

            if (username != null || password != null) {
                loginSuccess = LoginSuccess(username, password);
                isAdmin = IsAdmin(username);
            }

           
            if (loginSuccess== true) {
                PlayerModel player = GetPlayer(username);
                if(isAdmin == true)
                {
                    return RedirectToAction("Index", "Admin", player); //"Action", "Controller"
                }
                else
                {
                    return RedirectToAction("Index", "Player", player); //"Action", "Controller"
                }
                
            }
            else
            {
                return RedirectToAction("LoginOrRegister"); //"Action", "Controller"
            }
           
        }

        [HttpGet]
        public bool LoginSuccess(string? username, string? password) {
            bool loginSuccess = false;

                //Get Player from DB by username
                Player? tmpDBPlayer = dbCtx.Players.Where(x => x.Nickname == username).FirstOrDefault();

                if (tmpDBPlayer != null)
                {
                    //Get Logindata from Player by Loginid
                    Login? tmpDBLogin = dbCtx.Logins.Where(x => x.Id == tmpDBPlayer.LoginsId).FirstOrDefault();
                    if (tmpDBLogin != null)
                    {
                        //check if Password is correct
                        if (tmpDBLogin.Password == password)
                        {
                            // Passwords match => login successful
                            loginSuccess = true;
                        }
                    }
                }
            
            return loginSuccess;
        }

        [HttpGet]
        public bool IsAdmin(string? username)
        {
            bool isAdmin = false;
                //Get Player from DB by username
                Player? tmpDBPlayer = dbCtx.Players.Where(x => x.Nickname == username).FirstOrDefault();

                if (tmpDBPlayer != null)
                {
                    if(tmpDBPlayer.Admin == 1)
                    {
                        isAdmin = true;
                    }
                }
            return isAdmin;
        }

        [HttpGet]
        public PlayerModel GetPlayer(string? username)
        {
            PlayerModel player = null;
            Player? tmpDBPlayer = dbCtx.Players.Where(x => x.Nickname == username).FirstOrDefault();
            if (tmpDBPlayer != null)
            {
                player = new PlayerModel
                {
                    Id = tmpDBPlayer.Id,
                    FirstName = tmpDBPlayer.FirstName, 
                    LastName = tmpDBPlayer.LastName,
                    Nickname = tmpDBPlayer.Nickname,
                    Admin = tmpDBPlayer.Admin,
                    LoginsId = tmpDBPlayer.LoginsId
                };

            }
           
            return player;
        }


        [HttpPost]
        public IActionResult Register()
        {
            return View(new LoginOrRegisterModel());
        }


    }
  


    
}
