

using UnityEngine;

public class Bullet : Projectil
{
    public bool collisionBaseGround = false;


    protected virtual void Update()
    {
        float dist = speed * Time.deltaTime;

        int layerMask;
        if (collisionBaseGround) layerMask = ~ProjectMan.LayerMask_TowerGround & ~ProjectMan.LayerMask_ForceField;
        else                     layerMask = ~ProjectMan.LayerMask_TowerGround & ~ProjectMan.LayerMask_ForceField & ~ProjectMan.LayerMask_BaseGround;

        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out RaycastHit hit,
            dist,
            layerMask))
        {
            Enemy e = SearchEnemy(hit.collider.transform);
            if (e) e.TakeDamage(damage, tower);

            GameObject bulletImpact = Instantiate(impact, hit.point, transform.rotation);
            Destroy(bulletImpact, 1);
            
            Destroy(gameObject);
            return;
        }
        else transform.Translate(Vector3.forward * dist);



        base.Update();
    }
}
