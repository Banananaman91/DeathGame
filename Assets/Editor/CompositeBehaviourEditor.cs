using Boids.BehaviourScripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CompositeBehaviour))]
    public class CompositeBehaviourEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //Cast unity object to Composite Behaviour
            var cb = (CompositeBehaviour) target;

            //Check for behaviours
            if (cb.Behaviours == null || cb.Behaviours.Length == 0)
            {
                EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.LabelField("Behaviours", GUILayout.MinWidth(60f));
                EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f)); 
                EditorGUILayout.EndHorizontal();
                
                EditorGUI.BeginChangeCheck();
                
                for (var i = 0; i < cb.Behaviours.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                    cb.Behaviours[i] = (BoidBehaviour)EditorGUILayout.ObjectField(cb.Behaviours[i], typeof(BoidBehaviour), false);
                    cb.Weights[i] = EditorGUILayout.FloatField(cb.Weights[i]);
                    EditorGUILayout.EndHorizontal();
                }

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(cb);
                }
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Behaviour"))
            {
                AddBehaviour(cb);
                EditorUtility.SetDirty(cb);
            }
            EditorGUILayout.EndHorizontal();
            if (cb.Behaviours != null && cb.Behaviours.Length > 0)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove Behaviour"))
                {
                    RemoveBehaviour(cb);
                    EditorUtility.SetDirty(cb);
                }
                EditorGUILayout.EndHorizontal();
            }
            
            
        }

        private void AddBehaviour(CompositeBehaviour cb)
        {
            int oldCount = (cb.Behaviours != null) ? cb.Behaviours.Length : 0;
            BoidBehaviour[] newBehaviours = new BoidBehaviour[oldCount + 1];
            float[] newWeights = new float[oldCount + 1];
            for (int i = 0; i < oldCount; i++)
            {
                newBehaviours[i] = cb.Behaviours[i];
                newWeights[i] = cb.Weights[i];
            }

            newWeights[oldCount] = 1f;
            cb.Behaviours = newBehaviours;
            cb.Weights = newWeights;
        }

        private void RemoveBehaviour(CompositeBehaviour cb)
        {
            int oldCount = cb.Behaviours.Length;
            if (oldCount == 1)
            {
                cb.Behaviours = null;
                cb.Weights = null;
                return;
            }
            
            BoidBehaviour[] newBehaviours = new BoidBehaviour[oldCount - 1];
            float[] newWeights = new float[oldCount - 1];
            for (int i = 0; i < oldCount - 1; i++)
            {
                newBehaviours[i] = cb.Behaviours[i];
                newWeights[i] = cb.Weights[i];
            }
            cb.Behaviours = newBehaviours;
            cb.Weights = newWeights;
        }
    }
}
