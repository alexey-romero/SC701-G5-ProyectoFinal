
using PAWPMD.Architecture.Authentication;
using PAWPMD.Architecture.Exceptions;
using PAWPMD.Models;
using PAWPMD.Models.Models;

namespace PAWPMD.Service.Services
{
    /// <summary>
    /// Interface that defines the methods for the account service.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registerRequest">The data required for user registration.</param>
        /// <returns>A <see cref="User"/> object representing the registered user.</returns>
        Task<User> RegisterAsync(RegisterRequest registerRequest);

        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }

    /// <summary>
    /// Implementation of the account service, handling the user registration logic.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Constructor that initializes the account service with the necessary dependencies.
        /// </summary>
        /// <param name="userService">Service for handling users.</param>
        /// <param name="roleService">Service for handling roles.</param>
        /// <param name="userRoleService">Service for handling user-role relationships.</param>
        /// <param name="passwordHasher">Service for hashing passwords.</param>
        public AccountService(IUserService userService, IRoleService roleService, IUserRoleService userRoleService, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _roleService = roleService;
            _userRoleService = userRoleService;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Registers a new user in the system and assigns a default role.
        /// </summary>
        /// <param name="registerRequest">The data required for user registration.</param>
        /// <returns>A <see cref="User"/> object representing the registered user.</returns>
        /// <exception cref="PAWPMDException">Thrown if the default role 'User' is not found.</exception>
        public async Task<User> RegisterAsync(RegisterRequest registerRequest)
        {
            // Hash the provided password
            var passwordHash = _passwordHasher.Hash(registerRequest.Password);

            // Create a new user instance
            var user = new User
            {
                Name = registerRequest.Name,
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                LastName = registerRequest.LastName,
                SecondLastName = registerRequest.SecondLastName,
                Password = passwordHash,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
            };

            // Save the user and get its generated Id
            await _userService.SaveUser(user);

            // Retrieve the default role 'User' by name
            var defaultRole = await _roleService.GetRoleByNameAsync("User");

            // Check if the default role is null and throw an exception if so
            if (defaultRole == null)
            {
                throw new PAWPMDException("Default role 'User' wasn't found");
            }

            // Create a new user-role association using the Ids
            var userRole = new UserRole
            {
                UserId = user.UserId,   
                RoleId = defaultRole.RoleId   
            };

            // Save the user-role association to the database
            await _userRoleService.SaveUserRole(userRole);

            // Return the registered user
            return user;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            User user = null;

            if (!string.IsNullOrEmpty(loginRequest.Username))
            {
                user = await _userService.GetUserByUsernameAsync(loginRequest.Username);
            }
            else if (!string.IsNullOrEmpty(loginRequest.Email))
            {
                user = await _userService.GetUserByEmailAsync(loginRequest.Email);
            }

            if(user == null)
            {
                throw new PAWPMDException("Invalid username or email");
            }

            var isPasswordValid = _passwordHasher.Verify(user.Password, loginRequest.Password);
            if (!isPasswordValid)
            {
                throw new PAWPMDException("Invalid password");
            }

            var userRoles = await _userRoleService.GetUserRolesByUserIdAsync(user.UserId);

            var roles = new List<Role>();

            foreach (var userRole in userRoles) {
               if(userRole != null)
                {
                    var role = await _roleService.GetRole(userRole.RoleId);
                    if(role != null)
                    {
                        roles.Add(role);
                    }
                }
            }
            return new LoginResponse(user, roles);

        }
    }
}
