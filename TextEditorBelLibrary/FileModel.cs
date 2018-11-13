using System;

namespace TextEditorBelLibrary
{
    public class FileModel
    {
        public string Name { get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return Name + "     " + "Data";
        }
    }
}
