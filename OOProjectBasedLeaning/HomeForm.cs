
namespace OOProjectBasedLeaning
{

    public partial class HomeForm : DragDropForm
    {
        private Home home = NullHome.Instance;

        public HomeForm()
        {
            InitializeComponent();
            home = new HomeModel("MyHome");
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
                employeePanel.AddHome(home);
            }
        }

    }

}
