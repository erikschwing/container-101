namespace TodoApp.Models;


public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "New ToDo item";
    public bool Done { get; set; } = false;
    public bool Deleted { get; set; } = false;
}

