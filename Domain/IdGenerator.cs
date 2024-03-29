using System;
using System.Threading;

namespace spectabis_cli.Domain
{
    /// <summary>
    /// https://raw.githubusercontent.com/NimaAra/Easy.Common/master/Easy.Common/IDGenerator.cs
    /// </summary>
    public sealed class IdGenerator
    {
        private const string Encode_32_Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
        private static long _lastId = DateTime.UtcNow.Ticks;

        private static readonly ThreadLocal<char[]> _charBufferThreadLocal =
            new ThreadLocal<char[]>(() => new char[13]);

        static IdGenerator() { }
        private IdGenerator() { }

        /// <summary>
        /// Returns a single instance of the <see cref="IDGenerator"/>.
        /// </summary>
        public static IdGenerator Instance { get; } = new IdGenerator();

        /// <summary>
        /// Returns and ID. e.g: <c>0HLH7Q6V92BQE</c>
        /// </summary>
        public string Generate => GenerateImpl(Interlocked.Increment(ref _lastId));

        private static string GenerateImpl(long id)
        {
            var buffer = _charBufferThreadLocal.Value;

            buffer[0] = Encode_32_Chars[(int)(id >> 60) & 31];
            buffer[1] = Encode_32_Chars[(int)(id >> 55) & 31];
            buffer[2] = Encode_32_Chars[(int)(id >> 50) & 31];
            buffer[3] = Encode_32_Chars[(int)(id >> 45) & 31];
            buffer[4] = Encode_32_Chars[(int)(id >> 40) & 31];
            buffer[5] = Encode_32_Chars[(int)(id >> 35) & 31];
            buffer[6] = Encode_32_Chars[(int)(id >> 30) & 31];
            buffer[7] = Encode_32_Chars[(int)(id >> 25) & 31];
            buffer[8] = Encode_32_Chars[(int)(id >> 20) & 31];
            buffer[9] = Encode_32_Chars[(int)(id >> 15) & 31];
            buffer[10] = Encode_32_Chars[(int)(id >> 10) & 31];
            buffer[11] = Encode_32_Chars[(int)(id >> 5) & 31];
            buffer[12] = Encode_32_Chars[(int)id & 31];

            return new string(buffer, 0, buffer.Length);
        }
    }
}