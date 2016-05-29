namespace SampleWebApi.Domain
{
    public class Todo
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public bool Done { get; set; }

        public User User { get; set; }
    }
}
