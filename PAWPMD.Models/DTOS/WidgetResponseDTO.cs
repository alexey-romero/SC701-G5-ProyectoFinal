

namespace PAWPMD.Models.DTOS
{
    public  class WidgetResponseDTO
    {
        public WidgetDTO Widget { get; set; } = null!;
        public UserWidgetDTO? UserWidget { get; set; } = null!;
        public WidgetSettingDTO? WidgetSetting { get; set; } = null!;
    }
}
