// Decompiled with JetBrains decompiler
// Type: GPII.GPIIProximityListener
// Assembly: GPIIWindows8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE1BFC0-0D05-42C0-BFC7-1DFF997E6E01
// Assembly location: S:\gpii\other\gpii-proximity-listener\orig\GPIIWindowsProximityListener.exe.exe

using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows.Forms;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;

namespace GPII
{
  internal class GPIIProximityListener
  {
    private DateTime lastTapTime = DateTime.UtcNow;
    public Process localGPIIProcess = (Process) null;
    private ProximityDevice proximityDevice;

    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        Application.Run((ApplicationContext) new GPIIApplicationContext());
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Program Terminated Unexpectedly", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    public void InitializeProximityDevice()
    {
      this.proximityDevice = ProximityDevice.GetDefault();
      if (this.proximityDevice != null)
      {
        ProximityDevice proximityDevice1 = this.proximityDevice;
        // ISSUE: method pointer
        WindowsRuntimeMarshal.AddEventHandler<DeviceArrivedEventHandler>(new Func<DeviceArrivedEventHandler, EventRegistrationToken>(proximityDevice1.add_DeviceArrived), new Action<EventRegistrationToken>(proximityDevice1.remove_DeviceArrived), new DeviceArrivedEventHandler((object) this, __methodptr(ProximityDeviceArrived)));
        ProximityDevice proximityDevice2 = this.proximityDevice;
        // ISSUE: method pointer
        WindowsRuntimeMarshal.AddEventHandler<DeviceDepartedEventHandler>(new Func<DeviceDepartedEventHandler, EventRegistrationToken>(proximityDevice2.add_DeviceDeparted), new Action<EventRegistrationToken>(proximityDevice2.remove_DeviceDeparted), new DeviceDepartedEventHandler((object) this, __methodptr(ProximityDeviceDeparted)));
        // ISSUE: method pointer
        this.proximityDevice.SubscribeForMessage("NDEF", new MessageReceivedHandler((object) this, __methodptr(messageReceivedHandler)));
        this.WriteMessageText("Proximity device initialized.\n");
      }
      else
        this.WriteMessageText("Failed to initialized proximity device.\n");
    }

    public void gpiiLogonChange(string userToken)
    {
      string str = "http://localhost:8081/user/" + userToken + "/logonChange";
      this.WriteMessageText(str);
      using (WebClient webClient = new WebClient())
        webClient.DownloadString(str);
    }

    public string readUserTokenFromTag(ProximityMessage message)
    {
      DataReader dataReader = DataReader.FromBuffer(message.get_Data());
      byte[] bytes = new byte[(IntPtr) message.get_Data().get_Length()];
      dataReader.ReadBytes(bytes);
      string str = Encoding.ASCII.GetString(bytes, 0, (int) message.get_Data().get_Length()).Substring(7);
      Console.WriteLine("The read tag is: " + str);
      return str;
    }

    public void messageReceivedHandler(ProximityDevice device, ProximityMessage message)
    {
      DateTime utcNow = DateTime.UtcNow;
      TimeSpan timeSpan = utcNow - this.lastTapTime;
      this.WriteMessageText("Time since last tap (sec): " + (object) timeSpan.TotalSeconds);
      if (timeSpan.TotalSeconds < 3.0)
        return;
      this.lastTapTime = utcNow;
      this.gpiiLogonChange(this.readUserTokenFromTag(message));
    }

    public void ProximityDeviceArrived(ProximityDevice device)
    {
      this.WriteMessageText("Proximate device arrived. id = " + device.get_DeviceId() + "\n");
    }

    public void ProximityDeviceDeparted(ProximityDevice device)
    {
      this.WriteMessageText("Proximate device departed. id = " + device.get_DeviceId() + "\n");
    }

    public void WriteMessageText(string message)
    {
      Console.WriteLine(message);
    }
  }
}
