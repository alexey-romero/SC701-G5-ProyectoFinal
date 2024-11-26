using PAWPMD.Models.DTOS;


public class WidgetRequestDTO
{
    public string Type { get; set; } = null!;
    public WidgetDTO Widget { get; set; } = null!; 
    public WidgetImageDto? Image { get; set; }
    public WidgetTableDTO? Table { get; set; } 
    public WidgetVideoDTO? Video { get; set; }
    public UserWidgetDTO? UserWidget { get; set; }
    public WidgetSettingDTO? WidgetSetting { get; set; }
}