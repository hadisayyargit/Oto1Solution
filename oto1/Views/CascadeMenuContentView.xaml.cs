using oto1.Models;
using System.Windows.Input;

namespace oto1;

public partial class CascadeMenuContentView : ContentView
{
    public static readonly BindableProperty ItemTappedCommandProperty =
        BindableProperty.Create(nameof(ItemTappedCommand), typeof(ICommand), typeof(CascadeMenuContentView), null);

    public ICommand ItemTappedCommand
    {
        get => (ICommand)GetValue(ItemTappedCommandProperty);
        set => SetValue(ItemTappedCommandProperty, value);
    }

    public CascadeMenuContentView()
    {
        InitializeComponent();
    }
}
