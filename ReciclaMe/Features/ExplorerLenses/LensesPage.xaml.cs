using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.ExplorerLenses;

public partial class LensesPage : BasePage
{
    public LensesPage(LensesPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}