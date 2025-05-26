using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

class StopInfo : IApiGrabber<List<String>>
{
    string apiKey = "acfc328f-c87c-4279-af9f-2584412dcef5";
    string id;
    string url;

    public StopInfo(Location stop)
    {
        this.id = stop.id;
        this.url =
            $"https://api.resrobot.se/v2.1/departureBoard?id={id}&format=json&accessId={apiKey}";
    }

    public List<List<String>> UpdateData()
    {
        List<List<String>> buses = new List<List<String>>();
        using (var webClient = new WebClient())
        {
            var json = webClient.DownloadString(url);
            JsonNode root = JsonNode.Parse(json);
            JsonArray departures = root["Departure"]?.AsArray();
            if (departures != null)
            {
                foreach (JsonNode departure in departures)
                {
                    string name = departure["name"]?.ToString().Substring(18, 3);
                    string date = departure["date"]?.ToString();
                    string time = departure["time"]?.ToString().Substring(0, 5);
                    DateTime the_date = DateTime.Parse(date + " " + time);
                    TimeSpan duration = the_date - DateTime.Now;
                    string TimeLeft = duration.ToString().Substring(0, 5);
                    if (TimeLeft == "00:00" || TimeLeft.Substring(0, 1) == "-")
                    {
                        TimeLeft = "Now";
                    }
                    buses.Add(new List<String>() { name, TimeLeft, time });
                }
                return buses;
            }
            else
            {
                Console.WriteLine("Inga avgångar hittades.");
                return buses;
            }
        }
    }

    public List<List<string>> UpdateTimeLeft(List<List<string>> list)
    {
        foreach (List<string> strList in list)
        {
            DateTime the_date = DateTime.Parse(strList[2]);
            TimeSpan duration = the_date - DateTime.Now;
            string TimeLeft = duration.ToString().Substring(0, 5);
            if (TimeLeft == "00:00" || TimeLeft.Substring(0, 1) == "-")
            {
                TimeLeft = "Now";
            }
            strList[1] = TimeLeft;
        }
        return list;
    }
}
