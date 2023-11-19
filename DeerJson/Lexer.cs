using System;
using DeerJson.Util;

namespace DeerJson
{
    public class Lexer
    {
        private string m_inputStr = "";
        private char?  m_nowChar;
        private int    m_nowIndex = -1;

        public int CurLine { get; private set; } = 1;


        public Lexer(string inputStr)
        {
            m_inputStr = inputStr;
            MoveNext();
        }

        public void SetInputStr(string inputStr)
        {
            m_nowIndex = -1;
            m_inputStr = inputStr;
            m_nowChar = null;
            MoveNext();
        }

        public Token GetNextToken()
        {
            if (m_nowChar == null) return new Token(TokenType.EOF);
            SkipWhitespace();

            if (TypeUtil.IsNumber(m_nowChar) || m_nowChar == '-') return ScanNumber();
            switch (m_nowChar)
            {
                case 't': return ScanKeyword("true", TokenType.TRUE);
                case 'f': return ScanKeyword("false", TokenType.FALSE);
                case 'n': return ScanKeyword("null", TokenType.NULL);
                case '"':
                    return ScanString();
                case '[':
                    MoveNext();
                    return new Token(TokenType.LBRACKET);
                case ']':
                    MoveNext();
                    return new Token(TokenType.RBRACKET);
                case '{':
                    MoveNext();
                    return new Token(TokenType.LBRACE);
                case '}':
                    MoveNext();
                    return new Token(TokenType.RBRACE);
                case ':':
                    MoveNext();
                    return new Token(TokenType.COLON);
                case ',':
                    MoveNext();
                    return new Token(TokenType.COMMA);
            }

            throw new Exception($"lexer: unresolved symbol '{m_nowChar}'");
        }
        
        private Token ScanNumber()
        {
            var start = m_nowIndex;
            while (TypeUtil.IsNumber(m_nowChar)) MoveNext();
            if (m_nowChar == '.')
            {
                MoveNext();
                while (TypeUtil.IsNumber(m_nowChar)) MoveNext();
            }

            if (m_nowChar == 'e' || m_nowChar == 'E')
            {
                MoveNext();
                if (m_nowChar == '+' || m_nowChar == '-')
                {
                    MoveNext();
                    while (TypeUtil.IsNumber(m_nowChar)) MoveNext();
                }
                else if (TypeUtil.IsNumber(m_nowChar))
                {
                    while (TypeUtil.IsNumber(m_nowChar)) MoveNext();
                }
                else
                {
                    throw new Exception($"lexer: missing exponent after 'e', find {m_nowChar}");
                }
            }

            var end = m_nowIndex;
            var num = m_inputStr.Substring(start, end - start);
            return new Token(TokenType.NUMBER, num);
        }

        private Token ScanKeyword(string keyword, TokenType tokenType)
        {
            var n = keyword.Length;
            if (m_nowIndex + n > m_inputStr.Length)
                throw new Exception(
                    $"lexer: unresolved symbol '{m_inputStr.Substring(m_nowIndex)}', are you mean '{keyword}'");
            var startStr = m_inputStr.Substring(m_nowIndex, n);
            if (startStr == keyword)
            {
                MoveNext(n);
                return new Token(tokenType, keyword);
            }

            throw new Exception($"lexer: unresolved symbol '{startStr}', are you mean '{keyword}'");
        }

        private Token ScanString()
        {
            if (m_nowChar != '"') throw new Exception("string must begin with '\"' ");
            MoveNext();
            var start = m_nowIndex;

            void Scan()
            {
                while (m_nowChar != null && m_nowChar != '"') MoveNext();
                if (m_nowChar == null)
                    throw new Exception($"string '{m_inputStr.Substring(start)}' must end with '\"' ");
                // ignore ‘\"’
                if (m_inputStr[m_nowIndex - 1] == '\\')
                {
                    var slashCount = 1;
                    while (m_nowIndex - slashCount - 1 > 0 && m_inputStr[m_nowIndex - slashCount - 1] == '\\')
                        slashCount++;
                    if (slashCount % 2 == 1)
                    {
                        MoveNext();
                        Scan();
                    }
                }
            }

            Scan();
            // remove end '"'
            MoveNext();
            var end = m_nowIndex;
            var str = m_inputStr.Substring(start, end - start - 1);
            return new Token(TokenType.STRING, str);
        }

        private void MoveNext(int step = 1)
        {
            m_nowIndex += step;
            if (m_nowIndex >= m_inputStr.Length)
            {
                m_nowChar = null;
                return;
            }

            m_nowChar = m_inputStr[m_nowIndex];
        }

        private void SkipWhitespace()
        {
            while (m_nowChar == '\t' || m_nowChar == '\r' || m_nowChar == '\n' || m_nowChar == ' ')
            {
                if (m_nowChar == '\n') CurLine++;

                MoveNext();
            }

            ;
        }
    }
}