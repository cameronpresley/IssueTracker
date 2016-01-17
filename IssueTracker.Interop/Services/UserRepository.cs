﻿using System;
using IssueTracker.Core;
using IssueTracker.Interop.Wrappers;

namespace IssueTracker.Interop.Services
{
    public class UserRepository
    {
        private readonly UserDatabaseAccess _dbAccess;

        public UserRepository(UserDatabaseAccess dbAccess)
        {
            if (dbAccess == null) throw new ArgumentNullException(nameof(dbAccess));
            _dbAccess = dbAccess;
        }

        public void AddUser(Models.User user)
        {
            if (user == null) throw new ArgumentNullException();
            var result = _dbAccess.ReadUser(user.id);
            if (result == null)
                _dbAccess.CreateUser(user);
            else
            {
                _dbAccess.UpdateUser(user);
            }
        }

        public void DeleteUser(Models.User user)
        {
            _dbAccess.DeleteUser(user.id);
        }
    }
}
