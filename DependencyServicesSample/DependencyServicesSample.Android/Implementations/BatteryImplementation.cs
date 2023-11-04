using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DependencyServicesSample.Droid;
using DependencyServicesSample.Droid.Implementations;
using DependencyServicesSample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(BatteryImplementation))]
namespace DependencyServicesSample.Droid.Implementations
{
    internal class BatteryImplementation : IBattery
    {
        public BatteryImplementation()
        {
        }

        public int RemainingChargePercent
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            var level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                            var scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);

                            return (int)Math.Floor(level * 100D / scale);
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }
        
        public DependencyServicesSample.Interfaces.BatteryStatus Status
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status == (int)Interfaces.BatteryStatus.Charging || status == (int)Interfaces.BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;
                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                            isCharging = (usbCharge || acCharge || wirelessCharge);
                            if (isCharging)
                                return DependencyServicesSample.Interfaces.BatteryStatus.Charging;

                            switch (status)
                            {
                                case (int)Interfaces.BatteryStatus.Charging:
                                    return DependencyServicesSample.Interfaces.BatteryStatus.Charging;
                                case (int)Interfaces.BatteryStatus.Discharging:
                                    return DependencyServicesSample.Interfaces.BatteryStatus.Discharging;
                                case (int)Interfaces.BatteryStatus.Full:
                                    return DependencyServicesSample.Interfaces.BatteryStatus.Full;
                                case (int)Interfaces.BatteryStatus.NotCharging:
                                    return DependencyServicesSample.Interfaces.BatteryStatus.NotCharging;
                                default:
                                    return DependencyServicesSample.Interfaces.BatteryStatus.Unknown;
                            }
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status == (int)Interfaces.BatteryStatus.Charging || status == (int)Interfaces.BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;

                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                            isCharging = (usbCharge || acCharge || wirelessCharge);

                            if (!isCharging)
                                return DependencyServicesSample.Interfaces.PowerSource.Battery;
                            else if (usbCharge)
                                return DependencyServicesSample.Interfaces.PowerSource.Usb;
                            else if (acCharge)
                                return DependencyServicesSample.Interfaces.PowerSource.Ac;
                            else if (wirelessCharge)
                                return DependencyServicesSample.Interfaces.PowerSource.Wireless;
                            else
                                return DependencyServicesSample.Interfaces.PowerSource.Other;
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }
    }
}
