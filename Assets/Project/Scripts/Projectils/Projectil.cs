using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectil : MonoBehaviour
{
    public float life = 1, damage = 10, speed = 10;
    public GameObject impact;

    [HideInInspector] public Tower tower;



    protected virtual void Update() {

        life -= Time.deltaTime;
        if (life < 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    protected Enemy SearchEnemy(Transform body) {
        Transform parent = body.parent;
        if (parent) {
            Enemy enemy = parent.GetComponent<Enemy>();
            if (enemy) return enemy;
        }
        return null;
    }
}
