using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_ProbniTest.Exceptions
{
    public class InvalidPositionException: Exception
    {
        public InvalidPositionException(): base("You cannot make any more moves!")
        {
            
        }

        public InvalidPositionException(string message): base(message)
        {

        }
    }
}
