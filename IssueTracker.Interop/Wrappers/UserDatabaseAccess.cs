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
        private readonly UserLayer.UserDataAccess _dbAccess;

        public UserDatabaseAccess()
        {
            _dbAccess = new UserLayer.UserDataAccess();
        }

        public void CreateUser(Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _dbAccess.createUser(user);
        }

        public Models.User ReadUser(int id)
        {
            var user = _dbAccess.readUser(id);
            return OptionModule.IsSome(user) ? user.Value : null;
        }

        public void UpdateUser(Models.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _dbAccess.updateUser(user);
        }

        public List<Models.User> ReadAllUsers()
        {
            return _dbAccess.readAllUsers().ToList();
        } 

        public void DeleteUser(int id)
        {
            _dbAccess.deleteUser(id);
        }
    }
}
