/*using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] EnemyMovement _Enemy;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.attachedRigidbody == null) return;
        if (collision.attachedRigidbody.TryGetComponent<PlayerTag>(out var player))
        {
            _Enemy.SetTarget(player);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.attachedRigidbody.TryGetComponent<PlayerTag>(out var player))
        {
            _Enemy.ClearTarget();
        }
    }
}
*/