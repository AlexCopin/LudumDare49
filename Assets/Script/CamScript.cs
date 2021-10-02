using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    GameObject player;
    public float camY;
    public Vector2 countBefRota;
    public float currentCount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        currentCount = Random.Range(countBefRota.x, countBefRota.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velo = Vector3.zero;
        float camX;
        camX = Vector2.SmoothDamp(transform.position, player.transform.position, ref velo, 0.05f).x;
        transform.position = new Vector3(camX, camY, -10);
        currentCount -= Time.deltaTime;
        if(currentCount <= 0)
        {
            CamRotation();
        }
    }

    private void CamRotation()
    {
        currentCount = Random.Range(countBefRota.x, countBefRota.y);
        StartCoroutine(CamRotationEnum());
    }
    private IEnumerator CamRotationEnum()
    {
        float targetAngle = 90;
        float turnSpeed = 5;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);
        yield return 0;
    }
}
