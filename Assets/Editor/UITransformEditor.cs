using UnityEditor;
using UnityEngine;


[CanEditMultipleObjects]
[CustomEditor(typeof(Transform))]

public class UITargetableEditor : Editor{

	private SerializedProperty[] positionProperties;

	private SerializedProperty[] scaleProperties;

	public override void OnInspectorGUI(){
	
		if(positionProperties == null) {

			positionProperties = new SerializedProperty[3];

			positionProperties[0] = serializedObject.FindProperty("m_LocalPosition.x");
			positionProperties[1] = serializedObject.FindProperty("m_LocalPosition.y");
			positionProperties[2] = serializedObject.FindProperty("m_LocalPosition.z");

		}

		if(scaleProperties == null) {

			scaleProperties = new SerializedProperty[3];
			
			scaleProperties[0] = serializedObject.FindProperty("m_LocalScale.x");
			scaleProperties[1] = serializedObject.FindProperty("m_LocalScale.y");
			scaleProperties[2] = serializedObject.FindProperty("m_LocalScale.z");
		}

		EditorGUIUtility.labelWidth = 15.0f;
		EditorGUIUtility.fieldWidth = 0.0f;

		serializedObject.Update();

		EditorGUI.BeginChangeCheck();

		GUIContent resetToZeroText = new GUIContent("0", "Set all elements to 0."); 
		GUIContent resetToOneText = new GUIContent("1", "Set all elements to 1."); 
		GUIContent positionText = new GUIContent("Position", "The local position of the transform.");
		GUIContent rotationText = new GUIContent("Rotation", "The local euler angles rotation of the transform.");
		GUIContent scaleText = new GUIContent("Scale", "The local scale of the transform.");

		GUIContent[] directionLabels = new GUIContent[3];
		directionLabels[0] = new GUIContent("X");
		directionLabels[1] = new GUIContent("Y");
		directionLabels[2] = new GUIContent("Z");

		// handle position
		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button(resetToZeroText, new GUILayoutOption[] {GUILayout.MaxWidth(21.0f), GUILayout.MaxHeight(15.0f)})){

			foreach(Object obj in targets){

				Undo.RecordObject(obj, "Undo transform position change");

				Transform t = obj as Transform;
				t.localPosition = Vector3.zero;
			}

			for(int i=0;i<positionProperties.Length;++i){
				positionProperties[i].floatValue = 0.0f;
			}
		}

		EditorGUILayout.LabelField(positionText);

		for(int i=0;i<positionProperties.Length;++i){

			EditorGUI.BeginChangeCheck();
			
			EditorGUI.showMixedValue = positionProperties[i].hasMultipleDifferentValues;

			float newValue = EditorGUILayout.FloatField(directionLabels[i], positionProperties[i].floatValue, GUILayout.MinWidth(20.0f));

			if(EditorGUI.EndChangeCheck()){

				foreach(Object obj in targets){
				
					Undo.RecordObject(obj, "Undo transform position change");

					Transform t = obj as Transform;

					Vector3 newPos = t.localPosition;

					if(i == 0){
						newPos.x = newValue;
					}else if(i == 1){
						newPos.y = newValue;
					}else{
						newPos.z = newValue;
					}

					t.localPosition = newPos;
				}
				positionProperties[i].floatValue = newValue;
			}
		}
		EditorGUILayout.EndHorizontal();


		// handle rotation.  unfortunately, Unity stores it as a quaternion, but we want to display it in Euler angles, 
		// so we have to go about it in a more convoluted way than the position and scale
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button(resetToZeroText, new GUILayoutOption[] {GUILayout.MaxWidth(21.0f), GUILayout.MaxHeight(15.0f)})){
			
			foreach(Object obj in targets){
			
				Undo.RecordObject(obj, "Undo transform rotation change");

				(obj as Transform).localRotation = Quaternion.identity;
			}
		}

		EditorGUILayout.LabelField(rotationText);

		bool[] changes = new bool[]{false, false, false};
		float[] rotationValues = new float[]{(target as Transform).localEulerAngles.x, (target as Transform).localEulerAngles.y, (target as Transform).localEulerAngles.z};

		for(int i=1;i<targets.Length;++i){

			Transform previous = targets[i-1] as Transform;
			Transform current = targets[i] as Transform;

			if(previous.localEulerAngles.x != current.localEulerAngles.x) changes[0] = true;
			if(previous.localEulerAngles.y != current.localEulerAngles.y) changes[1] = true;
			if(previous.localEulerAngles.z != current.localEulerAngles.z) changes[2] = true;
		}

		for(int i=0;i<3;++i){
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = changes[i];
			rotationValues[i] = EditorGUILayout.FloatField(directionLabels[i], rotationValues[i], GUILayout.MinWidth(20.0f));
			
			if(EditorGUI.EndChangeCheck()){
				foreach(Object obj in targets){

					Undo.RecordObject(obj, "Undo transform rotation change");
					
					Transform t = obj as Transform;
					Vector3 rot = t.localEulerAngles;

					if(i==0) rot.x = rotationValues[i];
					if(i==1) rot.y = rotationValues[i];
					if(i==2) rot.z = rotationValues[i];
					
					t.localEulerAngles = rot;
				}
			}
		}
			
		EditorGUILayout.EndHorizontal();


		// handle scale
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button(resetToOneText, new GUILayoutOption[] {GUILayout.MaxWidth(21.0f), GUILayout.MaxHeight(15.0f)})){
			
			foreach(Object obj in targets){

				Undo.RecordObject(obj, "Undo transform scale change");

				Transform t = obj as Transform;
				t.localScale = Vector3.one;
			}
			
			for(int i=0;i<scaleProperties.Length;++i){
				scaleProperties[i].floatValue = 1.0f;
			}
		}

		if(GUILayout.Button(resetToZeroText, new GUILayoutOption[] {GUILayout.MaxWidth(21.0f), GUILayout.MaxHeight(15.0f)})){

			foreach(Object obj in targets){

				Undo.RecordObject(obj, "Undo transform scale change to 0");

				Transform t = obj as Transform;
				t.localScale = Vector3.one;
			}

			for(int i=0;i<scaleProperties.Length;++i){
				scaleProperties[i].floatValue = 0.0f;
			}
		}
		
		EditorGUILayout.LabelField(scaleText);
		
		for(int i=0;i<scaleProperties.Length;++i){
			
			EditorGUI.BeginChangeCheck();
			
			EditorGUI.showMixedValue = scaleProperties[i].hasMultipleDifferentValues;
			
			float newValue = EditorGUILayout.FloatField(directionLabels[i], scaleProperties[i].floatValue, GUILayout.MinWidth(20.0f));
			
			if(EditorGUI.EndChangeCheck()){
				foreach(Object obj in targets){

					Undo.RecordObject(obj, "Undo transform scale change");

					Transform t = obj as Transform;
					
					Vector3 newScale = t.localScale;
					
					if(i == 0){
						newScale.x = newValue;
					}else if(i == 1){
						newScale.y = newValue;
					}else{
						newScale.z = newValue;
					}
					
					t.localScale = newScale;
				}
				scaleProperties[i].floatValue = newValue;
			}
		}

		EditorGUILayout.EndHorizontal();

		EditorGUI.showMixedValue = false;

		serializedObject.ApplyModifiedProperties();
	}
}




