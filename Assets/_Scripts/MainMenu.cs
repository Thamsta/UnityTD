using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject[] pages;
    public GameObject levelHud;
     
    //TODO: linked-list-like implementation for level pages? functions to check whether there's a previous/next level page so buttons can be properly dis/enabled - and a back to menu button, left/right button menu steering
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            SetActivePage(0);
        }
    }

    public void OpenLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetActivePage(string page)
    {
        foreach(GameObject g in pages)
        {
            if(g.name == page)
            {
                g.SetActive(true);
                levelHud.SetActive(g.name.Contains("Level"));
            }
            else
            {
                g.SetActive(false);
            }
        }
    }

    public void SetActivePage(int index)
    {
        foreach (GameObject g in pages)
        {
            g.SetActive(false);
        }
        pages[index].SetActive(true);
        levelHud.SetActive(pages[index].name.Contains("Level"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}