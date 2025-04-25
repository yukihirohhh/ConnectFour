using UnityEngine;
using TMPro;

public class CharacterTile : MonoBehaviour
{
    // �^�C���ɕ\�����镶��
    private string character;

    // TextMeshPro �R���|�[�l���g
    private TextMeshPro textComponent;

    // ���L�v���C���[�i1�܂���2�j
    public int ownerPlayer;

    void Awake()
    {
        // TextMeshPro �R���|�[�l���g���擾
        textComponent = GetComponentInChildren<TextMeshPro>();
        if (textComponent == null)
        {
            Debug.LogError("TextMeshPro component not found!");
        }
    }

    // �������Z�b�g����
    public void SetCharacter(string newCharacter)
    {
        character = newCharacter;
        if (textComponent != null)
        {
            textComponent.text = character;
        }
    }

    // �������擾����
    public string GetCharacter()
    {
        return character;
    }

    // �v���C���[���Z�b�g����
    public void SetPlayer(int player)
    {
        ownerPlayer = player;

        // �v���C���[�ɂ���ĐF��ς���
        if (textComponent != null)
        {
            if (player == 1)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 1.0f); // �v���C���[1�̐F
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.7f, 0.7f); // �v���C���[2�̐F
            }
        }
    }
}