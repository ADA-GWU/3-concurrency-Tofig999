using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using ImageProcessing.View.Processing;
using Microsoft.Win32;

namespace ImageProcessing.View.Views;

public partial class MainWindow : Window
{
    private Image imageControl;
    private Bitmap bitmap;

    private ImageContainer _imageContainer;
    private ImageProcessor _imageProcessor;

    private string filename; 

    public MainWindow(string[] args)
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        setup(args);
        imageControl = this.FindControl<Image>("_imageControl")!;
        bitmap = LoadImage(); // Implement a method to load your initial image
        UpdateImage();

        Loaded += async (sender, e) => await ProcessImageAsync();
    }
    
    private async Task ProcessImageAsync()
    {
        await Task.Run(() => _imageProcessor.Process());
    }

    private void setup(string[] args) {
        filename = "mona_lisa.jpg";
        int blockSize = 5;
        string threadingMode = "M";
        if (args.Length >= 3) {
            filename = args[0];
            blockSize = int.Parse(args[1]);
            threadingMode = args[2];    
        }

        _imageContainer = new ImageContainer(0, 0, filename);
        _imageContainer.LoadImageFromFile();

        _imageProcessor = new ImageProcessor(_imageContainer, () => Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(UpdateImage));
        _imageProcessor.SquareSize = blockSize;
        _imageProcessor.ThreadsCount = threadingMode == "M" ? Environment.ProcessorCount / 2 : 1;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private Bitmap LoadImage()
    {
        try {
            return new Bitmap("result.png");
        }
        catch {
            return new Bitmap(filename);
        }
    }

    public void UpdateImage()
    {
        bitmap = LoadImage(); // Load a new image
        imageControl.Source = bitmap; // Update the Image control
    }
}