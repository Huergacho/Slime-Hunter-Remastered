
using UnityEngine;
using System;
using _Main.Scripts.Hud.UI;
using _Main.Scripts.Player;
using Assets._Main.Scripts.Characters.Player;
using Characters.Enemy;
using States;
using UnityEngine.AI;
using Random = UnityEngine.Random;
[RequireComponent(typeof(EnemyModel),typeof(LifeController))]
public class EnemyController : MonoBehaviour, IPooleable
{
    private EnemyModel _enemyModel;
    public EnemyModel EnemyModel => _enemyModel;
    private PlayerController _targetModel;
    public PlayerController Target => _targetModel;
    private bool _waitForIdleState;
    private FSM<EnemyStates> _fsm;
    private INode _root;
    public LifeController LifeController { get; private set;}
    [SerializeField] private EnemyStatsSO stats;
    public EnemyStatsSO Stats => stats;

    #region Events

    public event Action<float> OnMove; 
    public event Action OnAttack; 
    public event Action OnTakeDamage; 
    public event Action OnDie; 
    public event Action OnRespawn; 
    

    #endregion

    private void Awake()
    {
        LifeController = GetComponent<LifeController>();
        _enemyModel = GetComponent<EnemyModel>();

    }

    private void Start()
    {
        
        SuscribeEvents(); 
        LifeController.AssignMaxLife(stats.MaxLife);
        _enemyModel.SuscribeEvents(this);
        _targetModel = GameManager.Instance.Player.GetComponent<PlayerController>();
        InitDecisionTree();
        InitFsm();
    }

    private void SuscribeEvents()
    {
        LifeController.OnModifyHealth += OnTakeDamageCommand;
        LifeController.OnDie += DieCommand;

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
        if (!_targetModel.Model.LifeController.IsAlive() || !LifeController.IsAlive()) return;
        _fsm.UpdateState();

    }

    private bool Activate()
    {
        return (!GameManager.Instance.IsPaused && _targetModel.Model.LifeController.IsAlive());
    }
    public bool IsCloseEnoughToAttack()
    {
        var distance = Vector3.Distance(transform.position, _targetModel.transform.position);
        return distance <= stats.DistanceToAttack;
    }
    
    public void OnMoveCommand(float desiredSpeed)
    { 
        

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
        OnAttack?.Invoke();
    }

    public void DieCommand()
    {
        OnDie?.Invoke();
    }

    public void OnTakeDamageCommand(float a, float b)
    {
        OnTakeDamage?.Invoke();
    }
    public void OnObjectSpawn()
    {
        OnRespawn?.Invoke();
        gameObject.SetActive(true);
        if (RoundCounterController.Instance.CurrentRound > 1)
        {
            ModifyMaxHealth();
        }

        LifeController.Respawn();
    }
    private void ModifyMaxHealth()
    {
        LifeController.AssignMaxLife(LifeController.MaxLife + stats.PerRoundLifeUpgrade);   
    }
}