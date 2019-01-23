using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatFiller : MonoBehaviour {

    //disclaimer: this is not actually the player but the player can control it//
    public GameObject player;
    public DragonStats dS;
    
    [Space]
    
    [Tooltip("The amount of points the specific value regenerates")]
    public float regenValue;

    [Tooltip("")]
    public float range = 2; //range in which the statfiller fills the stats//

    public bool inRange; //true when the ai is within the chosen range//

    public enum Resource //the two default types which do nothing inheritance worthy//
    {
        Hunger,
        Water
    }

    public Resource resource;
    
    public virtual void Update ()
    {
		if(Vector3.Distance(player.transform.position, transform.position) <= range)
        {
            AddValue(); //this functions has a different effect depending on the script it's in//
            inRange = true;
        }
        else
        {
            inRange = false;
        }
	}

    public virtual void AddValue() //this function will be altered via inheritance//
    {
        float amount;
        amount = regenValue * Time.deltaTime;

        switch (resource) //this makes sure the right values are changed//
        {
            case Resource.Hunger:
                dS.hunger += amount;
                Mathf.Clamp(dS.hunger, 0, 100);
                break;
            case Resource.Water:
                dS.thirst += amount;
                Mathf.Clamp(dS.thirst, 0, 100);
                break;
            default:
                break;
        }
    }
}
