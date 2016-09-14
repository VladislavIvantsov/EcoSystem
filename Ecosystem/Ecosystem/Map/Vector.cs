using System;

public class Vector
{
    public double Distance { get; private set; }
    public Position FirtsPoint { get; private set; }
    public Position SecondPoint { get; private set; }

    public Vector(Position FirtsPoint, Position SecondPoint)
    {
        this.FirtsPoint = FirtsPoint;
        this.SecondPoint = SecondPoint;
        СalculateDistance();
    }

    void СalculateDistance()
    {
        Distance = Math.Sqrt((SecondPoint.X - FirtsPoint.X) * (SecondPoint.X - FirtsPoint.X) + (SecondPoint.Y - FirtsPoint.Y) * (SecondPoint.Y - FirtsPoint.Y));
    }

    static public int VectorCompare(Vector tmp1, Vector tmp2)
    {
        return tmp1.Distance.CompareTo(tmp2.Distance);
    }
}

