namespace BibliotecaApi.Model
{
	public class Result<T>
	{
		public bool IsSuccess { get; }
		public List<string?> Errors { get; }
		public string? Error { get; }
		public T? Data { get; }

		public Result(T data)
		{
			IsSuccess = true;
			Data = data;
		}

		public Result(List<string> error)
		{
			IsSuccess = false;
			Errors = error;
		}

		public Result(string error)
		{
			IsSuccess = false;
			Error = error;
		}

		public static Result<T> Success(T data) => new(data);

		public static Result<T> Failure(List<string> errors) => new(errors);
		public static Result<T> Failure(string error) => new(error);
	}
}
