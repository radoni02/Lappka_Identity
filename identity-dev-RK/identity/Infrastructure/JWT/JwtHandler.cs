using Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JWT
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _settings;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private SecurityKey _issuerSigningKey;
        private SigningCredentials _signingCredentials;
        public TokenValidationParameters Parameters { get; private set; }
        public JwtHandler(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
            if(_settings.UseRsa)
            {
                InitializeRsa();
            }
            else
            {
                InitializeHmac();
            }
            InitializeJwtParameters();
        }
        private void InitializeRsa()
        {
            RSA publicRsa = RSA.Create();
            var publicKeyPem = File.ReadAllText(_settings.RsaPublicKeyXML);
            publicRsa.ImportFromPem(publicKeyPem);
            _issuerSigningKey = new RsaSecurityKey(publicRsa);

            if (string.IsNullOrWhiteSpace(_settings.RsaPublicKeyXML))
            {
                return;
            }
            RSA privateRsa = RSA.Create();
            var privateKeyPem = File.ReadAllText(_settings.RsaPrivateKeyXML);
            privateRsa.ImportFromPem(privateKeyPem);
            var privateKey = new RsaSecurityKey(privateRsa);
                _signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
            if (string.IsNullOrWhiteSpace(_settings.RsaPrivateKeyXML))
            {
                return;
            }  
        }

        private void InitializeHmac()
        {
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.HmacSecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }


        private void InitializeJwtParameters()
        {
            Parameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = _settings.Issuer,
                IssuerSigningKey = _issuerSigningKey,
            };
        
        }

        public JwtSecurityToken DecodeToken(string token)
        {
            return _jwtSecurityTokenHandler.ReadJwtToken(token);
        }
        public Guid ReadAccessToken(string token)
        {
            var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);/*_jwtHandler.ReadJwtToken(token)*/
            var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdClaim.Value, out var id);
            return id;
        }

        public (string,Guid) ReadAccessTokenForEmail(string token)
        {
            var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
            var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtClaims.UserId);
            var emailClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtClaims.NewEmail);
            Guid.TryParse(userIdClaim.Value, out var id);
            return (emailClaim.Value,id);
        }

        public int GetExpiredDays()
        {
            return _settings.ExpiryDays;
        }
        public string CreateAccessToken(Guid userId,string role)
        {
            var expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMInutes);
            var issuer = _settings.Issuer ?? string.Empty;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                new Claim(ClaimTypes.Role,role)
            };
            var jwtPayload = new JwtSecurityToken(issuer, issuer, claims, expires: expires, signingCredentials: _signingCredentials);
            var token = _jwtSecurityTokenHandler.WriteToken(jwtPayload);
            return token;

        }

        public string GenerateConfirmUpdateEmailToken(Guid id, string newEmail)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Claims = new Dictionary<string, object>()
            {
                { JwtClaims.UserId, id },
                { JwtClaims.NewEmail, newEmail }
            }
            };
            var handler = new JwtSecurityTokenHandler();
            var secToken = new JwtSecurityTokenHandler().CreateToken(descriptor);
            var token = handler.WriteToken(secToken);

            return token;

        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        //public SigningCredentials CreateSigningCredentials(JwtSettings _settings)
        //{
        //    RSA privateRsa = RSA.Create();
        //    var privateKeyPem = File.ReadAllText(_settings.RsaPrivateKeyXML);
        //    privateRsa.ImportFromPem(privateKeyPem);
        //    var privateKey = new RsaSecurityKey(privateRsa);
        //    return new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
        //}
    }
}
