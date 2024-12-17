using PAWPMD.Models.DTOS;
using PAWPMD.Models.Enums;
using PAWPMD.Service.Services;
using PAWPMD.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Strategy
{
    public interface IWidgetStContext
    {
        Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId, WidgetType widgetType);
    }

    public class WidgetStContext : IWidgetStContext
    {
        private readonly IWidgetService _widgetService;
        private readonly IUserWidgetService _userWidgetService;
        private readonly IWidgetSettingService _widgetSettingService;
        private readonly IOpenWeatherService _openWeatherService;
        private readonly IApiNinjaService _apiNinjaService;
        private readonly IApiNewsService _apiNewsService;
        private readonly IApiPexelsService _apiPexelsService;
 
        public WidgetStContext(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService, IOpenWeatherService openWeatherService, IApiNinjaService apiNinjaService, IApiNewsService apiNewsService, IApiPexelsService apiPexelsService)
        {
            _widgetService = widgetService;
            _userWidgetService = userWidgetService;
            _widgetSettingService = widgetSettingService;
            _openWeatherService = openWeatherService;
            _apiNinjaService = apiNinjaService;
            _apiNewsService = apiNewsService;
            _apiPexelsService = apiPexelsService;
        }

        public async Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId, WidgetType widgetType)
        {
            IWidgetStrategy strategy;

            switch (widgetType)

            {
                case WidgetType.Image:
                    strategy = new ImageWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _apiPexelsService);
                    break;
                case WidgetType.News:
                    strategy = new NewsWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _apiNewsService);
                    break;

                case WidgetType.CityDetails:
                    strategy = new CityDetailsWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _apiNinjaService);
                    break;
                case WidgetType.Weather:
                    strategy = new WeatherWidgetStrategy(_widgetService, _userWidgetService, _widgetSettingService, _openWeatherService, _apiNinjaService);
                    break;
                default:
                    throw new ArgumentException("Invalid widget type");
            }
            return await strategy.SaveWidgetAsync(widgetDTO, userId, widgetId);
        }
    }
}
