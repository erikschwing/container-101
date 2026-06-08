using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Database;
using TodoApp.Models;

namespace TodoApp.Pages;

public class IndexModel : PageModel
{
    private static readonly object _lock = new();

    private readonly TodoContext _todoContext;

    [BindProperty]
    public string? NewTitle { get; set; }

    public IndexModel(TodoContext todoContext)
    {
        _todoContext = todoContext;
    }

    public IReadOnlyList<TodoItem> Todos
    {
        get
        {
            lock (_lock)
            {
                return _todoContext.TodoItems
                    .Where(x => !x.Deleted)
                    .OrderBy(t => t.Done)
                    .ThenBy(t => t.Id)
                    .ToList();
            }
        }
    }

    public int TotalCount => Todos.Count;
    public int DoneCount => Todos.Count(t => t.Done);
    public int OpenCount => Todos.Count(t => !t.Done);

    public void OnGet()
    {
    }

    public IActionResult OnPostAdd()
    {
        if (string.IsNullOrWhiteSpace(NewTitle))
        {
            return Page();
        }

        lock (_lock)
        {
            _todoContext.TodoItems.Add(new TodoItem
            {
                Title = NewTitle.Trim(),
                Done = false
            });
            _todoContext.SaveChanges();
        }

        return RedirectToPage();
    }

    public IActionResult OnPostToggle(int id)
    {
        lock (_lock)
        {
            var todo = _todoContext.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo is not null)
            {
                todo.Done = !todo.Done;
                _todoContext.SaveChanges();
            }
        }

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        lock (_lock)
        {
            var todo = _todoContext.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo is not null)
            {
                todo.Deleted = true;
                _todoContext.SaveChanges();
            }
        }

        return RedirectToPage();
    }
}
