using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gruntScript : MonoBehaviour
{
    public GameObject BalasPrefab;
    public GameObject john;
    public float LastShoot;
    private int Health = 3;

    private void Update()
    {
        if (john == null) return;

        Vector3 direction= john.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale= new Vector3(1.0f,1.0f,1.0f);
        else transform.localScale= new Vector3(1.0f,1.0f,1.0f);

        float distance = john.transform.position.x - transform.position.x;

        if (distance < 0.0f && Time.time > LastShoot +0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject Balas = Instantiate(BalasPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        Balas.GetComponent<balas>().SetDirection(direction);
    }
    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
