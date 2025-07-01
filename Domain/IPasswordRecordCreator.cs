namespace AndrewCo.LearningProjects.PasswordManager.Domain;

public interface IPasswordRecordCreator
{
    PasswordRecord Create(string url, string userName);
}