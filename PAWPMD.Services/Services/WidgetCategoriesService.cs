using PAWPMD.Data.Repository;
using PAWPMD.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{

    public interface IWidgetCategoriesService
    {
        Task<UserWidget> GetUserWidget(int id); //confirmar si los de user deben esta aqui

        Task<UserWidget> SaveUserWidget(UserWidget userWidget);

        Task<bool> DeleteUserWidgetAsync(UserWidget userWidget);

        Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync();
        //hasta aqui los de users
        Task<WidgetCategory> GetWidgetCategoryAsync(int id);

        Task<WidgetCategory> SaveWidgetCategoryAsync(WidgetCategory widgetCategory);

        //Task<bool> DeleteWidgetCategoryAsync(WidgetCategory widgetCategory);
        Task<bool> DeleteWidgetCategoryAsync(int id);

        Task<IEnumerable<WidgetCategory>> GetAllWidgetCategoriesAsync();


    }
    public class WidgetCategoriesService : IWidgetCategoriesService
    {
        private readonly IWidgetCategoriesRepository _widgetCategoriesRepository;

        public WidgetCategoriesService(IWidgetCategoriesRepository widgetCategoriesRepository)
        {
            _widgetCategoriesRepository = widgetCategoriesRepository;
        }

        public async Task<WidgetCategory> GetWidgetCategoryAsync(int id)
        {
            return await _widgetCategoriesRepository.GetWidgetCategoryAsync(id);
        }

        public async Task<WidgetCategory> SaveWidgetCategoryAsync(WidgetCategory widgetCategory)
        {
            return await _widgetCategoriesRepository.SaveWidgetCategoryAsync(widgetCategory);
        }



        //public async Task<bool> DeleteWidgetCategoryAsync(WidgetCategory widgetCategory)
        //{
        //    return await _widgetCategoriesRepository.DeleteWidgetCategoryAsync(widgetCategory);
        //}

        public async Task<bool> DeleteWidgetCategoryAsync(int id)
        {
            var widgetCategory = await _widgetCategoriesRepository.GetAllWidgetCategoriesAsync();
            var deletion = widgetCategory.SingleOrDefault(x => x.CategoryId == id);
            return await _widgetCategoriesRepository.DeleteWidgetCategoryAsync(deletion);
        }

        public async Task<IEnumerable<WidgetCategory>> GetAllWidgetCategoriesAsync()
        {
            return await _widgetCategoriesRepository.GetAllWidgetCategoriesAsync();
        }




        //confirmar si los de USER debe estar aqui
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
