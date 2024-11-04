using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PAWPMD.Models;
using PAWPMD.Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// Interface for generating JWT tokens.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified user with their associated roles.
    /// </summary>
    /// <param name="loginResponse">A <see cref="LoginResponse"/> object containing the user and their roles.</param>
    /// <returns>The generated JWT token as a string.</returns>
    string Generate(LoginResponse loginResponse);

    /// <summary>
    /// Gets the current user from the HTTP context.
    /// </summary>
    /// <returns>A <see cref="User"/> object representing the current user, or null if not found.</returns>
    User GetCurrentUser();
}

/// <summary>
/// Class that generates JWT tokens for user authentication.
/// </summary>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
    /// </summary>
    /// <param name="config">The application configuration.</param>
    /// <param name="httpContextAccessor">The accessor for the current HTTP context.</param>
    public JwtTokenGenerator(IConfiguration config, IHttpContextAccessor httpContextAccessor)
    {
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Generates a JWT token for the specified user with their associated roles.
    /// </summary>
    /// <param name="loginResponse">A <see cref="LoginResponse"/> object containing the user and their roles.</param>
    /// <returns>The generated JWT token as a string.</returns>
    public string Generate(LoginResponse loginResponse)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create claims
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginResponse.User.UserId.ToString()),
                new Claim(ClaimTypes.Name, loginResponse.User.Username),
                new Claim(ClaimTypes.Email, loginResponse.User.Email),
                new Claim(ClaimTypes.GivenName, loginResponse.User.Name),
                new Claim(ClaimTypes.Surname, loginResponse.User.LastName)
            };

        // Add a claim for each role the user has
        foreach (var role in loginResponse.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        // Create the token
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Gets the current user from the HTTP context.
    /// </summary>
    /// <returns>A <see cref="User"/> object representing the current user, or null if not found.</returns>
    public User GetCurrentUser()
    {
        var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var username = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = identity.FindFirst(ClaimTypes.Email)?.Value;
            var givenName = identity.FindFirst(ClaimTypes.GivenName)?.Value;
            var surname = identity.FindFirst(ClaimTypes.Surname)?.Value;

            return new User
            {
                Username = username,
                Email = email,
                Name = givenName,
                LastName = surname,
            };
        }

        return null;
    }
}
