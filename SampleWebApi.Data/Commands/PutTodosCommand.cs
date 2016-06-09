using System;
using System.Collections.Generic;
using System.Linq;
using SampleWebApi.Data.Queries;
using ShortBus;
using System.Data.Entity; 

namespace SampleWebApi.Data.Commands
{
    public class PutTodosCommand : IRequest<ViewTodos>
    {
        public int UserId { get; set; }
        public List<ViewTodo> Todos { get; set; } 
    }

    public class PutTodosCommandHandler : IRequestHandler<PutTodosCommand, ViewTodos>
    {
        private readonly ISampleWebApiContext _context;

        public PutTodosCommandHandler(ISampleWebApiContext context)
        {
            _context = context;
        }

        public ViewTodos Handle(PutTodosCommand request)
        {
            foreach (var todo in request.Todos)
            {
                var x = _context.Todos.Include(t => t.User).FirstOrDefault(t => t.Id == todo.Id);
                
                if (x == null)
                    continue;
                if (x.User.Id != request.UserId)
                    throw new Exception("User's task not found.");

                x.Detail = todo.Detail;
                x.Done = todo.Done;
            }

            _context.SaveChanges();
            return new ViewTodos {Todos = request.Todos, UserId = request.UserId};
        }
    }
}
