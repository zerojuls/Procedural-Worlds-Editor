﻿using UnityEngine;
using PW;
using PW.Core;

public class PWTopDown2DTerrain : PWTerrainBase {

	static Gradient			rainbow = null;

	void Start () {
		InitGraph(graph);
	}
	
	public override object OnChunkCreate(ChunkData cd, Vector3 pos)
	{
		if (cd == null)
			return null;
		
		if (rainbow == null)
			rainbow = PWUtils.CreateRainbowGradient();
		
		TopDown2DData	chunk = (TopDown2DData)cd;
		
		//create the terrain texture:
		//TODO: bind the blendMap with biome maps to the terrain shader

		GameObject g = CreateChunkObject(pos, PrimitiveType.Quad);
		g.transform.rotation = Quaternion.Euler(90, 0, 0);
		g.transform.localScale = Vector3.one * chunkSize;
		var mat = g.GetComponent< MeshRenderer >().sharedMaterial;
		mat.SetTexture("_AlbedoMaps", chunk.albedoMaps);
		mat.SetTexture("_BlendMaps", chunk.blendMaps);
		if (chunk.blendMaps != null)
			mat.SetInt("_BlendMapsCount", chunk.blendMaps.depth);
		return g;
	}

	public override void OnChunkRender(ChunkData cd, object chunkGameObject, Vector3 pos)
	{
		if (cd == null)
			return ;
		GameObject		g = chunkGameObject as GameObject;
		TopDown2DData	chunk = (TopDown2DData)cd;

		if (g == null) //if gameobject have been destroyed by user and reference was lost.
		{
			chunkGameObject = RequestCreate(cd, pos);
			g = chunkGameObject as GameObject;
		}
		g.GetComponent< MeshRenderer >().sharedMaterial.SetTexture("_MainTex", chunk.texture);
	}
}
