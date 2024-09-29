using UnityEngine;
using UnityEditor;

// Sprite 필드에 커스텀 Drawer를 적용하기 위한 클래스
[CustomPropertyDrawer(typeof(Sprite))]
public class SpriteDrawer : PropertyDrawer
{
    // 필드의 높이를 정의하는 함수
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Sprite sprite = property.objectReferenceValue as Sprite;
        if (sprite != null)
        {
            // 이미지의 원본 비율을 고려하여 높이를 설정
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            return base.GetPropertyHeight(property, label) + (64 / aspectRatio) + 10; // 여유 공간 추가
        }

        return base.GetPropertyHeight(property, label) + 70; // 기본 높이
    }

    // 필드와 이미지 미리보기를 그리는 함수
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Sprite 필드 그리기
        EditorGUI.BeginProperty(position, label, property);

        // 기본 Sprite 필드 그리기
        Rect spriteFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.objectReferenceValue = EditorGUI.ObjectField(spriteFieldRect, label, property.objectReferenceValue, typeof(Sprite), false);

        // 스프라이트를 가져옴
        Sprite sprite = property.objectReferenceValue as Sprite;

        // 만약 스프라이트가 있다면, 인스펙터에 미리보기를 표시
        if (sprite != null)
        {
            // 원본 비율을 유지하면서 이미지 미리보기 크기 설정
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            float previewHeight = 64 / aspectRatio;
            Rect previewRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 5, 64, previewHeight);

            // 텍스처가 있는 경우 원본 비율로 미리보기 표시
            if (sprite.texture != null)
            {
                // 스프라이트의 rect를 기반으로 해당 영역만 텍스처로 표시 (멀티플 스프라이트에서 특정 스프라이트만 표시)
                Rect texCoords = new Rect(
                    sprite.rect.x / sprite.texture.width,
                    sprite.rect.y / sprite.texture.height,
                    sprite.rect.width / sprite.texture.width,
                    sprite.rect.height / sprite.texture.height
                );

                // 해당 영역의 텍스처를 GUI에 그리기 (Sprite의 부분만)
                GUI.DrawTextureWithTexCoords(previewRect, sprite.texture, texCoords);
            }
        }

        EditorGUI.EndProperty();
    }
}
