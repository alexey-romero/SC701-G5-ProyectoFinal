using PAWPMD.Architecture.Exceptions;
using PAWPMD.Data.Repository;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers;


namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Interface defining the contract for widget table services.
    /// </summary>
    public interface IWidgetTableService
    {
        /// <summary>
        /// Asynchronously deletes a widget table.
        /// </summary>
        /// <param name="tableDTO">The widget table data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        Task<bool> DeleteWidgetTable(WidgetTableDTO tableDTO, int widgetId);

        /// <summary>
        /// Asynchronously retrieves all widget tables from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget tables.</returns>
        Task<IEnumerable<WidgetTable>> GetAllWidgetTablesAsync();

        /// <summary>
        /// Asynchronously retrieves a widget table by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget table to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget table entity if found, otherwise null.</returns>
        Task<WidgetTable> GetWidgetTableByIdAsync(int id);

        /// <summary>
        /// Asynchronously saves a widget table entity. If the widget table already exists, it updates it; otherwise, it creates a new widget table.
        /// </summary>
        /// <param name="tableDTO">The widget table data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<Widget> SaveWidgetTableAsync(Widget widget,WidgetRequestDTO widgetRequestDTO);
    }

    /// <summary>
    /// Implementation of the widget table services.
    /// </summary>
    public class WidgetTableService : IWidgetTableService
    {
        private readonly IWidgetTableRepository _widgetTableRepository;
        private readonly IWidgetRepository _widgetRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetTableService"/> class.
        /// </summary>
        /// <param name="widgetTableRepository">The widget table repository.</param>
        public WidgetTableService(IWidgetTableRepository widgetTableRepository, IWidgetRepository widgetRepository)
        {
            _widgetTableRepository = widgetTableRepository;
            _widgetRepository = widgetRepository;
        }

        /// <summary>
        /// Asynchronously retrieves a widget table by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the widget table to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the widget table entity if found, otherwise null.</returns>
        public async Task<WidgetTable> GetWidgetTableByIdAsync(int id)
        {
            return await _widgetTableRepository.GetWidgetTableAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves all widget tables from the system.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of widget tables.</returns>
        public async Task<IEnumerable<WidgetTable>> GetAllWidgetTablesAsync()
        {
            return await _widgetTableRepository.GetAllWidgetTablessAsync();
        }

        /// <summary>
        /// Asynchronously saves a widget table entity. If the widget table already exists, it updates it; otherwise, it creates a new widget table.
        /// </summary>
        /// <param name="tableDTO">The widget table data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task<Widget> SaveWidgetTableAsync(Widget widget,WidgetRequestDTO widgetRequestDTO)
        {
          if(widget is not WidgetTable widgetTable)
            {
                throw new PAWPMDException("Invalid widget type.");
            }
            var widgetData = await WidgetTableMapper.PrepareWidgetTableDataAsync(widgetTable, widgetRequestDTO);
            var widgetSaved = await _widgetRepository.SaveWidget(widgetData);
            
            if(widgetSaved == null || widgetSaved.WidgetId == 0)
            {
                throw new PAWPMDException("Failed to save the WidgetTable.");
            }
            return widgetSaved;
        }
        /// <summary>
        /// Asynchronously deletes a widget table from the system.
        /// </summary>
        /// <param name="tableDTO">The widget table data transfer object.</param>
        /// <param name="widgetId">The ID of the widget.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a boolean indicating success.</returns>
        public async Task<bool> DeleteWidgetTable(WidgetTableDTO tableDTO, int widgetId)
        {
            var widgetTable = new WidgetTable
            {
                Headers = tableDTO.Headers,
                Columns = tableDTO.Columns,
                Rows = tableDTO.Rows,
                Status = tableDTO.Status,
                ThemeConfig = tableDTO.ThemeConfig,
                LastModified = tableDTO.LastModified,
                WidgetId = widgetId
            };

            return await _widgetTableRepository.DeleteWidgetTableAsync(widgetTable);
        }
    }
}
