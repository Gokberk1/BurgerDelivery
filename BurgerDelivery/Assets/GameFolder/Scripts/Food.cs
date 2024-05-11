using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    CollectItems _collectItems;
    bool _isStacked = false;

    private void Start()
    {
        _collectItems = GameObject.FindObjectOfType<CollectItems>();
    }

    private void FixedUpdate()
    {
        IsTakenFromPlayer();
    }

    void IsTakenFromPlayer()
    {
        if (_collectItems._isPlayerClose && !_isStacked)
        {
            _isStacked = true;
            _collectItems.StackFood(this.gameObject);
        }
    }

}
