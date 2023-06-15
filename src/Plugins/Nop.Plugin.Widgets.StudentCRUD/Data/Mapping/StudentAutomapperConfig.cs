using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.StudentCRUD.Domain;
using Nop.Plugin.Widgets.StudentCRUD.Models;

namespace Nop.Plugin.Widgets.StudentCRUD.Data.Mapping
{
    public class StudentAutomapperConfig : Profile, IOrderedMapperProfile
    {
        public int Order => 10;

        public StudentAutomapperConfig()
        {
            CreateMap<Student, StudentModel>();
            CreateMap<StudentModel, Student>();
        }
    }
}
