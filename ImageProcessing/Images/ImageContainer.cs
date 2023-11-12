using SkiaSharp;

namespace ImageProcessing.Images; 

public class ImageContainer {
    public SKColor[,] Pixels { get; private set; } = null!;
    public int Height { get; set; }
    public int Width { get; set; }

    
    
    public ImageContainer() : this(0, 0) { }

    public ImageContainer(int height, int width) {
        Height = height;
        Width = width;
    }
    
    
    /// <summary>
    /// Loads an image from the specified file using the SkiaSharp library.
    /// </summary>
    public void LoadImageFromFile(string filename) {
        using var bitmap = SKBitmap.Decode(filename);

        Height = bitmap.Height;
        Width = bitmap.Width;

        Pixels = new SKColor[Height, Width];
        for (int i = 0; i < Height; ++i) {
            for (int j = 0; j < Width; ++j) {
                Pixels[i, j] = bitmap.GetPixel(j, i);
            }
        }
    }

    /// <summary>
    /// Saves the image to the specified file.
    /// </summary>
    public void SaveImageToFile(string filename) {
        var bitmap = new SKBitmap(Width, Height);

        for (int row = 0; row < Height; ++row)
        for (int col = 0; col < Width; ++col)
            bitmap.SetPixel(col, row, Pixels[row, col]);
        

        SKImage image = SKImage.FromBitmap(bitmap);
        SKData imageData = image.Encode(SKEncodedImageFormat.Png, 100);
        
        string? projectDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(projectDirectory, filename);
        
        using var stream = File.OpenWrite(filePath);
        imageData.SaveTo(stream);
    }
}