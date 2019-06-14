using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Carriage
    {
        public bool Light { get; set; }
        public Carriage(bool light)
        {
            Light = light;
        }
    }

    class Train
    {
        Carriage[] Carriages { get; set; }
        private int currentPos;

        public Train(int carriageNumber)
        {
            Random random = new Random();
            Carriages = new Carriage[carriageNumber];
            currentPos = 0;
            Carriage carriage;

            for (int i = 0; i < carriageNumber; i++)
            {
                carriage = new Carriage(random.Next(2) == 1);
                Carriages[i] = carriage;
            }
        }

        public Carriage Current
        {
            get { return Carriages[currentPos]; }
        }

        public Carriage Next
        {
            get
            {
                currentPos++;
                if (currentPos >= Carriages.Count())
                    currentPos = 0;
                return Current;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number of carriages: ");
            int carriageNumber = int.Parse(Console.ReadLine());
            Train train = new Train(carriageNumber);

            train.Current.Light = true; //Включаю свет в первом вагоне.
            Carriage startCarriage = train.Current;
            int steps = 0;

            while (startCarriage.Light != false) //Всегда можно вернуться и посмотреть какой свет в вагоне с которого мы стартовали.
            {
                train.Next.Light = false;
                steps++;
            }

            Console.WriteLine("Number of carriages: " + steps);
            Console.ReadKey();
        }
    }
}