using System;
using System.Collections.Generic;
using System.Linq;
using SampleWebApi.Data.Queries;
using SampleWebApi.Domain;
using ShortBus;

namespace SampleWebApi.Data.Commands
{
    public class PostTodosCommand : IRequest<ViewTodos>
    {
        public int UserId { get; set; }
        public List<ViewTodo> Todos { get; set; }
    }

    public class PostTodosCommandHandler : IRequestHandler<PostTodosCommand, ViewTodos>
    {
        private readonly ISampleWebApiContext _context;

        public PostTodosCommandHandler(ISampleWebApiContext context)
        {
            _context = context;
        }

        public ViewTodos Handle(PostTodosCommand request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.UserId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            foreach (var todo in request.Todos)
            {
                var x = new Todo
                {
                    Detail = todo.Detail,
                    Done = todo.Done,
                    User = user
                };
                _context.Todos.Add(x);
                _context.SaveChanges();
                todo.Id = x.Id;
            }

            return new ViewTodos
            {
                Todos = request.Todos,
                UserId = request.UserId
            };
        }
    }
}
