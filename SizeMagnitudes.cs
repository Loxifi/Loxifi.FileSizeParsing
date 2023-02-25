namespace Loxifi.FileSizeParsing
{
	public static class SizeMagnitudes
	{
		public static List<SizeMagnitude> All;

		public static SizeMagnitude Byte = new(1, "b", "byte");

		public static SizeMagnitude Gigabyte = new(1_000_000_000, "gb", "gigabyte");

		public static SizeMagnitude Kilobyte = new(1_000, "kb", "kilobyte");

		public static SizeMagnitude Megabyte = new(1_000_000, "mb", "megabyte");

		public static SizeMagnitude Petabyte = new(1_000_000_000_000_000, "pb", "petabyte");

		public static SizeMagnitude Terabyte = new(1_000_000_000_000, "tb", "terabyte");

		static SizeMagnitudes()
		{
			All = new List<SizeMagnitude>
			{
				Byte,
				Kilobyte,
				Megabyte,
				Gigabyte,
				Terabyte,
				Petabyte
			};
		}
	}
}