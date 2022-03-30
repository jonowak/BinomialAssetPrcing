using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BinomialAssetPricing
{
    class Program
    {
        static void Main(string[] args)
        {
            RunQ5();
            //nQ6();
        }

        public static double GetU(double sigma, double dt)
        {
            double u = Math.Exp(sigma * Math.Sqrt(dt));
            return u;   
        }

        public static double GetD(double u)
        {
            double d = 1/u;
            return d;
        }

        public static double GetP(double r, double dt, double d, double u)
        {
            double a = Math.Exp(r * dt); 
            double p = (a- d)/(u-d);
            return p;
        }

        public static void RunQ6()
        {
            string file = "C:\\Users\\joanna\\Documents\\MyStuff\\School\\ProbStats\\HW3\\Output_Q6.csv";
            TextWriter tw = new StreamWriter(file);
            double r = 0.02; //per annum what do I need it for??? 
            double u = 1.1745;
            double d = 0.84643;
            double t = (1.0 / 12.0);
            int n = 21;
            double dt = t / n;
            double s = 95.28;
            double p = 0.50;
            p = GetP(r, dt, d, u);
            double q = 1 - p;

            Boolean putCall = true;//true when a call

            double[] strikes = new double[] {45, 55, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 140, 150, 165, 180, 190, 200 };

            BinomialTree tree = new BinomialTree(n, s, u, d);
            tree.BuildBt();
            double optionPrice = -1;
            tw.WriteLine("Option strikes calculated with a p=0.5 calibration");
            // calls
            for (int i = 0; i < strikes.Length; i++)
            {

                optionPrice = tree.GetOptionValue(strikes[i], p, q, putCall);
                tw.WriteLine(("CALL," +strikes[i] + "," + optionPrice).ToString());
            }
            // puts
            putCall = false;
            for (int i = 0; i < strikes.Length; i++)
            {

                optionPrice = tree.GetOptionValue(strikes[i], p, q, putCall);
                tw.WriteLine(("PUT," + strikes[i] + "," + optionPrice).ToString());
            }

            //no calibartion------------------------------------------------------------------
            double sigma = 0.571921545;//annaul;
            u = GetU(sigma, dt);
            d = GetD(u);
            p = GetP(r, dt, d, u);
            q = 1 - p;
            putCall = true;
            tw.WriteLine("Option strikes calculated without calibration, with 0.5719 annual vol");
            // calls
            for (int i = 0; i < strikes.Length; i++)
            {

                optionPrice = tree.GetOptionValue(strikes[i], p, q, putCall);
                tw.WriteLine(("CALL," + strikes[i] + "," + optionPrice).ToString());
            }
            // puts
            putCall = false;
            for (int i = 0; i < strikes.Length; i++)
            {

                optionPrice = tree.GetOptionValue(strikes[i], p, q, putCall);
                tw.WriteLine(("PUT," + strikes[i] + "," + optionPrice).ToString());
            }
            //tree.Display();
            tw.Close();
        }


        public static void RunQ5() 
        {
            string file = "C:\\Users\\joanna\\Documents\\MyStuff\\School\\ProbStats\\HW3\\Output_Q5a&b.csv";
            TextWriter tw = new StreamWriter(file);

            double u = 1.04122;
            double d = 0.96116;
            int n = 64;
            double s = 150;
            //double k = 150;
            double p;
            double dt = 0;

            double r = 0.05;
            double t = 0.25;//3 months
            n = 100;
            dt = t / n;
            p = GetP(r, dt, d, u);
            double q = 1 - p;
            Boolean callPut = true; 

            double[] strikes = new double[] { 75, 85, 95, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 180, 190, 200, 210, 220, 230, 240, 250, 270, 300 };

            BinomialTree tree = new BinomialTree(n, s, u, d);
            tree.BuildBt();
            double optionPrice = -1;
            tw.WriteLine("Calibration assuming p = 0.5");
            for (int i = 0; i < strikes.Length; i++)
            {
                optionPrice = tree.GetOptionValue(strikes[i], p, q, callPut);
                tw.WriteLine(("CALL," + strikes[i] + "," + optionPrice).ToString());
            }
            //tree.Display();


            //part b of problem 6 
            u = 1.00253354;
            d = 0.99747286;
            p = GetP(r, dt, d, u);
            p = 0.5;
            q = 1 - p;
            tw.WriteLine("Calibration assuming u = 1/d");
            for (int i = 0; i < strikes.Length; i++)
            {
                optionPrice = tree.GetOptionValue(strikes[i], p, q, callPut);
                tw.WriteLine(("CALL," + strikes[i] + "," + optionPrice).ToString());
            }
            //tree.Display();
            tw.Close();

            //System.Console.WriteLine("Option price is {0}", optionPrice.ToString());
            System.Console.WriteLine();
        
        }


    }
}
