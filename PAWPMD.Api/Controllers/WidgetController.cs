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



        /// <summary>
        /// Retrieves all widgets along with their associated user-specific data and settings.
        /// It aggregates the widget data, user widget data, and widget settings, then returns a list of widget responses.
        /// </summary>
        /// <returns>An HTTP response indicating the success or failure of the operation. If successful, a list of widget details is returned.</returns>
        /// <response code="200">If the widgets and their related data are successfully retrieved, a successful response with the widget details list is returned.</response>
        /// <response code="400">If an error occurs, an error message with details of the exception is returned.</response>

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



        /// <summary>
        /// Retrieves a widget by its ID and returns detailed information about it, including the widget data, user-specific widget data, and widget settings.
        /// If the widget or related data cannot be found, a 404 Not Found response is returned.
        /// </summary>
        /// <param name="id">The unique identifier of the widget to retrieve.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation. If successful, the widget data, user widget data, and widget settings are returned.</returns>
        /// <response code="200">If the widget and its related data are found, a successful response with the widget details is returned.</response>
        /// <response code="400">If an error occurs, an error message with details of the exception is returned.</response>
        /// <response code="404">If the widget or its related data cannot be found, a Not Found response is returned.</response>

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


        /// <summary>
        /// Saves a widget based on the details provided in the request body.
        /// The widget is processed according to its category and saved using the appropriate widget type, such as Image, Weather, CityDetails, or News.
        /// </summary>
        /// <param name="widgetRequestDTO">The object containing the widget's details to be saved, passed in the request body.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation. Returns the saved widget data if successful.</returns>
        /// <response code="200">If the widget is saved successfully, the corresponding widget data is returned.</response>
        /// <response code="400">If an error occurs, an error message with details of the exception is returned.</response>
        [HttpPost("saveWidget")]
        public async Task<IActionResult> SaveWidget([FromBody] WidgetRequestDTO widgetRequestDTO)
        {
            var userId = 5;
            var categoryId = (WidgetType)widgetRequestDTO.Widget.CategoryId;
            try
            {
                switch (categoryId)
                {
                    case WidgetType.Image:
                        var resultImage = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Image);
                        return Ok(resultImage);
                    case WidgetType.Weather: 
                        var result = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Weather);
                        return Ok(result);
                    case WidgetType.CityDetails:
                        var resultCityDetails = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.CityDetails);
                       return Ok(resultCityDetails);
                    case WidgetType.News: 
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


        /// <summary>
        /// Updates a widget based on its id and the details provided in the request body.
        /// Depending on the widget's category type, it is processed and saved using different widget types.
        /// </summary>
        /// <param name="id">The unique identifier of the widget to be updated.</param>
        /// <param name="widgetRequestDTO">The object containing the widget's information to be updated, passed in the request body.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation.</returns>
        /// <response code="200">If the operation was successful, the result of the corresponding widget type is returned.</response>
        /// <response code="400">If an exception occurred, an error message with details of the exception is returned.</response>
        [HttpPut("{id}", Name = "UpdateWidget")]
        public async Task<IActionResult> UpdateWidget(int id,[FromBody] WidgetRequestDTO widgetRequestDTO)
        {
            int ? userId = null;
            var categoryId = (WidgetType)widgetRequestDTO.Widget.CategoryId;

            try
            {
                switch (categoryId)
                {
                    case WidgetType.Image:
                        var resultImage = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Image);
                        return Ok(resultImage);
                    case WidgetType.Weather:
                        var result = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.Weather);
                        return Ok(result);
                    case WidgetType.CityDetails:
                        var resultCityDetails = await _widgetStContext.SaveWidgetAsync(widgetRequestDTO, userId, null, WidgetType.CityDetails);
                        return Ok(resultCityDetails);
                    case WidgetType.News:
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
