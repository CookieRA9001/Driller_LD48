using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button Restart;
    public bool isCheckPoint;
    GameObject player;
    CheckPoint Point;
    public bool isDeathMessage;
    private void Start(){
        if (isDeathMessage) { 
            player = GameObject.FindGameObjectWithTag("Player");
            if (!isCheckPoint) { 
                if (PlayerPrefs.GetInt("Saved") == 1) PlayerPrefs.DeleteAll();
                Destroy(Restart.gameObject);
                Destroy(player);
            } 
            else {
                Point = player.GetComponent<PlayerStats>().chechPoint;
                player.SetActive(false);
            }   
        }
    }
    public void SampleScene(){
        FindObjectOfType<AudioManeger>().Play("Click");
        if (isDeathMessage) {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<SavingScript>().itemMenuManager.ClearPrices();
            if (PlayerPrefs.GetInt("Saved") == 1) PlayerPrefs.DeleteAll();
        }
        SceneManager.LoadScene("SampleScene");
    }
    public void Menu(){
        FindObjectOfType<AudioManeger>().Play("Click");
        SceneManager.LoadScene("MainMenu");
    }
    public void Respawn() {
        FindObjectOfType<AudioManeger>().Play("Click");
        player.SetActive(true);
        Point.Respawn(player);
        Destroy(gameObject);
    }
}
