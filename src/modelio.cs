///
// Authors:
//  Miguel de Icaza (miguel@xamarin.com)
//
// Copyright 2015 Xamarin, Inc.
//
//
#if XAMCORE_2_0 || !MONOMAC
using System;
using System.ComponentModel;

using XamCore.AudioUnit;
using XamCore.CoreFoundation;
using XamCore.Foundation;
using XamCore.ObjCRuntime;
using XamCore.CoreAnimation;
using XamCore.CoreGraphics;
using XamCore.SceneKit;
using Vector2i = global::OpenTK.Vector2i;
using Vector2 = global::OpenTK.Vector2;
using Vector3 = global::OpenTK.Vector3;
using Vector3i = global::OpenTK.Vector3i;
using Vector4 = global::OpenTK.Vector4;
using Vector4i = global::OpenTK.Vector4i;
using Matrix2 = global::OpenTK.Matrix2;
using Matrix3 = global::OpenTK.Matrix3;
using Matrix4 = global::OpenTK.Matrix4;
using Quaternion = global::OpenTK.Quaternion;
using MathHelper = global::OpenTK.MathHelper;
#if MONOMAC
using XamCore.AppKit;
using AUViewControllerBase = XamCore.AppKit.NSViewController;
#else
using XamCore.UIKit;
using AUViewControllerBase = XamCore.UIKit.UIViewController;
#endif

namespace XamCore.ModelIO {

