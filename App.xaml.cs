using Kodszerkeszto.Models;
using Kodszerkeszto.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;

namespace Kodszerkeszto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private EditorViewModel _viewModel;
        private MainWindow _window;
        private EditorModel _model;
        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
        }
        void App_Startup(object sender, StartupEventArgs eventArgs)
        {
            _viewModel = new EditorViewModel();
            _window = new MainWindow();
            FilePersistence dataAccess = new FilePersistence();
            _model = new EditorModel(dataAccess);

            _model.FileOpened += new EventHandler<FileOperationEventArgs>(_viewModel.Model_FileOpened);
            _model.FileSaved += new EventHandler<FileOperationEventArgs>(_viewModel.Model_FileSaved);
            _viewModel.SaveFile += new EventHandler<FileOperationEventArgs>(ViewModel_FileSaved);
            _viewModel.OpenFile += new EventHandler<FileOperationEventArgs>(ViewModel_FileOpened);

            _window.DataContext = _viewModel;
            _window.Show();
        }
        
        public async void ViewModel_FileOpened(object sender, FileOperationEventArgs e)
        {
            Debug.WriteLine("Megnyitva");
            await _model.LoadFileAsync(e.Url);
        }
        public async void ViewModel_FileSaved(object sender, FileOperationEventArgs e)
        {
            Debug.WriteLine("Mentés");
            await _model.SaveFileAsync(e.Content, e.Url);
        }
    }
}
