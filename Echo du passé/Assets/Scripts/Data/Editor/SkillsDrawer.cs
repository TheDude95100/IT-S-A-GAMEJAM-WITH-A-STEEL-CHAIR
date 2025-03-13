using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SkillData))]
public class SkillsDrawer : PropertyDrawer
{
    private SerializedProperty _skillName;
    private SerializedProperty _skillSlot;
    private SerializedProperty _description;
    private SerializedProperty _element;
    private SerializedProperty _focus;
    private SerializedProperty _damage;
    private SerializedProperty _accuracy;
    private SerializedProperty _curses;
    private SerializedProperty _healing;
    private SerializedProperty _buffs;

    private int padding = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        _skillName = property.FindPropertyRelative("_skillName");
        _skillSlot = property.FindPropertyRelative("_skillSlot");
        _description = property.FindPropertyRelative("_description");
        _element = property.FindPropertyRelative("_element");
        _focus = property.FindPropertyRelative("_focus");
        _damage = property.FindPropertyRelative("_damage");
        _accuracy = property.FindPropertyRelative("_accuracy");
        _curses = property.FindPropertyRelative("_curses");
        _healing = property.FindPropertyRelative("_healing");
        _buffs = property.FindPropertyRelative("_buffs");

        Rect foldOutBox = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, label);

        if (property.isExpanded)
        {
            DrawNameProperty(position);
            DrawArchetypeProperty(position);
        }

        EditorGUI.EndProperty();

    }
    private void DrawNameProperty(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _skillName, new GUIContent("Name"));
    }

    private void DrawArchetypeProperty(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 2 + padding;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _element, new GUIContent("Element"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 1;

        if (property.isExpanded)
        {
            totalLines += 3;
        }

        return (EditorGUIUtility.singleLineHeight * totalLines);
    }


}
