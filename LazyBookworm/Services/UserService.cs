using LazyBookworm.Common.Models;
using LazyBookworm.Database;
using log4net;
using System;

namespace LazyBookworm.Services
{
    public static class UserService
    {
        private static ILog _logger = LogManager.GetLogger(typeof(UserService));

        public static int CreateUser(UserAccount userAccount, LazyBookWormContext context)
        {
            context.Accounts.Add(userAccount);

            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
    }
}