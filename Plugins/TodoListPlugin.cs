using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.SemanticKernel;

public class TodoListPlugin
{

    /// <summary>
    /// Marks a todo list item as complete.
    /// </summary>
    /// <param name="task">The task to complete.</param>
    /// <returns>A message indicating that the task has been marked as complete.</returns>
    /// <remarks>
    /// This method reads a JSON file containing the todo list, finds the specified task,
    /// marks it as complete, and then saves the updated JSON back to the file.
    /// </remarks>
    
    [KernelFunction, Description("Mark a todo list item as complete")]
    public static string CompleteTask([Description("The task to complete")] string task)
    {
        // Read the JSON file
        string jsonFilePath = $"{Directory.GetCurrentDirectory()}/Plugins/todo.txt";
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Parse the JSON content
        JsonNode todoData = JsonNode.Parse(jsonContent);

        // Find the task and mark it as complete
        JsonArray todoList = (JsonArray)todoData["todoList"];
        foreach (JsonNode taskNode in todoList)
        {
            if (taskNode["task"].ToString() == task)
            {
                taskNode["completed"] = true;
                break;
            }
        }

        // Save the modified JSON back to the file
        File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(todoData));
        return $"Task '{task}' marked as complete.";
    }
}