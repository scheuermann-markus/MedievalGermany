using Newtonsoft.Json.Linq;

namespace MedievalGermany.Application.Extensions;

public static class StringExtensions
{
    public static bool IsNotNullOrWhiteSpace(this string? source)
    {
        if (source == null) return false;

        for (int i = 0; i < source.Length; i++)
        {
            if (!char.IsWhiteSpace(source[i])) return true;
        }

        return false;
    }
}
