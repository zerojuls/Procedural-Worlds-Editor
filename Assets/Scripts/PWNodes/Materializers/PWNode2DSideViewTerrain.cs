﻿using UnityEditor;
using UnityEngine;

namespace PW
{
	public class PWNodeSideView2DTerrain : PWNode {
	
		[PWInput("TEX")]
		public Sampler2D		texture;

		[PWOutput("MAP")]
		public SideView2DData	terrainOutput;

		private Texture2D		samplerTexture;

		public override void OnNodeCreate()
		{
			name = "2D SideView terrain";
			samplerTexture = new Texture2D(chunkSize, chunkSize, TextureFormat.ARGB32, false, false);
			texture = new Sampler2D(chunkSize);
		}

		public override void OnNodeGUI()
		{
			EditorGUILayout.LabelField("MAP:");
			
			if (chunkSizeHasChanged)
			{
				samplerTexture = new Texture2D(chunkSize, chunkSize, TextureFormat.ARGB32, false, false);
				texture = new Sampler2D(chunkSize);
			}
			
			if (seedHasChanged || positionHasChanged || chunkSizeHasChanged)
				texture.Foreach((x, y, val) => {samplerTexture.SetPixel(x, y, Color.blue * val);});
		}

		public override void OnNodeProcess()
		{
			terrainOutput.size = Vector2.one * chunkSize;
			// terrainOutput.texture = samplerTexture;
		}

	}
}