using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Microsoft.Maui.Controls.TabbedPage;

namespace oto1;

public partial class RequestTabbedPage : TabbedPage
{
	public RequestTabbedPage()
	{
		InitializeComponent();
        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().DisableSwipePaging();
    }
}