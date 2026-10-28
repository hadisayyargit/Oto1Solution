using oto1.Models;

namespace oto1;

public partial class CascadeMenuContentView : ContentView
{
    private readonly MenuViewModel _menuViewModel;
    public CascadeMenuContentView()
	{
		InitializeComponent();
        // به صورت دستی نمونه‌سازی می‌کنیم (Manual Instantiation)
        _menuViewModel = new MenuViewModel();

        // تنظیم BindingContext
        BindingContext = _menuViewModel;
    }
}