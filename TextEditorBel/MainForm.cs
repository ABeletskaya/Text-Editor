using System;
using System.IO;
using System.IO.Compression;
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

        private void SaveModifiedData() 
        {
            if (inputRTB.Modified)
            {
                var data = Encoding.Unicode.GetBytes(inputRTB.Rtf);
                var compressData = CompressData(data);
                ActionUtils.SaveCurrentFile(fileName, compressData);
                inputRTB.Modified = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveModifiedData();

            inputRTB.Clear();
            fileName = "";
            this.Text = AppText;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveModifiedData();

            if (!ActionUtils.OpenFile())
            {
                return;
            }

            fileName = ActionUtils.file.Name;  
            
            OutPutDecompressData(ActionUtils.file.Data);
            
            this.Text = fileName + " - " + AppText;
        }
               
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputRTB.Modified)
            {
                var data = Encoding.Unicode.GetBytes(inputRTB.Rtf);
                var compressData = CompressData(data);

                if (String.IsNullOrEmpty(fileName))
                {                   
                    ActionUtils.SaveAsFile(false, compressData);
                    if (!String.IsNullOrEmpty(ActionUtils.fileName))
                    {
                        fileName = ActionUtils.fileName;
                        this.Text = fileName + " - " + AppText;
                        inputRTB.Modified = false;
                    }
                    return;
                }

                ActionUtils.SaveFile(fileName, compressData);

                inputRTB.Modified = false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = Encoding.Unicode.GetBytes(inputRTB.Rtf);
            var compressData = CompressData(data);
            if (String.IsNullOrEmpty(fileName))
            {
                ActionUtils.SaveAsFile(false, compressData);
            }

            else
            {
                ActionUtils.SaveAsFile(true, compressData);
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

            SaveModifiedData();
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

        /// <summary>
        /// Методы для сжатия и восстановления данных
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] CompressData(byte[] data)
        {
            byte[] compressData = null;
            //поток для чтения исходных данных
            using (MemoryStream sourceStream = new MemoryStream(data))
            {
                // поток для записи сжатых данных
                using (MemoryStream targetStream = new MemoryStream())
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой                                                                
                    }
                    compressData = targetStream.ToArray();
                    object r = targetStream.ToArray();
                }
            }
            return compressData;
        }

        private void OutPutDecompressData(byte[] compressData)
        {
            using (MemoryStream sourceStream = new MemoryStream(compressData))
            {
                using (MemoryStream targetStream = new MemoryStream())
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                    var text = Encoding.Unicode.GetString(targetStream.ToArray());
                    inputRTB.Rtf = text;
                }
            }
        }

       
    }
}
