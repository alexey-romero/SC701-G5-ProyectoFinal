using PAWPMD.Data.Repository;
using PAWPMD.Models;

namespace PAWPMD.Service.Services;
/// <summary>
/// 
/// </summary>
public interface  IUserWidgetService
{
    Task<UserWidget> GetUserWidget(int id);

    Task<UserWidget> SaveUserWidget(UserWidget userWidget);

    Task<bool> DeleteUserWidgetAsync(UserWidget userWidget);

    Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync();

}

public class UserWidgetService : IUserWidgetService
{
    private readonly IUserWidgetRepository _userWidgetRepository;

    public UserWidgetService(IUserWidgetRepository userWidgetRepository)
    {
        _userWidgetRepository = userWidgetRepository;
    }

    public async Task<UserWidget> GetUserWidget(int id)
    {
        return await _userWidgetRepository.GetUserWidget(id);
    }

    public async Task<UserWidget> SaveUserWidget(UserWidget userWidget)
    {
        return await _userWidgetRepository.SaveUserWidget(userWidget);
    }

    public async Task<bool> DeleteUserWidgetAsync(UserWidget userWidget)
    {
        return await _userWidgetRepository.DeleteUserWidgetAsync(userWidget);
    }

    public async Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync()
    {
        return await _userWidgetRepository.GetAllUserWidgetsAsync();
    }
}