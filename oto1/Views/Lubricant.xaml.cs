using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using oto1.Models;

namespace oto1 {

    public partial class Lubricant : ContentPage
    {
        private readonly MenuViewModel _menuViewModel;


        public Lubricant()
        {
            InitializeComponent();
            _menuViewModel = new MenuViewModel();

            // تنظیم BindingContext
            BindingContext = _menuViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (BindingContext is MenuViewModel vm)
            {
                LoadSampleMenu(vm);
            }
        }

        private void LoadSampleMenu(MenuViewModel vm)
        {
            // همان منطق قبلی را اینجا کپی می‌کنیم اما داده‌ها را به vm می‌دهیم
            vm.MenuItems.Clear();

            // ۱. پاک کردن فیلتر
            vm.MenuItems.Add(new MenuItemModel { Title = "پاک کردن فیلتر", PartLevel1Id = -1, PartLevel2Id = -1, PartLevel3Id = -1 });

            // ۲. ساخت ساختار درختی (مثال)
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
