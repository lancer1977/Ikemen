//using EmbedIO;
//using EmbedIO.Routing;
//using EmbedIO.WebApi;
//using MilkDropHelper;
//using MilkDropHelper.Interfaces; 

//namespace Spotabot.Companion.Services;

//public class MilkdropController : WebApiController
//{
//    private readonly IMilkDropService _service = new MilkdropResidentService();

//    [Route(HttpVerbs.Get, "/RequestAction/{action}")]
//    public async Task RequestAction(MilkDropActions action)
//    {
//        await _service.RequestAction(action);
//    }

//}