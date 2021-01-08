using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private List<GameObject> boxes; // All 7 gameObjects containing possible slotItems
    #endregion
    public void SpawnColumn(List<SlotItem> boardSymbols)
    {
        foreach (GameObject b in boxes)
        {
            b.SetActive(false);
        }

        for (int i = 0; i < boardSymbols.Count; i++)
        {
            boxes[i].SetActive(true);
            boxes[i].GetComponent<SpriteRenderer>().sprite = boardSymbols[i].sprite;
            if (i != 0)
            {
                boxes[i].transform.position = new Vector3(boxes[i - 1].transform.position.x, boxes[i - 1].transform.position.y + boxes[i].GetComponent<SpriteRenderer>().sprite.bounds.extents.y + 0.1f, boxes[i - 1].transform.position.z);
            }
        }
    }
}
