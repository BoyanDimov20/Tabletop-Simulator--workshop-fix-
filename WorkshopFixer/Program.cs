using Newtonsoft.Json.Linq;
using System.Globalization;


Console.Write("Map Name: ");
var mapName = Console.ReadLine();
var text = File.ReadAllText(mapName);


var json = JObject.Parse(text);
ReplaceJson(json);


var fileResult = Path.GetFileNameWithoutExtension(mapName);
File.WriteAllText($"{fileResult}_fixed.json", json.ToString(Newtonsoft.Json.Formatting.Indented));

Console.WriteLine("Fixed!");
Console.WriteLine("Done By SorionN");
Thread.Sleep(2000);



void ReplaceJson(JToken token)
{
    if (token.Type == JTokenType.Property)
    {
        var prop = (JProperty)token;
        if (prop.Name == "$numberInt")
        {
            if (token.Parent.Parent.Type == JTokenType.Array)
            {
                token.Parent.Replace(int.Parse(prop.Value.ToString()));
            }
            else if (token.Parent.Parent.Type == JTokenType.Property)
            {
                var curr = (JProperty)token;
                var parent = (JProperty)token.Parent.Parent;
                token.Parent.Parent.Replace(new JProperty(parent.Name, int.Parse(curr.Value.ToString())));
            }
        }
        else if (prop.Name == "$numberDouble")
        {
            if (token.Parent.Parent.Type == JTokenType.Array)
            {
                token.Parent.Replace(Convert.ToDouble(prop.Value.ToString(), CultureInfo.InvariantCulture));
            }
            else if (token.Parent.Parent.Type == JTokenType.Property)
            {
                var curr = (JProperty)token;
                var parent = (JProperty)token.Parent.Parent;
                token.Parent.Parent.Replace(new JProperty(parent.Name, Convert.ToDouble(curr.Value.ToString(), CultureInfo.InvariantCulture)));
            }
        }
    }
    if (token.Next != null)
    {
        ReplaceJson(token.Next);
    }
    if (token.Children().Count() > 0)
    {
        ReplaceJson(token.First());
    }
}