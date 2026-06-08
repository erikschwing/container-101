using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Logic;
using TodoApp.Models;

namespace TodoApp.Pages;

public class IndexModel : PageModel
{
    private static readonly object _lock = new();

    private static int _nextId = 4;
    private static TodoRepository _todoRepository = new TodoRepository("Data/todo.txt");

    [BindProperty]
    public string? NewTitle { get; set; }

    public IReadOnlyList<TodoItem> Todos
    {
        get
        {
            lock (_lock)
            {
                return _todoRepository.GetAll()
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
            _todoRepository.Add(new TodoItem
            {
                Id = _nextId++,
                Title = NewTitle.Trim(),
                Done = false
            });
        }

        return RedirectToPage();
    }

    public IActionResult OnPostToggle(int id)
    {
        try
        {
            lock (_lock)
            {
                var todo = _todoRepository.GetAll().FirstOrDefault(t => t.Id == id);
                if (todo is not null)
                {
                    todo.Done = !todo.Done;
                    _todoRepository.Update(todo);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id)
    {
        lock (_lock)
        {
            var todo = _todoRepository.GetAll().FirstOrDefault(t => t.Id == id);
            if (todo is not null)
            {
                _todoRepository.Remove(todo);
            }
        }

        return RedirectToPage();
    }
}
