using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Sprite))]
public class SpriteDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Sprite sprite = property.objectReferenceValue as Sprite;
        if (sprite != null)
        {
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            return base.GetPropertyHeight(property, label) + (64 / aspectRatio) + 10;
        }

        return base.GetPropertyHeight(property, label) + 70;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect spriteFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.objectReferenceValue = EditorGUI.ObjectField(spriteFieldRect, label, property.objectReferenceValue, typeof(Sprite), false);

        Sprite sprite = property.objectReferenceValue as Sprite;

        if (sprite != null)
        {
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            float previewHeight = 64 / aspectRatio;
            Rect previewRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 5, 64, previewHeight);

            if (sprite.texture != null)
            {
                Rect texCoords = new Rect(
                    sprite.rect.x / sprite.texture.width,
                    sprite.rect.y / sprite.texture.height,
                    sprite.rect.width / sprite.texture.width,
                    sprite.rect.height / sprite.texture.height
                );

                GUI.DrawTextureWithTexCoords(previewRect, sprite.texture, texCoords);
            }
        }

        EditorGUI.EndProperty();
    }
}
