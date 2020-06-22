using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionLib
{
	public static class Encryption
	{
		private const string AESKEY = "smkldospdosldaaa";
		public static string AesEncrypt(string str)
		{
			if (string.IsNullOrEmpty(str)) return null;
			Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

			RijndaelManaged rm = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(AESKEY),
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};

			ICryptoTransform cTransform = rm.CreateEncryptor();
			Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
			return Convert.ToBase64String(resultArray);
		}

		public static string AesDecrypt(string str)
		{
			if (string.IsNullOrEmpty(str)) return null;
			Byte[] toEncryptArray = Convert.FromBase64String(str);

			RijndaelManaged rm = new RijndaelManaged
			{
				Key = Encoding.UTF8.GetBytes(AESKEY),
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			};

			ICryptoTransform cTransform = rm.CreateDecryptor();
			Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

			return Encoding.UTF8.GetString(resultArray);
		}


        private static byte[] _KEY = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        private static byte[] _IV = new byte[] { 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01 };   
        public static string DesEncrypt(string normalTxt)
        {
            //byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
            //byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(_KEY, _IV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(normalTxt);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();

            string strRet = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            return strRet;
        }
        
        public static string DesDecrypt(string securityTxt)//解密  
        {
            //byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(_KEY);
            //byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV);
            byte[] byEnc;
            try
            {
                securityTxt.Replace("_%_", "/");
                securityTxt.Replace("-%-", "#");
                byEnc = Convert.FromBase64String(securityTxt);
            }
            catch
            {
                return null;
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(_KEY, _IV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }
	}
}
