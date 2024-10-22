using PAWPMD.Data.Repository;
using PAWPMD.Models;

namespace PAWPMD.Service.Services;
/// <summary>
/// 
/// </summary>
public interface  IUserWidgetService
{
    /// <summary>
    ///  Asynchronously retrieves a user from the system.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserWidget> GetUserWidget(int id);
    /// <summary>
    /// Asynchronously saves a user widget entity.
    /// </summary>
    /// <param name="userWidget"></param>
    /// <returns></returns>
    Task<UserWidget> SaveUserWidget(UserWidget userWidget);
    /// <summary>
    /// Asynchronously deletes a user widget from the system.
    /// </summary>
    /// <param name="userWidget"></param>
    /// <returns></returns>
    Task<bool> DeleteUserWidgetAsync(UserWidget userWidget);
    /// <summary>
    ///  Asynchronously retrieves all users widgets from the system.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync();

}

public class UserWidgetService : IUserWidgetService
{
    private readonly IUserWidgetRepository _userWidgetRepository;
    /// <summary>
    /// Initializes a new instance of the <see cref="UserWidget/> class.
    /// </summary>
    /// <param name="userWidgetRepository"></param>
    public UserWidgetService(IUserWidgetRepository userWidgetRepository)
    {
        _userWidgetRepository = userWidgetRepository;
    }
    /// <summary>
    /// Asynchronously retrieves a userwidget by their unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<UserWidget> GetUserWidget(int id)
    {
        return await _userWidgetRepository.GetUserWidget(id);
    }
    /// <summary>
    /// Asynchronously saves a user widget entity.
    /// </summary>
    /// <param name="userWidget"></param>
    /// <returns></returns>
    public async Task<UserWidget> SaveUserWidget(UserWidget userWidget)
    {
        return await _userWidgetRepository.SaveUserWidget(userWidget);
    }
    /// <summary>
    /// Asynchronously deletes a userwidget from the system.
    /// </summary>
    /// <param name="userWidget"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUserWidgetAsync(UserWidget userWidget)
    {
        return await _userWidgetRepository.DeleteUserWidgetAsync(userWidget);
    }
    /// <summary>
    /// Asynchronously retrieves all users widgets from the system.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<UserWidget>> GetAllUserWidgetsAsync()
    {
        return await _userWidgetRepository.GetAllUserWidgetsAsync();
    }
}