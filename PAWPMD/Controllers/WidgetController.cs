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


    }
}
