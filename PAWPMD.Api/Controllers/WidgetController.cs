using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers.DTOS;
using PAWPMD.Service.Services;
using PAWPMD.Service.Strategy;
using PAWPMD.Models.Enums;
namespace PAWPMD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetService _widgetService;
        private readonly IUserWidgetService _userWidgetService;
        private readonly IWidgetSettingService _widgetSettingService;
        private readonly IWidgetStContext _widgetStContext;
    
        public WidgetController(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService, IWidgetStContext widgetStContext)
        {

            _widgetService = widgetService;
            _userWidgetService = userWidgetService;
            _widgetSettingService = widgetSettingService;
            _widgetStContext = widgetStContext;
        }

        [HttpGet("all", Name = "GetAllWidgets")]
        public async Task<IActionResult> GetWidgets()
        {
            try
            {
                var widgets = await _widgetService.GetAllWidgetsAsync();
                var userWidgets = await _userWidgetService.GetAllUserWidgetsAsync();
                var widgetSettings = await _widgetSettingService.GetAllWidgetSettingsAsync();
                var widgetResponseDTOs = new List<WidgetResponseDTO>();

                var result = await WidgetDTOResponseMapper.PrepareWidgetDTOResponseDataListAsync( widgets, userWidgets, widgetSettings);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetWidget")]
        public async Task<IActionResult> GetWidget(int id)
        {
            try
            {
                var widget = await _widgetService.GetWidgetByIdAsync(id);
                if (widget == null)
                {
                    return NotFound();
                }
                int parsedWidgetId = widget.WidgetId ?? 0;
                var userWidget = await _userWidgetService.GetUserWidgetByWidgetIdAsync(parsedWidgetId);
                if (userWidget == null)
                {
                    return NotFound();
                }

                var widgetSettings = await _widgetSettingService.GetWidgetSettingByUserWidgetIdAsync(userWidget.UserWidgetId);

                var widgetResponseDTO = new WidgetResponseDTO
                {
                    Widget = new WidgetDTO(),
                    UserWidget = new UserWidgetDTO(),
                    WidgetSetting = new WidgetSettingDTO()
                };

                var result = await WidgetDTOResponseMapper.PrepareWidgetDTOReponseDataAsync(widgetResponseDTO, widget, userWidget, widgetSettings);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("saveWidget")]
        public async Task<IActionResult> SaveWidget([FromBody] WidgetRequestDTO widgetRequestDTO)
        {
            var userId = 5;
            try
            {
                switch (widgetRequestDTO.Widget.CategoryId)
                {
                    case 1:
                        var resultImage = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Image);
                        return Ok(resultImage);
                    case 2: 
                        var result = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Weather);
                        return Ok(result);
                    case 4:
                        var resultCityDetails = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.CityDetails);
                       return Ok(resultCityDetails);
                    case 5: 
                        var resultNews = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.News);
                        return Ok(resultNews);

                    default:
                        throw new ArgumentException("Invalid widget type");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdateWidget")]
        public async Task<IActionResult> UpdateWidget(int id,[FromBody] WidgetRequestDTO widgetRequestDTO)
        {
            int ? userId = null;

            try
            {
                switch (widgetRequestDTO.Widget.CategoryId)
                {
                    case 1:
                        var resultImage = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Image);
                        return Ok(resultImage);
                    case 2:
                        var result = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Weather);
                        return Ok(result);
                    case 4:
                        var resultCityDetails = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.CityDetails);
                        return Ok(resultCityDetails);
                    case 5:
                        var resultNews = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.News);
                        return Ok(resultNews);

                    default:
                        throw new ArgumentException("Invalid widget type");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteWidget")]
        public async Task<IActionResult> DeleteWidget(int id)
        {
            try
            {
                var widget = await _widgetService.GetWidgetByIdAsync(id);
                if (widget == null)
                {
                    return NotFound();
                }
                int parsedWidgetId = widget.WidgetId ?? 0;
                var userWidget = await _userWidgetService.GetUserWidgetByWidgetIdAsync(parsedWidgetId);

                //falta por terminar

                await _widgetService.DeleteWidgetAsync(widget);
                return  Ok("Widget has been eliminated");
            }
            catch (Exception ex)
            { 
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
