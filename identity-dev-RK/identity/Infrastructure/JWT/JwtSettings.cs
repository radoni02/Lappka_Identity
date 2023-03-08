using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JWT
{
    public class JwtSettings 
    {
        public string Issuer { get; set; }
        public string HmacSecretKey { get; set; }
        public int ExpiryMInutes { get; set; }
        public int ExpiryDays { get; set; }
        
        public bool UseRsa { get; set; }
        public string RsaPrivateKeyXML { get; set; }
        public string RsaPublicKeyXML { get; set; }
    }
}
