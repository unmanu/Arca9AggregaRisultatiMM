public class InputValidationException : Exception
{

	public InputValidationException()
	{
	}

	public InputValidationException(string error)
		: base(error)
	{
	}


	public InputValidationException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
