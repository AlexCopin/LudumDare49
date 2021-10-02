using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public GameObject gravityCenter;
    enum GravityDirection { Down, Left, Up, Right };
    GravityDirection gravityDirection;
    public GameObject[] allFloors;
    public Vector2 dirGravity;
    public GameObject nearest = null;
    float dist = float.PositiveInfinity;
    float delay = 0;
    public bool isGravityOnCenter;
    // Start is called before the first frame update
    void Awake()
    {
        gravityCenter = GameObject.Find("GravityCenter");
        gravityDirection = GravityDirection.Down;
        allFloors = GameObject.FindGameObjectsWithTag("GravityWall");
    }
    private void Update()
    {
        if (isGravityOnCenter)
        {
            CaseGravityCenter();
        }
        else
        {
            dist = float.PositiveInfinity;
            foreach (GameObject g in allFloors)
            {
                float d = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), Physics2D.ClosestPoint(transform.position, g.GetComponent<BoxCollider2D>()));
                //Debug.Log(g.name + " - " + d + " ---- " + dist);
                if (d < dist)
                {
                    nearest = g;
                    dist = d;
                }
            }
            dirGravity = new Vector2(Physics2D.ClosestPoint(transform.position, nearest.GetComponent<BoxCollider2D>()).x - transform.position.x,
                                        Physics2D.ClosestPoint(transform.position, nearest.GetComponent<BoxCollider2D>()).y - transform.position.y);
            dirGravity.Normalize();
            Physics2D.gravity = dirGravity * 9.81f;
            Debug.DrawLine(Physics2D.ClosestPoint(transform.position, nearest.GetComponent<BoxCollider2D>()), transform.position);
        }

        UpdateRotation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*switch (gravityDirection)
        {
            case GravityDirection.Down:
                Physics2D.gravity = new Vector2(0, -9.8f);
                break;
            case GravityDirection.Up:
                Physics2D.gravity = new Vector2(0, 9.8f);
                break;
            case GravityDirection.Left:
                Physics2D.gravity = new Vector2(-9.8f, 0);
                break;
            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(9.8f, 0);
                break;
        }*/
    }

    void CaseGravityCenter()
    {
        dirGravity = gravityCenter.transform.position - transform.position;
        dirGravity.Normalize();
        Physics2D.gravity = dirGravity * 9.81f;
    }

    void UpdateRotation()
    {
        float angle = Vector2.SignedAngle(Vector2.down, dirGravity);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(Physics2D.ClosestPoint(transform.position, nearest.GetComponent<BoxCollider2D>()), new Vector3(0.5f, 0.5f, 0.5f));
    }
}
