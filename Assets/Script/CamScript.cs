using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public GameObject player;
    public static CamScript cam;
    public float camY;
    public Vector2 countBefRota;
    public float currentCount;
    public int targetAngle;
    public AnimationCurve animCurve;
    public List<GameObject> allPlatforms;
    public bool camMoving;
    public bool platformMoving;
    // Start is called before the first frame update
    private void Awake()
    {
        if (cam == null)
            cam = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        player = GameObject.Find("Player");
        currentCount = Random.Range(countBefRota.x, countBefRota.y);
        targetAngle = Random.Range(-145, 145);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velo = Vector3.zero;
        float camX;
        camX = Vector2.SmoothDamp(transform.position, player.transform.position, ref velo, 0.05f).x;
        transform.position = new Vector3(camX, camY, -10);
        if (camMoving)
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
        currentCount = Random.Range(countBefRota.x, countBefRota.y);
        targetAngle = Random.Range(-145, 145);
        StartCoroutine(CamRotationEnum(Quaternion.Euler(new Vector3(0,0, targetAngle)), 2.0f));
    }
    private IEnumerator CamRotationEnum(Quaternion endValue, float duration)
    {
        float time = 0;
        float ratio = 0.0f;
        Quaternion startValue = transform.rotation;
        while (time < duration)
        {
            ratio = time / duration;
            ratio = animCurve.Evaluate(ratio);
            transform.rotation = Quaternion.Lerp(startValue, endValue, ratio);
            time += Time.deltaTime;
            yield return null;
        }
        yield return 0;
    }
}
