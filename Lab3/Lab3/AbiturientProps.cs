using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDesign
{
    public partial class Abiturient
    {
        private readonly int _id;
        private string _name;
        private string _surname;
        private string _middleName;
        private string _address;
        private string _phone;
        private int[] _marks;

        private static int _num = 0;

        private const string UniversityName = "БГТУ";

        public string Name
        {
            get { return _name; }
            set
            {
                if(CheckForNullOrEmpty(value, "Name"))
                    _name = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
            protected set
            {
                if (CheckForNullOrEmpty(value, "Surname"))
                    _surname = value;
            }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                if (CheckForNullOrEmpty(value, "Middlename"))
                    _middleName = value;
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (CheckForNullOrEmpty(value, "Address"))
                    _address = value;
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (CheckForNullOrEmpty(value, "Phone"))
                    _phone = value;
            }
        }

        public int[] Marks
        {
            get { return _marks; }
            set
            {
                if(ValidateMarks(value, "Marks"))
                    _marks = value;
            }
        }
    }
}
