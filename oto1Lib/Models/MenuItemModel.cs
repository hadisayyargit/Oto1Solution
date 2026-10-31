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

    // متدی برای ساختن مسیر (مثلاً: تعلیق/چرخ/لاستیک)
    public string GetFullPath(IEnumerable<MenuItemModel> rootNodes)
    {
        var path = new List<string> { Title };
        var parent = FindParent(rootNodes, this);

        while (parent != null)
        {
            path.Insert(0, parent.Title); // نام پدر را به ابتدای لیست اضافه کن
            parent = FindParent(rootNodes, parent); // برو به عقب‌تر (پدرِ پدر)
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

    public MenuViewModel()
    {
        //LoadSampleMenu();
    }

    /// <summary>
    /// منطق اصلی Accordion: بستن بقیه و باز کردن آیتم انتخاب شده
    /// </summary>
    private void ToggleExpand(MenuItemModel item)
    {
        if (item == null || !item.HasChildren)
            return;

        bool willOpen = !item.IsExpanded;

        if (willOpen)
        {
            // اگر قرار است باز شود، ابتدا همه خواهر و برادرها (در همان سطح) را ببند
            CloseSiblings(item);
        }

        item.IsExpanded = willOpen;
    }

    private void CloseSiblings(MenuItemModel item)
    {
        // پیدا کردن والد در کل درخت برای دسترسی به خواهر و برادرها
        MenuItemModel parent = FindParent(MenuItems, item);

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
            // اگر والد نداشت، یعنی در سطح ریشه است
            foreach (var sibling in MenuItems)
            {
                if (sibling != item)
                    CollapseRecursively(sibling);
            }
        }
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


    [RelayCommand]
    private async Task ItemTapped(MenuItemModel clickedItem)
    {
        if (clickedItem == null) return;

        if (clickedItem.HasChildren)
        {
            // استفاده از منطق Accordion به جای تغییر مستقیم
            ToggleExpand(clickedItem);
            SelectedMenuTitle = clickedItem.GetFullPath(MenuItems);
        }
        else
        {
            CollapseAllMenus(MenuItems);
            // اجرای عملیات برای برگ‌ها (Leaf Nodes)
            SelectedMenuTitle = clickedItem.GetFullPath(MenuItems);
            //if (clickedItem.Title == "پاک کردن فیلتر")
            //{
            //    await Shell.Current.DisplayAlert("سیستم", "فیلترها پاک شدند.", "OK");
            //}
            //else
            //{
            await Shell.Current.DisplayAlert("انتخاب قطعه",
                    $"عنوان: {clickedItem.Title}\nL1: {clickedItem.PartLevel1Id} | L2: {clickedItem.PartLevel2Id} | L3: {clickedItem.PartLevel3Id}",
                    "OK");

        }
    }




    [RelayCommand]
    private void ClearFilter()
    {
        CollapseAllMenus(MenuItems);
        SelectedMenuTitle = string.Empty;

        Shell.Current.DisplayAlert("سیستم", "فیلترها پاک شدند.", "OK");
    }



    /// <summary>
    /// پیمایش بازگشتی برای بستن تمام منوها
    /// </summary>
    private void CollapseAllMenus(IEnumerable<MenuItemModel> nodes)
    {
        foreach (var node in nodes)
        {
            // اگر منو فرزند داشت، اول فرزندان را می‌بندیم (برای اطمینان از بستن کامل)
            if (node.HasChildren)
            {
                CollapseAllMenus(node.Children);
            }

            // بستن خودِ گره فعلی
            node.IsExpanded = false;
        }
    }

    private void CollapseRecursively(MenuItemModel item)
    {
        item.IsExpanded = false;
        foreach (var child in item.Children)
            CollapseRecursively(child);
    }
}