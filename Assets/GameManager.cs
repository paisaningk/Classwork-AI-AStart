using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[Serializable]
public struct Link
{
    public enum Direction
    {
        Uni,
        Bi
    };

    public GameObject Node1;
    public GameObject Node2;
    public Direction Dir;
}

public class GameManager : MonoBehaviour
{
    public GameObject[] WayPoint;
    public Link[] Links;
    public List<GameObject> Customers;
    public Camera[] Camera;
    public Graph Graph = new Graph();
    public LayerMask Waypoint;
    public GameObject TrackerGameObject;
    public GameObject PlayerGameObject;
    public Tracker Tracker;
    public float StartTime = 1;
    public float NextTime;
    public float CurrentTime;
    private GameObject hitgameObject = null;
    public bool ReachTheDestination = true;
    public GameObject Line;

    public void Start()
    {
        Tracker = TrackerGameObject.GetComponent<Tracker>();
        CurrentTime = StartTime;
        Time.timeScale = 1;
        if (WayPoint.Length > 0)
        {
            SetupNodeAndLink();
        }

    }

    public void Update()
    {
        Graph.debugDraw();
        Ray rayCamera0 = Camera[0].ScreenPointToRay(Input.mousePosition) ;
        Ray rayCamera1 = Camera[1].ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (ReachTheDestination)
            {
                if (Physics.Raycast(rayCamera0, out hit,Mathf.Infinity,Waypoint))
                {
                    CheckHit(hit.transform.gameObject);
                }
                if (Physics.Raycast(rayCamera1, out hit,Mathf.Infinity,Waypoint))
                {
                    CheckHit(hit.transform.gameObject);
                }
            }
        }

        if (hitgameObject != null)
        {
            var distance = Vector3.Distance(PlayerGameObject.transform.position, hitgameObject.transform.position);
            if (distance < 1.2)
            {
                hitgameObject.GetComponent<Renderer>().material.color = Color.white;
                var a = hitgameObject.transform.gameObject.GetComponent<Customer>();
                a.IsSelect = false;
                ReachTheDestination = true;
            }
        }

        if (CurrentTime < Time.time)
        {
            var a = Random.Range(0, Customers.Count);
            Customers[a].gameObject.GetComponent<Customer>().IsEnable = true;
            CurrentTime += NextTime;
        }

    }

    public void SetupNodeAndLink()
    {
        foreach (GameObject waypoint in WayPoint)
        {
            Graph.AddNode(waypoint);
            if (waypoint.CompareTag("Waypoint"))
            {
                Customers.Add(waypoint);
                waypoint.AddComponent<Customer>();
                waypoint.AddComponent<Rigidbody>();
                waypoint.GetComponent<Customer>().Line = Line;
            }
        }

        foreach (var link in Links)
        {
            Graph.AddEdge(link.Node1,link.Node2);
            if (link.Dir == Link.Direction.Bi)
            {
                Graph.AddEdge(link.Node2,link.Node1);
            }
        }
    }

    public void CheckHit(GameObject hit)
    {
        var hitGameObject = hit.transform.gameObject;
        Tracker.Move(hitGameObject);
        hitGameObject.GetComponent<Renderer>().material.color = Color.red;
        ReachTheDestination = false;
                    
        if (hitGameObject.CompareTag("Waypoint"))
        {
            hitGameObject.GetComponent<Customer>().IsSelect = true;
            hitgameObject = hitGameObject;
        }
    }
}
