using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CodingAssignment
{
    public sealed class RecordDatabase
    {
        #region Private Static Variables
        private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        private static List<Record> recordDB = new List<Record>();
        private static readonly Lazy<RecordDatabase> records =
            new Lazy<RecordDatabase>(() => new RecordDatabase());
        private static RecordParser parser = new RecordParser();
        private static bool generateRandom = true;
        #endregion

        // Our instance of the Singleton
        public static RecordDatabase Instance { get { return records.Value; } }
        
        /// <summary>
        /// Private Constructor which enforces use of Instance
        /// </summary>
        private RecordDatabase()
        {
            if (generateRandom)
            {
                var gen = new RecordGenerator();

                for (int i = 0; i < 10; i++)
                {
                    Add(gen.GenerateRandomRecord());
                }
                generateRandom = false;
            }
        }

        /// <summary>
        /// Single text line which contains a formatted record
        /// </summary>
        /// <param name="text"></param>
        public void AddFormattedText(string text)
        {
            Add(parser.ParseRecord(text));
        }

        /// <summary>
        /// Path to a file of formatted text lines. One record per line
        /// </summary>
        /// <param name="filename"></param>
        public void AddFile(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    AddFormattedText(line);
                }
            }
        }

        #region Standard CRUD
        public void Add(Record record)
        {
            if (IsRecordValid(record))
            {
                locker.EnterWriteLock();
                try
                {
                    recordDB.Add(record);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
        }

        public void AddRange(List<Record> records)
        {
            foreach (var record in records)
            {
                Add(record);
            }
        }

        public List<Record> Get()
        {
            locker.EnterReadLock();
            try
            {
                return recordDB;
            }
            finally
            {
                locker.ExitReadLock();
            }
        }

        public Record Get(int id)
        {
            if (IsRecordIdValid(id))
            {
                locker.EnterReadLock();
                try
                {
                    return recordDB[id];
                }
                finally
                {
                    locker.ExitReadLock();
                }
            }
            else
            {
                throw new ArgumentException(
                    $"ID: {id} is out of range",
                    "int id");
            }
        }

        public void Update(int id, Record record)
        {
            if (IsRecordIdValid(id))
            {
                if (IsRecordValid(record))
                {
                    locker.EnterWriteLock();
                    try
                    {
                        recordDB[id] = record;
                    }
                    finally
                    {
                        locker.ExitWriteLock();
                    }
                }
                else
                {
                    throw new ArgumentException(
                    $"Record: {record} is invalid",
                    "Record record");
                }
            }
            else
            {
                throw new ArgumentException(
                    $"ID: {id} is out of range",
                    "int id");
            }
        }

        public void Delete(int id)
        {
            if (IsRecordIdValid(id))
            {
                locker.EnterWriteLock();
                try
                {
                    recordDB.RemoveAt(id);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
            else
            {
                throw new ArgumentException(
                    $"ID: {id} is out of range",
                    "int id");
            }
        }

        public void Delete(Record record)
        {
            if (IsRecordValid(record))
            {
                locker.EnterWriteLock();
                try
                {
                    recordDB.Remove(record);
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
            else
            {
                throw new ArgumentException(
                $"Record: {record} is invalid",
                "Record record");
            }
        }
        #endregion

        /// <summary>
        /// Sort by Gender, then by Last Name, and return a list
        /// </summary>
        /// <returns></returns>
        public List<Record> SortByGenderThenByLastName()
        {
            return recordDB
                .OrderBy(r => r.Gender)
                .ThenBy(r => r.LastName)
                .ToList();
        }

        /// <summary>
        /// Sort by Birthdate and return a list
        /// </summary>
        /// <returns></returns>
        public List<Record> SortByBirthDate()
        {
            return recordDB
                .OrderBy(r => r.DateOfBirth)
                .ToList();
        }

        /// <summary>
        /// Sort by Last Name Descending and return a list
        /// </summary>
        /// <returns></returns>
        public List<Record> SortByLastNameDescending()
        {
            return recordDB
                .OrderByDescending(r => r.LastName)
                .ToList();
        }

        #region Private Helper Methods
        private bool IsRecordValid(Record record)
        {
            if (record == null) return false;
            else if (String.IsNullOrEmpty(record.LastName.Trim()) ||
                String.IsNullOrEmpty(record.FirstName.Trim()) ||
                String.IsNullOrEmpty(record.FavoriteColor.Trim()) ||
                String.IsNullOrEmpty(record.Gender))
                return false;
            else if (record.LastName.Any(c => char.IsDigit(c)) ||
                record.FirstName.Any(c => char.IsDigit(c)) ||
                record.Gender.Any(c => char.IsDigit(c)))
                return false;
            else
                return true;
        }

        private bool IsRecordIdValid(int id)
        {
            locker.EnterReadLock();
            try
            {
                return (id >= 0 && id < recordDB.Count);
            }
            finally
            {
                locker.ExitReadLock();
            }
        }
        #endregion
    }
}
