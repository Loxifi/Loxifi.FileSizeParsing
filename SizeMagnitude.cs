namespace Loxifi.FileSizeParsing
{
	public class SizeMagnitude
	{
		public SizeMagnitude(ulong magnitude, string suffix, params string[] parse)
		{
			Magnitude = magnitude;
			Suffix = suffix ?? throw new ArgumentNullException(nameof(suffix));
			ParseStrings.Add(suffix);

			if (parse != null)
			{
				foreach (string p in parse)
				{
					ParseStrings.Add(p);
				}
			}
		}

		public ulong Magnitude { get; set; }

		public List<string> ParseStrings { get; set; } = new List<string>();

		public string Suffix { get; set; }

		public bool MatchesString(string toMatch) => ParseStrings.Any(p => string.Equals(p, toMatch, StringComparison.OrdinalIgnoreCase));
	}
}