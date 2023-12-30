using AggregaRisultati.Models;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest.Parsers;

public class DifferencesFileParserTest
{

	private readonly DifferencesFileParser _parser;

	public DifferencesFileParserTest()
	{
		_parser = new DifferencesFileParser();
	}

	[Fact]
	public void Parse_EmptyFile_ReturnsEmptyArray()
	{
		FileInfo differencesFile = new(Path.Combine("Resources", "EmptyDifferencesFile", InputParser.DifferencesFileName));

		List<DifferenceDto> differences = _parser.Parse(differencesFile);

		differences.Should().NotBeNull().And.BeEmpty();
	}

	[Fact]
	public void Parse_FileWithMultipleDifferences_ReturnsMultipleDifferences()
	{
		FileInfo differencesFile = new(Path.Combine("Resources", "MultilpleDifferences", InputParser.DifferencesFileName));

		List<DifferenceDto> differences = _parser.Parse(differencesFile);

		differences.Should().NotBeNull().And.NotBeEmpty();
		differences.Count.Should().Be(4);
	}

	[Fact]
	public void Parse_FileWithNotEndsWithBlankLines_Returns1DifferenceWithSpecificNumeroPolizza()
	{
		FileInfo differencesFile = new(Path.Combine("Resources", "SingleEndsWithoutBlankLines", InputParser.DifferencesFileName));

		List<DifferenceDto> differences = _parser.Parse(differencesFile);

		differences.Should().NotBeNull().And.NotBeEmpty();
		differences.Count.Should().Be(1);
		differences[0].NumeroPolizza.Should().NotBeNull().And.Be("1337262");
	}

}
