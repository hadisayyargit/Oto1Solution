using CommunityToolkit.Maui.Views;
using MauiPersianToolkit;
using Microsoft.Maui.Controls;
using oto1.Models;
using oto1.Services;
using System.Collections.ObjectModel;

namespace oto1;

public partial class BasketItemsPopup : Popup
{


    public long ServiceId { get; set; }
    public CustomerServiceController myCustomerServiceController { get; set; }
    List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();
    ServiceController MyServiceController = new ServiceController();

    public List<PersonModel> allcouriers { get; set; } = new List<PersonModel>();

    private PersonModel _courier;

    // لیست جدید برای نمایش در CollectionView
    public ObservableCollection<PersonModel> FilteredCouriers { get; set; } = new ObservableCollection<PersonModel>();

    public BasketItemsPopup(long serviceid)
    {
        InitializeComponent();

        this.ServiceId = serviceid;
        RefreshForm();
    }

    private async void RefreshForm()
    {
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        //pickerCourier.ItemsSource = persons;


        ServiceController MyServiceController = new ServiceController();


        cvCourierResults.ItemsSource = FilteredCouriers;

        this.myCustomerServiceController = new CustomerServiceController();

        List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
        List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();

        int vendorid = GlobalClass.m_VendorId;

        List<CustomerServiceItemViewModel> myOrdersItems = new List<CustomerServiceItemViewModel>();
        myOrdersItems = await myCustomerServiceController.GetCustomerServiceItem(this.ServiceId);
        if (myOrdersItems[0].ServiceStatus == (byte)GlobalClass.enumServiceStatus.servicestatus_Payment ||
            myOrdersItems[0].ServiceStatus == (byte)GlobalClass.enumServiceStatus.servicestatus_Pending)
        {
            btnSend.IsVisible = true;
            btnRefused.IsVisible = true;
            await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Pending);
        }
        else
        {
            btnSend.IsVisible = false;
            btnRefused.IsVisible = false;
        }

        foreach (var item in myOrdersItems)
        {
            item.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + item.PartId.ToString() + ".png";
        }

        collectionviewBasket.ItemsSource = myOrdersItems;


        //myCustomerServiceController.GetCustomerService

