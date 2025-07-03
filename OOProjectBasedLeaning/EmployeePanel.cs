using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{

    public class EmployeePanel : DragDropPanel
    {



        private Employee employee;

        public EmployeePanel(Employee employee)
        {

            this.employee = employee;

            InitializeComponent();

        }

        protected override void OnPanelMouseDown()
        {
            DoDragDropMove();

            if (GetFrom() is not EmployeeCreatorForm)
            {


            }
            else
            {

            }
        }


        private void InitializeComponent()
        {

            Label employeeNameLabel = new Label
            {
                Text = employee.Name,
                AutoSize = true,
                Location = new Point(20, 10)
            };

            TextBox guestNameTextBox = new TextBox
            {
                Text = employee.Name,
                Location = new Point(140, 6),
                Width = 160
            };

            Controls.Add(employeeNameLabel);
            Controls.Add(guestNameTextBox);

        }

    }

    public class EmployeeStatusLabel : Label, Observer
    {
        private Employee employee = NullEmployee.Instance;

        public EmployeeStatusLabel()
        {
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

    public class EmployeeNameLabel : Label, Observer
    {
        private Employee employee = NullEmployee.Instance;

        public EmployeeNameLabel(Employee employee)
        {
            if(employee is NotifierModelEntity)
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



}
