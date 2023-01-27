using Godot;
using System;

public class RhythmBlockTileMap : TileMap
{
    private Node2D currentLevel;
    private Mochi mochi;
    private int waitTime = 2;
    [Export] private int ActivatingNote;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentLevel = GetNode<Node2D>("../");
        currentLevel.Connect("beatSignal", this, "_on_beatSignal");
        mochi = GetNode<Mochi>("../Mochi");
        mochi.Connect("ColourWheel_area_entered", this, "_on_ColourWheel_area_entered");
        mochi.Connect("ColourWheel_area_exited", this, "_on_ColourWheel_area_exited");

        //Visible = false;
        Modulate = Color.Color8(173, 216, 230, 32);
        SetCollisionLayerBit(3, false);
    }

    #region signals
    public void _on_beatSignal(int song_position_in_beats)
    {
        // if (song_position_in_beats % waitTime == 0)
        //     ToggleVisibility();
    }

    public void _on_ColourWheel_area_entered(int note)
    {
        if (note == ActivatingNote)
        {
            Modulate = Color.Color8(173, 216, 230, 255);
            SetCollisionLayerBit(3, true);
        }
    }

    public void _on_ColourWheel_area_exited(int note)
    {
        if (note == ActivatingNote)
        {
            Modulate = Color.Color8(173, 216, 230, 32);
            SetCollisionLayerBit(3, false);
        }
    }
    #endregion
}
