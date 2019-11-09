using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public class LoggingPath
    {
        private string[] _path = null;

        private static readonly string Seprator = "/";

        public static LoggingPath Empty { get; } = new LoggingPath();


        public LoggingPath(params string[] names)
        {
            this._path = names ?? new string[0];
        }

        public override string ToString()
        {
            return String.Join(Seprator, _path);
        }
        
        public LoggingPath Append(params string[] names)
        {
            var newNames = new string[this._path.Length + names.Length];
            Array.Copy(_path, newNames, names.Length);
            Array.Copy(names, 0, newNames, _path.Length, names.Length);
            return new LoggingPath(newNames);
        }

    }
}
