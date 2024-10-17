namespace Minsk.CodeAnalysis;

public sealed class Evaluator
{
	private readonly ExpressionSyntax _root;

	public Evaluator(ExpressionSyntax root)
	{
		_root = root;
	}

	public int Evaluate()
	{
		return EvaluateExpression(_root);
	}

	private int EvaluateExpression(ExpressionSyntax node)
	{
		// Binary Expression
		// Number Expression

		if (node is LiteralExpressionSyntax n)
			return (int)n.LiteralToken.Value;

		if (node is UnaryExpressionSyntax u)
		{
			var operand = EvaluateExpression(u.Operand);

			if (u.OperatorToken.Kind == SyntaxKind.PlusToken)
				return operand;
			else if (u.OperatorToken.Kind == SyntaxKind.MinusToken)
				return -operand;
			else
				throw new Exception($"Unexpected Unary Operator {u.OperatorToken.Kind}");
		}

		if (node is BinaryExpressionSyntax b)
		{
			var left = EvaluateExpression(b.Left);
			var right = EvaluateExpression(b.Right);

			if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
				return left + right;
			else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
				return left - right;
			else if (b.OperatorToken.Kind == SyntaxKind.StarToken)
				return left * right;
			else if (b.OperatorToken.Kind == SyntaxKind.SlashToken)
				return left / right;

			else
				throw new Exception($"Unexpected Binary Operator {b.OperatorToken.Kind}");
		}

		if (node is ParanthesizedExpressionSyntax p)
			return EvaluateExpression(p.Expression);

		throw new Exception($"Unexpected Node {node.Kind}");
	}
}
