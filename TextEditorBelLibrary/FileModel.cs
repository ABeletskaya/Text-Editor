using System;

namespace TextEditorBelLibrary
{
    public class FileModel
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
