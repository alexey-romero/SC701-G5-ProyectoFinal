using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Mappers
{
    public static class UserWidgetMapper
    {
        /// <summary>
        /// Prepares and returns a `UserWidget` object based on the provided data.
        /// If the `UserWidget` is null or has an ID of 0, a new `UserWidget` is created with data from the provided `widget` and `widgetRequestDTO`.
        /// If the `UserWidget` already exists, it updates its `IsFavorite` and `IsVisible` properties.
        /// </summary>
        /// <param name="userWidget">The existing `UserWidget` to update. If null or with an ID of 0, a new `UserWidget` will be created.</param>
        /// <param name="widget">The `Widget` object that provides the widget details, including the `WidgetId`.</param>
        /// <param name="widgetRequestDTO">The `WidgetRequestDTO` containing user-specific preferences such as `IsFavorite` and `IsVisible`.</param>
        /// <param name="userId">The unique identifier of the user to associate with the `UserWidget`.</param>
        /// <returns>A `UserWidget` object, either newly created or updated with the provided data.</returns>

        public static UserWidget PrepareUserWidgetData(UserWidget? userWidget, Widget widget ,WidgetRequestDTO widgetRequestDTO, int? userId)
        {
            if (userWidget == null || userWidget.UserWidgetId == 0) 
            {
                userWidget = new UserWidget
                {
                    UserId = userId,
                    WidgetId = widget.WidgetId ?? 0,
                    IsFavorite = widgetRequestDTO.UserWidget.IsFavorite,
                    IsVisible = widgetRequestDTO.UserWidget.IsVisible,
                    CreatedAt = DateTime.Now
                };
            }
            else 
            {
  
                userWidget.IsFavorite = widgetRequestDTO.UserWidget.IsFavorite;
                userWidget.IsVisible = widgetRequestDTO.UserWidget.IsVisible;
            }
            return userWidget;
        }
    }
}
