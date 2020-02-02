using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    
    [ContextMenu("Shake camera")]
    public void DoShake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        var original = new Vector3(0, 0, -20f);
        float elapsed = 0;
        while (elapsed < shakeDuration)
        {
            transform.position = original + new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = original;
    }
}
