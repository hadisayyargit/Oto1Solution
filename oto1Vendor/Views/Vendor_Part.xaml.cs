using oto1.Models;
using oto1.Services;
using System.ComponentModel;


namespace oto1;

public partial class Vendor_Part : ContentPage
{
    ServiceController MyServiceController = new ServiceController();
    public List<PartVendorModel> PartVendorList { get; set; } = new ();
    public bool IsRefreshing { get; set; }
    public PartVendorModel SelectedPartVendor { get; set; } 
    public Command RefreshCommand { get; set; }
    public Vendor_Part()
    {
        RefreshCommand = new Command(async () =>
        {
            IsRefreshing = true;
            await Task.Delay(2000);
            await RefreshForm();

            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));

        });

        //BindingContext = this;
        InitializeComponent();
	}

    private async void ClearForm()
    {
         PartVendorList.Clear();
    }

    private async Task RefreshForm()
    {        /*
        var assembly = typeof(DummyDataProvider).GetTypeInfo().Assembly;

        using var stream = assembly.GetManifestResourceStream("Maui.DataGrid.Sample.teams.json")
            ?? throw new FileNotFoundException("Could not load teams.json");

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        return JsonSerializer.Deserialize<List<Team>>(json)
            ?? throw new InvalidOperationException("Could not deserialize teams.json");
        */

        //HttpClient client = new HttpClient();
        // var teams = client.GetFromJsonAsync<Team>("https://montemagno.com/monkeys.json");
        //var teams = client.GetFromJsonAsync<Team>("file://team.json");

        //string jsonPath = "teams.json"; // مسیر فایل JSON
        //string jsonData = File.ReadAllText(jsonPath);
        //List<Team> teams2 = JsonSerializer.Deserialize<List<Team>>(jsonData);

        /*
        using var stream = await FileSystem.OpenAppPackageFileAsync("teams.json");
        using var reader = new StreamReader(stream);
        string jsonData = await reader.ReadToEndAsync();

        Teams = JsonSerializer.Deserialize<List<PartVendorModel>>(jsonData);
        //return JsonSerializer.Deserialize<List<Team>>(jsonData);

        */


        PartVendorList = await MyServiceController.GetVendor_Part(1);
        //PartVendorList = await MyServiceController.GetVendor_Part(GlobalClass.m_VendorId);
        foreach (var item in PartVendorList)
        {
            item.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + item.PartId.ToString() + ".png";
        }
        
        await Task.Delay(2000);

        BindingContext = PartVendorList;

        //BindingContext = this;

    }

    private void btnClear_Clicked(object sender, EventArgs e)
    {
        PartVendorModel item1 = this.SelectedPartVendor;
        ClearForm();
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
         await RefreshForm();
    }

}