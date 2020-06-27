using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DCMAPI.DBAccess
{
    public static class DataReaderExtensionMethods
    {
        /// <summary>
        /// Extension method for IDataReader to map a entity with result set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static IList<T> AutoMap<T>(this IDataReader dr)
        {
            //Declare a IList 
            IList<T> lst = new List<T>();

            try
            {
                var columns = Enumerable.Range(0, dr.FieldCount).Select(dr.GetName).ToList().Select(x => x.ToLower()).ToList();

                //Foreach datarow 
                while (dr.Read())
                {
                    //Create a new instance first 
                    T instance = (T)Activator.CreateInstance(typeof(T));

                    //Foreach property in the instance set the value 
                    foreach (PropertyInfo objField in typeof(T).GetProperties())
                    {
                        //if not collection type
                        if (!objField.GetType().IsGenericType)
                        {
                            string strColumnName = GetBoundColumnName(instance, objField.Name);
                            try
                            {
                                //if column name
                                if (!string.IsNullOrWhiteSpace(strColumnName) && columns.Contains(strColumnName.ToLower()))
                                {
                                    //Check for NULL 
                                    if (dr[strColumnName] != System.DBNull.Value)
                                    {
                                        objField.SetValue(instance, TypeConverter.ChangeType(dr[strColumnName], objField.PropertyType), null);
                                    }
                                }
                            }
                            catch (Exception) { }
                        }
                    }

                    //Add the instance to the list 
                    lst.Add(instance);
                }
            }
            catch (Exception ex) { throw new Exception("Error in class-property data binding with the data reader. Error: " + ex.Message); }

            //Return the list 
            return lst;
        }


        /// <summary>
        /// Returns the bound column name for each property
        /// </summary>
        /// <param name="objTarget"></param>
        /// <param name="strFieldName"></param>
        /// <returns></returns>
        private static string GetBoundColumnName(object objTarget, string strFieldName)
        {
            Type objType = objTarget.GetType();
            PropertyInfo objField = objType.GetProperty(strFieldName);
            System.ComponentModel.DataAnnotations.Schema.ColumnAttribute[] lstAttribute
                = (System.ComponentModel.DataAnnotations.Schema.ColumnAttribute[])objField.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), true);
            if (lstAttribute.Length > 0)
                return lstAttribute[0].Name;
            else
            {
                //check if not mapped attributed
                System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute[] lstNotMappedAttribute
                = (System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute[])objField.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute), true);

                //if not mapped attributed, return null
                if (lstNotMappedAttribute.Length > 0)
                    return "";
            }

            return strFieldName;
        }
    }
}
