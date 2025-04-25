using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{
    // ボードのサイズ
    public int width = 7;
    public int height = 5;

    // 各セルの位置を保存する2次元配列
    private Transform[,] grid;

    // 各列の現在の高さを追跡（次の石弁がどの高さに配置されるか）
    private int[] columnHeights;

    void Start()
    {
        // グリッドの初期化
        grid = new Transform[width, height];
        columnHeights = new int[width];

        // 各列の高さを0に初期化
        for (int i = 0; i < width; i++)
        {
            columnHeights[i] = 0;
        }

        // ボードの視覚的表現を作成
        CreateBoardVisual();
    }

    // ボードを視覚的に表示する
    void CreateBoardVisual()
    {
        // ボードの背景を作成
        GameObject boardBackground = new GameObject("BoardBackground");
        boardBackground.transform.parent = transform;

        SpriteRenderer boardRenderer = boardBackground.AddComponent<SpriteRenderer>();
        // ボード全体の背景色（薄い青色）
        boardRenderer.color = new Color(0.8f, 0.8f, 1.0f);

        // ボードサイズに合わせてスケール調整
        boardRenderer.drawMode = SpriteDrawMode.Sliced;
        boardRenderer.size = new Vector2(width, height);

        // ボードの位置調整
        boardBackground.transform.position = new Vector3(width / 2.0f - 0.5f, height / 2.0f - 0.5f, 0);

        // セルの区切り線を作成
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

    // 指定した列に石弁を追加する
    public bool AddTile(int column, Transform tile)
    {
        // 列が範囲外または既に列が満杯の場合
        if (column < 0 || column >= width || columnHeights[column] >= height)
        {
            return false;
        }

        // 石弁を適切な位置に配置
        int row = columnHeights[column];
        Vector3 position = new Vector3(column, row, 0);
        tile.position = position;

        // グリッドに石弁を登録
        grid[column, row] = tile;

        // 列の高さを更新
        columnHeights[column]++;

        // 単語のチェック（後で実装）
        CheckForWords(column, row);

        return true;
    }

    // 単語をチェックする（実装は後のステップで）
    private void CheckForWords(int column, int row)
    {
        // 横方向と縦方向の単語チェック
        // 単語が見つかったら消去する処理
        Debug.Log("単語チェック: 列=" + column + ", 行=" + row);
    }

    // ゲームボードの状態を取得
    public Transform GetTileAt(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return grid[x, y];
        }
        return null;
    }
}