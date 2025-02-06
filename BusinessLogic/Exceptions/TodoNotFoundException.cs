namespace BusinessLogic.Exceptions;

public class TodoNotFoundException : Exception
{
    public TodoNotFoundException(string message) : base(message)
    {
        
    }
}