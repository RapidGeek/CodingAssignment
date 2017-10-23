using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignment
{
    class Program
    {
        static private List<Record> recordList = new List<Record>();

        static void Main(string[] args)
        {
            // Check for insufficient number of arguments for the input file
            if(args == null || args.Length == 0)
            {
                args = HandleNoCommandLineArguments();

                // The user didn't make a test file so exit
                if (args.Length == 0) return;
            }

            recordList.AddRange(ParseFile(args[0]));

            var option = 0;
            do
            {
                DisplayOptionsToUser();
                option = UserEnteredOptionThroughConsole();
                ExecuteSortingOption(option);
            } while (option != 0);
        }

        static private string[] HandleNoCommandLineArguments()
        {
            Console.Write("Error: No input file. Should a random file be generated? yes/no? ");
            var generateFile = Console.ReadLine().ToLower().Contains("y");
            if (!generateFile)
            {
                return new string[] { };
            }

            var path = GenerateTestFile();
            return new string[] { path };
        }

        static private String GenerateTestFile()
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "TestFile.txt");
                using (var writer = new StreamWriter(path))
                {
                    RecordGenerator generator = new RecordGenerator();
                    for( int i =0; i< 10; i++)
                    {
                        var record = generator.GenerateRandomRecord();
                        writer.WriteLine($"{record.LastName} | {record.FirstName} | {record.Gender} | {record.FavoriteColor} | {record.DateOfBirth.ToShortDateString()}");
                    }
                }

                return path;
            }
            catch(Exception ex)
            {
                throw new Exception(
                    "Failed to Generate a test file",
                    ex);
            }
        }

        static private List<Record> ParseFile(String file)
        {
            List<Record> recordList = new List<Record>();

            try
            {
                using (var reader = new StreamReader(file))
                {
                    var parser = new RecordParser();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var record = parser.ParseRecord(line);
                        recordList.Add(record);
                    }
                }

                return recordList;
            }
            catch(Exception ex)
            {
                throw new ArgumentException(
                    "Unable to parse the given file",
                    "String file",
                    ex);
            }
        }

        static private void DisplayOptionsToUser()
        {
            Console.WriteLine("Choose Option 1 to sort by Gender and then Last Name Ascending");
            Console.WriteLine("Choose Option 2 to sort by Birthdate Ascending");
            Console.WriteLine("Choose Option 3 to sort by Last Name Descending");
            Console.WriteLine("Choose Option 0 to exit");

        }

        static private int UserEnteredOptionThroughConsole()
        {
            var value = -1;
            try
            {
                Console.WriteLine("Enter your Option: ");
                var line = Console.ReadLine().Trim();
                Console.Clear();
                value = Convert.ToInt32(line);
                return value;
            }
            catch
            {
                return value;
            }
        }

        static private void ExecuteSortingOption(int option)
        {
            var endEarly = false;

            switch(option)
            {
                case 1:
                    recordList = recordList
                        .OrderBy(r => r.LastName)
                        .OrderBy(r => r.Gender)
                        .ToList();
                    Console.WriteLine("Records Sorted by Gender and then Last Name Ascending");
                    break;
                case 2:
                    recordList = recordList
                        .OrderBy(r => r.DateOfBirth)
                        .ToList();
                    Console.WriteLine("Records Sorted by Birthdate Ascending");
                    break;
                case 3:
                    recordList = recordList
                        .OrderByDescending(r => r.LastName)
                        .ToList();
                    Console.WriteLine("Records Sorted by Last Name Descending");
                    break;
                default:
                    endEarly = true;
                    break;
            }

            // Invalid options are short circuited
            if (endEarly) return;

            foreach(var record in recordList)
            {
                Console.WriteLine(record.ToString());
            }
        }
    }
}
