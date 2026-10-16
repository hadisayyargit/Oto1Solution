using MauiPersianToolkit;
using Microsoft.Maui.Controls;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class Reports : ContentPage
{
    public long ServiceId { get; set; }


    public Reports()
    {
        InitializeComponent();


        RefreshForm();
    }

 


    private  void RefreshForm()
    {
       
    }

    private void toolbarItemReportNotSending_Clicked(object sender, EventArgs e)
    {

    }
    private void toolbarItemReportNotCheck_Clicked(object sender, EventArgs e)
    {

    }
}