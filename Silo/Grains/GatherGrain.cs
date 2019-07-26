using GrainInterfaces;
using Model;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class GatherGrain : Grain, IGather
    {
        public Task<ResponseModel<object>> HydrologyData(RequestModel<object> data)
        {
            ResponseModel<object> model = new ResponseModel<object>();
            model.Id = Guid.NewGuid();
            model.Items = new List<object> {
                new PassportModel  {
                Email ="iglong.com" ,
                Id =Guid.NewGuid(),
                Passport="789",
                }
            };
            model.IsSuccess = true;
            return Task.FromResult(model);
        }
    }
}
