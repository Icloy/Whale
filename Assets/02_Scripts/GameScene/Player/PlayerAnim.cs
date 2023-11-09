using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator anim;

    float maxTime = 0.25f;
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
            Debug.Log("AnimRun");
            anim.SetBool("Run", true);
            yield return null;
        }
        anim.SetBool("Run", false);
        isCoroutineRunning = false;
    }
}
