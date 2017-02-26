public class MoveLeftCommand : Command
{
    public override void Execute(SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.LEFT);
    }
}