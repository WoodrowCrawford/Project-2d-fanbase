using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleDialogueBox : MonoBehaviour
{
    [SerializeField]
    private int _letterPerSecond;

    [SerializeField]
    private TMP_Text _dialogueText;

    public void SetDialogue(string dialogue)
    {
        _dialogueText.text = dialogue;
    }

    //Types the dialogue letter by letter
    public IEnumerator TypeDialoge(string dialogue)
    {
        _dialogueText.text = " ";

        foreach (var letter in dialogue.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(1f / _letterPerSecond);
        }
    }
}
