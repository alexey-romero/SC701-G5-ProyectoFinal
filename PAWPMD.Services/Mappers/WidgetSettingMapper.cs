using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Mappers
{
    public static class WidgetSettingMapper
    {
        public static Task<WidgetSetting> PrepareWidgetSettingData(WidgetSetting widgetSetting,WidgetRequestDTO widgetRequestDTO,  UserWidget userWidget)
        {
            if (widgetSetting == null || widgetSetting.WidgetSettingsId == 0)
            {
                widgetSetting = new WidgetSetting
                {
                    UserWidgetId= userWidget.UserWidgetId,
                    Settings = widgetRequestDTO.WidgetSetting.Settings
                };
            }
            else
            {
                widgetSetting.Settings = widgetRequestDTO.WidgetSetting.Settings;
            }
            return Task.FromResult(widgetSetting);
        }
    }
}
