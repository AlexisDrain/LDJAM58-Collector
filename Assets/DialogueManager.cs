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
        } else if (newIndex == 2) {
            dialogueBox.SetActive(false);
            canSkipDialogue = 2f; // slightly longer
            StartCoroutine(DelayUntilShowBox2());
        } else if (newIndex == 3) {
            GameManager.SpawnLoudAudio(clipList[1]);
            nextDialogueBox = 4;
        } else if (newIndex == 4) {
            GameManager.SpawnLoudAudio(clipList[4]);
            GameManager.SwitchToCamera1();
            nextDialogueBox = 5;
        } else if (newIndex == 5) {
            // unused
        } else if (newIndex == 6) {
            GameManager.SpawnLoudAudio(clipList[1]);
            nextDialogueBox = 7;
        } else if (newIndex == 7) {
            GameManager.SpawnLoudAudio(clipList[3]);
            nextDialogueBox = -1;
        } else if (newIndex == 8) {
            GameManager.SpawnLoudAudio(clipList[0]);
            nextDialogueBox = 9;
        } else if (newIndex == 9) {
            GameManager.SpawnLoudAudio(clipList[2]);
            nextDialogueBox = 10;
        } else if (newIndex == 10) {
            GameManager.SpawnLoudAudio(clipList[4]);
            nextDialogueBox = -1;
        } else if (newIndex == 11) {
            GameManager.SpawnLoudAudio(clipList[0]);
            nextDialogueBox = 12;
        } else if (newIndex == 12) {
            GameManager.SpawnLoudAudio(clipList[2]);
            nextDialogueBox = 13;
        } else if (newIndex == 13) {
            GameManager.SpawnLoudAudio(clipList[4]);
            nextDialogueBox = -1;
        } else if (newIndex == 14) {
            GameManager.SpawnLoudAudio(clipList[0]);
            nextDialogueBox = -1;
        } else if (newIndex == 15) {
            GameManager.SpawnLoudAudio(clipList[1]);
            nextDialogueBox = 16;
        } else if (newIndex == 16) {
            GameManager.SpawnLoudAudio(clipList[3]);
            nextDialogueBox = 17;
        } else if (newIndex == 17) {
            GameManager.SpawnLoudAudio(clipList[1]);
            nextDialogueBox = 18;
        } else if (newIndex == 18) {
            GameManager.SpawnLoudAudio(clipList[3]);
            nextDialogueBox = -1;
        } else if (newIndex == 19) {
            // unused
        } else if (newIndex == 20) {
            nextDialogueBox = -1;
        } else if (newIndex == 21) {
            nextDialogueBox = -1;
        } else {
            nextDialogueBox = -1;
        }
    }

    public IEnumerator DelayUntilShowBox2() {
        yield return new WaitForSeconds(1.5f);

        dialogueBox.SetActive(true);
        GameManager.SpawnLoudAudio(clipList[0]);
        nextDialogueBox = 3;
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
            if (nextDialogueBox == 5) {
                GameManager.tutorialMove.SetActive(true); // this shows the Move tutorial as soon as the player is done with story.
                GameManager.playerInDialogue = false;
                dialogueBox.SetActive(false);
            }
            else if (nextDialogueBox != -1) {
                ShowDialogue(nextDialogueBox);
            } else {

                GameManager.playerInDialogue = false;
                dialogueBox.SetActive(false);
            }
        }
    }
}
