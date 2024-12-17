using PAWPMD.Models.DTOS;


public class WidgetRequestDTO
{
    public WidgetDTO Widget { get; set; } = null!;
    public UserWidgetDTO? UserWidget { get; set; }
    public WidgetSettingDTO? WidgetSetting { get; set; }
}