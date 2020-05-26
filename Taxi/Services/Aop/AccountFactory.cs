

using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi_Database.Models;

namespace Services.Aop
{
    public class AccountFactory
    {
        private readonly ILogger logger;
        private readonly Account account;
        public AccountFactory(ILogger logger, Account account)
        {
            this.logger = logger;
            this.account = account;
        }
        public IAccountController Create()
        {
            return LoggingAdvice<IAccountController>.Create(
                account,
                s => logger.LogInformation("Info:" + s),
                s => logger.LogInformation("Error:" + s),
                o => o?.ToString());
        }
    }
}
