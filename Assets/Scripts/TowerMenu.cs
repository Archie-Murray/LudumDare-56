using UnityEngine;

public class TowerMenu : MonoBehaviour {
    public void Refund(TowerBase tower) {
        // TODO: Refund money
        Destroy(tower);
    }
}
