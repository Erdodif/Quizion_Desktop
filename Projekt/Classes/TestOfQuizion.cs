using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Projekt
{
    [TestFixture]
    class TestOfQuizion
    {
        

         [TestCase]
         public void NemMegfelelo()
        {
            ColorsOfQuizion c = new ColorsOfQuizion();
            Assert.IsFalse(c.Black == c.OnSecondary);
        }


    }
}
