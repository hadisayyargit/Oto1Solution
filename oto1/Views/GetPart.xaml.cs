using oto1.Models;
using oto1.Services;
using System;
using System.Collections.ObjectModel;

namespace oto1
{
    public partial class GetPart : ContentPage
    {
        private MenuViewModel _menuViewModel;
        private VendorModel _vendor;
        public ObservableCollection<VendorModel> FilteredVendors { get; set; } = new ObservableCollection<VendorModel>();

        ServiceController MyServiceController = new ServiceController();
        CustomerServiceController MyCustomerServiceController = new CustomerServiceController();


        List<CarModel> cars = new List<CarModel>();
        List<ManufacturerModel> manufacturers = new List<ManufacturerModel>();
        List<PartModel> products = new List<PartModel>();
        List<PartCarVendorModel> searchresult = new List<PartCarVendorModel>();

        List<VendorModel> vendors = new List<VendorModel>();
        PartModel SelectedProduct { get; set; }
        ObservableCollection<PartCarVendorModel> myItems { get; set; }

        private int _pageNumber = 1;
        private const int _pageSize = 20;
        private bool _isLoading = false;
        private bool _hasMoreData = true;

        private const int PageSize = 20;

        private bool _isLoadingPage = false;

        private Func<int, int, Task<List<PartCarVendorModel>>>? _getPartPageAsync;

        private ObservableCollection<PartCarVendorModel> displayedProducts
            = new ObservableCollection<PartCarVendorModel>();

        public GetPart()
        {
            InitializeComponent();
            collectionviewProducts.ItemsSource = displayedProducts;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_isLoading)
                return;

            _isLoading = true;
            activityIndicator.IsRunning = true;

            try
            {
                if (GlobalClass.m_CustomerServiceItemView == null)
                {
                    await DisplayAlert(
                        "خطا",
                        "اطلاعات نوع سرویس مشخص نشده است.",
                        "باشه");

                    return;
                }

                if (!NetClass.CheckNetConnection())
                {
                    await DisplayAlert(
                        "خطا",
                        "خطا در اتصال شبکه",
                        "باشه");

                    return;
                }

                _menuViewModel = new MenuViewModel();

                BindingContext = _menuViewModel;

                frameLubricant.IsVisible = false;
                frameBattery.IsVisible = false;
                frameTire.IsVisible = false;
                stacklayoutPartClass.IsVisible = false;

                switch ((byte)GlobalClass.m_CustomerServiceItemView.ServiceItemType)
                {
                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_LubricantSale:
                        frameLubricant.IsVisible = true;
                        break;

                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_PartSale:
                        stacklayoutPartClass.IsVisible = true;
                        break;

                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_BatterySale:
                        frameBattery.IsVisible = true;
                        break;

                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_tiresale:
                        frameTire.IsVisible = true;
                        break;
                }

                // حتماً با await اجرا شود
                await LoadMenuAsync(_menuViewModel);

                cars = await MyServiceController.GetAllCarsView()
                       ?? new List<CarModel>();

                manufacturers = await MyServiceController.GetAllManufacturers()
                               ?? new List<ManufacturerModel>();

                products = await MyServiceController.GetAllParts()
                           ?? new List<PartModel>();

                vendors = await MyServiceController.GetAllVendors()
                          ?? new List<VendorModel>();

                await LoadVendorsAsync();

                pickerCar.ItemsSource = cars;
                pickerManufacturer.ItemsSource = manufacturers;

                cvVendorResults.ItemsSource = FilteredVendors;

                SelectedProduct = new PartModel { PName = "" };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                await DisplayAlert(
                    "خطای GetPart",
                    ex.Message,
                    "باشه");
            }
            finally
            {
                activityIndicator.IsRunning = false;
                _isLoading = false;
            }
        }

