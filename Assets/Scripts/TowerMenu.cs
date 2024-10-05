using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using Utilities;

public class Globals : Singleton<Globals> {
    public int money;
}

public class TowerMenu : MonoBehaviour {
    
    [SerializeField] private GameObject[] towers;
    [SerializeField] private CanvasGroup menuGroup;
    [SerializeField] private GameObject towerUIPrefab;
    [SerializeField] private MenuItem[] uiItems;

    public void Start() {
        menuGroup = GetComponent<CanvasGroup>();
        uiItems = new MenuItem[towers.Length];
        for (int i = 0; i < towers.Length; i++) {
            uiItems[i] = new MenuItem(Instantiate(towerUIPrefab, menuGroup.transform), towers[i]);
        }
    }

    public void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Hide();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Show();
            return;
        }
    }

    public void Refund(TowerBase tower) {
        // TODO: Refund money
        Destroy(tower);
    }

    public void Show() {
        foreach (MenuItem menuItem in uiItems) {
            menuItem.buy.interactable = Globals.instance.money >= menuItem.towerBase.Cost;
        }
        menuGroup.FadeCanvas(1f, false, this);
    }

    public void Hide() {
        foreach (MenuItem menuItem in uiItems) {
            menuItem.buy.interactable = false;
        }
        menuGroup.FadeCanvas(1f, true, this);
    }

    public class MenuItem {
        public Button buy;
        public Image icon;
        public GameObject towerPrefab;
        public TowerBase towerBase;

        public MenuItem(GameObject uiPrefab, GameObject towerPrefab) {
            buy = uiPrefab.GetComponentInChildren<Button>();
            icon = uiPrefab.GetComponentsInChildren<Image>().First(image => image.gameObject.Has<Tags.UI.MenuImage>());
            icon.sprite = towerPrefab.GetComponent<SpriteRenderer>().sprite;
            towerBase = towerPrefab.GetComponent<TowerBase>();
        }
    }
}