using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenMenu : MonoBehaviour
{
    public GameObject craftingMenu;
    public GameObject exitMenu;
    public GameObject craftingButton1;
    public GameObject craftingButton2;
    public EventSystem eventSystem;
    public GameObject firstSelected;
    private SavingScript saving;
    private void Start()
    {
        saving = gameObject.GetComponent<SavingScript>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (craftingMenu.activeSelf) {
                craftingMenu.SetActive(false);
                craftingButton1.SetActive(true);
                craftingButton2.SetActive(false);
                Time.timeScale = 1;
            } else {
                craftingMenu.SetActive(true);
                craftingButton1.SetActive(false);
                craftingButton2.SetActive(true);
                Time.timeScale = 0;
                eventSystem.SetSelectedGameObject(firstSelected);
                craftingMenu.GetComponent<ItemMenuManager>().UpdatePrices();
                saving.Save();
                saving.WriteSave();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (exitMenu.activeSelf) {
                exitMenu.SetActive(false);
                Time.timeScale = 1;
            } else {
                exitMenu.SetActive(true);
                Time.timeScale = 0; 
            }
        }
    }
}
