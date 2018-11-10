using UnityEngine;

//TODO:Abstact class
public class Command
{
    public virtual void Execute(Game.Player.SnakeHead snakeHead )
    {
    }
    public virtual void Execute(Game.GameManager game)
    {
    }
}