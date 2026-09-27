using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciclaMe.Controls;

public partial class BadgeView : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(BadgeView),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string Title
    {
        get => (string) GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly BindableProperty ImageProperty =
        BindableProperty.Create(
            propertyName: nameof(Image),
            returnType: typeof(ImageSource),
            declaringType: typeof(BadgeView),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

    public ImageSource Image
    {
        get => (ImageSource) GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public BadgeView()
    {
        InitializeComponent();
    }
}