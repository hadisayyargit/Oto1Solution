using Microsoft.Maui.Controls;
using oto1.Models;

namespace oto1; // (یا oto1.Views بسته به فضای نام پروژه شما)

public partial class CascadeMenuContentView : ContentView
{
    // تعریف BindableProperty برای ViewModel تا کامپایلر و XAML آن را بشناسند
    public static readonly BindableProperty ViewModelProperty =
        BindableProperty.Create(
            nameof(ViewModel),
            typeof(MenuViewModel),
            typeof(CascadeMenuContentView),
            null,
            propertyChanged: OnViewModelChanged);

    public MenuViewModel ViewModel
    {
        get => (MenuViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public CascadeMenuContentView()
    {
        InitializeComponent();

        // اگر ویومدل از بیرون پاس داده نشود، یک نمونه پیش‌فرض می‌سازد تا BindingContext خالی نماند
        if (ViewModel == null)
        {
            ViewModel = new MenuViewModel();
            BindingContext = ViewModel;
        }
    }

    private static void OnViewModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CascadeMenuContentView control && newValue is MenuViewModel vm)
        {
            control.BindingContext = vm;
        }
    }
}
