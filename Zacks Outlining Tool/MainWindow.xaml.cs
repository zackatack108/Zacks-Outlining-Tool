using Zacks_Outlining_Tool.ViewModels;
using System.Windows;

namespace Zacks_Outlining_Tool
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.DataContext = new MainPageViewModel();
            InitializeComponent();
        }
    }
}
