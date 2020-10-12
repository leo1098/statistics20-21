using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_A_CSharp
{
    
    class Program
    {
        static void Cube(int n1, int n2)
        {            
            n1 = n1 * n1 * n1;
            n2 = n2 * n2 * n2;

            Console.WriteLine("a and b inside Cube() function");
            Console.WriteLine(n1 + " " + n2);

        }

        static void Cube2(MyInt n1, MyInt n2)
        {
            n1.value = n1.value * n1.value * n1.value;
            n2.value = n2.value * n2.value * n2.value;

            Console.WriteLine("MyInt a and b inside Cube() function");
            Console.WriteLine(n1.value + " " + n2.value);
        }

        static void Main(string[] args)
        {
            //int num1 = 5;
            //int num2 = 10;

            //Console.WriteLine("num1 and num2 in Main() function");
            //Console.WriteLine(num1 + " " + num2);

            //Cube(num1, num2);

            //Console.WriteLine("num1 and num2 after Cube() was called");
            //Console.WriteLine(num1 + " " + num2);


            MyInt Num1 = new MyInt();
            MyInt Num2 = new MyInt();

            // ---------------------------------------

            Num1.value = 5;
            Num2.value = 10;

            Console.WriteLine("MyInt Num1 and num2 in Main() function");
            Console.WriteLine(Num1.value+ " " + Num2.value);

            Cube2(Num1, Num2);

            Console.WriteLine("MyInt Num1 and Num2 after Cube() was called");
            Console.WriteLine(Num1.value + " " + Num2.value);

            Console.WriteLine("Press a key to Exit..");
            Console.ReadLine();
        }
    }
}
