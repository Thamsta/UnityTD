using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject[] pages;
    public GameObject levelHud;
    private int activePage;
     
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

    public void SetActivePage(int index)
    {
        activePage = index;
        foreach (GameObject g in pages)
        {
            g.SetActive(false);
        }
        pages[index].SetActive(true);
        levelHud.SetActive(IsLevel(pages[index]));
        SetButtons();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="activePage">The index of the current active Page</param>
    void SetButtons()
    {
        Button nb = levelHud.transform.Find("Next").GetComponent<Button>();
        Button pb = levelHud.transform.Find("Previous").GetComponent<Button>();

        if(pages.Length - 1 > activePage)
        {
            nb.interactable = IsLevel(pages[activePage + 1]);
        }
        else
        {
            nb.interactable = false;
        }
        if(activePage > 0)
        {
            pb.interactable = IsLevel(pages[activePage - 1]);
        }
        else
        {
            pb.interactable = false;
        }
    }

    public void SetNextPage()
    {
        SetActivePage(activePage + 1);
    }

    public void SetPreviousPage()
    {
        SetActivePage(activePage - 1);
    }

    bool IsLevel(GameObject page)
    {
        return page.name.Contains("Level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}