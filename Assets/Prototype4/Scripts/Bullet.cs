using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform enemy;
    private Rigidbody bulletRb;
    [SerializeField] private float speed = 800.0f;
    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
        StartCoroutine(BulletLife());
    }
    private Vector3 EnemyPos()
    {
        enemy = FindObjectOfType<Enemy>().transform;
        Vector3 lookDir = (enemy.position - transform.position).normalized;
        return lookDir;
    }
    private void ShootBullet()
    {
        bulletRb.AddForce(EnemyPos() * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("AngEnemy")))
            Destroy(gameObject);     
    }

    IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
