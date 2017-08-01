// Decompiled with JetBrains decompiler
// Type: GPII.Properties.Resources
// Assembly: GPIIWindows8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE1BFC0-0D05-42C0-BFC7-1DFF997E6E01
// Assembly location: S:\gpii\other\gpii-proximity-listener\orig\GPIIWindowsProximityListener.exe.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace GPII.Properties
{
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) GPII.Properties.Resources.resourceMan, (object) null))
          GPII.Properties.Resources.resourceMan = new ResourceManager("GPII.Properties.Resources", typeof (GPII.Properties.Resources).Assembly);
        return GPII.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return GPII.Properties.Resources.resourceCulture;
      }
      set
      {
        GPII.Properties.Resources.resourceCulture = value;
      }
    }

    internal Resources()
    {
    }
  }
}
