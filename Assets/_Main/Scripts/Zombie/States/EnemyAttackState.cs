using System;
using _Main.Scripts.Gun;
using UnityEngine;

public class EnemyAttackState<T> : State<T>
{
        
    private INode _root;
    private EnemyController _controller;

    public EnemyAttackState(EnemyController controller,INode root)
    {
        _root = root;
        _controller = controller;
    }

    public override void Execute()
    {
        if (GameManager.Instance.IsPaused)
        {
            return;
        }
        if (!_controller.IsCloseEnoughToAttack())
        {
            _root.Execute();
            return;    
        }
        _controller.OnAttackCommand();
        _controller.OnMoveCommand(0);
    }
}