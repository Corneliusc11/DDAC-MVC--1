using DDACAPI.Data;
using DDACAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DDACAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessLogController : ControllerBase
    {

        private readonly UserAccessLogContext _context;
        private readonly UserContext _usercontext;
        DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        public UserAccessLogController(UserAccessLogContext context, UserContext usercontext)
        {
            _context = context;
            _usercontext = usercontext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLog>>> GetAccessLogs()
        {
            /*return await _context.UserAccessLog.ToListAsync(); Join(
                _context.User,
                accesslog => accesslog.userid,
                user => user.Id
              ).*/
            return await _context.UserAccessLog.Join(
                    _context.User,
                    log => log.userid,
                    user => user.Id,
                    (log, user) => new UserLog
                    {
                        logid = log.logid,
                        userid = log.userid,
                        username = user.Username,
                        login = log.login,
                        logout = log.logout

                    }
                    ).ToListAsync();

        }

        [HttpGet("login")]
        public async Task<ActionResult<int>> Login(int userid)
        {
           
            var log1 = new UserAccessLog();
            log1.userid = userid;
            log1.login = dateTimeNow;
            log1.logout = dateTimeNow;
            _context.UserAccessLog.Add(log1);
            await _context.SaveChangesAsync();
            return log1.logid;
        }

        [HttpPut]
        public async Task<ActionResult<int>> Logout(int logid)
        {

            var log1 = _context.UserAccessLog.Find(logid);
            _context.Entry(log1).State = EntityState.Modified;
            log1.logout = dateTimeNow;
            await _context.SaveChangesAsync();
            return log1.logid;
        }


    }
}
