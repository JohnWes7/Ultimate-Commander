using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace myEditor
{
    [CustomPropertyDrawer(typeof(OptionalValue<>))]
    public class CustomDrawOptionalValue : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            var enabledProperty = property.FindPropertyRelative("enabled");

            try
            {
                var valueProperty = property.FindPropertyRelative("value");
                position.width -= 24;
                EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
                EditorGUI.PropertyField(position, valueProperty, label, true);
                EditorGUI.EndDisabledGroup();
            }
            catch (System.Exception)
            {

            }
            

            position.x += position.width + 24;
            position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
            position.x -= position.width;
            EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
        }
    }
}
