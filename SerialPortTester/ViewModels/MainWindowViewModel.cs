using Prism.Mvvm;

namespace SerialPortTester.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get => this._title;
            set => SetProperty(ref this._title, value);
        }

        public MainWindowViewModel()
        {

        }
    }
}
