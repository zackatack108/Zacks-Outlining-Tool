using System.Windows;
using Zacks_Outlining_Tool.ViewModels;
namespace Zacks_Outlining_Tool;
public partial class MainWindow : Window
{

    public MainWindow()
    {
        DataContext = new MainPageViewModel();
        InitializeComponent();
    }
}
