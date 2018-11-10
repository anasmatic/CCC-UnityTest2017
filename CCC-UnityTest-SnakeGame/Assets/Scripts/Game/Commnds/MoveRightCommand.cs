public class MoveRightCommand : Command
{
    public override void Execute(Game.Player.SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.RIGHT);
    }
}