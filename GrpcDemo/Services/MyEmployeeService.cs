using Grpc.Core;
using GrpcDemo.Data.InMermoy;
using GrpcDemo.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.Services
{
    public class MyEmployeeService : EmployeeService.EmployeeServiceBase
    {
        private readonly ILogger<MyEmployeeService> _logger;
        public MyEmployeeService(ILogger<MyEmployeeService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Unary Rpc
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<EmployeeResonse> GetByNo(GetByNoRequest request, ServerCallContext context)
        {
            Console.WriteLine("\r\nGrpcServer即将为你演示 一元Unary Rpc");

            MetadataProcess(context);

            var data = InmemoryData.Employees.FirstOrDefault(m => m.No == request.No);
            if (data != null)
            {
                return await Task.FromResult(new EmployeeResonse()
                {
                    Employee = data
                });
            }
            throw new Exception("异常");
        }

        private void MetadataProcess(ServerCallContext context)
        {
            var metaData = context.RequestHeaders;
            foreach (var item in metaData)
            {
                _logger.LogInformation($"key:{item.Key},value:{item.Value}");
            }
        }

        /// <summary>
        /// 服务流Server streaming Rpcs
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GetAll(GetAllReqeust request, IServerStreamWriter<EmployeeResonse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("\r\nGrpcServer即将为你演示 服务流Server streaming Rpcs");

            MetadataProcess(context);
            foreach (var employee in InmemoryData.Employees)
            {
                Console.WriteLine($"responseStream.Write:{employee}");
                await responseStream.WriteAsync(new EmployeeResonse()
                {
                    Employee = employee
                });
            }
        }
        /// <summary>
        /// 客户端流Client streaming RPCs 
        /// </summary>
        /// <param name="requestStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<AddPhotoResponse> AddPhoto(IAsyncStreamReader<AddPhotoRequest> requestStream, ServerCallContext context)
        {
            Console.WriteLine("\r\nGrpcServer即将为你演示 客户端流Client streaming RPCs ");
            MetadataProcess(context);

            var data = new List<byte>();
            while (await requestStream.MoveNext())
            {
                Console.WriteLine($"Received:{requestStream.Current.Data.Length}");
                data.AddRange(requestStream.Current.Data);
            }

            Console.WriteLine($"Received file with{data.Count} bytes");

            return new AddPhotoResponse { IsOk = true };
        }

        /// <summary>
        /// 双向流Bidirectional streaming RPCs
        /// </summary>
        /// <param name="requestStream"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task SaveAll(IAsyncStreamReader<EmployeeRequest> requestStream, IServerStreamWriter<EmployeeResonse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("\r\nGrpcServer即将为你演示 双向流Bidirectional streaming RPCs");

            while (await requestStream.MoveNext()) {

                var employee = requestStream.Current.Employee;
                Console.WriteLine($"requestStream.Current:{employee}");
                lock (this)
                {
                    InmemoryData.Employees.Add(employee);
                }
                Console.WriteLine($"responseStream.Write:{employee}");
                await responseStream.WriteAsync(new EmployeeResonse()
                {
                    Employee = employee
                });
            }
        }
    }
}
