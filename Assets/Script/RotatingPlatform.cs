using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float currentCount;
    public int targetAngle;
    // Start is called before the first frame update
    void Start()
    {
        currentCount = Random.Range(CamScript.cam.countBefRota.x, CamScript.cam.countBefRota.y);
        targetAngle = Random.Range(-45, 45);
    }

    void Update()
    {
        if (CamScript.cam.platformMoving)
        {
            currentCount -= Time.deltaTime;
            if (currentCount <= 0)
            {
                CamRotation();
            }
        }
    }

    private void CamRotation()
    {
        currentCount = Random.Range(CamScript.cam.countBefRota.x, CamScript.cam.countBefRota.y);
        targetAngle = Random.Range(-135, 135);
        Vector3 newAngle = new Vector3(0, 0, targetAngle);
        StartCoroutine(CamRotationEnum(Quaternion.Euler(newAngle), 2.0f));
    }
    private IEnumerator CamRotationEnum(Quaternion endValue, float duration)
    {
        float time = 0;
        float ratio = 0.0f;
        Quaternion startValue = transform.rotation;
        while (time < duration)
        {
            ratio = time / duration;
            ratio = CamScript.cam.animCurve.Evaluate(ratio);
            transform.rotation = Quaternion.Lerp(startValue, endValue, ratio);
            time += Time.deltaTime;
            yield return null;
        }
        yield return 0;
    }
}
