// unset

using AutoMapper;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Domain.Entities;

namespace Localizer.Api.Infrastructure.Profiles
{
	public class AccountProfile : Profile
	{
		public AccountProfile()
		{
			CreateMap<SignUpRequest, Account>(MemberList.Source);
		}
	}
}