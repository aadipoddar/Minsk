namespace Minsk.CodeAnalysis;

public sealed class EvaluationResult
{
	public EvaluationResult(IEnumerable<Diagnostic> diagnostics, object value)
	{
		Diagnostics = diagnostics.ToArray();
		Value = value;
	}

	public IEnumerable<Diagnostic> Diagnostics { get; }
	public object Value { get; }
}
