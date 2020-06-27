using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMAPI.DBAccess
{
    public static class TypeConverter
    {
        public static object ChangeType(object value, Type conversionType)
        {
            //Throw error if null 
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType 
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;

                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            return Convert.ChangeType(value, conversionType);
        }
    } 
}
