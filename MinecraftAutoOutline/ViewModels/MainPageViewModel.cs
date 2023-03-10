using CommunityToolkit.Mvvm.Input;
using Zacks_Outlining_Tool.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Zacks_Outlining_Tool.ViewModels
{
    public partial class MainPageViewModel : INotifyPropertyChanged
    {
        public RelayCommand<string> RadioButtonCommand { get; }

        public MainPageViewModel()
        {
            PointCommand = new RelayCommand(ExecutePoint, CanExecutePoint);
            RadioButtonCommand = new RelayCommand<string>(radioButtonClick);
        }

        public ICommand PointCommand { get; set; }

        private bool CanExecutePoint(object parameter)
        {
            if (Origin != "")
            {
                return true;
            }
            return false;
        }

        private void ExecutePoint(object parameter)
        {
            CalculatePoint point = new CalculatePoint();
            var pointData = point.FindPoint();
            Output.Add($"Ruler Values: {pointData}");
            var mc = new MinecraftCommands();
            var command = mc.SendPointCommand(Origin, pointData);
            Output.Add($"Command ran: {command}");
            if (connectPoints == true)
            {
                var connect = mc.ConnectPoints();
                Output.Add($"Command ran: {command}");
            }
        }

        private string origin = "";
        public string Origin
        {
            get { return origin; }
            set
            {
                if (value == origin)
                    return;
                origin = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> output = new ObservableCollection<string>();
        public ObservableCollection<string> Output
        {
            get { return output; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool connectPoints = false;

        private void radioButtonClick(string name)
        {
            if (name.ToLower() == "yes")
                connectPoints = true;
            else
                connectPoints = false;
        }

    }
}
