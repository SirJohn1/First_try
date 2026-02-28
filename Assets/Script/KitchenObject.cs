using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public ClearCounter clearCounter;

    public ClearCounter GetClearCounter() {  return clearCounter; }
    public void SetClearCounter(ClearCounter clearCounter) 
    { 
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        if (clearCounter.HasKitchenObject()) 
        {
            Debug.Log("Там уже лежит что то");
        }

        this.clearCounter = clearCounter;
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }

    public KitchenObjectSO GetKitchenObjectSO() {  return kitchenObjectSO; }


}
