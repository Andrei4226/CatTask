using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand
        {
            //two aliases for each option
            new Option<string>(new[] {"--output", "-o"}, "Output file path to write the image from API") { IsRequired = true },
            new Option<string>(new[] {"--text", "-t"}, "Text to be included in the image")
        };

        rootCommand.Description = "Download a cat image from cataas.com(APIs) and save it to computer";
        rootCommand.Handler = CommandHandler.Create<string, string>(async (output, text) =>
        {
            await DownloadCatImageAsync(output, text);
        });

        return await rootCommand.InvokeAsync(args);
    }

    //params = outputPath and text(optional for the user) to add on cat image
    static async Task DownloadCatImageAsync(string outputPath, string textForImage)
    {
        using var client = new HttpClient();
        //link for API(to generate a cat image without text is by default)
        string baseUrl = "https://cataas.com/cat";
        if (!string.IsNullOrEmpty(textForImage))
        {
            baseUrl += $"/says/{Uri.EscapeDataString(textForImage)}";
        }
        // store the content
        var response = await client.GetAsync(baseUrl);
        // test the status code(200 is successful)
        // throws an exception if the HTTP response status is an error
        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
        //copy the Stream content in fileStream
        await stream.CopyToAsync(fileStream);
        Console.WriteLine($"Image saved: {outputPath}");
    }
}

// IDE Microsoft Visual Studio: ConsoleApp(application that can run on .NET on Windows)
// Run:
// for file.jpg, I ran the following command in the terminal: dotnet run --project "task.csproj" -o "file.jpg" -t "I like cats"
// for file2.jpg, I ran the following command in the terminal: dotnet run --project "task.csproj" -o "file2.jpg"