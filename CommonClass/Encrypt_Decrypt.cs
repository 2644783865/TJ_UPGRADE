using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Text;


    public class Encrypt_Decrypt
    {
        //对密码进行MD5加密的函数(添加盐值：&%#@?,:*)
        public string getEncryPassword(string Password)
        {
            string EncryedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(
                Password + "&%#@?,:*", "md5"); // Or "sha1" 
            return EncryedPassword;
        }
        // 加密
        public string EncryptText(String strText)
        {
            return Encrypt(strText, "&%#@?,:*");
            // return Encrypt(strText,DateTime.Now.ToString() );
        }

        //'解密
        public String DecryptText(String strText)
        {
            return Decrypt(strText, "&%#@?,:*");
            // return Decrypt(strText,DateTime.Now.ToString());
        }
        //'加密函数
        private String Encrypt(String strText, String strEncrKey)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x01, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //'解密函数
        private String Decrypt(String strText, String sDecrKey)
        {

            char[] stBase = strText.ToCharArray();
            for (int i = 0; i < stBase.Length; i++)
            {
                if (stBase[i] == ' ')
                {
                    stBase[i] = '+';
                }
            }
            strText = "";
            for (int i = 0; i < stBase.Length; i++)
            {
                strText += stBase[i];
            }
            Byte[] byKey = { };
            Byte[] IV = { 0x01, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            Byte[] inputByteArray = new byte[strText.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                inputByteArray = Convert.FromBase64String(strText);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region 8位密钥
        public string MD5Encrypt(string pToEncrypt, string sKey)
        {
            //8位密钥，16位加密结果
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }//8:9F7E6FFB4D70A38C


        /**/
        ///MD5解密  
        public string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0;
             x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

       

        #region 密钥不限定位数


        public byte[] getbyte(string str)
        {
            return Encoding.Default.GetBytes(str);
        }
        ///md5加密解密 
        ///   <param   name= "strkey ">   密钥不限定位数 </param> 
        public string md5Encrypt(String plainText, string strkey)
        {
            if (plainText.Trim() == " ") return " ";
            string encrypted = null;
            byte[] key = getbyte(strkey);
            try
            {
                byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
                byte[] pwdhash = null;
                MD5CryptoServiceProvider hashmd5;
                //generate   an   MD5   hash   from   the   password.   
                //a   hash   is   a   one   way   encryption   meaning   once   you   generate 
                //the   hash,   you   cant   derive   the   password   back   from   it. 
                hashmd5 = new MD5CryptoServiceProvider();
                pwdhash = hashmd5.ComputeHash(key);
                hashmd5 = null;
                //   Create   a   new   TripleDES   service   provider   
                TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
                tdesProvider.Key = pwdhash;
                tdesProvider.Mode = CipherMode.ECB;

                encrypted = Convert.ToBase64String(
                tdesProvider.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
            }
            catch (Exception e)
            {
                string str = e.Message;
                throw;
            }
            return encrypted;
        }
        ///   <summary> 
        ///   md5解密 
        ///   </summary> 
        ///   <param   name= "encryptedString "> </param> 
        ///   <param   name= "strkey "> 密钥 </param> 
        ///   <returns> </returns> 
        public String md5Decrypt(string encryptedString, string strkey)
        {
            if (encryptedString.Trim() == " ") return " ";
            string decyprted = null;
            byte[] inputBytes = null;
            byte[] key = getbyte(strkey);
            try
            {
                inputBytes = Convert.FromBase64String(encryptedString);
                byte[] pwdhash = null;
                MD5CryptoServiceProvider hashmd5;
                //generate   an   MD5   hash   from   the   password.   
                //a   hash   is   a   one   way   encryption   meaning   once   you   generate 
                //the   hash,   you   cant   derive   the   password   back   from   it. 
                hashmd5 = new MD5CryptoServiceProvider();
                pwdhash = hashmd5.ComputeHash(key);
                hashmd5 = null;
                //   Create   a   new   TripleDES   service   provider   
                TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
                tdesProvider.Key = pwdhash;
                tdesProvider.Mode = CipherMode.ECB;
                decyprted = ASCIIEncoding.ASCII.GetString(tdesProvider.CreateDecryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
            }
            catch (Exception e)
            {
                string str = e.Message;
                throw;
            }
            return decyprted;
        }

        #endregion
        ///   <summary> 
        ///   md5加密 
        ///   </summary> 
        ///   <param   name= "source "> </param> 
        ///   <returns> </returns> 
        public string md5Encrypt(string source)
        {
            //作为密码方式加密   
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
            //显示出来   
        }

        #region 简单的加密

        /// <summary>
        /// GetMD5Hash函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string GetMD5Hash(string sDataIn)
        {
            //byte[] b = Encoding.Default.GetBytes(str);
            //b = new MD5CryptoServiceProvider().ComputeHash(b);
            //string ret = "";
            //for (int i = 0; i < b.Length; i++)
            //    ret += b[i].ToString("x").PadLeft(2, '0');
            //return ret;

            System.Security.Cryptography.MD5CryptoServiceProvider md5 =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = " ";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("x2");
            }
            return sTemp.ToUpper();
        }


        ///   <summary> 
        ///   MD5加密 
        ///   </summary> 
        ///   <param   name= "sDataIn "> </param> 
        ///   <returns> </returns> 
        public string GetMD5(string sDataIn)
        {


            //System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            string passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(sDataIn, "MD5");
            return passWord;
            // C9F0F895FB98AB9159F51FD0297E236D C9F0F895FB98AB9159F51FD0297E236D  C9F0F895FB98AB9159F51FD0297E236D
            // D41D8CD98F00B204E9800998ECF8427E  2A38A4A9316C49E5A833517C45D31070
            //  21218CCA77804D2BA1922C33E0151105  21218CCA77804D2BA1922C33E0151105
        }

        #endregion

       
    }

