using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpGas : MonoBehaviour
{
    public float liveTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countTime());
    }

    private IEnumerator countTime()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(gameObject);
    }
}