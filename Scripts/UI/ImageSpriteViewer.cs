using UnityEngine;
using UnityEngine.UI;

public class ImageSpriteViewer : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Image image;
    #endregion

    /// <summary>
    ///     Gets the image sprite
    /// </summary>
    /// <returns>
    ///     Image sprite
    /// </returns>
    public Sprite GetImageSprite()
    {
        return image.sprite;
    }

    /// <summary>
    ///     Sets the image sprite to the input sprite
    /// </summary>
    /// <param name="arg_sprite"></param>
    public void SetImageSprite(Sprite arg_sprite)
    {
        image.sprite = arg_sprite;
    }
}
