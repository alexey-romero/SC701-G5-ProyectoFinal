using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Data.Repository
{
    public interface IWidgetTableRepository
    {
        Task<bool> DeleteWidgetTableAsync(WidgetTable widget);
        Task<IEnumerable<WidgetTable>> GetAllWidgetTablessAsync();
        Task<WidgetTable> GetWidgetTableAsync(int id);
        Task<WidgetTable> SaveWidgetTableAsync(WidgetTable widget);
    }
    public class WidgetTableRepository : RepositoryBase<WidgetTable>, IWidgetTableRepository
    {

        public async Task<WidgetTable> SaveWidgetTableAsync(WidgetTable widget)
        {
            var exists = widget.Id != null && widget.Id > 0;
            if (exists)
                await UpdateAsync(widget);
            else
                await CreateAsync(widget);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.Id == widget.Id);
        }

        public async Task<bool> DeleteWidgetTableAsync(WidgetTable widget)
        {
            return await DeleteAsync(widget);
        }

        public async Task<WidgetTable> GetWidgetTableAsync(int id)
        {
            var widgets = await ReadAsync();
            return widgets.SingleOrDefault(u => u.Id == id);
        }

        public async Task<IEnumerable<WidgetTable>> GetAllWidgetTablessAsync()
        {
            return await ReadAsync();
        }



    }
}
