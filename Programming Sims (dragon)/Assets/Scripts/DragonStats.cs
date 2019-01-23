using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonStats : MonoBehaviour {

    // all these values are for the stats //
    [Range(0,100)]
    public float hunger;
    public Slider hungerSlider;
    public Text hungerText;
    public StatFiller h; //hunger
    [Range(0, 100)]
    public float thirst;
    public Slider thirstSlider;
    public Text thirstText;
    public StatFiller t; //thirst / drinking
    [Range(0, 100)]
    public float sleep;
    public Slider sleepSlider;
    public Text sleepText;
    public StatFiller s; //sleeping

    public StatFiller f; //playing / firing

    [Space]

    // tooltip explains it //
    [Tooltip("Loss per second")]
    public float hungerLoss;
    [Tooltip("Loss per second")]
    public float thirstLoss;
    [Tooltip("Loss per second")]
    public float sleepLoss;

    [Space]

    // this is the min. value which something can go before the ai chooses to go there //
    public float minValue;
    public float playingThreshold = 80;
    public bool doesntWantToPlay;

    [Space]
    public UnityEngine.AI.NavMeshAgent agent;
    public Animator an; //the animator on DragonMove.cs

    private void Start()
    {
        SetValues(); // this prepares the values //
    }

    private void Update ()
    {
        hunger = hunger - (hungerLoss * Time.deltaTime);
        thirst = thirst - (thirstLoss * Time.deltaTime); //this is the loss of every value  per second//
        sleep = sleep - (sleepLoss * Time.deltaTime);

        SetValues();
        ControlStats(); //these refer to functions that maintain the stats//
	}

    public void ControlStats() // this makes the ai go to the place it needs for food, water or sleep //
    {
        if (hunger < minValue)
        {
            agent.SetDestination(h.transform.position);
            an.SetBool("Walking", true);
            doesntWantToPlay = false;
        }

        if (thirst < minValue)
        {
            agent.SetDestination(t.transform.position);
            an.SetBool("Walking", true);
            doesntWantToPlay = false;
        }

        if (sleep < minValue)
        {
            agent.SetDestination(s.transform.position);
            an.SetBool("Walking", true);
            doesntWantToPlay = false;
        }

        if(hunger > playingThreshold && thirst > playingThreshold && sleep > playingThreshold && !doesntWantToPlay) //this one is for when the ai has plenty of everything//
        {
            if (Vector3.Distance(gameObject.transform.position, f.transform.position) > 3)
            {
                agent.SetDestination(f.transform.position);
                an.SetBool("Walking", true);
                doesntWantToPlay = false;
            }
        }
    }

    public void SetValues() // this sets the ai values //
    {
        hungerSlider.value = hunger;
        hungerText.text = Mathf.Round(hunger).ToString() + "%";
        thirstSlider.value = thirst;
        thirstText.text = Mathf.Round(thirst).ToString() + "%";
        sleepSlider.value = sleep;
        sleepText.text = Mathf.Round(sleep).ToString() + "%";
    }
}
