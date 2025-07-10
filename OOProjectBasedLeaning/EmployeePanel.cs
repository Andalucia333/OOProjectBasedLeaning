using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{

    public class EmployeePanel : DragDropPanel
    {
        private Employee employee = NullEmployee.Instance;
        private EmployeeNameTextBox employeeNameTextBox = NullEmployeeNameTextBox.Instance;

        public EmployeePanel(Employee employee)
        {

            this.employee = employee;

            InitializeComponent();

        }

        private void InitializeComponent()
        {

            AutoSize = true;

            EmployeeStatusLabel employeeStatusLabel = new EmployeeStatusLabel(employee)
            {
                Location = new Point(20, 10)
            };

            EmployeeNameLabel employeeNameLabel = new EmployeeNameLabel(employee)
            {
                Location = new Point(20, 30),
                ForeColor = Color.Black,
            };

            employeeNameTextBox = new EmployeeNameTextBox(employee)
            {
                Location = new Point(140, 26),
            };

            Controls.Add(employeeStatusLabel);
            Controls.Add(employeeNameLabel);
            Controls.Add(employeeNameTextBox);

        }

        protected override void OnPanelMouseDown()
        {
            DoDragDropMove();

            if (GetFrom() is not EmployeeCreatorForm)
            {
                employeeNameTextBox.ReadOnly = true;
                employeeNameTextBox.Hide();

                try
                {
                    if (GetFrom() is CompanyForm)
                    {
                        employee.GoToCompany();
                    }
                    else if (GetFrom() is HomeForm)
                    {
                        employee.GoToHome();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                employeeNameTextBox.ReadOnly = false;
                employeeNameTextBox.Show();
            }
        }

        public void AddCompany(Company company)
        {
            employee.AddCompany(company);
        }

        public void RemoveCompany()
        {
            employee.RemoveCompany();
        }

        public void AddHome(Home home)
        {
            employee.AddHome(home);
        }

        public void RemoveHome()
        {
            employee.RemoveHome();
        }

    }



    //EmployeeStatusLabel
    //勤務状態を表示
    public class EmployeeStatusLabel : Label, Observer
    {
        private Employee employee = NullEmployee.Instance;

        public EmployeeStatusLabel(Employee employee)
        {
            if (employee is NotifierModelEntity)
            {
                (employee as NotifierModelEntity).AddObserver(this);
            }

            this.employee = employee;
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            this.AutoSize = true;
            this.Font = new Font("Arial", 10, FontStyle.Regular);

            Update(this);
        }

        public void Update(object sender)
        {
            if (employee.IsAtWork())
            {
                Text = "勤務中";
                ForeColor = Color.Red;
            }
            else if (employee.IsAtHome())
            {
                Text = "帰宅中";
                ForeColor = Color.Green;
            }
            else
            {
                Text = "―――";
                ForeColor = Color.Gray;
            }
        }
    }



    //EmployeeNameLabel 
    //社員の名前を表示
    public class EmployeeNameLabel : Label, Observer
    {
        private Employee employee = NullEmployee.Instance;

        public EmployeeNameLabel(Employee employee)
        {
            if (employee is NotifierModelEntity)
            {
                (employee as NotifierModelEntity).AddObserver(this);
            }

            this.employee = employee;
            InitializeComponent();

        }

        private void InitializeComponent()
        {
            this.AutoSize = true;
            Update(this);
        }

        public void Update(object sender)
        {
            Text = employee.Name;
        }
    }



    //EmployeeNameTextBox
    //社員の名前を編集（EmployeeCreatorForm限定）
    public class EmployeeNameTextBox : TextBox
    {
        private Employee employee = NullEmployee.Instance;
        public EmployeeNameTextBox(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AutoSize = true;
            Text = employee.Name;
        }

        public void Save()
        {
            employee.Name = Text;
        }
    }



    //NullEmployeeNameTextBox 
    //EmployeeNameTextBoxのNull
    public class NullEmployeeNameTextBox : EmployeeNameTextBox
    {
        private static readonly EmployeeNameTextBox instance = new NullEmployeeNameTextBox();
        private NullEmployeeNameTextBox() : base(NullEmployee.Instance)
        {

        }

        public static EmployeeNameTextBox Instance
        {
            get { return instance; }
        }

        public override string Text
        {
            get { return string.Empty; }
            set { }
        }
    }

}
