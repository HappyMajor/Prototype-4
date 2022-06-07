using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public GameObject explode;
    public GameObject aura1;
    public GameObject aura2;
    public void PlayExplode()
    {
        Instantiate(explode);
    }

    public void PlayAura1()
    {
        Instantiate(aura1);
    }

    public void PlayAura2()
    {
        Instantiate(aura2);
    }
}