        private async Task LoadMenuAsync(MenuViewModel vm)
        {
            List<PartLevelModel> partlevels1 = new List<PartLevelModel>();
            List<PartLevelModel> partlevels2 = new List<PartLevelModel>();
            List<PartLevelModel> partlevels3 = new List<PartLevelModel>();

            vm.MenuItems.Clear();

            var rootCategory = new MenuItemModel { Title = "دسته‌بندی قطعات", PartLevel1Id = -1, PartLevel2Id = -1, PartLevel3Id = -1 };

            partlevels1 = await MyServiceController.GetPartLevel(-1, -1, -1);
            foreach (PartLevelModel pl1 in partlevels1)
            {
                var node1 = new MenuItemModel { Title = pl1.PName, PartLevel1Id = pl1.PartLevel1Id, PartLevel2Id = pl1.PartLevel2Id, PartLevel3Id = pl1.PartLevel3Id };


                partlevels2 = await MyServiceController.GetPartLevel(pl1.PartLevel1Id, -1, -1);
                foreach (PartLevelModel pl2 in partlevels2)
                {
                    var node2 = new MenuItemModel { Title = pl2.PName, PartLevel1Id = pl2.PartLevel1Id, PartLevel2Id = pl2.PartLevel2Id, PartLevel3Id = pl2.PartLevel3Id };
                    node1.Children.Add(node2);

                    partlevels3 = await MyServiceController.GetPartLevel(pl2.PartLevel1Id, pl2.PartLevel2Id, -1);
                    foreach (PartLevelModel pl3 in partlevels3)
                    {
                        var node3 = new MenuItemModel { Title = pl3.PName, PartLevel1Id = pl3.PartLevel1Id, PartLevel2Id = pl3.PartLevel2Id, PartLevel3Id = pl3.PartLevel3Id };
                        node2.Children.Add(node3);
                    }
                }

                rootCategory.Children.Add(node1);
            }

            vm.MenuItems.Add(rootCategory);
        }



        private void pickerCar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pickerManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {

            pickerManufacturer.SelectedIndex = -1;
            pickerCar.SelectedIndex = -1;
            txtPartName.Text = string.Empty;
            txtPerformanceLevel.Text = "";
            txtViscosity.Text = string.Empty;
            txtVoltage.Text = string.Empty;
            txtAmperage.Text = string.Empty;
            txtPartCode.Text = string.Empty;
            txtAttribute.Text = string.Empty;
            txtTireRingSize.Text = string.Empty;
            txtTireYear.Text = string.Empty;
            txtTireWheels.Text = string.Empty;

            displayedProducts.Clear();

            _getPartPageAsync = null;
            _pageNumber = 1;
            _hasMoreData = false;

            FilteredVendors.Clear();
            _vendor = null;
        }

