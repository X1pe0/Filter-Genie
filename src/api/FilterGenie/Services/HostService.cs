using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace FilterGenie.Services
{
    public static class HostService
    {
        // public static byte[] ComputeHash(string text)
        // {
        //     using (SHA256 alg = SHA256.Create())
        //     {
        //         byte[] source = Encoding.UTF8.GetBytes(text);
        //         byte[] output = new byte[32];
        //         alg.TryComputeHash(source, output, out var _);
                
        //         return output;
        //     }
        // }


        public static String ComputeHash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())            
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static string GenerateHostFile(IReadOnlyList<string> global, IReadOnlyList<string> personal)
        {
            StringBuilder stringBuilder = new StringBuilder();
        
            stringBuilder.Append($"# Global\n");

            foreach (string host in global)
                stringBuilder.Append($"127.0.0.1 {host}\n");

            stringBuilder.Append($"\n# Perseonal\n");
            foreach (string host in personal)
                stringBuilder.Append($"127.0.0.1 {host}\n");

            return stringBuilder.ToString();
        }

        
    }
}