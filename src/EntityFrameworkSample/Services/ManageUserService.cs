using EntityFrameworkSample.Interfaces;
using EntityFrameworkSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkSample.Services
{
    public class ManageUserService : IManageUserService
    {
        private readonly sampleContext _context;

        public ManageUserService(sampleContext context)
        {
            _context = context;
        }

        public string GetUserId()
        {
            return (from row in this._context.AspNetUsers
                    select row.Id).FirstOrDefault();
        }

    }
}
