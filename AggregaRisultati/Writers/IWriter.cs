using AggregaRisultati.Models;
namespace AggregaRisultati.Writers;

public interface IWriter
{
	public string Write(DirectoryInfo directory, SortedDictionary<string, DifferenceDto> differences, SortedDictionary<string, PolizzaInputDto> polizzeInput, SortedDictionary<string, TimesDto> times);
}
