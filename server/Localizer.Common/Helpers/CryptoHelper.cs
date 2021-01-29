// unset

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Localizer.Common.Helpers
{
	public static class CryptoHelper
	{
		  private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
          private static readonly Random Random = new();
        
          /// <summary>
          ///     랜덤 토큰 생성기, 시작 시간에 의존됨
          ///     NOTE : 강력한 랜덤 스트링 생성기가 필요하다면
          ///     https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string 참고
          /// </summary>
          /// <param name="length"></param>
          /// <returns></returns>
          public static string GenerateToken(int length)
          {
            return GenerateToken(Alphabet, length);
          }
        
          public static string GenerateToken(string characters, int length)
          {
            return new(Enumerable
              .Range(0, length)
              .Select(_ => characters[Random.Next() % characters.Length])
              .ToArray());
          }
        
          public static string GenerateHash(string text)
          {
            using (var hash = SHA256.Create())
            {
              return Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(text)));
            }
          }
        
          /// <summary>
          ///     해시와 텍스트 비교
          /// </summary>
          /// <param name="hash"></param>
          /// <param name="text"></param>
          /// <returns>텍스트와 해시를 비교하여 같다면 true, 아니라면 false</returns>
          public static bool CompareHash(string hash, string text)
          {
            return GenerateHash(text) == hash;
          }
	}
}