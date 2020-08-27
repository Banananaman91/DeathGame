using System.Collections.Generic;
using System.Linq;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    [CustomEditor(typeof(NpcImages))]
    public class NpcBioEditor : UnityEditor.Editor
    {
        List<bool> showContent = new List<bool>();
        public override void OnInspectorGUI()
        {
            //Cast unity object to Composite Behaviour
            var bio = (NpcImages) target;
            
            
            //Check for behaviours
            if (bio.NpcImage == null || bio.NpcImage.Count == 0)
            {
                EditorGUILayout.HelpBox("No Bios in array.", MessageType.Warning);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                for (var i = 0; i < bio.NpcImage.Count; i++)
                {
                    if (showContent.Count < bio.NpcImage.Count) showContent.Add(new bool());
                    else if (showContent.Count > bio.NpcImage.Count) showContent.Remove(showContent.Last());
                    EditorGUI.indentLevel = 0;
                    showContent[i]= EditorGUILayout.Foldout(showContent[i], i + " " + bio.NpcImage[i].NpcName);
                    if (showContent[i])
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("# Name", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(10f), GUILayout.MaxWidth(10f));
                        bio.NpcImage[i].NpcName = EditorGUILayout.TextField(bio.NpcImage[i].NpcName);
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUI.indentLevel = 1;
                        EditorGUILayout.LabelField("Dialogue Background", GUILayout.MinWidth(60f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        bio.NpcImage[i].DialogueBackgroundImage = EditorGUILayout.ObjectField(bio.NpcImage[i].DialogueBackgroundImage, typeof(Sprite)) as Sprite;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Background Colour", GUILayout.MinWidth(60f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        bio.NpcImage[i].DialogueBackgroundColour = EditorGUILayout.ColorField(bio.NpcImage[i].DialogueBackgroundColour, GUILayout.Width(100f));
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Text Colour", GUILayout.MinWidth(60f), GUILayout.MaxWidth(100f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        bio.NpcImage[i].DialogueTextColour = EditorGUILayout.ColorField(bio.NpcImage[i].DialogueTextColour, GUILayout.Width(100f));
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Text Font", GUILayout.MinWidth(60f), GUILayout.MaxWidth(100f));
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        bio.NpcImage[i].DialogueTextFont = EditorGUILayout.ObjectField(bio.NpcImage[i].DialogueTextFont, typeof(Font)) as Font;
                        EditorGUILayout.EndHorizontal();

                        if (bio.NpcImage[i].NpcMoodImages != null && bio.NpcImage[i].NpcMoodImages.Count > 0)
                        {
                            for (int j = 0; j < bio.NpcImage[i].NpcMoodImages.Count; j++)
                            {
                                EditorGUI.indentLevel = 3;
                                EditorGUILayout.BeginHorizontal();

                                EditorGUILayout.LabelField("Mood " + j, GUILayout.MinWidth(150f),
                                    GUILayout.MaxWidth(150f));
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                bio.NpcImage[i].NpcMoodImages[j].NpcMoodImage =
                                    EditorGUILayout.ObjectField(bio.NpcImage[i].NpcMoodImages[j].NpcMoodImage, typeof(MoodImage), GUILayout.Width(150f)) as Image;
                                EditorGUILayout.EndHorizontal();
                            }
                        }


                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(50f);
                        if (GUILayout.Button("Add Image", GUILayout.Width(150f)))
                        {
                            AddImage(bio.NpcImage[i]);
                            EditorUtility.SetDirty(bio);
                        }

                        EditorGUILayout.EndHorizontal();
                        if (bio.NpcImage != null && bio.NpcImage.Count > 0)
                        {
                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(50f);
                            if (GUILayout.Button("Remove Image", GUILayout.Width(150f)))
                            {
                                RemoveImage(bio.NpcImage[i]);
                                EditorUtility.SetDirty(bio);
                            }

                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(bio);
                }
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Bio"))
            {
                AddBio(bio);
                EditorUtility.SetDirty(bio);
            }
            EditorGUILayout.EndHorizontal();
            if (bio.NpcImage != null && bio.NpcImage.Count > 0)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove Bio"))
                {
                    RemoveBio(bio);
                    EditorUtility.SetDirty(bio);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void AddBio(NpcImages d)
        {
            if (d.NpcImage == null) d.NpcImage = new List<NpcBio>();
            d.NpcImage.Add(new NpcBio());
        }
        
        private void RemoveBio(NpcImages d)
        {
            d.NpcImage.Remove(d.NpcImage.Last());
        }

        private void AddImage(NpcBio m)
        {
            if (m.NpcMoodImages == null) m.NpcMoodImages = new List<MoodImage>();
            m.NpcMoodImages.Add(new MoodImage());
        }

        private void RemoveImage(NpcBio m)
        {
            m.NpcMoodImages.Remove(m.NpcMoodImages.Last());
        }
    }
}
