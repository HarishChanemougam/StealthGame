using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] Transform _root;

    PlayerTag _playerFound;

    public PlayerTag PlayerFound => _playerFound;


    private void OnTriggerEnter(Collider other)
    {
        TryDetectPlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDetectPlayer(other);
    }
    private void OnTriggerExit(Collider other)
    {
        var playerTag = other.GetComponentInParent<PlayerTag>();
        if(_playerFound = playerTag)
        {
            _playerFound = null;
        }
    }

    private void TryDetectPlayer(Collider other)
    {
        var playerTag = other.GetComponentInParent<PlayerTag>();
        if (playerTag != null)
        {
            var direction = playerTag.transform.position - _root.transform.position;
            if (Physics.Raycast(_root.transform.position, direction, direction.magnitude, LayerMask.GetMask("Env")))
            {
                Debug.DrawRay(_root.transform.position, direction, Color.red, 1f);
            }

            else
            {
                Debug.DrawRay(_root.transform.position, direction, Color.green, 1f);
                _playerFound = playerTag;
            }

        }
    }


}
