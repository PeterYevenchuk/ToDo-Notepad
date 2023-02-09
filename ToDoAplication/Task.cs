using System;

namespace ToDoAplication;

[Serializable]
public class Task
{
    public string Name { get; set; }
    public bool IsCompleted { get; set; }
    public string Description { get; set; }
}
