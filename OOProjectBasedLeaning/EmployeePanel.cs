using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{
    public class EmployeePanel : DragDropPanel
    {
        private Employee employee;
        private TimeTracker timeTracker;
        private Label statusLabel;
        private TextBox nameTextBox;

        public Employee Employee => employee;

        public EmployeePanel(Employee employee, TimeTracker timeTracker)
        {
            this.employee = employee;
            this.timeTracker = timeTracker;

            InitializeComponent();
        }

        protected override void OnPanelMouseDown()
        {
            // DragDropPanelのDoDragDropMoveを呼び出してドラッグ開始
            DoDragDropMove();
        }

        private void InitializeComponent()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.LightYellow;
            this.Size = new Size(300, 40);

            Label nameLabel = new Label
            {
                Text = "名前:",
                Location = new Point(10, 10),
                AutoSize = true
            };

            nameTextBox = new TextBox
            {
                Text = employee.Name,
                Location = new Point(60, 6),
                Width = 120
            };
            nameTextBox.TextChanged += (s, e) => { employee.Name = nameTextBox.Text; };

            statusLabel = new Label
            {
                Text = GetStatusText(),
                Location = new Point(190, 10),
                AutoSize = true,
                ForeColor = Color.DarkBlue
            };

            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
            Controls.Add(statusLabel);
        }

        public void UpdateStatus()
        {
            statusLabel.Text = GetStatusText();
        }

        private string GetStatusText()
        {
            return timeTracker.IsAtWork(employee.Id) ? "勤務中" : "退勤中";
        }
    }
}


/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{

    public class EmployeePanel : Panel
    {

        private Employee employee;

        public EmployeePanel(Employee employee)
        {

            this.employee = employee;

            InitializeComponent();

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

}
*/