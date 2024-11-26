using PAWPMD.Data.Repository;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Interface defining the contract for widget image services.
    /// </summary>
    public interface IWidgetImageService
    {
        /// <summary>
        /// Asynchronously deletes a widget image.
        /// </summary>
        /// <param name="widgetImageDto">The widget image data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteWidgetImage(WidgetImageDto widgetImageDto, int widgetId);

        /// <summary>
        /// Asynchronously retrieves all widget images from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget images.</returns>
        Task<IEnumerable<WidgetImage>> GetAllWidgetImages();

        /// <summary>
        /// Asynchronously retrieves a widget image by its unique identifier.
        /// </summary>
        /// <param name="widgetId">The ID of the widget image to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget image entity if found, otherwise null.</returns>
        Task<WidgetImage> GetWidgetImagebyIdAsync(int widgetId);

        /// <summary>
        /// Asynchronously saves a widget image entity. If the widget image already exists, it updates it; otherwise, it creates a new widget image.
        /// </summary>
        /// <param name="widgetImageDto">The widget image data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated widget image entity.</returns>
        Task<Widget> SaveWidgetImageAsync(Widget widget, WidgetRequestDTO widgetRequestDTO);
    }


    /// <summary>
    /// Service class for managing widget images.
    /// </summary>
    public class WidgetImageService : IWidgetImageService
    {
        private readonly IWidgetImageRepository _widgetImageRepository;
        private readonly IWidgetRepository _widgetRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetImageService"/> class.
        /// </summary>
        /// <param name="widgetImageRepository">The widget image repository.</param>
        public WidgetImageService(IWidgetImageRepository widgetImageRepository, IWidgetRepository widgetRepository)
        {
            _widgetImageRepository = widgetImageRepository;
            _widgetRepository = widgetRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a widget image by its unique identifier.
        /// </summary>
        /// <param name="widgetId">The ID of the widget image to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget image entity if found, otherwise null.</returns>
        public async Task<WidgetImage> GetWidgetImagebyIdAsync(int widgetId)
        {
            return await _widgetImageRepository.GetWidgeImageByIdAsync(widgetId);
        }

        /// <summary>
        /// Asynchronously retrieves all widget images from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget images.</returns>
        public async Task<IEnumerable<WidgetImage>> GetAllWidgetImages()
        {
            return await _widgetImageRepository.GetAllWidgetsAsync();
        }

        /// <summary>
        /// Asynchronously saves a widget image entity. If the widget image already exists, it updates it; otherwise, it creates a new widget image.
        /// </summary>
        /// <param name="widgetImageDto">The widget image data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the saved or updated widget image entity.</returns>
        public async Task<Widget> SaveWidgetImageAsync(Widget widget,WidgetRequestDTO widgetRequestDTO)
        {
            if(widget is not WidgetImage widgetImage)
            {
                throw new Exception("Widget is not a WidgetImage");
            }

            var widgetData = await WidgetImageMapper.PrepareWidgetTImageDataAsync(widgetImage, widgetRequestDTO);

           var widgetSaved =  await _widgetRepository.SaveWidget(widgetData);
        
            if(widgetSaved == null || widgetSaved.WidgetId <= 0)
            {
                throw new Exception("Failed to save the ImageWidget.");
            }

            return widgetSaved;
        }

        /// <summary>
        /// Asynchronously deletes a widget image from the system.
        /// </summary>
        /// <param name="widgetImageDto">The widget image data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteWidgetImage(WidgetImageDto widgetImageDto, int widgetId)
        {
            var widgetImage = new WidgetImage
            {
                ImageUrl = widgetImageDto.ImageUrl,
                ImageAltText = widgetImageDto.ImageAltText,
                ImageTitle = widgetImageDto.ImageTitle,
                Status = widgetImageDto.Status,
                ThemeConfig = widgetImageDto.ThemeConfig,
                LastModified = DateTime.Now,
                WidgetId = widgetId
            };

            return await _widgetImageRepository.DeleteWidgetAsync(widgetImage);
        }

        
    }
}
