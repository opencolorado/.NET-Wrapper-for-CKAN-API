﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Globalization;

namespace CkanDotNet.Api
{
    internal class JsonParser
    {
        private static readonly char[] whitespace = new char[] { ' ', '\t', '\n', '\r' };
        private const char BeginObject = '{';
        private const char EndObject = '}';
        private const char BeginArray = '[';
        private const char EndArray = ']';
        private const char NameSeparator = ':';
        private const char ValueSeparator = ',';
        private const char BeginString = '"';
        private const char EndString = '"';

        private char[] input;
        private int currentIndex;

        private Stack<dynamic> stack = new Stack<dynamic>();

        private char CurrentChar
        {
            get
            {
                if (this.currentIndex == this.input.Length)
                {
                    throw new FormatException("Unexpected end of input.");
                }

                return this.input[this.currentIndex];
            }
        }

        // Parse    ->  Object | Array
        /// <summary>
        /// Parse input as JSON. JSON array is converted to IList&lt;&gt;, JSON number to double.
        /// </summary>
        /// <remarks>
        /// Parser accepts trainling zeros in numbers.
        /// </remarks>
        /// <param name="input">Valid JSON input.</param>
        /// <returns>ExpandObject filled with JSON data.</returns>
        public dynamic Parse(string input)
        {
            this.input = input.ToCharArray();
            this.TrimWhiteSpace();
            switch (this.CurrentChar)
            {
                case BeginObject:
                    this.ParseObject();
                    break;
                case BeginArray:
                    this.ParseArray();
                    break;
                default:
                    throw new FormatException("Array or object expected.");
            }

            if (this.stack.Count != 1 || this.currentIndex != this.input.Length)
            {
                throw new FormatException("Malformed input.");
            }

            return this.stack.Pop();
        }

        // Array   ->  [ ] | [ Elements ]
        private void ParseArray()
        {
            this.LoadChar(BeginArray);
            this.stack.Push(new List<dynamic>());
            if (this.CurrentChar != EndArray)
            {
                this.ParseElements();
            }

            this.LoadChar(EndArray);
        }

        // Elements    ->  Value | Value, Elements
        private void ParseElements()
        {
            this.ParseValue();
            object value = this.stack.Pop();
            this.stack.Peek().Add(value);
            if (this.CurrentChar == ValueSeparator)
            {
                this.LoadChar(ValueSeparator);
                this.ParseElements();
            }
        }

        // Object  ->  {} | { Members }
        private void ParseObject()
        {
            this.LoadChar(BeginObject);
            this.stack.Push(new ExpandoObject());
            if (this.CurrentChar != EndObject)
            {
                this.ParseMembers();
            }

            this.LoadChar(EndObject);
        }

        // Members -> Pair | Pair , Members
        private void ParseMembers()
        {
            this.ParsePair();
            this.TrimWhiteSpace();
            if (this.CurrentChar == ValueSeparator)
            {
                this.LoadChar(ValueSeparator);
                this.ParseMembers();
            }
        }

        // Pair    ->  String : Value
        private void ParsePair()
        {
            this.ParseString();
            this.LoadChar(NameSeparator);
            this.ParseValue();
            object value = this.stack.Pop();
            string name = this.stack.Pop();
            ((IDictionary<string, object>)this.stack.Peek()).Add(name, value);
        }

        // String  ->  "" | " Chars "
        private void ParseString()
        {
            this.LoadChar(BeginString);
            this.stack.Push(new StringBuilder());
            if (this.CurrentChar != EndString)
            {
                this.ParseChars();
            }

            this.LoadChar(EndString);
            StringBuilder result = this.stack.Pop();
            this.stack.Push(result.ToString());
        }

        // Chars   ->  Char | Char Chars
        private void ParseChars()
        {
            char c;
            bool valid = this.TryParseChar(out c);
            if (!valid)
            {
                throw new FormatException("Valid character expected at position " + this.currentIndex + ".");
            }

            StringBuilder result = this.stack.Peek();
            do
            {
                result.Append(c);
            }
            while (this.TryParseChar(out c));
        }

        //// Char =  allowed characters
        private bool TryParseChar(out char c)
        {
            if (this.CurrentChar != '"' && this.CurrentChar != '\\' && !char.IsControl(this.CurrentChar))
            {
                c = this.CurrentChar;
                this.NextChar();
                return true;
            }
            else if (this.CurrentChar == '\\')
            {
                this.NextChar();
                switch (this.CurrentChar)
                {
                    case '"':
                        c = '"';
                        this.NextChar();
                        return true;
                    case '\\':
                        c = '\\';
                        this.NextChar();
                        return true;
                    case '/':
                        c = '/';
                        this.NextChar();
                        return true;
                    case 'b':
                        c = '\b';
                        this.NextChar();
                        return true;
                    case 'f':
                        c = '\f';
                        this.NextChar();
                        return true;
                    case 'n':
                        c = '\n';
                        this.NextChar();
                        return true;
                    case 'r':
                        c = '\r';
                        this.NextChar();
                        return true;
                    case 't':
                        c = '\t';
                        this.NextChar();
                        return true;
                    default:
                        break;
                }

                if (this.CurrentChar == 'u')
                {
                    // TODO UTF16??
                    char[] code = new char[4];
                    for (int i = 0; i < 4; i++)
                    {
                        this.NextChar();
                        code[i] = this.CurrentChar;
                    }

                    string charStringHex = new string(code);

                    int charNumber = int.Parse(charStringHex, System.Globalization.NumberStyles.HexNumber);

                    c = Convert.ToChar(charNumber);

                    //c = char.Parse("\u2019");
                    this.NextChar();
                    return true;
                }
            }

            c = '\0';
            return false;
        }

