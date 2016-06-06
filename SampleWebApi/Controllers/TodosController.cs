using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleWebApi.Data;
using SampleWebApi.Domain;
using System.Data.Entity;

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

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User does not exists.");
            }

            var todos =
                _dbContext.Todos
                    .Where(t => t.User.Id == id)
                    .ToList();

            var data = new ViewTodos
            {
                Todos = todos.Select(t => new ViewTodo
                {
                    Id = t.Id, Detail = t.Detail, Done = t.Done
                }).ToList(),
                User = new ViewUser
                {
                    Id = user.Id
                }
            };

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public class ViewTodos
        {
            public List<ViewTodo> Todos { get; set; }
            public ViewUser User { get; set; }
        }

        public class ViewTodo
        {
            public int Id { get; set; }
            public string Detail { get; set; }
            public bool Done { get; set; }
        }

        public class ViewUser
        {
            public int Id { get; set; }
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
