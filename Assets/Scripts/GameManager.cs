using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GameState
{
    MainMenu,
    InRoom,
    InCinematic
}

public class GameManager : Singleton<GameManager>
{
    public GameState GameState => _gameState;
    private GameState _gameState;
    
    [SerializeField] private ItemManager itemManager;
    
    private DialogTrigger _dialog;
    private List<ItemSO> _inventory = new List<ItemSO>();
    private int _acquiredNotes;
    private int _puzzlePiecesPlaced;

    [Button(ButtonSizes.Small, ButtonStyle.FoldoutButton)]
    public void FindItemManager()
    {
#if UNITY_EDITOR
        if (itemManager == null)
        {
            var itemManagerAsset = AssetDatabase.FindAssets("t:ItemManager");
            itemManager =
                AssetDatabase.LoadAssetAtPath<ItemManager>(AssetDatabase.GUIDToAssetPath(itemManagerAsset[0]));
        }
#endif
    }
    
    protected override void Awake()
    {
        base.Awake();
        FindItemManager();
        foreach (var item in itemManager.items)
        {
            item.isCompleted = false;
            item.evolution = -1;
        }
        try
        {
            _dialog = GameObject.FindWithTag("DialogueCollider").GetComponent<DialogTrigger>();
        }
        catch { /*die*/ }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) =>
        {
            try
            {
                _dialog = GameObject.FindWithTag("DialogueCollider").GetComponent<DialogTrigger>();
            }
            catch { /*die*/ }
        };
    }

    public void SetGameState(GameState state)
    {
        _gameState = state;
    }

    public int GetNotes()
    {
        return _acquiredNotes;
    }
    
    public int GetPuzzlePieces()
    {
        return _puzzlePiecesPlaced;
    }
    
    public void AddNote()
    {
        _acquiredNotes++;
    }

    public void AddPuzzlePiece()
    {
        _puzzlePiecesPlaced++;
    }
    
    public void AddToInventory(ItemSO obj)
    {
        if (!obj.dynamic || !_inventory.Contains(obj))
        {
            obj.evolution = -1;
            _inventory.Add(obj);
        }

        InventoryUI.instance.AddItem(obj);
    }
    
    public void RemoveFromInventory(ItemSO obj)
    {
        _inventory.Remove(obj);
        InventoryUI.instance.RemoveItem(obj);
    }

    public bool InventoryContains(ItemSO obj)
    {
        return _inventory.Contains(obj);
    }

    public void IncreaseChatCount()
    {
        _dialog.IncreaseChatIndex();
        _dialog.ChatOneShot();
    }

    public bool InventoryFull()
    {
        return _inventory.Count >= 4;
    }
}
