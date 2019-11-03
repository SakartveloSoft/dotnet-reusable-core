using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Metadata
{
    public static class Base36
    {
        private static string Palette36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string Palette62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static void ToCustomBase(ulong value, ulong newBase, string palette, StringBuilder target)
        {
            var val = value;
            while (true)
            {
                var p = val % newBase;
                target.Insert(0, palette[(byte)p]);
                val = val / newBase;
                if (val == 0)
                {
                    break;
                }
            }
        }
        public static void ToBase36(ulong value, StringBuilder target)
        {
            ToCustomBase(value, 36ul, Palette36, target);
        }

        public static string ToBase36(ulong value)
        {
            var builder = new StringBuilder();
            ToCustomBase(value, 36ul, Palette36, builder);
            return builder.ToString();
        }

        public static void ToBase62(ulong value, StringBuilder target)
        {
            ToCustomBase(value, 62, Palette62, target);
        }

        public static string ToBase62(ulong value)
        {
            var builder = new StringBuilder();
            ToCustomBase(value, 62, Palette62, builder);
            return builder.ToString();
        }
    }
}
