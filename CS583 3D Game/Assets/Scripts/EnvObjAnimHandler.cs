using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvObjAnimHandler : MonoBehaviour
{
    private Animator anim;
    private AngleToPlayer angleToPlayer;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();
    }
    
    void Update()
    {   
        anim.SetFloat("spriteRot", angleToPlayer.lastIndex); //handles which animation to play based on direction facing player

        //animations called later will have correct index
    }
}
