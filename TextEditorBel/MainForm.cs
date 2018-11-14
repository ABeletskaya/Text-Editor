using System;
using System.Text;
using System.Windows.Forms;

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

        private async void SaveModifiedDataAsync() 
        {
            if (inputRTB.Modified)
            {
                var data = Encoding.Unicode.GetBytes(inputRTB.Rtf);
                var compressData =  Archive.CompressData(data);
                await ActionUtils.SaveCurrentFileAsync(fileName, compressData);
                inputRTB.Modified = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveModifiedDataAsync();

            inputRTB.Clear();
            fileName = "";
            this.Text = AppText;
        }

        private async void openToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            SaveModifiedDataAsync();

            if (! await ActionUtils.OpenFileAsync())
            {
                return;
            }

            fileName = ActionUtils.file.Name;

            inputRTB.Rtf = Archive.OutPutDecompressData(ActionUtils.file.Data);

            this.Text = fileName + " - " + AppText;
        }
               
        private async void saveToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            if (inputRTB.Modified)
            {
                var data = Encoding.Unicode.GetBytes(inputRTB.Rtf);
                var compressData =  Archive.CompressData(data);

                if (String.IsNullOrEmpty(fileName))
                {                   
                    await ActionUtils.SaveAsFileAsync(false, compressData);
                    if (!String.IsNullOrEmpty(ActionUtils.fileName))
                    {
                        fileName = ActionUtils.fileName;
                        this.Text = fileName + " - " + AppText;
                        inputRTB.Modified = false;
                    }
                    return;
                }

                await ActionUtils.SaveFileAsync(fileName, compressData);

                inputRTB.Modified = false;
            }
        }

        private async void saveAsToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            var data = Encoding.Unicode.GetBytes(inputRTB.Rtf);
            var compressData =  Archive.CompressData(data);
            if (String.IsNullOrEmpty(fileName))
            {
                await ActionUtils.SaveAsFileAsync(false, compressData);
            }

            else
            {
                await ActionUtils.SaveAsFileAsync(true, compressData);
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
       
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            SaveModifiedDataAsync();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = inputRTB.SelectionFont;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                inputRTB.SelectionFont = fontDialog.Font;
            }
        }               
    }
}
