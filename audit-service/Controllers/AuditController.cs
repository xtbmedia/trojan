using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data_library.Interfaces;
using data_library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace audit_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private IAuditService<object> auditService;

        public AuditController(IAuditService<object> auditService)
        {
            this.auditService = auditService;
        }

        // GET: api/Audit
        [HttpGet]
        public Task<IEnumerable<AuditEvent>> Get()
            => auditService.GetAsync();
    }
}
