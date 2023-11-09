using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator anim;

    float maxTime = 0.5f;
    public float curTime = 0f;

    private void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        while(curTime <= maxTime)
        {
            curTime += Time.deltaTime;
            anim.SetBool("Run", false);
            Debug.Log("AnimRun");
            yield return null;
        }
    }
}
