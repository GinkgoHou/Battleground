using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleAnim : MonoBehaviour
{
    public Transform enemyIdle;

    // Start is called before the first frame update
    void Start()
    {
        enemyIdle.localScale = new Vector3(1f, 1f, 1f);
        IdleAnim();

    }

    void IdleAnim()
    {
        DOTween.Sequence()
            .Append(enemyIdle.DOScaleY(1.1f, 1f))
            .SetLoops(-1, LoopType.Yoyo);
    }



    // Update is called once per frame
    void Update()
    {

    }
}