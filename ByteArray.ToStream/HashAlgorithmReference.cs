using System.Buffers;
using System.Security.Cryptography;

namespace ByteArray.ToStream
{
    internal class HashAlgorithmReference
    {
        //taken from https://github.com/dotnet/runtime/blob/main/src/libraries/System.Security.Cryptography/src/System/Security/Cryptography/HashAlgorithm.cs

        public byte[] ComputeHash(Stream inputStream)
        {
            if (_disposed)
                throw new ObjectDisposedException(null);
            // Use ArrayPool.Shared instead of CryptoPool because the array is passed out.
            byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
            int bytesRead;
            int clearLimit = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                if (bytesRead > clearLimit)
                {
                    clearLimit = bytesRead;
                }
                HashCore(buffer, 0, bytesRead);
            }
            CryptographicOperations.ZeroMemory(buffer.AsSpan(0, clearLimit));
            ArrayPool<byte>.Shared.Return(buffer, clearArray: false);
            return CaptureHashCodeAndReinitialize();
        }

    
        private void HashCore(byte[] buffer, int v, int bytesRead)
        {
            throw new NotImplementedException();
        }

        private byte[] CaptureHashCodeAndReinitialize()
        {
            throw new NotImplementedException();
        }

        private bool _disposed;
    }
}
