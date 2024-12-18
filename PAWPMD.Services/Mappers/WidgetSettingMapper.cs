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
        /// <summary>
        /// Prepares and returns a `WidgetSetting` object by updating its properties based on the provided `WidgetRequestDTO` and `UserWidget`.
        /// If the `WidgetSetting` is null or has an ID of 0, a new `WidgetSetting` is created with the `UserWidgetId` and settings from the `WidgetRequestDTO`.
        /// If the `WidgetSetting` already exists, it updates the `Settings` property with the new values from the request.
        /// </summary>
        /// <param name="widgetSetting">The existing `WidgetSetting` to update. If null or with an ID of 0, a new `WidgetSetting` will be created.</param>
        /// <param name="widgetRequestDTO">The `WidgetRequestDTO` containing the new settings to update in the `WidgetSetting`.</param>
        /// <param name="userWidget">The `UserWidget` object that provides the `UserWidgetId` to associate with the `WidgetSetting`.</param>
        /// <returns>A `Task` that represents the asynchronous operation. The task result is the updated or newly created `WidgetSetting` object.</returns>

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
