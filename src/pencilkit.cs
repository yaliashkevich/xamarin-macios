//
// PencilKit C# bindings
//
// Authors:
//	TJ Lambert  <t-anlamb@microsoft.com>
//
// Copyright 2019 Microsoft Corporation All rights reserved.
//

#if MONOMAC
using AppKit;
using UIColor = AppKit.NSColor;
using UIImage = AppKit.NSImage;

using UIScrollViewDelegate = Foundation.NSObjectProtocol;
using UIScrollView = Foundation.NSObject;
using UIGestureRecognizer = Foundation.NSObject;
using UIResponder = Foundation.NSObject;
using UIView = Foundation.NSObject;
using UIWindow = Foundation.NSObject;
using UIUserInterfaceStyle = Foundation.NSObject;
#else
using UIKit;
#endif

using System;
using ObjCRuntime;
using Foundation;
using CoreGraphics;

namespace PencilKit {

	[iOS (13, 0), NoMac]
	[Native]
	enum PKEraserType : long {
		Vector,
		Bitmap,
	}

	[iOS (13, 0), NoMac]
	enum PKInkType {
		[Field ("PKInkTypePen")]
		Pen,

		[Field ("PKInkTypePencil")]
		Pencil,

		[Field ("PKInkTypeMarker")]
		Marker,
	}

