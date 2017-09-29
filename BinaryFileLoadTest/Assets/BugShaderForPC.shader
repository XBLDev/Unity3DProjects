// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/*
* Copyright 2016 Google Inc. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

Shader "Custom/UnlitVertexColorEdited" {
	Properties{
		//_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
		//annotationFacePos2("display", vector) = (0.0 , 0.0 , 0.0 , 0.0)
		//mousePositionx("mouse position x", Float) = 20.0
		//currentSelectedPointX("current selected pos", Float) = 0.0
	}

	//sampler2D _MainTex;

		SubShader{


		//
		LOD 200

		Pass{
		CGPROGRAM
		// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
		//#pragma exclude_renderers d3d11 gles
		//#pragma exclude_renderers d3d11 xbox360
		//#pragma exclude_renderers d3d11_9x

		#pragma target 3.0
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		struct IN
	{
		float4 pos : POSITION;
		float4 color : COLOR;
		float3 normal: NORMAL;
		float2 uv: TEXCOORD0;
	};

	struct v2f
	{
		//float4 originalPosition: POSITION;
		float4 position : SV_POSITION;
		float4 color : COLOR;
		float3 normal: NORMAL;
		float2 uv: TEXCOORD0;
		float4 screenpos : TEXCOORD1;

	};

	sampler2D _MainTex;
	float mousePositionx = 0;
	float mousePositiony = 0;
	float screenWidth = 0;
	float screenHeight = 0;
	int hasAnnotationFaces = 0;
	int isDrawingLine = 0;
	int isAnnotationFacePoint = 0;
	int lengthOfAnnotation = 0;
	vector annotationFaces[500];
	vector faceDrawingPositions[500];

	float3 annotationFacePos1;
	float3 annotationFacePos2;
	float3 annotationFacePos3;
	float3 annotationFacePos4;

	float currentSelectedPointX;
	float currentSelectedPointY;
	float currentSelectedPointZ;

	float faceRadius = 0.025;
	int isInEditMode = 0;
	int isInLineMode = 0;
	int isInFaceMode = 0;
	int lengthOfFaceDrawnPassed = 0;


	v2f vert(IN input) {
		v2f o;

		//o.originalPosition = input.pos;
		o.position = UnityObjectToClipPos(input.pos);

		o.normal = input.normal;
		o.uv = input.uv;
		o.screenpos = ComputeScreenPos(o.position);

		o.color = float4(0, 0, 0, 0);

		//int c = 0;
		if (hasAnnotationFaces == 1 && isInEditMode == 0)//<------------HAS ANNOTATION INPUT
		{

			for (int i = 0; i < 500; i++)
			{
				//c++;
				if (
					(annotationFaces[i].x > 0 || annotationFaces[i].x < 0) &&
					(annotationFaces[i].y > 0 || annotationFaces[i].y < 0) &&
					(annotationFaces[i].z > 0 || annotationFaces[i].z < 0)
					)
				{


					vector temp = annotationFaces[i];
					float x = temp.x;
					float y = temp.y;
					float z = temp.z;

					float xDifference = 0;
					float yDifference = 0;
					float zDifference = 0;

					if (input.pos.x >= x)
					{
						xDifference = input.pos.x - x;
					}
					else
					{
						xDifference = x - input.pos.x;

					}

					if (input.pos.y >= y)
					{
						yDifference = input.pos.y - y;
					}
					else
					{
						yDifference = y - input.pos.y;

					}

					if (input.pos.z >= z)
					{
						zDifference = input.pos.z - z;
					}
					else
					{
						zDifference = z - input.pos.z;

					}

					if (xDifference < 0.1 &&
						yDifference < 0.1 &&
						zDifference < 0.1
						)
					{
						o.color = float4(0, 255, 0, 255);
						//c = 1;
						break;
					}
					else
					{
						//c = 0;
						o.color = float4(0, 0, 0, 255);
						if (i == lengthOfAnnotation - 1)
						{
							break;
						}
					}
				}

			}

		}

		if (isInEditMode == 1 && hasAnnotationFaces == 0)//EDIT MODE
		{

			if (isInFaceMode == 1 && isInLineMode == 0)//FACE MODE
			{

				if (lengthOfFaceDrawnPassed == 0)//NO FACE PASSED IN, NO NEED FOR PROCESSING
				{
					o.color = float4(0, 0, 0, 255);
				}
				else//FACES PASSED IN, NEED PROCESSING
				{
					//int c = 0;
					for (int a = 0; a < 500; a++)
					{

						//if (
						//	(faceDrawingPositions[a].x > 0 || faceDrawingPositions[a].x < 0) &&
						//	(faceDrawingPositions[a].y > 0 || faceDrawingPositions[a].y < 0) &&
						//	(faceDrawingPositions[a].z > 0 || faceDrawingPositions[a].z < 0)
						//	)
						//{//IF CURRENT FACE DRAWING POINT IS NOT NULL

						vector temp = faceDrawingPositions[a];
						float x = temp.x;
						float y = temp.y;
						float z = temp.z;

						float xDifference = 0;
						float yDifference = 0;
						float zDifference = 0;

						if (input.pos.x  >= x)
							//if (o.position.x >= x)

						{
							xDifference = input.pos.x  - x;
							//xDifference = o.position.x - x;

						}
						else
						{
							xDifference = x - input.pos.x ;
							//xDifference = x - o.position.x;

						}

						if (input.pos.y  >= y)
							//if (o.position.y >= y)

						{
							yDifference = input.pos.y - y;
							//yDifference = o.position.y - y;

						}
						else
						{
							yDifference = y - input.pos.y ;
							//yDifference = y - o.position.y;

						}

						if (input.pos.z >= z)
							//if (o.position.z >= z)

						{
							zDifference = input.pos.z  - z;
							//zDifference = o.position.z - z;

						}
						else
						{
							zDifference = z - input.pos.z ;
							//zDifference = z - o.position.z;

						}

						if (xDifference < 1 &&//1 works on PC
							yDifference < 1 &&//1 works on PC
							zDifference < 1//1 works on PC
							)
						{
							o.color = float4(0, 255, 0, 255);
							//c = 255;
							break;
						}
						else
						{
							o.color = float4(0, 0, 0, 255);
							//c = 0;
							if (a == lengthOfFaceDrawnPassed - 1)
							{
								break;
							}
						}


						//}//IF CURRENT FACE DRAWING POINT IS NOT NULL/INVALID
						//else//IF CURRENT FACE DRAWING POINT IS NULL/INVALID
						//{
						//	o.color = float4(0, 0, 0, 255);
						//	break;
						//}
						//o.color = float4(0, 255, 0, 255);
						//break;
					}//FOR LOOP END

					 //o.color = float4(0, c, 0, 255);
				}//FACES PASSED IN, NEED PROCESSING
			}//FACE MODE

			else if (isInFaceMode == 0 && isInLineMode == 1)//LINE MODE
			{
				o.color = float4(255, 255, 255, 255);

			}
			else//NOT IN FACE OR LINE MODE, STILL IN EDIT MODE
			{
				o.color = float4(255,255,255,255);
			}
		}//EDIT MODE

		if (hasAnnotationFaces == 0 && isInEditMode == 0)//<-------------------NO ANNOTATION INPUT && NO EDIT MODE
		{
			o.color = float4(255, 255, 255, 255);
		}
		//o.color = float4(255,0,0,255);


		return o;
	}

	fixed4 frag(v2f i) : COLOR{



		if (hasAnnotationFaces == 0)//HAS NO ANNOTATIONS
		{


			if (isInEditMode == 0)//NOT IN EDIT MODE
			{
				float2 temp = i.screenpos.xy / i.screenpos.ww;

				if (abs(temp.y - (mousePositiony / screenHeight)) < 0.001

					&& abs(temp.x - (mousePositionx / screenWidth)) < 0.001
					)//CLOSE ENOUGHT TO THE CAMERA RAY POINT, RETURN CURSOR COLOR

				{


					return fixed4(0, 255, 0, 255);


				}
				else//NOT CLOSE ENOUGH TO THE CAMERA RAY POINT, RETURN TEXTURE COLOR
				{
					return tex2D(_MainTex, i.uv);
				}
			}
			//else
			//{
			//	return tex2D(_MainTex, i.uv);
			//}
			else//IN EDIT MODE
			{
				if (isInFaceMode == 0 && isInLineMode == 1)
					//IN LINE MODE, JUST TEST IF POINT IS CLOSE ENOUGH TO CAMERA RAY
				{
					float2 temp = i.screenpos.xy / i.screenpos.ww;

					if (abs(temp.y - (mousePositiony / screenHeight)) < 0.001

						&& abs(temp.x - (mousePositionx / screenWidth)) < 0.001
						)//CLOSE ENOUGH TO THE CAMERA RAY POINT, RETURN CURSOR COLOR

					{
						//{
						return fixed4(0, 255, 0, 255);
						//}

					}
					else//NOT CLOSE ENOUGH TO THE CAMERA RAY POINT, RETURN TEXTURE COLOR
					{
						//return i.color;
						return tex2D(_MainTex, i.uv);
					}
				}
				else if (isInFaceMode == 1 && isInLineMode == 0)
					//IN FACE MODE, USE VERTEX SHADER COLOR AS REFERENCE
				{
					if (i.color.g == 255)
						//POINT IS CLOSE TO ONE OF THE PASSED IN FACE DRAWING
						//POINTS, GREEN VALUE IS 255, USE VERTEX SHADER COLOR
					{
						return i.color;
					}
					else
						//POINT NOT CLOSE ENOUGH TO ANY OF THE FACE POINTS, TEST
						//IF POINT IS CLOSE TO RAY, IF NOT USE TEXTURE COLOR
					{

						float2 temp = i.screenpos.xy / i.screenpos.ww;

						if (abs(temp.y - (mousePositiony / screenHeight)) < 0.001

							&& abs(temp.x - (mousePositionx / screenWidth)) < 0.001
							)//CLOSE ENOUGHT TO THE CAMERA RAY POINT, RETURN CURSOR COLOR

						{


							return fixed4(255, 0, 0, 255);


						}
						else//NOT CLOSE ENOUGH TO THE CAMERA RAY POINT, RETURN TEXTURE COLOR
						{
							return tex2D(_MainTex, i.uv);
						}
					}
				}
				else//NOT IN LINE OR FACE MODE, STILL IN EDIT MODE
				{
					float2 temp = i.screenpos.xy / i.screenpos.ww;

					if (abs(temp.y - (mousePositiony / screenHeight)) < 0.001

						&& abs(temp.x - (mousePositionx / screenWidth)) < 0.001
						)//CLOSE ENOUGH TO THE CAMERA RAY POINT, RETURN CURSOR COLOR

					{
						//{
						return fixed4(0, 255, 0, 255);
						//}

					}
					else//NOT CLOSE ENOUGH TO THE CAMERA RAY POINT, RETURN TEXTURE COLOR
					{
						//return i.color;
						return tex2D(_MainTex, i.uv);
					}
				}
			}
		}
		else//HAS ANNOTATIONS
		{
			if (i.color.g == 255)//IS ONE OF ANNOTATION POINTS
			{
				float2 temp = i.screenpos.xy / i.screenpos.ww;

				//return fixed4(mousePositionx/255, 0, 0, 255);
				if (abs(temp.y - (mousePositiony / screenHeight)) < 0.001//0.001

					&& abs(temp.x - (mousePositionx / screenWidth)) < 0.001//0.001
					)

				{
					return fixed4(0, 255, 0, 255);

				}
				else
				{
					return float4(i.color.r, i.color.g, i.color.b, 255);
					//return i.color;
				}

				//return i.color;
			}
			else//NOT ONE OF ANNOTATION POINTS
			{
				float2 temp = i.screenpos.xy / i.screenpos.ww;

				//return fixed4(mousePositionx/255, 0, 0, 255);
				if (abs(temp.y - (mousePositiony / screenHeight)) < 0.001//0.001

					&& abs(temp.x - (mousePositionx / screenWidth)) < 0.001//0.001
					)

				{
					return fixed4(0, 255, 0, 255);

				}
				else
				{
					//return fixed4(0, 0, 0, 255);
					return tex2D(_MainTex, i.uv);
				}
			}
			//<-------------------EXPERIMENTAL CODE BLOEW----------------->

		}

	}


		ENDCG
	}
	}
}