using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class Helper
    {
        public static byte[] StringToBytes(string str)
        {
            var arr = Encoding.Unicode.GetBytes(str);
            return arr;
        }

        public static string BytesToString(byte[] arr,int size)
        {
            var str = Encoding.Unicode.GetString(arr, 0, size);
            return str;
        }
    }
}
