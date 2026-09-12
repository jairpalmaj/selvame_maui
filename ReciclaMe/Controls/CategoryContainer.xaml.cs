using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciclaMe.Controls;

public partial class CategoryContainer : Border
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(CategoryContainer),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string Title
    {
        get => (string) GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(
            propertyName: nameof(Description),
            returnType: typeof(string),
            declaringType: typeof(CategoryContainer),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string Description
    {
        get => (string) GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(
            propertyName: nameof(ImageSource),
            returnType: typeof(ImageSource),
            declaringType: typeof(CategoryContainer),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

    public ImageSource ImageSource
    {
        get => (ImageSource) GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    
    public CategoryContainer()
    {
        InitializeComponent();
    }
}