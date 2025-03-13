using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(JobData))]
public class JobDrawer : PropertyDrawer
{
    private SerializedProperty _jobName;
    private SerializedProperty _skillList;
    private int _skillCount = 0;
    private SerializedProperty _archetype;
    private SerializedProperty _skillListProperty;
    private PropertyDrawer _skillDrawer;

    private int _padding = 1;
    private int _skillHeight = 3;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //_skillListProperty = property.FindPropertyRelative("_skillList");
        //_skillDrawer = (PropertyDrawer)System.Activator.CreateInstance(typeof(SkillsDrawer));


        EditorGUI.BeginProperty(position, label, property);

        _jobName = property.FindPropertyRelative("_jobName");
        _skillList = property.FindPropertyRelative("_skillList");
        _archetype = property.FindPropertyRelative("_archetype");

        _skillCount = _skillList.arraySize;

        Rect foldOutBox = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox,property.isExpanded,label);

        if(property.isExpanded)
        {
            DrawNameProperty(position);
            DrawArchetypeProperty(position);
            DrawSkillsProperty(position);
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
        EditorGUI.PropertyField(drawArea, _jobName, new GUIContent("Name"));
    }
    
    private void DrawArchetypeProperty(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 2 + _padding;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _archetype, new GUIContent("Archetype"));
    }

    private void DrawSkillsProperty(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 3 + _padding;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _skillList, new GUIContent("Skills"));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float totalLines = 1;

        if(property.isExpanded)
        {
            totalLines += 7 + _skillHeight * _skillCount + _padding;
        }

        return (EditorGUIUtility.singleLineHeight * totalLines);
    }


}
