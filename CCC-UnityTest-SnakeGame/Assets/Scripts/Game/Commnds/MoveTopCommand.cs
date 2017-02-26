public class MoveTopCommand : Command
{
    public override void Execute(SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.TOP);
    }
}