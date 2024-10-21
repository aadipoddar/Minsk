﻿namespace mc.CodeAnalysis.Syntax;

// Lexer - Creates Words
// It produces token which you think as leaves in a tree
internal class Lexer
{
    private readonly string _text;
    private int _position;
    private List<string> _diagnostics = new();

    public Lexer(string text)
    {
        _text = text;
    }

    public IEnumerable<string> Diagnostics => _diagnostics;

    private char Current
    {
        get
        {
            if (_position >= _text.Length)
                return '\0';

            return _text[_position];
        }
    }

    private void Next()
    {
        _position++;
    }


    // Find the next word and return
    public SyntaxToken Lex()
    {
        // <numbers>
        // + - * / ( )
        // <whitespaces>

        if (_position >= _text.Length)
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);

        if (char.IsDigit(Current))
        {
            var start = _position;

            while (char.IsDigit(Current))
                Next();

            // We keep reading numbers
            // At the end we just create the word that represents the number

            var length = _position - start;
            var text = _text.Substring(start, length);

            if (!int.TryParse(text, out var value))
                _diagnostics.Add($"The Number {_text} Isn't a valid Int32");

            return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
        }

        if (char.IsWhiteSpace(Current))
        {
            var start = _position;

            while (char.IsWhiteSpace(Current))
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);

            return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
        }

        switch (Current)
        {
            case '+':
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            case '-':
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            case '*':
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
            case '/':
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            case '(':
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
            case ')':
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
        }

        _diagnostics.Add($"ERROR: Bad Character Input: '{Current}");
        return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
    }
}
