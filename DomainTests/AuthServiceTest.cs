// using System;
// using xUnit;
// using AndrewCo.LearningProjects.PasswordManager;
// using System.Linq;

// namespace AndrewCo.LearningProjects.PasswordManager.Domain.Tests;

// public class AuthServiceTests
// {
//     [Fact]
//     public void Hash_SameInputSameSalt_ReturnsSameHash()
//     {
//         string password = "Test123!";
//         string salt = "abc123";

//         string hash1 = CallHash(password, salt);
//         string hash2 = CallHash(password, salt);

//         Assert.Equal(hash1, hash2);
//     }

//     [Fact]
//     public void SetupMasterPassword_SetsCorrectHashAndSalt()
//     {
//         var io = new TestIOService();
//         string testPass = "Valid1!";
//         string salt;

//         io.InputQueue.Enqueue(testPass);     // First entry
//         io.InputQueue.Enqueue(testPass);     // Confirm entry

//         AuthServiceTestWrapper.SetupMasterPassword(io);

//         Assert.True(io.FileStore.ContainsKey("salt.txt"));
//         Assert.True(io.FileStore.ContainsKey("master_hash.txt"));

//         salt = io.FileStore["salt.txt"];
//         string expectedHash = CallHash(testPass, salt);

//         Assert.Equal(expectedHash, io.FileStore["master_hash.txt"]);
//     }

//     [Fact]
//     public void Authenticate_WithCorrectPassword_ReturnsTrue()
//     {
//         var io = new TestIOService();
//         string password = "Correct1!";
//         string salt = PasswordUtils.GenerateSalt();
//         string hash = CallHash(password, salt);

//         io.FileStore["salt.txt"] = salt;
//         io.FileStore["master_hash.txt"] = hash;

//         io.InputQueue.Enqueue(password); // Correct password

//         bool result = AuthServiceTestWrapper.Authenticate(io);
//         Assert.True(result);
//     }

//     [Fact]
//     public void Authenticate_WithIncorrectPassword_ReturnsFalse()
//     {
//         var io = new TestIOService();
//         string correct = "Correct1!";
//         string wrong = "Wrong1!";
//         string salt = PasswordUtils.GenerateSalt();
//         string hash = CallHash(correct, salt);

//         io.FileStore["salt.txt"] = salt;
//         io.FileStore["master_hash.txt"] = hash;

//         io.InputQueue.Enqueue(wrong);
//         io.InputQueue.Enqueue(wrong);
//         io.InputQueue.Enqueue(wrong);

//         bool result = AuthServiceTestWrapper.Authenticate(io);
//         Assert.False(result);
//     }

//     private string CallHash(string password, string salt)
//     {
//         return AuthServiceTestWrapper.Hash(password, salt);
//     }
// }
