using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator anim;

    float maxTime = 0.5f;
    public float curTime = 0f;
    private bool isCoroutineRunning = false;

    public void AnimRun()
    {
        curTime = 0f;
        if (isCoroutineRunning)
        {
            return; 
        }
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        isCoroutineRunning = true;
        while (curTime <= maxTime)
        {
            curTime += Time.deltaTime;
            anim.SetBool("Run", false);
            Debug.Log("AnimRun");
            yield return null;
        }
        isCoroutineRunning = false;
    }
}
