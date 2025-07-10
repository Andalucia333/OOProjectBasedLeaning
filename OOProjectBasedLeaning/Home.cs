using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{
    public interface Home : Model,Place
    {
        Home AddEmployee(Employee employee);
        Home RemoveEmployee(Employee employee);
    }

    public class HomeModel : ModelEntity, Home
    {
        private Employee employee = NullEmployee.Instance;

        public HomeModel(string name)
        {
            this.Name = name;
        }

        public Home AddEmployee(Employee employee)
        {
            this.employee = employee;
            return this;
        }

        public Home RemoveEmployee( Employee employee)
        {
            employee = NullEmployee.Instance;
            return this;
        }
    }

    public class NullHome : ModelEntity, Home, NullObject
    {
        private static readonly Home instance = new NullHome();

        public override string Name
        {
            get { return string.Empty; }
            set { }
        }

        public static Home Instance
        {
            get { return instance; }
        }

        public Home AddEmployee(Employee employee)
        {
            return this;
        }

        public Home RemoveEmployee(Employee employee)
        {
            return this;
        }
    }
}
