using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.StartAdventure;

public partial class StartPage : BasePage
{
    public StartPage(StartPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}