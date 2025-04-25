using UnityEngine;
using TMPro;

public class CharacterTile : MonoBehaviour
{
    // タイルに表示する文字
    private string character;

    // TextMeshPro コンポーネント
    private TextMeshPro textComponent;

    // 所有プレイヤー（1または2）
    public int ownerPlayer;

    void Awake()
    {
        // TextMeshPro コンポーネントを取得
        textComponent = GetComponentInChildren<TextMeshPro>();
        if (textComponent == null)
        {
            Debug.LogError("TextMeshPro component not found!");
        }
    }

    // 文字をセットする
    public void SetCharacter(string newCharacter)
    {
        character = newCharacter;
        if (textComponent != null)
        {
            textComponent.text = character;
        }
    }

    // 文字を取得する
    public string GetCharacter()
    {
        return character;
    }

    // プレイヤーをセットする
    public void SetPlayer(int player)
    {
        ownerPlayer = player;

        // プレイヤーによって色を変える
        if (textComponent != null)
        {
            if (player == 1)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 1.0f); // プレイヤー1の色
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f); // プレイヤー2の色
            }
        }
    }
}