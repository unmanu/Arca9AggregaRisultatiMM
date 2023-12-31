using AggregaRisultati.Models;

namespace AggregaRisultati.Writers;

public class CsvWriter : IWriter
{
	public string Write(DirectoryInfo directory, SortedDictionary<string, DifferenceDto> differences, SortedDictionary<string, PolizzaInputDto> polizzeInput, SortedDictionary<string, TimesDto> times)
	{
		string outputFile = Path.Combine(directory.FullName, "Differenze.csv");

		using (var writer = new StreamWriter(outputFile))
		{
			Aggregator headerAggregator = new();
			WriteLine(writer, headerAggregator, true);
			if (polizzeInput.Any())
			{
				AggregateAll(differences, polizzeInput, times, writer);
			}
			else
			{
				AggregateOnlyDifferences(differences, writer);
			}
		}
		return outputFile;
	}

	private static void AggregateAll(SortedDictionary<string, DifferenceDto> differences, SortedDictionary<string, PolizzaInputDto> polizzeInput, SortedDictionary<string, TimesDto> times, StreamWriter writer)
	{
		SortedSet<string> keys = GetKeys(differences, polizzeInput);
		int count = 0;
		foreach (string key in keys)
		{
			count++;
			differences.TryGetValue(key, out DifferenceDto? difference);
			polizzeInput.TryGetValue(key, out PolizzaInputDto? polizza);
			times.TryGetValue(key + "_True", out TimesDto? timesTotale);
			times.TryGetValue(key + "_False", out TimesDto? timesParziale);

			Aggregator aggregator = new(difference, polizza, timesTotale, timesParziale);
			WriteLine(writer, aggregator, false);
		}
	}

	private static SortedSet<string> GetKeys(SortedDictionary<string, DifferenceDto> differences, SortedDictionary<string, PolizzaInputDto> polizzeInput)
	{
		SortedSet<string> keys = new();
		foreach (var item in differences)
		{
			keys.Add(item.Key);
		}
		foreach (var item in polizzeInput)
		{
			keys.Add(item.Key);
		}
		return keys;
	}

	private static void AggregateOnlyDifferences(SortedDictionary<string, DifferenceDto> differences, StreamWriter writer)
	{
		foreach (var difference in differences)
		{
			Aggregator aggregator = new(difference.Value);
			WriteLine(writer, aggregator, false);
		}
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