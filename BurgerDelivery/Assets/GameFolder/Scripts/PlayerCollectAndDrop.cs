using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectAndDrop : MonoBehaviour
{
    public int CurrentLoad 
    { 
        get { return _currentLoad; } 
        set {
            if(value < 0)
            {
                this._currentLoad = 0;
            }
        } 
    }
   
    [SerializeField] PlayerDataSo _data;
    [SerializeField] private List<GameObject> _foodList = new List<GameObject>();
    [SerializeField] private int _currentLoad;
    [SerializeField] Transform _collectPosition;

    public static FoodSpawner _foodSpawner;
    public static DropFoodArea _dropFoodArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            _foodSpawner = other.GetComponent<FoodSpawner>();

            while (_currentLoad < _data.MaxLoad && _foodSpawner.FoodList.Count > 0)
            {
                _foodSpawner.OnCollecting(_collectPosition, 0.1f);
                _foodList.Add(_foodSpawner.FoodList[_foodSpawner.FoodList.Count - 1]);
                _currentLoad = _foodList.Count;
                _foodSpawner.RemoveLast();
            }
        }

        if (other.gameObject.CompareTag("DropArea"))
        {
            _dropFoodArea = other.GetComponent<DropFoodArea>();

            while (_currentLoad > 0 && _dropFoodArea.FoodList.Count < _dropFoodArea.MaxLoad)
            {
                _dropFoodArea.OnDropping(_foodList, 0.15f);
                _foodList.RemoveAt(_foodList.Count - 1);
                _currentLoad = _foodList.Count;
            }

            
            _dropFoodArea.CollectMoney();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            _foodSpawner = null;
        }

        if (other.gameObject.CompareTag("DropArea"))
        {
            _dropFoodArea = null;
        }
    }
}
