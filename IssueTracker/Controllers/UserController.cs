using System.Collections.Generic;
using System.Web.Http;
using IssueTracker.Core;
using IssueTracker.Interop.Services;
using IssueTracker.Interop.Wrappers;

namespace IssueTracker.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<Models.User> Get()
        {
            return new List<Models.User>();
        }

        // GET: api/User/5
        public Models.User Get(int id)
        {
            var repository = new UserRepository(new UserDatabaseAccess());
            return repository.GetUser(id);
        }

        // POST: api/User
        public void Post([FromBody]string value)
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
