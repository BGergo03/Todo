namespace Data.Entities;

public class User
{
    public ulong Id { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }

    public IList<Todo> Todos { get; set; } = new List<Todo>();
}