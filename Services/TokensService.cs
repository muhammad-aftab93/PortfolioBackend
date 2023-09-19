using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Services.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class TokensService : ITokensService
    {
        private readonly IGenericRepository<BlacklistedTokens> _repository;

        public TokensService(IGenericRepository<BlacklistedTokens> repository)
            => _repository = repository;            

        public async Task<bool> InvalidateTokenAsync(string token)
        {
            var tokenBlacklisted = new BlacklistedTokens()
            {
                Token = token,
                BlacklistedOn = DateTime.Now
            };
            await _repository.CreateAsync(tokenBlacklisted);
            return !string.IsNullOrEmpty(tokenBlacklisted.Id);
        }

        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            var blacklistedToken = await _repository.GetAsync(x => x.Token == token);
            return blacklistedToken?.Any() == true;
        }
    }
}
