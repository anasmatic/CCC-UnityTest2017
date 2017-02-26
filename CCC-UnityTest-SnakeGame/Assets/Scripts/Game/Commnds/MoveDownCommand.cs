internal class MoveDownCommand : Command
{
    public override void Execute(SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.DOWN);
    }
}