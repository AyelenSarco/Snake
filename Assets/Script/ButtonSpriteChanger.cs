using UnityEngine.UI;
using UnityEngine.EventSystems; 
using UnityEngine;

public class ButtonSpriteChanger : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    private Button button;
    private Image buttonImage;

    private bool useSprite1 = true;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }

    private void Start()
    {
        button.onClick.AddListener(ChangeButtonSprite);
    }

    private void ChangeButtonSprite()
    {
        if (useSprite1)
        {
            buttonImage.sprite = sprite2;
            useSprite1 = false;
        }
        else
        {
            buttonImage.sprite = sprite1;
            useSprite1 = true;
        }
    }
}
