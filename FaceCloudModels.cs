using System;
using System.Collections.Generic;

namespace facecloud.core
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Profile Profile { get; set; } = new Profile();
        public List<Post> Posts { get; set; } = new List<Post>();

        // нетривіальний метод 1: реєстрація з валідацією даних (умовні конструкції та винятки)
        public bool Register(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new ArgumentException("некоректний формат email");
            }
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                throw new ArgumentException("пароль має бути не менше 6 символів");
            }

            Email = email;
            Password = password;
            return true;
        }

        // нетривіальний метод 2: авторизація з перевіркою стану об'єкта
        public bool Login(string email, string password)
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new InvalidOperationException("користувач ще не зареєстрований");
            }
            return Email == email && Password == password;
        }

        // нетривіальний метод 3: створення публікації з обмеженням на довжину тексту
        public Post CreatePost(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("вміст публікації не може бути порожнім");
            }
            if (content.Length > 140)
            {
                throw new ArgumentException("текст публікації перевищує ліміт 140 символів");
            }

            var post = new Post
            {
                Id = Posts.Count + 1,
                Content = content,
                CreatedAt = DateTime.Now
            };
            Posts.Add(post);
            return post;
        }
    }

    public class Profile
    {
        public string Nickname { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }

        // метод оновлення профілю з перевіркою коректності нікнейму
        public void UpdateProfile(string nickname, string bio)
        {
            if (string.IsNullOrWhiteSpace(nickname) || nickname.Length > 20)
            {
                throw new ArgumentException("довжина нікнейму має бути від 1 до 20 символів");
            }
            Nickname = nickname;
            Bio = bio;
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // ось тут додано обгортку класу, тепер структура дужок правильна
    public class Program
    {
        public static void Main(string[] args)
        {

        }
    }
}