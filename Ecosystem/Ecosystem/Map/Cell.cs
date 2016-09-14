using System.Drawing;
using System;

public enum State { Empty, Herb, Herbivore, Predator, HerbAndPredator, InaccessibleCell }

public class Cell
{
    public State CellState { get; private set; }
    public Herb HerbInCell { get; set; }
    public Animal AnimalInCell { get; set; }
    
    public TextureBrush Brush {get; private set;}
    bool Drawing;

    public Cell(bool _Drawing)
    {
        Drawing = _Drawing;
        HerbInCell = null;
        AnimalInCell = null;
        SetState(State.Empty);
    }

    public Cell(State CellState)
    {
        Drawing = false;
        HerbInCell = null;
        AnimalInCell = null;
        SetState(CellState);
    }

    public void SetState(State CellState)
    {
        this.CellState = CellState;
        if(Drawing) LoadBrush();
    }

    void LoadBrush()
    {
        switch (CellState)
        {
            case State.Empty:
                Brush = new TextureBrush(BitMaps.Empty);
                break;
            case State.Herb:
                Brush = new TextureBrush(BitMaps.Herb);
                break;
            case State.Herbivore:
                Brush = new TextureBrush(BitMaps.Herbivore);
                break;
            case State.Predator:
                Brush = new TextureBrush(BitMaps.Predator);
                break;
            case State.HerbAndPredator:
                Brush = new TextureBrush(BitMaps.HerbAndPredator);
                break;
            case State.InaccessibleCell:
                Brush = new TextureBrush(BitMaps.InaccessibleCell);
                break;
        }
    }

    public Organism GetInformation()
    {
        if (AnimalInCell != null) return AnimalInCell;
        if (HerbInCell != null) return HerbInCell;
        return null;
    }
}

