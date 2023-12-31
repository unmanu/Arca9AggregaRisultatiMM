using CsvHelper.Configuration.Attributes;

namespace AggregaRisultatiTest.Writers;

public class CsvReadDto
{
	[Index(0)]
	public string? Riga { get; set; }
	[Index(1)]
	public string? Input { get; set; }
	[Index(2)]
	public string? AgenziaGestione { get; set; }
	[Index(3)]
	public string? CodiceProdotto { get; set; }
	[Index(4)]
	public string? CodiceSocieta { get; set; }
	[Index(5)]
	public string? Categoria { get; set; }
	[Index(6)]
	public string? NumeroCollettiva { get; set; }
	[Index(7)]
	public string? NumeroPolizza { get; set; }
	[Index(8)]
	public string? DataDecorrenza { get; set; }
	[Index(9)]
	public string? TipiRiscatto { get; set; }
	[Index(10)]
	public string? Convenzione { get; set; }
	[Index(11)]
	public string? Errore { get; set; }
	[Index(12)]
	public string? TipoRiscatto { get; set; }
	[Index(13)]
	public string? ImportoNettoCics { get; set; }
	[Index(14)]
	public string? ImportoLordoCics { get; set; }
	[Index(15)]
	public string? ImposteLordoCics { get; set; }
	[Index(16)]
	public string? ImportoNettoAlbedino { get; set; }
	[Index(17)]
	public string? ImportoLordoAlbedino { get; set; }
	[Index(18)]
	public string? ImposteLordoAlbedino { get; set; }
	[Index(19)]
	public string? ErroreCics { get; set; }
	[Index(20)]
	public string? ErroreAlbedino { get; set; }
	[Index(21)]
	public string? ImportoRParziale { get; set; }
	[Index(22)]
	public string? InizioRTotale { get; set; }
	[Index(23)]
	public string? FineRTotale { get; set; }
	[Index(24)]
	public string? TempoImpiegatoRTotale { get; set; }
	[Index(25)]
	public string? MillisecondiRTotale { get; set; }
	[Index(26)]
	public string? InizioRParziale { get; set; }
	[Index(27)]
	public string? FineRParziale { get; set; }
	[Index(28)]
	public string? TempoImpiegatoRParziale { get; set; }
	[Index(29)]
	public string? MillisecondiRParziale { get; set; }
}
