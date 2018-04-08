using System;
using System.Security.Cryptography;  
using System.Text;


namespace JIT.Utility
{
	/// <summary>
	/// ���ܹ�����
	/// </summary>
	public static class DEncrypt
	{
		#region ʹ�� ȱʡ��Կ�ַ��� ����/����string
		/// <summary>
		/// ʹ��ȱʡ��Կ�ַ�������string
		/// </summary>
		/// <param name="pPlaintext">����</param>
		/// <returns>����</returns>
		public static string Encrypt(string pPlaintext)
		{
			return Encrypt(pPlaintext,"JIT");
		}
		/// <summary>
		/// ʹ��ȱʡ��Կ�ַ�������string
		/// </summary>
        /// <param name="pCiphertext">����</param>
		/// <returns>����</returns>
        public static string Decrypt(string pCiphertext)
		{
            return Decrypt(pCiphertext, "JIT", System.Text.Encoding.Default);
		}
		#endregion

		#region ʹ�� ������Կ�ַ��� ����/����string
		/// <summary>
		/// ʹ�ø�����Կ�ַ�������string
		/// </summary>
		/// <param name="pOriginal">ԭʼ����</param>
		/// <param name="pKey">��Կ</param>
		/// <param name="encoding">�ַ����뷽��</param>
		/// <returns>����</returns>
		public static string Encrypt(string pOriginal, string pKey)  
		{  
			byte[] buff = System.Text.Encoding.Default.GetBytes(pOriginal);  
			byte[] kb = System.Text.Encoding.Default.GetBytes(pKey);
			return Convert.ToBase64String(Encrypt(buff,kb));      
		}
		/// <summary>
		/// ʹ�ø�����Կ�ַ�������string
		/// </summary>
		/// <param name="pOriginal">����</param>
		/// <param name="pKey">��Կ</param>
		/// <returns>����</returns>
		public static string Decrypt(string pOriginal, string pKey)
		{
			return Decrypt(pOriginal,pKey,System.Text.Encoding.Default);
		}

		/// <summary>
		/// ʹ�ø�����Կ�ַ�������string,����ָ�����뷽ʽ����
		/// </summary>
		/// <param name="pEncrypted">����</param>
		/// <param name="pKey">��Կ</param>
		/// <param name="pEncoding">�ַ����뷽��</param>
		/// <returns>����</returns>
		public static string Decrypt(string pEncrypted, string pKey,Encoding pEncoding)  
		{       
			byte[] buff = Convert.FromBase64String(pEncrypted);  
			byte[] kb = System.Text.Encoding.Default.GetBytes(pKey);
			return pEncoding.GetString(Decrypt(buff,kb));      
		}  
		#endregion

		#region ʹ�� ȱʡ��Կ�ַ��� ����/����/byte[]
		/// <summary>
		/// ʹ��ȱʡ��Կ�ַ�������byte[]
		/// </summary>
		/// <param name="pEncrypted">����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
		public static byte[] Decrypt(byte[] pEncrypted)  
		{  
			byte[] key = System.Text.Encoding.Default.GetBytes("Enterprise"); 
			return Decrypt(pEncrypted,key);     
		}
		/// <summary>
		/// ʹ��ȱʡ��Կ�ַ�������
		/// </summary>
		/// <param name="original">ԭʼ����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
		public static byte[] Encrypt(byte[] original)  
		{  
			byte[] key = System.Text.Encoding.Default.GetBytes("Enterprise"); 
			return Encrypt(original,key);     
		}  
		#endregion

		#region  ʹ�� ������Կ ����/����/byte[]

		/// <summary>
		/// ����MD5ժҪ
		/// </summary>
		/// <param name="original">����Դ</param>
		/// <returns>ժҪ</returns>
		public static byte[] MakeMD5(byte[] original)
		{
			MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();   
			byte[] keyhash = hashmd5.ComputeHash(original);       
			hashmd5 = null;  
			return keyhash;
		}


		/// <summary>
		/// ʹ�ø�����Կ����
		/// </summary>
		/// <param name="original">����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
		public static byte[] Encrypt(byte[] original, byte[] key)  
		{  
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();       
			des.Key =  MakeMD5(key);
			des.Mode = CipherMode.ECB;  
     
			return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);     
		}  

		/// <summary>
		/// ʹ�ø�����Կ��������
		/// </summary>
		/// <param name="encrypted">����</param>
		/// <param name="key">��Կ</param>
		/// <returns>����</returns>
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
