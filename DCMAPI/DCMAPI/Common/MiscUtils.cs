using DCMAPI.DBAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace DCMAPI.Common
{
    public static class MiscUtils
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public static string Encrypt(string toEncrypt, bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(AppConstants.APPSECRET_KEY));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(AppConstants.APPSECRET_KEY);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            //return ByteToHex(resultArray);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }

        public static string Decrypt(string cipherString, bool useHashing = true)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            //byte[] toEncryptArray = HexToByte(cipherString);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(AppConstants.APPSECRET_KEY));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(AppConstants.APPSECRET_KEY);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string ByteToHex(byte[] input)
        {
            return input.Aggregate(new StringBuilder(), (sb, v) => sb.Append(v.ToString("x2"))).ToString();
        }

        public static string GenerateParameterString(List<SqlParameter> sqlParameters)
        {
            return sqlParameters.Aggregate(new StringBuilder(), (sb, x) => sb.Append(string.Format("{0}={1},", x.ParameterName.Substring(0, (x.ParameterName.Length - 1)), x.ParameterName))).ToString().TrimEnd(new char[] { ',' });
        }
        public enum Action
        {
            Add = 1,
            Edit = 2,
            Delete = 3,
            Retrive = 4,
            Print = 5
        }
    }
}
