using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignment
{
    public class Program
    {
        // Record DB Singleton
        static RecordDatabase db = RecordDatabase.Instance;

        static void Main(string[] args)
        {
            // Check for insufficient number of arguments for the input file
            if(args == null || args.Length == 0)
            {
                args = HandleNoCommandLineArguments();

                // The user didn't make a test file so exit
                if (args.Length == 0) return;
            }

            // Add the file to the record database
            db.AddFile(args[0]);

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
            var recordList = new List<Record>();

            switch(option)
            {
                case 1:
                    recordList = db.SortByGenderThenByLastName();
                    Console.WriteLine("Records Sorted by Gender and then Last Name Ascending");
                    break;
                case 2:
                    recordList = db.SortByBirthDate();
                    Console.WriteLine("Records Sorted by Birthdate Ascending");
                    break;
                case 3:
                    recordList = db.SortByLastNameDescending();
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
