﻿using AggregaRisultati.Models;
using AggregaRisultati.Parsers;
using AggregaRisultati.Writers;

InputParser inputParser = new();
Input input = inputParser.Parse(args);

DifferencesFileParser differencesFileParser = new();
List<DifferenceDto> differences = differencesFileParser.Parse(input.DifferencesFile);

IWriter excelWriter = WriterFactory.CreateWriter();
string outputFile = excelWriter.Write(input.Directory, differences);

Console.WriteLine("Wrote file: " + outputFile);