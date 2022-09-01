using Microsoft.AspNetCore.Mvc;
using UbikeService.Models;

namespace UbikeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbikeController : Controller
    {
        private TaipeiResource _taipei;

        //自訂建構子 注入依賴的物件
        public UbikeController(TaipeiResource _taipei)
        {
            this._taipei = _taipei;
        }

        //提供前端輸入區域 查詢該區域及時 Ubike 基座狀態
        [HttpGet]
        [Route("ubike/qry/{area}/rawdata")]
        [Produces("Application/json")]
        public List<UbikeData> ubikeQry([FromRoute(Name = "area")]string sarea)
        {
            //HttpClient 物件請求遠端服務位址
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_taipei.ubike);
            // 採用非同步提出請求 GET
            string jsonString = client.GetStringAsync("").GetAwaiter().GetResult();
            Console.WriteLine(jsonString);
            // 將 json string 反序列化
            List<UbikeData> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UbikeData>>(jsonString);            
            // 使用LINQ進行集合物件的查詢
            var result = data.Where(d => d.sarea == sarea).ToList<UbikeData>();
            return result;
        }
    }
}
