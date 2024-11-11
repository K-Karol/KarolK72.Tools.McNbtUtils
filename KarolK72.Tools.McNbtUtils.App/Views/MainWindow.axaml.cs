using Avalonia.Controls;
using Avalonia.ReactiveUI;
using KarolK72.Tools.McNbtUtils.App.ViewModels;

namespace KarolK72.Tools.McNbtUtils.App.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}