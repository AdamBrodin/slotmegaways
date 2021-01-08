using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SlotItem
{
    public Sprite sprite;
    public float blockSpaceNeeded, payoutMultiplier;
    public int randMin, randMax;
}
public class BoardSymbol
{
    public int columnID;
    public SlotItem slotItem;

    public BoardSymbol(int columnID, SlotItem slotItem)
    {
        this.columnID = columnID;
        this.slotItem = slotItem;
    }
}

public class SlotMachine : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int minBlocks, maxBlocks;
    [SerializeField]
    private float spinClickDelay; // Delay between clicking the button and actually spinning
    [SerializeField]
    private List<Column> columns; // The physical gameObject column
    [SerializeField]
    private List<SlotItem> slotItems; // All possible slotItems
    private List<BoardSymbol> generatedBoard;
    #endregion

    private void Start()
    {
        generatedBoard = new List<BoardSymbol>();
    }
    private SlotItem RandomSlotItem(float maxAllowedSpace)
    {
        SlotItem returnItem = null;
        while (returnItem == null)
        {
            int random = Random.Range(0, 1001); // Between 0 and 1000
            foreach (SlotItem item in slotItems)
            {
                if (item.randMin <= random && item.randMax >= random && item.blockSpaceNeeded <= maxAllowedSpace)
                {
                    returnItem = item;
                    continue;
                }
            }
        }

        return returnItem;
    }
    private void GenerateBoard()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            {
                float columnSpaceLeft = maxBlocks;
                Debug.Log($"Column {i} space left: {columnSpaceLeft}");

                while (columnSpaceLeft > 0)
                {
                    SlotItem item = RandomSlotItem(columnSpaceLeft);
                    columnSpaceLeft -= item.blockSpaceNeeded;
                    generatedBoard.Add(new BoardSymbol(i, item));
                    Debug.Log($"Column {i} added new board symbol {item.sprite}, space required for symbol {item.blockSpaceNeeded}, space left {columnSpaceLeft}");
                }
            }
        }
    }

    public IEnumerator Spin()
    {
        generatedBoard.Clear();
        GenerateBoard();
        yield return new WaitForSeconds(spinClickDelay);
        for (int i = 0; i <= columns.Count - 1; i++)
        {
            List<SlotItem> itemsToAdd = new List<SlotItem>();
            foreach (BoardSymbol b in generatedBoard)
            {
                if (b.columnID == i)
                {
                    itemsToAdd.Add(b.slotItem);
                }
            }
            columns[i].SpawnColumn(itemsToAdd);
        }
    }
}
