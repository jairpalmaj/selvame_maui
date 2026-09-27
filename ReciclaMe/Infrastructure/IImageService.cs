namespace ReciclaMe.Infrastructure;

public interface IImageService
{
    string SavePicture(Stream stream, string fileName = "img.png");
    void DeletePicture(string fileName = "img.png");
    Stream ResizeByFileNameAsync(string fileName = "img.png"); 
    Stream ResizeByPathAsync(string path);
    Stream ReadPhoto(string path);
    void DeleteAll();
}