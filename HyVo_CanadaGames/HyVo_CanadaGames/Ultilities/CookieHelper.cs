﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyVo_CanadaGames.Ultilities
{
    public static class CookieHelper
    {
        public static void CookieSet(HttpContext _context, string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            _context.Response.Cookies.Append(key, value, option);
        }
    }
}
