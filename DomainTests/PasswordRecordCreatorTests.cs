using AndrewCo.LearningProjects.PasswordManager.Domain;
using AndrewCo.LearningProjects.PasswordManager.Tools;
using Moq;

namespace AndrewCo.LearningProjects.PasswordManager.DomainTests;


public class PasswordRecordCreatorTests
{
    [Fact]
    public void Create_NullPasswordGnenerator_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new PasswordRecordCreator(null));
    }

    // test url null and empty, userName null and empy,

    [Fact]
    public void Create_ValidPasswordRecord_ValidPassword()
    {
        // Setup
        var passwordGeneratorMock = new Mock<IPasswordGenerator>();
        passwordGeneratorMock
            .Setup(x => x.GeneratePassword(It.IsAny<int>()))
            .Returns("12345678901234567890");

        var sut = new PasswordRecordCreator(passwordGeneratorMock.Object);

        // Execute
        var result = sut.Create(url: "someUrl", userName: "someUser");

        Assert.NotNull(result);
        Assert.Equal("someUrl", result.Name);
        Assert.Equal("someUser", result.UserName);
        Assert.Equal("12345678901234567890", result.Password);
        Assert.Equal("someUrl", result.Url);
        Assert.Empty(result.Notes);
    }    
}