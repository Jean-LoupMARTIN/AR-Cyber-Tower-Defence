
using UnityEngine;



public abstract class TowerThrow : TowerLook
{
    public float attDamage = 1;
    public ParticleSystem[] atts;
    public AudioSource attSound;




    protected override void Awake()
    {
        base.Awake();
        foreach (ParticleSystem att in atts) att.name = "TowerThrow AttParticle";
    }


    protected override void Attack()   => ActiveAtt(true);
    protected override void NoAttack() => ActiveAtt(false);

    void ActiveAtt(bool active)
    {
        if (active != attSound.isPlaying)
        {
            if (active) { atts[v].Play(); attSound.Play(); }
            else        { atts[v].Stop(); attSound.Stop(); }
        }
    }
}
