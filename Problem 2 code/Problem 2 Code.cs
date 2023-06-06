using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace BookMinder
{
    public class BookMinder
    {
        static async Task Main()
        {
            // Path to the CSV file
            string csvFilePath = @"C:/Problem 2 Data.csv";

            // Read data from the CSV file and convert it to a DataTable
            DataTable csvData = CSVreader.GetDataTableFromCSVFile(csvFilePath);
            Console.WriteLine($"Read {csvData.Rows.Count} records");

            // Create a list to store the Book objects
            List<Book> books = new List<Book>();

            // Iterate over each row in the DataTable and create a Book object
            foreach (DataRow row in csvData.Rows)
            {
                // Create a new Book object using the values from the row
                books.Add(new Book(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString()));
            }

            // Print the details of two sample books
            Console.WriteLine($"Sample - Book 1: {books[0]}");
            Console.WriteLine($"Sample - Book 2: {books[1]}");

            // Export the list of books to a CSV file
            ExportToCSV(books, @"C:/output.csv");

            Console.WriteLine("Data exported successfully.");
        }


        private static void ExportToCSV(List<Book> books, string csvFilePath)
        {
            // Create a StreamWriter to write to the CSV file
            using (var writer = new StreamWriter(csvFilePath))
            {
                // Write the header row with column names
                writer.WriteLine("Name,Title,PublishedIn,Publisher,Date,Cat");

                // Iterate over each book and write its details as a CSV row
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Name},{book.Title},{book.PublishedIn},{book.Publisher},{book.Date},{book.Cat}");
                }
            }
        }
    }

    static class CSVreader
    {
        public static DataTable GetDataTableFromCSVFile(string csvFilePath)
        {
            DataTable csvData = new DataTable();

            try
            {
                // Use TextFieldParser to read the CSV file
                using (TextFieldParser csvReader = new TextFieldParser(csvFilePath))
                {
                    // Set the delimiters for parsing the fields
                    csvReader.SetDelimiters(new string[] { "," });

                    // Specify if fields are enclosed in quotes
                    csvReader.HasFieldsEnclosedInQuotes = true;

                    // Read the first row as column names
                    string[] colFields = csvReader.ReadFields();

                    // Create columns in the DataTable based on the column names
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    // Read the remaining rows and add them to the DataTable
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();

                        // Replace empty values with null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }

                        // Add the row to the DataTable
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during reading the CSV file
                // You might want to add error logging or display an error message here
            }

            return csvData;
        }
    }

    public readonly record struct Book
    {
        public Book(string name, string title, string publishedIn, string publisher, string date)
        {
            // Assign the provided values to the corresponding properties
            Name = name;
            Title = title;
            PublishedIn = publishedIn;
            Publisher = publisher;
            Date = date;

            // Generate the Cat value based on the provided data
            Cat = GetCatFor(name, title, publishedIn, publisher, date);
        }

        // Properties of the Book record
        public string Cat { get; }
        public string Name { get; }
        public string Title { get; }
        public string PublishedIn { get; }
        public string Publisher { get; }
        public string Date { get; }

        private static string GetCatFor(string name, string title, string publishedIn, string publisher, string date)
        {
            string source = name + title + publishedIn + publisher + date;

            using (MD5 md5 = MD5.Create())
            {
                string hash = GetHash(md5, source);
                return hash[..10];
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            // Convert the input string to a byte array and compute the hash
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a StringBuilder to collect the bytes and create a string
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string
            return sBuilder.ToString();
        }
    }
}
