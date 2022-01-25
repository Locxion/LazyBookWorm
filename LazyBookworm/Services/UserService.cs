using LazyBookworm.Common.Models;
using LazyBookworm.Database;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int> CreateUserAsync(UserAccount userAccount)
        {
            _context.Accounts.Add(userAccount);

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return 0;
            }
        }

        /// <summary>
        /// Gets all UserAccounts from Database
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserAccount>> GetAllAsync()
        {
            return await _context.Accounts.AsQueryable().ToListAsync();
        }

        /// <summary>
        /// Deletes UserAccount from Database
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserAsync(UserAccount userAccount)
        {
            _context.Accounts.Remove(userAccount);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return 0;
            }
        }
    }
}