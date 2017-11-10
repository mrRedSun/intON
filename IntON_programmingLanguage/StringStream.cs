using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    /// <summary>
    /// Basic sting stream for Lexer
    /// </summary>
    class StringStream
    {
        private string source;

        private int currentPos;
        /// <summary>
        /// Parameter with out of range check for setter
        /// </summary>
        private int CurrentPos 
        {
            get
            {
                return currentPos;
            }
            set
            {
                if (value >= source.Length)
                {
                    throw new IndexOutOfRangeException("StringStream currentPos setter out of range error");
                }

                currentPos = value;
            }
        }

        /// <summary>
        /// Basic indexer for sting
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char this[int index]
        {
            get
            {
                if(index >= source.Length || index < 0)
                {
                    throw new IndexOutOfRangeException("Indexer out of range");
                }
                return source[index];
            }
        }

        /// <summary>
        /// Creates basic string stream for given string
        /// </summary>
        /// <param name="s"></param>
        public StringStream(string s)
        {
            source = s;
            CurrentPos = 0;
        }


        /// <summary>
        /// Returns current character from the stream, shifts stream index to the right
        /// </summary>
        /// <returns> Current character in the stream </returns>
        public char Get()
        {
            if (currentPos == source.Length)
            {
                throw new IndexOutOfRangeException("You've reached end of stream (get)");
            }
            return source[currentPos++];
        }

        /// <summary>
        /// Returns current character in the stream
        /// </summary>
        /// <returns>Current character</returns>
        public char Peek()
        {
            if (currentPos >= source.Length)
            {
                throw new IndexOutOfRangeException("You've reached end of stream (peek)");
            }
            return source[currentPos];
        }

        /// <summary>
        /// Shifts stream indexer to the right for given distance
        /// </summary>
        public void Shift(int distance)
        {
            currentPos += distance;
        }

        /// <summary>
        /// Sets stream indexer to the given position
        /// </summary>
        /// <param name="pos"> Position to shift to </param>
        public void Seek(int pos)
        {
            if (source.Length <= pos)
            {
                throw new IndexOutOfRangeException("Seeking out of range");
            }
            CurrentPos = pos;
        }

        /// <summary>
        /// Shifts stream one character to the left
        /// </summary>
        public void Putback()
        {
            if (currentPos == 0)
            {
                throw new IndexOutOfRangeException("Can't putback when current pos = 0");
            }
            currentPos--;
        }

       
        private bool IsCurrentDigit()
        {
            if (Peek() >= '0' && Peek() <= '9')
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Parses and returns floating-point number from the stream
        /// </summary>
        /// <returns> Double number </returns>
        public double GetNumber()
        {
            string numberStr = "";

            while (!Eof() && IsCurrentDigit())
            {
                numberStr += Get();
            }

            if (this.Peek() == ',' || this.Peek() == '.')
            {
                Get();

                if (IsCurrentDigit())
                {
                    numberStr += '.';
                    while (!Eof() && IsCurrentDigit())
                    {
                        numberStr += Get();
                    }
                }
            }
            Console.WriteLine(numberStr);

            return double.Parse(numberStr);
        }

        /// <summary>
        /// Returns number of character before the end of the stream
        /// </summary>
        public int GetCharsLeft()
        {
            return source.Length - CurrentPos;
        }


        /// <summary>
        /// Gets string of given length from the stream
        /// </summary>
        /// <param name="len"> Length of a string user wants to get</param>
        public string PeekSlice(int len)
        {
            return source.Substring(CurrentPos, len);
        }

        /// <summary>
        /// Gets string of given length from the stream, and shifts the stream
        /// </summary>
        /// <param name="len"> Length of a string user wants to get</param>
        public string GetSlice(int len)
        {
            string temp = source.Substring(currentPos, len);
            Shift(len);
            return temp;
        }

        /// <summary>
        /// Returns bool that tells if the stream has ended
        /// </summary>
        public bool Eof()
        {
            if (currentPos >= source.Length)
            {
                return true;
            }
            return false;
        }
    }
}
