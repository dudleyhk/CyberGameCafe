using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation_script : MonoBehaviour
{

    [SerializeField] Animator anim;

    private Vector3 lastPosition;
    private Vector3 currentPosition;

    void Start()
    {
		anim = GetComponent<Animator> ();
    }

    void Update()
    {

        float horizontal = 0;
        float vertical = 0;

        currentPosition = transform.position;

        if(currentPosition.x > lastPosition.x)
        {
            horizontal = 1;
        }
        else if (currentPosition.x < lastPosition.x)
        {
            horizontal = -1;
        }
        else if (currentPosition.y > lastPosition.y)
        {
            vertical = 1;
        }
        else if (currentPosition.y < lastPosition.y)
        {
            vertical = -1;
        }

        lastPosition = currentPosition;

        AnimatorUpdate(horizontal, vertical);

    }

    void AnimatorUpdate(float horizontal, float vertical)
    {
        anim.SetFloat("x", horizontal);
        anim.SetFloat("y", vertical);
    }

}
