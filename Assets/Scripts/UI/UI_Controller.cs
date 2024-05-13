using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    private GameObject Book;
    private Animator BookAnimator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find The Book Sprite and Animator
        Book = this.gameObject;
        BookAnimator = Book.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BookAnimator.GetBool("Open") == false)
        {
           if (Input.anyKeyDown)
           {
            BookAnimator.SetTrigger("Opened Book");
           }
        }
    }

    public void SetBookOpen()
    {
        BookAnimator.SetBool("Open", true);
    }
}
