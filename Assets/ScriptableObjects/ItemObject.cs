using UnityEngine;

[CreateAssetMenu(fileName = "ItemObject", menuName = "Tropic Thunder/Item", order = 0)]
public class ItemObject : ScriptableObject {
    [SerializeField]
    protected ItemType type;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected GameObject itemPrefab;

    public ItemType ItemType => type;
    public Sprite Sprite => sprite;
    public GameObject ItemPrefab => itemPrefab;
}