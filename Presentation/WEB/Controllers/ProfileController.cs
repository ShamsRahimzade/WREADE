﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace WEB.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public ProfileController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IActionResult> User(string username)
        {

            AppUser user = await _service.GetUser(username);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string username)
        {
            AppUser user = await _service.GetUser(username);
            if (user.UserName != HttpContext.User.Identity.Name)
            {
                return BadRequest();
            }
            if (user == null)
            {
                return NotFound();
            }
            EditProfileVM vm = _mapper.Map<EditProfileVM>(user);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileVM vm)
        {
            AppUser user = await _service.GetUser(HttpContext.User.Identity.Name);
            if (!ModelState.IsValid)
            {
                vm.MainImage = user.MainImage;
                vm.BackImage = user.BackImage;
                return View(vm);

            }
            var res = await _service.UpdateUser(user, vm);
            if (res.Any())
            {
                foreach (var item in res)
                {

                    ModelState.AddModelError(String.Empty, item);
                }
                vm.MainImage = user.MainImage;
                vm.BackImage = user.BackImage;
                return View(vm);
            }
            await _service.Logout();

            await _service.LoginNoPass(user.UserName);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Follow(string followeeId)
        {
            await _service.Follow(followeeId);
            AppUser user = await _service.GetUserById(followeeId);
            return RedirectToAction("User", "Profile", new { username = user.UserName });
        }
        public async Task<IActionResult> Unfollow(string followeeId)
        {
            await _service.Unfollow(followeeId);
            AppUser user = await _service.GetUserById(followeeId);
            return RedirectToAction("User", "Profile", new { username = user.UserName });
        }
    }
}
