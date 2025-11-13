using Todo.Core;

var todoList = new TodoList();

ShowMenu();

while (true)
{
    Console.Write("Выберите команду: ");
    var command = Console.ReadLine()?.Trim();

    switch (command)
    {
        case "add":
            AddTodo(todoList);
            break;
        case "list":
            ShowAllTodos(todoList);
            break;
        case "done":
            MarkTodoDone(todoList);
            break;
        case "undone":
            MarkTodoUndone(todoList);
            break;
        case "remove":
            RemoveTodo(todoList);
            break;
        case "search":
            SearchTodos(todoList);
            break;
        case "exit":
            Console.WriteLine("Работа завершена.");
            return;
        default:
            Console.WriteLine("Неверная команда. Попробуйте снова.");
            break;
    }

    Console.WriteLine();
}

void ShowMenu()
{
    Console.WriteLine("Доступные команды:");
    Console.WriteLine("  add - Добавить задачу");
    Console.WriteLine("  list - Показать все задачи");
    Console.WriteLine("  done - Отметить задачу как выполненную");
    Console.WriteLine("  undone - Отметить задачу как невыполненную");
    Console.WriteLine("  remove - Удалить задачу");
    Console.WriteLine("  search - Найти задачи");
    Console.WriteLine("  exit - Выход из приложения");
}

void AddTodo(TodoList todoList)
{
    Console.Write("Введите название задачи: ");
    var title = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("Ошибка: название задачи не может быть пустым.");
        return;
    }

    var todo = todoList.Add(title);
    Console.WriteLine($"Задача добавлена. ID: {todo.Id}");
}

void ShowAllTodos(TodoList todoList)
{
    if (todoList.Count == 0)
    {
        Console.WriteLine("Нет задач.");
        return;
    }

    Console.WriteLine($"Всего задач: {todoList.Count}");
    foreach (var todo in todoList.Items)
    {
        Console.WriteLine($"  [{GetStatus(todo)}] {todo.Title} (ID: {todo.Id})");
    }
}

void MarkTodoDone(TodoList todoList)
{
    var todo = GetTodoById(todoList);
    if (todo == null)
    {
        return;
    }

    todo.MarkDone();
    Console.WriteLine($"Задача '{todo.Title}' отмечена как выполненная.");
}

void MarkTodoUndone(TodoList todoList)
{
    var todo = GetTodoById(todoList);
    if (todo == null)
    {
        return;
    }

    todo.MarkUndone();
    Console.WriteLine($"Задача '{todo.Title}' отмечена как невыполненная.");
}

void RemoveTodo(TodoList todoList)
{
    var todo = GetTodoById(todoList);
    if (todo == null)
    {
        return;
    }

    if (todoList.Remove(todo.Id))
    {
        Console.WriteLine($"Задача '{todo.Title}' удалена.");
    }
    else
    {
        Console.WriteLine("Ошибка: не удалось удалить задачу.");
    }
}

void SearchTodos(TodoList todoList)
{
    Console.Write("Введите текст для поиска: ");
    var searchText = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(searchText))
    {
        Console.WriteLine("Ошибка: текст для поиска не может быть пустым.");
        return;
    }

    var foundTodos = todoList.Find(searchText).ToList();
    if (foundTodos.Count == 0)
    {
        Console.WriteLine("Задачи не найдены.");
        return;
    }

    Console.WriteLine($"Найдено задач: {foundTodos.Count}");
    foreach (var todo in foundTodos)
    {
        Console.WriteLine($"  [{GetStatus(todo)}] {todo.Title} (ID: {todo.Id})");
    }
}

TodoItem? GetTodoById(TodoList todoList)
{
    if (todoList.Count == 0)
    {
        Console.WriteLine("Нет задач.");
        return null;
    }

    Console.Write("Введите ID задачи: ");
    var idInput = Console.ReadLine()?.Trim();

    if (!Guid.TryParse(idInput, out var id))
    {
        Console.WriteLine("Ошибка: неверный формат ID.");
        return null;
    }

    var todo = todoList.Items.FirstOrDefault(t => t.Id == id);
    if (todo == null)
    {
        Console.WriteLine("Задача с таким ID не найдена.");
        return null;
    }

    return todo;
}

string GetStatus(TodoItem todo)
{
    return todo.IsDone ? "X" : " ";
}
