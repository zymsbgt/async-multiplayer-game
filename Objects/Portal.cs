using Godot;
using System;

[Tool]
public class Portal : Area2D
{
    [Export] private PackedScene nextScene;
    private AnimationPlayer animationPlayer;
    public Node CurrentScene { get; set; }
    public Node Conductor { get; set; }
    
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        Conductor = GetNode<Node>("../../");
        CurrentScene = GetNode<Node2D>("../");
    }

    public override string _GetConfigurationWarning()
    {
        if (nextScene == null)
            return "The next scene property can't be empty!";
        else
            return "";
    }

    public void _on_body_entered(Area2D _area)
    {
        teleport();
        // maybe change this in future to show a speech bubble prompt to enter the portal?
    }

    private async void teleport()
    {
        animationPlayer.Play("fade_to_black");
        await ToSignal(animationPlayer, "animation_finished");
        CallDeferred(nameof(DeferredGotoScene), nextScene);
    }

    public void DeferredGotoScene(PackedScene nextScene)
    {
        CurrentScene.QueueFree();

        // Instance the new scene.
        CurrentScene = nextScene.Instance();

        // Add it to the active scene, as child of root.
        Conductor.AddChild(CurrentScene);

        // Optionally, to make it compatible with the SceneTree.change_scene() API.
        //GetTree().CurrentScene = CurrentScene;
    }
}