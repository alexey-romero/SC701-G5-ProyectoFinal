using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Data.Repository
{
    public interface IWidgetSettingRepository
    {
        Task<bool> DeleteWidgetSettingAsync(WidgetSetting widget);
        Task<IEnumerable<WidgetSetting>> GetAllWidgetSettingsAsync();
        Task<WidgetSetting> GetWidgetSettingAsync(int id);
        Task<WidgetSetting> SaveWidgetSettingAsync(WidgetSetting widget);

        Task<WidgetSetting> GetWidgetSettingByUserWidgetIdAsync(int userWidgetId);
    }

    public class WidgetSettingRepository : RepositoryBase<WidgetSetting>, IWidgetSettingRepository
    {
        public async Task<WidgetSetting> SaveWidgetSettingAsync(WidgetSetting widget)
        {
            var exists = widget.WidgetSettingsId != null && widget.WidgetSettingsId > 0;
            if (exists)
                await UpdateAsync(widget);
            else
                await CreateAsync(widget);

            var updated = await ReadAsync();
            return updated.SingleOrDefault(x => x.WidgetSettingsId == widget.WidgetSettingsId);
        }

        public async Task<bool> DeleteWidgetSettingAsync(WidgetSetting widget)
        {
            return await DeleteAsync(widget);
        }

        public async Task<WidgetSetting> GetWidgetSettingAsync(int id)
        {
            var widgets = await ReadAsync();
            return widgets.SingleOrDefault(u => u.WidgetSettingsId == id);
        }

        public async Task<IEnumerable<WidgetSetting>> GetAllWidgetSettingsAsync()
        {
            return await ReadAsync();
        }

        public async Task<WidgetSetting> GetWidgetSettingByUserWidgetIdAsync(int userWidgetId)
        {
            var widgets = await ReadAsync();
            return widgets.SingleOrDefault(u => u.UserWidgetId == userWidgetId);
        }


    }
}
