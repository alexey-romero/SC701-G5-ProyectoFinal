

namespace PAWPMD.Models.DTOS
{
    public  class WidgetResponseDTO
    {
        public string? Type { get; set; } = null!;
        public WidgetDTO Widget { get; set; } = null!;
        public WidgetImageDto? Image { get; set; } = null!;
        public WidgetTableDTO? Table { get; set; } = null!;
        public WidgetVideoDTO? Video { get; set; } = null!;

        public UserWidgetDTO? UserWidget { get; set; } = null!;
        public WidgetSettingDTO? WidgetSetting { get; set; } = null!;

    }
}
