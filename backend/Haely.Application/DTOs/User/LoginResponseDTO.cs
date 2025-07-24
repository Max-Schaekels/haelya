using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Haelya.Application.DTOs.User
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; } = default!;
        public string AccessToken { get; set; } = string.Empty;
        [JsonIgnore] // 👈 ne sera pas sérialisé dans la réponse JSON
        public string RefreshToken { get; set; } = string.Empty;
    }
}
