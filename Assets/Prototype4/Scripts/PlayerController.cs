using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float playerSpeed=5.0f;
    
    private GameObject focalPoint;

    private float powerUpStength = 15.0f;
    private bool hasPowerUp=false;
    [SerializeField] private GameObject powerUpIndicator;

    [SerializeField] private GameObject bullet;
    private bool isReadyToShoot=false;
    enum powerUP { 
        PushPower,
        ShootPower
    };
    powerUP powertype;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * playerSpeed * forwardInput*Time.deltaTime);
        powerUpIndicator.transform.position = transform.position +new Vector3(0,-.65f,0);

        ShootPower();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp")) 
        {
            hasPowerUp = true;
            powertype = powerUP.PushPower;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUPTime());
        }
        else if (other.CompareTag("PowerUp1"))
        {
            hasPowerUp = true;
            powertype = powerUP.ShootPower;
            isReadyToShoot = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUPTime());
        }
    }
    IEnumerator PowerUPTime()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("AngEnemy")) && hasPowerUp&&powertype==powerUP.PushPower)
        {
            Rigidbody enemyRb=collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyRb.AddForce(awayFromPlayer * powerUpStength, ForceMode.Impulse);
            Debug.Log("is collid a "+collision.gameObject.name+"with a power up is a "+ hasPowerUp);
        }
    }

    private void ShootPower()
    {
        if(hasPowerUp&&powertype==powerUP.ShootPower&&isReadyToShoot) 
        {

            Instantiate(bullet,transform.position+new Vector3(0,1,0), bullet.transform.rotation);
            isReadyToShoot = false;
            StartCoroutine(FiringRate());
        }
    }
    IEnumerator FiringRate()
    {
        yield return new WaitForSeconds(.4f);
        isReadyToShoot=true;
    }
}
