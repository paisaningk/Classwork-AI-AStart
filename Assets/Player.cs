using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Tracker;
    public int MaxFood = 1;
    public int CurrentFood;
    public int Speed;
    public float RotSpeed;
    public int Score;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Tracker.transform.position);
        
        if (distance < 0.2)
        {
            return;
        }
        
        Quaternion loolatWp = Quaternion.LookRotation( Tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, loolatWp, RotSpeed * Time.deltaTime);
        transform.Translate(0,0,Speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shop"))
        {
            if (CurrentFood < MaxFood)
            {
                Debug.Log("adc");
                CurrentFood = MaxFood;
            }
        }

        if (other.CompareTag("Waypoint"))
        {
            var customer = other.GetComponent<Customer>();
            
            if (customer.IsEnable)
            {
                if (CurrentFood > 0)
                {
                    Score++;
                    CurrentFood -= 1;
                    customer.IsEnable = false;
                    customer.Line01.SetActive(false);
                }
            }
        }
    }
}
