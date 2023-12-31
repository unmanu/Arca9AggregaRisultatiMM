using AggregaRisultati.Models;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest.Parsers;

public class TimesFileParserTest
{

	private readonly TimesFileParser _parser;

	public TimesFileParserTest()
	{
		_parser = new TimesFileParser();
	}

	[Fact]
	public void Parse_EmptyFile_ReturnsEmptyArray()
	{
		FileInfo file = new(Path.Combine("Resources", "EmptyDifferencesFile", InputParser.TimesFileName));

		List<TimesDto> times = _parser.Parse(file);

		times.Should().NotBeNull().And.BeEmpty();
	}

	[Fact]
	public void Parse_FileWithMultipleDifferences_ReturnsMultipleDifferences()
	{
		FileInfo file = new(Path.Combine("Resources", "MultipleDifferences", InputParser.TimesFileName));

		List<TimesDto> times = _parser.Parse(file);

		times.Should().NotBeNull().And.NotBeEmpty();
		times.Count.Should().Be(3);
	}

	[Fact]
	public void Parse_FileWithNotEndsWithBlankLines_Returns1DifferenceWithSpecificNumeroPolizza()
	{
		FileInfo file = new(Path.Combine("Resources", "SingleEndsWithoutBlankLines", InputParser.TimesFileName));

		List<TimesDto> times = _parser.Parse(file);

		times.Should().NotBeNull().And.NotBeEmpty();
		times.Count.Should().Be(1);
		times[0].NumeroPolizza.Should().NotBeNull().And.Be("1337636");
	}

}
