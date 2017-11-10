using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class StringStream
    {
        private string source;

        private int currentPos;
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

        public char this[int index]
        {
            get
            {
                return source[index];
            }
        }

        public StringStream(string s)
        {
            source = s;
            CurrentPos = 0;
        }

        public char Get()
        {
            if (currentPos == source.Length)
            {
                throw new IndexOutOfRangeException("You've reached end of stream (get)");
            }
            return source[currentPos++];
        }

        public char Peek()
        {
            if (currentPos >= source.Length)
            {
                throw new IndexOutOfRangeException("You've reached end of stream (peek)");
            }
            return source[currentPos];
        }

        public void Shift(int distance)
        {
            currentPos += distance;
        }

        public void Seek(int pos)
        {
            if (source.Length <= pos)
            {
                throw new IndexOutOfRangeException("Seeking out of range");
            }
            CurrentPos = pos;
        }

        public void Putback()
        {
            if (currentPos == 0)
            {
                throw new IndexOutOfRangeException("Can't putback when current pos = 0");
            }
            currentPos--;
        }

        public bool IsCurrentDigit()
        {
            if (Peek() >= '0' && Peek() <= '9')
            {
                return true;
            }
            return false;
        }

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

        public int GetCharsLeft()
        {
            return source.Length - CurrentPos;
        }



        public string PeekSlice(int start, int end)
        {
            return source.Substring(start, end);
        }

        public string PeekSlice(int end)
        {
            return source.Substring(CurrentPos, end);
        }

        public string GetSlice(int end)
        {
            string temp = source.Substring(currentPos, end);
            CurrentPos += end;
            return temp;
        }

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
