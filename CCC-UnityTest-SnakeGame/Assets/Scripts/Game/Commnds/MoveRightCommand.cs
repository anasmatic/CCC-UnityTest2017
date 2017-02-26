public class MoveRightCommand : Command
{
    public override void Execute(SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.RIGHT);
    }
}