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
    public Transform h;
    [Range(0, 100)]
    public float thirst;
    public Slider thirstSlider;
    public Text thirstText;
    public Transform t;
    [Range(0, 100)]
    public float sleep;
    public Slider sleepSlider;
    public Text sleepText;
    public Transform s;

    // these are the values used when he is playing with fire //
    [Space]
    public Transform f;
    public bool isFiring;
    public GameObject head;
    public Transform shootingTarget;
    public float shootCooldown;
    public ParticleSystem fireBall;
    private bool doesntWantToPlay; //best name 2019//

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

    [Space]
    public UnityEngine.AI.NavMeshAgent agent;
    public Animator an;

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
        FillStats();

        if (Input.GetMouseButtonDown(0))
        {
            doesntWantToPlay = false;
            StopCoroutine("GoToSleep"); //this will stop a timer//
        }
	}

    public void FillStats() //this increases the values when the ai is near their locations//
    {
        if (Vector3.Distance(h.position, gameObject.transform.position) < 2)
        {
            hunger = hunger + (3 * Time.deltaTime);
            hunger = Mathf.Clamp(hunger, 1, 100);
        }

        if (Vector3.Distance(t.position, gameObject.transform.position) < 3)
        {
            thirst = thirst + (3 * Time.deltaTime);
            thirst = Mathf.Clamp(thirst, 1, 100);
        }

        if(Vector3.Distance(f.position, gameObject.transform.position) < 3)
        {
            head.transform.LookAt(shootingTarget.transform);
            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine("Fireballs");
                StartCoroutine("GoToSleep");
            }
            else
            {
                StopCoroutine("Fireballs");
            }
        }
        else
        {
            isFiring = false;
            fireBall.Stop();
        }

        if (Vector3.Distance(s.position, gameObject.transform.position) < 3)
        {
            sleep = sleep + (3 * Time.deltaTime);
            sleep = Mathf.Clamp(sleep, 1, 100);
            an.SetBool("Sleeping", true);
        }
        else
        {
            if(Vector3.Distance(s.position,gameObject.transform.position) > 3)
            {
                an.SetBool("Sleeping", false);
            }
        }
    }

    public void LateUpdate()
    {
        if (Vector3.Distance(f.position, gameObject.transform.position) < 3)
        {
            head.transform.LookAt(shootingTarget.transform);
            head.transform.rotation = new Quaternion(head.transform.rotation.x, head.transform.rotation.y - 180, head.transform.rotation.z, 0);
        }
    }

    public void ControlStats() // this makes the ai go to the place it needs for food, water or sleep //
    {
        if (hunger < minValue)
        {
            agent.SetDestination(h.position);
            an.SetBool("Walking", true);
            doesntWantToPlay = false;
        }

        if (thirst < minValue)
        {
            agent.SetDestination(t.position);
            an.SetBool("Walking", true);
            doesntWantToPlay = false;
        }

        if (sleep < minValue)
        {
            agent.SetDestination(s.position);
            an.SetBool("Walking", true);
            doesntWantToPlay = false;
        }

        if(hunger > 80 && thirst > 80 && sleep > 80 && !doesntWantToPlay)
        {
            if (Vector3.Distance(gameObject.transform.position, f.position) > 3)
            {
                agent.SetDestination(f.position);
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

    public IEnumerator Fireballs() // this shoots the fireballs //
    {
        fireBall.Play();
        print("Fireball Shoot");
        yield return new WaitForSeconds(shootCooldown);
        StartCoroutine("Fireballs");
    }

    public IEnumerator GoToSleep() // this makes the ai rest after playing // 
    {
        yield return new WaitForSeconds(10);
        doesntWantToPlay = true;
        fireBall.Stop();
        agent.SetDestination(s.position);
        an.SetBool("Walking", true);
    }
}
