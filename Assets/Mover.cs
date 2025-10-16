using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float timeSpan = 2f;

    private void Start()
    {
        transform.DOMove(new Vector3 (5,transform .position .y ,0),timeSpan ).SetEase (Ease.InCubic );

    }
}
