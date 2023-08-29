using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClickHandler : MonoBehaviour, IPointerClickHandler
{
    public bool check = false;
    int count = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        check = true;
        count++;
        Debug.Log("on");
    }
}
