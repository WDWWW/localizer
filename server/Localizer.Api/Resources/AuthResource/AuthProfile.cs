// unset

using AutoMapper;
using Localizer.Api.Resources.AuthResource.Models;
using Localizer.Domain.Entities;

namespace Localizer.Api.Resources.AuthResource
{
	public class AuthProfile : Profile
	{
		public AuthProfile()
		{
			CreateMap<SignUpRequest, Account>(MemberList.Source);
		}
	}
}