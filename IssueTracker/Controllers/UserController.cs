using System;
using System.Collections.Generic;
using System.Web.Http;
using IssueTracker.Core;
using IssueTracker.DataAccess;
using Microsoft.FSharp.Core;

namespace IssueTracker.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserLayer.IUserDataAccess _dataAcess;

        public UserController(UserLayer.IUserDataAccess dataAccess)
        {
            if (dataAccess == null) throw new ArgumentNullException(nameof(dataAccess));
            _dataAcess = dataAccess;
        }

        // GET: api/User
        public IEnumerable<Models.User> Get()
        {
            return _dataAcess.ReadAllUsers();
        }

        // GET: api/User/5
        public Models.User Get(int id)
        {
            var result = _dataAcess.ReadUser(id);
            if (OptionModule.IsNone(result))
                return null;
            return result.Value;
        }

        // POST: api/User
        public void Post([FromBody]Models.User user)
        {

        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
