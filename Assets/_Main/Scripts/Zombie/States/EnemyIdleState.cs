using System;
using UnityEngine;
public class EnemyIdleState<T> : State<T>
{
    private INode _root;
    private EnemyController _controller;

    public EnemyIdleState(EnemyController controller, INode root)
    {
        _controller = controller;
        _root = root;
    }

    public override void Execute()
    {
        if (_controller.IsCloseEnoughToAttack() && !GameManager.Instance.IsPaused)
        {
            _root.Execute();
            return;
        }

        _controller.OnMoveCommand(0);
    }
}