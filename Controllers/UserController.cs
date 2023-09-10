using CachingExample.Models;
using CachingExample.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace CachingExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string userCacheKey = "user";
        private readonly IMemoryCache _cache;  
        private readonly IUserService _userService;  
        private ILogger<UserController> _logger;
        public UserController(IMemoryCache cache, ILogger<UserController> logger, IUserService userService)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            if (_cache.TryGetValue(userCacheKey, out User user))
            {
                _logger.Log(LogLevel.Information, "User found in cache.");
                user.From = "FromCache";
            }
            else
            {
                _logger.Log(LogLevel.Information, "User not found in cache. Fetching from database.");
                user = await _userService.GetUser();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(userCacheKey, user, cacheEntryOptions);
                user.From = "FromService";
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult ClearCache([FromBody]User user)
        { 
            //bussines rules
            _cache.Remove(userCacheKey);
            return new ObjectResult(user) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}
