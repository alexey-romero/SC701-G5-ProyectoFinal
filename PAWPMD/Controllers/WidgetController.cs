using MediaDashboard.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PAWPMD.Architecture;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Mvc.Models;
using PAWPMD.Mvc.ViewStrategies;
using System.Dynamic;


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
            var jsonResponse = await _restProvider.GetAsync($"{_appSettings.Value.WidgetApi}/all", null);
            var widgetResponse = JsonProvider.DeserializeSimple<List<WidgetResponseDTO>>(jsonResponse);

            var widgets = new List<Widget>();
            var widgetSettings = new List<WidgetSetting>();
            var weatherWidgetModels = new List<WeatherWidgetModel>();

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

             
                var strategy = WidgetViewStrategyFactory.GetStrategy(widget.Widget.CategoryId);
                if (strategy is WeatherWidgetViewStrategy weatherStrategy)
                {
                    var weatherModel = weatherStrategy.GetWeatherModel(widget, widgetSetting);
                    if (weatherModel != null)
                    {
                        weatherWidgetModels.Add(weatherModel);
                    }
                }



            }

            var viewModel = new WidgetViewModel
            {
                Widgets = widgets,
                WidgetSettings = widgetSettings,
                WeatherWidgets = weatherWidgetModels
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
