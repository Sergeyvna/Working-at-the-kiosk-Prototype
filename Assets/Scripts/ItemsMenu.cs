using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsMenu : MonoBehaviour
{

    private bool sliderAnim;
    private Animator animator;

    void Awake() 
    {
        sliderAnim = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            sliderAnim = !sliderAnim;
            animator.SetBool("openItemMenu", sliderAnim);
        }
    }
}
