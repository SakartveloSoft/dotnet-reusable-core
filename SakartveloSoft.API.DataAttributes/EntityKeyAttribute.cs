using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.DataAttributes
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public sealed class EntityKeyAttribute: Attribute
    {
        public EntityKeyAttribute(EntityKeyType type = EntityKeyType.PrefixedRandomString)
        {
            KeyType = type;
        }

        public EntityKeyType KeyType { get; private set; }
    }
}
