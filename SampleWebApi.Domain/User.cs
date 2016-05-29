using System;
using System.Collections.Generic;

namespace SampleWebApi.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Todo> Todos { get; set; }
    }
}
