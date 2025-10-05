using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour
{
    public Sprite sprite_healthFull;
    public Sprite sprite_healthEmpty;

    public List<GameObject> sprites;

    public void UpdateHealthValue(int newHealth)
    {
        if (newHealth == 0) {
            sprites[0].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[1].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[2].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[3].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[4].GetComponent<Image>().sprite = sprite_healthEmpty;
        }
        else if (newHealth == 20) {
            sprites[0].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[1].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[2].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[3].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[4].GetComponent<Image>().sprite = sprite_healthEmpty;
        } else if (newHealth == 40) {
            sprites[0].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[1].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[2].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[3].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[4].GetComponent<Image>().sprite = sprite_healthEmpty;
        } else if (newHealth == 60) {
            sprites[0].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[1].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[2].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[3].GetComponent<Image>().sprite = sprite_healthEmpty;
            sprites[4].GetComponent<Image>().sprite = sprite_healthEmpty;
        } else if (newHealth == 80) {
            sprites[0].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[1].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[2].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[3].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[4].GetComponent<Image>().sprite = sprite_healthEmpty;
        } else if (newHealth == 100) {
            sprites[0].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[1].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[2].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[3].GetComponent<Image>().sprite = sprite_healthFull;
            sprites[4].GetComponent<Image>().sprite = sprite_healthFull;
        } else {
            print("Warning: player health is not divisible by 20!");
        }
    }
}
