using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaveOutGameManager : BaseGameManager
{
    //使用するイラスト
    Sprite sprite_Correct;
    Sprite sprite_Incorrect;

    //画面上にあるアイコン
    Image[] iconList;

    GameObject[] buttonList;

    // COMMENT_KUWABARA iconList、buttonListといった変数名の方が望ましいです.

    //イラストの数（イラストの通し番号の最も大きい数）
    public int spriteMax;

    bool buttonDown = false;

    public override void Arrangements()
    {
        GameObject iconParent = GameObject.Find("IconParent");
        GameObject buttonParent = GameObject.Find("ButtonParent");
        if (!iconParent || iconParent.transform.childCount <= 0)
        {
            return;
        }

        int childCount = iconParent.transform.childCount;
        iconList = new Image[childCount];
        buttonList = new GameObject[childCount];

        for (int n = 0; n < childCount; n++)
        {
            Transform childTransform = iconParent.transform.GetChild(n);
            GameObject childObject= childTransform.gameObject;
            Transform childTransform_Button = buttonParent.transform.GetChild(n);
            GameObject childObject_Button = childTransform_Button.gameObject;

            iconList[n] = childObject.GetComponent<Image>();
            buttonList[n] = childObject_Button;
        }

        RandomChange();
    }

    void RandomChange()
    {
        ButtonReset();

        int correnctIconId = Random.Range(0, iconList.Length);
        int TextureId = Random.Range(0, spriteMax + 1);
        // COMMENT_KUWABARA randomIconって、3 * 5で並んでいるアイコンの中で正解のアイコンはどれかを示しているのだと思いますので、
        // correnctIconIdといった名前にしてください。変数が指しているものが何なのか提示してください.

        // COMMENT_KUWABARA randomTextureについても、選択した後に、正解のテクスチャと、誤りのテクスチャがわかると思うので、それぞれを変数に納めてほしいですし、こちらもTextureIdといった名前の変数にしてほしいです.

        if (Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + TextureId))
        {
            sprite_Correct = Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + TextureId);

            if (TextureId % 2 == 0)
            {
                TextureId++;
            }
            else
            {
                TextureId--;
            }
            sprite_Incorrect = Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + TextureId);
        }

        for (int i = 0; i < iconList.Length; i++)
        {
            if (i == correnctIconId)
            {
                iconList[i].sprite = sprite_Incorrect;
                buttonList[i].SetActive(true);
                continue;
            }

            iconList[i].sprite = sprite_Correct;
        }
    }

    void ButtonReset()
    {
        for (int n = 0; n < buttonList.Length; n++)
        {
            buttonList[n].SetActive(false);
        }
    }

    public void Correct()
    {
        if (inGameEnable && !buttonDown)
        {
            AddScore();

            RandomChange();

            buttonDown = true;
        }
    }

    public void Incorrect()
    {
        if (inGameEnable && !buttonDown)
        {
            LessTime();

            buttonDown = true;
        }
    }

    public void ButtonDownFLG()
    {
        buttonDown = false;
    }
}