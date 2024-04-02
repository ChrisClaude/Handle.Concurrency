namespace Handle.Concurrency.Application.Exceptions;

public class ConcurrentConflictException(string message) : Exception(message)
{
}
