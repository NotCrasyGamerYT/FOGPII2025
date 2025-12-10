using UnityEngine;
using UnityEngine.UI;

public class TabControllers : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image[] tabImages;
    public GameObject[] pages;
    void Start()
    {
        ActiveTab(0);
    }

    // Update is called once per frame
    public void ActiveTab(int tabNO)
    {
        for(int i =0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        pages[tabNO].SetActive(true);
        tabImages[tabNO].color = Color.white;
    }
}
