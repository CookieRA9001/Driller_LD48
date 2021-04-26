using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HartCounter : MonoBehaviour
{
    public Sprite heart_sprite, heartGone_sprite;
    public float incrementX = 50;
    public float incrementY = 50;
    private List<GameObject> heartsObj = new List<GameObject>();
    private int pozitiveHeartsIndex = -1;
    private int negativeHeartsIndex = 0;
    private Vector3 heartpos;
    private UpgradeScript uScript = null;
    private void Start()
    {
        uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        heartpos = gameObject.transform.position;
    }
    public void UpdateHearts(int hearts, int maxhearts) {
        heartpos = gameObject.transform.position;

        if (heartsObj.Count < maxhearts) {
            for (int i = heartsObj.Count; i < maxhearts; i++) {
                heartsObj.Add(new GameObject());
                if (i < negativeHeartsIndex) {
                    negativeHeartsIndex = i;
                }
                heartsObj[i].transform.parent = gameObject.transform;
                heartsObj[i].transform.position = new Vector3(heartpos.x + incrementX*(i%10), heartpos.y + -incrementY*(Mathf.Floor(i/10)), heartpos.z);
                heartsObj[i].AddComponent<Image>();
                heartsObj[i].GetComponent<Image>().sprite = heartGone_sprite;
                heartsObj[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            } 
        } else if (heartsObj.Count > maxhearts) {
            for (int i = heartsObj.Count - 1; i >= maxhearts; i--) {
                if (i < negativeHeartsIndex) {
                    negativeHeartsIndex = i;
                }
                Destroy(heartsObj[i]);
                heartsObj.RemoveAt(i);
            }
        }

        if (pozitiveHeartsIndex + 1 > hearts) {
            int temp = negativeHeartsIndex - 1;
            for (int i = temp; i + 1 > hearts; i--) {
                heartsObj[i].GetComponent<Image>().sprite = heartGone_sprite;
                pozitiveHeartsIndex--;
                negativeHeartsIndex = i;
            }
        } else if (pozitiveHeartsIndex + 1 < hearts) {
            for (int i = pozitiveHeartsIndex + 1; i < hearts; i++) {
                heartsObj[i].GetComponent<Image>().sprite = heart_sprite;
                pozitiveHeartsIndex++;
                negativeHeartsIndex++;
            }
        }
        if (uScript != null) {
            uScript.UpdateTexts(6);
            uScript.UpdateTexts(5);
        }
        
        /*foreach(GameObject go in hearts) {
            Destroy(go);
        }
        for (int i =1; i<=maxharts; i++) {
            hearts[i].transform.parent = gameObject.transform;
            Vector3 hGotp = harts_GO[harts_GO.Count - 1].transform.position;
            harts_GO[harts_GO.Count - 1].transform.position = new Vector3(hGotp.x + (i-1)*transform.lossyScale.x, hGotp.y + (Mathf.Floor(i/6))*transform.lossyScale.y, hGotp.z);
            Image sr = harts_GO[harts_GO.Count - 1].AddComponent<Image>();
            if (i<=harts) {
                sr.sprite = hart_srpite;
            }
            else {
                sr.sprite = hartGone_sprite;
            }
        }*/
    }


}
