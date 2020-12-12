

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    public Tower towerPrefab;
    public RawImage img;
    public TMP_Text nameCost;

    bool clicking = false;
    float t_click;

    bool locked;

    void Awake()
    {
        nameCost.text = towerPrefab.name + " " + towerPrefab.cost + "$";
    }


    void Update()
    {
        if (Tool.Click(img.rectTransform)) {
            clicking = true;
            t_click = Time.time;
        }

        if (clicking && !Input.GetMouseButton(0)) {
            clicking = false;
            if (!locked && Time.time - t_click < 0.2) {
                Tower tower = Instantiate(towerPrefab);
                tower.gameObject.SetActive(false);
                GrabMan.inst.Grab(tower);
            }
        }
    }

    public void UpdateLock()
    {
        float a = 1;
        locked = false;

        if (Shop.inst.money < towerPrefab.cost) {
            a = 0.3f;
            locked = true;
        }
        Color color = Color.white;
        color.a = a;
        img.color = color;
        color = nameCost.color;
        color.a = a;
        nameCost.color = color;
    }
}
