using AndrewCo.LearningProjects.PasswordManager.Tools;

namespace AndrewCo.LearningProjects.PasswordManager.Domain;

public class PasswordRecordCreator : IPasswordRecordCreator
{
    private readonly IPasswordGenerator _passwordGenerator;
    private const int PasswordLength = 20; // this is the recommended length these days

    public PasswordRecordCreator(IPasswordGenerator passwordGenerator)
    {
        _passwordGenerator = passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));
    }

    private string CreateNameFromUrl(string url)
    {
        // regex that removes http://, https://, www. and any trailing slashes
        // aso would remove .com, .net, etc. from the end of the url
        // the rest would be beutified by replacing dots with spaces
        return url; // TODO
    }

    public PasswordRecord Create(string url, string userName)
    {
        var name = CreateNameFromUrl(url);

        var password = _passwordGenerator.GeneratePassword(PasswordLength);
        var result = new PasswordRecord
        {
            Name = name,
            UserName = userName,
            Password = password,
            Url = url,
            Notes = string.Empty
        };

        return result;
    }
}