using System;
using System.Collections.Generic;
using System.Text;

namespace Kodszerkeszto.Models
{
    public class FileOperationEventArgs : EventArgs
    {
        private String _url;
        private String _content;

        public String Url { get => _url; }
        public String Content { get => _content; }

        public FileOperationEventArgs(String url, String content)
        {
            _url = url;
            _content = content;
        }
    }
}
