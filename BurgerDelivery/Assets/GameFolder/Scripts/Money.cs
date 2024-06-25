using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public void Spawning(Vector3 spawnPos)
    {
        transform.DOJump(spawnPos, 0.5f, 1, 0.5f).SetEase(Ease.Linear);
    }

    public void Collecting(Vector3 collectPos)
    {
        //transform.parent = null;
        transform.DOMove(collectPos, 1f).SetEase(Ease.InOutBack);
    }
}
