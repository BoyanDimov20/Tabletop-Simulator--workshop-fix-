using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;

var file = File.ReadAllBytes("WorkshopUpload");

MemoryStream ms = new MemoryStream(file);
using (BsonReader reader = new BsonReader(ms))
{
    JsonSerializer serializer = new JsonSerializer();

    var result = (JObject)serializer.Deserialize(reader);


    string json = result.ToString(Newtonsoft.Json.Formatting.Indented);
    
    File.WriteAllText("WorkshopUpload.json", json);
}

Console.WriteLine("Done by SorionN <3");
Thread.Sleep(1000);
