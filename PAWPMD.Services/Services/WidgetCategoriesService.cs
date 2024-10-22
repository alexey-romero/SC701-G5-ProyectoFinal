using PAWPMD.Data.Repository;
using PAWPMD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{

    public interface IWidgetCategoriesService
    {
        Task<UserWidget> GetUserWidget(int id);

        Task<UserWidget> SaveUserWidget(UserWidget userWidget);

        Task<bool> DeleteUserWidgetAsync(UserWidget userWidget);

        Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync();

    }
    public class WidgetCategoriesService : IWidgetCategoriesService
    {
        private readonly IWidgetCategoriesRepository _widgetCategoriesRepository;

        public WidgetCategoriesService(IWidgetCategoriesRepository widgetCategoriesRepository)
        {
            _widgetCategoriesRepository = widgetCategoriesRepository;
        }

        public async Task<WidgetCategory> GetWidgetCategory(int id)
        {
            return await _widgetCategoriesRepository.GetWidgetCategory(id);
        }

        public async Task<WidgetCategory> SaveWidgetCategory(WidgetCategory widgetCategory)
        {
            return await _widgetCategoriesRepository.SaveWidgetCategory(widgetCategory);
        }

        public async Task<bool> DeleteWidgetCategoryAsync(WidgetCategory widgetCategory)
        {
            return await _widgetCategoriesRepository.DeleteWidgetCategoryAsync(widgetCategory);
        }

        public async Task<IEnumerable<WidgetCategory>> GetAllWidgetCategoriesAsync()
        {
            return await _widgetCategoriesRepository.GetAllWidgetCategoriesAsync();
        }

        public Task<UserWidget> GetUserWidget(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserWidget> SaveUserWidget(UserWidget userWidget)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserWidgetAsync(UserWidget userWidget)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
