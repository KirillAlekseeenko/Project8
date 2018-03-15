using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DiplomacyData))]
public class CustomDiplomacyEditor : PropertyDrawer {

	private const int dimension = 8;

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return dimension * 50;
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);

		var rows = property.FindPropertyRelative ("rows");
		rows.arraySize = dimension;

		for (int i = 0; i < dimension; i++) {
			var row = rows.GetArrayElementAtIndex (i).FindPropertyRelative("rowData");
			row.arraySize = i;
			for (int j = 0; j < i; j++) {
				var rect = new Rect (position.x + 50 + j * 100, position.y + 30 + i * 40, 80, 30);
				EditorGUI.PropertyField(rect, row.GetArrayElementAtIndex(j), GUIContent.none);
			}

			var columnLabelRect = new Rect (position.x, position.y + 30 + i * 40, position.width, position.height);
			var rowLabelRect = new Rect (position.x + 50 + i * 100, position.y, position.width, position.height);

			EditorGUI.LabelField(columnLabelRect, ((Team) (i)).ToString());
			EditorGUI.LabelField (rowLabelRect, ((Team)(i)).ToString ());
		}

		EditorGUI.EndProperty ();
	}
}
