using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Interfaces
{
    public interface ISecurityLogger
    {
        Task LogAsync(long? userId, string action, string? ipAddress = null, string? userAgent = null);
    }
}
