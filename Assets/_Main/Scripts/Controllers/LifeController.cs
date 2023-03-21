using System;
using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{ 
    private float _maxLife;
    public float MaxLife => _maxLife;
    private float _currentLife;
    public float CurrentLife => _currentLife;
    public event Action<float, float> OnModifyHealth; 
    public event Action OnDie;
    public event Action OnRespawn;
    public void AssignMaxLife(float data)
    {
        _maxLife = data;
        _currentLife = _maxLife;
    }
    public void TakeDamage(int damage)
    {
        _currentLife -= damage;
        OnModifyHealth?.Invoke(_currentLife, _maxLife);
        if (!IsAlive())
        {
            OnDie?.Invoke();
        }
    }

    public void Heal(float amount = 1)
    {
        _currentLife += amount;
        if (_currentLife > _maxLife)
        {
            _currentLife = _maxLife;
        }
        OnModifyHealth?.Invoke(_currentLife,_maxLife);
    }
    public bool IsAlive()
    {
        return _currentLife >= 1;
    }

    public void ModifyMaxHealth(float amount)
    {
        _maxLife = amount;
    }

    public void Respawn()
    {
        Heal(_maxLife);
        OnRespawn?.Invoke();
    }
}