using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text name;
    [SerializeField] private Image image;

    private Item item;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Setup(Item item)
    {
        this.item = item;

        // populate information views
        name.text = item.entity.Type.ToString();
        image.sprite = item.sprite;
    }
}