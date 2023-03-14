using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Zacks_Outlining_Tool.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Zacks_Outlining_Tool.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private string origin = "";

        [ObservableProperty]
        private bool connectPoints = false;

        public ObservableCollection<string> Output { get; set; } = new();

        public string Origin
        {
            get => origin;
            set
            {
                SetProperty(ref origin, value);
                CalculatPointCommand.NotifyCanExecuteChanged();
            }
        }
        private bool CanCalculate()
        {
            if(Origin != "")
            {
                return true;
            }
            return false;
        }

        [RelayCommand(CanExecute = nameof(CanCalculate))]
        public async Task CalculatPoint()
        {
            CalculatePoint point = new CalculatePoint();
            var pointData = await point.FindPoint();
            Output.Add($"Ruler Values: {pointData}");
            var mc = new MinecraftCommands();
            var command = await mc.SendPointCommand(Origin, pointData);
            Output.Add($"Command ran: {command}");
            if (ConnectPoints == true)
            {
                var connect = await mc.ConnectPoints();
                Output.Add($"Command ran: {connect}");
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
