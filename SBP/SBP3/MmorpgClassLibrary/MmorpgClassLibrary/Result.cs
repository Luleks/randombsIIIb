namespace MmorpgClassLibrary;

public readonly struct Result<TValue, TError> {
    public bool IsError { get; }
    
    public TValue Data => _value ?? default!;

    public TError Error => _error ?? default!;
    
    public int StatusCode { get; }
    
    private readonly TValue? _value;
    private readonly TError? _error;

    public Result(TValue value, int statusCode = 200) {
        IsError = false;
        _value = value;
        _error = default;
        StatusCode = 200;
    }

    public Result(TError error, int statusCode = 400) {
        IsError = true;
        _error = error;
        _value = default;
        StatusCode = statusCode;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public void Deconstruct(out bool isError, out TValue? value, out TError? error, out int statusCode) {
        isError = IsError;
        value = _value;
        error = _error;
        statusCode = StatusCode;
    }
}