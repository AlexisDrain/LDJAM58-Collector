using TMPro;
using UnityEngine;


public class CopyPasteText : MonoBehaviour
{
    private TMP_InputField myText;
    void Awake()
    {
        myText = GetComponent<TMP_InputField>();
        myText.onTextSelection.AddListener(selectText);
    }

    private int SelectionStart { get; set; }
    private int SelectionEnd { get; set; }
    private string copyData;

    public void selectText(string str, int pos1, int pos2) {
        if (myText.isFocused) {
            SelectionStart = Mathf.Min(pos1, pos2);
            SelectionEnd = Mathf.Max(pos1, pos2);//minus one to convert from caret pos to character pos
            //SelectionEnd = Mathf.Max(pos1 - 1, pos2 - 1);//minus one to convert from caret pos to character pos
            copyData = myText.text.Substring(SelectionStart, Mathf.Max(1, SelectionEnd - SelectionStart));
            print($"selectionStart {SelectionStart} SelectionEnd {SelectionEnd} copyData {copyData}");
            
        }


    }
    public void Update() {
        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)
        || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand)) {
            if (Input.GetKeyDown(KeyCode.C)) {
                print("Copy to clipboard: " + copyData);
                GUIUtility.systemCopyBuffer = copyData; // set the clipboard
            }
        }
    }
}
