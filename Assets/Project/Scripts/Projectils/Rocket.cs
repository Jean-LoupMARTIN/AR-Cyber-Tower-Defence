

using UnityEngine;

public class Rocket : Projectil
{
    public float rotSpeed = 1, view = 1;
    public ParticleSystem smoke;


    protected override void Update()
    {
        float dist = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * dist);


        Enemy target;
        float distTarget;
        (target, distTarget) = Enemy.GetClosest(transform.position);

        if (target && distTarget < view) {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(Tool.Dir(this, target.gravity)),
                rotSpeed * Time.deltaTime);
        }

        base.Update();
    }


    private void OnTriggerEnter(Collider other)
    {
        Enemy e = SearchEnemy(other.transform);
        if (e) e.TakeDamage(damage, tower);

        GameObject i = Instantiate(impact, transform.position, transform.rotation);
        Destroy(i, 1);

        smoke.transform.parent = transform.parent;
        smoke.Stop();
        Destroy(smoke.gameObject, 1);

        Destroy(gameObject);
    }


}
