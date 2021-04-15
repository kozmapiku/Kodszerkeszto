using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Kodszerkeszto.Models
{
    public class EditorModel
    {
        private String _currentText;
        private String _url;
        private String _name;

        private FilePersistence _dataAccess;

        public event EventHandler<FileOperationEventArgs> FileOpened;
        public event EventHandler<FileOperationEventArgs> FileSaved;

        public EditorModel(FilePersistence dataAccess)
        {
            _url = null;
            _dataAccess = dataAccess;
        }

        public String CurrentText { get => _currentText; set => _currentText = value; }
        public String Url { get => _url; set => _url = value; }
        public String Name { get => _name; set => _name = value; }
        public Boolean UrlIsNull()
        {
            return _url == null;
        }
        public async Task SaveFileAsync(String content, String url = "")
        {
            if(_dataAccess == null)
            {
                throw new InvalidOperationException("No data access is provided.");
            }
            if (UrlIsNull())
            {
                Debug.WriteLine("Megy");
                await _dataAccess.SaveAsync(content);
                if (FileSaved != null)
                    FileSaved(this, new FileOperationEventArgs(url, content));
            }
            else
            {
                Debug.WriteLine("Megy2");
                await _dataAccess.SaveAsync(content, url);
                if (FileSaved != null)
                    FileSaved(this, new FileOperationEventArgs(url, content));
            }
            
        }
        public async Task LoadFileAsync(String url)
        {
            if (_dataAccess == null)
            {
                throw new InvalidOperationException("No data access is provided.");
            }
            CurrentText = await _dataAccess.LoadAsync(url);
            Debug.WriteLine("Fájl: " + CurrentText);
            if (FileOpened != null)
                FileOpened(this, new FileOperationEventArgs(url, _currentText));
        }
    }
}
