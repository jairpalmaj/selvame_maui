using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReciclaMe.Controls;

public partial class CategoryClass : Border
{
    public static readonly BindableProperty IconGlyphProperty =
        BindableProperty.Create(
            propertyName: nameof(IconGlyph),
            returnType: typeof(string),
            declaringType: typeof(CategoryClass),
            defaultValue: "\uf539",
            defaultBindingMode: BindingMode.OneWay);

    public string IconGlyph
    {
        get => (string) GetValue(IconGlyphProperty);
        set => SetValue(IconGlyphProperty, value);
    }
    
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(CategoryClass),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string Title
    {
        get => (string) GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(
            propertyName: nameof(TapCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CategoryClass),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

    public ICommand TapCommand
    {
        get => (ICommand) GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }
    
    public static readonly BindableProperty TapCommandParameterProperty =
        BindableProperty.Create(
            propertyName: nameof(TapCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(CategoryClass),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

    public object TapCommandParameter
    {
        get => (object) GetValue(TapCommandParameterProperty);
        set => SetValue(TapCommandParameterProperty, value);
    }
    
    public CategoryClass()
    {
        InitializeComponent();
    }
}