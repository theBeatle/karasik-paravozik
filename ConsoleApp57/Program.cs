using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp57
{
    class Program
    {
        static void Main(string[] args)
        {
            var tunnel = new Tunnel();
            tunnel.Randomize();
            var count = tunnel.SegmentsCount;
            Console.WriteLine($"Room count -{count}");
            Console.WriteLine($"Current position - {tunnel.CurrentPosition}");
            Console.ReadLine();

            Stopwatch sw = new Stopwatch();

            sw.Start();
            int segmentsFound = Strategy(tunnel);
            sw.Stop();

            Console.WriteLine($"Strategy result - {segmentsFound}");
            Console.WriteLine($"Seconds per section {sw.ElapsedMilliseconds / 1000}");
            Console.WriteLine($"Total  {tunnel.SecondsCount} Divided {tunnel.SecondsCount / (segmentsFound) }");
            Console.WriteLine($"C-{tunnel.SetCount} F-{tunnel.ForwardMoves} B-{tunnel.BackwardCount}");
        }
        public static int Strategy(Tunnel tunnel)
        {
            int qty = 2;
            bool chkLight = false;
            for (int i = 0; i <= qty; i++)
            {
                tunnel.MoveForvard();
            }

            bool flag = false;
            while (!flag)
            {
                chkLight = tunnel.RoomStatus;

                for (int i = 0; i <= (qty + 1); i++)
                {
                    tunnel.MoveBackward();
                }
                var turnPointLight = tunnel.RoomStatus;
                if (chkLight == turnPointLight)
                {
                    turnPointLight = !turnPointLight;
                    tunnel.RoomStatus = !tunnel.RoomStatus;

                    for (int i = 0; i <= (qty + 1); i++)
                    {
                        tunnel.MoveForvard();
                    }
                    bool testLight = tunnel.RoomStatus;
                    bool testPointCheck = (turnPointLight == testLight && chkLight != testLight);
                    if (testPointCheck)
                    {
                        flag = true;
                    }
                }
                else
                {
                    qty++;
                    tunnel.MoveBackward();
                    tunnel.IsDefaultDirection = !tunnel.IsDefaultDirection;
                }
            }
            return qty + 2;
        }
    }


    public class Tunnel
    {
        public int SegmentsCount { get; private set; }
       // private List<int> tunnel = new List<int>();
        private bool[] tunnel2; // = new List<int>();
        private readonly Random rnd = new Random();
        private int currentPosition;
        private int zeroPosition;
        public bool IsDefaultDirection { get; set; } //default value False;
        public long SecondsCount { get; private set; }
        public long ForwardMoves { get; private set; }
        public long BackwardCount { get; private set; }
        public long SetCount { get; private set; }

        public int CurrentPosition
        {
            get { return currentPosition; }
            private set { currentPosition = value; }
        }

        public void Randomize()
        {
           //tunnel.Clear();
           //SegmentsCount = rnd.Next(2, 10_000);
           //for (int i = 0; i < SegmentsCount; i++)
           //{
           //    tunnel.Add(rnd.Next(0, 1));
           //}
           //zeroPosition = rnd.Next(0, SegmentsCount - 1);
           //CurrentPosition = zeroPosition;

            
            SegmentsCount = rnd.Next(2, 100_000);
            tunnel2 = new bool[SegmentsCount];
            for (int i = 0; i < SegmentsCount; i++)
            {
                tunnel2[i] = rnd.Next(0, 1) == 0 ? false : true;
            }
            zeroPosition = rnd.Next(0, SegmentsCount - 1);
            CurrentPosition = zeroPosition;


        }

        public bool RoomStatus
        {
            get
            {
                //return tunnel[CurrentPosition];
                return tunnel2[CurrentPosition];
            }
            set
            {
                //tunnel[CurrentPosition] = value;
                tunnel2[CurrentPosition] = value;
                SecondsCount++;
                SetCount++;
            }
        }

        public void MoveForvard()
        {
            ForwardMoves++;
            SecondsCount += 2;
            if (IsDefaultDirection == true)
            {
                if (CurrentPosition == 0)
                {
                    //CurrentPosition = tunnel.Count - 1;
                    CurrentPosition = tunnel2.Length - 1;
                }
                else
                {
                    CurrentPosition--;
                }
                return;
            }
            //if (CurrentPosition == tunnel.Count - 1)
            if (CurrentPosition == tunnel2.Length - 1)
            {
                CurrentPosition = 0;
            }
            else
            {
                CurrentPosition++;
            }
        }

        public void MoveBackward()
        {
            BackwardCount++;
            SecondsCount += 2;
            if (IsDefaultDirection)
            {
                //if (CurrentPosition == tunnel.Count - 1)
                if (CurrentPosition == tunnel2.Length - 1)
                {
                    CurrentPosition = 0;
                }
                else
                {
                    CurrentPosition++;
                }
                return;
            }
            if (CurrentPosition == 0)
            {
                //CurrentPosition = tunnel.Count - 1;
                CurrentPosition = tunnel2.Length - 1;
            }
            else
            {
                CurrentPosition--;
            }
        }
    }
}
