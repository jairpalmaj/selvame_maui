using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Achievements;

public partial class AchievementPage : BasePage
{
    public AchievementPage(AchievementPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}