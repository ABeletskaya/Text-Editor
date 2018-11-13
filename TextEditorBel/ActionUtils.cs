using System;
using System.Windows.Forms;
using TextEditorBelLibrary;

namespace TextEditorBel
{
    public class ActionUtils
    {
        public static string fileName { get; private set; }
        public static FileModel file { get; private set; }

        public static bool OpenFile()
        {
            fileName = "";          
            OpenFileForm openDialog = new OpenFileForm();
            openDialog.ShowDialog();

            if (openDialog.isPassName)
            {
                fileName = openDialog.fileName;
                file =  TextEditorBelDataAccess.LoadFile(fileName);
                return true;
            }

            openDialog.Dispose();

            return false;
        }

        public static void SaveCurrentFile(string name, string data)
        {
            var res = MessageBox.Show("Do you want to save changes into current file? "
                          , "Save changes", MessageBoxButtons.YesNo
                          , MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (String.IsNullOrEmpty(name))
                {
                    SaveFile(false, data);
                }

                else
                {
                    file.Name = name;
                    file.Data = data;
                    TextEditorBelDataAccess.SaveExistFile(file);
                    MessageBox.Show("File has been saved");
                }
            }
        }

        public static void SaveFile(bool isExist, string data)
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
                    TextEditorBelDataAccess.SaveExistFile(file);
                }
                else
                {
                    TextEditorBelDataAccess.CreateNewFile(file);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
