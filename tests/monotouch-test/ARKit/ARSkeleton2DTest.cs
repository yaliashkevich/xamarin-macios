﻿//
// Unit tests for ARSkeleton2D
//
// Authors:
//	Vincent Dondain <vidondai@microsoft.com>
//
// Copyright 2019 Microsoft. All rights reserved.
//

#if XAMCORE_2_0 && __IOS__

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ARKit;
using Foundation;
using NUnit.Framework;
using ObjCRuntime;

using Vector2 = global::OpenTK.Vector2;

namespace MonoTouchFixtures.ARKit {

	class ARSkeleton2DPoker : ARSkeleton2D {

		GCHandle vectorArrayHandle;
		Vector2 [] vectorArray;

		public ARSkeleton2DPoker () : base (IntPtr.Zero)
		{
		}

		public override nuint JointCount {
			get {
				return 2;
			}
		}

		protected unsafe override IntPtr RawJointLandmarks
		{
			get {
				vectorArray = new Vector2 [] { new Vector2 (1, 2), new Vector2 (3, 4) };
				if (!vectorArrayHandle.IsAllocated)
					vectorArrayHandle = GCHandle.Alloc (vectorArray, GCHandleType.Pinned);
				return vectorArrayHandle.AddrOfPinnedObject ();
			}
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			if (vectorArrayHandle.IsAllocated)
				vectorArrayHandle.Free ();
		}
	}

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class ARSkeleton2DTest {

		[SetUp]
		public void Setup ()
		{
			TestRuntime.AssertXcodeVersion (11, 0);
		}

		[Test]
		public void JointLandmarksTest ()
		{
			var skeleton = new ARSkeleton2DPoker ();

			var landmarks = skeleton.JointLandmarks;
			Assert.AreEqual (new Vector2 (1, 2), landmarks [0]);
			Assert.AreEqual (new Vector2 (3, 4), landmarks [1]);
		}
	}
}

#endif // XAMCORE_2_0 && __IOS__