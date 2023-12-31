using AggregaRisultati.Models;

namespace AggregaRisultati.Writers;

public class CsvWriter : IWriter
{
	public string Write(DirectoryInfo directory, List<DifferenceDto> differences, List<PolizzaInputDto> polizzeInput, List<TimesDto> times)
	{
		string outputFile = Path.Combine(directory.FullName, "Differenze.csv");

		using (var writer = new StreamWriter(outputFile))
		{
			Aggregator headerAggregator = new();
			WriteLine(writer, headerAggregator, true);
			foreach (DifferenceDto difference in differences)
			{
				Aggregator aggregator = new(difference);
				WriteLine(writer, aggregator, false);
			}
		}
		return outputFile;
	}

	private static void WriteLine(StreamWriter writer, Aggregator aggregator, bool header)
	{
		for (int i = 0; i < Aggregator.NUMBER_OF_FIELDS; i++)
		{
			string separator = i == 0 ? "" : ";";
			string value = aggregator.Aggregate(i, header);
			writer.Write(separator + Normalize(value));
		}
		writer.WriteLine();
		writer.Flush();
	}

	private static string Normalize(string value)
	{
		return "\"" + value.Replace("\"", "\"\"") + "\"";
	}
}