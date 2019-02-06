using System.Linq;
using System.Threading.Tasks;
using Storage;
using static System.Int32;

namespace REST_Server2
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements
        (RequirementsMode = 
        AspNetCompatibilityRequirementsMode.Allowed)]
    public class RaceService
    {         
        private DataAccessMethods _dataAccessMethods;

        public RaceService()
        {
            _dataAccessMethods = new DataAccessMethods();
        }
     
        [WebGet(UriTemplate = "/GetRaces")]
        public IList<Race> GetRaces()
        {
            return _dataAccessMethods.GetRaces().ToList();
        }


        [WebInvoke(Method = "POST", UriTemplate = "/CreateRace")]
        public async Task<Race> CreateRace(Race newRace)
        {
            return await _dataAccessMethods.AddRace(newRace);
        }

        //[WebInvoke(Method = "PUT", UriTemplate = "/Race/{id}")]
        //public async Task UpdateRace(string RaceId, Race updateRace)
        //{
        //    TryParse(RaceId, out var RaceIdParsedToInt);
        //    await _dataAccessMethods.UpdateRace(RaceIdParsedToInt, updateRace);
        //}

        [WebInvoke(Method = "DELETE", UriTemplate = "/DeleteRace")]
        public async Task<Storage.Race> DeleteRace(string deleteRaceId)
        {
            TryParse(deleteRaceId, out var deleteRaceIdParsedToInt);

            return await _dataAccessMethods.DeleteRace(deleteRaceIdParsedToInt);
        }
    }
}
