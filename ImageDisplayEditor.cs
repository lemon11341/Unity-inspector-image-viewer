using UnityEngine;
using UnityEditor;

// Sprite �ʵ忡 Ŀ���� Drawer�� �����ϱ� ���� Ŭ����
[CustomPropertyDrawer(typeof(Sprite))]
public class SpriteDrawer : PropertyDrawer
{
    // �ʵ��� ���̸� �����ϴ� �Լ�
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Sprite sprite = property.objectReferenceValue as Sprite;
        if (sprite != null)
        {
            // �̹����� ���� ������ ����Ͽ� ���̸� ����
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            return base.GetPropertyHeight(property, label) + (64 / aspectRatio) + 10; // ���� ���� �߰�
        }

        return base.GetPropertyHeight(property, label) + 70; // �⺻ ����
    }

    // �ʵ�� �̹��� �̸����⸦ �׸��� �Լ�
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Sprite �ʵ� �׸���
        EditorGUI.BeginProperty(position, label, property);

        // �⺻ Sprite �ʵ� �׸���
        Rect spriteFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.objectReferenceValue = EditorGUI.ObjectField(spriteFieldRect, label, property.objectReferenceValue, typeof(Sprite), false);

        // ��������Ʈ�� ������
        Sprite sprite = property.objectReferenceValue as Sprite;

        // ���� ��������Ʈ�� �ִٸ�, �ν����Ϳ� �̸����⸦ ǥ��
        if (sprite != null)
        {
            // ���� ������ �����ϸ鼭 �̹��� �̸����� ũ�� ����
            float aspectRatio = sprite.rect.width / sprite.rect.height;
            float previewHeight = 64 / aspectRatio;
            Rect previewRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 5, 64, previewHeight);

            // �ؽ�ó�� �ִ� ��� ���� ������ �̸����� ǥ��
            if (sprite.texture != null)
            {
                // ��������Ʈ�� rect�� ������� �ش� ������ �ؽ�ó�� ǥ�� (��Ƽ�� ��������Ʈ���� Ư�� ��������Ʈ�� ǥ��)
                Rect texCoords = new Rect(
                    sprite.rect.x / sprite.texture.width,
                    sprite.rect.y / sprite.texture.height,
                    sprite.rect.width / sprite.texture.width,
                    sprite.rect.height / sprite.texture.height
                );

                // �ش� ������ �ؽ�ó�� GUI�� �׸��� (Sprite�� �κи�)
                GUI.DrawTextureWithTexCoords(previewRect, sprite.texture, texCoords);
            }
        }

        EditorGUI.EndProperty();
    }
}
