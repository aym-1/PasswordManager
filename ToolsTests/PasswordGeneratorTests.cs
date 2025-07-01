public class PasswordGeneratorTests
{
    [Fact]
    public void GeneratePassword_NegativeLength_ThrowArgumentException()
    {
        // Setup
        var sut = new PasswordGenerator();

        // Execute + Assert
        Assert.Throws<ArgumentException>(() => sut.GeneratePassword(-5));
    }

    [Fact]
    public void GeneratePassword_ZeroLength_ThrowArgumentException()
    {
        // Setup
        var sut = new PasswordGenerator();

        // Execute + Assert
        Assert.Throws<ArgumentException>(() => sut.GeneratePassword(0));
    }

    [Fact]
    public void GeneratePassword_MoreThan1000Length_ThrowArgumentException()
    {
        // Setup
        var sut = new PasswordGenerator();

        // Execute + Assert
        Assert.Throws<ArgumentException>(() => sut.GeneratePassword(1001));
    }

    [Fact]
    public void GeneratePassword_Length10_Generates10CharPassword()
    {
        // Setup
        var sut = new PasswordGenerator();

        // Execute
        var result = sut.GeneratePassword(10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Length);
    }      

    [Fact]
    public void GeneratePassword_CheckForAllAllowedCharacters_UsesOnlyAlowedCharacters()
    {
        // Setup
        var sut = new PasswordGenerator();

        // Execute
        var result = sut.GeneratePassword(10);

        // Assert
        var _allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890-=:;<>[]{}";
        Assert.NotNull(result);
        foreach(var ch in result)
        {
            Assert.Contains(ch, _allowedCharacters);
        }
    }      
}
