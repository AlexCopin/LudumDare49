using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    GameObject player;
    public float camY;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 velo = player.GetComponent<Rigidbody2D>().velocity;
        Vector2 velo = Vector3.zero;
        float camX;
        camX = Vector2.SmoothDamp(transform.position, player.transform.position, ref velo, 0.05f).x;
        transform.position = new Vector3(camX, camY, -10);
        //transform.position = new Vector3(player.transform.position.x, );
    }
    private IEnumerator CamRotation()
    {
        float targetAngle = 90;
        float turnSpeed = 5;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);
        yield return 0;
    }
}
