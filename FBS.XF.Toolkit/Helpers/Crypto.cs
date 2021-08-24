using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// Encryption/Decryption
	/// </summary>
	public sealed class CryptoHelper
	{
		#region Constants/Enumerations
		// 24 = 192 bits
		private const int SaltByteSize = 24;
		private const int HashByteSize = 24;
		private const int HashIterationsCount = 1024;	

		/// <summary>
		/// Types of symmetric encryption
		/// </summary>
		public enum CryptoType
		{
			// DES and RC2 have been removed due to reported compromises
			Rijndael,
			// ReSharper disable once InconsistentNaming
			TripleDES
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Decrypt a byte array
		/// </summary>
		/// <param name="cryptoType">Type of encryption</param>
		/// <param name="key">Key aka password</param>
		/// <param name="input">Data to encrypt</param>
		/// <returns>Decrypted data</returns>
		public static byte[] Decrypt(CryptoType cryptoType, string key, byte[] input)
		{
			var deriveBytes = new Rfc2898DeriveBytes(key, defaultSalt);
			var algorithm = SelectAlgorithm(cryptoType);
			algorithm.Key = deriveBytes.GetBytes(algorithm.KeySize / 8);
			algorithm.IV = deriveBytes.GetBytes(algorithm.BlockSize / 8);

			return DoCrypt(algorithm.CreateDecryptor(), input);
		}

		/// <summary>
		/// Decrypt a text string
		/// </summary>
		/// <param name="cryptoType">Type of encryption</param>
		/// <param name="key">Key aka password</param>
		/// <param name="inputText">Text to decrypt</param>
		/// <returns>Decrypted text</returns>
		/// <exception cref="ArgumentException">Input text is not Base64;inputText</exception>
		public static string Decrypt(CryptoType cryptoType, string key, string inputText)
		{
			// Create new encoder 
			var utf8Encoder = new UTF8Encoding();

			// Get byte representation of string
			byte[] inputBytes;

			try
			{
				inputBytes = Convert.FromBase64String(inputText);
			}
			catch (FormatException)
			{
				throw new ArgumentException(@"Input text is not Base64", nameof(inputText));
			}

			// Convert back to a string
			return utf8Encoder.GetString(Decrypt(cryptoType, key, inputBytes));
		}

		/// <summary>
		/// Encrypt a byte array
		/// </summary>
		/// <param name="cryptoType">Type of encryption</param>
		/// <param name="key">Key aka password</param>
		/// <param name="input">Data to encrypt</param>
		/// <returns>Encrypted data</returns>
		public static byte[] Encrypt(CryptoType cryptoType, string key, byte[] input)
		{
			var deriveBytes = new Rfc2898DeriveBytes(key, defaultSalt);
			var algorithm = SelectAlgorithm(cryptoType);
			algorithm.Key = deriveBytes.GetBytes(algorithm.KeySize / 8);
			algorithm.IV = deriveBytes.GetBytes(algorithm.BlockSize / 8);

			return DoCrypt(algorithm.CreateEncryptor(), input);
		}

		/// <summary>
		/// Encrypt a text string
		/// </summary>
		/// <param name="cryptoType">Type of encryption</param>
		/// <param name="key">Key aka password</param>
		/// <param name="inputText">Text to encrypt</param>
		/// <returns>Encrypted text</returns>
		public static string Encrypt(CryptoType cryptoType, string key, string inputText)
		{
			// Create new encoder 
			var utf8Encoder = new UTF8Encoding();

			// Get byte representation of string
			var inputBytes = utf8Encoder.GetBytes(inputText);

			// Convert back to a string
			return Convert.ToBase64String(Encrypt(cryptoType, key, inputBytes));
		}

		/// <summary>
		/// Generates the salt.
		/// </summary>
		/// <param name="saltByteSize">Size of the salt byte.</param>
		/// <returns>System.Byte[].</returns>
		public static byte[] GenerateSalt(int saltByteSize = SaltByteSize)
		{
			using (var saltGenerator = new RNGCryptoServiceProvider())
			{
				defaultSalt = new byte[saltByteSize];
				saltGenerator.GetBytes(defaultSalt);
				return defaultSalt;
			}
		}

		/// <summary>
		/// Hashes the specified password.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="username">The username.</param>
		/// <param name="iterations">The iterations.</param>
		/// <param name="hashByteSize">Size of the hash byte.</param>
		/// <returns>System.Byte[].</returns>
		public static byte[] Hash(string password, string username = null, int iterations = HashIterationsCount, int hashByteSize = HashByteSize)
		{
			var saltBytes = string.IsNullOrWhiteSpace(username) ? defaultSalt : Encoding.ASCII.GetBytes(username);

			if (saltBytes.Length < 8)
			{
				saltBytes = saltBytes.Concat(defaultSalt.Take(10 - saltBytes.Length).ToArray()).ToArray();
			}

			using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes))
			{
				deriveBytes.IterationCount = iterations;
				return deriveBytes.GetBytes(hashByteSize);
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Perform the encryption/decryption
		/// </summary>
		/// <param name="transform">Direction</param>
		/// <param name="input">Data to work with</param>
		/// <returns>Cipher'ed data</returns>
		private static byte[] DoCrypt(ICryptoTransform transform, byte[] input)
		{
			// Memory stream for output
			var memoryStream = new MemoryStream();

			try
			{
				// Setup the ebcryption - output written to memory stream
				var cryptStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);

				// Write data to cryption engine
				cryptStream.Write(input, 0, input.Length);
				cryptStream.FlushFinalBlock();

				// Get result
				var output = memoryStream.ToArray();

				// Finished with engine, so Close the stream
				cryptStream.Close();
				return output;
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Error in symmetric engine. Error : " + ex);
			}
		}

		/// <summary>
		///	Returns the specific symmetric algorithm acc. to the crypto type
		/// </summary>
		/// <returns>SymmetricAlgorithm</returns>
		private static SymmetricAlgorithm SelectAlgorithm(CryptoType cryptoType)
		{
			switch (cryptoType)
			{
				case CryptoType.Rijndael:
					return Rijndael.Create();
				case CryptoType.TripleDES:
					return TripleDES.Create();
					// Default is Rijndael
				default:
					return Rijndael.Create();
			}
		}
		#endregion

		#region Properties
		public byte[] Salt { get; set; }
		#endregion

		#region Fields
		private static byte[] defaultSalt = {0x4d, 0x49, 0x76, 0x61, 0x65, 0x76, 0x20, 0x65, 0x64, 0x76, 0x64, 0x6e, 0x65, 0x64, 0x76, 0x61};
		#endregion
	}
}