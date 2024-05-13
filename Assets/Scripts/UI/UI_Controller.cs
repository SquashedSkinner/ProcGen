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

    [SerializeField]
    public GameObject[] Page;
    private int pageCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find The Book Sprite and Animator
        Book = this.gameObject;
        BookAnimator = Book.GetComponent<Animator>();

        // Set Page to 0
        pageCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (BookAnimator.GetBool("Open") == false)
        {
           if (Input.anyKeyDown)
           {
            BookAnimator.SetTrigger("Opened Book");
            pageCount++;
           }
        }
    }

    public void SetBookOpen()
    {
        BookAnimator.SetBool("Open", true);
    }

    public void OpenPage(int pageCount)
    {
        Page[pageCount].SetActive(true);
    }

}
