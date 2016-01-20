using System;
using System.Collections.Generic;
using System.Web.Http;
using IssueTracker.Core;
using IssueTracker.Interop.Interfaces;

namespace IssueTracker.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _repository = repository;
        }

        // GET: api/User
        public IEnumerable<Models.User> Get()
        {
            return _repository.GetUsers();
        }

        // GET: api/User/5
        public Models.User Get(int id)
        {
            return _repository.GetUser(id);
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
