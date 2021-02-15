using GrpcDemo.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.Data.InMermoy
{
    public class InmemoryData
    {
        public static readonly List<Employee> Employees = new List<Employee>() {
        new Employee(){  Id=1, FirstName="FN1", LastName="LN1", No=1, Salary=1},
        new Employee(){  Id=2, FirstName="FN1", LastName="LN1", No=2, Salary=1},
        new Employee(){  Id=3, FirstName="FN1", LastName="LN1", No=3, Salary=1},
        new Employee(){  Id=4, FirstName="FN1", LastName="LN1", No=4, Salary=1},
        };
    }
}
