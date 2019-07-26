using Model;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IGather:IGrainWithStringKey
    {
        Task<ResponseModel<object>> HydrologyData(RequestModel<object> data);
    }
}
