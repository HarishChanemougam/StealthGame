using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _startHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] UnityEvent _onDie;
    [SerializeField] HealthBar _healthBar;

    int _currentHealth;

    public event UnityAction OnDamage;
    public event UnityAction OnDie;

    public int CurrentHealth
    {
        get
        {           
         return _currentHealth;
        }
    }


    public int HealthProgress
    {
        get
        {
            { return  CurrentHealth / _maxHealth; }
        }
    }

    public bool IsDead => CurrentHealth <=0;

    public void Start()
    {
        _currentHealth = _startHealth;
        _healthBar.setHealth(_maxHealth);
    }

    internal void Damage()
    {
        _currentHealth--;
        OnDamage?.Invoke();

        if(IsDead)
        {
            OnDie.Invoke();
        }

        _healthBar.setHealth(CurrentHealth);

    }
}
