using System;

namespace MentolProvision.Extensions
{
	static class StringExtensions
	{
		/// <summary>
		/// Переводит первый символ строки в верхний регистр
		/// </summary>
		/// <param name="str">Входная строка</param>
		/// <returns>Результат операции</returns>
		public static string Capitalize(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return str;

			var chars = str.ToCharArray();
			if (char.IsLower(chars[0]))
			{
				chars[0] = char.ToUpper(chars[0]);
				return new string(chars);
			}
			return str;
		}

		/// <summary>
		/// Сравнение строк
		/// </summary>
		/// <param name="string">Проверяемая строка</param>
		/// <param name="compareTo">Строка для сравнения</param>
		/// <param name="comparison">Правила стравнения</param>
		/// <returns>Результат сравнения</returns>
		public static bool Is(this string @string, string compareTo, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
		{
			if (@string == null && compareTo == null) return true;

			return (@string?.Equals(compareTo, comparison)).GetValueOrDefault();
		}
	}
}
