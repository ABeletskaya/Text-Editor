using System;
using System.Windows.Forms;
using TextEditorBelLibrary;

namespace TextEditorBel
{
    public partial class EnterNameForm : Form
    {
        public string fileName { get; private set; }
        public bool isPassName { get; private set; }
        public EnterNameForm()
        {
            InitializeComponent();
            OKbtn.DialogResult = DialogResult.Yes;
        }

        public EnterNameForm(string oldName)
        {
            InitializeComponent();
            nameTB.Text = oldName;
            OKbtn.DialogResult = DialogResult.Yes;
            isPassName = false;
            fileName = "";
        }

        private async void OKbtn_ClickAsync(object sender, EventArgs e)
        {
            fileName = nameTB.Text;
           
            var collection = await TextEditorBelDataAccess.LoadFilesNameAsync();
            if (!collection.Contains(fileName))
            {
                isPassName = true;               
                return;
            }
            var res = MessageBox.Show("File with this name already exists. \nDo you want to replace it? "
                            , "File is exist", MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                isPassName = true;              
                return;
            }

            isPassName = false;
        }

        private void canselBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
