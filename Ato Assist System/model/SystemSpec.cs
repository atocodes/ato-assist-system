using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Management;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using QRCoder;

class SystemSpec
{
    private Dictionary<int, string> ramTypes
    {
        get
        {
            return new Dictionary<int, string>() {
                {0x0,"Unknown"},
                {0x1,"Other"},
                {0x2,"DRAM"},
                {0x3,"Synchronous DRAM"},
                {0x4,"Cache DRAM"},
                {0x5 ,"EDO"},
                {0x6 ,"EDRAM"},
                {0x7,"VRAM"},
                {0x8,"SRAM"},
                {0x9,"RAM"},
                {0xa,"ROM"},
                {0xb,"Flash"},
                {0xc,"EEPROM"},
                {0xd,"FEPROM"},
                {0xe,"EPROM"},
                {0xf,"CDRAM"},
                {0x10,"3DRAM"},
                {0x11,"SDRAM"},
                {0x12,"SGRAM"},
                {0x13,"RDRAM"},
                {0x14,"DDR"},
                {0x15,"DDR2"},
                {0x16,"DDR2 FB-DIMM"},
                {0x17,"Undefined 23"},
                {0x18,"DDR3"},
                {0x19,"FBD2"},
                {0x1a,"DDR4"},
            };
        }
    }

    public static string id
    {
        get
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string,string>>(File.ReadAllText(AppConsts.jsonFileName))["id"];
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
    }

    public string username = Environment.UserName;
    public string userdomainame = Environment.UserDomainName;
    public string os = RuntimeInformation.OSDescription;

    public string cpu
    {
        get
        {
            ManagementObjectSearcher search = new ManagementObjectSearcher("Select * From Win32_Processor");
            string cpuName = "";

            foreach (ManagementObject Mobject in search.Get())
            {
                    cpuName = Mobject["Name"].ToString();
                    break;
            }

            return cpuName;

        }
    }

    public string memory
    {
        get
        {
            string memoryInfo = "";
            foreach (ManagementObject Mobject in new ManagementObjectSearcher("Select * From Win32_ComputerSystem").Get())
            {
                // Ram Size in GB
                double Ram_Gbytes = (Convert.ToDouble(Mobject["TotalPhysicalMemory"])) / 1073741824;
                memoryInfo += $"{Math.Round(Ram_Gbytes)}GB ";
                break;
            }
            foreach(ManagementObject Mobject in new ManagementObjectSearcher("Select * From Win32_PhysicalMemory").Get())
            {
                // Ram Type And Speed
                memoryInfo += $" {ramTypes[Convert.ToInt32(Mobject["MemoryType"])]} {Convert.ToInt32(Mobject["Speed"])} Mhz";
                break;
            }

            return memoryInfo;
        }
    }

    public string uuid
    {
        get
        {
            string systemId = "";
            foreach (ManagementObject Mobject in new ManagementObjectSearcher("Select * From Win32_ComputerSystemProduct").Get())
            {
                return systemId = Mobject["UUID"].ToString();
            }
            return systemId;
        }
    }

    public Dictionary<string,string>[] storage
    {
        get
        {
            IEnumerable<DriveInfo> drives = DriveInfo.GetDrives().AsQueryable().Where((drive) => drive.DriveType == DriveType.Fixed);

            List<Dictionary<string, string>> listOfDrives = new List<Dictionary<string, string>>() { };


            foreach(DriveInfo drive in drives)
            {
                Dictionary<string, string> driveData = new Dictionary<string, string>()
                {
                    {"name",drive.Name },
                    {"totalSize", Convert.ToString(drive.TotalSize / 1073741824) },
                    {"freeSpace", Convert.ToString(drive.AvailableFreeSpace / 1073741824) }
                };

                listOfDrives.Add(driveData);
            }

            return listOfDrives.ToArray();
        }
    }

    public static void SaveSystemData(String id)
    {
        File.WriteAllText(AppConsts.jsonFileName, JsonConvert.SerializeObject(new Dictionary<string, string>() { { "id", id } }));
        // Qr Code Image Creating
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(id, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        Bitmap qrCodeImage = qrCode.GetGraphic(20);
        qrCodeImage.Save(AppConsts.qrImgFileName, System.Drawing.Imaging.ImageFormat.Png);
    }

    public static string JsonData() => JsonConvert.SerializeObject(new SystemSpec());

    
}
