using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 100.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Enemies();
        OutOfRange();
    }
    void AttackOnPlayer(float speed)
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }
    void OutOfRange()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
    }
    private void Enemies()
    {
        if (gameObject.CompareTag("AngEnemy"))
            AttackOnPlayer(3 * enemySpeed);
        else if (gameObject.CompareTag("Enemy"))
            AttackOnPlayer(enemySpeed);
    }
}
