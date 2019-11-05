using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public static class Filters
    {
        public static ComparationOperand<TProperty> Property<T, TProperty>(params string[] names) where T: class, new() 
            where TProperty : IEquatable<TProperty>, IComparable<TProperty>
        {
            return new PropertyReference<T, TProperty>(names);
        }

        public static ComparationOperand<TProperty> Property<T,  TProperty>(Expression<Func<T, TProperty>> lambda)
            where T : class, new()
            where TProperty : IEquatable<TProperty>, IComparable<TProperty>
        {
            return new PropertyReference<T, TProperty>(GetLambdaDataPath(lambda.Body));
        }

        internal static IReadOnlyList<string> GetLambdaDataPath(Expression body)
        {

            var node = body;
            var names = new List<string>();
            while (node != null && node.NodeType != ExpressionType.Parameter)
            {
                if (node is MemberExpression) {
                    var ma = node as MemberExpression;
                    names.Insert(0, ma.Member.Name);
                    node = ma.Expression;
                    continue;
                }
                if (node is UnaryExpression)
                {
                    var u = node as UnaryExpression;
                    node = u.Operand;
                    continue;
                }
                throw new InvalidOperationException("Porperty expression must be chain of member access expressions, but got " + node.ToString());
            }
            return names;
        }

        public static DataFilter<T> Compare<T, TProperty>(string name, TProperty value, FilterComparison op = FilterComparison.Equal)
            where T : class, new()
            where TProperty : IComparable<TProperty>, IEquatable<TProperty>
        {
            return new DataFilter<T>(new CompareOperation<TProperty>(new PropertyReference<T, TProperty>(name), new ScalarValue<TProperty>(value), op));
        }

        public static ComparationOperand<TValue> Parameter<TValue>(string name) where TValue: IComparable<TValue>, IEquatable<TValue>
        {
            return new FilterParameter<TValue>(name);
        }

        public static FltersBuilder<T> Builder<T>() where T : class, new()
        {
            return new FltersBuilder<T>();
        }


        public static IDictionary<string, object> ParseValuesBag(object parametersBag)
        {
            var result = new Dictionary<string, object>();
            if (parametersBag != null)
            {
                if (parametersBag is IDictionary<string, object>)
                {
                    foreach (var entry in (parametersBag as IDictionary<string, object>))
                    {
                        result[entry.Key] = entry.Value;
                    }
                }
                else
                {
                    var type = parametersBag.GetType();
                    var members = type.GetMembers(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
                    foreach (var member in members)
                    {
                        switch (member.MemberType)
                        {
                            case MemberTypes.Property:
                                result[member.Name] = (member as PropertyInfo).GetGetMethod().Invoke(parametersBag, null);
                                break;
                            case MemberTypes.Field:
                                result[member.Name] = (member as FieldInfo).GetValue(parametersBag);
                                break;
                        }
                    }
                }
            }
            return result;
        }

        public static DynamicScalar DynamicValue(object value)
        {
            return new DynamicScalar(value);
        }

        public static RawPropertyReference RawProperty(string name)
        {
            return new RawPropertyReference(name);
        }

        public static LogicalOperation CompareValue(string propName, object value, FilterComparison op = FilterComparison.Equal)
        {
            return new DynamicComparation(propName, value, op);
        }
    }


}


