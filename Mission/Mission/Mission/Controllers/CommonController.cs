using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models.CommonModels;
using Mission.Service.IServices;
using Mission.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mission.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController(ICommonService commonService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly ICommonService _commonService = commonService;
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        ResponseResult result = new ResponseResult();

        [HttpGet]
        [Route("CountryList")]
        [Authorize]
        public ResponseResult CountryList()
        {
            try
            {
                result.Data = _commonService.CountryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("CityList/{countryId}")]
        [Authorize]
        public ResponseResult CityList(int countryId)
        {
            try
            {
                result.Data = _commonService.CityList(countryId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionCountryList")]
        public ResponseResult MissionCountryList()
        {
            try
            {
                result.Data = _commonService.MissionCountryList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionCityList")]
        public ResponseResult MissionCityList()
        {
            try
            {
                result.Data = _commonService.MissionCityList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionThemeList")]
        public ResponseResult MissionThemeList()
        {
            try
            {
                result.Data = _commonService.MissionThemeList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("MissionSkillList")]
        public ResponseResult MissionSkillList()
        {
            try
            {
                result.Data = _commonService.MissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("MissionTitleList")]
        public ResponseResult MissionTitleList()
        {
            try
            {
                result.Data = _commonService.MissionTitleList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        // CommonController.cs
        [HttpPost]
        [Route("UploadImage")]
        public async Task<ActionResult> UploadImage()
        {
            List<string> fileList = new List<string>();
            var files = Request.Form.Files;

            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest(new { success = false, message = "No files uploaded" });
                }

                string uploadFolder = Path.Combine("MissionImages");
                string rootUploadPath = Path.Combine(_hostingEnvironment.ContentRootPath, "UploadedFiles", uploadFolder);

                if (!Directory.Exists(rootUploadPath))
                {
                    Directory.CreateDirectory(rootUploadPath);
                }

                foreach (var file in files)
                {
                    if (file.Length == 0) continue;

                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    // Validate file type
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return BadRequest(new { success = false, message = "Invalid file type" });
                    }

                    string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    string fullPath = Path.Combine(rootUploadPath, uniqueFileName);
                    string relativePath = Path.Combine("UploadedFiles", uploadFolder, uniqueFileName).Replace("\\", "/");

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    fileList.Add(relativePath); // Remove leading slash for consistency
                }

                return Ok(new { success = true, files = fileList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetUserSkill/{userId}")]
        public ResponseResult GetUserSkill(int userId)
        {
            try
            {
                result.Data = _commonService.GetUserSkill(userId);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("AddUserSkill")]
        public async Task<ResponseResult> AddUserSkill(UserSkills skills)
        {
            try
            {
                result.Data = await _commonService.AddUserSkill(skills);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    
    }
}
