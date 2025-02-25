using AutoMapper;
using Contact.Application.Dto;
using Contact.Application.Dto.RegularUser;
using Contact.Application.Features.Groups.Commands;
using Contact.Application.Features.GroupSettings.Commands;
using Contact.Domain.AggregatesModel.GroupAggregate;
using Utilities.Framework.Pagination;

namespace Contact.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Group, CreateGroupCommand>().ReverseMap();
            CreateMap<Group, DeleteGroupCommand>().ReverseMap();
            CreateMap<Group, UpdateGroupCommand>().ReverseMap();
            CreateMap<GroupApiDto, GroupDto>().ReverseMap();
            CreateMap<GroupApiDto, UpdateGroupCommand>().ReverseMap();
            CreateMap<CreateGroupApiDto, CreateGroupCommand>().ReverseMap();


            CreateMap<GroupNumber, GroupNumberDto>().ReverseMap();

            CreateMap<GroupNumberApiDto, GroupNumberDto>().ReverseMap();
            CreateMap<PagedList<GroupNumberApiDto>, PagedList<GroupNumberDto>>().ReverseMap();

            CreateMap<GroupSettings, GroupSettingsDto>().ReverseMap();
            CreateMap<GroupSettings, CreateGroupSettingsCommand>().ReverseMap();
            CreateMap<GroupSettings, DeleteGroupSettingsCommand>().ReverseMap();
            CreateMap<GroupSettings, UpdateGroupSettingsCommand>().ReverseMap();
            CreateMap<GroupSettingsApiDto, GroupSettingsDto>().ReverseMap();
            CreateMap<GroupSettingsApiDto, UpdateGroupSettingsCommand>().ReverseMap();
            CreateMap<SetAutoRegisterCancelApiDto, SetAutoRegisterCancelCommand>().ReverseMap();
            CreateMap<SetAutoRegisterApiDto, SetAutoRegisterCommand>().ReverseMap();
        }
    }
}