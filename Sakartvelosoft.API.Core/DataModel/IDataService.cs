using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public interface IDataService
    {
        IDatabase OpenDefaultDatabase();
        IDatabase OpenDefaultDatabase(IAPIContext context);
        IDatabase OpenDatabase(string name);
        IDatabase OpenDatabase(IAPIContext context, string databaseName);
    }
}