        private async void btnSearch_Clicked(object sender, EventArgs e)
        {
            int PartId = -1, manufacturerid = -1, carid = -1, vendorid = -1, partlevel1id = -1, partlevel2id = -1, partlevel3id = -1, batteryamperage = -1, batteryvoltage = -1, tireyear = -1;
            string partname = "", partcode = "", viscosity = "", performancelevel = "", tireringsize = "", tirewheels = "", partcolor = "", partattribute = "";

            activityIndicator.IsRunning = true;

            /*
                       
            if (pickerProduct.SelectedIndex >= 0)
            {
                PartId = ((PartModel)pickerProduct.ItemsSource[pickerProduct.SelectedIndex]).PartId;
                //DisplayAlert("", picker.Items[pickerProduct.SelectedIndex], "ok");
                //DisplayAlert("", ((ProductModel)pickerProduct.ItemsSource[pickerProduct.SelectedIndex]).PartId.ToString(), "ok");
            }
            */

            if (pickerManufacturer.SelectedIndex >= 0)
            {
                manufacturerid = ((ManufacturerModel)pickerManufacturer.ItemsSource[pickerManufacturer.SelectedIndex]).ManufacturerId;
            }
            if (pickerCar.SelectedIndex >= 0)
            {
                carid = ((CarModel)pickerCar.ItemsSource[pickerCar.SelectedIndex]).CarId;

            }

            if (_vendor != null)
                vendorid = _vendor.VendorId;

            var selectedItem = _menuViewModel.MenuItems
        .SelectMany(FindAllNodes)
        .FirstOrDefault(x => x.GetFullPath(_menuViewModel.MenuItems) == _menuViewModel.SelectedMenuTitle);

            if (_menuViewModel.SelectedMenuItem != null)
            {
                partlevel1id = _menuViewModel.SelectedMenuItem.PartLevel1Id ?? -1;
                partlevel2id = _menuViewModel.SelectedMenuItem.PartLevel2Id ?? -1;
                partlevel3id = _menuViewModel.SelectedMenuItem.PartLevel3Id ?? -1;
            }

            partname = txtPartName.Text;
            partcode = txtPartCode.Text;
            viscosity = txtViscosity.Text;
            performancelevel = txtPerformanceLevel.Text;

            batteryamperage = int.TryParse(txtAmperage.Text, out int result) ? result : -1;
            batteryvoltage = int.TryParse(txtVoltage.Text, out int result1) ? result1 : -1;
            tireyear = int.TryParse(txtTireYear.Text, out int result2) ? result2 : -1;

            tireringsize = txtTireRingSize.Text;
            tirewheels = txtTireWheels.Text;
            //partcolor = txtColor.Text;
            partattribute = txtAttribute.Text;



            switch (GlobalClass.m_CustomerServiceItemView.ServiceItemType)
            {
                case (byte)GlobalClass.enumServiceItemType.serviceitemtype_LubricantSale:
                    partlevel1id = 7;
                    partlevel2id = 73;
                    break;
                case (byte)GlobalClass.enumServiceItemType.serviceitemtype_PartSale:
                    break;
                case (byte)GlobalClass.enumServiceItemType.serviceitemtype_BatterySale:
                    partlevel1id = 5;
                    partlevel2id = 51;
                    partlevel3id = 510;
                    break;
                case (byte)GlobalClass.enumServiceItemType.serviceitemtype_tiresale:
                    partlevel1id = 4;
                    partlevel2id = 42;
                    partlevel3id = 421;
                    break;
            }

            searchresult = await MyServiceController.GetPart(PartId, manufacturerid, carid, vendorid, partlevel1id, partlevel2id, partlevel3id, partname, partcode, viscosity, performancelevel, batteryamperage, batteryvoltage, tireyear, tireringsize, tirewheels, partcolor, partattribute);

            /*
            if (searchresult != null && searchresult.Count > 0)
            {
                foreach (var pr in searchresult)
                {

                    //pr.PhotoFileName = "p" + pr.PartId.ToString() + ".png";
                    pr.PhotoFileName = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + pr.PartId.ToString() + ".png";

                }
            }


            collectionviewProducts.ItemsSource = searchresult;
            activityIndicator.IsRunning = false;
            */

            // ذخیره فیلترهای جستجوی فعلی برای صفحات بعدی
            _getPartPageAsync = (pageNumber, pageSize) =>
                MyServiceController.GetPart(
                    partid: PartId,
                    manufacturerid: manufacturerid,
                    carid: carid,
                    vendorid: vendorid,
                    partlevel1id: partlevel1id,
                    partlevel2id: partlevel2id,
                    partlevel3id: partlevel3id,
                    partname: partname,
                    partcode: partcode,
                    viscosity: viscosity,
                    performancelevel: performancelevel,
                    batteryamperage: batteryamperage,
                    batteryvoltage: batteryvoltage,
                    tireyear: tireyear,
                    tireringsize: tireringsize,
                    tirewheels: tirewheels,
                    partcolor: partcolor,
                    partattribute: partattribute,
                    pagenumber: pageNumber,
                    pagesize: pageSize
                );

            // شروع جستجوی جدید
            _pageNumber = 1;
            _hasMoreData = true;

            displayedProducts.Clear();

            await LoadNextPageAsync();

        }

