using System;
using System.Collections.Generic;
using System.Linq;
using ShortBus;

namespace SampleWebApi.Data.Queries
{
    public class GetTodosQuery : IRequest<ViewTodos>
    {
        public int UserId { get; set; }
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, ViewTodos>
    {
        private readonly ISampleWebApiContext _context;

        public GetTodosQueryHandler(ISampleWebApiContext context)
        {
            _context = context;
        }

        public ViewTodos Handle(GetTodosQuery request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.UserId);

            if (user == null)
            {
                throw new Exception("User does not exists.");
            }

            var todos = _context.Todos
                    .Where(t => t.User.Id == request.UserId)
                    .ToList();

            return new ViewTodos
            {
                Todos = todos.Select(t => new ViewTodo
                {
                    Id = t.Id,
                    Detail = t.Detail,
                    Done = t.Done
                }).ToList(),
                UserId = user.Id
            };
        }
    }

    public class ViewTodos
    {
        public List<ViewTodo> Todos { get; set; }
        public int UserId { get; set; }
    }

    public class ViewTodo
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public bool Done { get; set; }
    }
}
