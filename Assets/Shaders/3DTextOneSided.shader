﻿/*
This Shader was made by Eric Haines (Eric5h5) and retrieved from the Unity community Wiki.
Available from: http://wiki.unity3d.com/index.php?title=3DText
*/

Shader "GUI/3D Text Shader - Cull Back" {
	Properties{
		_MainTex("Font Texture", 2D) = "white" {}
	_Color("Text Color", Color) = (1,1,1,1)
	}

		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Lighting Off Cull Back ZWrite Off Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass{
		Color[_Color]
		SetTexture[_MainTex]{
		combine primary, texture * primary
	}
	}
	}
}