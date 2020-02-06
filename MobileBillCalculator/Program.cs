using System;
using System.Globalization;

namespace MobileBillCalculator
{
    class Program
    {
        private static string dateFormat = "yyyy-MM-dd hh:mm:ss tt";

        static void Main(string[] args)
        {
            Console.WriteLine("Mobile Bill Calculator");

            DateTime startTime = ReadValidDateTime("Start");
            DateTime endTime = ReadValidDateTime("End");

            var bill = CalculatorBill(startTime, endTime);

            Console.WriteLine("Bill: {0} Taka", bill);
            Console.ReadLine();
        }

        private static DateTime ReadValidDateTime(string dateType)
        {
            DateTime resultDate;
            do
            {
                Console.WriteLine($"Please Enter {dateType} Date in {dateFormat} Format");
                string stringTime = Console.ReadLine();

                bool validDate = DateTime.TryParseExact(
                    stringTime,
                    dateFormat,
                    DateTimeFormatInfo.InvariantInfo,
                    DateTimeStyles.None,
                    out resultDate);
                if (validDate)
                    break;

            } while (true);

            return resultDate;
        }

        private static decimal CalculatorBill(DateTime startTime, DateTime endTime)
        {
            int peakHourStart = 9;
            int peakHourEnd = 11;
            decimal billInPaisa = 0;
            while (startTime <= endTime)
            {
                var pulseEnd = startTime.AddSeconds(20);

                int rate = ((startTime.Hour >= peakHourStart && startTime.Hour <= peakHourEnd) || (pulseEnd.Hour >= peakHourStart && pulseEnd.Hour <= peakHourEnd)) ? 30 : 20;

                billInPaisa = billInPaisa + rate;

                Console.WriteLine($"{startTime:yyyy-MM-dd hh:mm:ss tt}, to {pulseEnd} = {rate} Paisa");

                startTime = pulseEnd;
                startTime = startTime.AddSeconds(1); // To Go to next second to start
            }

            var billInTaka = billInPaisa / 100;

            return billInTaka;
        }

    }
}
