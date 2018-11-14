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
            InitialFilesNameListAsync();
            isPassName = false;
            fileName = "";
        }

        private async void InitialFilesNameListAsync()
        {
            var collection = await TextEditorBelDataAccess.LoadFilesNameAsync();
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
            isPassName = false;
            if (fileName.Length > 2)
            {
                isPassName = true;
                this.Close();
            }           
        }
    }
}
