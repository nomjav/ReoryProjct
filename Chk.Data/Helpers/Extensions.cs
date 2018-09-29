using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AcademyLockSmith.Data.Helpers
{
    public static class Extensions
    {


        public static bool IsEntityFrameworkProvider(this IQueryProvider provider)
        {
            return provider.GetType().FullName == "System.Data.Objects.ELinq.ObjectQueryProvider";
        }

        public static bool IsLinqToObjectsProvider(this IQueryProvider provider)
        {
            return provider.GetType().FullName.Contains("EnumerableQuery");
        }

        public static string FirstSortableProperty(this Type type)
        {
            PropertyInfo firstSortableProperty = type.GetProperties().Where(property => property.PropertyType.IsPredefinedType()).FirstOrDefault();

            if (firstSortableProperty == null)
            {
                throw new NotSupportedException("Cannot find property to sort by.");
            }

            return firstSortableProperty.Name;
        }

        internal static bool IsPredefinedType(this Type type)
        {
            return PredefinedTypes.Any(t => t == type);
        }

        public static readonly Type[] PredefinedTypes = {
            typeof(Object),
            typeof(Boolean),
            typeof(Char),
            typeof(String),
            typeof(SByte),
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert)
        };

       

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, bool markCurrentAsSelected = true) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

            //var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            //var workContext = EngineContext.Current.Resolve<IWorkContext>();

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum)) select new { ID = Convert.ToInt32(enumValue), Name = enumValue };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values, "ID", "Name", selectedValue);
        }

        


        /// <summary>
        /// Relative formatting of DateTime (e.g. 2 hours ago, a month ago)
        /// </summary>
        /// <param name="source">Source (UTC format)</param>
        /// <returns>Formatted date and time string</returns>
        public static string RelativeFormat(this DateTime source)
        {
            return RelativeFormat(source, string.Empty);
        }

        /// <summary>
        /// Relative formatting of DateTime (e.g. 2 hours ago, a month ago)
        /// </summary>
        /// <param name="source">Source (UTC format)</param>
        /// <param name="defaultFormat">Default format string (in case relative formatting is not applied)</param>
        /// <returns>Formatted date and time string</returns>
        public static string RelativeFormat(this DateTime source, string defaultFormat)
        {
            return RelativeFormat(source, false, defaultFormat);
        }

        /// <summary>
        /// Relative formatting of DateTime (e.g. 2 hours ago, a month ago)
        /// </summary>
        /// <param name="source">Source (UTC format)</param>
        /// <param name="convertToUserTime">A value indicating whether we should convet DateTime instance to user local time (in case relative formatting is not applied)</param>
        /// <param name="defaultFormat">Default format string (in case relative formatting is not applied)</param>
        /// <returns>Formatted date and time string</returns>
        public static string RelativeFormat(this DateTime source,
            bool convertToUserTime, string defaultFormat)
        {
            string result = "";

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - source.Ticks);
            double delta = ts.TotalSeconds;

            if (delta > 0)
            {
                if (delta < 60) // 60 (seconds)
                {
                    result = ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
                }
                else

                    if (delta < 120) //2 (minutes) * 60 (seconds)
                    {
                        result = "a minute ago";
                    }
                    else if (delta < 2700) // 45 (minutes) * 60 (seconds)
                    {
                        result = ts.Minutes + " minutes ago";
                    }
                    else if (delta < 5400) // 90 (minutes) * 60 (seconds)
                    {
                        result = "an hour ago";
                    }
                    else if (delta < 86400) // 24 (hours) * 60 (minutes) * 60 (seconds)
                    {
                        int hours = ts.Hours;
                        if (hours == 1)
                            hours = 2;
                        result = hours + " hours ago";
                    }
                    else if (delta < 172800) // 48 (hours) * 60 (minutes) * 60 (seconds)
                    {
                        result = "yesterday";
                    }
                    else if (delta < 2592000) // 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                    {
                        result = ts.Days + " days ago";
                    }
                    else if (delta < 31104000) // 12 (months) * 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                    {
                        int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                        result = months <= 1 ? "one month ago" : months + " months ago";
                    }
                    else
                    {
                        int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                        result = years <= 1 ? "one year ago" : years + " years ago";
                    }
            }
            else
            {
                DateTime tmp1 = source;
                if (convertToUserTime)
                {
                    //tmp1 = EngineContext.Current.Resolve<IDateTimeHelper>().ConvertToUserTime(tmp1, DateTimeKind.Utc);
                }
                //default formatting
                if (!String.IsNullOrEmpty(defaultFormat))
                {
                    result = tmp1.ToString(defaultFormat);
                }
                else
                {
                    result = tmp1.ToString();
                }
            }
            return result;
        }

        public static string RelativeFormat(this int totalMinutes)
        {
            string result = "";

            //var ts = new TimeSpan(DateTime.UtcNow.Ticks - source.Ticks);
            //double delta = ts.TotalSeconds;
            double delta = totalMinutes;

            if (delta > 0)
            {
                //if (delta < 60) // 60 (seconds)
                //{
                //	result = ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
                //}
                //else

                if (delta < 2) //2 (minutes) * 60 (seconds)
                {
                    result = "1 M";
                }
                else if (delta < 45) // 45 (minutes) * 60 (seconds)
                {
                    result = delta + " M";
                }
                else if (delta < 90) // 90 (minutes) * 60 (seconds)
                {
                    result = "1 H";
                }
                else if (delta < 1440) // 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    int hours = (int)(delta / 60);
                    if (hours == 1)
                        hours = 2;
                    result = hours + " H";
                }
                else if (delta < 2880) // 48 (hours) * 60 (minutes) * 60 (seconds)
                {
                    result = "1 D";
                }
                else //if (delta < 2592000) // 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    result = (int)(delta / 1440) + " D";
                }
                //else if (delta < 31104000) // 12 (months) * 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                //{
                //	int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                //	result = months <= 1 ? "1 Month" : months + " Months";
                //}
                //else
                //{
                //	int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                //	result = years <= 1 ? "1 Year" : years + " Years";
                //}
            }
            else
            {
                result = "0 M";
            }
            return result;
        }


        public static string RenderRazorViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://", StringComparison.Ordinal) > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }
        /// <summary>
        /// 	Convert the Object to the XML Representaion of it
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "obj"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T obj)
        {
            if (obj == null)
                return "";

            DataContractSerializer dataContractSerializer = new DataContractSerializer(obj.GetType());

            String text;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                dataContractSerializer.WriteObject(memoryStream, obj);

                byte[] data = new byte[memoryStream.Length];
                Array.Copy(memoryStream.GetBuffer(), data, data.Length);
                text = Encoding.UTF8.GetString(data);
            }

            return text;
        }
        /// <summary>
        /// remove specific string between two characters
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="char1"></param>
        /// <param name="char2"></param>
        /// <returns></returns>
        public static string RemoveStringBetweenCharacters(string fileContent, string char1, string char2)
        {
            int indexOfOpen = fileContent.IndexOf('^');
            int indexOfClose = fileContent.IndexOf('|', indexOfOpen + 1);
            string results = fileContent.Substring(0, indexOfOpen) + fileContent.Substring(indexOfClose + 1);
            return results;
        }

        public static string GetCurrentIpAddress()
        {
            if (HttpContext.Current.Request.UserHostAddress != null)
                return HttpContext.Current.Request.UserHostAddress;
            return string.Empty;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static DateTime ToDateTime(this string date, string format = "MM/dd/yyyy")
        {
            return DateTime.ParseExact(date.Trim(), format, CultureInfo.InvariantCulture);
        }

        public static string ReplaceString(this string str, string oldValue, string newValue, StringComparison comparison = StringComparison.CurrentCultureIgnoreCase)
        {
            StringBuilder sb = new StringBuilder();
            if (oldValue != "" || !string.IsNullOrEmpty(oldValue))
            {
                int previousIndex = 0;
                int index = str.IndexOf(oldValue, comparison);
                while (index != -1)
                {
                    sb.Append(str.Substring(previousIndex, index - previousIndex));
                    sb.Append(newValue);
                    index += oldValue.Length;

                    previousIndex = index;
                    index = str.IndexOf(oldValue, index, comparison);
                }
                sb.Append(str.Substring(previousIndex));
            }
            return sb.ToString();
        }

        public static string ToProperCaseString(this string str, CultureInfo cultureInfo = null)
        {
            try
            {
                if (!String.IsNullOrEmpty(str))
                {
                    string title = str.ToLower();
                    TextInfo textInfo = null;
                    if (cultureInfo == null)
                        textInfo = new CultureInfo("en-US", false).TextInfo;
                    else
                        textInfo = new CultureInfo(cultureInfo.Name, false).TextInfo;
                    title = textInfo.ToTitleCase(title);
                    return title;
                }
                return "";
            }
            catch (Exception ex)
            {
                //Logger.Logger.LogException(ex.Message, ex);
                return str;
            }
        }

    }
}
