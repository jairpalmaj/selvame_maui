using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReciclaMe.Controls;

public partial class CharacterSelection : Border
{
    public static readonly BindableProperty CharacterNameProperty =
        BindableProperty.Create(
            propertyName: nameof(CharacterName),
            returnType: typeof(string),
            declaringType: typeof(CharacterSelection),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string CharacterName
    {
        get => (string) GetValue(CharacterNameProperty);
        set => SetValue(CharacterNameProperty, value);
    }
    
    public static readonly BindableProperty CharacterSkillProperty =
        BindableProperty.Create(
            propertyName: nameof(CharacterSkill),
            returnType: typeof(string),
            declaringType: typeof(CharacterSelection),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.OneWay);

    public string CharacterSkill
    {
        get => (string) GetValue(CharacterSkillProperty);
        set => SetValue(CharacterSkillProperty, value);
    }
    
    public static readonly BindableProperty CharacterGlyphProperty =
        BindableProperty.Create(
            propertyName: nameof(CharacterGlyph),
            returnType: typeof(string),
            declaringType: typeof(CharacterSelection),
            defaultValue: "\uf539",
            defaultBindingMode: BindingMode.OneWay);

    public string CharacterGlyph
    {
        get => (string) GetValue(CharacterGlyphProperty);
        set => SetValue(CharacterGlyphProperty, value);
    }
    
    public static readonly BindableProperty CharacterImageSourceProperty =
        BindableProperty.Create(
            propertyName: nameof(CharacterImageSource),
            returnType: typeof(ImageSource),
            declaringType: typeof(CharacterSelection),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

    public ImageSource CharacterImageSource
    {
        get => (ImageSource) GetValue(CharacterImageSourceProperty);
        set => SetValue(CharacterImageSourceProperty, value);
    }
    
    public static readonly BindableProperty CharacterGlyphColorProperty =
        BindableProperty.Create(
            propertyName: nameof(CharacterGlyphColor),
            returnType: typeof(Color),
            declaringType: typeof(CharacterSelection),
            defaultValue: Color.FromArgb("#FFF"),
            defaultBindingMode: BindingMode.OneWay);

    public Color CharacterGlyphColor
    {
        get => (Color) GetValue(CharacterGlyphColorProperty);
        set => SetValue(CharacterGlyphColorProperty, value);
    }
    
    public static readonly BindableProperty SelectCommandProperty =
        BindableProperty.Create(
            propertyName: nameof(SelectCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CharacterSelection),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

    public ICommand SelectCommand
    {
        get => (ICommand) GetValue(SelectCommandProperty);
        set => SetValue(SelectCommandProperty, value);
    }
    
    public static readonly BindableProperty SelectCommandParameterProperty =
        BindableProperty.Create(
            propertyName: nameof(SelectCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(CharacterSelection),
            defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

    public object SelectCommandParameter
    {
        get => (object) GetValue(SelectCommandParameterProperty);
        set => SetValue(SelectCommandParameterProperty, value);
    }
    
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(
            propertyName: nameof(IsSelected),
            returnType: typeof(bool),
            declaringType: typeof(CharacterSelection),
            defaultValue: false,
            defaultBindingMode: BindingMode.OneWay);

    public bool IsSelected
    {
        get => (bool) GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    
    public CharacterSelection()
    {
        InitializeComponent();
    }
}