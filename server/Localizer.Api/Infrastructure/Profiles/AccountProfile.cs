// unset

using AutoMapper;
using Localizer.Api.Resources.Account.Models;
using Localizer.Domain.Entities;

namespace Localizer.Api.Infrastructure.Profiles
{
	public class AccountProfile : Profile
	{
		public AccountProfile()
		{
			CreateMap<CreateAccountRequest, Account>(MemberList.Source);
		}
	}
}