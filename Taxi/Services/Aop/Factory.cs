
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Aop
{
    public class Factory<T1,T2>
        where T2: class, T1
    {
        private readonly ILogger _logger;
        private readonly T2 _implementation;

        public Factory(ILogger logger, T2 implementation)
        {
            _logger = logger;
            _implementation = implementation;
        }

        public T1 Create()
        {
            return LoggingAdvice<T1>.Create(
                _implementation,
                s => _logger.LogInformation("Info:" + s),
                s => _logger.LogInformation("Error:" + s),
                o => o?.ToString());
        }
    }
}
