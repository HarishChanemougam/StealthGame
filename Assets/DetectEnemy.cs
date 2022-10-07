using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EnemyHealth>(out var h))
        {

        }


    }

    private void OnTriggerExit(Collider other)
    {
        
    }


}
