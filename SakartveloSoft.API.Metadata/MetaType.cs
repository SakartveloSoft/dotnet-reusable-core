using SakartveloSoft.API.DataAttributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SakartveloSoft.API.Metadata
{
    public class MetaType
    {
        public string TypeAlias { get; private set; }

        public Type DescribedType { get; private set; }

        public bool PersistenceEnabled { get; private set; }

        public string StoreName { get; private set; }

        public string IdPrefix { get; private set; }

        public bool HaveKeywords { get; private set; }

        public MetaType BaseType { get; private set; }

        public IDictionary<string, MetaProperty> MetaProperties { get; private set; }

        public MetaProperty KeyProperty { get; protected set; }

        public bool HasKeyProperty { get
            {
                return KeyProperty != null;
            } }

        public MetaType([NotNull]Type type, Func<Type, MetaType> metaTypeResolver)
        {
            DescribedType = type;
            TypeAlias = DescribedType.Name;
            MetaProperties = new Dictionary<string, MetaProperty>();
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                BaseType = metaTypeResolver(type.BaseType);
                foreach(var prop in BaseType.MetaProperties)
                {
                    this.MetaProperties.Add(prop.Key, prop.Value);
                }
            }
            foreach(var propInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)) {
                MetaProperty prop = new MetaProperty(this, propInfo);
                MetaProperties.Add(prop.Name, prop);
            } 
            foreach(var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                MetaProperty field = new MetaProperty(this, fieldInfo);
                MetaProperties.Add(field.Name, field);
            }
        }

        public void PropertiesUpdated()
        {
            KeyProperty = MetaProperties.Values.FirstOrDefault(a => a.IsKeyProperty);
        }

        public MetaType EnableKeywordsSearch()
        {
            HaveKeywords = true;
            return this;
        }

        public MetaType BindToStoreName(string name)
        {
            this.StoreName = name;
            return this;
        }

        public MetaType UseTypeAlias(string newAlias)
        {
            this.TypeAlias = newAlias;
            return this;
        }

        public MetaType UseIdPrefix(string idPrefix)
        {
            this.IdPrefix = idPrefix;
            return this;
        }

        public MetaType MarkAsPersisted()
        {
            this.PersistenceEnabled = true;
            return this;
        }

        public MetaProperty this[FieldInfo field]
        {
            get
            {
                if (this.MetaProperties.TryGetValue(field.Name, out MetaProperty prop))
                {
                    return prop;
                }
                throw new ArgumentException(@$"Unknown field {field.Name}", "field");
            }
        }
        public MetaProperty this[PropertyInfo propInfo]
        {
            get
            {
                if (this.MetaProperties.TryGetValue(propInfo.Name, out MetaProperty prop))
                {
                    return prop;
                }
                throw new ArgumentException(@$"Unknown field {propInfo.Name}", "field");
            }
        }

        public string TryGetObjectKeyString(object target)
        {
            if (HasKeyProperty)
            {
                var value = KeyProperty.GetValueForObject(target);
                if (value == null)
                {
                    return null;
                } else
                {
                    return value.ToString();
                }
            } else
            {
                return null;
            }
        }
    }
}
