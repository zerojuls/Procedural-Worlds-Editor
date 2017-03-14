﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PW
{
	[System.SerializableAttribute]
	public class PWNodeGraph : ScriptableObject {
	
		[SerializeField]
		public List< PWNode >				nodes = new List< PWNode >();
		
		[SerializeField]
		public HorizontalSplitView			h1;
		[SerializeField]
		public HorizontalSplitView			h2;
	
		[SerializeField]
		public Vector2			leftBarScrollPosition;
		[SerializeField]
		public Vector2			selectorScrollPosition;
	
		[SerializeField]
		public Vector2			graphDecalPosition;
		[SerializeField]
		public Vector2			lastMousePosition;
		[SerializeField]
		public bool			dragginGraph = false;
		[SerializeField]
		public bool			mouseAboveNodeAnchor = false;
		[SerializeField]
		public int				localWindowIdCount;
		[SerializeField]
		public string			firstInitialization;
		
		[SerializeField]
		public PWAnchorInfo	startDragAnchor;
		[SerializeField]
		public bool			draggingLink = false;
		
		[SerializeField]
		public string			searchString = "";
	}
}