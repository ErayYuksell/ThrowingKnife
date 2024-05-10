using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5;
    [SerializeField] bool isTheFirstKnife;

    public bool forward;
    bool targetDone;


    void Update()
    {
        if (isTheFirstKnife)
        {
            return;
        }
        if (!forward)
        {
            transform.Rotate(rotateSpeed * new Vector3(90, 0, 0) * Time.deltaTime, Space.World);
        }
        else
        {
            if (!targetDone)
            {
                transform.Translate(30 * Time.deltaTime * Vector3.left, Space.World);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTheFirstKnife)
        {
            return;
        }
        if (other.CompareTag("Arrival"))
        {
            transform.SetPositionAndRotation(other.transform.position, other.transform.rotation);
            targetDone = true;
            GameManager.instance.KnifeStabbed();
        }
        else if (other.CompareTag("Final"))
        {
            transform.SetPositionAndRotation(other.transform.position, other.transform.rotation);
            targetDone = true;
            GameManager.instance.KnifeStabbed();
            GameManager.instance.WinWin();
        }
    }
}
