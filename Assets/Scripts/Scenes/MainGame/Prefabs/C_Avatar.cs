using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Avatar : MonoBehaviour
{
    C_Character character;

    public void Set(C_Character character)
    {
        this.character = character;
    }

    public void OnClick()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
        character.isUlti = true;
    }
}
