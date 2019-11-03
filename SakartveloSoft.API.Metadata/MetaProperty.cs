using SakartveloSoft.API.DataAttributes;
using SakartveloSoft.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SakartveloSoft.API.Metadata
{
    public class MetaProperty
    {
        public MetaType DeclaredAt { get; private set; }
        public MemberInfo Member { get; private set; }

        public Type ValuesType { get; private set; }

        public string Name { get; private set; }

        private Func<object, object> valueGetter;
        private Action<object, object> valueSetter;

        private List<IValuesValidator> rules = new List<IValuesValidator>(); 

        public MetaProperty(MetaType declaredAt, MemberInfo memberInfo)
        {
            DeclaredAt = declaredAt;
            Member = memberInfo;
            Name = memberInfo.Name;
            if (Member.MemberType == MemberTypes.Field)
            {
                var field = (Member as FieldInfo);
                valueGetter = (source) => field.GetValue(source);
                valueSetter = (target, value) => field.SetValue(target, value);
            } else
            {
                var prop = (Member as PropertyInfo);
                var propGetter = prop.GetGetMethod();
                var propSetter = prop.GetSetMethod();
                valueGetter = (source) => propGetter.Invoke(source, null);
                valueSetter = (target, value) => propSetter.Invoke(target, new[] { value });
            }
        }

        

        public bool Required { get; private set; }

        public IndexingModel Indexing { get; private set; }

        public bool Indexed { get
            {
                return Indexing != IndexingModel.NoIndexing;
            } 
        }

        public bool HasDefaultValue { get; private set; }
        public DefaultValueType? DefaultValueType { get; private set; }
        public object DefaultValue { get; private set; }

        public MetaProperty MakeRequired()
        {
            this.Required = true;
            return this;
        }

        public MetaProperty SetIndexingMode(IndexingModel model)
        {
            Indexing = model;
            return this;
        }

        public MetaProperty SetDefaultValue(DefaultValueType valueType, object value)
        {
            if (value == null)
            {
                DefaultValueType = null;
                DefaultValue = null;
                HasDefaultValue = false;
            }
            else
            {
                DefaultValueType = valueType;
                DefaultValue = value;
                HasDefaultValue = true;
            }
            return this;
        }

        public decimal? Min { get; private set; }
        public decimal? Max { get; private set; }

        public bool HasRangeRestriction { get; private set; }

        public MetaProperty RestrictToRange(decimal? min = null, decimal? max = null)
        {
            Min = min;
            Max = max;
            HasRangeRestriction = min.HasValue || max.HasValue;
            return this;
        }

        public IReadOnlyList<object> AllowedValues { get; private set; }

        public bool HasListRestriction
        {
            get { return AllowedValues != null && AllowedValues.Count > 0; }
        }

        public bool IsKeyProperty { get; private set; }

        public EntityKeyType? KeyType { get; private set; }

        public MetaProperty RestrictToList(IReadOnlyList<object> values)
        {
            AllowedValues = values;
            return this;
        }
        public MetaProperty RestrictToList(IEnumerable<object> values)
        {
            AllowedValues = values.ToList();
            return this;
        }
        public MetaProperty RestrictToList(params object[] values)
        {
            AllowedValues = values.ToList();
            return this;
        }

        public MetaProperty UseAsEntityKey(EntityKeyType keyType)
        {
            IsKeyProperty = true;
            KeyType = keyType;
            return this;
        }

        public void ApplyDefaultValue(object target)
        {
            if (this.HasDefaultValue)
            {
                valueSetter(target, DefaultValue);
            }
        }

        public void SetValueForObject(object target, object value)
        {
            valueSetter(target, value);
        }

        public object GetValueForObject(object target)
        {
            return valueGetter(target);
        }

        public MetaProperty AddValidatorImplementation([NotNull]IValuesValidator impl)
        {
            if (impl.CanValidateType(ValuesType))
            {
                this.rules.Add(impl);
            }
            return this;
        }

        public void ValidateValueOfObject(object instance, ObjectValidationReport report)
        {
            var value = GetValueForObject(instance);
            if (rules != null && rules.Count > 0)
            {
                foreach(var rule in rules)
                {
                    if (!rule.IsValueValid(value))
                    {
                        report.AddError(new ValidationError
                        {
                            ErrorCode = rule.ErrorCode,
                            Property = Name,
                            Value = value,
                            Message = GenerateValidationMessage(rule.MessageTemplate, Name)
                        });
                        break;
                    }
                    
                }
            }
        }

        private static string GenerateValidationMessage(string messageTemplate, string name)
        {
            return messageTemplate.Replace("{name}", name);
        }
    }
}

