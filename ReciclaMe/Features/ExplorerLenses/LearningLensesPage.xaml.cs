using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.ExplorerLenses;

public partial class LearningLensesPage : BasePage
{
    public LearningLensesPage(LearningLensesPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}