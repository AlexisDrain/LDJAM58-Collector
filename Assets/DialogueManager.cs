using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [TextArea(3,3)]
    public List<string> dialogueList = new List<string>();
    public List<AudioClip> clipList = new List<AudioClip>();
    public GameObject dialogueBox;
    public TextMeshProUGUI textMeshProUGUI;

    public int nextDialogueBox = -1;

    private float canSkipDialogue;

    public void Start() {
        dialogueBox.SetActive(false);
    }

    public void ShowDialogue(int newIndex) {
        GameManager.playerInDialogue = true;
        dialogueBox.SetActive(true);
        textMeshProUGUI.text = dialogueList[newIndex];
        canSkipDialogue = 0.4f;

        // dialogue scripting goes here. If we need an event to happen BEFORE the player presses the dialogue close button.
        if (newIndex == 0) {
            GameManager.SpawnLoudAudio(clipList[0]);
            nextDialogueBox = 1;
        } else if (newIndex == 1) {
            GameManager.SpawnLoudAudio(clipList[2]);
            nextDialogueBox = -1;
        } else {
            nextDialogueBox = -1;
        }
    }
    public void FixedUpdate() {
        if(canSkipDialogue >= 0) {
            canSkipDialogue -= Time.deltaTime;
        }
    }
    public void Update() {
        if(GameManager.playerIsDead) {
            GameManager.playerInDialogue = false;
            dialogueBox.SetActive(false);
            nextDialogueBox = -1;
        }

        if(GameManager.playerInDialogue && canSkipDialogue <= 0f && Input.GetButtonDown("ProgressDialogue")) {

            // dialogue scripting goes here. If we need an event to happen AFTER the player presses the dialogue close button.

            if (nextDialogueBox != -1) {
                ShowDialogue(nextDialogueBox);
            } else {
                GameManager.playerInDialogue = false;
                dialogueBox.SetActive(false);
            }
        }
    }
}
