using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_ProbniTest.Exceptions
{
    public class NumberOfMovesExceededException: Exception
    {
        public NumberOfMovesExceededException()
        {

        }

        public NumberOfMovesExceededException(string message): base(message)
        {

        }
    }
}
