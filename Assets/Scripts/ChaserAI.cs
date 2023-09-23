using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaserAI : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float movespeed = 900f;
    public float nextWayPointDistance = 3.0f;

    Path path;
    int currentWayPoint;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 1f);
        
    }

    void UpdatePath()
    {
        if(seeker.IsDone()){
         seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint=0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(path == null)
        {
            return;
        }
        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2) path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction*movespeed*Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance< nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }
}
