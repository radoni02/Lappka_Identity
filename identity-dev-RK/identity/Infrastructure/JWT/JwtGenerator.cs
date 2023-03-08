using Application.Services;
using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JWT
{
    public class JwtGenerator 
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtSettings _settings;
        private readonly SigningCredentials _signingCredentials;

        public JwtGenerator(SignInManager<AppUser> signInManager, JwtSettings settings)
        {
            _signInManager = signInManager;
            _settings = settings;
        }

        public async Task<string> GenerateToken(SecurityTokenDescriptor descriptor)
        {
            var handler = new JwtSecurityTokenHandler();
            var secToken = new JwtSecurityTokenHandler().CreateToken(descriptor);
            var token = handler.WriteToken(secToken);
            return token;
        }

        public async Task<string> GenerateAccessToken(AppUser user)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = (await _signInManager.CreateUserPrincipalAsync(user)).Identities.First(),
                Expires = DateTime.Now.AddMinutes(_settings.ExpiryMInutes),
                SigningCredentials = _signingCredentials,
            };

            return await GenerateToken(descriptor);
        }
    }
}
