using UnityEngine;

using Utilities;

public class Globals : Singleton<Globals> {
    [SerializeField] private int _money;
    public int money => _money;

    public void ChangeMoney(int change) {
        _money += change;
        GridManager.instance.UpdateCosts();
    }
}