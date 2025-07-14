using Haelya.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Infrastructure.Logging.Security
{
    public class DummySecurityLogger : ISecurityLogger
    {
        public Task LogAsync(long? userId, string action, string? ipAddress = null, string? userAgent = null)
        {
            return Task.CompletedTask;
        }
    }
}
