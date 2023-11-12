# ImageProcessing

## Project Overview
This project, named **ImageProcessing**, is written in .NET/C# and is designed to process images by calculating the average color of square-sized blocks from left to right, top to bottom. The resulting color is then applied to the entire square. The progress of the image processing is displayed incrementally. Additionally, the project supports multi-processing mode, leveraging parallel threads based on the computer's CPU cores. In multithreaded mode, the project utilizes half of the available CPU cores.

## How to Run the Project
To run the project, follow these steps:

1. Restore packages:
    ```bash
    dotnet restore
    ```

2. Change directory to the project directory:
    ```bash
    cd ImageProcessing
    ```

3. Run the project using the following command template:
    ```bash
    dotnet run {filename} {square_size} {threading_mode: S or M}
    ```
    Replace `{filename}`, `{square_size}`, and `{threading_mode}` with the appropriate values.

## Example Command
```bash
dotnet run mona_lisa.jpg 20 M
```
This command processes the image file `mona_lisa.jpg` with a square size of 20 and uses multi-threading mode.

**Note:** The output file will be named `result.png`. Depending on the task, the initial intention was to output `result.jpg`. However, due to issues with the SkiaSharp library, the output format had to be adjusted to PNG.

## Code Overview
### ImageContainer Class
```csharp
public class ImageContainer {
    // ... (existing code)

    /// <summary>
    /// Loads an image from the specified file using the SkiaSharp library.
    /// </summary>
    public void LoadImageFromFile(string filename) {
        // ... (existing code)
    }

    /// <summary>
    /// Saves the image to the specified file.
    /// </summary>
    public void SaveImageToFile(string filename) {
        // ... (existing code)
    }
}
```

### ImageProcessor Class
```csharp
public class ImageProcessor {
    // ... (existing code)

    /// <summary>
    /// Processes the image by dividing it into blocks and calculating the average color for each block.
    /// Supports multi-threading based on the specified number of threads.
    /// </summary>
    public void Process() {
        // ... (existing code)
    }

    private void _processBlock(int rowStart, int rowEnd) {
        // ... (existing code)
    }
}
```

### PixelBlock Struct
```csharp
public struct PixelBlock {
    // ... (existing code)

    /// <summary>
    /// Gets the average color of the pixels in the block.
    /// </summary>
    public SKColor GetAverage() {
        // ... (existing code)
    }
}
```
