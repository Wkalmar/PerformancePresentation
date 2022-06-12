using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ByteArray.ToStream
{
    internal static class HashUtils
    {
        public static Guid ComputeHash(byte[] data)
        {
            using HashAlgorithm algorithm = MD5.Create();
            byte[] bytes = algorithm.ComputeHash(data);
            return new Guid(bytes);
        }

        public static Guid ComputeStream(Stream stream)
        {
            using HashAlgorithm algorithm = MD5.Create();
            byte[] bytes = algorithm.ComputeHash(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return new Guid(bytes);
        }
    }
}
