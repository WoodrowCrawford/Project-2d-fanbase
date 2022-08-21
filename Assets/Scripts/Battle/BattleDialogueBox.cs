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
    private Color _highlightedColor;

    [SerializeField]
    private TMP_Text _dialogueText;

    [SerializeField]
    private GameObject _actionSelector;

    [SerializeField]
    private GameObject _moveSelector;

    [SerializeField]
    private GameObject _moveDetails;

    [SerializeField]
    private List<TMP_Text> _actionTexts;

    [SerializeField]
    private List<TMP_Text> _moveTexts;

    [SerializeField]
    private TMP_Text _ppText;

    [SerializeField]
    private TMP_Text _typeText;

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

        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogueText(bool enabled)
    {
        _dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        _actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled)
    {
        _moveSelector.SetActive(enabled);
        _moveDetails.SetActive(enabled);
    }


    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < _actionTexts.Count; i++)
        {
            //Set the selected color to the highlighted color
            if (i == selectedAction)
            {
                _actionTexts[i].color = _highlightedColor;
            }
            //Otherwise the text is black
            else
            {
                _actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < _moveTexts.Count; ++i)
        {
            if (i == selectedMove)
            {
                _moveTexts[i].color = _highlightedColor;
            }
            else
            {
                _moveTexts[i].color = Color.black;
            }
        }

        _ppText.text = $"PP {move.PP}/ {move.Base.PP}";
        _typeText.text = move.Base.Type.ToString();
    }


    public void SetMoveNames(List<Move> moves)
    {
        for(int i = 0; i < _moveTexts.Count; i++)
        {
            if(i < moves.Count)
            {
                _moveTexts[i].text = moves[i].Base.name;
            }
            else
            {
                _moveTexts[i].text = "-";
            }
        }
    }
}
