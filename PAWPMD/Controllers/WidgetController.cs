using MediaDashboard.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PAWPMD.Architecture;
using PAWPMD.Architecture.Helpers;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Models.Enums;
using PAWPMD.Mvc.Models;
using PAWPMD.Mvc.ViewStrategies;
using System.Dynamic;
using System.Text.Json;


namespace PAWPMD.Mvc.Controllers
{
    public class WidgetController : Controller
    {
        private readonly IRestProvider _restProvider;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ILogger<HomeController> _logger;

        public WidgetController(IRestProvider restProvider, IOptions<AppSettings> appSettings, ILogger<HomeController> logger)
        {
            _restProvider = restProvider;
            _appSettings = appSettings;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {

            var token = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var roles =  JwtTokenHelper.GetUserRoles(token);

            if (!roles.Contains("User"))
            {
                return RedirectToAction( "Index", "Home");
            }

            var jsonResponse = await _restProvider.GetAsync($"{_appSettings.Value.WidgetApi}/all", null);
            var widgetResponse = JsonProvider.DeserializeSimple<List<WidgetResponseDTO>>(jsonResponse);

            var widgets = new List<Widget>();
            var widgetSettings = new List<WidgetSetting>();
            var weatherWidgetModels = new List<WeatherWidgetModel>();
            var cityDetailsModels = new List<CityDetailsWidgetModel>();
            var newsWidgetModels = new List<NewsWidgetModel>();
            var imageWidgetModels = new List<ImageWidgetModel>();

            foreach (var widget in widgetResponse)
            {
                widgets.Add(new Widget
                {
                    WidgetId = widget.Widget.WidgetId,
                    Name = widget.Widget.Name,
                    Description = widget.Widget.Description,
                    CategoryId = widget.Widget.CategoryId,
                    Apiendpoint = widget.Widget.Apiendpoint,
                    UserId = widget.Widget.UserId
                });

                var widgetSetting = new WidgetSetting
                {
                    UserWidgetId = widget.WidgetSetting.UserWidgetId,
                    Settings = widget.WidgetSetting.Settings,
                    WidgetSettingsId = widget.WidgetSetting.WidgetSettingsId,
                };

                widgetSettings.Add(widgetSetting);

                var categiryId = (WidgetType)widget.Widget.CategoryId;

                var strategy = WidgetViewStrategyFactory.GetStrategy(categiryId);

                if (strategy is ImageWidgetViewStrategy imageStrategy)
                {
                    var imageModel = imageStrategy.GetImageModel(widget, widgetSetting);
                    if (imageModel != null)
                    {
                        imageWidgetModels.Add(imageModel);
                    }
                }

                if (strategy is WeatherWidgetViewStrategy weatherStrategy)
                {
                    var weatherModel = weatherStrategy.GetWeatherModel(widget, widgetSetting);
                    if (weatherModel != null)
                    {
                        weatherWidgetModels.Add(weatherModel);
                    }
                }

                if (strategy is CityDetailsWidgetViewStrategy cityDetailsStrategy)
                {
                    var cityDetailsModel = cityDetailsStrategy.GetCityDetailsModel(widget, widgetSetting);
                    if (cityDetailsModel != null)
                    {
                        cityDetailsModels.Add(cityDetailsModel);
                    }
                }

                if (strategy is NewsWidgetViewStrategy widgetNewsStrategy)
                {
                    var newsModel = widgetNewsStrategy.GetNewsWidgetModel(widget, widgetSetting);
                    if (newsModel != null)
                    {
                        newsWidgetModels.Add(newsModel);
                    }
                }
            }

            var viewModel = new WidgetViewModel
            {
                Widgets = widgets,
                WidgetSettings = widgetSettings,
                WeatherWidgets = weatherWidgetModels,
                CityDetails = cityDetailsModels,
                NewsWidgets = newsWidgetModels,
                ImageWidgets = imageWidgetModels,
            };

            return View(viewModel);
        }
        public async Task<IActionResult> GetAllWidgets()
        {
            try
            {
                var jsonResponse = await _restProvider.GetAsync($"{_appSettings.Value.WidgetApi}/all", null);
                var widgets = JsonProvider.DeserializeSimple<List<Widget>>(jsonResponse);
                return PartialView("_WidgetCard", widgets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load widgets.");
                return Content("<p>Error loading widgets. Please try again later.</p>");
            }
        }

        public async Task<IActionResult> GetAllWidgetCategories()
        {
            try
            {
                var jsonResponse = await _restProvider.GetAsync($"{_appSettings.Value.WidgetCategoriesApi}/all", null);
                var widgetCategories = JsonProvider.DeserializeSimple<List<WidgetCategory>>(jsonResponse);
                return PartialView("_WidgetCategoryCard", widgetCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load widget categories.");
                return Content("<p>Error loading categories. Please try again later.</p>");
            }
        }

        public async Task<IActionResult> CreateWidget()
        {
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var roles = JwtTokenHelper.GetUserRoles(token);

            if (!roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {

                var jsonResponse = await _restProvider.GetAsync($"{_appSettings.Value.WidgetCategoriesApi}/all", null);
                var widgetCategories = JsonProvider.DeserializeSimple<List<WidgetCategory>>(jsonResponse);

                ViewBag.WidgetCategories = widgetCategories;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load widget categories.");
                return Content("<p>Error loading categories. Please try again later.</p>");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWidget([FromBody] JsonElement formModel)
        {
            try
            {
                // Extract values from JSON
                var widgetName = formModel.GetProperty("WidgetName").GetString();
                var widgetDescription = formModel.GetProperty("WidgetDescription").GetString();
                var categoryId = formModel.GetProperty("CategoryId").GetString();
                var favorite = formModel.GetProperty("favorite").GetBoolean();
                var visible = formModel.GetProperty("visible").GetBoolean();
                var widgetSetting = formModel.GetProperty("WidgetSetting").ToString();

                // Create the WidgetDTO
                var widgetDTO = new WidgetDTO
                {
                    Name = widgetName,
                    CategoryId = int.Parse(categoryId),
                    Description = widgetDescription,
                    CreatedAt = DateTime.Now,
                    RequiresApiKey = true,
                    UserId = 5,
                    Apiendpoint = "apiendpoint"
                };

                // Create the UserWidgetDTO
                var userWidgetDTO = new UserWidgetDTO
                {
                    IsFavorite = favorite,
                    IsVisible = visible
                };

                // Create the WidgetSettingDTO
                var widgetSettingDTO = new WidgetSettingDTO
                {
                    Settings = widgetSetting
                };

                // Create the request WidgetDTO
                var request = new WidgetRequestDTO
                {
                    UserWidget = userWidgetDTO,
                    Widget = widgetDTO,
                    WidgetSetting = widgetSettingDTO
                };

                // Serialize the request to JSON
                var jsonRequest = JsonConvert.SerializeObject(request);

                // Send the JSON request to the API
                var jsonResponse = await _restProvider.PostAsync($"{_appSettings.Value.WidgetApi}/saveWidget", jsonRequest);

                // If fail, return error
                if (jsonResponse == null)
                    return NotFound();

                // If success, return to index
                return RedirectToAction("Index", "Widget");
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditWidget([FromBody] JsonElement formModel)
        {
            try
            {
                var widgetId = formModel.GetProperty("WidgetId").GetString();
                var widgetName = formModel.GetProperty("WidgetName").GetString();
                var widgetDescription = formModel.GetProperty("WidgetDescription").GetString();
                var categoryId = formModel.GetProperty("CategoryId").GetString();
                var favorite = formModel.GetProperty("favorite").GetBoolean();
                var visible = formModel.GetProperty("visible").GetBoolean();
                var widgetSetting = formModel.GetProperty("WidgetSetting").ToString();
                var widgetSettingId = formModel.GetProperty("WidgetSettingId").GetString();
                var userWidgetId = formModel.GetProperty("UserWidgetId").GetString();
                var widgetDTO = new WidgetDTO
                {
                    WidgetId = int.Parse(widgetId),
                    Name = widgetName,
                    CategoryId = int.Parse(categoryId),
                    Description = widgetDescription,
                    RequiresApiKey = true,
                    UserId = 5,
                    Apiendpoint = "apiendpoint",
                };

                var userWidgetDTO = new UserWidgetDTO
                {
                    UserWidgetId = int.Parse(userWidgetId),
                    IsFavorite = favorite,
                    IsVisible = visible
                };

                var widgetSettingDTO = new WidgetSettingDTO
                {
                    WidgetSettingsId = int.Parse(widgetSettingId),
                    Settings = widgetSetting
                };

                var request = new WidgetRequestDTO
                {
                    UserWidget = userWidgetDTO,
                    Widget = widgetDTO,
                    WidgetSetting = widgetSettingDTO
                };

                var jsonRequest = JsonConvert.SerializeObject(request);

                var jsonResponse = await _restProvider.PutAsync($"{_appSettings.Value.WidgetApi}/", widgetId ,jsonRequest);

                if (jsonResponse == null)
                    return Json(new { success = false, error = "Failed to update the widget" });

                return Json(new { success = true, message = "Widget updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        private class DeleteResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public async Task<IActionResult> DeleteWidgetCategories(int id)
        {
            try
            {
                // Realizar la solicitud DELETE interactuando con el WidgetCategoriesApi
                var jsonResponse = await _restProvider.DeleteAsync($"{_appSettings.Value.WidgetCategoriesApi}/{id}", null);

                //Refenrencia mis datos de DeleteResponse
                var response = JsonProvider.DeserializeSimple<DeleteResponse>(jsonResponse);

                if (response != null && response.Success)
                {
                    _logger.LogInformation($"Widget category with ID {id} was successfully deleted.");
                    return Json(new { success = true, message = "Category deleted successfully." });
                }
                else
                {
                    _logger.LogWarning($"Failed to delete widget category with ID {id}. Service returned failure.");
                    return Json(new { success = false, message = "Failed to delete category." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting widget category with ID {id}.");
                return Json(new { success = false, message = "An error occurred while trying to delete the category. Please try again later." });
            }
        }

        public async Task<IActionResult>EditWidget(int widgetId)
        {

            var token = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("SignIn", "Auth");
            }

            var roles = JwtTokenHelper.GetUserRoles(token);

            if (!roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (widgetId == 0)
                    return NotFound();

                var jsonResponse = await _restProvider.GetAsync($"{_appSettings.Value.WidgetApi}/{widgetId}", $"{widgetId}");
                if (jsonResponse == null)
                    return NotFound();

                var  widgetResponse = JsonProvider.DeserializeSimple<WidgetResponseDTO>(jsonResponse);

                var jsonResponseCategories = await _restProvider.GetAsync($"{_appSettings.Value.WidgetCategoriesApi}/all", null);

                var widgetCategories = JsonProvider.DeserializeSimple<List<WidgetCategory>>(jsonResponseCategories);

                ViewBag.WidgetCategories = widgetCategories;

                //transfort widgetSettings to a JSON object by JObject
                var jObject = JObject.Parse(widgetResponse.WidgetSetting.Settings);
                //extract query parameters from the JSON object (querie or city)
                var query = jObject["querie"]?.ToString();
                var city = jObject["city"]?.ToString();
                // sabe that parameters in the ViewBag
                ViewBag.Query = query;
                ViewBag.City = city;
                return View(widgetResponse);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        //Edit Category name
        // Obtener el WidgetCategory para editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var jsonResponse = await _restProvider.GetAsync($"{_appSettings.Value.WidgetCategoriesApi}/{id}", $"{id}");
            if (jsonResponse == null)
                return NotFound();

            var widgetCategory = JsonProvider.DeserializeSimple<WidgetCategory>(jsonResponse);
            return View("_EditWidgetCategory", widgetCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] WidgetCategory widgetCategory)
        {
            if (widgetCategory == null || id != widgetCategory.CategoryId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var jsonResponse = await _restProvider.PutAsync($"{_appSettings.Value.WidgetCategoriesApi}/{id}", $"{id}", JsonProvider.Serialize(widgetCategory));
                    if (jsonResponse == null)
                        return NotFound();

                    var updatedWidgetCategory = JsonProvider.DeserializeSimple<WidgetCategory>(jsonResponse);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating widget category");
                    return View(widgetCategory);
                }
            }

            return View(widgetCategory);
        }
    }
}
