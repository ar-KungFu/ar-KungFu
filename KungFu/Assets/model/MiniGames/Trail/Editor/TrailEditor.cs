/*
 * Copyright (c) 2023 MiniGames
 *
 * Check out how to use it here.
 * https://www.youtube.com/channel/UCrLZAN_rgpW7i84gDAHHH1g
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tiny
{
	[CustomEditor(typeof(Trail), true)]
	public class TrailEditor : Editor
	{
		public enum Edition
		{
			None,
			Point,
			Position,
			Rotate,
			Scale
		}

		Quaternion editorRotation = Quaternion.identity;
		Vector3 editorPoition = Vector3.zero;
		Vector3 editorScale = Vector3.one;

		Edition _edition = Edition.None;
		Edition edition {
			get { return _edition; }
			set { 
				if(_edition == value)
					return;
				_edition = value;
				if(value > Edition.Point)
					Tools.current = Tool.None;
				editorRotation = Quaternion.identity;
				editorPoition = tm.position;
				editorScale = Vector3.one;
				EditorUtility.SetDirty(target);
			}
		}
		bool foldout = false;
		Vector3 center = Vector3.zero;
		int normal = 0;
		float radius = 1f;
		int count = 10;

		Vector3[] points = new Vector3[0];

		Trail trail = null;
		Transform tm = null;

		private void OnSceneGUI()
		{
			Event currentEvent = Event.current;
			if (currentEvent.type == EventType.MouseUp && currentEvent.button == 0)
			{
				editorPoition = tm.position;
				editorRotation = Quaternion.identity;
				editorScale = Vector3.one;
			}
			
			EditPoints();
		}

		private void OnEnable()
		{
			trail = (Trail)target;
			tm = trail.transform;
		}

		private void DrawPoint(Vector3 point)
		{
			float handleSize = HandleUtility.GetHandleSize(point);
			Handles.DrawWireCube(point, new Vector3(handleSize, handleSize, handleSize) * 0.2f);
		}

		private void EditPoints()
		{
			Matrix4x4 localToWorldMatrix = tm.localToWorldMatrix;
			Matrix4x4 worldToLocalMatrix = tm.worldToLocalMatrix;

			if (points.Length != trail.Points.Length)
				points = new Vector3[trail.Points.Length];

			for (int i = -1; ++i < points.Length;)
				points[i] = localToWorldMatrix.MultiplyPoint3x4(trail.Points[i]);

			Handles.color = edition == Edition.None ? Color.yellow : Color.white;

			Vector3 prev = trail.Loop ? points[points.Length-1] : points[0];
			for (int i = -1; ++i < points.Length;)
			{
				DrawPoint(points[i]);
				Handles.DrawLine(prev, points[i]);
				prev = points[i];
			}

			switch (_edition)
			{
				case Edition.Point:
					for (int i = -1; ++i < points.Length;)
					{
						EditorGUI.BeginChangeCheck();
						points[i] = Handles.PositionHandle(points[i], Quaternion.identity);
						if (!EditorGUI.EndChangeCheck())
							continue;
						Undo.RecordObject(target, "MoveTrailPoint");
						trail.Points[i] = worldToLocalMatrix.MultiplyPoint3x4(points[i]);
					}
					break;
				case Edition.Position:					
					if (!MovePoints(ref worldToLocalMatrix))
						return;
					break;
				case Edition.Rotate:
					if (!RotatePoints(ref worldToLocalMatrix))
						return;
					break;
				case Edition.Scale:
					if (!ScalePoints(ref worldToLocalMatrix))
						return;
					break;

				default:
					return;
			}

			EditorUtility.SetDirty(trail);
		}

		private bool MovePoints(ref Matrix4x4 worldToLocalMatrix)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 move = Handles.PositionHandle(editorPoition, Quaternion.identity);
			Vector3 offset = worldToLocalMatrix.MultiplyPoint3x4(move) - worldToLocalMatrix.MultiplyPoint3x4(editorPoition);
			editorPoition = move;
			if (!EditorGUI.EndChangeCheck())
				return false;

			Undo.RecordObject(target, "MoveTrailPoints");
			for (int n = -1; ++n < trail.Points.Length;)
				trail.Points[n] = trail.Points[n] + offset;
			return true;
		}

		private bool RotatePoints(ref Matrix4x4 worldToLocalMatrix)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 pivot = tm.position;
			Quaternion angle = Handles.RotationHandle(editorRotation, pivot);
			Quaternion rotate = angle * Quaternion.Inverse(editorRotation);
			editorRotation = angle;
			if (!EditorGUI.EndChangeCheck())
				return false;

			Undo.RecordObject(target, "RotateTrailPoints");
			for (int n = -1; ++n < points.Length;)
				trail.Points[n] = worldToLocalMatrix.MultiplyPoint3x4(rotate * (points[n] - pivot)) + pivot;
			return true;
		}

		private bool ScalePoints(ref Matrix4x4 worldToLocalMatrix)
		{
			EditorGUI.BeginChangeCheck();
			Vector3 pivot = tm.position;
			Vector3 scale = Handles.ScaleHandle(editorScale, pivot, Quaternion.identity);
			Vector3 _scale = scale - editorScale;
			editorScale = scale;
			if (!EditorGUI.EndChangeCheck())
				return false;//"d_ToolHandlePivot"

			Undo.RecordObject(target, "ScaleTrailPoints");
			_scale.x += 1f;
			_scale.y += 1f;
			_scale.z += 1f;
			for (int n = -1; ++n < points.Length;)
				trail.Points[n] = worldToLocalMatrix.MultiplyPoint3x4(Vector3.Scale(points[n] - pivot, _scale)) + pivot;
			return true;
		}

		static Vector3 RotateYaw(Vector3 v, float radian)
		{
			float c = Mathf.Cos(radian);
			float s = Mathf.Sin(radian);
			return new Vector3(v.x * c + v.z * s, v.y, v.x * -s + v.z * c);
		}

		static Vector3 RotatePitch(Vector3 v, float radian)
		{
			float c = Mathf.Cos(radian);
			float s = Mathf.Sin(radian);
			return new Vector3(v.x, v.y * c + v.z * -s, v.y * s + v.z * c);
		}

		static Vector3 RotateRoll(Vector3 v, float radian)
		{
			float sin = Mathf.Sin(radian);
			float cos = Mathf.Cos(radian);
			return new Vector3(v.x * cos - v.y * sin, v.x * sin + v.y * cos, v.z);
		}

		private void Make()
		{
			if (count < 2)
				return;

			Trail trail = (Trail)target;

			Vector3 vector = normal == 1 ? new Vector3(radius, 0f, 0f) : new Vector3(0f, 0f, radius);

			Vector3 scale = tm.lossyScale;
			scale = new Vector3(scale.x > Mathf.Epsilon ? 1f / scale.x : 1f, scale.y > Mathf.Epsilon ? 1f / scale.y : 1f, scale.z > Mathf.Epsilon ? 1f / scale.z : 1f);

			Vector3[] points = new Vector3[count];
			points[0] = Vector3.Scale(vector, scale);

			float _360 = Mathf.PI * 2f;
			for (int i = 0; ++i < count; )
			{
				float angle = i * _360 / points.Length;
				points[i] = Vector3.Scale(normal == 0 ? RotateYaw(vector, angle) : (normal == 1 ? RotateRoll(vector, angle) : RotatePitch(vector, angle)), scale);
			}

			Undo.RecordObject(trail, "Make Point");

			trail.Points = points;

			EditorUtility.SetDirty(trail);
		}

		void ToggleMode(Edition edition, GUIContent content)
		{
			if (GUILayout.Toggle(this.edition == edition, content, GUI.skin.button, GUILayout.Width(32f), GUILayout.Height(32f)))
				this.edition = edition;
			else
			{
				if (this.edition == edition)
					this.edition = Edition.None;
			}
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.Space(5);

			if (Application.isPlaying)
				return;

			GUILayout.BeginHorizontal();
			ToggleMode(Edition.Point, EditorGUIUtility.IconContent("d_ToolHandleLocal"));
			ToggleMode(Edition.Position, EditorGUIUtility.IconContent("d_MoveTool"));
			ToggleMode(Edition.Rotate, EditorGUIUtility.IconContent("d_RotateTool"));
			ToggleMode(Edition.Scale, EditorGUIUtility.IconContent("d_ScaleTool"));
			GUILayout.EndHorizontal();

			foldout = EditorGUILayout.Foldout(foldout, "Making a circle");
			if (foldout)
			{
				EditorGUI.indentLevel++;
				center = EditorGUILayout.Vector3Field("Center", center);
				normal = EditorGUILayout.Popup("Normal", normal, new string[] { "Up", "Forward", "Right" });
				radius = EditorGUILayout.FloatField("Radius", radius);
				count = Mathf.Max(3, EditorGUILayout.IntField("Count", count));
				if (GUILayout.Button("Make"))
					Make();
				EditorGUI.indentLevel--;
			}
		}
	}
}

#endif