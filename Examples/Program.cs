using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jettsonator.Combinators;
namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Example of using the K-combinator as a logging function
             */
            List<int> randomList = new List<int>();
            
            Random rand = new Random(DateTime.Now.Second);
            
            randomList.Add(rand.Next());
            randomList.Add(rand.Next());
            randomList.Add(rand.Next());

            randomList.K(x => Console.WriteLine(x));

            int singleInt = 100;
            singleInt.K(x => Console.WriteLine("This is single int {0}", x));
            Console.ReadLine();
            
        }
    }
}
