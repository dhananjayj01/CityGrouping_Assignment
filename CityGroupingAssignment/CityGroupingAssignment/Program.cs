using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var data = new[]
        {
            new { Country = "India", City = "Delhi" },
            new { Country = "India", City = "Bangalore" },
            new { Country = "India", City = "Chennai" },
            new { Country = "India", City = "Trivandrum" },
            new { Country = "India", City = "Goa" },
            new { Country = "USA", City = "New York" },
            new { Country = "USA", City = "Seattle" },
            new { Country = "USA", City = "San Diego" },
            new { Country = "USA", City = "California" },
            new { Country = "Canada", City = "Montreal" },
            new { Country = "Canada", City = "Toronto" },
            new { Country = "Canada", City = "Vancouver" },
            new { Country = "Canada", City = "Ottawa" }
        };

        var groupedData = data.GroupBy(x => x.Country);

        var result = groupedData.Select(g => new
        {
            Country = g.Key,
            Columns = SplitCitiesIntoColumns(g.Select(x => x.City).ToArray())
        });

        // Find the maximum number of columns
        var maxColumns = result.Max(x => x.Columns.Length);

        // Print the header
        Console.Write("Country\t");
        for (int i = 0; i < maxColumns; i++)
        {
            Console.Write($"Column {i + 1}\t");
        }
        Console.WriteLine();

        // Print the result
        foreach (var country in result)
        {
            Console.Write(country.Country + "\t");
            for (int i = 0; i < maxColumns; i++)
            {
                if (i < country.Columns.Length)
                {
                    Console.Write(country.Columns[i] + "\t");
                }
                else
                {
                    Console.Write("\t");
                }
            }
            Console.WriteLine();
        }
    }

    public static string[] SplitCitiesIntoColumns(string[] cities)
    {
        var columns = new List<string>();
        var currentColumn = string.Empty;

        foreach (var city in cities)
        {
            if (currentColumn.Length + city.Length + 1 > 15)
            {
                columns.Add(currentColumn.Trim());
                currentColumn = city;
            }
            else
            {
                currentColumn += (string.IsNullOrEmpty(currentColumn) ? string.Empty : ", ") + city;
            }
        }

        if (!string.IsNullOrEmpty(currentColumn))
        {
            columns.Add(currentColumn.Trim());
        }

        return columns.ToArray();
    }
}