using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectableOption : MonoBehaviour
{
    [SerializeField]
    private Image selectableOptionIcon;

    [SerializeField]
    private TMP_Text tmpTextField;

    public void SetSprite(Sprite sprite)
    {
        selectableOptionIcon.sprite = sprite;
    }

    public void SetText(string text)
    {
        tmpTextField.SetText(text);
    }
}
