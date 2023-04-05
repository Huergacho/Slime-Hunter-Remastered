using System;
using UnityEngine;
public class EnemySeekState<T> : State<T>
{
    private INode _root;
    private EnemyController _controller;

    public EnemySeekState(EnemyController controller, INode root)
    {
        _controller = controller;
        _root = root;
    }

    public override void Execute()
    {

        if (GameManager.Instance.IsPaused)
        {
            _root.Execute();
            _controller.OnMoveCommand(0);
            return;
        }
        if (_controller.IsCloseEnoughToAttack())
        {
            _root.Execute();
            return;
        }
        _controller.OnMoveCommand(_controller.Stats.MaxSpeed);
    }
}