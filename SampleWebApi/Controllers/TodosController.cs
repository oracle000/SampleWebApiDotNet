using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleWebApi.Data;
using SampleWebApi.Domain;

namespace SampleWebApi.Controllers
{
    public class TodosController : ApiController
    {
        private readonly ISampleWebApiContext _dbContext;

        public TodosController(ISampleWebApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HttpResponseMessage Get(int id)
        {
            if (id <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid id");
            }

            var todos = _dbContext.Todos.Where(t => t.User.Id == id).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, todos);
        }

        public HttpResponseMessage Post([FromBody]List<Todo> todos)
        {
            using (var context = new SampleWebApiContext())
            {
                foreach (var todo in todos)
                {
                    context.Todos.Add(todo);
                }
                context.SaveChanges();
            }

            return Request.CreateResponse(HttpStatusCode.Created, "");
        }
    }
}
