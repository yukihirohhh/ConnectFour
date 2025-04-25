using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ゲームボードの参照
    public GameBoard gameBoard;

    // タイル（文字牌）のプレハブ
    public GameObject tilePrefab;

    // UIテキスト
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI nextCharacterText;

    // 現在のプレイヤー（1 or 2）
    private int currentPlayer = 1;

    // 使用するひらがなリスト
    private List<string> hiraganaList = new List<string>
    {
        "あ", "い", "う", "え", "お",
        "か", "き", "く", "け", "こ",
        "さ", "し", "す", "せ", "そ",
        "た", "ち", "つ", "て", "と",
        "な", "に", "ぬ", "ね", "の",
        "は", "ひ", "ふ", "へ", "ほ",
        "ま", "み", "む", "め", "も",
        "や", "ゆ", "よ",
        "ら", "り", "る", "れ", "ろ",
        "わ", "を", "ん"
    };

    // 次のひらがな
    private string nextHiragana;

    // 現在プレイヤーが持つタイル
    private GameObject currentTile;
    private CharacterTile currentTileScript;

    // マウスの位置からのレイキャスト用
    private Camera mainCamera;

    void Start()
    {
        // メインカメラの参照を取得
        mainCamera = Camera.main;

        // 最初のターンの準備
        PrepareNextTurn();
    }

    void Update()
    {
        // マウス位置に現在のタイルを追従させる
        if (currentTile != null)
        {
            // マウス位置をワールド座標に変換
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Z座標を0に設定

            // X座標をゲームボードの範囲内に制限 (列の中央に配置)
            float clampedX = Mathf.Clamp(Mathf.Round(mousePosition.x), 0, gameBoard.width - 1);

            // タイルの位置を更新（ボードの上に表示）
            currentTile.transform.position = new Vector3(clampedX, gameBoard.height + 1, 0);

            // 左クリックでタイルをドロップ
            if (Input.GetMouseButtonDown(0)) // 0は左クリック
            {
                DropTile((int)clampedX);
            }
        }
    }

    // 次のランダムなひらがなを選択
    string GetRandomHiragana()
    {
        return hiraganaList[Random.Range(0, hiraganaList.Count)];
    }

    // タイルを落とす処理
    void DropTile(int column)
    {
        if (currentTile == null) return;

        // 選択した列にタイルを追加
        bool success = gameBoard.AddTile(column, currentTile.transform);

        if (success)
        {
            // 次のターンの準備
            PrepareNextTurn();
        }
    }

    // 次のターンの準備
    void PrepareNextTurn()
    {
        // プレイヤーを切り替え
        currentPlayer = (currentPlayer == 1) ? 2 : 1;

        // UI更新
        if (currentPlayerText != null)
        {
            currentPlayerText.text = "プレイヤー " + currentPlayer + " の番";
        }

        // 次のひらがなを選択
        nextHiragana = GetRandomHiragana();

        // UI更新
        if (nextCharacterText != null)
        {
            nextCharacterText.text = "次の文字: " + nextHiragana;
        }

        // 新しいタイルを生成
        currentTile = Instantiate(tilePrefab);
        currentTileScript = currentTile.GetComponent<CharacterTile>();

        // ひらがなを設定
        currentTileScript.SetCharacter(nextHiragana);
        currentTileScript.SetPlayer(currentPlayer);

        // マウス位置にタイルを配置（初期位置）
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float clampedX = Mathf.Clamp(Mathf.Round(mousePosition.x), 0, gameBoard.width - 1);
        currentTile.transform.position = new Vector3(clampedX, gameBoard.height + 1, 0);
    }
}