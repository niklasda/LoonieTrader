using System.Text.Json;

namespace LoonieTrader.Library.Extensions;

public static class JsonEx
{
    public static string PrettyPrintJson(this string json)
    {
        using var jDoc = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });

    }

    
}