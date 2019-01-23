using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatFiller : MonoBehaviour {

    public DragonStats dS;

    //disclaimer: this is not actually the player but the player can control it//
    public GameObject player;
    
    [Space]
    
    [Tooltip("The amount of points the specific value regenerates")]
    public float regenValue;

    [Tooltip("")]
    public float range = 2;

    public enum Resource
    {
        Hunger,
        Water 
        //the other two types are added via inheritance//
    }

    public Resource resource;

    void Update ()
    {
		if(Vector3.Distance(player.transform.position, transform.position) <= range)
        {
            AddValue();
        }
	}

    public void AddValue() //this function will be altered via inheritance//
    {
        float amount;
        amount = regenValue * Time.deltaTime;

        switch (resource)
        {
            case Resource.Hunger:
                dS.hunger += amount;
                break;
            case Resource.Water:
                dS.thirst += amount;
                break;
            default:
                break;
        }
    }
}
