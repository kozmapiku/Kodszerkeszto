using Kodszerkeszto.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Kodszerkeszto.ViewModels
{
    public class EditorViewModel : ViewModelBase
    {
        private String _currentText;
        private String _lineNumbers;
        private Double _fontSize;
        private Boolean _isDirty;

        public DelegateCommand IncreaseFontSize { get; private set; }
        public DelegateCommand DecreaseFontSize { get; private set; }

        public DelegateCommand OpenFileCommand { get; private set; }
        public DelegateCommand SaveFileCommand { get; private set; }

        public event EventHandler<FileOperationEventArgs> OpenFile;
        public event EventHandler<FileOperationEventArgs> SaveFile;

        public Boolean IsDirty { get => _isDirty; set { _isDirty = value; } }

        public String CurrentText
        {
            get => _currentText;
            set
            {
                _currentText = String.IsNullOrEmpty(value) ? "" : value;
                int lineCount = _currentText.Count(c => c == '\n') + 1;
                var numbers = Enumerable.Range(1, lineCount);
                LineNumbers = String.Join("\n", numbers.Select(x => x.ToString()));
                IsDirty = true;
                OnPropertyChanged();
            }
        }
        public String LineNumbers
        {
            get { return _lineNumbers; }
            set 
            {
                _lineNumbers = value;
                OnPropertyChanged();
            }
        }
        public Double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }
        public EditorViewModel()
        {
            _fontSize = 12;

            IncreaseFontSize = new DelegateCommand(param =>
            {
                FontSize++;
                OnPropertyChanged();
            });
            DecreaseFontSize = new DelegateCommand(param =>
            {
                if(FontSize != 1)
                {
                    FontSize--;
                    OnPropertyChanged();
                }
            });
            OpenFileCommand = new DelegateCommand(param =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    String url = openFileDialog.FileName;
                    if (OpenFile != null)
                        OpenFile(this, new FileOperationEventArgs(url, ""));
                }
            });
            SaveFileCommand = new DelegateCommand(param =>
            {
                if(IsDirty)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        String url = saveFileDialog.FileName;
                        if (SaveFile != null)
                            SaveFile(this, new FileOperationEventArgs(url, _currentText));
                    }
                }
                
            });

        }
        public void Model_FileOpened(object sender, FileOperationEventArgs e)
        {
            CurrentText = e.Content;
            Debug.WriteLine(_currentText);
        }
        public void Model_FileSaved(object sender, FileOperationEventArgs e)
        {
            IsDirty = false;
        }

    }
}
