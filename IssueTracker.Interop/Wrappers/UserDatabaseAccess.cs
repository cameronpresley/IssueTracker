using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Core;
using IssueTracker.DataAccess;
using Microsoft.FSharp.Core;

namespace IssueTracker.Interop.Wrappers
{
    public class UserDatabaseAccess
    {
        private readonly UserLayer.IUserDataAccess _dbAccess;

        public UserDatabaseAccess()
        {
            _dbAccess = new UserLayer.UserDataAccess();
        }

        public void CreateUser(Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _dbAccess.CreateUser(user);
        }

        public Models.User ReadUser(int id)
        {
            var user = _dbAccess.ReadUser(id);
            return OptionModule.IsSome(user) ? user.Value : null;
        }

        public void UpdateUser(Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _dbAccess.UpdateUser(user);
        }

        public List<Models.User> ReadAllUsers()
        {
            return _dbAccess.ReadAllUsers().ToList();
        } 

        public void DeleteUser(int id)
        {
            _dbAccess.DeleteUser(id);
        }
    }
}
