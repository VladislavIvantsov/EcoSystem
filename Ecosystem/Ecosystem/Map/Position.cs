using System;

public class Position
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool ComparePoint(Position SomePoint)
    {
        if (X == SomePoint.X && Y == SomePoint.Y) return true;
        return false;
    }
}