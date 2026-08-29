using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.Classification;

public partial class ClassificationPage : BasePage
{
    public ClassificationPage(ClassificationPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (IViewModelLifeCycle) BindingContext;
        await vm.OnAppearing();
    }
}