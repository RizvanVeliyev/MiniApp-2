using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp_2
{
    internal class StudentNotFountException:Exception
    {
        public StudentNotFountException(string message) : base(message)
        {

        }
    }
}
