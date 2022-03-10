using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public GameManager GameManager;
    public float Speed = 5;
    public int StartWaypoint = 0;
    public float TrackDistanceStopWaitingPlayer;
    public Transform player;
    public GameObject currentNode;
    private GameObject[] waypoints;
    private Graph graph;
    private int currentWaypoint = 0;
    private GameObject goal;
    
    void Start()
    {
        graph = GameManager.Graph;
        waypoints = GameManager.WayPoint;
        currentNode = waypoints[StartWaypoint];
        transform.position = waypoints[StartWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.ReachTheDestination)
            {
                if (currentNode != waypoints[0])
                {
                    graph.AStar(currentNode,waypoints[0]);
                    waypoints[0].GetComponent<Renderer>().material.color = Color.red;
                    currentWaypoint = 0;
                }
            }
        }
    
        if (graph.getPathLength() == 0 || currentWaypoint == graph.getPathLength())
        {
            return;
        }
        currentNode = graph.getPathPoint(currentWaypoint);
        
        float distanWP = Vector3.Distance(transform.position, currentNode.transform.position);
        if (distanWP < 1)
        {
            currentWaypoint++;
        }
       
        float trackerdistance = Vector3.Distance(player.position, transform.position);
        if (trackerdistance > TrackDistanceStopWaitingPlayer)
        {
            return;
        }

        if (currentWaypoint < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypoint);
            transform.LookAt(goal.transform.position);
            transform.Translate(0,0,Speed * Time.deltaTime);
        }
    }

    public void Move(GameObject index)
    {
        graph.AStar(currentNode,index);
        currentWaypoint = 0;
    }
}
