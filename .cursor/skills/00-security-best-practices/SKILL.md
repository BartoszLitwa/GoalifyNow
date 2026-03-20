---
name: 00-security-best-practices
description: General security best practices for GoalifyNow application
---

# Security Best Practices

## Input Validation

### Backend Validation
```csharp
// FluentValidation for all DTOs
public class CreatePropertyValidator : AbstractValidator<CreatePropertyDto>
{
    public CreatePropertyValidator()
    {
        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200)
            .Matches(@"^[a-zA-Z0-9\s,.-]+$");  // Prevent injection
        
        RuleFor(x => x.MonthlyRent)
            .GreaterThan(0)
            .LessThan(1000000);
    }
}
```

### SQL Injection Prevention
```csharp
// GOOD - Parameterized queries (EF Core)
var users = await db.Users
    .Where(u => u.Email == email)  // Parameterized
    .ToListAsync();

// BAD - String concatenation (NEVER DO THIS)
var query = $"SELECT * FROM Users WHERE Email = '{email}'";  // DANGEROUS!
```

## Secret Management

### Azure Key Vault
```csharp
// Secrets loaded from Key Vault
builder.Configuration.AddAzureKeyVault(
    new Uri(builder.Configuration["Azure:KeyVault:VaultUri"]!),
    new DefaultAzureCredential()
);

// Access secrets
var stripeKey = configuration["Stripe--SecretKey"];  // From Key Vault
```

### Never Commit Secrets
```gitignore
# Always in .gitignore
appsettings.Production.json
appsettings.*.json
*.env
.env.*
secrets.json
```

## API Security

### HTTPS Enforcement
```csharp
app.UseHttpsRedirection();

// HSTS in production
if (app.Environment.IsProduction())
{
    app.UseHsts();
}
```

### API Versioning
```csharp
// Version API to deprecate insecure endpoints
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SecureController : ControllerBase { }
```

## Data Protection

### Sensitive Data Encryption
```csharp
// Encrypt sensitive fields
public class User : Entity
{
    public string Email { get; set; }
    
    [EncryptColumn]  // Custom attribute
    public string? SensitiveData { get; set; }
}
```

### Soft Delete (Audit Trail)
```csharp
// Never hard delete sensitive data
user.IsActive = false;
user.DeletedAt = DateTime.UtcNow;
user.DeletedBy = currentUserId;
await db.SaveChangesAsync();
```

## Multi-Tenancy Security

### Tenant Isolation
```csharp
// Automatic tenant filtering in DbContext
protected override void OnModelCreating(ModelBuilder builder)
{
    builder.Entity<Property>().HasQueryFilter(
        p => p.TenantId == _contextService.TenantId
    );
}

// CRITICAL: Always verify tenant access
if (property.TenantId != currentContext.TenantId)
    return Forbid();
```

## Logging & Monitoring

### Security Event Logging
```csharp
_logger.LogWarning(
    "Failed login attempt for email {Email} from IP {IP}",
    email,
    httpContext.Connection.RemoteIpAddress
);

// NEVER log sensitive data
_logger.LogInformation("User {UserId} logged in", userId);  // Good
_logger.LogInformation("Password: {Password}", password);   // BAD!
```

### Application Insights
```json
{
  "Azure": {
    "ApplicationInsights": {
      "Enabled": true,
      "ConnectionString": "...",
      "EnableExceptionCollection": true
    }
  }
}
```

## Frontend Security

### XSS Prevention
```typescript
// Angular sanitizes by default
// Trust only when necessary
<div [innerHTML]="sanitizedHtml"></div>

constructor(private sanitizer: DomSanitizer) {}

sanitize(html: string): SafeHtml {
  return this.sanitizer.sanitize(SecurityContext.HTML, html) || '';
}
```

### CSRF Protection
```typescript
// Angular CSRF protection (automatic with HttpClient)
// Backend validates XSRF token
```

## Dependency Security

### Regular Updates
```bash
# Check for vulnerabilities
dotnet list package --vulnerable
npm audit

# Update packages
dotnet update
npm update
```

### Trusted Sources Only
- NuGet.org for .NET packages
- npm registry for Node packages
- Verify package signatures

## Access Control

### Principle of Least Privilege
```csharp
// Users get minimal required permissions
services.AddAuthorization(options =>
{
    options.AddPolicy("ViewProperties", policy =>
        policy.RequireClaim("permission", "properties:read"));
    
    options.AddPolicy("ManageProperties", policy =>
        policy.RequireClaim("permission", "properties:write"));
});
```

## Compliance

### GDPR Compliance
- User data export functionality
- Right to deletion (soft delete)
- Consent management
- Data retention policies

### Audit Trail
```csharp
// Track all changes
public class AuditLog : Entity
{
    public string Action { get; set; }
    public Guid UserId { get; set; }
    public string EntityType { get; set; }
    public Guid EntityId { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}
```

## Security Checklist

- [ ] All secrets in Azure Key Vault
- [ ] HTTPS enforced everywhere
- [ ] Strong JWT keys (32+ chars)
- [ ] Short token lifetimes
- [ ] Rate limiting on auth endpoints
- [ ] BCrypt password hashing (work factor 12+)
- [ ] Input validation on all endpoints
- [ ] SQL injection prevention (EF Core)
- [ ] XSS prevention (Angular sanitization)
- [ ] CORS properly configured
- [ ] Security headers implemented
- [ ] Tenant isolation enforced
- [ ] Audit logging enabled
- [ ] Regular dependency updates
- [ ] No secrets in code
- [ ] GDPR compliance measures

**See authentication rules for detailed auth patterns.**