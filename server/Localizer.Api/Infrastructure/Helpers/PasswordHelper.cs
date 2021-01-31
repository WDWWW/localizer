using System;
using System.Security.Cryptography;
using Localizer.Common.Helpers;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Localizer.Api.Infrastructure.Helpers
{
	public static class PasswordHelper
	{
		private const int IterCount = 10000;
		private const int HashByteSize = 32;
		private const int SaltByteSize = 16;
		private static readonly KeyDerivationPrf HashType;

		public const string SpecialCharacters = "!@#$%_";

		static PasswordHelper()
		{
			HashType = KeyDerivationPrf.HMACSHA512;
		}

		/// <summary>
		///     Pbkdf2로 구현된 Hash함수, 매 해쉬 결과는 다르므로 비교가 필요하신 경우 <see cref="VerifyPassword" />를 사용해주시길
		///     바랍니다.
		/// </summary>
		/// <param name="password">해싱할 패스워드</param>
		/// <returns>Salt가 내부적으로 포함된 BASE64 Hash String</returns>
		public static string HashPassword(string password)
		{
			using var provider = new RNGCryptoServiceProvider();
			var salt = new byte[SaltByteSize];
			provider.GetBytes(salt);
			return HashPassword(password, salt);
		}

		private static string HashPassword(string password, byte[] salt)
		{
			// Pbkdf2를 사용한 이유는 https://d2.naver.com/helloworld/318732를 참고부탁드립니다.
			var hash = KeyDerivation.Pbkdf2(password, salt, HashType, IterCount, HashByteSize);
			var result = new byte[SaltByteSize + HashByteSize];

			CopySalt(salt, result);
			Buffer.BlockCopy(hash, 0, result, 0 + SaltByteSize, HashByteSize);

			return Convert.ToBase64String(result);
		}

		/// <summary>
		///     해시와 패스워드를 비교 여부 반환
		/// </summary>
		/// <param name="hash"></param>
		/// <param name="password"></param>
		/// <returns>같을 경우 true, 아닐경우 false</returns>
		/// <exception cref="ArgumentException">hash의 byte size가 48이 아닌경우 에러가 발생합니다.</exception>
		public static bool VerifyPassword(string hash, string password)
		{
			var hashBytes = Convert.FromBase64String(hash);

			if (hashBytes.Length != SaltByteSize + HashByteSize)
				throw new ArgumentException($"{nameof(hash)}의 byte 사이즈는 반드시 {SaltByteSize + HashByteSize}이여야 합니다.");

			var salt = new byte[SaltByteSize];
			CopySalt(hashBytes, salt);

			return HashPassword(password, salt) == hash;
		}

		private static void CopySalt(byte[] saltBytes, byte[] target)
		{
			Buffer.BlockCopy(saltBytes, 0, target, 0, SaltByteSize);
		}

		public static string GenerateTempPassword(int length)
		{
			if (length < 4) 
				throw new ArgumentException("length는 반드시 4자 이상이여야 합니다.");

			var partLength = length / 4;
			return GenerateTempPassword(partLength * 2, partLength, length - (partLength * 3));
		}

		public static string GenerateTempPassword(int alphabets, int numbers, int specials)
		{
			var length = alphabets + numbers + specials;
			var tempPasswordBase = (CryptoHelper.GenerateToken("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", alphabets)
					+ CryptoHelper.GenerateToken("0123456789", numbers)
					+ CryptoHelper.GenerateToken(SpecialCharacters, specials))
				.ToCharArray();

			var random = new Random();
			for (var i = 0; i < length / 2; i++)
			{
				var from = random.Next(0, length - 1);
				var to = length - 1 - from;
				var temp = tempPasswordBase[from];
				tempPasswordBase[from] = tempPasswordBase[to];
				tempPasswordBase[to] = temp;
			}

			return new string(tempPasswordBase);
		}
	}

}