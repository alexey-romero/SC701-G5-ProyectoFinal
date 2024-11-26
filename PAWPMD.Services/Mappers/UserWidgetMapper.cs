﻿using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Mappers
{
    public static class UserWidgetMapper
    {
        public static UserWidget PrepareUserWidgetData(UserWidget? userWidget, Widget widget ,WidgetRequestDTO widgetRequestDTO, int? userId)
        {
            if (userWidget == null || userWidget.UserWidgetId == 0) 
            {
                userWidget = new UserWidget
                {
                    UserId = userId,
                    WidgetId = widget.WidgetId,
                    PositionX = widgetRequestDTO.UserWidget.PositionX,
                    PositionY = widgetRequestDTO.UserWidget.PositionY,
                    IsFavorite = widgetRequestDTO.UserWidget.IsFavorite,
                    IsVisible = widgetRequestDTO.UserWidget.IsVisible,
                    CreatedAt = DateTime.Now
                };
            }
            else 
            {
                userWidget.PositionX = widgetRequestDTO.UserWidget.PositionX;
                userWidget.PositionY = widgetRequestDTO.UserWidget.PositionY;
                userWidget.IsFavorite = widgetRequestDTO.UserWidget.IsFavorite;
                userWidget.IsVisible = widgetRequestDTO.UserWidget.IsVisible;
            }
            return userWidget;
        }
    }
}