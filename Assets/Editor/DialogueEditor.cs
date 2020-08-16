using System.Collections.Generic;
using System.Linq;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Dialogue))]
    public class DialogueEditor : UnityEditor.Editor
    {
        List<bool> showContent = new List<bool>();
        public override void OnInspectorGUI()
        {
            //Cast unity object to Composite Behaviour
            var d = (Dialogue) target;
            
            
            //Check for behaviours
            if (d.Messages == null || d.Messages.Count == 0)
            {
                EditorGUILayout.HelpBox("No Messages in array.", MessageType.Warning);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                for (var i = 0; i < d.Messages.Count; i++)
                {
                    if (showContent.Count < d.Messages.Count) showContent.Add(new bool());
                    else if (showContent.Count > d.Messages.Count) showContent.Remove(showContent.Last());
                    EditorGUI.indentLevel = 0;
                    showContent[i]= EditorGUILayout.Foldout(showContent[i], i + " " + d.Messages[i].NpcName);
                    if (showContent[i])
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("# Name", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(10f), GUILayout.MaxWidth(10f));
                        d.Messages[i].NpcName = EditorGUILayout.TextField(d.Messages[i].NpcName);
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUI.indentLevel = 1;
                        EditorGUILayout.LabelField("Messages", GUILayout.MinWidth(60f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        d.Messages[i].MessageText =
                            EditorGUILayout.TextArea(d.Messages[i].MessageText, EditorStyles.textArea);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Npc Mood", GUILayout.MinWidth(60f), GUILayout.MaxWidth(100f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        d.Messages[i].NpcMood = EditorGUILayout.TextField(d.Messages[i].NpcMood, GUILayout.Width(100f));
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Next Message", GUILayout.MinWidth(60f), GUILayout.MaxWidth(100f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        d.Messages[i].NextMessage = EditorGUILayout.IntField(d.Messages[i].NextMessage, GUILayout.Width(40f));
                        EditorGUILayout.EndHorizontal();

                        if (d.Messages[i].Responses != null && d.Messages[i].Responses.Count > 0)
                        {
                            for (int j = 0; j < d.Messages[i].Responses.Count; j++)
                            {
                                EditorGUI.indentLevel = 3;
                                EditorGUILayout.BeginHorizontal();

                                EditorGUILayout.LabelField("Response " + j, GUILayout.MinWidth(150f),
                                    GUILayout.MaxWidth(150f));
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                d.Messages[i].Responses[j].Reply =
                                    EditorGUILayout.TextField(d.Messages[i].Responses[j].Reply, GUILayout.Width(150f));

                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();

                                EditorGUI.indentLevel = 4;

                                EditorGUILayout.LabelField("Next Message", GUILayout.MinWidth(60f),
                                    GUILayout.MaxWidth(150f));
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                d.Messages[i].Responses[j].Next = EditorGUILayout.IntField(
                                    d.Messages[i].Responses[j].Next,
                                    GUILayout.Width(80f));

                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();

                                EditorGUILayout.LabelField("Trigger Event", GUILayout.MinWidth(60f),
                                    GUILayout.MaxWidth(150f));
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                d.Messages[i].Responses[j].TriggerEvent =
                                    EditorGUILayout.Toggle(d.Messages[i].Responses[j].TriggerEvent);

                                EditorGUILayout.EndHorizontal();
                            }
                        }


                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(50f);
                        if (GUILayout.Button("Add Response", GUILayout.Width(150f)))
                        {
                            AddResponse(d.Messages[i]);
                            EditorUtility.SetDirty(d);
                        }

                        EditorGUILayout.EndHorizontal();
                        if (d.Messages != null && d.Messages.Count > 0)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(50f);
                            if (GUILayout.Button("Remove Response", GUILayout.Width(150f)))
                            {
                                RemoveResponse(d.Messages[i]);
                                EditorUtility.SetDirty(d);
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(d);
                }
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Message"))
            {
                AddMessage(d);
                EditorUtility.SetDirty(d);
            }
            EditorGUILayout.EndHorizontal();
            if (d.Messages != null && d.Messages.Count > 0)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove Message"))
                {
                    RemoveMessage(d);
                    EditorUtility.SetDirty(d);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void AddMessage(Dialogue d)
        {
            if (d.Messages == null) d.Messages = new List<Message>();
            d.Messages.Add(new Message());
        }
        
        private void RemoveMessage(Dialogue d)
        {
            d.Messages.Remove(d.Messages.Last());
        }

        private void AddResponse(Message m)
        {
            if (m.Responses == null) m.Responses = new List<Response>();
            m.Responses.Add(new Response());
        }

        private void RemoveResponse(Message m)
        {
            m.Responses.Remove(m.Responses.Last());
        }
    }
}
