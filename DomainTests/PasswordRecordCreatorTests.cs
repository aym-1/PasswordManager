using xUnit;

public class PasswordRecordCreatorTests
{
    [Fact]
    public void Create_NullUrl_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new PasswordRecordCreator(null));
    }
}