	[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
	[iOS (13, 0), NoMac]
	[BaseType (typeof (NSObject))]
	[Model (AutoGeneratedName = true)] [Protocol]
	interface PKCanvasViewDelegate : UIScrollViewDelegate {

		[Export ("canvasViewDrawingDidChange:")]
		void DrawingDidChange (PKCanvasView canvasView);

		[Export ("canvasViewDidFinishRendering:")]
		void DidFinishRendering (PKCanvasView canvasView);

		[Export ("canvasViewDidBeginUsingTool:")]
		void DidBeginUsingTool (PKCanvasView canvasView);

		[Export ("canvasViewDidEndUsingTool:")]
		void EndUsingTool (PKCanvasView canvasView);
	}

	interface IPKCanvasViewDelegate {}

	[iOS (13, 0), NoMac]
	[BaseType (typeof (UIScrollView))]
	interface PKCanvasView : PKToolPickerObserver {

		// This exists in the base class
		// [Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		// NSObject WeakDelegate { get; set; }

		[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
		[Wrap ("WeakDelegate"), NullAllowed, New]
		IPKCanvasViewDelegate Delegate { get; set; }

		[Export ("drawing", ArgumentSemantic.Copy)]
		PKDrawing Drawing { get; set; }

		[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
		[Export ("tool", ArgumentSemantic.Copy)]
		PKTool Tool { get; set; }

		[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
		[Export ("rulerActive")]
		bool RulerActive { [Bind ("isRulerActive")] get; set; }

		[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
		[Export ("drawingGestureRecognizer")]
		UIGestureRecognizer DrawingGestureRecognizer { get; }

		[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
		[Export ("allowsFingerDrawing")]
		bool AllowsFingerDrawing { get; set; }
	}

	[iOS (13, 0), Mac (10, 15)]
	[BaseType (typeof (NSObject))]
	[DesignatedDefaultCtor]
	interface PKDrawing : NSCopying, NSSecureCoding {

		[Field ("PKAppleDrawingTypeIdentifier")]
		NSString AppleDrawingTypeIdentifier { get; }

		[DesignatedInitializer]
		[Export ("initWithData:error:")]
		IntPtr Constructor (NSData data, [NullAllowed] out NSError error);

		[Export ("dataRepresentation")]
		NSData DataRepresentation { get; }

		[Export ("bounds")]
		CGRect Bounds { get; }

		[Export ("imageFromRect:scale:")]
		UIImage GetImage (CGRect rect, nfloat scale);

		[Export ("drawingByApplyingTransform:")]
		PKDrawing GetDrawing (CGAffineTransform transform);

		[Export ("drawingByAppendingDrawing:")]
		PKDrawing GetDrawing (PKDrawing drawing);
	}

	[iOS (13, 0), NoMac]
	[BaseType (typeof (PKTool))]
	[DisableDefaultCtor]
	interface PKEraserTool {

		[Export ("eraserType")]
		PKEraserType EraserType { get; }

		[DesignatedInitializer]
		[Export ("initWithEraserType:")]
		IntPtr Constructor (PKEraserType eraserType);
	}

	[iOS (13, 0), NoMac]
	[BaseType (typeof (PKTool))]
	[DisableDefaultCtor]
	interface PKInkingTool {

		[DesignatedInitializer]
		[Export ("initWithInkType:color:width:")]
		IntPtr Constructor ([BindAs (typeof (PKInkType))] NSString type, UIColor color, nfloat width);

		[Export ("initWithInkType:color:")]
		IntPtr Constructor ([BindAs (typeof (PKInkType))] NSString type, UIColor color);

		[Static]
		[Export ("defaultWidthForInkType:")]
		nfloat GetDefaultWidth ([BindAs (typeof (PKInkType))] NSString inkType);

		[Static]
		[Export ("minimumWidthForInkType:")]
		nfloat GetMinimumWidth ([BindAs (typeof (PKInkType))] NSString inkType);

		[Static]
		[Export ("maximumWidthForInkType:")]
		nfloat GetMaximumWidth ([BindAs (typeof (PKInkType))] NSString inkType);

		[Export ("inkType")]
		[BindAs (typeof (PKInkType))]
		NSString InkType { get; }

		[Static]
		[Export ("convertColor:fromUserInterfaceStyle:to:")]
		UIColor ConvertColor (UIColor color, UIUserInterfaceStyle fromUserInterfaceStyle, UIUserInterfaceStyle toUserInterfaceStyle);

		[Export ("color")]
		UIColor Color { get; }

		[Export ("width")]
		nfloat Width { get; }
	}

	[iOS (13, 0), NoMac]
	[BaseType (typeof (PKTool))]
	[DesignatedDefaultCtor]
	interface PKLassoTool {}

	[iOS (13, 0), NoMac]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PKTool : NSCopying {}

	interface IPKToolPickerObserver {}

	[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
	[iOS (13, 0), NoMac]
	[Protocol]
	interface PKToolPickerObserver {

		[Export ("toolPickerSelectedToolDidChange:")]
		void SelectedToolDidChange (PKToolPicker toolPicker);

		[Export ("toolPickerIsRulerActiveDidChange:")]
		void IsRulerActiveDidChange (PKToolPicker toolPicker);

		[Export ("toolPickerVisibilityDidChange:")]
		void VisibilityDidChange (PKToolPicker toolPicker);

		[Export ("toolPickerFramesObscuredDidChange:")]
		void FramesObscuredDidChange (PKToolPicker toolPicker);
	}


	[Unavailable (PlatformName.UIKitForMac), Advice ("This API is not available when using UIKit on macOS.")]
	[iOS (13, 0), NoMac]
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PKToolPicker {

		[Export ("addObserver:")]
		void AddObserver (IPKToolPickerObserver observer);

		[Export ("removeObserver:")]
		void RemoveObserver (IPKToolPickerObserver observer);

		[Export ("setVisible:forFirstResponder:")]
		void SetVisible (bool visible, UIResponder responder);

		[Export ("selectedTool", ArgumentSemantic.Strong)]
		PKTool SelectedTool { get; set; }

		[Export ("rulerActive")]
		bool RulerActive { [Bind ("isRulerActive")] get; set; }

		[Export ("isVisible")]
		bool IsVisible { get; }

		[Export ("frameObscuredInView:")]
		CGRect GetFrameObscured (UIView view);

		[Export ("overrideUserInterfaceStyle", ArgumentSemantic.Assign)]
		UIUserInterfaceStyle OverrideUserInterfaceStyle { get; set; }

		[Export ("colorUserInterfaceStyle", ArgumentSemantic.Assign)]
		UIUserInterfaceStyle ColorUserInterfaceStyle { get; set; }

		[Static]
		[return: NullAllowed]
		[Export ("sharedToolPickerForWindow:")]
		PKToolPicker GetSharedToolPicker (UIWindow window);
	}
}
