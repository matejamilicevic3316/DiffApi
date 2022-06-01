using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static int ReverseCharIndex(this string word, int offset) => word.Length - offset;
        public static string DecodeBase64(this string value) 
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
            catch (Exception)
            {
                throw new Base64ParseEncodeFailureException("Base64 decoding failed to execute");
            }
        }
    }
}
