using System;
using System.Windows.Forms;
using TextEditorBelLibrary;

namespace TextEditorBel
{
    public partial class OpenFileForm : Form
    {
        public string fileName { get; private set; }
        public bool isPassName { get; private set; }

        public OpenFileForm()
        {
            InitializeComponent();
            InitialFilesNameList();
            isPassName = false;
            fileName = "";
        }

        private void InitialFilesNameList()
        {
            var collection = TextEditorBelDataAccess.LoadFilesName();
            foreach (var item in collection)
            {
                filesLB.Items.Add(item);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void filesLB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Open();
        }

        private void Open()
        {
            fileName = filesLB.Text;
            isPassName = true;
            this.Close();
        }
    }
}
