using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignment
{
    public class RecordParser : IRecordParser
    {
        public char[] ParsingCharacters = new char[] { '|', ',', ' ' };

        public Record ParseRecord(string text)
        {
            var record = new Record();
            var parts = text
                .Split(ParsingCharacters) // Split based upon the characters given
                .Select(p => p.Trim()) // Trim any white space
                .Where(p => !String.IsNullOrEmpty(p)) // Remove any empty strings
                .ToList();

            if (parts.Count < 5)
            {
                throw new ArgumentException(
                    "Invalid Line to parse",
                    "string text",
                    new Exception($"Invalid Line: {text}"));
            }

            record.LastName = parts[0];
            record.FirstName = parts[1];
            record.Gender = parts[2];
            record.FavoriteColor = parts[3];
            var timeParsed = DateTime.TryParseExact(parts[4], "M/d/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out record.DateOfBirth);
            if(!timeParsed)
            {
                throw new ArgumentException(
                    "Invalid Date to parse",
                    "DateTime DateOfBirth",
                    new Exception($"Invalid DateTime for Birthdate: {parts[4]}"));
            }

            return record;
        }
    }
}
