using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Classification;

public partial class ClassificationCategoryPage : BasePage
{
    public ClassificationCategoryPage(ClassificationCategoryPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}