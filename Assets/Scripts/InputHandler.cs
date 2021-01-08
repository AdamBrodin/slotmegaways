using UnityEngine;

public class InputHandler : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private SlotMachine slotMachine;
    #endregion
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(slotMachine.Spin());
        }
    }
}
