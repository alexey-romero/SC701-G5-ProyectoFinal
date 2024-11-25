using Microsoft.AspNetCore.Mvc;
using PAWPMD.Models;
using PAWPMD.Service.Services;

namespace PAWPMD.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WidgetCategoriesController : Controller
{
    private readonly IWidgetCategoriesService _widgetCategoriesService;

    public WidgetCategoriesController(IWidgetCategoriesService widgetCategoriesService)
    {
        _widgetCategoriesService = widgetCategoriesService;
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public async Task<WidgetCategory> Get(int id)
    {
        return await _widgetCategoriesService.GetWidgetCategoryAsync(id);
    }

    [HttpGet("all", Name = "GetAllCategories")]
    public async Task<IEnumerable<WidgetCategory>> GetAll()
    {
        return await _widgetCategoriesService.GetAllWidgetCategoriesAsync();
    }

    [HttpPost("save", Name = "SaveCategory")]
    public async Task<WidgetCategory> Save([FromBody] WidgetCategory widgetCategory)
    {
        return await _widgetCategoriesService.SaveWidgetCategoryAsync(widgetCategory);
    }

    [HttpPut("{id}", Name = "UpdateCategory")]
    public async Task<WidgetCategory> Update([FromBody] WidgetCategory widgetCategory)
    {
        return await _widgetCategoriesService.SaveWidgetCategoryAsync(widgetCategory);
    }

    [HttpDelete("{id}", Name = "DeleteCategory")]
    public async Task<bool> Delete(int id)
    {
        return await _widgetCategoriesService.DeleteWidgetCategoryAsync(id);
    }
}