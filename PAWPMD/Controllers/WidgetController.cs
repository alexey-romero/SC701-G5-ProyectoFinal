using MediaDashboard.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PAWPMD.Architecture;
using PAWPMD.Models;
using PAWPMD.Mvc.Models;


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

        public IActionResult Index()
        {
            return View();
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



    }
}
