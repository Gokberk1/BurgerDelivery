using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    List<GameObject> _foodList = new List<GameObject>();
    private int _stackCount;
    public bool _isPlayerClose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            _isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            _isPlayerClose = false;
        }
    }

    public void StackFood(GameObject obj)
    {
        _stackCount++;
        obj.transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f * _stackCount, transform.position.z);
        obj.transform.SetParent(transform);
        _foodList.Add(obj);    
    }

    public void RemoveFood(GameObject obj)
    {
        obj.transform.parent = null;
        _foodList.Remove(obj);
        _stackCount--;
    }
}
