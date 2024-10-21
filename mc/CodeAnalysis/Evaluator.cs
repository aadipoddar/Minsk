﻿using Minsk.CodeAnalysis.Binding;

namespace Minsk.CodeAnalysis;

internal sealed class Evaluator
{
	private readonly BoundExpression _root;

	public Evaluator(BoundExpression root)
	{
		_root = root;
	}

	public int Evaluate()
	{
		return EvaluateExpression(_root);
	}

	private int EvaluateExpression(BoundExpression node)
	{
		if (node is BoundLiteralExpression n)
			return (int)n.Value;

		if (node is BoundUnaryExpression u)
		{
			var operand = EvaluateExpression(u.Operand);

			switch (u.OperatorKind)
			{
				case BoundUnaryOperatorKind.Identity:
					return operand;
				case BoundUnaryOperatorKind.Negation:
					return -operand;
				default:
					throw new Exception($"Unexpected Unary Operator {u.OperatorKind}");
			}
		}

		if (node is BoundBinaryExpression b)
		{
			var left = EvaluateExpression(b.Left);
			var right = EvaluateExpression(b.Right);

			switch (b.OperatorKind)
			{
				case BoundBinaryOperatorKind.Addition:
					return left + right;
				case BoundBinaryOperatorKind.Substraction:
					return left - right;
				case BoundBinaryOperatorKind.Multiplication:
					return left * right;
				case BoundBinaryOperatorKind.Division:
					return left / right;
				default:
					throw new Exception($"Unexpected Binary Operator {b.OperatorKind}");
			}
		}

		throw new Exception($"Unexpected Node {node.Kind}");
	}
}
