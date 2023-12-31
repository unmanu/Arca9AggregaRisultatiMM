using AggregaRisultati.Models;

namespace AggregaRisultati.Utils;

public class KeyExtractor
{
	private const string KEY_FORMAT = "{0}-{1}-{2}-{3}";

	public static string Extract(DifferenceDto dto)
	{
		return string.Format(KEY_FORMAT, dto.CodiceSocieta, dto.Categoria, dto.NumeroCollettiva, dto.NumeroPolizza);
	}

	public static string Extract(PolizzaInputDto dto)
	{
		return string.Format(KEY_FORMAT, dto.CodiceSocieta, dto.Categoria, dto.NumeroCollettiva, dto.NumeroPolizza);
	}

	public static string Extract(TimesDto dto)
	{
		return string.Format(KEY_FORMAT, dto.CodiceSocieta, dto.Categoria, dto.NumeroCollettiva, dto.NumeroPolizza);
	}
}
