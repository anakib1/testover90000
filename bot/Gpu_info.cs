using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Collections;
using OpenHardwareMonitor;
namespace bot
{
    class Gpu_info
    {
        public string[] GetComponent(string hwclass,string syntax)
        {
            string[] res = new string[1000];
            int i = 0;
            ManagementObjectSearcher m = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);
            foreach(ManagementObject mtmp in m.Get())
            {
                res[i] = mtmp[syntax].ToString();
            }
            return res;
        }
        public string GetComponentGPU()
        {
            Computer thisComputer;
            thisComputer = new Computer() { GPUEnabled = true };
            thisComputer.Open();
            String temp = "";

            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuAti)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {

                            temp += String.Format("{0} Temperature = {1}\r\n", sensor.Name, sensor.Value.HasValue ? sensor.Value.Value.ToString() : "no value");

                        }
                    }
                }
            }
            return temp;
        }
    }
}
