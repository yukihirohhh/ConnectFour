using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{
    // �{�[�h�̃T�C�Y
    public int width = 7;
    public int height = 5;

    // �e�Z���̈ʒu��ۑ�����2�����z��
    private Transform[,] grid;

    // �e��̌��݂̍�����ǐՁi���̐Εق��ǂ̍����ɔz�u����邩�j
    private int[] columnHeights;

    void Start()
    {
        // �O���b�h�̏�����
        grid = new Transform[width, height];
        columnHeights = new int[width];

        // �e��̍�����0�ɏ�����
        for (int i = 0; i < width; i++)
        {
            columnHeights[i] = 0;
        }

        // �{�[�h�̎��o�I�\�����쐬
        CreateBoardVisual();
    }

    // �{�[�h�����o�I�ɕ\������
    void CreateBoardVisual()
    {
        // �{�[�h�̔w�i���쐬
        GameObject boardBackground = new GameObject("BoardBackground");
        boardBackground.transform.parent = transform;

        SpriteRenderer boardRenderer = boardBackground.AddComponent<SpriteRenderer>();
        // �{�[�h�S�̂̔w�i�F�i�����F�j
        boardRenderer.color = new Color(0.8f, 0.8f, 1.0f);

        // �{�[�h�T�C�Y�ɍ��킹�ăX�P�[������
        boardRenderer.drawMode = SpriteDrawMode.Sliced;
        boardRenderer.size = new Vector2(width, height);

        // �{�[�h�̈ʒu����
        boardBackground.transform.position = new Vector3(width / 2.0f - 0.5f, height / 2.0f - 0.5f, 0);

        // �Z���̋�؂�����쐬
        for (int x = 0; x < width + 1; x++)
        {
            GameObject line = new GameObject("VerticalLine_" + x);
            line.transform.parent = boardBackground.transform;

            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(x - 0.5f, -0.5f, -0.1f));
            lineRenderer.SetPosition(1, new Vector3(x - 0.5f, height - 0.5f, -0.1f));
        }

        for (int y = 0; y < height + 1; y++)
        {
            GameObject line = new GameObject("HorizontalLine_" + y);
            line.transform.parent = boardBackground.transform;

            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(-0.5f, y - 0.5f, -0.1f));
            lineRenderer.SetPosition(1, new Vector3(width - 0.5f, y - 0.5f, -0.1f));
        }
    }

    // �w�肵����ɐΕق�ǉ�����
    public bool AddTile(int column, Transform tile)
    {
        // �񂪔͈͊O�܂��͊��ɗ񂪖��t�̏ꍇ
        if (column < 0 || column >= width || columnHeights[column] >= height)
        {
            return false;
        }

        // �Εق�K�؂Ȉʒu�ɔz�u
        int row = columnHeights[column];
        Vector3 position = new Vector3(column, row, 0);
        tile.position = position;

        // �O���b�h�ɐΕق�o�^
        grid[column, row] = tile;

        // ��̍������X�V
        columnHeights[column]++;

        // �P��̃`�F�b�N�i��Ŏ����j
        CheckForWords(column, row);

        return true;
    }

    // �P����`�F�b�N����i�����͌�̃X�e�b�v�Łj
    private void CheckForWords(int column, int row)
    {
        // �������Əc�����̒P��`�F�b�N
        // �P�ꂪ����������������鏈��
        Debug.Log("�P��`�F�b�N: ��=" + column + ", �s=" + row);
    }

    // �Q�[���{�[�h�̏�Ԃ��擾
    public Transform GetTileAt(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return grid[x, y];
        }
        return null;
    }
}