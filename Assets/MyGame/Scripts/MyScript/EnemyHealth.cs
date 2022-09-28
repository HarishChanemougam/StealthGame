using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _startHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] UnityEvent _onDie;

    int _currentHealth;

    public event UnityAction OnDamage;
    public event UnityAction Ondie;

    public int CurrentHealth
    {
        get
        { 
            return _currentHealth; 
        } 
    }

    public int HealthDammage
    {
        get
        {
            { return CurrentHealth / _maxHealth; }
        }
    }

    public bool Dead => CurrentHealth <= 0;

    public void Start()
    {
        _currentHealth = _startHealth;
    }

    internal void Damage()
    {
        _currentHealth--;
        OnDamage?.Invoke();

        if(Dead)
        {
            Ondie.Invoke();
        }
    }
}
