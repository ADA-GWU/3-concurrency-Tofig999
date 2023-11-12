using SkiaSharp;

namespace ImageProcessing.Images; 

public class ImageProcessor {
    private readonly ImageContainer _imageContainer;

    
    public ImageProcessor(ImageContainer imageContainer) {
        _imageContainer = imageContainer;
    }


    public int ThreadsCount { get; set; } = 1;
    public int SquareSize { get; set; } = 5;


    /// <summary>
    /// Processes the image by dividing it into blocks and calculating the average color for each block.
    /// Supports multi-threading based on the specified number of threads.
    /// </summary>
    public void Process() {
        int rowThreadBlockSize = _imageContainer.Height / ThreadsCount;

        Task[] tasks = new Task[ThreadsCount];
        for (int i = 0; i < ThreadsCount - 1; ++i) {
            int start = rowThreadBlockSize * i;

            tasks[i] = Task.Factory.StartNew(() => _processBlock(start, start + rowThreadBlockSize));
        }

        tasks[ThreadsCount - 1] = Task.Factory.StartNew(() => 
            _processBlock(rowThreadBlockSize * (ThreadsCount - 1), _imageContainer.Height));

        Task.WaitAll(tasks);
    }

    private void _processBlock(int rowStart, int rowEnd) {
        for (int row = rowStart; row < rowEnd; row += SquareSize) {
            for (int col = 0; col < _imageContainer.Width; col += SquareSize) {
                int rowCountLimit = Math.Min(SquareSize, rowEnd - row);
                int colCountLimit = Math.Min(SquareSize, _imageContainer.Width - col);

                PixelBlock px = new PixelBlock {
                    Pixels = Enumerable.Range(row, rowCountLimit)
                        .Zip(Enumerable.Range(col, colCountLimit), (r, c) => _imageContainer.Pixels[r, c])
                        .ToArray()
                };

                SKColor average = px.GetAverage();

                for (int r = row; r < row + rowCountLimit; r++) 
                for (int c = col; c < col + colCountLimit; c++) 
                    _imageContainer.Pixels[r, c] = average;
            }
        }
    }

}


file struct PixelBlock {
    public SKColor[] Pixels { get; set; }

    
    /// <summary>
    /// Gets the average color of the pixels in the block.
    /// </summary>
    public SKColor GetAverage() {
        byte red = (byte)Pixels.Average(p => p.Red);
        byte green = (byte)Pixels.Average(p => p.Green);
        byte blue = (byte)Pixels.Average(p => p.Blue);
        byte alpha = (byte)Pixels.Average(p => p.Alpha);

        return new SKColor(red, green, blue, alpha);
    }
}