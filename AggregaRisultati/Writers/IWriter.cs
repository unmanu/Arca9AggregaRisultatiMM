using AggregaRisultati.Models;
namespace AggregaRisultati.Writers;

public interface IWriter
{
	public string Write(DirectoryInfo directory, List<DifferenceDto> differences, List<PolizzaInputDto> polizzeInput, List<TimesDto> times);
}
