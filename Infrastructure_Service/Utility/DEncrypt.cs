using System;
using System.Security.Cryptography;  
using System.Text;


namespace JIT.Utility
{
	/// <summary>
	/// 加密工具类
	/// </summary>
	public static class DEncrypt
	{
		#region 使用 缺省密钥字符串 加密/解密string
		/// <summary>
		/// 使用缺省密钥字符串加密string
		/// </summary>
		/// <param name="pPlaintext">明文</param>
		/// <returns>密文</returns>
		public static string Encrypt(string pPlaintext)
		{
			return Encrypt(pPlaintext,"JIT");
		}
		/// <summary>
		/// 使用缺省密钥字符串解密string
		/// </summary>
        /// <param name="pCiphertext">密文</param>
		/// <returns>明文</returns>
        public static string Decrypt(string pCiphertext)
		{
            return Decrypt(pCiphertext, "JIT", System.Text.Encoding.Default);
		}
		#endregion

		#region 使用 给定密钥字符串 加密/解密string
		/// <summary>
		/// 使用给定密钥字符串加密string
		/// </summary>
		/// <param name="pOriginal">原始文字</param>
		/// <param name="pKey">密钥</param>
		/// <param name="encoding">字符编码方案</param>
		/// <returns>密文</returns>
		public static string Encrypt(string pOriginal, string pKey)  
		{  
			byte[] buff = System.Text.Encoding.Default.GetBytes(pOriginal);  
			byte[] kb = System.Text.Encoding.Default.GetBytes(pKey);
			return Convert.ToBase64String(Encrypt(buff,kb));      
		}
		/// <summary>
		/// 使用给定密钥字符串解密string
		/// </summary>
		/// <param name="pOriginal">密文</param>
		/// <param name="pKey">密钥</param>
		/// <returns>明文</returns>
		public static string Decrypt(string pOriginal, string pKey)
		{
			return Decrypt(pOriginal,pKey,System.Text.Encoding.Default);
		}

		/// <summary>
		/// 使用给定密钥字符串解密string,返回指定编码方式明文
		/// </summary>
		/// <param name="pEncrypted">密文</param>
		/// <param name="pKey">密钥</param>
		/// <param name="pEncoding">字符编码方案</param>
		/// <returns>明文</returns>
		public static string Decrypt(string pEncrypted, string pKey,Encoding pEncoding)  
		{       
			byte[] buff = Convert.FromBase64String(pEncrypted);  
			byte[] kb = System.Text.Encoding.Default.GetBytes(pKey);
			return pEncoding.GetString(Decrypt(buff,kb));      
		}  
		#endregion

		#region 使用 缺省密钥字符串 加密/解密/byte[]
		/// <summary>
		/// 使用缺省密钥字符串解密byte[]
		/// </summary>
		/// <param name="pEncrypted">密文</param>
		/// <param name="key">密钥</param>
		/// <returns>明文</returns>
		public static byte[] Decrypt(byte[] pEncrypted)  
		{  
			byte[] key = System.Text.Encoding.Default.GetBytes("Enterprise"); 
			return Decrypt(pEncrypted,key);     
		}
		/// <summary>
		/// 使用缺省密钥字符串加密
		/// </summary>
		/// <param name="original">原始数据</param>
		/// <param name="key">密钥</param>
		/// <returns>密文</returns>
		public static byte[] Encrypt(byte[] original)  
		{  
			byte[] key = System.Text.Encoding.Default.GetBytes("Enterprise"); 
			return Encrypt(original,key);     
		}  
		#endregion

		#region  使用 给定密钥 加密/解密/byte[]

		/// <summary>
		/// 生成MD5摘要
		/// </summary>
		/// <param name="original">数据源</param>
		/// <returns>摘要</returns>
		public static byte[] MakeMD5(byte[] original)
		{
			MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();   
			byte[] keyhash = hashmd5.ComputeHash(original);       
			hashmd5 = null;  
			return keyhash;
		}


		/// <summary>
		/// 使用给定密钥加密
		/// </summary>
		/// <param name="original">明文</param>
		/// <param name="key">密钥</param>
		/// <returns>密文</returns>
		public static byte[] Encrypt(byte[] original, byte[] key)  
		{  
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();       
			des.Key =  MakeMD5(key);
			des.Mode = CipherMode.ECB;  
     
			return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);     
		}  

		/// <summary>
		/// 使用给定密钥解密数据
		/// </summary>
		/// <param name="encrypted">密文</param>
		/// <param name="key">密钥</param>
		/// <returns>明文</returns>
		public static byte[] Decrypt(byte[] encrypted, byte[] key)  
		{  
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();  
			des.Key =  MakeMD5(key);    
			des.Mode = CipherMode.ECB;  

			return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
		}  
  
		#endregion
	}
}
