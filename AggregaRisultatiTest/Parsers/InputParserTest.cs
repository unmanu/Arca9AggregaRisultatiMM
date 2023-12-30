using AggregaRisultati.Models;
using AggregaRisultati.Parsers;

namespace AggregaRisultatiTest.Parsers;

public class InputParserTest
{

	private readonly InputParser _parser;

	public InputParserTest()
	{
		_parser = new InputParser();
	}

	[Fact]
	public void Parse_InputDirectoryAndDifferencesFileExists_ReturnsDifferencesFile()
	{
		string directoryPath = Path.Combine("Resources", "EmptyDifferencesFile");

		Input input = _parser.Parse([directoryPath]);

		input.Should().NotBeNull();
		input.Directory.ToString().Should().Be(directoryPath);
		input.DifferencesFile.Name.Should().Be(InputParser.DifferencesFileName);
	}

	[Fact]
	public void Parse_InputDirectoryNotExists_ThrowsException()
	{
		string directoryPath = "testFolderThatNotExists";

		Action act = () => _parser.Parse([directoryPath]);

		act.Should().Throw<InputValidationException>()
			.WithMessage(string.Format(InputParser.FolderNotFound, directoryPath));
	}

	[Fact]
	public void Parse_InputDifferencesFileNotExists_ThrowsException()
	{
		string directoryPath = "/";

		Action act = () => _parser.Parse([directoryPath]);

		act.Should().Throw<InputValidationException>()
			.WithMessage(string.Format(InputParser.DifferencesFileNotFound, directoryPath));
	}

	[Fact]
	public void Parse_NoInputDefined_ThrowsExceptionWithCurrentDirReference()
	{
		string currentDirectory = Directory.GetCurrentDirectory();

		Action act = () => _parser.Parse([]);

		act.Should().Throw<InputValidationException>()
			.WithMessage(string.Format(InputParser.DifferencesFileNotFound, currentDirectory));
	}
}
