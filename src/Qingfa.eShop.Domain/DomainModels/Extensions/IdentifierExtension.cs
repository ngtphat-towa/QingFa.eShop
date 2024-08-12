using System.Security.Cryptography;

namespace QingFa.EShop.Domain.DomainModels.Extensions
{
    internal static class IdentifierExtension
    {
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public static string NewId_FromTicks() => DateTime.UtcNow.Ticks.ToString("x");

        public static string NewId_FromGuid() => Guid.NewGuid().ToString();

        public static string NewId_FromGuidBase64() => Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                                                    .Replace("/", "_")
                                                    .Replace("+", "-")
                                                    .Substring(0, 15);

        public static string NewId_FromGuidShorted() => Guid.NewGuid().ToString("N").Substring(0, 15);

        public static string NewId_FromRandomLong()
        {
            byte[] bytes = new byte[8];
            _rng.GetBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0).ToString("x");
        }

        public static string NewId_FromRandomDoubleLong()
        {
            byte[] bytes = new byte[16];
            _rng.GetBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0).ToString("x") + BitConverter.ToUInt64(bytes, 8).ToString("x");
        }
    }
}
