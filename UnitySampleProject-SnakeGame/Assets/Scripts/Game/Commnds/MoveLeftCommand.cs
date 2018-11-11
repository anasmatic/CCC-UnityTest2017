public class MoveLeftCommand : Command
{
    public override void Execute(Game.Player.SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.LEFT);
    }
}