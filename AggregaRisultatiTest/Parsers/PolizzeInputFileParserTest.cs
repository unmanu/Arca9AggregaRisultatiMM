using AggregaRisultati.Models;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest.Parsers;

public class PolizzeInputFileParserTest
{

	private readonly PolizzeInputFileParser _parser;

	public PolizzeInputFileParserTest()
	{
		_parser = new PolizzeInputFileParser();
	}

	[Fact]
	public void Parse_NoInputFile_ReturnsEmptyList()
	{
		List<PolizzaInputDto> polizze = _parser.Parse(null);

		polizze.Should().NotBeNull().And.BeEmpty();
	}

	[Fact]
	public void Parse_MultipleLinesWithEmptyLines_ReturnsListWithoutEmptyLines()
	{
		FileInfo file = new(Path.Combine("Resources", "EmptyDifferencesFile", InputParser.InputFileName));

		List<PolizzaInputDto> polizze = _parser.Parse(file);

		polizze.Should().NotBeNull().And.NotBeEmpty();
		polizze.Count.Should().Be(2);
	}

	[Fact]
	public void Parse_SingleLine_ReturnsPolizzaWithSpecificValues()
	{
		FileInfo file = new(Path.Combine("Resources", "MultipleDifferences", InputParser.InputFileName));

		List<PolizzaInputDto> polizze = _parser.Parse(file);

		polizze.Should().NotBeNull().And.NotBeEmpty();
		polizze.Count.Should().Be(1);
		PolizzaInputDto firstPolizza = polizze[0];
		firstPolizza.AgenziaGestione.Should().Be("073");
		firstPolizza.CodiceProdotto.Should().Be("961");
		firstPolizza.CodiceSocieta.Should().Be("341");
		firstPolizza.Categoria.Should().Be("11");
		firstPolizza.NumeroCollettiva.Should().Be("0");
		firstPolizza.NumeroPolizza.Should().Be("1337523");
		firstPolizza.DataDecorrenza.Should().Be("2022-12-19");
		firstPolizza.TipiRiscatto.Should().Be("NO0102");
		firstPolizza.Convenzione.Should().Be("073961");
	}
}
