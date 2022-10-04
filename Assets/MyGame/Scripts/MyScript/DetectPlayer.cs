using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] EnemyMovement _Enemy; //Enemy Moving Script

    public void OnTriggerEnter(Collider collision) //Decting The Player  In Enemy Zone
    {
        if (collision is CharacterController)
        {
            if (collision.TryGetComponent<PlayerTag>(out var player)) //Getting Enemy Calculation
            {
                _Enemy.SetTarget(player); //Setting The Player As The Target To The Enemy
            }
        }
    }

    public void OnTriggerExit(Collider collision) //Seting The Player To Enemy To Find The Player 
    {
        if (collision is CharacterController)
        {
            if (collision.TryGetComponent<PlayerTag>(out var player)) //Find The Player With Tag Calculation
            {
                _Enemy.ClearTarget(); //No Target Clear Action
            }
        }
    }
}
