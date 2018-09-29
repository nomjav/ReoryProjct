using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AcademyLockSmith.Data.Helpers
{
    public static class CSVHelper
    {
        /// <summary>
        /// Serialize objects to Comma Separated Value (CSV) format [1].
        /// 
        /// Rather than try to serialize arbitrarily complex types with this
        /// function, it is better, given type A, to specify a new type, A'.
        /// Have the constructor of A' accept an object of type A, then assign
        /// the relevant values to appropriately named fields or properties on
        /// the A' object.
        /// </summary>
        /// 
        public static string ToCSV<T>(this IEnumerable<T> objects, Type type = null, string CsvSeparator = ",")
        {
            StringBuilder output = new StringBuilder();
            var fields =
                from mi in typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                where new[] { MemberTypes.Field, MemberTypes.Property }.Contains(mi.MemberType)

                select mi;

            if (type != null)
            {

                PropertyInfo[] props = type.GetProperties();

                List<string> record = new List<string>();

                foreach (PropertyInfo prop in props)
                {
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        DisplayNameAttribute authAttr = attr as DisplayNameAttribute;
                        if (authAttr != null)
                        {
                            string fieldName = authAttr.DisplayName;
                            record.Add(fieldName);
                        }
                    }
                }

                output.AppendLine(QuoteRecord(record, CsvSeparator));
            }

            else
            {
                output.AppendLine(QuoteRecord(fields.Select(f => f.Name), CsvSeparator));
            }

            foreach (var record in objects)
            {
                output.AppendLine(QuoteRecord(FormatObject(fields, record), CsvSeparator));
            }
            return output.ToString();
        }

        public static string WeeklyJobsToCSV<T>(this IEnumerable<T> objects, int weekNum, Type type = null, string CsvSeparator = ",")
        {
            List<MemberInfo> list1 = new List<MemberInfo>();
            StringBuilder output = new StringBuilder();
            var fields =
                from mi in typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                where new[] { MemberTypes.Field, MemberTypes.Property }.Contains(mi.MemberType)
                select mi;

            if (type != null)
            {
                List<PropertyInfo> props = type.GetProperties().ToList();
                List<PropertyInfo> toBeRemoved = new List<PropertyInfo>();
                foreach (var item in props)
                {
                    try
                    {
                        if ((!item.Name.ToLower().Contains("customer")) && (!item.Name.ToLower().Contains("total")))
                        {
                            string[] pr = item.Name.Split('_');
                            int num = Convert.ToInt32(pr[1]);
                            if (num > weekNum)
                            {
                                toBeRemoved.Add(item);
                            }
                        }
                    }
                    catch (Exception ex) {
                        var msg = ex.Message;
                    }
                }

                foreach (var item in toBeRemoved)
                {

                    props.Remove(item);
                   
                }

                list1 = fields.ToList();

                foreach (var field in fields)
                {
                    var pi = (PropertyInfo)field;
                    if (toBeRemoved.Contains(pi))
                        list1.Remove(field);
                }

                fields = list1.AsEnumerable();


                List<string> record = new List<string>();

                foreach (PropertyInfo prop in props)
                {
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        DisplayNameAttribute authAttr = attr as DisplayNameAttribute;
                        if (authAttr != null)
                        {
                            string fieldName = authAttr.DisplayName;
                            record.Add(fieldName);
                        }
                    }
                }
                output.AppendLine(QuoteRecord(record, CsvSeparator));
            }
            else
            {
                output.AppendLine(QuoteRecord(fields.Select(f => f.Name), CsvSeparator));
            }
            foreach (var record in objects)
            {
               output.AppendLine(QuoteRecord(FormatObject(fields, record), CsvSeparator));
            }
            return output.ToString();
        }


        static IEnumerable<string> FormatObject<T>(IEnumerable<MemberInfo> fields, T record)
        {
            foreach (var field in fields)
            {
                if (field is FieldInfo)
                {
                    var fi = (FieldInfo)field;
                    yield return Convert.ToString(fi.GetValue(record));
                }
                else if (field is PropertyInfo)
                {
                    var pi = (PropertyInfo)field;
                    yield return Convert.ToString(pi.GetValue(record, null));
                }
                else
                {
                    throw new Exception("Unhandled case.");
                }
            }
        }

        //const string CsvSeparator = ",";

        static string QuoteRecord(IEnumerable<string> record, string CsvSeparator)
        {
            return String.Join(CsvSeparator, record.Select(field => QuoteField(field, CsvSeparator)).ToArray());
        }

        static string QuoteField(string field, string CsvSeparator)
        {
            if (String.IsNullOrEmpty(field))
            {
                return "\"\"";
            }
            else if (field.Contains(CsvSeparator) || field.Contains("\"") || field.Contains("\r") || field.Contains("\n"))
            {
                return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else
            {
                return field;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnOrderAttribute : Attribute
    {
        public int Order { get; private set; }
        public ColumnOrderAttribute(int order) { Order = order; }
    }
}
