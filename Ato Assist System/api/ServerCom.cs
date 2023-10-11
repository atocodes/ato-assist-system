using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Controls;

class ServerCom
{

    public static async Task<bool> RegisterSystem (Action callback)
    {
        HttpContent systemData = new StringContent(SystemSpec.JsonData(),Encoding.UTF8,"application/json");
        HttpResponseMessage response = await AppConsts.client.PostAsync(
            AppConsts.uri + "system/registerSystem", 
            systemData
        );


        if (response.IsSuccessStatusCode)
        {
            string res = AppConsts.ServerResponse(response.Content.ReadAsStringAsync().Result);
            SystemSpec.SaveSystemData(res);
            callback();
            return true;
        }
        else
        {
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            callback();
            return false;
        }

    }


}
