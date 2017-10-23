using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodingAssignment
{
    public class Record : IComparable<Record>
    {
        public String LastName;
        public String FirstName;
        public String Gender;
        public String FavoriteColor;
        public DateTime DateOfBirth;

        public int CompareTo(Record other)
        {
            var lastNameComparison = this.LastName.CompareTo(other.LastName);
            if (lastNameComparison == 0)
            {
                var firstNameComparison = this.FirstName.CompareTo(other.FirstName);
                if(firstNameComparison == 0)
                {
                    var dateOfBirthComparison = this.DateOfBirth.CompareTo(other.DateOfBirth);
                    if(dateOfBirthComparison == 0)
                    {
                        var genderComparison = this.Gender.CompareTo(other.Gender);
                        if(genderComparison == 0)
                        {
                            return this.FavoriteColor.CompareTo(other.FavoriteColor);
                        }
                        else
                        {
                            return genderComparison;
                        }
                    }
                    else
                    {
                        return dateOfBirthComparison;
                    }
                }
                else
                {
                    return firstNameComparison;
                }
            }
            else
            {
                return lastNameComparison;
            }
        }

        public override string ToString()
        {
            return $"{LastName} | {FirstName} | {Gender} | {FavoriteColor} | {DateOfBirth.ToString("M/d/yyyy")}";
        }
    }

    public class RecordGenerator
    {
        public Record GenerateRandomRecord()
        {
            var gen = new Generator();
            var record = new Record();
            record.LastName = gen.GenerateString(capitalizeFirstLetter:true);
            record.FirstName = gen.GenerateString(capitalizeFirstLetter:true);
            record.Gender = gen.GenerateGender();
            record.FavoriteColor = gen.GenerateColor();
            record.DateOfBirth = gen.GenerateDateOfBirth();

            return record;
        }
    }

    public class Generator
    {
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        KnownColor[] colorArray = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToArray();

        public String GenerateString(int lengthOfString = 10, bool capitalizeFirstLetter = false)
        {
            StringBuilder buffer = new StringBuilder();

            var bytes = new Byte[lengthOfString * 8];
            crypto.GetBytes(bytes);

            int i = 0;
            int a = (int)'a';
            int z = (int)'z';
            while(buffer.Length < lengthOfString)
            {
                while(bytes[i] < a)
                {
                    bytes[i] += 26;
                }

                while(bytes[i] > z)
                {
                    bytes[i] -= 26;
                }

                var letter = (char)bytes[i];

                if (capitalizeFirstLetter)
                {
                    letter = letter.ToString().ToUpper().ToCharArray()[0];
                    capitalizeFirstLetter = false;
                }
           

                buffer.Append(letter);
                i++;
            }

            return buffer.ToString();
        }

        public String GenerateGender()
        {
            var bytes = new Byte[8];
            crypto.GetBytes(bytes);
            var value = BitConverter.ToUInt64(bytes, 0) % 2;
            return (value == 1) ? "Male" : "Female";
        }

        public String GenerateColor()
        {
            var length = colorArray.Length;
            var bytes = new Byte[4];
            crypto.GetBytes(bytes);
            var index = Math.Abs(BitConverter.ToInt32(bytes, 0)) % length;

            return colorArray[index].ToString();
        }

        public DateTime GenerateDateOfBirth()
        {
            var bytes = new Byte[8];
            crypto.GetBytes(bytes);
            var yearYalue = BitConverter.ToUInt16(bytes, 0) % (DateTime.Now.Year-1930);
            var monthYalue = (BitConverter.ToUInt16(bytes, 2) % 12) + 1;
            var dayModValue = (monthYalue == 2) ?
                28 :
                ((monthYalue == 9 || monthYalue == 4 || monthYalue == 6 || monthYalue == 11) ?
                    30 :
                    31);
            var dayYalue = BitConverter.ToUInt16(bytes, 4) % dayModValue;
            var year = 1930 + yearYalue;
            var month = monthYalue;
            var day = dayYalue + 1;

            return new DateTime(year, month, day);
        }
    }
}
