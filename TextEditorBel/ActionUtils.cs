using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditorBelLibrary;

namespace TextEditorBel
{
    public class ActionUtils
    {
        public static string fileName { get; private set; }
        public static FileModel file { get; private set; }

        public static async Task<bool> OpenFileAsync()
        {
            fileName = "";          
            OpenFileForm openDialog = new OpenFileForm();
            openDialog.ShowDialog();

            if (openDialog.isPassName)
            {
                fileName = openDialog.fileName;
                file = await TextEditorBelDataAccess.LoadFileAsync(fileName);
                return true;
            }

            openDialog.Dispose();

            return false;
        }

        public static async Task SaveCurrentFileAsync(string name, byte[] data)
        {
            var res = MessageBox.Show("Do you want to save changes into current file? "
                          , "Save changes", MessageBoxButtons.YesNo
                          , MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (String.IsNullOrEmpty(name))
                {
                    await SaveAsFileAsync(false, data);
                }

                else
                {
                    await SaveFileAsync(name, data);
                    MessageBox.Show("File has been saved");
                }
            }
        }

        public static async Task SaveFileAsync(string name, byte[] data)
        {
            file.Name = name;
            file.Data = data;
            await TextEditorBelDataAccess.SaveExistFileAsync(file);
        }

        public static async Task SaveAsFileAsync(bool isExist, byte[] data)
        {
            EnterNameForm nameDialog = (isExist) ? new EnterNameForm(fileName) : new EnterNameForm();
            nameDialog.ShowDialog();

            if (!nameDialog.isPassName)
            {
                MessageBox.Show("File not saved");
                return;
            }

            if (fileName != nameDialog.fileName)
            {
                isExist = false;
                fileName = nameDialog.fileName;
            }

            var file = new FileModel { Name = fileName, Data = data };
            nameDialog.Dispose();

            try
            {
                if (isExist)
                {
                    await TextEditorBelDataAccess.SaveExistFileAsync(file);
                }
                else
                {
                    await TextEditorBelDataAccess.CreateNewFileAsync(file);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }        
    }
}
