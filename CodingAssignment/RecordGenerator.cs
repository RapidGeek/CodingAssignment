namespace CodingAssignment
{
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
}
