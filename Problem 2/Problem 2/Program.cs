using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LibarySystem
{
    public class Program
    {
        static void Main()
        {
            try
            {
                /*
                Attribution/Refrencing:
                
                Book Minder:
                CSVReader – Class to read the csv form data file – Modified(Removed parts)
                Data table – Way to store data from csv file

                Me:
                Exportocsv – How to export data to new csv file
                Getcafor – How to generate hash for data from csv file
                Added multiple tries and catches to catch errors
                Public book
                */
                // Path to the CSV file
                string csvFile = @"Problem 2 Data.csv";

                // Read data from the CSV file and convert it to a DataTable
                DataTable csvData = CSVreader.GetDataTableFromCSVFile(csvFile);

                // Console.WriteLine how many records the program read
                Console.WriteLine($"Read {csvData.Rows.Count} records");

                // Create a list to store the Book objects
                List<Book> books = new List<Book>();

                // Iterate over each row in the DataTable and create a Book object
                foreach (DataRow row in csvData.Rows)
                {
                    // Create a new Book object using the values from the row
                    books.Add(new Book(row));
                }

                // Print the details of three sample books
                Console.WriteLine($"Sample - Book 1: {books[0]}");
                Console.WriteLine($"Sample - Book 26: {books[25]}");
                Console.WriteLine($"Sample - Book 51: {books[50]}");

                // Export the list of books to a CSV file
                try
                {
                    ExportToCSV(books, @"Output.csv");
                    Console.WriteLine("Data exported successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        private static void ExportToCSV(List<Book> books, string csvFile)
        {
            // Create a StreamWriter to write to the CSV file
            using (var writer = new StreamWriter(csvFile))
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
        public static DataTable GetDataTableFromCSVFile(string csvFile)
        {
            DataTable csvData = new DataTable();

            try
            {
                // Use TextFieldParser to read the CSV file
                using (TextFieldParser csvReader = new TextFieldParser(csvFile))
                {
                    // Set the delimiters for parsing the fields
                    csvReader.SetDelimiters(new string[] { "," });

                    // Read the first row as column names
                    string[] colFields = csvReader.ReadFields();

                    // Create columns in the DataTable based on the column names
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        csvData.Columns.Add(datecolumn);
                    }

                    // Read the remaining rows and add them to the DataTable
                    while (!csvReader.EndOfData)
                    {
                        // Add the row to the DataTable
                        csvData.Rows.Add(csvReader.ReadFields());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during reading the CSV file
                Console.WriteLine("Error occured");
            }

            return csvData;
        }
    }

    public readonly record struct Book
    {
    public Book(DataRow row)
    {
        // Assign the provided values from the DataRow to the corresponding properties
        Name = row[0].ToString();
        Title = row[1].ToString();
        PublishedIn = row[2].ToString();
        Publisher = row[3].ToString();
        Date = row[4].ToString();

        // Generate the Cat value based on the provided data
        Cat = GetCatFor(Name, Title, PublishedIn, Publisher, Date);
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

            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the SHA256 hash of the source string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
                // Convert the hash bytes to a hexadecimal string representation
                string hash = BitConverter.ToString(hashBytes).Replace("-", "");
                // Take the first 10 characters of the hash as the 'Cat' value
                string cat = hash.Substring(0, 10);

                return cat;
            }
        }
    }
}