        private async void btnAddToBasket_Clicked(object sender, EventArgs e)
        {

            PartCarVendorModel mm = new PartCarVendorModel();
            List<CustomerServiceItemViewModel> myServices = new List<CustomerServiceItemViewModel>();

            string res = "";
            string msg = "";


            var allItems = collectionviewProducts.ItemsSource;

            if (allItems != null)
            {
                foreach (PartCarVendorModel item in allItems)
                {
                    if (item.Quantity > 0)
                    {
                        myServices.Add(new CustomerServiceItemViewModel() { CustomerId = GlobalClass.m_CustomerId, PartId = item.PartId, Quantity = item.Quantity, ServiceType = GlobalClass.m_CustomerServiceItemView.ServiceType, ServiceItemType = GlobalClass.m_CustomerServiceItemView.ServiceItemType, ServiceItemStatus = (byte)GlobalClass.enumServiceItemStatus.serviceitemstatus_Request, NetAmount = item.NetAmount, PriceAmount = item.PriceAmount, VendorId = item.VendorId, Latitude = GlobalClass.m_Latitude, Longitude = GlobalClass.m_Longitude, Address = GlobalClass.m_Address });
                    }
                }


                res = await MyCustomerServiceController.AddCustomerService(myServices);
                msg = "درخواست شما ثبت شد با شماره‌های: " + res;
                await DisplayAlert("", msg, "ok");
            }

        }

        private void pickerVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private async void btnBeforeStep_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
        // متد کمکی برای پیدا کردن تمام گره‌ها در درخت (برای جستجوی سریع‌تر)
        private IEnumerable<MenuItemModel> FindAllNodes(MenuItemModel node)
        {
            yield return node;
            foreach (var child in node.Children)
            {
                foreach (var descendant in FindAllNodes(child))
                {
                    yield return descendant;
                }
            }
        }


        public async Task LoadVendorsAsync()
        {
            FilteredVendors.Clear();
            foreach (var item in vendors)
                FilteredVendors.Add(item);

        }

        private void OnVendorSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                FilteredVendors.Clear();
                foreach (var item in vendors) FilteredVendors.Add(item);
                cvVendorResults.IsVisible = false;
            }
            else
            {
                var filtered = vendors
                    .Where(x => x.PName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredVendors.Clear();
                foreach (var item in filtered) FilteredVendors.Add(item);

                cvVendorResults.IsVisible = filtered.Any();
            }
        }

        private void OnVendorSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is VendorModel selectedVendor)
            {
                _vendor = new VendorModel();
                _vendor.VendorId = selectedVendor.VendorId;
                _vendor.PName = selectedVendor.PName;

                searchbarVendor.Text = selectedVendor.PName; // نمایش نام در سرچ‌بار

                cvVendorResults.IsVisible = false; // بستن لیست
                cvVendorResults.SelectedItem = null; // ریست کردن انتخاب
            }
        }


        private async Task LoadNextPageAsync()
        {
            if (_isLoadingPage || !_hasMoreData || _getPartPageAsync == null)
                return;

            _isLoadingPage = true;
            activityIndicator.IsRunning = true;

            try
            {
                var pageItems = await _getPartPageAsync(_pageNumber, PageSize);

                if (pageItems == null || pageItems.Count == 0)
                {
                    _hasMoreData = false;
                    return;
                }

                foreach (var pr in pageItems)
                {
                    pr.PhotoFileName =
                        $"https://khordadnet.ir/mysites/oto1/assets/part/prt{pr.PartId}.png";

                    displayedProducts.Add(pr);
                }

                _pageNumber++;

                // اگر تعداد رکوردها کمتر از PageSize باشد، صفحه آخر است
                if (pageItems.Count < PageSize)
                {
                    _hasMoreData = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("خطا", ex.Message, "OK");
            }
            finally
            {
                _isLoadingPage = false;
                activityIndicator.IsRunning = false;
            }
        }


        private async void collectionviewProducts_RemainingItemsThresholdReached(    object sender, EventArgs e)
        {
            await LoadNextPageAsync();
        }



    }
}
