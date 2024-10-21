using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Architecture.Authentication
{
    /// <summary>
    /// Interface that defines methods for password hashing and verification.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes a given password using a secure hashing algorithm.
        /// </summary>
        /// <param name="password">The plain text password to hash.</param>
        /// <returns>A hashed password, including the salt and hash, as a string.</returns>
        string Hash(string password);

        /// <summary>
        /// Verifies if the input password matches the provided hashed password.
        /// </summary>
        /// <param name="passwordHash">The previously hashed password.</param>
        /// <param name="inputPassword">The plain text password to verify.</param>
        /// <returns>True if the password matches, otherwise false.</returns>
        bool Verify(string passwordHash, string inputPassword);
    }

    /// <summary>
    /// Class that implements IPasswordHasher, providing functionality to hash and verify passwords.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        // Constants for hashing parameters
        private const int SaltSize = 128 / 8; // Size of the salt in bytes
        private const int KeySize = 256 / 8;  // Size of the hash in bytes
        private const int Iterations = 1000;  // Number of iterations for the hash function
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256; // Algorithm used for hashing
        private const char Delimiter = ';'; // Delimiter to separate salt and hash in the final stored string

        /// <summary>
        /// Hashes a password using PBKDF2 with SHA-256, a salt, and a defined number of iterations.
        /// </summary>
        /// <param name="password">The plain text password to be hashed.</param>
        /// <returns>A string containing the base64-encoded salt and hash, separated by a delimiter.</returns>
        /// <example>
        /// Example usage:
        /// <code>
        /// var hasher = new PasswordHasher();
        /// var hashedPassword = hasher.Hash("mySecurePassword");
        /// </code>
        /// </example>
        public string Hash(string password)
        {
            // Generate a random salt
            var salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Derive the hash from the password and salt
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

            // Return the salt and hash as a single string, separated by the delimiter
            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        /// <summary>
        /// Verifies whether the input password matches the stored hashed password.
        /// </summary>
        /// <param name="passwordHash">The stored password hash, which includes both the salt and the hash.</param>
        /// <param name="inputPassword">The plain text password to be verified.</param>
        /// <returns>True if the input password matches the stored hash, otherwise false.</returns>
        /// <example>
        /// Example usage:
        /// <code>
        /// var hasher = new PasswordHasher();
        /// bool isMatch = hasher.Verify(storedHash, "mySecurePassword");
        /// </code>
        /// </example>
        public bool Verify(string passwordHash, string inputPassword)
        {
            // Split the stored password hash into salt and hash components
            var elements = passwordHash.Split(Delimiter);

            // Convert the base64-encoded salt and hash back to byte arrays
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            // Derive the hash for the input password using the same salt and parameters
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithmName, KeySize);

            // Compare the computed hash with the stored hash using a time-constant comparison
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
