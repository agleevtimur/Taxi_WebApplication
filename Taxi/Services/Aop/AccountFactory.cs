using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.Extensions.Logging;

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
