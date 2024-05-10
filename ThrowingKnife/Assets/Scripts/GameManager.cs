using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public class Blocks
{
    public GameObject block;
    public GameObject knifeHole;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] knifes;
    [SerializeField] List<Blocks> blocks;

    bool isGameDone;
    int knifePoolIndex;
    float currentYPosition = 0.75f;

    [SerializeField] CameraController cameraController;
    [SerializeField] MainBallController mainBallController;

    TimeSpan time;
    [SerializeField] TextMeshProUGUI countDownTime;
    public int duration;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        knifes[knifePoolIndex].transform.position = new Vector3(2.5f, currentYPosition, 0);
        knifes[knifePoolIndex].SetActive(true);
        StartCoroutine(CountDown());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameDone)
        {
            knifes[knifePoolIndex].GetComponent<KnifeController>().forward = true;
            knifePoolIndex++;
            currentYPosition += 0.5f;

            if (knifePoolIndex <= knifes.Length - 1)
            {
                knifes[knifePoolIndex].transform.position = new Vector3(2.5f, currentYPosition, 0);
                knifes[knifePoolIndex].SetActive(true);
            }
        }
        //if (Input.touchCount > 0 && Input.touchCount == 1)
        //{
        //    Touch touch= Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        knifes[knifePoolIndex].GetComponent<KnifeController>().forward = true;
        //        knifePoolIndex++;
        //        currentYPosition += 0.5f;

        //        if (knifePoolIndex <= knifes.Length - 1)
        //        {
        //            knifes[knifePoolIndex].transform.position = new Vector3(2.5f, currentYPosition, 0);
        //            knifes[knifePoolIndex].SetActive(true);
        //        }
        //    }
        //}
    }

    IEnumerator CountDown()
    {
        time = TimeSpan.FromSeconds(duration);
        countDownTime.text = time.ToString(@"mm\:ss");

        while (true)
        {
            yield return new WaitForSeconds(1);

            if (duration != 0)
            {
                duration--;
                time = TimeSpan.FromSeconds(duration);
                countDownTime.text = time.ToString(@"mm\:ss");

                if (duration == 0)
                {
                    Lose("timeDone");
                    yield break;
                }
            }
        }
    }
    public void KnifeStabbed()
    {
        blocks[knifePoolIndex - 1].knifeHole.SetActive(true);
        cameraController.targets[0] = blocks[knifePoolIndex - 1].block.transform;
    }
    public void WinWin()
    {
        mainBallController.gameObject.SetActive(false);
    }
    public void Lose(string state = "ballExploded")
    {
        isGameDone = true;
        knifes[knifePoolIndex].SetActive(false);

        countDownTime.gameObject.SetActive(false);
        StopAllCoroutines();

        if (state == "timeDone")
        {
            mainBallController.gameObject.SetActive(false);
        }
    }

   
}
