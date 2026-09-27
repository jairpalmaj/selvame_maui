using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Alerts;

public partial class FailedExtraPointsPage : BasePage
{
    public FailedExtraPointsPage(FailedExtraPointsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}