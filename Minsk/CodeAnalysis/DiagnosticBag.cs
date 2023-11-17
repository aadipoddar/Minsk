using System.Collections;

using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> _diagnostics = new();

        public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        private void Report(TextSpan span, string message)
        {
            var diagnostics = new Diagnostic(span, message);
            _diagnostics.Add(diagnostics);
        }

        public void ReportInvalidNumber(TextSpan span, string text, Type type)
        {
            var message = $"The Number {text} Isn't a valid {type}";
            Report(span, message);
        }

        public void ReportBadCharacter(int position, char charcater)
        {
            var span = new TextSpan(position, 1);
            var message = $"Bad Character Input: '{charcater}";
            Report(span, message);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
        {
            var message = $"Unexpected Token <{actualKind}>, expected <{expectedKind}>";
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
        {
            var message = $"Unary Operator '{operatorText}' is not defined for type {operandType}";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
        {
            var message = $"Binary Operator '{operatorText}' is not defined for type {leftType} and {rightType}";
            Report(span, message);
        }
    }
}
