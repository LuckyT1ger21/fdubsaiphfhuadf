using System;
using System.Text.Encodings.Web;
using System.Text.Json;

class FileWriter
{
    private string fileLocation =
        @"C:\Users\tobias.stridsberg\Documents\Bus_Time_Table+Weather\Webside\test.json"; // default file location

    // Change the file which is written to
    public string FileLocation
    {
        set { fileLocation = value; }
    }

    // Formats a file into a string that is JsonString that can be written to a Json
    private string FormatJson(List<object> list)
    {
        // allows åöä to be written as a json and makes it pretty print
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
        };
        try
        {
            return JsonSerializer.Serialize(list, options); // formats the list as a json
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return JsonSerializer.Serialize("Api Failure", options);
        }
    }

    /// <summary>
    /// Writes a list onto the file that is specified within the class
    /// </summary>
    /// <param name="list">The list that will be written to a the file</param>
    public void Write(List<object> list)
    {
        string jsonString = FormatJson(list);
        try
        {
            File.WriteAllText(fileLocation, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
