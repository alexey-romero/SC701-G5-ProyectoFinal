﻿using Newtonsoft.Json.Linq;
using PAWPMD.Architecture.Exceptions;
using PAWPMD.Models;
using PAWPMD.Models.DTOS;
using PAWPMD.Service.Mappers.DTOS;
using PAWPMD.Service.Services;
using PAWPMD.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Service.Strategy
{
    public class ImageWidgetStrategy : IWidgetStrategy
    {
        private readonly IWidgetService _widgetService;
        private readonly IUserWidgetService _userWidgetService;
        private readonly IWidgetSettingService _widgetSettingService;
        private readonly IApiPexelsService _apiPexelsService;

        public ImageWidgetStrategy(IWidgetService widgetService, IUserWidgetService userWidgetService, IWidgetSettingService widgetSettingService, IApiPexelsService pexelsService)
        {
            _widgetService = widgetService;
            _userWidgetService = userWidgetService;
            _widgetSettingService = widgetSettingService;
            _apiPexelsService = pexelsService;
        }


        public async Task<WidgetResponseDTO> SaveWidgetAsync(WidgetRequestDTO widgetDTO, int? userId, int? widgetId)
        {
            try
            {
                var widget = await _widgetService.SaveWidgetAsync(widgetDTO, userId, null);
                var userWidget = await _userWidgetService.SaveUserWidgetAsync(widgetDTO, widget, userId);

                var widgetSettingsJson = JObject.Parse(widgetDTO.WidgetSetting.Settings);

                var imagesData = await FetchAndAppendImageDataAsync(widgetSettingsJson);

                widgetDTO.WidgetSetting.Settings = widgetSettingsJson.ToString();
                widgetDTO.WidgetSetting.UserWidgetId = userWidget.UserWidgetId;

                var saveSettings = await _widgetSettingService.SaveWidgetSettinsAsync(widgetDTO, userWidget);

                var widgetResponseDTO = await PrepareWidgetResponseAsync(widget, userWidget, widgetDTO.WidgetSetting);
                
                return widgetResponseDTO;
                 
            }
            catch (PAWPMDException)
            {
                throw;
            }
        }

        public async Task<string> FetchAndAppendImageDataAsync(JObject widgetSettingsJson)
        {
            var querie = widgetSettingsJson["querie"].ToString();
            var imagesDataJson = await _apiPexelsService.GetImagesByQuerie( querie, "tcwczHVxnNrwhnuQSNjttPYmv4xQqQ6ATvbGPlDHzXcnry38QEVzPv0t");
            var imagesData = JObject.Parse(imagesDataJson);
            widgetSettingsJson["images"] = imagesData;
            return imagesDataJson;
        }

        private async Task<WidgetResponseDTO> PrepareWidgetResponseAsync(Widget widget, UserWidget userWidget, WidgetSettingDTO widgetSettingsDTO)
        {
            var widgetSettings = new WidgetSetting
            {
                Settings = widgetSettingsDTO.Settings
            };


            return await WidgetDTOResponseMapper.PrepareWidgetDTOReponseDataAsync(
                new WidgetResponseDTO
                {
                    Widget = new WidgetDTO(),
                    UserWidget = new UserWidgetDTO(),
                    WidgetSetting = widgetSettingsDTO
                },
                widget,
                userWidget,
                widgetSettings
            );
        }
    }


}