
using UnityEngine;
using System;
using _Main.Scripts.Player;
using Assets._Main.Scripts.Characters.Player;
using States;
using UnityEngine.AI;
using Random = UnityEngine.Random;
[RequireComponent(typeof(EnemyModel))]
public class EnemyController : MonoBehaviour
{
    private EnemyModel _enemyModel;
    public EnemyModel EnemyModel => _enemyModel;
    private PlayerController _targetModel;
    public PlayerController Target => _targetModel;
    private bool _waitForIdleState;
    private FSM<EnemyStates> _fsm;
    private INode _root;
    private NavMeshAgent _agent;
    
    public event Action<float> OnMove; 
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyModel = GetComponent<EnemyModel>();
    }

    private void Start()
    {
        _enemyModel.SuscribeEvents(this);
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _targetModel = GameManager.Instance.Player.GetComponent<PlayerController>();
        InitDecisionTree();
        InitFsm();
    }
    #region FSM Methods
    private void InitDecisionTree()
    {
  
        // Actions

        var goToChase = new ActionNode(()=> _fsm.Transition(EnemyStates.Move));
        var goToAttack = new ActionNode(()=> _fsm.Transition(EnemyStates.Attack));
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStates.Idle));
        
        // Questions
        var attemptPlayerKill = new QuestionNode(IsCloseEnoughToAttack, goToAttack, goToChase);
        var isPlayerAlive = new QuestionNode(Activate, attemptPlayerKill, goToIdle);
         
        _root = isPlayerAlive;
    }   
    private void InitFsm()
    {
        //--------------- FSM Creation -------------------//                
        // States Creation
        var seekState = new EnemySeekState<EnemyStates>(this,_root);
        
        var attack = new EnemyAttackState<EnemyStates>(this,_root);

        //Chase 
        seekState.AddTransition(EnemyStates.Attack,attack);
        //Attack
        attack.AddTransition(EnemyStates.Move,seekState);

        _fsm = new FSM<EnemyStates>(seekState);
   
    }
    #endregion

    void Update()
    {
        if (!_targetModel.Model.LifeController.IsAlive()) return;
        _fsm.UpdateState();

    }

    private bool Activate()
    {
        return (!GameManager.Instance.IsPaused && _targetModel.Model.LifeController.IsAlive());
    }
    public bool IsCloseEnoughToAttack()
    {
        var distance = Vector3.Distance(transform.position, _targetModel.transform.position);
        return distance <= _enemyModel.Stats.DistanceToAttack;
    }
    
    public void OnMoveCommand(float desiredSpeed)
    { 
        
        _agent.speed = desiredSpeed;
        if (desiredSpeed != 0)
        {
            _agent.SetDestination(_targetModel.transform.position);
        }
        OnMove?.Invoke(desiredSpeed);
        Rotate();
    }

    public void Rotate()
    {
        var target = _targetModel.transform.position;
        target.y = transform.position.y;
        
        transform.LookAt(target);
    }
    public void OnAttackCommand()
    {
        _enemyModel.Attack();
    }
    private void OnDieCommand()
    {
    }
    public void OnHit(GameObject hitter)
    {
        // var owner = hitter.GetComponent<CharacterModel>();
        // if (owner != null)
        // {
        //     print("Lo hacemo");
        //     owner.onHitAction?.Invoke();
        // }
    }
}