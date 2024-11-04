using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PAWPMD.Architecture;
using PAWPMD.Models;
using PAWPMD.Mvc.Models;

namespace PAWPMD.Mvc.Controllers
{
    public class UserProfileController(IRestProvider restProvider, IOptions<AppSettings> appSettings) : Controller
    {
        private readonly IRestProvider _restProvider = restProvider;
        private readonly IOptions<AppSettings> _appSettings = appSettings;

        public async Task<IActionResult> Index(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _restProvider.GetAsync($"{_appSettings.Value.UserApi}/{id}", $"{id}");
            if (user == null)

            {
                return NotFound();
            }
            return View(JsonProvider.DeserializeSimple<User>(user));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _restProvider.GetAsync($"{_appSettings.Value.UserApi}/{id}", $"{id}");
            if (user == null)
            {
                return NotFound();
            }
            return View(JsonProvider.DeserializeSimple<User>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            try
            {
                var jsonResponse = await _restProvider.PutAsync($"{_appSettings.Value.UserApi}/{id}", $"{id}" ,JsonProvider.Serialize(user));
                var userResponse = JsonProvider.DeserializeSimple<User>(jsonResponse);
                if (userResponse != null)
                {
                    return RedirectToAction("Index", "UserProfile", new { id = user.UserId });
                }
                ModelState.AddModelError(string.Empty, "Update failed. Please try again.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred during update. Please check your input and try again.");
            }
            return View(user);
        } 
    }
}
