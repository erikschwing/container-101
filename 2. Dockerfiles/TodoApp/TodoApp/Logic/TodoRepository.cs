using System.Text.Json;
using TodoApp.Models;

namespace TodoApp.Logic;

public class TodoRepository
{

    private readonly string _filePath;

    public TodoRepository(string filePath)
    {
        _filePath = filePath;

        if (!File.Exists(_filePath))
            File.Create(_filePath).Dispose();
    }


    public List<TodoItem> GetAll()
    {
        return File.ReadAllLines(_filePath)
            .Select(line => JsonSerializer.Deserialize<TodoItem>(line)!)
            .ToList();
    }

    public void Add(TodoItem item)
    {
        var json = JsonSerializer.Serialize(item);
        File.AppendAllLines(_filePath, new[] { json });
    }

    public void Remove(TodoItem item)
    {
        var items = GetAll();
        items.RemoveAll(x => x.Id == item.Id);
        WriteAll(items);
    }

    public void Update(TodoItem item)
    {
        var items = GetAll();
        var index = items.FindIndex(x => x.Id == item.Id);
        if (index >= 0)
        {
            items[index] = item;
            WriteAll(items);
        }
    }

    private void WriteAll(List<TodoItem> items)
    {
        var lines = items.Select(i => JsonSerializer.Serialize(i));
        File.WriteAllLines(_filePath, lines);
    }


}
