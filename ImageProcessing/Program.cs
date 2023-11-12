using ImageProcessing.Images;


string filename = "mona_lisa.jpg";
int blockSize = 5;
string threadingMode = "M";
if (args.Length >= 3) {
    filename = args[0];
    blockSize = int.Parse(args[1]);
    threadingMode = args[2];    
}

var container = new ImageContainer();
container.LoadImageFromFile(filename);

var processor = new ImageProcessor(container);
processor.SquareSize = blockSize;
processor.ThreadsCount = threadingMode == "M" ? Environment.ProcessorCount / 2 : 1;
processor.Process();

container.SaveImageToFile("result.png");
