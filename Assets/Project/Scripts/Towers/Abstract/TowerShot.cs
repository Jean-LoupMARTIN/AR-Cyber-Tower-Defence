

using UnityEngine;

public abstract class TowerShot<T> : TowerLook where T : Projectil
{
    public float dtShot = 1, damageProj = 10;
    protected float tNextShot;

    public T projectil;
    public ParticleSystem shotParticle;
    public Animator animator;



    void Start() { if (animator) animator.SetFloat("shotSpeed", 1.1f / dtShot); }





    protected override void Attack()
    {
        if (Time.time > tNextShot) {
            tNextShot = Time.time + dtShot;
            Shot();
        }
    }


    protected virtual void Shot()
    {
        Projectil p = Instantiate(projectil, firePoint.position, firePoint.rotation);
        p.damage = damageProj;
        p.tower = this;
        animator.SetTrigger("shot");

        ParticleSystem sp = Instantiate(shotParticle, firePoint.position, firePoint.rotation);
        Destroy(sp.gameObject, 1);
    }
}
