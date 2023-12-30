﻿namespace AggregaRisultati.Writers;

public class WriterFactory
{
	public static IWriter CreateWriter()
	{
		return new CsvWriter();
	}
}
