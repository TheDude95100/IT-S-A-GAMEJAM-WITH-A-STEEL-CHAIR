using log4net.Core;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntityData))]
public class EntityDataEditor : Editor
{
    private SerializedProperty _entityName;
    
    private SerializedProperty _strength;
    private SerializedProperty _dexterity;
    private SerializedProperty _intelligence;
    private SerializedProperty _vitality;
    private SerializedProperty _luck;

    private SerializedProperty _level;
    private SerializedProperty _job;

    private SerializedProperty _showDesignerDisplay;

    private void OnEnable()
    {
        _entityName = serializedObject.FindProperty("_entityName");
        _strength = serializedObject.FindProperty("_strength");
        _dexterity = serializedObject.FindProperty("_dexterity");
        _intelligence = serializedObject.FindProperty("_intelligence");
        _vitality = serializedObject.FindProperty("_vitality");
        _luck = serializedObject.FindProperty("_luck");
        _level = serializedObject.FindProperty("_level");
        _job = serializedObject.FindProperty("_job");
        _showDesignerDisplay = serializedObject.FindProperty("_showDesignerDisplay");
    }

    public override void OnInspectorGUI()
    {
        //Save if any change
        serializedObject.UpdateIfRequiredOrScript();

        // Cast param
        //JobData _jobData = (JobData)_job.objectReferenceValue;

        // Entity name at the begin
        EditorGUILayout.LabelField(_entityName.stringValue.ToUpper(), EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(_showDesignerDisplay, new GUIContent("Show Designer Display"));

        // ========== Summary Display ==========

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(_entityName.stringValue, EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
        //EditorGUILayout.LabelField(_jobData.JobName, GUILayout.ExpandWidth(false));
        EditorGUILayout.EndHorizontal();
        ProgressBar((_strength.intValue + _dexterity.intValue + _intelligence.intValue + _vitality.intValue + _luck.intValue) / 50f, "Difficulty");
        

        // ========== Designer Display ==========

        if (_showDesignerDisplay.boolValue == true) 
        {
            // Name and job field
            EditorGUILayout.LabelField("General Stats", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_entityName, new GUIContent("Name"));
            if (_entityName.stringValue == string.Empty)
            {
                EditorGUILayout.HelpBox("Caution: No name specified. Please name the entity", MessageType.Warning);
            }
            EditorGUILayout.PropertyField(_job, new GUIContent("Job"));

            // Stats glider
            _strength.intValue = (int)EditorGUILayout.Slider(new GUIContent("Strength"), _strength.intValue, 1, 10);
            _dexterity.intValue = (int)EditorGUILayout.Slider(new GUIContent("Dexterity"), _dexterity.intValue, 1, 10);
            _intelligence.intValue = (int)EditorGUILayout.Slider(new GUIContent("Intelligence"), _intelligence.intValue, 1, 10);
            _vitality.intValue = (int)EditorGUILayout.Slider(new GUIContent("Vitality"), _vitality.intValue, 1, 10);
            _luck.intValue = (int)EditorGUILayout.Slider(new GUIContent("Luck"), _luck.intValue, 1, 10);

            if(GUILayout.Button("Random Stats"))
            {
                RandomizeStats();
            }
        }

        //Apply if any change
        serializedObject.ApplyModifiedProperties();
    }

    void ProgressBar(float value, string label)
    {
        //EditorGUILayout.Space(10); 
        Rect rect = GUILayoutUtility.GetRect(10, 60, "Textfield");
        EditorGUI.ProgressBar(rect, value, label);
        //EditorGUILayout.Space(10);
    }

    void RandomizeStats()
    {
        _strength.intValue = UnityEngine.Random.Range(0, 10);
        _dexterity.intValue = UnityEngine.Random.Range(0, 10);
        _intelligence.intValue = UnityEngine.Random.Range(0, 10);
        _vitality.intValue = UnityEngine.Random.Range(0, 10);
        _luck.intValue = UnityEngine.Random.Range(0, 10);
    }
}
