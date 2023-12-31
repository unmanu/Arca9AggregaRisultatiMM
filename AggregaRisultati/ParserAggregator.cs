using AggregaRisultati.Models;
using AggregaRisultati.Parsers;
using AggregaRisultati.Writers;

namespace AggregaRisultati;

public class ParserAggregator()
{
	public static void Aggregate(string[] args)
	{
		Console.WriteLine("start");
		InputParser inputParser = new();
		Input input = inputParser.Parse(args);
		Console.WriteLine("parsed args");

		DifferencesFileParser differencesFileParser = new();
		List<DifferenceDto> differences = differencesFileParser.Parse(input.DifferencesFile);
		Console.WriteLine("parsed differences");

		PolizzeInputFileParser polizzeInputFileParser = new();
		List<PolizzaInputDto> polizzeInput = polizzeInputFileParser.Parse(input.PolizzeInputFile);
		Console.WriteLine("parsed polizze input");

		TimesFileParser timesFileParser = new();
		List<TimesDto> times = timesFileParser.Parse(input.TimesFile);
		Console.WriteLine("parsed times");

		IWriter writer = WriterFactory.CreateWriter();
		string outputFile = writer.Write(input.Directory, differences, polizzeInput, times);
		Console.WriteLine("wrote file: " + outputFile);

		Console.WriteLine("end");

	}
}