using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGrid.Customer.Framework.Templates.Abstraction;
using NGrid.Customer.Framework.Templates.Api;

namespace NGrid.Customer.ToReplace.Api.Controller;

[Route($"{AppConstants.ApiInternalPath}/{AppConstants.ToReplaceApiPath}/toReplace/")]
[ApiExplorerSettings(GroupName = AppConstants.ToReplaceApiInternalGroupName)]
public class InternalToReplaceController(
    IValidator<Domain.ToReplace> validator,
    ISingleKeyBaseRepository<Domain.ToReplace, int> repository,
    IAuditTrailService auditTrailService,
    ILogger<InternalToReplaceController> logger)
    : SingleKeyRepositoryBaseController<Domain.ToReplace, int>(validator, repository, auditTrailService, logger)
{
    protected override string DefaultSourceName => AppConstants.ToReplaceApiName;
    protected override string DefaultAddedBy => AppConstants.Source;
}