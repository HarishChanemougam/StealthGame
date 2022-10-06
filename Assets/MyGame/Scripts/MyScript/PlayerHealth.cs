using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _startHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] Animator _animator;
    [SerializeField] GameOver gameOver;
    bool death;

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
            { return CurrentHealth / _maxHealth; }
        }
    }

    public bool IsDead => CurrentHealth <= 0;
    

    public void Start()
    {
        _currentHealth = _startHealth;
        _healthBar.setHealth(_maxHealth);
        Mathf.Max(_currentHealth, 0);
    }

    internal void Damage()
    {
        _currentHealth--;
        OnDamage?.Invoke();

        if(IsDead)
        {
            OnDie?.Invoke();
            _animator.SetTrigger("Death");
            gameOver.EndGame();
        }

        _healthBar.setHealth(CurrentHealth);


        
       
    }
}
