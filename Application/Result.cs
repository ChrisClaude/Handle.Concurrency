namespace Handle.Concurrency.Application;

public class Result(bool isSuccess, string errorMessage)
{
	public bool IsSuccess { get; } = isSuccess;

	public string ErrorMessage { get; } = errorMessage;
}
