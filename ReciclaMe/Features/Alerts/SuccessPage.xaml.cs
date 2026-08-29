using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Alerts;

public partial class SuccessPage : BasePage
{
    public SuccessPage(SuccessPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}