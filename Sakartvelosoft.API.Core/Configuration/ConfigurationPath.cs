using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace SakartveloSoft.API.Core.Configuration
{
    public class ConfigurationPath: IEquatable<ConfigurationPath>, IEquatable<string>
    {
        private static readonly char[] separators = "/:".ToCharArray();
        private static readonly string[] EmptyNames = new string[0];
        private readonly string[] Names;
        public ConfigurationPath(ConfigurationPath path)
        {
            this.Names = path.Names;
        }
        private ConfigurationPath(string[] names)
        {
            Names = names ?? EmptyNames;
        }
        public ConfigurationPath(string literal)
        {
            if (string.IsNullOrWhiteSpace(literal))
            {
                this.Names = EmptyNames;
            }
            else
            {
                this.Names = literal.ToLower().Split().Select(str => str.Trim()).Where(a => a.Length >= 0).ToArray();
            }
        }

        public static ConfigurationPath operator+(ConfigurationPath a, ConfigurationPath other) {
            return new ConfigurationPath(a.Names.Concat(other.Names).ToArray());
        }
        public static implicit operator ConfigurationPath(string path)
        {
            return new ConfigurationPath(path);
        }

        public string Leaf
        {
            get
            {
                return this.Names.Length == 0 ? string.Empty : this.Names[this.Names.Length - 1];
            }
        }

        public int Length
        {
            get { return Names.Length; }
        }

        public string this[int level]
        {
            get
            {
                if (level < 0 || level >= Names.Length)
                {
                    return string.Empty;
                }
                return Names[level];
            }
        }

        bool StartsFrom(ConfigurationPath path)
        {
            if (path.Names == null || path.Names.Length > Names.Length)
            {
                return false;
            }
            for(var p = 0; p < path.Names.Length; p++)
            {
                if (this.Names[p] != path.Names[p])
                {
                    return false;
                }
            }
            return true;
        }

        public bool Equals([AllowNull] ConfigurationPath other)
        {
            if (other == null)
            {
                return false;
            }
            if (other.Names.Length != Names.Length)
            {
                return false;
            }
            for(var x = 0; x < Names.Length; x++)
            {
                if (Names[x] != other.Names[x])
                {
                    return false;
                }
            }
            return true;
        }

        public bool Equals([AllowNull] string other)
        {
            if (string.IsNullOrWhiteSpace(other))
            {
                return ReferenceEquals(this, Empty);
            }
            return new ConfigurationPath(other).Equals(this);
        }

        public static implicit operator string(ConfigurationPath path)
        {
            return path.ToString();
        }

        public override string ToString()
        {
            return string.Join('/', Names);
        }

        public string ToString(bool asPrefix)
        {
            var str = this.ToString();
            if (asPrefix)
            {
                str = str + separators[0];
            }
            return str;
        }

        public static readonly ConfigurationPath Empty = new ConfigurationPath(EmptyNames);

        public static bool IsNullOrEmpty(ConfigurationPath path)
        {
            return ReferenceEquals(null, path) || ReferenceEquals(path, Empty) || path.Length == 0; 
        }

    }
}
