using System;
using System.ComponentModel;
using System.Linq;

namespace NibrsModels.Extensions
{
    public static class EnumExtensions
    {
        #region Description Attributes
        public static string GetDescription(this Enum enumType)
        {
            var fi = enumType.GetType().GetField(enumType.ToString());
            var attr = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attr.Length > 0 ? attr[0].Description : enumType.ToString();
        }

        public static T GetDescriptionForAttributeType<T>(this Enum enumType)
        {
            var fi = enumType.GetType().GetField(enumType.ToString());
            var attr = fi.GetCustomAttributes(typeof(DescriptionAttribute), false).Where(element => element.GetType() == typeof(T));

            return (T)attr.FirstOrDefault();
        }

        public static TAttributeType GetDescription<TAttributeType>(this Enum enumType)
        {
            var fi = enumType.GetType().GetField(enumType.ToString());
            var attr = fi.GetCustomAttributes(typeof(DescriptionAttribute), false).Where(element => element.GetType() == typeof(TAttributeType));

            return (TAttributeType)attr.FirstOrDefault();
        }
        #endregion
    }
}
