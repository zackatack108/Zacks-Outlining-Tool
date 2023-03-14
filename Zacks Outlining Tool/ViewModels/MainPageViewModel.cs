using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Zacks_Outlining_Tool.Utilities;

namespace Zacks_Outlining_Tool.ViewModels;

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
        try
        {
            CalculatePoint point = new CalculatePoint();
            var pointData = await point.FindPoint().WithTimeout(TimeSpan.FromSeconds(30));
            Output.Add($"Ruler Values: {pointData}");

            var mc = new MinecraftCommands();
            var command = await mc.SendPointCommand(Origin, pointData).WithTimeout(TimeSpan.FromSeconds(10));
            Output.Add($"Command ran: {command}");

            if (ConnectPoints == true)
            {
                var connect = await mc.ConnectPoints().WithTimeout(TimeSpan.FromSeconds(10));
                Output.Add($"Command ran: {connect}");
            }

        } catch(Exception ex)
        {
            Output.Add(ex.ToString());
        }
    }

}
