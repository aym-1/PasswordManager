using System.Security.Cryptography;
using System.Text;

namespace AndrewCo.LearningProjects.PasswordManager.Tools;

public class PasswordGenerator : IPasswordGenerator
{
    private const string _allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890-=:;<>[]{}";

    private readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

    public string GeneratePassword(int length)
    {
        if (length <= 0)
        {
            throw new ArgumentException($"Argument {nameof(length)} is {length} that is too small.");
        }

        if (length > 1000)
        {
            throw new ArgumentException($"Argument {nameof(length)} is {length} that is too big.");
        }

        var result = new StringBuilder();

        while (result.Length < length)
        {
            var randomBytes = new byte[1];
            _randomNumberGenerator.GetBytes(randomBytes);

            var randomByte = randomBytes[0];
            if (randomByte < _allowedCharacters.Length)
            {
                var randomChar = _allowedCharacters[randomByte];
                result.Append(randomChar);
            }
        }

        return result.ToString();
    }
}