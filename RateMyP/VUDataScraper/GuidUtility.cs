using System;
using System.Security.Cryptography;
using System.Text;

namespace VUDataScraper
    {
    public static class GuidUtility
        {
        public static Guid Create(Guid namespaceId, string name)
            {
            return Create(namespaceId, name, 5);
            }

        public static Guid Create(Guid namespaceId, string name, int version)
            {
            if (name == null)
                throw new ArgumentNullException("name");
            if (version != 3 && version != 5)
                throw new ArgumentOutOfRangeException("version", "version must be either 3 or 5.");

            byte[] nameBytes = Encoding.UTF8.GetBytes(name);

            byte[] namespaceBytes = namespaceId.ToByteArray();
            SwapByteOrder(namespaceBytes);

            byte[] hash;
            using (HashAlgorithm algorithm = version == 3 ? (HashAlgorithm)MD5.Create() : SHA1.Create())
                {
                algorithm.TransformBlock(namespaceBytes, 0, namespaceBytes.Length, null, 0);
                algorithm.TransformFinalBlock(nameBytes, 0, nameBytes.Length);
                hash = algorithm.Hash;
                }

            byte[] newGuid = new byte[16];
            Array.Copy(hash, 0, newGuid, 0, 16);

            newGuid[6] = (byte)((newGuid[6] & 0x0F) | (version << 4));

            newGuid[8] = (byte)((newGuid[8] & 0x3F) | 0x80);

            SwapByteOrder(newGuid);
            return new Guid(newGuid);
            }

        public static readonly Guid NameSpaceX500 = new Guid("6ba7b814-9dad-11d1-80b4-00c04fd430c8");

        internal static void SwapByteOrder(byte[] guid)
            {
            SwapBytes(guid, 0, 3);
            SwapBytes(guid, 1, 2);
            SwapBytes(guid, 4, 5);
            SwapBytes(guid, 6, 7);
            }

        private static void SwapBytes(byte[] guid, int left, int right)
            {
            byte temp = guid[left];
            guid[left] = guid[right];
            guid[right] = temp;
            }
        }
    }
