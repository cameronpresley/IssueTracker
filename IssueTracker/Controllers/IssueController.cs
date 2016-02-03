using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IssueTracker.Core;
using IssueTracker.DataAccess;
using Microsoft.FSharp.Core;

namespace IssueTracker.Controllers
{
    public class IssueController : ApiController
    {
        private readonly IssueLayer.IIssueDataAccess _dataAccess;

        public IssueController(IssueLayer.IIssueDataAccess dataAccess)
        {
            if (dataAccess == null) throw new ArgumentNullException(nameof(dataAccess));
            _dataAccess = dataAccess;
        }

        // GET: api/Issue
        public IEnumerable<Models.Issue> Get()
        {
            return _dataAccess.ReadAllIssues();
        }

        // GET: api/Issue/5
        public Models.Issue Get(int id)
        {
            var result = _dataAccess.ReadIssue(id);
            if (OptionModule.IsNone(result))
            {
                return null;
            }
            return result.Value;
        }

        // POST: api/Issue
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Issue/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Issue/5
        public void Delete(int id)
        {
            _dataAccess.DeleteIssue(id);
        }
    }
}
