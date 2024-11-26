using PAWPMD.Architecture.Exceptions;
using PAWPMD.Data.Repository;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Interface defining the contract for widget video services.
    /// </summary>
    public interface IWidgetVideoService
    {
        /// <summary>
        /// Asynchronously deletes a widget video.
        /// </summary>
        /// <param name="widgetVideoDto">The widget video data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteWidgetVideoAsync(WidgetVideoDTO widgetVideoDto, int widgetId);

        /// <summary>
        /// Asynchronously retrieves all widget videos from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget videos.</returns>
        Task<IEnumerable<WidgetVideo>> GetAllWidgetVideosAsync();

        /// <summary>
        /// Asynchronously retrieves a widget video by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget video to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget video entity if found, otherwise null.</returns>
        Task<WidgetVideo> GetWidgetVideoByIdAsync(int id);

        /// <summary>
        /// Asynchronously saves a widget video entity. If the widget video already exists, it updates it; otherwise, it creates a new widget video.
        /// </summary>
        /// <param name="widgetVideoDto">The widget video data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated widget video entity.</returns>
        Task<Widget> SaveVideoWidgetAsync(Widget widget,WidgetRequestDTO widgetRequestDTO);
    }

    /// <summary>
    /// Service class for managing widget videos.
    /// </summary>
    public class WidgetVideoService : IWidgetVideoService
    {
        private readonly IWidgetVideoRepository _widgetVideoRepository;
        private readonly IWidgetRepository _widgetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetVideoService"/> class.
        /// </summary>
        /// <param name="widgetVideoRepository">The widget video repository.</param>
        public WidgetVideoService(IWidgetVideoRepository widgetVideoRepository, IWidgetRepository widgetRepository)
        {
            _widgetVideoRepository = widgetVideoRepository;
            _widgetRepository = widgetRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a widget video by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget video to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget video entity if found, otherwise null.</returns>
        public async Task<WidgetVideo> GetWidgetVideoByIdAsync(int id)
        {
            return await _widgetVideoRepository.GetWidgetVideoAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves all widget videos from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget videos.</returns>
        public async Task<IEnumerable<WidgetVideo>> GetAllWidgetVideosAsync()
        {
            return await _widgetVideoRepository.GetAllWidgetVideosAsync();
        }

        /// <summary>
        /// Asynchronously saves a widget video entity. If the widget video already exists, it updates it; otherwise, it creates a new widget video.
        /// </summary>
        /// <param name="widgetVideoDto">The widget video data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated widget video entity.</returns>
        public async Task<Widget> SaveVideoWidgetAsync(Widget widget, WidgetRequestDTO widgetRequestDTO)
        {
            if (widget is not WidgetVideo videoWidget)
            {
                throw new PAWPMDException("Invalid widget type.");
            }

            var widgetData = await WidgetVideoMapper.PrepareWidgetVideoDataAsync(videoWidget, widgetRequestDTO);
            var widgetSaved = await _widgetRepository.SaveWidget(widgetData);

            if (widgetSaved == null || widgetSaved.WidgetId <= 0)
            {
                throw new InvalidOperationException("Failed to save the VideoWidget.");
            }

            return widgetSaved;
        }

        /// <summary>
        /// Asynchronously deletes a widget video from the system.
        /// </summary>
        /// <param name="widgetVideoDto">The widget video data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteWidgetVideoAsync(WidgetVideoDTO widgetVideoDto, int widgetId)
        {
            var widgetVideo = new WidgetVideo
            {
                VideoUrl = widgetVideoDto.VideoUrl,
                VideoAltText = widgetVideoDto.VideoAltText,
                VideoTitle = widgetVideoDto.VideoTitle,
                Status = widgetVideoDto.Status,
                Duration = widgetVideoDto.Duration,
                ThemeConfig = widgetVideoDto.ThemeConfig,
                LastModified = DateTime.Now,
                WidgetId = widgetId
            };

            return await _widgetVideoRepository.DeleteWidgetVideoAsync(widgetVideo);
        }
    }
}
