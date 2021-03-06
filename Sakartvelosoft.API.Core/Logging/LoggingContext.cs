﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public class LoggingContext
    {
        private readonly List<string> componentsPath = new List<string>();
        public IReadOnlyList<string> ComponentsPath => componentsPath;

        public string ComponentName { get; private set; }

        private Dictionary<string, object> propertiesBag = new Dictionary<string, object>();
        public IReadOnlyDictionary<string, object> Properties => propertiesBag;

        private List<NamedValue> formattedValuesList;

        public Type ContextType { get; private set; }

        public IReadOnlyList<NamedValue> FormattedValuesList
        {
            get
            {
                if (formattedValuesList == null)
                {
                    formattedValuesList = new List<NamedValue>();
                    foreach(var val in propertiesBag)
                    {
                        formattedValuesList.Add(new NamedValue(val.Key, val.Value));
                    }
                    formattedValuesList.Add(new NamedValue("ComponentsPath", String.Join("/", componentsPath)));
                }
                return formattedValuesList;
            }
        }

        public LoggingContext(LoggingContext parent, string componentName, object properties)
        {
            if (parent != null)
            {
                foreach(var name in parent.componentsPath)
                {
                    this.componentsPath.Add(name);
                }
                foreach(var prop in parent.propertiesBag)
                {
                    this.propertiesBag.Add(prop.Key, prop.Value);
                }
            }
            if (componentName != null && componentName != string.Empty)
            {
                componentsPath.Add(componentName);
                ComponentName = componentName;
            }
            UpdateProperties(properties);
        }

        public void UpdateProperties(object properties)
        {
            if (properties != null)
            {
                var propType = properties.GetType();
                if (propType.IsClass)
                {
                    foreach (var prop in propType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (prop.CanRead)
                        {
                            var value = prop.GetGetMethod().Invoke(properties, null);
                            if (value == null || value is IExcludedFromLogging)
                            {
                                continue;
                            }
                            this.propertiesBag[prop.Name] = value;
                        }
                    }
                    foreach (var field in propType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        var fieldVal = field.GetValue(properties);
                        if (fieldVal == null || fieldVal is IExcludedFromLogging)
                        {
                            continue;
                        }
                        this.propertiesBag[field.Name] = field.GetValue(properties);
                    }
                }
                else
                {
                    this.propertiesBag["Context"] = properties.ToString();
                }

            }
            formattedValuesList = null;
        }

        public static LoggingContext Empty { get; } = new LoggingContext(null, null, null);

        public LoggingContext CreateSubContext(string componentName, object properties = null)
        {
            return new LoggingContext(this, componentName, properties);
        }

        public LoggingContext<TScope> CreateSubContext<TScope>(string name, TScope subScope)
        {
            return new LoggingContext<TScope>(this, name, subScope);
        }

        public LoggingContext<TScope> CreateSubContext<TScope>(TScope scope = default) where TScope: class
        {
            return new LoggingContext<TScope>(this, scope == null ? (typeof(TScope).Name) : scope.ToString(), scope);
        }
    }

    public class LoggingContext<TScope> : LoggingContext
    {
        public TScope Scope { get; private set; }
        public LoggingContext(LoggingContext parent, string name, TScope scope) : base(parent, name, scope)
        {
            Scope = scope;
        }

    }
}
