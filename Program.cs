using System;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace test2
{
    class Program
    {
        static int Main()
        {
            MainAsync().GetAwaiter().GetResult();
            return 0;
        }

        static async Task MainAsync()
        {
            var stream = new FixedResultStream();
            var result = await JsonSerializer.ReadAsync(stream, typeof(string));
            Console.WriteLine(result);
        }
    }

    class FixedResultStream : Stream
    {
        public override bool CanRead => true;
        public override bool CanSeek { get; }
        public override bool CanWrite { get; }
        public override long Length { get; }
        public override long Position { get; set; }
        private int _index;


        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            _index++;

            if (_index == 1 || _index == 500)
            {
                var value = "\"";
                return Encoding.UTF8.GetBytes(
                    value.AsSpan(),
                    buffer.AsSpan(offset, count));
            }
            else
            {
                var value = "A";
                return Encoding.UTF8.GetBytes(
                    value.AsSpan(),
                    buffer.AsSpan(offset, count));
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
