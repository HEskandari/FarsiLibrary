using System;
using FarsiLibrary.Utils;

namespace FarsiLibrary.WinFormDemo.Data
{
    public class Employee
    {
        #region Fields

        private int employeeID;
        private string lastname;
        private string firstname;
        private string address;
        private string city;
        private PersianDate hireDate;
        private PersianDate birthDate;

        #endregion

        #region Events

        public event EventHandler EmployeeChanged;

        #endregion
        
        #region Ctor

        public Employee(int employeeID)
        {
            this.employeeID = employeeID;
        }

        public Employee(int employeeID, string lastname, string firstname, string address, string city, PersianDate hireDate, PersianDate birthDate)
        {
            this.employeeID = employeeID;
            this.lastname = lastname;
            this.firstname = firstname;
            this.address = address;
            this.city = city;
            this.hireDate = hireDate;
            this.birthDate = birthDate;
        }

        #endregion

        #region Props

        public int EmployeeID
        {
            get { return employeeID; }
        }

        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnEmployeeChanged(EventArgs.Empty);
            }
        }

        public string Firstname
        {
            get { return firstname; }
            set
            {
                firstname = value;
                OnEmployeeChanged(EventArgs.Empty);
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnEmployeeChanged(EventArgs.Empty);
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnEmployeeChanged(EventArgs.Empty);
            }
        }

        public DateTime HireDate
        {
            get { return hireDate; }
            set
            {
                hireDate = value;
                OnEmployeeChanged(EventArgs.Empty);
            }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnEmployeeChanged(EventArgs.Empty);
            }
        }

        #endregion

        #region Methods

        protected virtual void OnEmployeeChanged(EventArgs e)
        {
            if (EmployeeChanged != null)
                EmployeeChanged(this, e);
        }
        
        #endregion
    }
}