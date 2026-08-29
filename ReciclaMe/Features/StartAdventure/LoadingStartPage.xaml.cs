using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.StartAdventure;

public partial class LoadingStartPage : BasePage
{
    public LoadingStartPage(LoadingStartPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}