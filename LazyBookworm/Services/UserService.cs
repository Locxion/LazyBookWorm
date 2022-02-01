using LazyBookworm.Common.Models;
using LazyBookworm.Common.Models.Common;
using LazyBookworm.Database;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyBookworm.Services
{
    public class UserService
    {
        private readonly LazyBookWormContext _context;
        private readonly ILog _logger = LogManager.GetLogger(typeof(UserService));

        public UserService(LazyBookWormContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates new UserAccount in Database
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public ActionResult<UserAccount> CreateUserAsync(UserAccount userAccount)
        {
            _context.Accounts.Add(userAccount);
            try
            {
                _context.SaveChanges();

                return ActionResult<UserAccount>.Success(userAccount);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return ActionResult<UserAccount>.SystemError("There was an Database Error!", $"{e.Message}: {e.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Gets all UserAccounts from Database
        /// </summary>
        /// <returns></returns>
        public List<UserAccount> GetAll()
        {
            return _context.Accounts.AsQueryable().ToList();
        }

        /// <summary>
        /// Deletes UserAccount from Database
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public ActionResult<UserAccount> DeleteUserAsync(UserAccount userAccount)
        {
            _context.Accounts.Remove(userAccount);

            try
            {
                _context.SaveChanges();

                return ActionResult<UserAccount>.Success(userAccount);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return ActionResult<UserAccount>.SystemError("There was an Database Error!", $"{e.Message}: {e.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Deletes User from Database with given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<UserAccount> DeleteUserA(Guid id)
        {
            var account = GetAccount(id);
            if (account == null)
                return ActionResult<UserAccount>.Error("User Account not found!");
            _context.Accounts.Remove(account);

            try
            {
                _context.SaveChanges();

                return ActionResult<UserAccount>.Success(account);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return ActionResult<UserAccount>.SystemError("There was an Database Error!", $"{e.Message}: {e.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Gets UserAccount for given Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>UserAccount</returns>
        public UserAccount GetAccount(Guid id)
        {
            return _context.Accounts.AsQueryable().FirstOrDefault(x => x.ID == id);
        }
    }
}