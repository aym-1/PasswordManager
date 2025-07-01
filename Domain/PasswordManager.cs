public class PasswordManager
{
    IPasswordGenerator _passwordGenerator;

    public PasswordManager(IPasswordGenerator passwordGenerator)
    {
        _passwordGenerator = passwordGenerator;
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
        var password = _passwordGenerator.GeneratePassword();

        var result = new PasswordRecord
        {
            Name = name,
            UserName = userName,
            Password = password,
            Url = url,
            Notes = string.Empty
        };
    }

    public PasswordRecord Read()
    {
        throw new NotImplementedException();
    }

    public PasswordRecord[] ReadAll()
    {
        throw new NotImplementedException();
    }

    public PasswordRecord Update()
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }
}