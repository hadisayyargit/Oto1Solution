using System;
using System.Collections.ObjectModel;
using oto1.Models;

namespace oto1
{
    public partial class GetPart : ContentPage
    {
        private readonly MenuViewModel _menuViewModel;

        public GetPart()
        {
            InitializeComponent();

            // نمونه‌سازی و تنظیم BindingContext در کد پشت صفحه
            _menuViewModel = new MenuViewModel();
            BindingContext = _menuViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // بارگذاری نمونه منو هنگام ظاهر شدن صفحه
            LoadSampleMenu(_menuViewModel);
        }

        private void LoadSampleMenu(MenuViewModel vm)
        {
            vm.MenuItems.Clear();

            var rootCategory = new MenuItemModel { Title = "دسته‌بندی قطعات", PartLevel1Id = 0, PartLevel2Id = -1, PartLevel3Id = -1 };

            var engine = new MenuItemModel { Title = "پیشرانه", PartLevel1Id = 1, PartLevel2Id = -1, PartLevel3Id = -1 };
            engine.Children.Add(new MenuItemModel { Title = "پیستون", PartLevel1Id = 1, PartLevel2Id = 11, PartLevel3Id = -1 });
            rootCategory.Children.Add(engine);

            var suspension = new MenuItemModel { Title = "تعلیق و هدایت", PartLevel1Id = 4, PartLevel2Id = -1, PartLevel3Id = -1 };
            var wheel = new MenuItemModel { Title = "چرخ", PartLevel1Id = 4, PartLevel2Id = 41, PartLevel3Id = -1 };
            wheel.Children.Add(new MenuItemModel { Title = "لاستیک", PartLevel1Id = 4, PartLevel2Id = 41, PartLevel3Id = 411 });
            suspension.Children.Add(wheel);
            rootCategory.Children.Add(suspension);

            vm.MenuItems.Add(rootCategory);
        }
    }
}
