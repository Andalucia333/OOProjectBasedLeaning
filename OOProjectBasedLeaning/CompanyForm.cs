using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOProjectBasedLeaning
{

    public partial class CompanyForm : DragDropForm
    {

        private Company company = NullCompany.Instance;

        public CompanyForm()
        {

            InitializeComponent();

            company = new CompanyModel("MyCompany");

            // TODO: タイムレコーダーのパネルを設置する
            new TimeTrackerModel(company);

        }

        protected override void OnFormDragEnterSerializable(DragEventArgs dragEventArgs)
        {
            dragEventArgs.Effect = DragDropEffects.Move;
        }

        protected override void OnFormDragDropSerializable(object? serializableObject, DragEventArgs dragEventArgs)
        {
            if(serializableObject is EmployeePanel)
            {
                EmployeePanel employeePanel = serializableObject as EmployeePanel;
                employeePanel.AddDragDropForm(this,PointToClient(new Point(dragEventArgs.X,dragEventArgs.Y)));
                employeePanel.AddCompany(company);
            }
        }


    }

}
