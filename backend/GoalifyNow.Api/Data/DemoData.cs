using System.Security.Cryptography;
using System.Text;

namespace GoalifyNow.Api.Data;

public static class DemoData
{
    public static readonly Guid DemoUserId = GenerateId("demo@goalifynow.local");

    public static Guid GenerateId(string seed)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        var guidBytes = new byte[16];
        Array.Copy(hash, guidBytes, 16);
        return new Guid(guidBytes);
    }
}
