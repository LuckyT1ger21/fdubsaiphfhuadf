using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

class WeatherInfo : IApiGrabber<String>
{
    string longitude;
    string latitude;
    string url;

    /// <summary>
    /// Enters an object containing the necessary information for the api to function
    /// </summary>
    /// <param name="location">Contains the latitude and longitude</param>
    public WeatherInfo(Location location)
    {
        this.longitude = location.longitude;
        this.latitude = location.latitude;
        this.url =
            $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
    }

    /// <summary>
    /// Pulls the latest weather data from the API.
    /// </summary>
    /// <returns>A list containing weather and temperature.</returns>
    public List<string> UpdateData()
    {
        string temperature;
        string weatherDescription;
        try
        {
            // Pulls information from the api
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(url);
                JsonNode root = JsonNode.Parse(json);
                // Interprets the weather code into a weather
                switch (root["current_weather"]["weathercode"].ToString())
                {
                    case "0":
                        weatherDescription = "Clear sky";
                        break;
                    case "1":
                        weatherDescription = "Mainly clear";
                        break;
                    case "2":
                        weatherDescription = "Partly cloudy";
                        break;
                    case "3":
                        weatherDescription = "Overcast";
                        break;
                    case "45":
                        weatherDescription = "Fog";
                        break;
                    case "48":
                        weatherDescription = "Depositing rime fog";
                        break;
                    case "51":
                        weatherDescription = "Light drizzle";
                        break;
                    case "53":
                        weatherDescription = "Moderate drizzle";
                        break;
                    case "55":
                        weatherDescription = "Dense drizzle";
                        break;
                    case "56":
                        weatherDescription = "Light freezing drizzle";
                        break;
                    case "57":
                        weatherDescription = "Dense freezing drizzle";
                        break;
                    case "61":
                        weatherDescription = "Slight rain";
                        break;
                    case "63":
                        weatherDescription = "Moderate rain";
                        break;
                    case "65":
                        weatherDescription = "Heavy rain";
                        break;
                    case "66":
                        weatherDescription = "Light freezing rain";
                        break;
                    case "67":
                        weatherDescription = "Heavy freezing rain";
                        break;
                    case "71":
                        weatherDescription = "Slight snowfall";
                        break;
                    case "73":
                        weatherDescription = "Moderate snowfall";
                        break;
                    case "75":
                        weatherDescription = "Heavy snowfall";
                        break;
                    case "77":
                        weatherDescription = "Snow grains";
                        break;
                    case "80":
                        weatherDescription = "Slight rain showers";
                        break;
                    case "81":
                        weatherDescription = "Moderate rain showers";
                        break;
                    case "82":
                        weatherDescription = "Violent rain showers";
                        break;
                    case "85":
                        weatherDescription = "Slight snow showers";
                        break;
                    case "86":
                        weatherDescription = "Heavy snow showers";
                        break;
                    case "95":
                        weatherDescription = "Thunderstorm";
                        break;
                    case "96":
                        weatherDescription = "Thunderstorm with slight hail";
                        break;
                    case "99":
                        weatherDescription = "Thunderstorm with heavy hail";
                        break;
                    default:
                        weatherDescription = "Unknown weather condition";
                        break;
                }
                temperature = root["current_weather"]["temperature"].ToString();
                return new List<String> { temperature, weatherDescription };
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<String> { "No recorded temperature", "No weather" };
        }
    }
}
