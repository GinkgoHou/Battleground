using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleAnim : MonoBehaviour
{
    public Transform playerIdle; 

    // Start is called before the first frame update
    void Start()
    {
        playerIdle.localScale = new Vector3(1f, 1f, 1f);
        IdleAnim();
        
    }

    void IdleAnim() 
    {
        DOTween.Sequence()
            .Append(playerIdle.DOScaleY(1.1f,1f))
            .SetLoops(-1, LoopType.Yoyo);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
