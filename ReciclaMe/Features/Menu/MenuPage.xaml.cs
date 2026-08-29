using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Menu;

public partial class MenuPage : BasePage
{
    public MenuPage(MenuPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}