using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace oto1.Models;

public partial class MenuItemModel : ObservableObject
{
    public string Title { get; set; }
    public int? PartLevel1Id { get; set; }
    public int? PartLevel2Id { get; set; }
    public int? PartLevel3Id { get; set; }

    [ObservableProperty]
    private bool _isExpanded;

    public ObservableCollection<MenuItemModel> Children { get; set; } = new();

    public bool HasChildren => Children != null && Children.Count > 0;

    public string GetFullPath(IEnumerable<MenuItemModel> rootNodes)
    {
        var path = new List<string> { Title };
        var parent = FindParent(rootNodes, this);

        while (parent != null)
        {
            path.Insert(0, parent.Title);
            parent = FindParent(rootNodes, parent);
        }

        return string.Join("/", path);
    }

    private MenuItemModel FindParent(IEnumerable<MenuItemModel> nodes, MenuItemModel target)
    {
        foreach (var node in nodes)
        {
            if (node.Children.Contains(target))
                return node;

            var found = FindParent(node.Children, target);
            if (found != null)
                return found;
        }
        return null;
    }
}

public partial class MenuViewModel : ObservableObject
{


    public ObservableCollection<MenuItemModel> MenuItems { get; set; } = new();

    [ObservableProperty]
    private string _selectedMenuTitle;

    // به جای استفاده از [ObservableProperty] روی فیلد، آن را دستی تعریف می‌کنیم تا خطا رفع شود
    private MenuItemModel _selectedMenuItem;
    public MenuItemModel SelectedMenuItem
    {
        get => _selectedMenuItem;
        set => SetProperty(ref _selectedMenuItem, value);
    }

    public MenuViewModel()
    {
        // در اینجا داده‌ها Load می‌شوند
    }

    [RelayCommand]
    private async Task ItemTapped(MenuItemModel clickedItem)
    {
        if (clickedItem == null) return;

        // ۱. ذخیره اطلاعات انتخاب شده برای استفاده در GetPart
        SelectedMenuItem = clickedItem;
        SelectedMenuTitle = clickedItem.GetFullPath(MenuItems);

        if (clickedItem.HasChildren)
        {
            // ۲. مدیریت حالت Accordion (باز و بسته شدن)
            ToggleExpand(clickedItem);
        }
        else
        {
            // ۳. اگر کاربر روی یک "برگ" (Leaf Node) کلیک کرد
            // ابتدا همه منوها را می‌بندیم تا UI تمیز شود
            CollapseAllMenus(MenuItems);

            // نمایش اطلاعات (برای تست شما)
            await Shell.Current.DisplayAlert("انتخاب قطعه",
                    $"عنوان: {clickedItem.Title}\nL1: {clickedItem.PartLevel1Id} | L2: {clickedItem.PartLevel2Id} | L3: {clickedItem.PartLevel3Id}",
                    "OK");
        }
    }

    private void ToggleExpand(MenuItemModel item)
    {
        bool willOpen = !item.IsExpanded;

        if (willOpen)
        {
            // اگر قرار است باز شود، بقیه هم‌سطح‌ها را ببند
            CloseSiblings(item);
        }

        item.IsExpanded = willOpen;
    }

    private void CloseSiblings(MenuItemModel item)
    {
        MenuItemModel parent = FindParent(MenuItems, item);

        // اگر والد داشت، خواهر و برادرها را در سطح والد ببند
        if (parent != null)
        {
            foreach (var sibling in parent.Children)
            {
                if (sibling != item)
                    CollapseRecursively(sibling);
            }
        }
        else
        {
            // اگر در سطح ریشه است، بقیه ریشه‌ها را ببند
            foreach (var sibling in MenuItems)
            {
                if (sibling != item)
                    CollapseRecursively(sibling);
            }
        }
    }

    private void CollapseRecursively(MenuItemModel item)
    {
        item.IsExpanded = false;
        foreach (var child in item.Children)
        {
            CollapseRecursively(child);
        }
    }

    private void CollapseAllMenus(IEnumerable<MenuItemModel> nodes)
    {
        foreach (var node in nodes)
        {
            node.IsExpanded = false;
            if (node.HasChildren)
            {
                CollapseAllMenus(node.Children);
            }
        }
    }

    [RelayCommand]
    private void ClearFilter()
    {
        CollapseAllMenus(MenuItems);
        SelectedMenuTitle = string.Empty;
        SelectedMenuItem = null;

        Shell.Current.DisplayAlert("سیستم", "فیلترها پاک شدند.", "OK");
    }

    private MenuItemModel FindParent(IEnumerable<MenuItemModel> nodes, MenuItemModel target)
    {
        foreach (var node in nodes)
        {
            if (node.Children.Contains(target))
                return node;

            var found = FindParent(node.Children, target);
            if (found != null)
                return found;
        }
        return null;
    }
}
