﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using ProceduralWorlds;
using ProceduralWorlds.Core;
using ProceduralWorlds.Biomator;
using ProceduralWorlds.IsoSurfaces;

public class TopDownNaive2DTerrain : TerrainBase< TopDownChunkData >
{
	public float	yPosition;
	public bool		heightDisplacement;
	public float	heightScale = .1f;

	Gradient		rainbow;

	Naive2DIsoSurface	isoSurface = new Naive2DIsoSurface();

	protected override void OnTerrainEnable()
	{
		//global settings, not depending from the editor
		generateBorders = true;
		isoSurface.generateUvs = true;
	}

	void	UpdateMeshDatas(Mesh mesh, BiomeMap2D biomes)
	{
		List< Vector4 >		blendInfos = new List< Vector4 >();

		for (int x = 0; x < chunkSize; x++)
			for (int z = 0; z < chunkSize; z++)
			{
				Vector4 biomeInfo = Vector4.zero;
				blendInfos.Add(biomeInfo);
			}
		mesh.SetUVs(1, blendInfos);
	}
	
	public override object	OnChunkCreate(TopDownChunkData chunk, Vector3 pos)
	{
		if (chunk == null)
			return null;
		
		if (rainbow == null)
			rainbow = Utils.CreateRainbowGradient();

		pos = GetChunkWorldPosition(pos);
		
		GameObject g = CreateChunkObject(pos);
		
		MeshRenderer mr = g.AddComponent< MeshRenderer >();
		MeshFilter mf = g.AddComponent< MeshFilter >();

		if (heightDisplacement)
			isoSurface.SetHeightDisplacement(chunk.terrain as Sampler2D, heightScale);
		else
			isoSurface.SetHeightDisplacement(null, 0);
	
		Mesh m = isoSurface.Generate(chunkSize);
			
		UpdateMeshDatas(m, chunk.biomeMap);

		mf.sharedMesh = m;

		Shader topDown2DBasicTerrainShader = Shader.Find("Standard");
		Material mat = new Material(topDown2DBasicTerrainShader);
		mr.sharedMaterial = mat;
		return g;
	}

	public override void 	OnChunkDestroy(TopDownChunkData terrainData, object userStoredObject, Vector3 pos)
	{
		GameObject g = userStoredObject as GameObject;

		if (g != null)
			DestroyImmediate(g);
	}

	public override void	OnChunkRender(TopDownChunkData chunk, object chunkGameObject, Vector3 pos)
	{
		if (chunk == null)
			return ;
		
		GameObject		g = chunkGameObject as GameObject;

		if (g == null) //if gameobject have been destroyed by user and reference was lost.
			RequestCreate(chunk, pos);
	}
	
	public override Vector3 GetChunkPosition(Vector3 pos)
	{
		pos.y = yPosition;

		return pos;
	}
}