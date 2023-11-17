namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken openParanthesisToken, ExpressionSyntax expression, SyntaxToken closeParanthesisToken)
        {
            OpenParanthesisToken = openParanthesisToken;
            Expression = expression;
            CloseParanthesisToken = closeParanthesisToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public SyntaxToken OpenParanthesisToken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParanthesisToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParanthesisToken;
            yield return Expression;
            yield return CloseParanthesisToken;
        }
    }
}
