using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject tracker;
    public int MaxFood = 1;
    public int CurrentFood;
    public int Speed;
    public float RotSpeed;
    public int Score;

    public void Start()
    {
        transform.position = tracker.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, tracker.transform.position);
        
        if (distance < 0.2)
        {
            return;
        }
        
        Quaternion loolatWP = Quaternion.LookRotation( tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, loolatWP, RotSpeed * Time.deltaTime);
        transform.Translate(0,0,Speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            Debug.Log(CurrentFood <= MaxFood);
            if (CurrentFood < MaxFood)
            {
                CurrentFood = MaxFood;
            }
        }

        if (other.CompareTag("Waypoint"))
        {
            var Customer = other.GetComponent<Customer>();
            
            if (Customer.IsEnable)
            {
                if (CurrentFood > 0)
                {
                    Score++;
                    CurrentFood -= 1;
                    Customer.IsEnable = false;
                    Customer.Line01.SetActive(false);
                }
            }
        }
    }
}
