using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public static CharacterUI inst;

    public GameObject showButton;

    private ScrollRect scrollRect;
    private bool isActive;

    private void Awake()
    {
        inst = this;
        scrollRect = GetComponent<ScrollRect>();
    }

    public void OpenCharacterUI()
    {
        if (!isActive)
        {
            isActive = true;

            showButton.SetActive(!isActive);
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Character);
        }
    }

    public void CloseCharacterUI()
    {
        if (isActive)
        {
            isActive = false;

            showButton.SetActive(!isActive);
            gameObject.SetActive(isActive);

            GameManager.inst.ChangeState(State.Phone);
        }
    }
}
