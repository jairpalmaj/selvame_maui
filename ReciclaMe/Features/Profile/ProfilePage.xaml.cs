using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Profile;

public partial class ProfilePage : BasePage
{
    public ProfilePage(ProfilePageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}