/// <summary>
/// This program demonstrates the use of the Microsoft Semantic Kernel to interact with an Azure OpenAI Chat Completion service.
/// It initializes the kernel with the necessary Azure OpenAI credentials and imports a plugin for managing a to-do list.
/// The program then invokes a method from the to-do list plugin to complete a task and prints the result to the console.
/// </summary>
/// <param name="yourDeploymentName">The name of your Azure OpenAI deployment.</param>
/// <param name="yourEndpoint">The endpoint URL for your Azure OpenAI service.</param>
/// <param name="yourApiKey">The API key for your Azure OpenAI service.</param>
/// <remarks>
/// Ensure that you have the necessary Azure OpenAI credentials and that the TodoListPlugin is properly implemented and available.
/// </remarks>


using Microsoft.SemanticKernel;


string yourDeploymentName = "";
string yourEndpoint = "";
string yourApiKey = "";

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    yourDeploymentName,
    yourEndpoint,
    yourApiKey,
    "gpt-35-turbo-16k");

var kernel = builder.Build();
kernel.ImportPluginFromType<TodoListPlugin>();

var result = await kernel.InvokeAsync<string>(
  "TodoListPlugin",
  "CompleteTask",
  new() { { "task", "Buy groceries" } }
);
Console.WriteLine(result);