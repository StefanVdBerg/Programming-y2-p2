using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunResource : StatFiller {

    // these are the values used when he is playing with fire //
    public bool isFiring;
    public GameObject head; //the object that aims to fire (in this case a particlesystem)
    public Transform shootingTarget; //target at which to shoot at//
    public float shootCooldown;
    public ParticleSystem fireBall; //the particlesystem which creates the fireballs (this could be changed to a prefab but why would I pass on using particles)
    private bool doesntWantToPlay; //best name 2019//

    [Space]

    public GameObject sleepLocation; //the location to walk to when the ai is tired

    public override void AddValue()
    {
        head.transform.LookAt(shootingTarget.transform);

        if (!isFiring) //this boolean makes sure this gets activated only once even if it would be called every frame
        {
            isFiring = true;
            StartCoroutine("Fireballs"); //starts the fireballs
            StartCoroutine("GoToSleep"); //starts the timer for getting tired.. I guess even AI's need some rest
        }
        else
        {
            StopCoroutine("Fireballs"); //stops the fireballs
        }

        if (Input.GetMouseButtonDown(0))
        {
            doesntWantToPlay = false;
            dS.doesntWantToPlay = false;
            StopCoroutine("GoToSleep"); //this will stop a timer//
        }
    }

    public void LateUpdate()
    {
        doesntWantToPlay = dS.doesntWantToPlay; //copies the boolean from another script//

        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < range) //this check whether it's in range and then makes the ai aim at it
        {
            head.transform.LookAt(shootingTarget.transform);
            head.transform.rotation = new Quaternion(head.transform.rotation.x, head.transform.rotation.y - 180, head.transform.rotation.z, 0);
        }
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
        dS.doesntWantToPlay = true;
        fireBall.Stop();
        dS.agent.SetDestination(sleepLocation.transform.position);
        dS.an.SetBool("Walking", true);
    }
}
