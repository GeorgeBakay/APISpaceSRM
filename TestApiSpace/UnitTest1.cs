using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
namespace TestApiSpace
{
    public class LoginControllerTests
    {
       
            private readonly HttpClient _client;

            public LoginControllerTests()
            {
                // Створіть HttpClient для використання у всіх тестах
                _client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7173") // Змініть на адресу вашого локального сервера
                };
            }

            [Fact]
            public async Task TestLogin_ValidCredentials_ReturnsToken()
            {
                // Arrange
                var user = new User
                {
                    userName = "Space",
                    password = "Flyboxx"
                };

                // Serialize user object to JSON
                var json = System.Text.Json.JsonSerializer.Serialize(user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _client.PostAsync("/Login", data);

                // Assert
                response.EnsureSuccessStatusCode(); // Переконайтеся, що запит успішний (код відповіді 200-299)

                // Deserialize response content to retrieve token
                var responseContent = await response.Content.ReadAsStringAsync();
                Assert.NotNull(responseContent);

                // Additional assertions as needed
            }

            [Fact]
            public async Task TestLogin_InvalidCredentials_ReturnsUnauthorized()
            {
                // Arrange
                var user = new User
                {
                    userName = "InvalidUser",
                    password = "InvalidPassword"
                };

                var json = System.Text.Json.JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                // Act
                var response = await _client.PostAsync("/Login", data);

                // Assert
                Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        // Клас користувача
        public class User
        {
            public string userName { get; set; }
            public string password { get; set; }
        }

    
}