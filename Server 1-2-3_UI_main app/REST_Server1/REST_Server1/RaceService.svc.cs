using System.Linq;
using System.Threading.Tasks;
using static System.Int32;

namespace REST_Server1
{
    using Storage;
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
        public IList<Storage.Race> GetRaces()
        {
            return _dataAccessMethods.GetRaces().ToList();
        }

        [WebGet(UriTemplate = "/Race/{RaceId}")]
        public Task<Storage.Race> GetRaceByID(string RaceId)
        {
            TryParse(RaceId, out var RaceIdParsedToInt);

            return _dataAccessMethods.GetRace(RaceIdParsedToInt);
        }

        [WebInvoke(UriTemplate = "/CreateRace")]
        public async Task<Storage.Race> CreateRace(Storage.Race newRace)
        {
            return await _dataAccessMethods.AddRace(newRace);
        }

        [WebInvoke(Method = "PUT", UriTemplate = "/Race/{id}")]
        public async Task UpdateRace(string id, Storage.Race updateRace)
        {
            TryParse(id, out var RaceIdParsedToInt);
            await _dataAccessMethods.UpdateRace(RaceIdParsedToInt, updateRace);
        }

        [WebInvoke(Method = "DELETE", UriTemplate = "/Race/{deleteRaceId}")]
        public async Task<Storage.Race> DeleteRace(string deleteRaceId)
        {
            TryParse(deleteRaceId, out var deleteRaceIdParsedToInt);

            return await _dataAccessMethods.DeleteRace(deleteRaceIdParsedToInt);
        }
    }
}
