using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _startHealth; //Enemy Staring Health
    [SerializeField] int _maxHealth; //Enemy Maximum Health 
    [SerializeField]Animator _animator;

    int _currentHealth; //Current Health Of Enemy

    public event UnityAction OnDamage; //Enemy Getting Damage
    public event UnityAction Ondie; //Enemy Die
    bool dead;

    public int CurrentHealth
    {
        get
        { 
            return _currentHealth; //Showing The Current Health Of The Enemy 
        } 
    }

    public int HealthDammage
    {
        get
        {
            { return CurrentHealth / _maxHealth; } //Enemy Dampage Health Calculation
        }
    }

    public bool Dead => CurrentHealth <= 0; //Enemy Death State

    public void Start()
    {
        _currentHealth = _startHealth; //Saying That The Current Health Is The Starting Health Of Enemy When Game Starts
        Mathf.Max(_currentHealth, 0);
    }

    internal void Damage()
    {
        _currentHealth--; //Getting Current Health To Dammage
        OnDamage?.Invoke(); // Invok The Enemy To Damage

        if(Dead)
        {
            Ondie?.Invoke(); //Invok The Enemy To Death 
            _animator.SetTrigger("dead");
        }
    }

}
