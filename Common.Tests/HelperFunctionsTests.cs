using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Common.Interfaces;
using Database.Entities;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using Xunit;

namespace Common.Tests
{
    public class HelperFunctionsTests
    {
        private readonly IHelperFunctions _helperFunctions;

        public HelperFunctionsTests()
        {
            _helperFunctions = new HelperFunctions();
            JwtSettings.JwtSecretKey = "k7ML45YbnDtm1N0wAgAAcuf88qrq01f4Z4AlEJRi";
            JwtSettings.JwtIssuer = "https://localhost:5001";
            JwtSettings.JwtAudience = "https://localhost:5001";
        }

        [Fact]
        public void EvaluateCollectionName_ShouldReturnCorrectCollectionName()
        {
            // Arrange
            var expectedCollectionName = "users";

            // Act
            var actualCollectionName = _helperFunctions.EvaluateCollectionName<User>();

            // Assert
            Assert.Equal(expectedCollectionName, actualCollectionName);
        }

        [Fact]
        public void EvaluateCollectionName_ShouldReturnEmptyString()
        {
            // Arrange
            var expectedCollectionName = "";

            // Act
            var actualCollectionName = _helperFunctions.EvaluateCollectionName<HelperFunctionsTests>();

            // Assert
            Assert.Equal(expectedCollectionName, actualCollectionName);
        }

        [Fact]
        public void GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var id = "testId";
            var email = "test@example.com";

            // Act
            var token = _helperFunctions.GenerateToken(id, email);

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            Assert.Equal(id, jwtToken.Claims.First(claim => claim.Type == "id").Value);
            Assert.Equal(email, jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value);
        }

        [Fact]
        public void GetUserIdFromToken_ShouldReturnCorrectUserId()
        {
            // Arrange
            var id = "testId";
            var email = "test@example.com";
            var token = _helperFunctions.GenerateToken(id, email);

            // Act
            var userId = _helperFunctions.GetUserIdFromToken(token);

            // Assert
            Assert.Equal(id, userId);
        }

        [Fact]
        public void GetTokenValidationParameters_ShouldReturnCorrectParameters()
        {
            // Act
            var parameters = _helperFunctions.GetTokenValidationParameters();

            // Assert
            Assert.True(parameters.ValidateIssuer);
            Assert.False(parameters.ValidateAudience);
            Assert.True(parameters.ValidateLifetime);
            Assert.True(parameters.ValidateIssuerSigningKey);
            Assert.Equal(JwtSettings.JwtIssuer, parameters.ValidIssuer);
            Assert.Equal(JwtSettings.JwtAudience, parameters.ValidAudience);
            var expectedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtSecretKey));
            Assert.Equal(expectedKey.ToString(), parameters.IssuerSigningKey.ToString());
        }
    }
}
