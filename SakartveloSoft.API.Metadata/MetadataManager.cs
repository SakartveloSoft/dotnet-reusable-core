using SakartveloSoft.API.DataAttributes;
using SakartveloSoft.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SakartveloSoft.API.Metadata
{
    public class MetadataManager
    {

        private IDictionary<string, MetaType> metaTypes = new Dictionary<string, MetaType>();

        private IdentitiesFactory identitiesFactory = new IdentitiesFactory();

        public MetadataManager()
        {
            InitiaizePipelines();
        }

        public MetadataManager DiscoverAssembly(Assembly assembly)
        {
            foreach(var type in assembly.ExportedTypes)
            {
                if (type.IsDefined(typeof(KnownType)))
                {
                    EnsureForMetaType(type);
                }
            }
            return this;
        }

        private MetaType EnsureForMetaType(Type type)
        {
            if (metaTypes.TryGetValue(type.FullName, out MetaType metaType))
            {
                return metaType;
            }
            if (type.IsGenericTypeDefinition || type.IsSignatureType || type.IsAbstract || type.IsPrimitive || type.IsArray)
            {
                return null;
            }

            if (type.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 0) == null)
            {
                return null;
            }

            var result = new MetaType(type, EnsureForMetaType);
            metaTypes[result.DescribedType.FullName] = result;
            KnownTypeAttributes.ApplyDiscoveredAttributes(result, type.GetCustomAttributes());
            foreach(var prop in result.MetaProperties.Values)
            {
                if (prop.DeclaredAt == result)
                {
                    KnownPropertyAttributes.ApplyDiscoveredAttributes(metaType, prop, prop.Member.GetCustomAttributes());
                }
            }
            result.PropertiesUpdated();
            return result;
        }

        public bool HasMetadataForType<T>() where T: class
        {
            return this.metaTypes.ContainsKey(typeof(T).FullName);
        }

        public MetaType this[Type type]
        {
            get { return EnsureForMetaType(type); }
        }

        public ObjectValidationReport ValidateObject<T>(T instance) where T: class
        {
            var type = EnsureForMetaType(typeof(T));
            return ValidateObject(type, instance);

        }

        public MetaType DiscoverType<T>() where T : class, new()
        {
            return EnsureForMetaType(typeof(T));
        }

        protected virtual void InitiaizePipelines()
        {
            KnownTypeAttributes.AddAttributeHandler<Persisted>((type, persisted) =>
            {
                type.MarkAsPersisted();
            });
            KnownTypeAttributes.AddAttributeHandler<IdPrefix>((metaType, idPrefix) =>
            {
                metaType.UseIdPrefix(idPrefix.Prefix);
            });
            KnownTypeAttributes.AddAttributeHandler<StoreName>((metaType, storeName) =>
            {
                metaType.BindToStoreName(storeName.Name);
            });

            KnownPropertyAttributes.AddAttributeHandler<Indexing>((type, prop, indexing) =>
            {
                prop.SetIndexingMode(indexing.Model);
                if (indexing.Model == IndexingModel.Keywords)
                {
                    type.EnableKeywordsSearch();
                }
            });
            KnownPropertyAttributes.AddAttributeHandler<NoIndexingAttribute>((type, prop, noIndexing) =>
            {
                prop.SetIndexingMode(IndexingModel.NoIndexing);
            });
            KnownPropertyAttributes.AddAttributeHandler<JSONOnlyAttribute>((type, prop, noIndexing) =>
            {
                prop.SetIndexingMode(IndexingModel.JSONOnly);
            });
            KnownPropertyAttributes.AddAttributeHandler<KeywordsAtribute>((type, prop, keywords) =>
            {
                prop.SetIndexingMode(IndexingModel.Keywords);
                type.EnableKeywordsSearch();
            });
            KnownPropertyAttributes.AddAttributeHandler<SearcheableAttribute>((type, prop, searcheable) =>
            {
                prop.SetIndexingMode(IndexingModel.Searcheable);
            });
            KnownPropertyAttributes.AddAttributeHandler<DefaultValue>((type, prop, defaultValue) =>
            {
                prop.SetDefaultValue(defaultValue.ValueType, defaultValue.Value);
            });
            KnownPropertyAttributes.AddAttributeHandler<Required>((type, prop, req) =>
            {
                prop.MakeRequired();
                prop.AddValidatorImplementation(req);
            });
            KnownPropertyAttributes.AddAttributeHandler<InRangeAttribute>((type, prop, range) =>
            {
                prop.RestrictToRange(range.Min, range.Max);
                prop.AddValidatorImplementation(range);
            });
            KnownPropertyAttributes.AddAttributeHandler<InListAttribute>((type, prop, listAttr) =>
            {
                prop.RestrictToList(listAttr.Values);
                prop.AddValidatorImplementation(listAttr);
            });
            KnownPropertyAttributes.AddAttributeHandler<EntityKeyAttribute>((metaType, prop, keyAttr) =>
            {
                prop.UseAsEntityKey(keyAttr.KeyType);
            });
        }

        public TypeAttributesPipeline KnownTypeAttributes { get; } = new TypeAttributesPipeline();
        public PropertyAttributesPipeline KnownPropertyAttributes { get; } = new PropertyAttributesPipeline();
        protected void ApplyDefaultValues(MetaType metaType, object target)
        {
            foreach (var prop in metaType.MetaProperties.Values)
            {
                if (prop.HasDefaultValue)
                {
                    prop.ApplyDefaultValue(target: target);
                }
            }

        }

        public string GeneratePrefixedShortRandomId(string prefix)
        {
            return this.identitiesFactory.GenerateCompactPrefixedId(prefix);
        }

        public string GeneratePrefixedRandomId(string prefix)
        {
            return this.identitiesFactory.GeneratePrefixedId(prefix);
        }

        protected void GenerateObjectIdentity(MetaType metaType, object instance, bool requireKeyProperty = false)
        {
            if (metaType.HasKeyProperty)
            {
                var keyType = metaType.KeyProperty.KeyType.Value;
                switch (keyType)
                {
                    case EntityKeyType.PrefixedRandomString:
                        metaType.KeyProperty.SetValueForObject(instance, identitiesFactory.GeneratePrefixedId(metaType.IdPrefix));
                        break;
                    case EntityKeyType.PrefixedCompactRandomString:
                        metaType.KeyProperty.SetValueForObject(instance, identitiesFactory.GenerateCompactPrefixedId(metaType.IdPrefix));
                        break;
                    case EntityKeyType.Guid:
                        metaType.KeyProperty.SetValueForObject(instance, Guid.NewGuid());
                        break;
                }
            } else if (requireKeyProperty)
            {
                throw new InvalidOperationException("Type does not have key property bound. Please update type metadata");
            }
        }

        public void AssignObjectId<T>(T instance) where T: class
        {
            GenerateObjectIdentity(EnsureForMetaType(typeof(T)), instance, true);
        }

        public T CreateNewObject<T>(bool forLoading = false) where T: class, new()
        {
            var result = new T();
            if (!forLoading)
            {
                var metaType = EnsureForMetaType(typeof(T));
                ApplyDefaultValues(metaType, result);
                GenerateObjectIdentity(metaType, result);
            }
            return result;
        }

        protected ObjectValidationReport BeginValidation(MetaType metaType, object target)
        {
            return new ObjectValidationReport()
            {
                MetaTypeName = metaType.TypeAlias,
                ObjectIdentity = metaType.TryGetObjectKeyString(target)
            };
        }

        protected ObjectValidationReport ValidateObject(MetaType metaType, object instance)
        {
            var report = BeginValidation(metaType, instance);
            foreach(var prop in metaType.MetaProperties.Values)
            {
                prop.ValidateValueOfObject(instance, report);
            }
            return report;
        }

    }
}
