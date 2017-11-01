using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodingAssignment
{
    public class Record : IComparable<Record>,IEqualityComparer<Record>,IEquatable<Record>
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

        public static bool operator ==(Record obj1, Record obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (ReferenceEquals(obj1, null))
            {
                return false;
            }
            if (ReferenceEquals(obj2, null))
            {
                return false;
            }

            return obj1.CompareTo(obj2) == 0;
        }

        public static bool operator !=(Record obj1, Record obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(Record x, Record y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal.
            return x.CompareTo(y) == 0;
        }

        public bool Equals(Record other)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return this.CompareTo(other) == 0;
        }

        public int GetHashCode(Record obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashLastName = obj.LastName == null ? 0 : obj.LastName.GetHashCode();
            int hashFirstName = obj.FirstName == null ? 0 : obj.FirstName.GetHashCode();
            int hashGender = obj.Gender == null ? 0 : obj.Gender.GetHashCode();
            int hashFavoriteColor = obj.FavoriteColor == null ? 0 : obj.FavoriteColor.GetHashCode();
            int hashDateOfBirth = obj.DateOfBirth.GetHashCode();

            return hashDateOfBirth ^ hashFavoriteColor ^ hashFirstName ^ hashGender ^ hashLastName;
        }

        public override string ToString()
        {
            return $"{LastName} | {FirstName} | {Gender} | {FavoriteColor} | {DateOfBirth.ToString("M/d/yyyy")}";
        }
    }
}
