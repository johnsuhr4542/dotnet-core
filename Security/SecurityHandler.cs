using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using NLog;

namespace application.Security {
  public class SecurityHandler : IAuthorizationService
  {
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
      var roles = user.FindFirstValue("Roles");
      _logger.Info($"roles :: {roles}");

      var accepted = requirements.All(e => {
        var requirement = e as ClaimsAuthorizationRequirement;
        return requirement.ClaimType == roles;
      });

      var result = accepted ? AuthorizationResult.Success() : AuthorizationResult.Failed();

      return Task.Run(() => result);
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
    {
      var roles = user.FindFirstValue("Roles");
      _logger.Info($"roles :: {roles}");
      _logger.Info($"policyName :: {policyName}");
      return Task.Run(() => AuthorizationResult.Failed());
    }
  }
}