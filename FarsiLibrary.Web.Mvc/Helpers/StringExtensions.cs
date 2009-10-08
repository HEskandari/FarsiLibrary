using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using FarsiLibrary.Utils.Internals;

namespace FarsiLibrary.Web.Mvc.Helpers
{
    /// <summary>
    /// Contains the extension methods of <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces the format item in a specified System.String with the text equivalent of the value of a corresponding System.Object instance in a specified array.
        /// </summary>
        /// <param name="instance">A string to format.</param>
        /// <param name="args">An System.Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the System.String equivalent of the corresponding instances of System.Object in args.</returns>
        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(CultureHelper.CurrentCulture, instance, args);
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseSensitiveEqual(this string instance, string comparing)
        {
            return string.CompareOrdinal(instance, comparing) == 0;
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseInsensitiveEqual(this string instance, string comparing)
        {
            return string.Compare(instance, comparing, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Compresses the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string Compress(this string instance)
        {
            Guard.Against(instance == null, "instance is null");

            byte[] binary = Encoding.UTF8.GetBytes(instance);
            byte[] compressed;

            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress))
                {
                    zip.Write(binary, 0, binary.Length);
                }

                compressed = ms.ToArray();
            }

            byte[] compressedWithLength = new byte[compressed.Length + 4];

            Buffer.BlockCopy(compressed, 0, compressedWithLength, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(binary.Length), 0, compressedWithLength, 0, 4);

            return Convert.ToBase64String(compressedWithLength);
        }

        /// <summary>
        /// Decompresses the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string Decompress(this string instance)
        {
            Guard.Against(instance == null, "instance is null");

            byte[] compressed = Convert.FromBase64String(instance);
            byte[] binary;

            using (MemoryStream ms = new MemoryStream())
            {
                int length = BitConverter.ToInt32(compressed, 0);
                ms.Write(compressed, 4, compressed.Length - 4);

                binary = new byte[length];

                ms.Seek(0, SeekOrigin.Begin);

                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(binary, 0, binary.Length);
                }
            }

            return Encoding.UTF8.GetString(binary);
        }
    }
}