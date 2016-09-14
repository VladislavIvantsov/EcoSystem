using System;
using System.Drawing;
using System.IO;

public class BitMaps
{
    const int PowerThreads = 40;
    static public Bitmap Empty;
    static public Bitmap Herbivore;
    static public Bitmap Predator;
    static public Bitmap Herb;
    static public Bitmap HerbAndPredator;
    static public Bitmap InaccessibleCell;
    public int EffectiveSize {get; private set;}

    public void LoadBitmaps(int SizeOfField, int WidthScreen, int HeigthScreen)
    {
        GetEffectiveSize(WidthScreen, HeigthScreen, SizeOfField);
        Size SizeOfBitmap = new Size(EffectiveSize, EffectiveSize);
        Empty = new Bitmap(new Bitmap("Images/Empty.jpg"), SizeOfBitmap);
        Herbivore = new Bitmap(new Bitmap("Images/Herbivore.jpg"), SizeOfBitmap);
        Predator = new Bitmap(new Bitmap("Images/Predator.jpg"), SizeOfBitmap);
        Herb = new Bitmap(new Bitmap("Images/Herb.jpg"), SizeOfBitmap);
        HerbAndPredator = new Bitmap(new Bitmap("Images/HerbAndPredator.jpg"), SizeOfBitmap);
        InaccessibleCell = new Bitmap(new Bitmap("Images/InaccessibleCell.jpg"), SizeOfBitmap);
    }

    void GetEffectiveSize(int WidthScreen, int HeigthScreen, int CountOfCells)
    {
        int MinSide = Math.Min(WidthScreen - PowerThreads, HeigthScreen - PowerThreads);
        EffectiveSize = MinSide / CountOfCells;
    }
}