        await LoadCouriersAsync();


        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }


    private void TapNavigateBack_Tapped(object sender, TappedEventArgs e)
    {
        btnCancel_Clicked(this, null);
    }


    private async void btnSend_Clicked(object sender, EventArgs e)
    {
        bool answer = await Shell.Current.DisplayAlert("هشدار", "سفارش ارسال شود؟", "بله", "خیر");

        if (answer)
        {
            if (string.IsNullOrEmpty(sbCourier.Text))
            {
                await Shell.Current.DisplayAlert("تایید", "لطفا پیک را از لیست انتخاب کن", "قبول");
                return;
            }



            var servicesitems = collectionviewBasket.ItemsSource;
            this.myCustomerServiceController = new CustomerServiceController();

            try
            {
                List<CustomerServiceViewModel> mycustomerserviceviewmodellist = await myCustomerServiceController.GetCustomerService(this.ServiceId);
                CustomerServiceViewModel myCustomerService = mycustomerserviceviewmodellist.FirstOrDefault();

                //await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Refusal);


                CustomerServiceModel newcustomerServiceModel = new CustomerServiceModel()
                {
                    ServiceId = this.ServiceId,
                    CustomerId = myCustomerService.CustomerId,
                    ServiceType = myCustomerService.ServiceType,
                    TicketDateTime = myCustomerService.TicketDateTime,
                    DiscountCode = myCustomerService.DiscountCode,
                    DiscountAmount = myCustomerService.DiscountAmount,
                    ExtraAmount = myCustomerService.ExtraAmount,
                    ShippingAmount = myCustomerService.ShippingAmount,
                    TotalAmount = myCustomerService.TotalAmount,
                    ServiceDateTime = DateTime.Now,
                    VendorId = myCustomerService.VendorId,
                    Address = myCustomerService.Address,
                    Latitude = myCustomerService.Latitude,
                    Longitude = myCustomerService.Longitude,
                    ServicesDesc = txtRemark.Text,
                    ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Sending
                    ,
                    PostmanId = _courier.PersonId
                    //PostmanId =  ((PersonModel)pickerCourier.ItemsSource[pickerCourier.SelectedIndex]).PersonId

                };

                bool res = await myCustomerServiceController.UpdateCustomerService(newcustomerServiceModel);

                foreach (CustomerServiceItemViewModel serviceitem in servicesitems)
                {

                    try
                    {
                        MyServiceController = new ServiceController();
                        await MyServiceController.UpdateVendorPartExistance(serviceitem.VendorId.Value, serviceitem.PartId, -serviceitem.Quantity);

                    }
                    catch (Exception ex)
                    {
                    }
                }


                await Shell.Current.DisplayAlert("تایید", "سفارش به پیک سپرده شد", "قبول");
                /*
                foreach (CustomerServiceItemViewModel serviceitem in servicesitems)
                {

                    try
                    {
                        if (serviceitem.IsCheckBoxChecked)
                        {
                            await myCustomerServiceController.UpdateCustomerServiceItemStatus(serviceitem.ServiceItemId, (byte)GlobalClass.enumServiceItemStatus.serviceitemstatus_Done);

                        }
                        else
                        {
                            await myCustomerServiceController.UpdateCustomerServiceItemStatus(serviceitem.ServiceItemId, (byte)GlobalClass.enumServiceItemStatus.serviceitemstatus_Noexist);
                        }
                    }
                    catch
                    {

                    }
                }

                await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Sending);
                /// finding Nearset and available postman
                int postmanid = 4;
                await myCustomerServiceController.UpdateCustomerServicePostman(this.ServiceId, postmanid);
                */

            }
            catch (Exception ex)
            {
            }
            btnCancel_Clicked(this, null);
        }
    }


    private async void btnRefused_Clicked(object sender, EventArgs e)
    {

        bool answer = await Shell.Current.DisplayAlert("هشدار", "سفارش رد شود؟", "بله", "خیر");

        if (answer)
        {
            if (txtRemark.Text == "" || txtRemark.Text == null)
            {
                await Shell.Current.DisplayAlert("تایید", "لطفا توضیحات را وارد کن", "قبول");
            }
            else
            {
                try
                {
                    List<CustomerServiceViewModel> mycustomerserviceviewmodellist = await myCustomerServiceController.GetCustomerService(this.ServiceId);
                    CustomerServiceViewModel myCustomerService = mycustomerserviceviewmodellist.FirstOrDefault();

                    //await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Refusal);


                    CustomerServiceModel newcustomerServiceModel = new CustomerServiceModel()
                    {
                        ServiceId = this.ServiceId,
                        CustomerId = myCustomerService.CustomerId,
                        ServiceType = myCustomerService.ServiceType,
                        TicketDateTime = myCustomerService.TicketDateTime,
                        DiscountCode = myCustomerService.DiscountCode,
                        DiscountAmount = myCustomerService.DiscountAmount,
                        ExtraAmount = myCustomerService.ExtraAmount,
                        ShippingAmount = myCustomerService.ShippingAmount,
                        TotalAmount = myCustomerService.TotalAmount,
                        ServiceDateTime = DateTime.Now,
                        VendorId = myCustomerService.VendorId,
                        Address = myCustomerService.Address,
                        Latitude = myCustomerService.Latitude,
                        Longitude = myCustomerService.Longitude,
                        ServicesDesc = txtRemark.Text,
                        ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Refusal
                    };

                    bool res = await myCustomerServiceController.UpdateCustomerService(newcustomerServiceModel);

                    await Shell.Current.DisplayAlert("تایید", "سفارش رد شد", "قبول");
                    btnCancel_Clicked(this, null);
                }

                catch
                {

                }
            }
        }


    }


    public async Task LoadCouriersAsync()
    {
        allcouriers = await MyServiceController.GetAllPersonsByUserRole((byte)GlobalClass.enumUserRole.userrole_postman, GlobalClass.m_CurrentAddress.CityId.Value);



        // مقداردهی اولیه لیست فیلتر شده
        FilteredCouriers.Clear();
        foreach (var item in allcouriers)
            FilteredCouriers.Add(item);


        if (_courier != null)
        {
            var selectedCourier = allcouriers.FirstOrDefault(x => x.PersonId == _courier.PersonId);
            if (selectedCourier != null)
            {
                _courier.PersonId = selectedCourier.PersonId;
                //_courier.PName = selectedCourier.PName;
                sbCourier.Text = selectedCourier.FullName; // نمایش نام در سرچ‌بار
            }
        }
    }

    private void OnCourierSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            FilteredCouriers.Clear();
            foreach (var item in allcouriers) FilteredCouriers.Add(item);
            cvCourierResults.IsVisible = false;
        }
        else
        {
            var filtered = allcouriers
                .Where(x => x.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredCouriers.Clear();
            foreach (var item in filtered) FilteredCouriers.Add(item);

            cvCourierResults.IsVisible = filtered.Any();
        }
    }

    // وقتی کاربر از لیست انتخاب می‌کند
    private void OnCourierSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is PersonModel selectedCourier)
        {
            _courier = new PersonModel();

            _courier.PersonId = selectedCourier.PersonId;
            //_courier.FullName = selectedCourier.FullName;

            sbCourier.Text = selectedCourier.FullName;

            cvCourierResults.IsVisible = false;
            cvCourierResults.SelectedItem = null;
        }
    }

    private async void btnCancel_Clicked(object sender, EventArgs e) => await CloseAsync();

}