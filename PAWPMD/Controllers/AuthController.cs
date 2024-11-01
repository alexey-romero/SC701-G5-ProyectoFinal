
using MediaDashboard.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PAWPMD.Architecture;
using PAWPMD.Architecture.Helpers;
using PAWPMD.Models;
using PAWPMD.Models.Models;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.Controllers
{
    public class AuthController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {
        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;


        public IActionResult SignUp()
        {
            var model = new RegisterRequest();
            return View (model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }

            try
            {
                var jsonResponse = await restProvider.PostAsync($"{appSettings.Value.SignUpApi}",
                    JsonProvider.Serialize(registerRequest));

                var registerResponse = JsonProvider.DeserializeSimple<User>(jsonResponse);

                if (registerResponse != null)
                {
                    return RedirectToAction("SignIn", "Auth"); 
                }

                ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred during registration. Please check your input and try again.");
            }

            return View(registerRequest);
        }

        public IActionResult SignIn()
        {
            var model = new LoginRequest();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            try
            {
                var jsonResponse = await restProvider.PostAsync($"{appSettings.Value.LoginApi}",
                    JsonProvider.Serialize(loginRequest));

                var loginResponse = JsonProvider.DeserializeSimple<UserDTO>(jsonResponse);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    HttpContext.Response.Cookies.Append("AuthToken", loginResponse.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTimeOffset.UtcNow.AddHours(1)
                        });

                    var roles = JwtTokenHelper.GetUserRoles(loginResponse.Token);
                    var userId = JwtTokenHelper.GetUserId(loginResponse.Token);

                    HttpContext.Session.SetString("Id", userId.ToString());
                    HttpContext.Session.SetString("UserRoles", string.Join(",", roles));
                    HttpContext.Session.SetString("UserName", loginResponse.Name);
                    HttpContext.Session.SetString("UserEmail", loginResponse.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            catch (HttpRequestException ex)
            {
               
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError(string.Empty,
                        "Invalid username or password. Please check your credentials.");
                }
                
                else
                {
                    ModelState.AddModelError(string.Empty,
                        "Unable to connect to the login service. Please try again later.");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty,
                    "An unexpected error occurred. Please try again later.");
            }

            return View(loginRequest);
        }

     
        public  IActionResult Logout()
        {
            
            HttpContext.Response.Cookies.Delete("AuthToken");

          
            HttpContext.Session.Clear();

            return RedirectToAction("SignIn", "Auth");
        }
    }
}