	[iOS (9,0)][Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLPhysicallyPlausibleLight))]
	[DisableDefaultCtor]
	interface MDLAreaLight
	{
		[Export ("areaRadius")]
		float AreaRadius { get; set; }

		[Export ("superEllipticPower", ArgumentSemantic.Assign)]
		Vector2 SuperEllipticPower {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			set;
		}

		[Export ("aspect")]
		float Aspect { get; set; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLAsset : NSCopying
	{
		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithURL:vertexDescriptor:bufferAllocator:")]
		IntPtr Constructor (NSUrl url, [NullAllowed] MDLVertexDescriptor vertexDescriptor, [NullAllowed] IMDLMeshBufferAllocator bufferAllocator);

		[Export ("initWithURL:vertexDescriptor:bufferAllocator:preserveTopology:error:")]
		IntPtr Constructor (NSUrl url, [NullAllowed] MDLVertexDescriptor vertexDescriptor, [NullAllowed] IMDLMeshBufferAllocator bufferAllocator, bool preserveTopology, out NSError error);

		// note: by choice we do not export "exportAssetToURL:"
		[Export ("exportAssetToURL:error:")]
		bool ExportAssetToUrl (NSUrl url, out NSError error);

		[Static]
		[Export ("canImportFileExtension:")]
		bool CanImportFileExtension (string extension);

		[Static]
		[Export ("canExportFileExtension:")]
		bool CanExportFileExtension (string extension);

		[Export ("boundingBoxAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLAxisAlignedBoundingBox GetBoundingBox (double atTime);

		[Export ("boundingBox")]
		MDLAxisAlignedBoundingBox BoundingBox { get; }

		[Export ("frameInterval")]
		double FrameInterval { get; set; }

		[Export ("startTime")]
		double StartTime { get; set; }

		[Export ("endTime")]
		double EndTime { get; set; }

		[NullAllowed, Export ("URL", ArgumentSemantic.Retain)]
		NSUrl Url { get; }

		[Export ("bufferAllocator", ArgumentSemantic.Retain)]
		IMDLMeshBufferAllocator BufferAllocator { get; }

		[NullAllowed, Export ("vertexDescriptor", ArgumentSemantic.Retain)]
		MDLVertexDescriptor VertexDescriptor { get; }

		[Export ("addObject:")]
		void AddObject (MDLObject @object);

		[Export ("removeObject:")]
		void RemoveObject (MDLObject @object);

		[Export ("count")]
		nuint Count { get; }

		[Export ("objectAtIndexedSubscript:")]
		[return: NullAllowed]
		MDLObject GetObjectAtIndexedSubscript (nuint index);

		[Export ("objectAtIndex:")]
		MDLObject GetObject (nuint index);

		[iOS (9,0), Mac(10,11)]
		[Static]
		[Export ("assetWithSCNScene:")]
		MDLAsset FromScene (SCNScene scene);
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLObject))]
	interface MDLCamera
	{
		[Export ("projectionMatrix")]
		Matrix4 ProjectionMatrix {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
		}

		[Export ("frameBoundingBox:setNearAndFar:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] 
		void FrameBoundingBox (MDLAxisAlignedBoundingBox boundingBox, bool setNearAndFar);

		[Export ("lookAt:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] 
		void LookAt (Vector3 focusPosition);

		[Export ("lookAt:from:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] 
		void LookAt (Vector3 focusPosition, Vector3 cameraPosition);

		[Export ("rayTo:forViewPort:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Vector3 RayTo (Vector2i pixel, Vector2i size);

		[Export ("nearVisibilityDistance")]
		float NearVisibilityDistance { get; set; }

		[Export ("farVisibilityDistance")]
		float FarVisibilityDistance { get; set; }

		[Export ("barrelDistortion")]
		float BarrelDistortion { get; set; }

		[Export ("worldToMetersConversionScale")]
		float WorldToMetersConversionScale { get; set; }

		[Export ("fisheyeDistortion")]
		float FisheyeDistortion { get; set; }

		[Export ("opticalVignetting")]
		float OpticalVignetting { get; set; }

		[Export ("chromaticAberration")]
		float ChromaticAberration { get; set; }

		[Export ("focalLength")]
		float FocalLength { get; set; }

		[Export ("focusDistance")]
		float FocusDistance { get; set; }
		
		[Export ("fieldOfView")]
		float FieldOfView { get; set; }

		[Export ("fStop")]
		float FStop { get; set; }

		[Export ("apertureBladeCount", ArgumentSemantic.Assign)]
		nuint ApertureBladeCount { get; set; }

		[Export ("maximumCircleOfConfusion")]
		float MaximumCircleOfConfusion { get; set; }

		[Export ("bokehKernelWithSize:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLTexture BokehKernelWithSize (Vector2i size);

		[Export ("shutterOpenInterval")]
		double ShutterOpenInterval { get; set; }

		[Export ("sensorVerticalAperture")]
		float SensorVerticalAperture { get; set; }

		[Export ("sensorAspect")]
		float SensorAspect { get; set; }

		[Export ("sensorEnlargement", ArgumentSemantic.Assign)]
		Vector2 SensorEnlargement {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Export ("sensorShift", ArgumentSemantic.Assign)]
		Vector2 SensorShift {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Export ("flash", ArgumentSemantic.Assign)]
		Vector3 Flash {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Export ("exposureCompression", ArgumentSemantic.Assign)]
		Vector2 ExposureCompression {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Export ("exposure", ArgumentSemantic.Assign)]
		Vector3 Exposure { 
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[iOS (9,0), Mac(10,11)]
		[Static]
		[Export ("cameraWithSCNCamera:")]
		MDLCamera FromSceneCamera (SCNCamera sceneCamera);
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLTexture))]
	[DisableDefaultCtor]
	interface MDLCheckerboardTexture
	{
		[Export ("initWithData:topLeftOrigin:name:dimensions:rowStride:channelCount:channelEncoding:isCube:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] NSData pixelData, bool topLeftOrigin, [NullAllowed] string name, Vector2i dimensions, nint rowStride, nuint channelCount, MDLTextureChannelEncoding channelEncoding, bool isCube);

		// -(instancetype __nonnull)initWithDivisions:(float)divisions name:(NSString * __nullable)name dimensions:(vector_int2)dimensions channelCount:(int)channelCount channelEncoding:(MDLTextureChannelEncoding)channelEncoding color1:(CGColorRef __nonnull)color1 color2:(CGColorRef __nonnull)color2;
		[Export ("initWithDivisions:name:dimensions:channelCount:channelEncoding:color1:color2:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (float divisions, [NullAllowed] string name, Vector2i dimensions, int channelCount, MDLTextureChannelEncoding channelEncoding, CGColor color1, CGColor color2);

		[Export ("divisions")]
		float Divisions { get; set; }

		[NullAllowed]
		[Export ("color1", ArgumentSemantic.Assign)]
		CGColor Color1 { get; set; }

		[NullAllowed]
		[Export ("color2", ArgumentSemantic.Assign)]
		CGColor Color2 { get; set; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLTexture))]
	[DisableDefaultCtor]
	interface MDLColorSwatchTexture
	{
		[Export ("initWithData:topLeftOrigin:name:dimensions:rowStride:channelCount:channelEncoding:isCube:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] NSData pixelData, bool topLeftOrigin, [NullAllowed] string name, Vector2i dimensions, nint rowStride, nuint channelCount, MDLTextureChannelEncoding channelEncoding, bool isCube);

		[Export ("initWithColorTemperatureGradientFrom:toColorTemperature:name:textureDimensions:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (float colorTemperature1, float colorTemperature2, [NullAllowed] string name, Vector2i textureDimensions);

		[Export ("initWithColorGradientFrom:toColor:name:textureDimensions:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (CGColor color1, CGColor color2, [NullAllowed] string name, Vector2i textureDimensions);
	}


	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLObject))]
	interface MDLLight
	{
		[Export ("irradianceAtPoint:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		CGColor GetIrradiance (Vector3 point);

		[Export ("irradianceAtPoint:colorSpace:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		CGColor GetIrradiance (Vector3 point, CGColorSpace colorSpace);

		[Export ("lightType")]
		MDLLightType LightType { get; set; }

		[iOS (9,0), Mac(10,11)]
		[Static]
		[Export ("lightWithSCNLight:")]
		MDLLight FromSceneLight (SCNLight sceneLight);
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLLight))]
	interface MDLLightProbe
	{
		[Export ("initWithReflectiveTexture:irradianceTexture:")]
		IntPtr Constructor ([NullAllowed] MDLTexture reflectiveTexture, [NullAllowed] MDLTexture irradianceTexture);

		[Export ("generateSphericalHarmonicsFromIrradiance:")]
		void GenerateSphericalHarmonicsFromIrradiance (nuint sphericalHarmonicsLevel);

		[NullAllowed, Export ("reflectiveTexture", ArgumentSemantic.Retain)]
		MDLTexture ReflectiveTexture { get; }

		[NullAllowed, Export ("irradianceTexture", ArgumentSemantic.Retain)]
		MDLTexture IrradianceTexture { get; }

		[Export ("sphericalHarmonicsLevel")]
		nuint SphericalHarmonicsLevel { get; }

		[NullAllowed, Export ("sphericalHarmonicsCoefficients", ArgumentSemantic.Copy)]
		NSData SphericalHarmonicsCoefficients { get; }

		// inlined from MDLLightBaking (MDLLightProbe)
		// reason: static protocol members made very bad extensions methods

		[Static]
		[Export ("lightProbeWithTextureSize:forLocation:lightsToConsider:objectsToConsider:reflectiveCubemap:irradianceCubemap:")]
		[return: NullAllowed]
		MDLLightProbe Create (nint textureSize, MDLTransform transform, MDLLight[] lightsToConsider, MDLObject[] objectsToConsider, [NullAllowed] MDLTexture reflectiveCubemap, [NullAllowed] MDLTexture irradianceCubemap);
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLMaterial : MDLNamed, INSFastEnumeration
	{
		[Export ("initWithName:scatteringFunction:")]
		IntPtr Constructor (string name, MDLScatteringFunction scatteringFunction);

		[Export ("setProperty:")]
		void SetProperty (MDLMaterialProperty property);

		[Export ("removeProperty:")]
		void RemoveProperty (MDLMaterialProperty property);

		[Export ("propertyNamed:")]
		[return: NullAllowed]
		MDLMaterialProperty GetProperty (string name);

		[Export ("propertyWithSemantic:")]
		[return: NullAllowed]
		MDLMaterialProperty GetProperty (MDLMaterialSemantic semantic);

		[Export ("removeAllProperties")]
		void RemoveAllProperties ();

		[Export ("scatteringFunction", ArgumentSemantic.Retain)]
		MDLScatteringFunction ScatteringFunction { get; }

		[NullAllowed, Export ("baseMaterial", ArgumentSemantic.Retain)]
		MDLMaterial BaseMaterial { get; set; }

		[Export ("objectAtIndexedSubscript:")]
		[Internal]
		[return: NullAllowed]
		MDLMaterialProperty ObjectAtIndexedSubscript (nuint idx);

		[Export ("objectForKeyedSubscript:")]
		[Internal]
		[return: NullAllowed]
		MDLMaterialProperty ObjectForKeyedSubscript (string name);

		[Export ("count")]
		nuint Count { get; }

		[iOS (9,0), Mac(10,11)]
		[Static]
		[Export ("materialWithSCNMaterial:")]
		MDLMaterial FromSceneMaterial (SCNMaterial material);
	}

	[iOS (9,0)][Mac (10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface MDLMaterialProperty : MDLNamed
	{
		[DesignatedInitializer]
		[Export ("initWithName:semantic:")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic);

		[Export ("initWithName:semantic:float:")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, float value);

		[Export ("initWithName:semantic:float2:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, Vector2 value);

		[Export ("initWithName:semantic:float3:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, Vector3 value);

		[Export ("initWithName:semantic:float4:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, Vector4 value);

		[Export ("initWithName:semantic:matrix4x4:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, Matrix4 value);

		[Export ("initWithName:semantic:URL:")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, [NullAllowed] NSUrl url);

		[Export ("initWithName:semantic:string:")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, [NullAllowed] string stringValue);

		[Export ("initWithName:semantic:textureSampler:")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, [NullAllowed] MDLTextureSampler textureSampler);

		[Export ("initWithName:semantic:color:")]
		IntPtr Constructor (string name, MDLMaterialSemantic semantic, CGColor color);

		[Export ("setProperties:")]
		void SetProperties (MDLMaterialProperty property);

		[Export ("semantic", ArgumentSemantic.Assign)]
		MDLMaterialSemantic Semantic { get; set; }

		[Export ("type")]
		MDLMaterialPropertyType Type { get; }

		[NullAllowed, Export ("stringValue")]
		string StringValue { get; set; }

		[NullAllowed, Export ("URLValue", ArgumentSemantic.Copy)]
		NSUrl UrlValue { get; set; }

		[NullAllowed, Export ("textureSamplerValue", ArgumentSemantic.Retain)]
		MDLTextureSampler TextureSamplerValue { get; set; }

		[NullAllowed]
		[Export ("color", ArgumentSemantic.Assign)]
		CGColor Color { get; set; }

		[Export ("floatValue")]
		float FloatValue { get; set; }

		[Export ("float2Value", ArgumentSemantic.Assign)]
		Vector2 Float2Value {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Export ("float3Value", ArgumentSemantic.Assign)]
		Vector3 Float3Value {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Export ("float4Value", ArgumentSemantic.Assign)]
		Vector4 Float4Value {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Export ("matrix4x4", ArgumentSemantic.Assign)]
		Matrix4 Matrix4x4 {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLObject))]
	interface MDLMesh
	{
		[Export ("initWithVertexBuffer:vertexCount:descriptor:submeshes:")]
		IntPtr Constructor (IMDLMeshBuffer vertexBuffer, nuint vertexCount, MDLVertexDescriptor descriptor, MDLSubmesh [] submeshes);

		[Export ("initWithVertexBuffers:vertexCount:descriptor:submeshes:")]
		IntPtr Constructor (IMDLMeshBuffer[] vertexBuffers, nuint vertexCount, MDLVertexDescriptor descriptor, MDLSubmesh[] submeshes);

		[Internal]
		[Export ("vertexAttributeDataForAttributeNamed:")]
		[return: NullAllowed]
		MDLVertexAttributeData GetVertexAttributeDataForAttribute (string attributeName);

		[Export ("boundingBox")]
		MDLAxisAlignedBoundingBox BoundingBox {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
		}

		[Export ("vertexDescriptor", ArgumentSemantic.Copy), NullAllowed]
		MDLVertexDescriptor VertexDescriptor { get; set; }

		[Export ("vertexCount")]
		nuint VertexCount { get; }

		[Export ("vertexBuffers", ArgumentSemantic.Retain)]
		IMDLMeshBuffer[] VertexBuffers { get; }

		[Export ("submeshes", ArgumentSemantic.Retain)]
		NSMutableArray<MDLSubmesh> Submeshes { get; }

		// These are categories on MDLMesh, so I am inlining them here
		[Export ("addAttributeWithName:format:")]
		void AddAttribute (string name, MDLVertexFormat format);

		[Export ("addNormalsWithAttributeNamed:creaseThreshold:")]
		void AddNormals ([NullAllowed] string name, float creaseThreshold);

		[Export ("addTangentBasisForTextureCoordinateAttributeNamed:tangentAttributeNamed:bitangentAttributeNamed:")]
		void AddTangentBasis (string textureCoordinateAttributeName, string tangentAttributeName, [NullAllowed] string bitangentAttributeName);

		[Export ("addTangentBasisForTextureCoordinateAttributeNamed:normalAttributeNamed:tangentAttributeNamed:")]
		void AddTangentBasisWithNormals (string textureCoordinateAttributeName, string normalAttributeName, string tangentAttributeName);

		[Export ("makeVerticesUnique")]
		void MakeVerticesUnique ();

		[Static]
		[Export ("newBoxWithDimensions:segments:geometryType:inwardNormals:allocator:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLMesh CreateBox (Vector3 dimensions, Vector3i segments, MDLGeometryType geometryType, bool inwardNormals, [NullAllowed] IMDLMeshBufferAllocator allocator);

		[Static]
		[Export ("newPlaneWithDimensions:segments:geometryType:allocator:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLMesh CreatePlane (Vector2 dimensions, Vector2i segments, MDLGeometryType geometryType, [NullAllowed] IMDLMeshBufferAllocator allocator);

		[Static]
		[Export ("newEllipsoidWithRadii:radialSegments:verticalSegments:geometryType:inwardNormals:hemisphere:allocator:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLMesh CreateEllipsoid (Vector3 radii, nuint radialSegments, nuint verticalSegments, MDLGeometryType geometryType, bool inwardNormals, bool hemisphere, [NullAllowed] IMDLMeshBufferAllocator allocator);

		[Static]
		[Export ("newCylinderWithHeight:radii:radialSegments:verticalSegments:geometryType:inwardNormals:allocator:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLMesh CreateCylindroid (float height, Vector2 radii, nuint radialSegments, nuint verticalSegments, MDLGeometryType geometryType, bool inwardNormals, [NullAllowed] IMDLMeshBufferAllocator allocator);

		[Static]
		[Export ("newEllipticalConeWithHeight:radii:radialSegments:verticalSegments:geometryType:inwardNormals:allocator:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLMesh CreateEllipticalCone (float height, Vector2 radii, nuint radialSegments, nuint verticalSegments, MDLGeometryType geometryType, bool inwardNormals, [NullAllowed] IMDLMeshBufferAllocator allocator);

		[Static]
		[Export ("newIcosahedronWithRadius:inwardNormals:allocator:")]
		MDLMesh CreateIcosahedron (float radius, bool inwardNormals, [NullAllowed] IMDLMeshBufferAllocator allocator);

		[Static]
		[Export ("newSubdividedMesh:submeshIndex:subdivisionLevels:")]
		MDLMesh CreateSubdividedMesh (MDLMesh mesh, nuint submeshIndex, nuint subdivisionLevels);

		[Export ("generateAmbientOcclusionTextureWithSize:raysPerSample:attenuationFactor:objectsToConsider:vertexAttributeNamed:materialPropertyNamed:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		bool GenerateAmbientOcclusionTexture (Vector2i textureSize, nint raysPerSample, float attenuationFactor, MDLObject [] objectsToConsider, string vertexAttributeName, string materialPropertyName);

		[Export ("generateAmbientOcclusionTextureWithQuality:attenuationFactor:objectsToConsider:vertexAttributeNamed:materialPropertyNamed:")]
		bool GenerateAmbientOcclusionTexture (float bakeQuality, float attenuationFactor, MDLObject [] objectsToConsider, string vertexAttributeName, string materialPropertyName);

		[Export ("generateAmbientOcclusionVertexColorsWithRaysPerSample:attenuationFactor:objectsToConsider:vertexAttributeNamed:")]
		bool GenerateAmbientOcclusionVertexColors (nint raysPerSample, float attenuationFactor, MDLObject [] objectsToConsider, string vertexAttributeName);

		[Export ("generateAmbientOcclusionVertexColorsWithQuality:attenuationFactor:objectsToConsider:vertexAttributeNamed:")]
		bool GenerateAmbientOcclusionVertexColors (float bakeQuality, float attenuationFactor, MDLObject [] objectsToConsider, string vertexAttributeName);


		[Export ("generateLightMapTextureWithTextureSize:lightsToConsider:objectsToConsider:vertexAttributeNamed:materialPropertyNamed:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		bool GenerateLightMapTexture (Vector2i textureSize, MDLLight [] lightsToConsider, MDLObject [] objectsToConsider, string vertexAttributeName, string materialPropertyName);

		[Export ("generateLightMapTextureWithQuality:lightsToConsider:objectsToConsider:vertexAttributeNamed:materialPropertyNamed:")]
		bool GenerateLightMapTexture (float bakeQuality, MDLLight [] lightsToConsider, MDLObject [] objectsToConsider, string vertexAttributeName, string materialPropertyName);

		[Export ("generateLightMapVertexColorsWithLightsToConsider:objectsToConsider:vertexAttributeNamed:")]
		bool GenerateLightMapVertexColors (MDLLight [] lightsToConsider, MDLObject [] objectsToConsider, string vertexAttributeName);

		[iOS (9,0), Mac(10,11)]
		[Static]
		[Export ("meshWithSCNGeometry:")]
		MDLMesh FromGeometry (SCNGeometry geometry);

	}

	interface IMDLMeshBuffer {}
	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[Protocol]
	interface MDLMeshBuffer : NSCopying
	{
		[Abstract]
		[Export ("fillData:offset:")]
		void FillData (NSData data, nuint offset);

		[Abstract]
		[Export ("map")]
		MDLMeshBufferMap Map { get; }

		[Export ("length")]
		nuint Length { get; }

		[Export ("allocator", ArgumentSemantic.Retain)]
		IMDLMeshBufferAllocator Allocator { get; }

		[Export ("zone", ArgumentSemantic.Retain)]
		IMDLMeshBufferZone Zone { get; }

		[Export ("type")]
		MDLMeshBufferType Type { get; }
	}

	interface IMDLMeshBufferAllocator {}
	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[Protocol]
	interface MDLMeshBufferAllocator
	{
		[Abstract]
		[Export ("newZone:")]
		IMDLMeshBufferZone CreateZone (nuint capacity);

		[Abstract]
		[Export ("newZoneForBuffersWithSize:andType:")]
		IMDLMeshBufferZone CreateZone (NSNumber[] sizes, NSNumber[] types);

		[Abstract]
		[Export ("newBuffer:type:")]
		IMDLMeshBuffer CreateBuffer (nuint length, MDLMeshBufferType type);

		[Abstract]
		[Export ("newBufferWithData:type:")]
		IMDLMeshBuffer CreateBuffer (NSData data, MDLMeshBufferType type);

		[Abstract]
		[Export ("newBufferFromZone:length:type:")]
		[return: NullAllowed]
		IMDLMeshBuffer CreateBuffer ([NullAllowed] IMDLMeshBufferZone zone, nuint length, MDLMeshBufferType type);

		[Abstract]
		[Export ("newBufferFromZone:data:type:")]
		[return: NullAllowed]
		IMDLMeshBuffer CreateBuffer ([NullAllowed] IMDLMeshBufferZone zone, NSData data, MDLMeshBufferType type);
	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLMeshBufferDataAllocator : MDLMeshBufferAllocator
	{

	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLMeshBufferZoneDefault : MDLMeshBufferZone
	{
		// We get Capacity and Allocator from MDLMeshBufferZone
		// [Export ("capacity")]
		// nuint Capacity { get; }

		// [Export ("allocator", ArgumentSemantic.Retain)]
		// IMDLMeshBufferAllocator Allocator { get; }
	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLMeshBufferData : MDLMeshBuffer, NSCopying
	{
		[Export ("initWithType:length:")]
		IntPtr Constructor (MDLMeshBufferType type, nuint length);

		[Export ("initWithType:data:")]
		IntPtr Constructor (MDLMeshBufferType type, [NullAllowed] NSData data);

		[Export ("data", ArgumentSemantic.Retain)]
		NSData Data { get; }
	}

	interface IMDLMeshBufferZone {}
	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[Protocol]
	interface MDLMeshBufferZone
	{
		[Export ("capacity")]
		nuint Capacity { get; }

		[Export ("allocator")]
		IMDLMeshBufferAllocator Allocator { get; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[Protocol]
	interface MDLNamed {
		[Abstract]
		[Export ("name")]
		string Name { get; set; }
	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLTexture))]
	[DisableDefaultCtor]
	interface MDLNoiseTexture
	{
		[Export ("initWithData:topLeftOrigin:name:dimensions:rowStride:channelCount:channelEncoding:isCube:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] NSData pixelData, bool topLeftOrigin, [NullAllowed] string name, Vector2i dimensions, nint rowStride, nuint channelCount, MDLTextureChannelEncoding channelEncoding, bool isCube);

		[Export ("initVectorNoiseWithSmoothness:name:textureDimensions:channelEncoding:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (float smoothness, [NullAllowed] string name, Vector2i textureDimensions, MDLTextureChannelEncoding channelEncoding);

		[Export ("initScalarNoiseWithSmoothness:name:textureDimensions:channelCount:channelEncoding:grayscale:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (float smoothness, [NullAllowed] string name, Vector2i textureDimensions, int channelCount, MDLTextureChannelEncoding channelEncoding, bool grayscale);
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLTexture))]
	[DisableDefaultCtor]
	interface MDLNormalMapTexture
	{
		[Export ("initWithData:topLeftOrigin:name:dimensions:rowStride:channelCount:channelEncoding:isCube:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] NSData pixelData, bool topLeftOrigin, [NullAllowed] string name, Vector2i dimensions, nint rowStride, nuint channelCount, MDLTextureChannelEncoding channelEncoding, bool isCube);

		[Export ("initByGeneratingNormalMapWithTexture:name:smoothness:contrast:")]
		IntPtr Constructor (MDLTexture sourceTexture, [NullAllowed] string name, float smoothness, float contrast);
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLObject : MDLNamed
	{
		[Export ("setComponent:forProtocol:")]
		void SetComponent (IMDLComponent component, Protocol protocol);

		[Export ("componentConformingToProtocol:")]
		[return: NullAllowed]
		IMDLComponent IsComponentConforming (Protocol protocol);

		[NullAllowed, Export ("parent", ArgumentSemantic.Weak)]
		MDLObject Parent { get; set; }

		[NullAllowed, Export ("transform", ArgumentSemantic.Retain)]
		IMDLTransformComponent Transform { get; set; }

		[Export ("children", ArgumentSemantic.Retain), NullAllowed]
		IMDLObjectContainerComponent Children { get; set; }

		[Export ("addChild:")]
		void AddChild (MDLObject child);

		[Export ("boundingBoxAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLAxisAlignedBoundingBox GetBoundingBox (double atTime);

		[iOS (9,0), Mac(10,11)]
		[Static]
		[Export ("objectWithSCNNode:")]
		MDLObject FromNode (SCNNode node);
		
	}

	[iOS (9,0), Mac (10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLObjectContainer : MDLObjectContainerComponent
	{
	}

	interface IMDLObjectContainerComponent {}
	[iOS (9,0)]
	[Protocol]
	interface MDLObjectContainerComponent : MDLComponent, INSFastEnumeration
	{
		[Abstract]
		[Export ("addObject:")]
		void AddObject (MDLObject @object);

		[Abstract]
		[Export ("removeObject:")]
		void RemoveObject (MDLObject @object);

		[Abstract]
		[Export ("objects", ArgumentSemantic.Retain)]
 		MDLObject[] Objects { get; }
 	}

	interface IMDLComponent {}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[Protocol]
	interface MDLComponent
	{
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLPhysicallyPlausibleLight))]
	interface MDLPhotometricLight
	{
		[Export ("initWithIESProfile:")]
		IntPtr Constructor (NSUrl url);

		[Export ("generateSphericalHarmonicsFromLight:")]
		void GenerateSphericalHarmonics (nuint sphericalHarmonicsLevel);

		[Export ("generateCubemapFromLight:")]
		void GenerateCubemap (nuint textureSize);

		[NullAllowed, Export ("lightCubeMap", ArgumentSemantic.Retain)]
		MDLTexture LightCubeMap { get; }

		[Export ("sphericalHarmonicsLevel")]
		nuint SphericalHarmonicsLevel { get; }

		[NullAllowed, Export ("sphericalHarmonicsCoefficients", ArgumentSemantic.Copy)]
		NSData SphericalHarmonicsCoefficients { get; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLLight))]
	interface MDLPhysicallyPlausibleLight
	{
		[Export ("setColorByTemperature:")]
		void SetColor (float temperature);

		[NullAllowed, Export ("color", ArgumentSemantic.Assign)]
		CGColor Color { get; set; }

		[Export ("lumens")]
		float Lumens { get; set; }

		[Export ("innerConeAngle")]
		float InnerConeAngle { get; set; }

		[Export ("outerConeAngle")]
		float OuterConeAngle { get; set; }

		[Export ("attenuationStartDistance")]
		float AttenuationStartDistance { get; set; }

		[Export ("attenuationEndDistance")]
		float AttenuationEndDistance { get; set; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLScatteringFunction))]
	interface MDLPhysicallyPlausibleScatteringFunction
	{
		[Export ("version")]
		nint Version { get; }

		[Export ("subsurface", ArgumentSemantic.Retain)]
		MDLMaterialProperty Subsurface { get; }

		[Export ("metallic", ArgumentSemantic.Retain)]
		MDLMaterialProperty Metallic { get; }

		[Export ("specularAmount", ArgumentSemantic.Retain)]
		MDLMaterialProperty SpecularAmount { get; }

		[Export ("specularTint", ArgumentSemantic.Retain)]
		MDLMaterialProperty SpecularTint { get; }

		[Export ("roughness", ArgumentSemantic.Retain)]
		MDLMaterialProperty Roughness { get; }

		[Export ("anisotropic", ArgumentSemantic.Retain)]
		MDLMaterialProperty Anisotropic { get; }

		[Export ("anisotropicRotation", ArgumentSemantic.Retain)]
		MDLMaterialProperty AnisotropicRotation { get; }

		[Export ("sheen", ArgumentSemantic.Retain)]
		MDLMaterialProperty Sheen { get; }

		[Export ("sheenTint", ArgumentSemantic.Retain)]
		MDLMaterialProperty SheenTint { get; }

		[Export ("clearcoat", ArgumentSemantic.Retain)]
		MDLMaterialProperty Clearcoat { get; }

		[Export ("clearcoatGloss", ArgumentSemantic.Retain)]
		MDLMaterialProperty ClearcoatGloss { get; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLScatteringFunction : MDLNamed
	{
		[Export ("baseColor", ArgumentSemantic.Retain)]
		MDLMaterialProperty BaseColor { get; }

		[Export ("emission", ArgumentSemantic.Retain)]
		MDLMaterialProperty Emission { get; }

		[Export ("specular", ArgumentSemantic.Retain)]
		MDLMaterialProperty Specular { get; }

		[Export ("materialIndexOfRefraction", ArgumentSemantic.Retain)]
		MDLMaterialProperty MaterialIndexOfRefraction { get; }

		[Export ("interfaceIndexOfRefraction", ArgumentSemantic.Retain)]
		MDLMaterialProperty InterfaceIndexOfRefraction { get; }

		[Export ("normal", ArgumentSemantic.Retain)]
		MDLMaterialProperty Normal { get; }

		[Export ("ambientOcclusion", ArgumentSemantic.Retain)]
		MDLMaterialProperty AmbientOcclusion { get; }

		[Export ("ambientOcclusionScale", ArgumentSemantic.Retain)]
		MDLMaterialProperty AmbientOcclusionScale { get; }
	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLTexture))]
	[DisableDefaultCtor]
	interface MDLSkyCubeTexture
	{
		[Export ("initWithData:topLeftOrigin:name:dimensions:rowStride:channelCount:channelEncoding:isCube:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] NSData pixelData, bool topLeftOrigin, [NullAllowed] string name, Vector2i dimensions, nint rowStride, nuint channelCount, MDLTextureChannelEncoding channelEncoding, bool isCube);

		[Export ("initWithName:channelEncoding:textureDimensions:turbidity:sunElevation:upperAtmosphereScattering:groundAlbedo:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] string name, MDLTextureChannelEncoding channelEncoding, Vector2i textureDimensions, float turbidity, float sunElevation, float upperAtmosphereScattering, float groundAlbedo);

		[Export ("updateTexture")]
		void UpdateTexture ();

		[Export ("turbidity")]
		float Turbidity { get; set; }

		[Export ("sunElevation")]
		float SunElevation { get; set; }

		[Export ("upperAtmosphereScattering")]
		float UpperAtmosphereScattering { get; set; }

		[Export ("groundAlbedo")]
		float GroundAlbedo { get; set; }

		[Export ("horizonElevation")]
		float HorizonElevation { get; set; }

		[NullAllowed]
		[Export ("groundColor", ArgumentSemantic.Assign)]
		CGColor GroundColor { get; set; }

		[Export ("gamma")]
		float Gamma { get; set; }

		[Export ("exposure")]
		float Exposure { get; set; }

		[Export ("brightness")]
		float Brightness { get; set; }

		[Export ("contrast")]
		float Contrast { get; set; }

		[Export ("saturation")]
		float Saturation { get; set; }

		[Export ("highDynamicRangeCompression", ArgumentSemantic.Assign)]
		Vector2 HighDynamicRangeCompression {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLCamera))]
	interface MDLStereoscopicCamera
	{
		[Export ("interPupillaryDistance")]
		float InterPupillaryDistance { get; set; }

		[Export ("leftVergence")]
		float LeftVergence { get; set; }

		[Export ("rightVergence")]
		float RightVergence { get; set; }

		[Export ("overlap")]
		float Overlap { get; set; }

		[Export ("leftViewMatrix")]
		Matrix4 LeftViewMatrix {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		[Export ("rightViewMatrix")]
		Matrix4 RightViewMatrix {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		[Export ("leftProjectionMatrix")]
		Matrix4 LeftProjectionMatrix {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}

		[Export ("rightProjectionMatrix")]
		Matrix4 RightProjectionMatrix {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
		}
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLSubmesh : MDLNamed
	{
		[Export ("initWithName:indexBuffer:indexCount:indexType:geometryType:material:")]
		IntPtr Constructor (string name, IMDLMeshBuffer indexBuffer, nuint indexCount, MDLIndexBitDepth indexType, MDLGeometryType geometryType, [NullAllowed] MDLMaterial material);

		[Export ("initWithIndexBuffer:indexCount:indexType:geometryType:material:")]
		IntPtr Constructor (IMDLMeshBuffer indexBuffer, nuint indexCount, MDLIndexBitDepth indexType, MDLGeometryType geometryType, [NullAllowed] MDLMaterial material);

		[Export ("initWithName:indexBuffer:indexCount:indexType:geometryType:material:topology:")]
		IntPtr Constructor (string name, IMDLMeshBuffer indexBuffer, nuint indexCount, MDLIndexBitDepth indexType, MDLGeometryType geometryType, [NullAllowed] MDLMaterial material, [NullAllowed] MDLSubmeshTopology topology);

		[Export ("initWithMDLSubmesh:indexType:geometryType:")]
		IntPtr Constructor (MDLSubmesh indexBuffer, MDLIndexBitDepth indexType, MDLGeometryType geometryType);

		[Export ("indexBuffer", ArgumentSemantic.Retain)]
		IMDLMeshBuffer IndexBuffer { get; }

		[Export ("indexCount")]
		nuint IndexCount { get; }

		[Export ("indexType")]
		MDLIndexBitDepth IndexType { get; }

		[Export ("geometryType")]
		MDLGeometryType GeometryType { get; }

		[NullAllowed, Export ("material", ArgumentSemantic.Retain)]
		MDLMaterial Material { get; set; }

		[NullAllowed, Export ("topology", ArgumentSemantic.Retain)]
		MDLSubmeshTopology Topology { get; }

		[Static]
		[Export ("submeshWithSCNGeometryElement:")]
		MDLSubmesh FromGeometryElement (SCNGeometryElement element);
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLTexture : MDLNamed
	{
		[Static]
		[Export ("textureNamed:")]
		MDLTexture FromBundle (string name);

		[Static]
		[Export ("textureNamed:bundle:")]
		MDLTexture FromBundle (string name, [NullAllowed] NSBundle bundleOrNil);

		[Static]
		[Export ("textureCubeWithImagesNamed:")]
		MDLTexture CreateTextureCube (string[] imageNames);

		[Static]
		[Export ("textureCubeWithImagesNamed:bundle:")]
		MDLTexture CreateTextureCube (string[] imageNames, [NullAllowed] NSBundle bundleOrNil);

		[Static]
		[Export ("irradianceTextureCubeWithTexture:name:dimensions:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLTexture CreateIrradianceTextureCube (MDLTexture texture, [NullAllowed] string name, Vector2i dimensions);

		[Static]
		[Export ("irradianceTextureCubeWithTexture:name:dimensions:roughness:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLTexture CreateIrradianceTextureCube (MDLTexture reflectiveTexture, [NullAllowed] string name, Vector2i dimensions, float roughness);

		[Export ("initWithData:topLeftOrigin:name:dimensions:rowStride:channelCount:channelEncoding:isCube:")]
		[DesignatedInitializer]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] NSData pixelData, bool topLeftOrigin, [NullAllowed] string name, Vector2i dimensions, nint rowStride, nuint channelCount, MDLTextureChannelEncoding channelEncoding, bool isCube);

		[Export ("writeToURL:")]
		bool WriteToUrl (NSUrl url);

		[Export ("writeToURL:type:")]
		bool WriteToUrl (NSUrl url, string type);

		[NullAllowed, Export ("imageFromTexture")]
		CGImage GetImageFromTexture ();

		[NullAllowed, Export ("texelDataWithTopLeftOrigin")]
		NSData GetTexelDataWithTopLeftOrigin ();

		[NullAllowed, Export ("texelDataWithBottomLeftOrigin")]
		NSData GetTexelDataWithBottomLeftOrigin ();

		[Export ("texelDataWithTopLeftOriginAtMipLevel:create:")]
		[return: NullAllowed]
		NSData GetTexelDataWithTopLeftOrigin (nint mipLevel, bool create);

		[Export ("texelDataWithBottomLeftOriginAtMipLevel:create:")]
		[return: NullAllowed]
		NSData GetTexelDataWithBottomLeftOrigin (nint mipLevel, bool create);

		[Export ("dimensions")]
		Vector2i Dimensions {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
		}

		[Export ("rowStride")]
		nint RowStride { get; }

		[Export ("channelCount")]
		nuint ChannelCount { get; }

		[Export ("mipLevelCount")]
		nuint MipLevelCount { get; }

		[Export ("channelEncoding")]
		MDLTextureChannelEncoding ChannelEncoding { get; }

		[Export ("isCube")]
		bool IsCube { get; set; }
	}

	[iOS (9,0)][Mac (10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLTextureFilter {
		[Export ("sWrapMode", ArgumentSemantic.Assign)]
		MDLMaterialTextureWrapMode SWrapMode { get; set; }

		[Export ("tWrapMode", ArgumentSemantic.Assign)]
		MDLMaterialTextureWrapMode TWrapMode { get; set; }

		[Export ("rWrapMode", ArgumentSemantic.Assign)]
		MDLMaterialTextureWrapMode RWrapMode { get; set; }

		[Export ("minFilter", ArgumentSemantic.Assign)]
		MDLMaterialTextureFilterMode MinFilter { get; set; }

		[Export ("magFilter", ArgumentSemantic.Assign)]
		MDLMaterialTextureFilterMode MagFilter { get; set; }

		[Export ("mipFilter", ArgumentSemantic.Assign)]
		MDLMaterialMipMapFilterMode MipFilter { get; set; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLTextureSampler
	{
		[NullAllowed, Export ("texture", ArgumentSemantic.Retain)]
		MDLTexture Texture { get; set; }

		[NullAllowed, Export ("hardwareFilter", ArgumentSemantic.Retain)]
		MDLTextureFilter HardwareFilter { get; set; }

		[NullAllowed, Export ("transform", ArgumentSemantic.Retain)]
		MDLTransform Transform { get; set; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLTransform : MDLTransformComponent
	{
		[Export ("initWithTransformComponent:")]
		IntPtr Constructor (IMDLTransformComponent component);

		[Export ("initWithMatrix:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor (Matrix4 matrix);

		[Export ("setIdentity")]
		void SetIdentity ();

		[Export ("shearAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Vector3 GetShear (double atTime);

		[Export ("scaleAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Vector3 GetScale (double atTime);

		[Export ("translationAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Vector3 GetTranslation (double atTime);

		[Export ("rotationAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Vector3 GetRotation (double atTime);

		[Export ("rotationMatrixAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Matrix4 GetRotationMatrix (double atTime);

		[Export ("setShear:forTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		void SetShear (Vector3 scale, double time);

		[Export ("setScale:forTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		void SetScale (Vector3 scale, double time);

		[Export ("setTranslation:forTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		void SetTranslation (Vector3 translation, double time);

		[Export ("setRotation:forTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		void SetRotation (Vector3 rotation, double time);

		[Export ("shear", ArgumentSemantic.Assign)]
		Vector3 Shear {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			set;
		}

		[Export ("scale", ArgumentSemantic.Assign)]
		Vector3 Scale {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			set;
		}

		[Export ("translation", ArgumentSemantic.Assign)]
		Vector3 Translation {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			set;
		}

		[Export ("rotation", ArgumentSemantic.Assign)]
		Vector3 Rotation {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
			set;
		}
	}

	interface IMDLTransformComponent {}
	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[Protocol]
	interface MDLTransformComponent : MDLComponent
	{
		[Abstract]
		[Export ("matrix", ArgumentSemantic.Assign)]
		Matrix4 Matrix {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}

		[Abstract]
		[Export ("minimumTime")]
		double MinimumTime { get; }

		[Abstract]
		[Export ("maximumTime")]
		double MaximumTime { get; }

		[Export ("setLocalTransform:forTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		void SetLocalTransform (Matrix4 transform, double time);

		[Export ("setLocalTransform:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		void SetLocalTransform (Matrix4 transform);

		[Export ("localTransformAtTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Matrix4 GetLocalTransform (double atTime);

		[Static]
		[Export ("globalTransformWithObject:atTime:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Matrix4 CreateGlobalTransform (MDLObject obj, double atTime);
	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(MDLTexture), Name = "MDLURLTexture")]
	[DisableDefaultCtor]
	interface MDLUrlTexture
	{
		[Export ("initWithData:topLeftOrigin:name:dimensions:rowStride:channelCount:channelEncoding:isCube:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		IntPtr Constructor ([NullAllowed] NSData pixelData, bool topLeftOrigin, [NullAllowed] string name, Vector2i dimensions, nint rowStride, nuint channelCount, MDLTextureChannelEncoding channelEncoding, bool isCube);

		[Export ("initWithURL:name:")]
		IntPtr Constructor (NSUrl url, [NullAllowed] string name);

		[Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; set; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLVertexAttribute : NSCopying
	{
		[Export ("initWithName:format:offset:bufferIndex:")]
		IntPtr Constructor (string name, MDLVertexFormat format, nuint offset, nuint bufferIndex);

		[Export ("name")]
		string Name { get; set; }

		[Export ("format", ArgumentSemantic.Assign)]
		MDLVertexFormat Format { get; set; }

		[Export ("offset", ArgumentSemantic.Assign)]
		nuint Offset { get; set; }

		[Export ("bufferIndex", ArgumentSemantic.Assign)]
		nuint BufferIndex { get; set; }

		[Export ("initializationValue", ArgumentSemantic.Assign)]
		Vector4 InitializationValue {
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] get;
			[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")] set;
		}
	}

	[iOS (9,0)][Mac (10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor] // apple headers: created by MDLMesh's vertexAttributeData selector
	interface MDLVertexAttributeData
	{
		[Export ("map", ArgumentSemantic.Retain), NullAllowed]
		MDLMeshBufferMap Map { get; set; }

		[Export ("dataStart", ArgumentSemantic.Assign)]
		IntPtr DataStart { get; set; }

		[Export ("stride", ArgumentSemantic.Assign)]
		nuint Stride { get; set; }

		[Export ("format", ArgumentSemantic.Assign)]
		MDLVertexFormat Format { get; set; }
	}

	[iOS (9,0)][Mac (10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLMeshBufferMap
	{
		// FIXME: provide better API.
		[Export ("initWithBytes:deallocator:")]
		IntPtr Constructor (IntPtr bytes, [NullAllowed] Action deallocator);

		[Export ("bytes")]
		IntPtr Bytes { get; }
	}

	[iOS (9,0), Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLVertexDescriptor : NSCopying
	{
		[Export ("initWithVertexDescriptor:")]
		IntPtr Constructor (MDLVertexDescriptor vertexDescriptor);

		[Export ("attributeNamed:")]
		[return: NullAllowed]
		MDLVertexAttribute AttributeNamed (string name);

		[Export ("addOrReplaceAttribute:")]
		void AddOrReplaceAttribute (MDLVertexAttribute attribute);

		[Export ("attributes", ArgumentSemantic.Retain)]
		NSMutableArray<MDLVertexAttribute> Attributes { get; set; }

		[Export ("layouts", ArgumentSemantic.Retain)]
		NSMutableArray<MDLVertexBufferLayout> Layouts { get; set; }

		[Export ("reset")]
		void Reset ();

		[Export ("setPackedStrides")]
		void SetPackedStrides ();

		[Export ("setPackedOffsets")]
		void SetPackedOffsets ();
	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	[DisableDefaultCtor]
	interface MDLVoxelArray
	{

		[Export ("initWithAsset:divisions:interiorShells:exteriorShells:patchRadius:")]
		IntPtr Constructor (MDLAsset asset, int divisions, int interiorShells, int exteriorShells, float patchRadius);

		[Export ("initWithAsset:divisions:interiorNBWidth:exteriorNBWidth:patchRadius:")]
		IntPtr Constructor (MDLAsset asset, int divisions, float interiorNBWidth, float exteriorNBWidth, float patchRadius);

		[Export ("initWithData:boundingBox:voxelExtent:")]
		IntPtr Constructor (NSData voxelData, MDLAxisAlignedBoundingBox boundingBox, float voxelExtent);
		
		[Export ("meshUsingAllocator:")]
		[return: NullAllowed]
		MDLMesh CreateMesh ([NullAllowed] IMDLMeshBufferAllocator allocator);

		[Export ("voxelExistsAtIndex:allowAnyX:allowAnyY:allowAnyZ:allowAnyShell:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		bool VoxelExists (Vector4i atIndex, bool allowAnyX, bool allowAnyY, bool allowAnyZ, bool allowAnyShell);

		[Export ("setVoxelAtIndex:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		void SetVoxel (Vector4i index);

		[Export ("setVoxelsForMesh:divisions:interiorShells:exteriorShells:patchRadius:")]
		void SetVoxels (MDLMesh mesh, int divisions, int interiorShells, int exteriorShells, float patchRadius);

		[Export ("setVoxelsForMesh:divisions:interiorNBWidth:exteriorNBWidth:patchRadius:")]
		void SetVoxels (MDLMesh mesh, int divisions, float interiorNBWidth, float exteriorNBWidth, float patchRadius);

		[Export ("voxelsWithinExtent:")]
		[return: NullAllowed]
		NSData GetVoxels (MDLVoxelIndexExtent withinExtent);

		[Export ("voxelIndices")]
		[return: NullAllowed]
		NSData GetVoxelIndices ();

		[Export ("unionWithVoxels:")]
		void UnionWith (MDLVoxelArray voxels);

		[Export ("differenceWithVoxels:")]
		void DifferenceWith (MDLVoxelArray voxels);

		[Export ("intersectWithVoxels:")]
		void IntersectWith (MDLVoxelArray voxels);

		[Export ("indexOfSpatialLocation:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Vector4i GetIndex (Vector3 spatiallocation);

		[Export ("spatialLocationOfIndex:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		Vector3 GetSpatialLocation (Vector4i index);

		[Export ("voxelBoundingBoxAtIndex:")]
		[MarshalDirective (NativePrefix = "xamarin_simd__", Library = "__Internal")]
		MDLAxisAlignedBoundingBox GetVoxelBoundingBox (Vector4i index);

		[Export ("count")]
		nuint Count { get; }

		[Export ("voxelIndexExtent")]
		MDLVoxelIndexExtent VoxelIndexExtent { get; }

		[Export ("boundingBox")]
		MDLAxisAlignedBoundingBox BoundingBox { get; }
	}

	[Static]
	[Mac(10,11, onlyOn64 : true),iOS(9,0)]
	interface MDLVertexAttributes {
		[Field ("MDLVertexAttributeAnisotropy")]
		NSString Anisotropy { get; }

		[Field ("MDLVertexAttributeBinormal")]
		NSString Binormal { get; }

		[Field ("MDLVertexAttributeBitangent")]
		NSString Bitangent { get; }

		[Field ("MDLVertexAttributeColor")]
		NSString Color { get; }

		[Field ("MDLVertexAttributeEdgeCrease")]
		NSString EdgeCrease { get; }

		[Field ("MDLVertexAttributeJointIndices")]
		NSString JointIndices { get; }

		[Field ("MDLVertexAttributeJointWeights")]
		NSString JointWeights { get; }

		[Field ("MDLVertexAttributeNormal")]
		NSString Normal { get; }

		[Field ("MDLVertexAttributeOcclusionValue")]
		NSString OcclusionValue { get; }

		[Field ("MDLVertexAttributePosition")]
		NSString Position { get; }

		[Field ("MDLVertexAttributeShadingBasisU")]
		NSString ShadingBasisU { get; }

		[Field ("MDLVertexAttributeShadingBasisV")]
		NSString ShadingBasisV { get; }

		[Field ("MDLVertexAttributeSubdivisionStencil")]
		NSString SubdivisionStencil { get; }

		[Field ("MDLVertexAttributeTangent")]
		NSString Tangent { get; }

		[Field ("MDLVertexAttributeTextureCoordinate")]
		NSString TextureCoordinate { get; }
	}

	[iOS (9,0),Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof(NSObject))]
	interface MDLVertexBufferLayout : NSCopying
	{
		[Export ("stride", ArgumentSemantic.Assign)]
		nuint Stride { get; set; }
	}

	[iOS (9,0)][Mac(10,11, onlyOn64 : true)]
	[BaseType (typeof (NSObject))]
	interface MDLSubmeshTopology {
		[NullAllowed, Export ("faceTopology", ArgumentSemantic.Retain)]
		IMDLMeshBuffer FaceTopology { get; set; }

		[Export ("faceCount", ArgumentSemantic.Assign)]
		nuint FaceCount { get; set; }

		[NullAllowed, Export ("vertexCreaseIndices", ArgumentSemantic.Retain)]
		IMDLMeshBuffer VertexCreaseIndices { get; set; }

		[NullAllowed, Export ("vertexCreases", ArgumentSemantic.Retain)]
		IMDLMeshBuffer VertexCreases { get; set; }

		[Export ("vertexCreaseCount", ArgumentSemantic.Assign)]
		nuint VertexCreaseCount { get; set; }

		[NullAllowed, Export ("edgeCreaseIndices", ArgumentSemantic.Retain)]
		IMDLMeshBuffer EdgeCreaseIndices { get; set; }

		[NullAllowed, Export ("edgeCreases", ArgumentSemantic.Retain)]
		IMDLMeshBuffer EdgeCreases { get; set; }

		[Export ("edgeCreaseCount", ArgumentSemantic.Assign)]
		nuint EdgeCreaseCount { get; set; }

		[NullAllowed, Export ("holes", ArgumentSemantic.Retain)]
		IMDLMeshBuffer Holes { get; set; }

		[Export ("holeCount", ArgumentSemantic.Assign)]
		nuint HoleCount { get; set; }
	}
}
#endif
