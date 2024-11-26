using PAWPMD.Models;

namespace PAWPMD.Data.Repository
{
    public interface IWidgetImageRepository
    {
        Task<bool> DeleteWidgetAsync(WidgetImage widget);
        Task<IEnumerable<WidgetImage>> GetAllWidgetsAsync();
        Task<WidgetImage> GetWidgeImageByIdAsync(int id);
        Task<WidgetImage> SaveWidgetImageAsync(WidgetImage widget);
    }

    public class WidgetImageRepository : RepositoryBase<WidgetImage>, IWidgetImageRepository
    {

        public async Task<WidgetImage> SaveWidgetImageAsync(WidgetImage widget)
        {
            var exists = widget.WidgetId != null && widget.WidgetId > 0;
            if (exists)
                await UpdateAsync(widget);
            else
                await CreateAsync(widget);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.WidgetId == widget.WidgetId);
        }

        public async Task<bool> DeleteWidgetAsync(WidgetImage widget)
        {
            return await DeleteAsync(widget);
        }

        public async Task<WidgetImage> GetWidgeImageByIdAsync(int id)
        {
            var widgets = await ReadAsync();
            return widgets.SingleOrDefault(u => u.WidgetId == id);
        }

        public async Task<IEnumerable<WidgetImage>> GetAllWidgetsAsync()
        {
            return await ReadAsync();
        }

    }
}
