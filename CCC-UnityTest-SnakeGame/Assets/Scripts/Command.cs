using UnityEngine;

//TODO:Abstact class
public class Command
{
    public virtual void Execute(  SnakeHead snakeHead )
    {
    }
    public virtual void Execute(Game game)
    {
    }
}