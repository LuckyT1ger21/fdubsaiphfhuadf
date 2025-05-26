using System;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

class Program
{
    static void Main()
    {
        var nestedList = InitializeNestedList();
        Location NackaStrand = new Location("740024852", "59.307014", "18.228745");
        StopInfo Information = new StopInfo(NackaStrand);
        WeatherInfo Weather = new WeatherInfo(NackaStrand);
        TextTv text_tv = new TextTv();
        FileWriter file_writer = new FileWriter();
        int i = 0;
        // keeps updating the data
        while (true)
        {
            try
            {
                // this is so that the maximum number of free api uses will not be reached
                if (i % 40 == 0)
                {
                    nestedList[0] = Information.UpdateData();
                }
                // will still update the time left without a need for an api use
                else
                {
                    nestedList[0] = Information.UpdateTimeLeft((List<List<string>>)nestedList[0]);
                }
                nestedList[1] = Weather.UpdateData();
                nestedList[2] = text_tv.UpdateData();
                file_writer.Write(nestedList);
                i++;
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                break;
            }
        }
    }

    /// <summary>
    /// Creates a list that is made for storing the data in order to merge the apis.
    /// </summary>
    /// <returns>A list that is ordered in specific way</returns>
    static List<object> InitializeNestedList()
    {
        return new List<object>
        {
            new List<List<string>>
            {
                new List<string> { "", "", "" },
                new List<string> { "", "", "" },
                new List<string> { "", "", "" },
            },
            new List<string> { "", "" },
            new List<List<string>> { new List<string> { "" } },
        };
    }
}
