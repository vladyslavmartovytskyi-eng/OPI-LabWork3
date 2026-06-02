using System;
using Xunit;
using facecloud.core;

namespace facecloud.tests
{
    public class FaceCloudTests
    {
        // ==========================================
        // тести для методу Register (EP + BVA)
        // ==========================================

        [Fact]
        public void Test_Register_ValidData_ReturnsTrue()
        {
            // Arrange (підготовка початкових умов)
            var user = new User();

            // Act (виконання дії, що тестується)
            var result = user.Register("student@nure.ua", "password123");

            // Assert (перевірка результату)
            // техніка: позитивний тест, ЕР (допустимий клас значень)
            Assert.True(result);
        }

        [Fact]
        public void Test_Register_InvalidEmail_ThrowsArgumentException()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            // техніка: негативний тест, ЕР (недопустимий формат пошти без знака @)
            Assert.Throws<ArgumentException>(() => user.Register("invalidemail.com", "password123"));
        }

        [Fact]
        public void Test_Register_ShortPassword_ThrowsArgumentException()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            // техніка: негативний тест, BVA (межа довжини 6, перевіряємо значення 5)
            Assert.Throws<ArgumentException>(() => user.Register("student@nure.ua", "12345"));
        }

        // ==========================================
        // тести для методу Login
        // ==========================================

        [Fact]
        public void Test_Login_NotRegistered_ThrowsInvalidOperationException()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            // техніка: негативний тест (перевірка поведінки системи до реєстрації)
            Assert.Throws<InvalidOperationException>(() => user.Login("student@nure.ua", "123456"));
        }

        [Fact]
        public void Test_Login_CorrectCredentials_ReturnsTrue()
        {
            // Arrange
            var user = new User();
            user.Register("student@nure.ua", "password123");

            // Act
            var result = user.Login("student@nure.ua", "password123");

            // Assert
            // техніка: позитивний тест (збіг логіна та пароля)
            Assert.True(result);
        }

        [Fact]
        public void Test_Login_WrongPassword_ReturnsFalse()
        {
            // Arrange
            var user = new User();
            user.Register("student@nure.ua", "password123");

            // Act
            var result = user.Login("student@nure.ua", "wrongpass");

            // Assert
            // техніка: негативний тест (неправильний пароль)
            Assert.False(result);
        }

        // ==========================================
        // тести для методу UpdateProfile (EP + BVA)
        // ==========================================

        [Fact]
        public void Test_UpdateProfile_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            var profile = new Profile();

            // Act
            profile.UpdateProfile("max_kh", "hello from nure!");

            // Assert
            // техніка: позитивний тест, ЕР (допустима довжина нікнейму)
            Assert.Equal("max_kh", profile.Nickname);
        }

        [Fact]
        public void Test_UpdateProfile_EmptyNickname_ThrowsArgumentException()
        {
            // Arrange
            var profile = new Profile();

            // Act & Assert
            // техніка: негативний тест, BVA (нижня межа порожнього рядка)
            Assert.Throws<ArgumentException>(() => profile.UpdateProfile("", "some bio"));
        }

        [Fact]
        public void Test_UpdateProfile_TooLongNickname_ThrowsArgumentException()
        {
            // Arrange
            var profile = new Profile();
            string longNickname = new string('a', 21);

            // Act & Assert
            // техніка: негативний тест, BVA (верхня межа 20 знаків, перевіряємо значення 21)
            Assert.Throws<ArgumentException>(() => profile.UpdateProfile(longNickname, "some bio"));
        }

        // ==========================================
        // тести для методу CreatePost (EP + BVA)
        // ==========================================

        [Fact]
        public void Test_CreatePost_ValidContent_CreatesPostSuccessfully()
        {
            // Arrange
            var user = new User();

            // Act
            var post = user.CreatePost("hello world, this is facecloud!");

            // Assert
            // техніка: позитивний тест, ЕР (коректний вміст публікації)
            Assert.NotNull(post);
            Assert.Equal("hello world, this is facecloud!", post.Content);
            Assert.Single(user.Posts);
        }

        [Fact]
        public void Test_CreatePost_TooLongContent_ThrowsArgumentException()
        {
            // Arrange
            var user = new User();
            string longContent = new string('x', 141);

            // Act & Assert
            // техніка: негативний тест, BVA (межа довжини 140 знаків, перевіряємо значення 141)
            Assert.Throws<ArgumentException>(() => user.CreatePost(longContent));
        }
    }
}