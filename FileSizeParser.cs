using System.Globalization;

namespace Loxifi.FileSizeParsing
{
	public static class FileSizeParser
	{
		public static ulong Parse(string size)
		{
			if (TryParse(size, out ulong result))
			{
				return result;
			}

			throw new FormatException();
		}

		public static string ToString(SizeMagnitude fileSize, ulong value, int decimalPlaces = 0)
		{
			NumberFormatInfo nInfo = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();

			nInfo.NumberDecimalDigits = decimalPlaces;

			float parsed = ((float)value) / fileSize.Magnitude;

			string lstring = string.Format(nInfo, "{0:N}", parsed);

			return $"{lstring} {fileSize.Suffix}";
		}

		public static string ToString(ulong value, int decimalPlaces = 0)
		{
			if (value == 0)
			{
				return ToString(SizeMagnitudes.Byte, value, decimalPlaces);
			}

			foreach (SizeMagnitude fileSize in SizeMagnitudes.All.OrderByDescending(s => s.Magnitude))
			{
				if (value >= fileSize.Magnitude)
				{
					return ToString(fileSize, value, decimalPlaces);
				}
			}

			throw new NotImplementedException();
		}

		public static bool TryParse(string size, out ulong v)
		{
			string numberPart = string.Empty;

			string letterPart = string.Empty;

			foreach (char c in size)
			{
				if (char.IsDigit(c) || c == '.' || c == ',')
				{
					numberPart += c;
					continue;
				}

				if (char.IsLetter(c))
				{
					letterPart += c;
					continue;
				}

				if (c == '-')
				{
					v = 0;
					return false;
				}
			}

			if (string.IsNullOrEmpty(numberPart))
			{
				v = ulong.Parse(size);
				return true;
			}

			double nv = double.Parse(numberPart, CultureInfo.CurrentCulture.NumberFormat);

			SizeMagnitude? sm = SizeMagnitudes.All.FirstOrDefault(f => f.MatchesString(letterPart));

			if (sm is null)
			{
				v = 0;

				return false;
			}

			v = (ulong)(nv * sm.Magnitude);

			return true;
		}
	}
}