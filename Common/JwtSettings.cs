using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class JwtSettings
    {
        public static string JwtIssuer { get; set; } = null!;
        public static string JwtAudience { get; set; } = null!;
        public static string JwtSecretKey { get; set; } = null!;
    }
}
