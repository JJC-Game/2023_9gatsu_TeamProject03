using UnityEngine;
using UnityEngine.EventSystems;

public class Tap : MonoBehaviour, IPointerClickHandler
{
    public GameObject seikai; // 正解のゲームUI
    public GameObject hazule; // ハズレのゲームUI

    public string tap;
    public void Awake()
    {
        seikai.SetActive(false);
        hazule.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // タップしたオブジェクトの名前に応じて処理を実行する
        GameObject tappedObject = eventData.pointerCurrentRaycast.gameObject;

        switch (tap)
        {
            case "タップ":
                if (tappedObject.name == "○○")
                {
                    seikai.SetActive(true);
                    Debug.Log("seikai");
                }
                else if (tappedObject.name == "××")
                {
                    hazule.SetActive(true);
                    Debug.Log("huseikai");
                }
                break;
            case "答えを消す":
                seikai.SetActive(false);
                hazule.SetActive(false);
                break;
        }
      
    }
}
