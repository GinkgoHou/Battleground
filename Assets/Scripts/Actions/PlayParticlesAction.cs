using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(
    menuName = "CardGame/Actions/Play Particles Action",
    fileName = "PlayParticlesAction",
    order = 9)]

public class PlayParticlesAction : EffectAction
{
    public ParticleSystem Particles;
    public Vector3 Offset;

    public override string GetName()
    {
        return "Play Particles";
    }
   

    public override void Execute(GameObject gameObj)
    {
        var enemyPosition = gameObj.transform.position;
        var particles = Instantiate(Particles);
        // ֱ�ӽ���Чλ����Ϊ���˵�λ�ã�������������ƫ�ƣ�
        particles.transform.position = enemyPosition;
        particles.Play();

        var autoDestroy = particles.gameObject.AddComponent<AutoDestroy>();
        autoDestroy.Duration = 2.0f;
    }
}
