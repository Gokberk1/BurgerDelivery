using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropFoodArea : MonoBehaviour
{
    public static event Action<DropFoodArea, int> OnTableIsDone;
    public static event Action<Transform> OnMoneyIsCollecting;
    public int MaxLoad => _maxLoad;
    public List<GameObject> FoodList => _foodList;
    public Transform MoneyPos => _moneyPos;

    [SerializeField] private List<GameObject> _foodList = new List<GameObject>();
    [SerializeField] Transform _dropPos;
    [SerializeField] Transform _moneyPos;
    [SerializeField] private int _maxLoad;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private bool _tableIsResetted;

    private void Start()
    {
        ResetTable();
        _tableIsResetted = false;
        _maxLoad = Random.Range(1, 6);
    }

    public void OnDropping(List<GameObject> foodList, float offsetY)
    {
        _foodList.Add(foodList[foodList.Count - 1]);
        GameObject obj = _foodList[_foodList.Count - 1];
        obj.GetComponent<Collectable>().Dropping(_dropPos, offsetY * (_foodList.Count - 1));

        if (_foodList.Count == _maxLoad)
        {
            StartCoroutine(ServeIsDone());
        }
    }

    IEnumerator ServeIsDone() 
    {
        yield return new WaitForSeconds(5);
        OnTableIsDone?.Invoke(this, _foodList.Count);
        ResetTable();
    }

    public void ResetTable()
    {
        int tempFoodCount = _foodList.Count;

        for (int i = 0; i < tempFoodCount; i++)
        {
            GameObject obj = _foodList[_foodList.Count - 1];
            _pool.ReturnObject(obj);
            _foodList.RemoveAt(_foodList.Count - 1);
        }

        //_maxLoad = Random.Range(1, 6);
        _tableIsResetted = true;
    }

    public void CollectMoney()
    {
        if (_tableIsResetted)
        {
            _tableIsResetted = false;
            OnMoneyIsCollecting?.Invoke(_moneyPos);
            _maxLoad = Random.Range(1, 6);
        }
    }
}
