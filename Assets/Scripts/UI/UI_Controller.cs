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

    public GameObject MapGenerator;

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
            
           }
        }
    }

    public void SetBookOpen()
    {
        BookAnimator.SetBool("Open", true);
    }

    public void OpenPage()
    {
        pageCount++;
        Page[pageCount].SetActive(true);
        
    }


    public void RetryGeneration()
    {
        MapGenerator.GetComponent<Parchment>().GenerateMap();
    }

    public void Continue()
    {
        // Set up difficulty and settings
        Page[pageCount].SetActive(false);
        BookAnimator.SetTrigger("NextPage");

    }

    public void Settings()
    {
        // Close book and game
    }

    public void resetNextPageTrigger()
    {
        BookAnimator.ResetTrigger("NextPage");
    }

}
