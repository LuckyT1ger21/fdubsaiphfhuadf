using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

class TextTv : IApiGrabber<List<String>>
{
    string url = "https://api.texttv.nu/api/get/101";
    public string Url
    {
        set { url = value; }
    }

    public List<List<String>> UpdateData()
    {
        List<List<String>> TextTvData = new List<List<String>>();
        try
        {
            using (var webClient = new WebClient())
            {
                var doc = new HtmlDocument();
                var json = webClient.DownloadString(url);
                JsonNode root = JsonNode.Parse(json);
                doc.LoadHtml(root[0]["content"][0].ToString());
                var topRow = doc.DocumentNode.SelectSingleNode(
                    "//span[contains(@class, 'toprow')]"
                );
                if (topRow != null)
                {
                    string headerText = topRow.InnerText.Trim();
                    TextTvData.Add(new List<string> { headerText });
                }
                var newsLines = doc.DocumentNode.SelectNodes("//span[@class='line']");
                if (newsLines != null)
                {
                    foreach (var line in newsLines)
                    {
                        var link = line.SelectSingleNode(".//a");
                        if (link != null)
                        {
                            string lineText = line.InnerText;
                            string pageNumber = link.GetAttributeValue("href", "").Replace("/", "");
                            string cleanHeadline = lineText.Replace(pageNumber, "").Trim();
                            TextTvData.Add(new List<string> { pageNumber, cleanHeadline });
                        }
                    }
                }
            }
            return TextTvData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return TextTvData;
        }
    }
}
