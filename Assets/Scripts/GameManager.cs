using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // �Q�[���{�[�h�̎Q��
    public GameBoard gameBoard;

    // �^�C���i�����v�j�̃v���n�u
    public GameObject tilePrefab;

    // UI�e�L�X�g
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI nextCharacterText;

    // ���݂̃v���C���[�i1 or 2�j
    private int currentPlayer = 1;

    // �g�p����Ђ炪�ȃ��X�g
    private List<string> hiraganaList = new List<string>
    {
        "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��",
        "��", "��", "��", "��", "��",
        "��", "��", "��"
    };

    // ���̂Ђ炪��
    private string nextHiragana;

    // ���݃v���C���[�����^�C��
    private GameObject currentTile;
    private CharacterTile currentTileScript;

    // �}�E�X�̈ʒu����̃��C�L���X�g�p
    private Camera mainCamera;

    void Start()
    {
        // ���C���J�����̎Q�Ƃ��擾
        mainCamera = Camera.main;

        // �ŏ��̃^�[���̏���
        PrepareNextTurn();
    }

    void Update()
    {
        // �}�E�X�ʒu�Ɍ��݂̃^�C����Ǐ]������
        if (currentTile != null)
        {
            // �}�E�X�ʒu�����[���h���W�ɕϊ�
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z���W��0�ɐݒ�

            // X���W���Q�[���{�[�h�͈͓̔��ɐ��� (��̒����ɔz�u)
            float clampedX = Mathf.Clamp(Mathf.Round(mousePosition.x), 0, gameBoard.width - 1);

            // �^�C���̈ʒu���X�V�i�{�[�h�̏�ɕ\���j
            currentTile.transform.position = new Vector3(clampedX, gameBoard.height + 1, 0);

            // ���N���b�N�Ń^�C�����h���b�v
            if (Input.GetMouseButtonDown(0)) // 0�͍��N���b�N
            {
                DropTile((int)clampedX);
            }
        }
    }

    // ���̃����_���ȂЂ炪�Ȃ�I��
    string GetRandomHiragana()
    {
        return hiraganaList[Random.Range(0, hiraganaList.Count)];
    }

    // �^�C���𗎂Ƃ�����
    void DropTile(int column)
    {
        if (currentTile == null) return;

        // �I��������Ƀ^�C����ǉ�
        bool success = gameBoard.AddTile(column, currentTile.transform);

        if (success)
        {
            // ���̃^�[���̏���
            PrepareNextTurn();
        }
    }

    // ���̃^�[���̏���
    void PrepareNextTurn()
    {
        // �v���C���[��؂�ւ�
        currentPlayer = (currentPlayer == 1) ? 2 : 1;

        // UI�X�V
        if (currentPlayerText != null)
        {
            currentPlayerText.text = "�v���C���[ " + currentPlayer + " �̔�";
        }

        // ���̂Ђ炪�Ȃ�I��
        nextHiragana = GetRandomHiragana();

        // UI�X�V
        if (nextCharacterText != null)
        {
            nextCharacterText.text = "���̕���: " + nextHiragana;
        }

        // �V�����^�C���𐶐�
        currentTile = Instantiate(tilePrefab);
        currentTileScript = currentTile.GetComponent<CharacterTile>();

        // �Ђ炪�Ȃ�ݒ�
        currentTileScript.SetCharacter(nextHiragana);
        currentTileScript.SetPlayer(currentPlayer);

        // �}�E�X�ʒu�Ƀ^�C����z�u�i�����ʒu�j
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float clampedX = Mathf.Clamp(Mathf.Round(mousePosition.x), 0, gameBoard.width - 1);
        currentTile.transform.position = new Vector3(clampedX, gameBoard.height + 1, 0);
    }
}