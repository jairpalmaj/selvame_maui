using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Learn;

public partial class LearnPage : BasePage
{
    public LearnPage(LearnPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}