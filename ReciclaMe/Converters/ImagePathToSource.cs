using System.Globalization;

namespace ReciclaMe.Converters;

public sealed class ImagePathToSource : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string imagePath)
        {
            ImageSource source = ImageSource.FromStream(() =>
            {
                var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return fileStream;
            });
            
            return source;
        }
        
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}