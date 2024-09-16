using AutoMapper;
using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Company.G03.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap(); // It Will Use Maping In The Two Direction
        }
    }
}
