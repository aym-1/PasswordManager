namespace AndrewCo.LearningProjects.PasswordManager.Tools;

public interface IPasswordGenerator
{
    string GeneratePassword(int length);
}