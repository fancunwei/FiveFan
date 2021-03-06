﻿
using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FiveFan.Infrastructure;
using FiveFan.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiveFan
{
    public class Program
    {
        public static List<UserModel> userList = new List<UserModel>();
        public static void Main(string[] args)
        {
            Console.ReadLine();
        }

        static void BenchmarkRun() {
            BenchmarkRunner.Run<TestContext>();

        }
    }
    public class TestContext
    {
        private readonly List<UserModel> userList = new List<UserModel>();
        private readonly IMapper _mapper;
        public TestContext() {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserModel, UserVm>();
            });
            _mapper = configuration.CreateMapper();
            for (var i = 0; i <= 1000000; i++)
            {
                userList.Add(new UserModel()
                {
                    Id = $"Id{i}",
                    Company = $"Company{i}",
                    Department = $"Department{i}",
                    Email = $"Email{i}",
                    Group = $"Group{i}",
                    Mobile = $"Mobile{i}",
                    Name = $"Name{i}"
                });
            }
        }

        [Benchmark]
        public List<UserVm> TestGetUserVmsBySelect()
        {
            return userList.Select(user => new UserVm()
            {
                Id = user.Id,
                Company = user.Company,
                Department = user.Department,
                Email = user.Email,
                Group = user.Group,
                Mobile = user.Mobile,
                Name = user.Name
            }).ToList();
            
        }
        [Benchmark]
        public List<UserVm> TestGetUserVms()
        {
            var userVms = new List<UserVm>();
            foreach (var user in userList)
            {
                userVms.Add(new UserVm()
                {
                    Id = user.Id,
                    Company = user.Company,
                    Department = user.Department,
                    Email = user.Email,
                    Group = user.Group,
                    Mobile = user.Mobile,
                    Name = user.Name
                });
            }

            return userVms;
        }
        [Benchmark]
        public List<UserVm> GetUserVmsByJson()
        {
            var jsons = JsonConvert.SerializeObject(userList);
            var userVms = JsonConvert.DeserializeObject<List<UserVm>>(jsons);
            return userVms;
        }
        [Benchmark]
        public List<UserVm> GetUserVmsByAutoMapper()
        {
          
            return _mapper.Map<List<UserVm>>(userList);
        }
    }

}
