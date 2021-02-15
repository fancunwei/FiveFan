using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcDemo.Protos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static GrpcDemo.Protos.EmployeeService;

namespace GrpcDemo.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new EmployeeService.EmployeeServiceClient(channel);
            var md = new Metadata {
                { "username","zhangsan"},
                { "role","administrator"},
                { "Authorization", $"Bearer xxxxxxxxxxxxxxxxxx" }
            };

            Console.WriteLine("\r\nGrpcClient即将为你演示 一元Unary Rpc");
                        await GetByNoAsync(client, md);

            Console.WriteLine("\r\nGrpcClient即将为你演示 服务流Server streaming Rpcs");
            await GetAll(client, md);

            Console.WriteLine("\r\nGrpcClient即将为你演示 客户端流Client streaming RPCs ");
            await AddPhoto(client,md);

            Console.WriteLine("\r\nGrpcClient即将为你演示 双向流Bidirectional streaming RPCs");
            await SaveAll(client, md);
            Console.WriteLine("Press Any key Exit!");
            Console.Read();

        }

        /// <summary>
        /// Unary RPC一元RPC
        /// </summary>
        static async Task GetByNoAsync(EmployeeServiceClient client, Metadata md)
        {

            //一元
            var response = await client.GetByNoAsync(new GetByNoRequest()
            {
                No = 1
            }, md);

            Console.WriteLine($"Reponse:{response}");
        }

        /// <summary>
        /// server-stream
        /// </summary>
        /// <param name="client"></param>
        /// <param name="md"></param>
        /// <returns></returns>
        static async Task GetAll(EmployeeServiceClient client, Metadata md)
        {
            using var call = client.GetAll(new GetAllReqeust() { });
            var responseStream = call.ResponseStream;
            while (await responseStream.MoveNext())
            {
                Console.WriteLine(responseStream.Current.Employee);
            }

        }

        /// <summary>
        /// client-stream
        /// </summary>
        /// <param name="client"></param>
        /// <param name="md"></param>
        /// <returns></returns>
        static async Task AddPhoto(EmployeeServiceClient client, Metadata md)
        {
            FileStream fs = File.OpenRead("Q1.png");
            using var call = client.AddPhoto(md);
            var stram = call.RequestStream;

            while (true)
            {
                byte[] buffer = new byte[1024];
                int numRead = await fs.ReadAsync(buffer, 0, buffer.Length);
                if (numRead == 0)
                {
                    break;
                }
                if (numRead < buffer.Length)
                {
                    Array.Resize(ref buffer, numRead);
                }
                await stram.WriteAsync(new AddPhotoRequest()
                {
                    Data = ByteString.CopyFrom(buffer)
                });
            }

            await stram.CompleteAsync();
            var res = await call.ResponseAsync;


            Console.WriteLine(res.IsOk);
        }

        /// <summary>
        /// 双向流
        /// </summary>
        /// <param name="client"></param>
        /// <param name="md"></param>
        /// <returns></returns>
        static async Task SaveAll(EmployeeServiceClient client, Metadata md)
        {
            var employees = new List<Employee>() {
             new Employee(){ Id=10, FirstName="F10", LastName="L10", No=10, Salary=10 },
             new Employee(){ Id=11, FirstName="F11", LastName="L11", No=11, Salary=11 },
             new Employee(){ Id=12, FirstName="F12", LastName="L12", No=12, Salary=12 },
            };

            using var call = client.SaveAll(md);
            var requestStream = call.RequestStream;
            var responseStream = call.ResponseStream;

            var responseTask = Task.Run(async () =>
            {
                while (await responseStream.MoveNext())
                {
                    Console.WriteLine($"response:{responseStream.Current.Employee}");
                }
            });

            foreach (var employee in employees) {
                await requestStream.WriteAsync(new EmployeeRequest()
                {
                    Employee = employee
                });
            }
            await requestStream.CompleteAsync();
            await responseTask;
        }
    }
}
