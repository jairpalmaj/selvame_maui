using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciclaMe.Controls;

public partial class RuleContainer : Border
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(RuleContainer),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string Title
    {
        get => (string) GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
    public static readonly BindableProperty SubtitleProperty =
        BindableProperty.Create(
            propertyName: nameof(Subtitle),
            returnType: typeof(string),
            declaringType: typeof(RuleContainer),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string Subtitle
    {
        get => (string) GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }
    
    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(
            propertyName: nameof(Glyph),
            returnType: typeof(string),
            declaringType: typeof(RuleContainer),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string Glyph
    {
        get => (string) GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }
    
    public RuleContainer()
    {
        InitializeComponent();
    }
}