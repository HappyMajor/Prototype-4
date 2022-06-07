using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRb;
    private bool hasPowerup = false;
    private float powerupStrength = 22.0f;
    public float speed = 1.0f;
    public GameObject powerupIndicator;
    public float maxAngularVelocity = 30.0f;
    public VFXController vfxController;
    public GameObject youLostPanel;

    public float rotationSpeed = 1.0f;
        // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.maxAngularVelocity = maxAngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * vertical * speed);
        playerRb.AddForce(Vector3.right * horizontal * speed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddTorque(Vector3.up * rotationSpeed);
        }

        powerupIndicator.transform.position = transform.position;

        if (transform.position.y < -10)
        {
            youLostPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("powerup")) {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            if(hasPowerup)
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength * (playerRb.angularVelocity.magnitude), ForceMode.Impulse);
                vfxController.PlayExplode();
            } else
            {
                enemyRigidbody.AddForce(awayFromPlayer * (playerRb.angularVelocity.magnitude), ForceMode.Impulse);
            }
        }
    }

    IEnumerator PowerupCountdownRoutine() {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
