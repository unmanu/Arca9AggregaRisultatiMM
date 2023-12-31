using AggregaRisultati.Models;
using AggregaRisultati.Parsers;
using AggregaRisultati.Writers;
using System.Collections.Generic;

InputParser inputParser = new();
Input input = inputParser.Parse(args);

DifferencesFileParser differencesFileParser = new();
List<DifferenceDto> differences = differencesFileParser.Parse(input.DifferencesFile);

PolizzeInputFileParser polizzeInputFileParser = new();
List<PolizzaInputDto> polizzeInput = polizzeInputFileParser.Parse(input.PolizzeInputFile);

TimesFileParser timesFileParser = new();
List<TimesDto> times = timesFileParser.Parse(input.TimesFile);

IWriter excelWriter = WriterFactory.CreateWriter();
string outputFile = excelWriter.Write(input.Directory, differences, polizzeInput, times);

Console.WriteLine("Wrote file: " + outputFile);