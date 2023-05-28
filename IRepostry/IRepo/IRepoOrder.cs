using IRepostry.Model_Dto;
using IRepostry.Model_Respons;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.IRepo
{
    public interface IRepoOrder
    {
        Task<OperationResult<OrderResponceInfo>> CreatOrder(OrderdtoModel orderDto);
        Task<OperationResult<OrderResponceInfo>> UpdateOrder(OrderdtoUpdate orderDto);
        Task<OperationResult<bool>> DeletOrder(int id);
        Task<OperationResult<dynamic>> GetOrderById(int Id);
        Task<OperationResult<dynamic>> GetAllOreders();

    }
}
