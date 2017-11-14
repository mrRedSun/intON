using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntON_programmingLanguage
{
    class TokenStream
    {
        List<Token> tokenList;

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
                if (value >= tokenList.Count)
                {
                    throw new IndexOutOfRangeException("StringStream currentPos setter out of range error");
                }

                currentPos = value;
            }
        }

        public TokenStream(List<Token> list)
        {
            currentPos = 0;
            tokenList = new List<Token>(list);
        }


        public Token GetToken()
        {
            if (currentPos == tokenList.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return tokenList[currentPos++];
        }

        public Token PeekToken()
        {
            if (currentPos == tokenList.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return tokenList[currentPos];
        }

        /// <summary>
        /// Sets current token pointer to a given position
        /// </summary>
        /// <param name="pos"> Position </param>
        public void Seek(int pos)
        {
            CurrentPos = pos;
        }


        /// <summary>
        /// Returns bool that tells if the stream has ended
        /// </summary>
        public bool Eof()
        {
            if (currentPos >= tokenList.Count)
            {
                return true;
            }
            return false;
        }
    }
}
