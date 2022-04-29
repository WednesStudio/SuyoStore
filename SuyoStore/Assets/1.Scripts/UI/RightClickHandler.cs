using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class RightClickHandler : MonoBehaviour, IPointerClickHandler 
{
    UIManager ui;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ui = FindObjectOfType<UIManager>();
            ui.ChangeItemState(gameObject);
            print(gameObject.name);
            SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        }
    }
}
