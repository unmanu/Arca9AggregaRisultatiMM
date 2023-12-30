using CsvHelper.Configuration.Attributes;

namespace AggregaRisultatiTest.Writers;

public class CsvReadDto
{
	[Index(0)]
	public string? AgenziaGestione { get; set; }
	[Index(1)]
	public string? CodiceProdotto { get; set; }
	[Index(2)]
	public string? CodiceSocieta { get; set; }
	[Index(3)]
	public string? Categoria { get; set; }
	[Index(4)]
	public string? NumeroCollettiva { get; set; }
	[Index(5)]
	public string? NumeroPolizza { get; set; }
	[Index(6)]
	public string? ErroreCics { get; set; }
	[Index(7)]
	public string? ErroreAlbedino { get; set; }
	[Index(8)]
	public string? ImportoNettoCics { get; set; }
	[Index(9)]
	public string? ImportoLordoCics { get; set; }
	[Index(10)]
	public string? ImposteLordoCics { get; set; }
	[Index(11)]
	public string? ImportoNettoAlbedino { get; set; }
	[Index(12)]
	public string? ImportoLordoAlbedino { get; set; }
	[Index(13)]
	public string? ImposteLordoAlbedino { get; set; }
}
