using ArcheryProject.Models;
using Microsoft.AspNetCore.Mvc;
using artaimusDBlib;
using System;
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
            // Retrieve the value from TempData
            bool? openModalRegisterFailed = TempData["OpenModalRegisterFailed"] as bool?;
            bool? openModalLoginFailed = TempData["OpenModalLoginFailed"] as bool?;

            // Set the ViewBag or any other mechanism to pass the value to the view
            ViewBag.OpenModalRegisterFailed = openModalRegisterFailed ?? false;
            ViewBag.OpenModalLoginFailed = openModalLoginFailed ?? false;

            return View();
        }

        public IActionResult RegisterLanding()
        {
            return View();
        }

        //public ActionResult MyModal()
        //{
        //    // Your logic here
        //    return PartialView("_MyModal");
        //}
        //public ActionResult RegisterFailedModal()
        //{

        //    return PartialView("_RegisterFailedModal");
        //}


        //Login
        ////////////////////////////////////////////////////

        [HttpPost]
        public IActionResult Login(LoginOrRegisterModel LoginData)
        {
            var usernameOrEmail = LoginData.loginNameOrMail;
            var password = LoginData.loginPassword;
            bool loginSuccess = false;
            bool isAdmin = false;

            if (usernameOrEmail != null || password != null) {
                if (usernameOrEmail.Contains('@'))
                {
                    loginSuccess = LoginSuccessByEmail(usernameOrEmail, password);
                }
                else
                {
                    loginSuccess = LoginSuccessByUsername(usernameOrEmail, password);
                }


                
                isAdmin = IsAdmin(usernameOrEmail);
            }

           
            if (loginSuccess == true) {
                PlayerModel player = GetPlayer(usernameOrEmail);
                
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
                TempData["OpenModalLoginFailed"] = true;
                return RedirectToAction("LoginOrRegister"); //"Action", "Controller"
            }
           
        }

        [HttpGet]
        public bool LoginSuccessByUsername(string? username, string? password) {
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
        public bool LoginSuccessByEmail(string? email, string? password)
        {
            bool loginSuccess = false;

            //Get Login from DB by Email
            Login? tmpDBLogin = dbCtx.Logins.Where(x => x.Email == email).FirstOrDefault();

            if (tmpDBLogin != null)
            {
                //Get Player from Login by Loginid
                Player? tmpDBPlayer = dbCtx.Players.Where(x => x.LoginsId == tmpDBLogin.Id).FirstOrDefault();
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
        public bool IsAdmin(string? usernameOrEmail)
        {
            bool isAdmin = false;
            //get Info By Username
            if (usernameOrEmail.Contains('@') == false)
            {
                //Get Player from DB by username
                Player? tmpDBPlayer = dbCtx.Players.Where(x => x.Nickname == usernameOrEmail).FirstOrDefault();

                if (tmpDBPlayer != null)
                {
                    if(tmpDBPlayer.Admin == 1)
                    {
                        isAdmin = true;
                    }
                }
            }
            
            //get info by email
            else if(usernameOrEmail.Contains('@') == true)
            {
                Login? tmpDBLogin = dbCtx.Logins.Where(x => x.Email == usernameOrEmail).FirstOrDefault();

                if (tmpDBLogin != null)
                {
                    Player? tmpDBPlayer = dbCtx.Players.Where(x => x.LoginsId == tmpDBLogin.Id).FirstOrDefault();
                    if (tmpDBPlayer.Admin == 1)
                    {
                        isAdmin = true;
                    }
                }
            }
            return isAdmin;
        }

        [HttpGet]
        public PlayerModel GetPlayer(string? usernameOrEmail)
        {
            PlayerModel player = null;
            Player? tmpDBPlayer = null;
            if (usernameOrEmail.Contains('@') == true)
            {
                //get Player over login id
                Login? tmpDBLogin = dbCtx.Logins.Where(x => x.Email == usernameOrEmail).FirstOrDefault();
                if (tmpDBLogin != null)
                {
                    tmpDBPlayer = dbCtx.Players.Where(x => x.LoginsId == tmpDBLogin.Id).FirstOrDefault();
                }
            }
            else if (usernameOrEmail.Contains('@') == false)
            {
                tmpDBPlayer = dbCtx.Players.Where(x => x.Nickname == usernameOrEmail).FirstOrDefault();
            }
           
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

        //Register
        ////////////////////////////////////////////////////

        [HttpPost]
        public IActionResult Register(LoginOrRegisterModel register)
        {
            if (registerdataIsValid(register) == true)
            {

                Player? tmpDBPlayer = null;
                tmpDBPlayer = dbCtx.Players.Where(x => x.Nickname == register.registerUsername).FirstOrDefault();

                Login? tmpDBLogin = null;
                tmpDBLogin = dbCtx.Logins.Where(x => x.Email == register.registerEmail).FirstOrDefault();
                if(tmpDBLogin != null)
                {
                    tmpDBPlayer = dbCtx.Players.Where(x => x.LoginsId == tmpDBLogin.Id).FirstOrDefault();
                }
   

                if (tmpDBPlayer == null)
                {

                    tmpDBLogin = new Login
                    {
                        Id = 0,
                        Email = register.registerEmail,
                        Password = register.registerPassword
                    };
                    dbCtx.Logins.Add(tmpDBLogin);
                    dbCtx.SaveChanges();
                    tmpDBPlayer = new Player { 
                        Id = 0,
                        FirstName = register.RegisterFirstName,
                        LastName = register.registerLastName,
                        Nickname = register.registerUsername,
                        Admin = 0,
                        LoginsId = tmpDBLogin.Id
                    };
                    dbCtx.Players.Add(tmpDBPlayer);
                    dbCtx.SaveChanges();
                }
                return RedirectToAction("RegisterLanding");
            }
            TempData["OpenModalRegisterFailed"] = true;
            return RedirectToAction("LoginOrRegister");
        }
     

        [HttpGet]
        public bool registerdataIsValid(LoginOrRegisterModel register)
        {
            bool dataIsValid = false;
            /*
             * Firstname & Lastname: no numbers
               Password = PasswordRepeat && longer than 6 char && doesnt contain @ && symbols
               username is available / unique
             */
            var firstname = register.RegisterFirstName;
            var lastname = register.registerLastName;
            var username = register.registerUsername;
            var email = register.registerEmail;
            var password = register.registerPassword;
            var passwordRepeat = register.registerRepeatPassword;

            bool passwordIsValid = false;
            //Password = PasswordRepeat && longer than 6 char && doesnt contain @ && symbols
            if (password == passwordRepeat)
            {
                if(password != null)
                {
                    if(password.Any(c => char.IsDigit(c) == true && char.IsSymbol(c) == true && c != '@') && password.Length >= 6)
                    {
                        passwordIsValid = true;
                    }    
                }
            }
            //username is available / unique
            bool usernameIsAvailable = UsernameIsAvailable(username);

            bool namesAreValid = false;
            if(firstname != null && lastname != null) {
                if (firstname.Any(char.IsDigit) == false &&
              lastname.Any(char.IsDigit) == false)
                {
                    namesAreValid = true;
                }
            }

            if (namesAreValid ==true &&
               passwordIsValid == true &&
               email.Contains('@') &&
               usernameIsAvailable == true) 
            { 
                dataIsValid = true;
            }

            return dataIsValid;

        }

        [HttpGet]
        public bool UsernameIsAvailable(string username)
        {
            bool usernameIsAvailable = false;
            if (username != null)
            {
                Player? tmpDBPlayer = dbCtx.Players.Where(x => x.Nickname == username).FirstOrDefault();

                if (tmpDBPlayer == null)
                {
                    usernameIsAvailable = true;
                }
            }
            return usernameIsAvailable;
        } 
    }
}
