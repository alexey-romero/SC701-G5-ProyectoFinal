using PAWPMD.Architecture.Exceptions;
using PAWPMD.Architecture.Factory;
using PAWPMD.Data.Repository;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Interface defining the contract for widget services.
    /// </summary>
    public interface IWidgetService
    {
        /// <summary>
        /// Asynchronously deletes a widget.
        /// </summary>
        /// <param name="widget">The widget entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteWidgetAsync(Widget widget);

        /// <summary>
        /// Asynchronously retrieves all widgets from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widgets.</returns>
        Task<IEnumerable<Widget>> GetAllWidgetsAsync();

        /// <summary>
        /// Asynchronously retrieves a widget by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget entity if found, otherwise null.</returns>
        Task<Widget> GetWidgetByIdAsync(int id);

        /// <summary>
        /// Asynchronously saves a widget based on the provided widget data and user ID.
        /// </summary>
        /// <param name="widgetDTO">The widget data transfer object containing widget details.</param>
        /// <param name="userId">The ID of the user associated with the widget.</param>
        /// <returns>A task that represents the asynchronous operation, returning the saved widget.</returns>
        Task <Widget> SaveWidgetAsync(WidgetRequestDTO widgetRequestDTO, int? userId, int? widgetId);
    }

    /// <summary>
    /// Service class for managing widgets.
    /// </summary>
    public class WidgetService : IWidgetService
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IWidgetFactory _widgetFactory;
        private readonly IWidgetVideoService _widgetVideoService;
        private readonly IWidgetTableService _widgetTableService;
        private readonly IWidgetImageService _widgetImageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetService"/> class.
        /// </summary>
        /// <param name="widgetRepository">The widget repository.</param>
        /// <param name="widgetFactory">The widget factory.</param>
        public WidgetService(
                IWidgetRepository widgetRepository , 
                IWidgetVideoService widgetVideoService,
                IWidgetTableService widgetTableService,
                IWidgetImageService widgetImageService,
                IWidgetFactory widgetFactory
            )
        {
            _widgetRepository = widgetRepository;
            _widgetFactory = widgetFactory;
            _widgetVideoService = widgetVideoService;
            _widgetTableService = widgetTableService;
            _widgetImageService = widgetImageService;
        }

        /// <summary>
        /// Asynchronously saves a widget based on the specified type and user.
        /// </summary>
        /// <param name="widgetDTO">The data transfer object containing widget details.</param>
        /// <param name="userId">The ID of the user creating the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved widget entity.</returns>
        /// <exception cref="PAWPMDException">Thrown when the widget type is invalid.</exception>
        public async Task<Widget> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId)
        {

            Widget widget;

            if (widgetId.HasValue && widgetId > 0)
            { 
                widget = await _widgetRepository.GetWidget(widgetId.Value);
                if(widget == null)
                {
                     throw new PAWPMDException("Widget not found");
                }
            }
            else
            {
                // Create a new widget instance based on the specified type
                widget = _widgetFactory.Create(widgetDTO.Type);
            }


            // Prepare the widget data using the provided DTO and user ID
             var mapWidget = await WidgetMapper.PrepareWidgetDataAsync(widget, widgetDTO, userId);

            // Save the widget based on its type
            switch (widgetDTO.Type)
            {
                case "TableWidget":
                    return await _widgetTableService.SaveWidgetTableAsync(widget, widgetDTO);

                case "ImageWidget":
                    return await _widgetImageService.SaveWidgetImageAsync(mapWidget, widgetDTO);

                case "VideoWidget":
                    return await _widgetVideoService.SaveVideoWidgetAsync(widget, widgetDTO);

                default:
                    throw new PAWPMDException($"Invalid widget type: {widgetDTO.Type}");
            }
        }

        /// <summary>
        /// Asynchronously retrieves a widget by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget entity if found, otherwise null.</returns>
        public async Task<Widget> GetWidgetByIdAsync(int id)

        {
            return await _widgetRepository.GetWidget(id);
        }

        /// <summary>
        /// Asynchronously retrieves all widgets from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widgets.</returns>
        public async Task<IEnumerable<Widget>> GetAllWidgetsAsync()
        {
            return await _widgetRepository.GetAllWidgetsAsync();
        }

        /// <summary>
        /// Asynchronously deletes a widget from the system.
        /// </summary>
        /// <param name="widget">The widget entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteWidgetAsync(Widget widget)
        {   
            return await _widgetRepository.DeleteWidgetAsync(widget);
        }
    }
}

