using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers.DTOS;
using PAWPMD.Service.Services;

namespace PAWPMD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetService _widgetService;
        private readonly IUserWidgetService _userWidgetService;
        private readonly IWidgetSettingService _widgetSettingService;
        public WidgetController(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService)
        {
            _widgetService = widgetService;
            _userWidgetService = userWidgetService;
            _widgetSettingService = widgetSettingService;
        }

        [HttpGet(Name = "Get Widgets")]
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

                var userWidget = await _userWidgetService.GetUserWidgetByWidgetIdAsync(widget.WidgetId);
                if (userWidget == null)
                {
                    return NotFound();
                }

                var widgetSettings = await _widgetSettingService.GetWidgetSettingByUserWidgetIdAsync(userWidget.UserWidgetId);

                var widgetResponseDTO = new WidgetResponseDTO
                {
                    Widget = new WidgetDTO(),
                    Video = new WidgetVideoDTO(),
                    Image = new WidgetImageDto(),
                    Table = new WidgetTableDTO(),
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
            var userId= 5;
            try
            {
                var widget = await _widgetService.SaveWidgetAsync(widgetRequestDTO, userId, null);

                //now we need to save UserWidget in the database
          
                var userWidget = await _userWidgetService.SaveUserWidgetAsync(widgetRequestDTO, widget, userId);

                // then we can save the WidgetSettings for each user 
                var widgetSettings = await _widgetSettingService.SaveWidgetSettinsAsync(widgetRequestDTO, userWidget);

                var widgetResponseDTO = new WidgetResponseDTO
                {
                    Widget = new WidgetDTO(), 
                    Video = new WidgetVideoDTO(),
                    Image = new WidgetImageDto(),
                    Table = new WidgetTableDTO(),
                    UserWidget = new UserWidgetDTO(),
                    WidgetSetting = new WidgetSettingDTO()
                };

                var result = await WidgetDTOResponseMapper.PrepareWidgetDTOReponseDataAsync(widgetResponseDTO, widget, userWidget, widgetSettings);

                return Ok(result);

            }catch(Exception ex)
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
                var widget = await _widgetService.SaveWidgetAsync(widgetRequestDTO, userId , id);
                if (widget == null)
                {
                    return NotFound();
                }
                //UPDATE USER WIDGET
                var userWidget = await _userWidgetService.SaveUserWidgetAsync(widgetRequestDTO, widget, userId);

                //UPDATE WIDGET SETTINGS
                var widgetSettings = await _widgetSettingService.SaveWidgetSettinsAsync(widgetRequestDTO, userWidget);
                
                var widgetResponseDTO = new WidgetResponseDTO
                {
                    Widget = new WidgetDTO(),
                    Video = new WidgetVideoDTO(),
                    Image = new WidgetImageDto(),
                    Table = new WidgetTableDTO(),
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
                var userWidget = await _userWidgetService.GetUserWidgetByWidgetIdAsync(widget.WidgetId);

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
