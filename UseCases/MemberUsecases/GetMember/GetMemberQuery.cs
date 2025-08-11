using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F23.Kernel;
using FluentValidation;
using UseCases.MemberUsecases.GetMember;

namespace UseCases.MemberUsecases.GetMember
{
    public class GetMemberQuery: IQuery<GetMemberQueryResult>
    {
        public string Username { get; init; }
    }
}


public class GetMemberQueryValidator : AbstractValidator<GetMemberQuery>
{
    public GetMemberQueryValidator()
    {
        RuleFor(q => q.Username)
            .NotEmpty()
            .WithMessage("Username is required.");
    }
}
