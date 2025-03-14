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

    private int parametresCount = 14;
    private int padding = 5;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        _skillName = property.FindPropertyRelative("_skillName");//
        _skillSlot = property.FindPropertyRelative("_skillSlot");//
        _description = property.FindPropertyRelative("_description");//
        _element = property.FindPropertyRelative("_element"); //
        _focus = property.FindPropertyRelative("_focus");//
        _damage = property.FindPropertyRelative("_damage");//
        _accuracy = property.FindPropertyRelative("_accuracy");
        _curses = property.FindPropertyRelative("_curses");
        _healing = property.FindPropertyRelative("_healing");
        _buffs = property.FindPropertyRelative("_buffs");

        Rect foldOutBox = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, label);

        if (property.isExpanded)
        {
            DrawNameProperty(position,1);
            DrawElementProperty(position, 2);
            DrawDescriptionProperty(position, 3);
            DrawSkillSlotProperty(position, 4);
            DrawFocusProperty(position, 5);
            DrawAccuracyProperty(position, 6);
            DrawDamageProperty(position, 7);
            DrawCurseProperty(position, 8);
            DrawHealingProperty(position, 11);
            DrawBuffProperty(position, 12);
        }

        EditorGUI.EndProperty();

    }
    private void DrawNameProperty(Rect position,int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _skillName, new GUIContent("Name"));
    }

    private void DrawElementProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber -1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _element, new GUIContent("Element"));
    }

    private void DrawDescriptionProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _description, new GUIContent("Description"));
    }

    private void DrawSkillSlotProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _skillSlot, new GUIContent("SkillSlot"));
    }

    private void DrawDamageProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _damage, new GUIContent("Damage"));
    }

    private void DrawFocusProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _focus, new GUIContent("Focus"));
    }

    private void DrawAccuracyProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _accuracy, new GUIContent("Accuracy"));
    }

    private void DrawHealingProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _healing, new GUIContent("Healing"));
    }

    private void DrawBuffProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _buffs, new GUIContent("Buff"));
    }

    private void DrawCurseProperty(Rect position, int lineNumber)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * lineNumber + padding * (lineNumber - 1);
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _curses, new GUIContent("Curse"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 1;

        if (property.isExpanded)
        {
            totalLines += parametresCount*2- parametresCount/2 - 2;
        }

        return (EditorGUIUtility.singleLineHeight * totalLines);
    }


}
