using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBallController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject[] balls;
    bool ballExploded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KnifeHandle") && !ballExploded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(0, Random.Range(2, 4), 0, ForceMode.Impulse);
        }
        else if (other.CompareTag("KnifeTip"))
        {
            ballExploded = true;
            GameManager.instance.Lose();
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.None;

            balls[0].SetActive(false);
            balls[1].SetActive(true);

            transform.position = new Vector3(0.7f, other.gameObject.transform.position.y, 0);
        }
    }
}
