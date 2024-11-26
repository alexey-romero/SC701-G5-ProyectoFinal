using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Data.Repository
{
    public interface IWidgetVideoRepository
    {
        Task<bool> DeleteWidgetVideoAsync(WidgetVideo widget);
        Task<IEnumerable<WidgetVideo>> GetAllWidgetVideosAsync();
        Task<WidgetVideo> GetWidgetVideoAsync(int id);
        Task<WidgetVideo> SaveWidgetVideoAsync(WidgetVideo widget);
    }

    public class WidgetVideoRepository : RepositoryBase<WidgetVideo>, IWidgetVideoRepository
    {
        public async Task<WidgetVideo> SaveWidgetVideoAsync(WidgetVideo widget)
        {
            var exists = widget.Id != null && widget.Id > 0;
            if (exists)
                await UpdateAsync(widget);
            else
                await CreateAsync(widget);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.Id == widget.Id);
        }

        public async Task<bool> DeleteWidgetVideoAsync(WidgetVideo widget)
        {
            return await DeleteAsync(widget);
        }

        public async Task<WidgetVideo> GetWidgetVideoAsync(int id)
        {
            var widgets = await ReadAsync();
            return widgets.SingleOrDefault(u => u.Id == id);
        }

        public async Task<IEnumerable<WidgetVideo>> GetAllWidgetVideosAsync()
        {
            return await ReadAsync();
        }

    }
}
