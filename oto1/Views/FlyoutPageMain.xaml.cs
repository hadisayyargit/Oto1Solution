using oto1.Models;

namespace oto1;

public partial class FlyoutPageMain : FlyoutPage
{
	public FlyoutPageMain()
	{
		InitializeComponent();

        flyoutPage.collectionViewMenu.SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItemModel;
        if (item != null)
        {
            if (item.MenuName == "mnuHome")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                IsPresented = false ;
            }
            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                IsPresented = false;
            }
        }
    }

}