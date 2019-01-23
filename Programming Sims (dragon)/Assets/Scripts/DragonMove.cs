﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonMove : MonoBehaviour {

    public Vector3 target;
    public NavMeshAgent agent;
    public LayerMask lM;
    public GameObject cam;

    public bool isWalking;

    public Animator a;

    public GameObject prefabEffect; //the effect that plays when you click somewhere//

	void Start () {
        cam = Camera.main.gameObject;
	}
	
	void Update ()
    { 
        if(Vector3.Distance(agent.transform.position,agent.destination) > 1.2f || agent.hasPath || agent.velocity.z > 0)
        {
            isWalking = true;
        }
        else                           // Sets the bool to see whether it is walking or not //
        {
            isWalking = false;
        }

        if (Input.GetButtonDown("Fire1")) // This part gets the position of the cursor and makes the dragon walk to it //
        {
            print("Fire");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, lM)) 
            {
                print("hit");
                target = hit.point;

                agent.SetDestination(target);
                GameObject selectionCircle = Instantiate(prefabEffect, hit.point,transform.rotation);
                Destroy(selectionCircle, 1);
                a.SetBool("Walking", true);
                isWalking = true;
            }
        }

        if (a.GetBool("Sleeping"))
        {
            a.SetBool("Walking", false);
        }

        if (!isWalking) // this part stops the walking animation when it has stopped walking //
        {
            if (!a.GetBool("Sleeping"))
            {
                a.SetBool("Walking", false);
            }
        }
        else
        {
            a.SetBool("Walking", true);
        }

        //print(a.get
	}
}