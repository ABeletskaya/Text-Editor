using System;
using System.Windows.Forms;
using TextEditorBelLibrary;

namespace TextEditorBel
{
    public partial class MainForm : Form
    {
        private string fileName;
        private const string AppText = "Text Editor Bel";

        public MainForm()
        {
            InitializeComponent();
            fileName = "";
            this.Text = AppText;           
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
             if (inputRTB.Modified)
            {
                ActionUtils.SaveCurrentFile(fileName, inputRTB.Text);
                inputRTB.Modified = false;
            }
            inputRTB.Clear();
            fileName = "";
            this.Text = AppText;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputRTB.Modified)
            {
                ActionUtils.SaveCurrentFile(fileName, inputRTB.Text);
                inputRTB.Modified = false;
            }

            if (!ActionUtils.OpenFile())
            {
                return;
            }
            fileName = ActionUtils.file.Name;
            inputRTB.Text = ActionUtils.file.Data;
            this.Text = fileName + " - " + AppText;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputRTB.Modified)
            {
                if (String.IsNullOrEmpty(fileName))
                {
                    ActionUtils.SaveFile(false, inputRTB.Text);
                    if (!String.IsNullOrEmpty(ActionUtils.fileName))
                    {
                        fileName = ActionUtils.fileName;
                        this.Text = fileName + " - " + AppText;
                        inputRTB.Modified = false;
                    }
                    return;
                }

                TextEditorBelDataAccess.SaveExistFile(new FileModel { Name = fileName, Data = inputRTB.Text });
                inputRTB.Modified = false;
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                ActionUtils.SaveFile(false, inputRTB.Text);
            }

            else
            {
                ActionUtils.SaveFile(true, inputRTB.Text);
            }

            if (!String.IsNullOrEmpty(ActionUtils.fileName))
            {
                fileName = ActionUtils.fileName;
                this.Text = fileName + " - " + AppText;
                inputRTB.Modified = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRTB.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRTB.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRTB.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRTB.Undo();
        }

        private void rendoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputRTB.Redo();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Text Editor (test task). \nCreated by Anna Beletskaya");
        }

        private void BeforeExit()
        {
            if (inputRTB.Modified)
            {
                ActionUtils.SaveCurrentFile(fileName, inputRTB.Text);
                inputRTB.Modified = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            BeforeExit();
        }

    }
}
