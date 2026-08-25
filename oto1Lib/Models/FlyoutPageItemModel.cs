using oto1;
using oto1.Models;

namespace oto1.Models;

public class FlyoutPageItemModel
{
    public string Title { get; set; }
    public string IconSource { get; set; }
    public Type TargetType { get; set; }
    public string MenuName { get; set; }
}
