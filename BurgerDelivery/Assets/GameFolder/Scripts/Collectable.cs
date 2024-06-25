using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //public static event Action OnCollected;
    //public static event Action OnDroppped;
    [SerializeField] float _jumpPover;


    public void Collecting(Transform collectPosition, float offsetY)
    {
        transform.DOJump(collectPosition.position + new Vector3(0, offsetY, 0), _jumpPover, 1, 0.5f);
            //OnComplete(() => transform.position = collectPosition.position);
        
        transform.SetParent(collectPosition.transform);
    }

    public void Dropping(Transform dropPos, float offsetY)
    {
        transform.DOJump(dropPos.position + new Vector3(0, offsetY, 0), _jumpPover, 1, 0.5f);
        transform.parent = null;
    }
}