        public byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;

        }
        // Value   ->  String | Number | Object | Array | true | false | null
        private void ParseValue()
        {
            switch (this.CurrentChar)
            {
                case BeginString:
                    this.ParseString();
                    return;
                case BeginObject:
                    this.ParseObject();
                    return;
                case BeginArray:
                    this.ParseArray();
                    return;
            }

            if (char.IsDigit(this.CurrentChar) || this.CurrentChar == '-')
            {
                this.ParseNumber();
                return;
            }
            else if (this.CurrentChar == 't')
            {
                this.LoadWord("true");
                this.stack.Push(true);
            }
            else if (this.CurrentChar == 'f')
            {
                this.LoadWord("false");
                this.stack.Push(false);
            }
            else if (this.CurrentChar == 'n')
            {
                this.LoadWord("null");
                this.stack.Push(null);
            }
        }

        // Number  ->  Int | Int Frac | Int Exp | Int Frac Exp
        private void ParseNumber()
        {
            this.stack.Push(new StringBuilder());
            this.ParseInt();
            if (this.CurrentChar == '.')
            {
                this.ParseFrac();
            }

            if (this.CurrentChar == 'e' || this.CurrentChar == 'E')
            {
                this.ParseExp();
            }

            StringBuilder result = this.stack.Pop();
            this.stack.Push(double.Parse(result.ToString(), CultureInfo.InvariantCulture));
        }

        // Int     ->  Digit | - Digit | Digit Digits | - Digit Digits
        private void ParseInt()
        {
            StringBuilder result = this.stack.Peek();
            if (this.CurrentChar == '-')
            {
                result.Append('-');
                this.NextChar();
            }

            this.ParseDigits();
        }

        // Digits  ->  Digit | Digit Digits
        private void ParseDigits()
        {
            if (!char.IsDigit(this.CurrentChar))
            {
                throw new FormatException("Digit expected at position " + this.currentIndex + ", found '" + this.CurrentChar + "'.");
            }

            StringBuilder result = this.stack.Peek();
            do
            {
                result.Append(this.CurrentChar);
                this.NextChar();
            }
            while (char.IsDigit(this.CurrentChar));
        }

        // Frac    ->  . Digits
        private void ParseFrac()
        {
            if (this.CurrentChar != '.')
            {
                throw new FormatException("Period expected at position " + this.currentIndex + ", found '" + this.CurrentChar + "'.");
            }

            this.NextChar();
            StringBuilder result = this.stack.Peek();
            result.Append('.');
            this.ParseDigits();
        }

        // Exp     ->  Exp Digits | Exp - Digits | Exp + Digits
        private void ParseExp()
        {
            if (this.CurrentChar != 'e' && this.CurrentChar != 'E')
            {
                throw new FormatException("Exponent symbol expected at position " + this.currentIndex + ", found '" + this.CurrentChar + "'.");
            }

            StringBuilder result = this.stack.Peek();
            result.Append('e');
            this.NextChar();
            if (this.CurrentChar == '-')
            {
                result.Append('-');
                this.NextChar();
            }
            else if (this.CurrentChar == '+')
            {
                result.Append('+');
                this.NextChar();
            }

            this.ParseDigits();
        }

        /// <summary>
        /// Advance to next character.
        /// </summary>
        private void NextChar()
        {
            this.currentIndex++;
        }

        /// <summary>
        /// Advance to next char and trim whitespace.
        /// </summary>
        /// <param name="c">Hopefully current character.</param>
        private void LoadChar(char c)
        {
            if (this.CurrentChar != c)
            {
                throw new FormatException("Unexpected character at position " + this.currentIndex + ", expected: '" + c + "', found: '" + this.CurrentChar + "'.");
            }

            this.NextChar();
            this.TrimWhiteSpace();
        }

        /// <summary>
        /// Load characters from given word and trim whitespaces afterwards.
        /// </summary>
        /// <param name="word">String of characters to load.</param>
        private void LoadWord(string word)
        {
            foreach (char c in word.ToCharArray())
            {
                if (this.CurrentChar != c)
                {
                    throw new FormatException("Unexpected character at position " + this.currentIndex + ", expected: '" + c + "', found: '" + this.CurrentChar + "'.");
                }

                this.NextChar();
            }

            this.TrimWhiteSpace();
        }

        /// <summary>
        /// Trim leading whitespace.
        /// </summary>
        private void TrimWhiteSpace()
        {
            while (this.currentIndex != this.input.Length && Array.IndexOf(whitespace, this.CurrentChar) != -1)
            {
                this.NextChar();
            }
        }
    }
}
