using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator
{
    class QuarterlyIncomeReport
    {
        static void Main(string[] args)
        {
            // create a new instance of the class
            QuarterlyIncomeReport report = new QuarterlyIncomeReport();

            // call the GenerateSalesData method
            SalesData[] salesData = report.GenerateSalesData();
            
            // call the QuarterlySalesReport method
            report.QuarterlySalesReport(salesData);

        }

        /* public struct SalesData includes the following fields: date sold, department name, product ID, quantity sold, unit price */
        public struct SalesData
        {
            public DateOnly dateSold;
            public string departmentName;
            public string productID;
            public int quantitySold;
            public double unitPrice;
            public double baseCost;
            public int volumeDiscount;
        }

        /* the GenerateSalesData method returns 1000 SalesData records. It assigns random values to each field of the data structure */
        public SalesData[] GenerateSalesData()
        {
            SalesData[] salesData = new SalesData[10000];
            Random random = new Random();
            
            /* 1. departmentName should be randomly selected from a list of 8 departments
               2. for each department name, create a 4-character abbreviation that can be included in productID */
            string[] departments = { "Electronics", "Clothing", "Home Goods", "Toys", "Sporting Goods", "Books", "Food", "Health & Beauty" };
            string[] departmentAbbreviations = { "ELEC", "CLTH", "HOME", "TOYS", "SPRT", "BOOK", "FOOD", "HB" };
            string[] manufacturingSites = { "US1", "US2", "US3", "CA1", "CA2", "CA3", "MX1", "MX2", "MX3", "EU1" };

            for (int i = 0; i < 10000; i++)
            {
                salesData[i].dateSold = new DateOnly(2023, random.Next(1, 13), random.Next(1, 29));
                salesData[i].departmentName = departments[random.Next(departments.Length)];

                /* productID should be formatted using the pattern "DDDD-###-SS-CC-MMM" where the components of the ID are defined as
                 follows:
                    1. a 4-character code representing the department abbreviation
                    2. a 3-digit number representing the product
                    3. a 2-character code representing the size of the product
                    4. a 2-character code representing the color of the product
                    5. a 3-character code representing the manufacturing site 
                
                The manufacturing site is randomly selected from a list of 10 manufacturing sites). The codes should be a 2-letter ISO 
                country code followed by a digit (e.g., US1, CA2, MX3, etc.).
                */

                salesData[i].productID = departmentAbbreviations[random.Next(departmentAbbreviations.Length)] + "-" + 
                                        random.Next(1, 999) + "-" + (char)random.Next('A', 'Z') + (char)random.Next('A', 'Z') + "-" + 
                                        manufacturingSites[random.Next(manufacturingSites.Length)];
                salesData[i].quantitySold = random.Next(1, 101);
                salesData[i].unitPrice = random.Next(25, 300) + random.NextDouble();

                /* baseCost: Add a field for the manufacturing cost of the item. The baseCost values should be generated using randomly 
                generated discount off the unitPrice (5 to 20 percent).*/

                double discount = random.Next(5, 21) / 100.0;
                salesData[i].baseCost = salesData[i].unitPrice * (1 - discount);

                /* volumeDiscount: Add a field for a volume discount percentage. The value assigned to volumeDiscount should be the 
                integer component of 10 percent of the quantitySold (10% of 19 units = 1% volumeDiscount) */

                salesData[i].volumeDiscount = (int)(salesData[i].quantitySold * 0.1);

            }

            return salesData;
        }

        public void QuarterlySalesReport(SalesData[] salesData)
        {
            // create dictionaries to store the quarterly sales and profit data
            Dictionary<string, Dictionary<string, double>> quarterlySales = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, Dictionary<string, double>> quarterlyProfit = new Dictionary<string, Dictionary<string, double>>();

            // iterate through the sales data
            foreach (SalesData data in salesData)
            {
            try
            {
                // calculate the total sales and profit for each quarter
                string quarter = GetQuarter(data.dateSold.Month);
                double totalSales = (data.quantitySold - data.volumeDiscount) * data.unitPrice;
                double totalProfit = totalSales - (data.quantitySold * data.baseCost);

                if (!quarterlySales.ContainsKey(quarter))
                {
                quarterlySales[quarter] = new Dictionary<string, double>();
                quarterlyProfit[quarter] = new Dictionary<string, double>();
                }

                if (quarterlySales[quarter].ContainsKey(data.departmentName))
                {
                quarterlySales[quarter][data.departmentName] += totalSales;
                quarterlyProfit[quarter][data.departmentName] += totalProfit;
                }
                else
                {
                quarterlySales[quarter].Add(data.departmentName, totalSales);
                quarterlyProfit[quarter].Add(data.departmentName, totalProfit);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing sales data for {data.departmentName} on {data.dateSold}: {ex.Message}");
            }
            }

            // display the quarterly sales and profit report
            Console.WriteLine("Quarterly Sales and Profit Report by Department");
            Console.WriteLine("-----------------------------------------------");
            string[] quarters = { "Q1", "Q2", "Q3", "Q4" };
            foreach (var quarter in quarters)
            {
            if (quarterlySales.ContainsKey(quarter))
            {
                Console.WriteLine("{0}:", quarter);
                foreach (var department in quarterlySales[quarter])
                {
                double profitPercentage = quarterlyProfit[quarter][department.Key] / quarterlySales[quarter][department.Key] * 100;
                Console.WriteLine("  {0}: Sales = ${1}, Profit = ${2}, Profit Percentage = {3}%", department.Key, department.Value, quarterlyProfit[quarter][department.Key], profitPercentage);
                }
            }
            }
        }

        

        public string GetQuarter(int month)
        {
            if (month >= 1 && month <= 3)
            {
                return "Q1";
            }
            else if (month >= 4 && month <= 6)
            {
                return "Q2";
            }
            else if (month >= 7 && month <= 9)
            {
                return "Q3";
            }
            else
            {
                return "Q4";
            }
        }
    }
}
