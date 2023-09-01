using System.Net;
using AutoMapper;
using MagicVilla_WebAPI.Data;
using MagicVilla_WebAPI.Logging;
using MagicVilla_WebAPI.Models;
using MagicVilla_WebAPI.Models.DTOs;
using MagicVilla_WebAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_WebAPI.Controllers;


[Route("api/VillaNumberApi")]
[ApiController]
public class VillaNumberApiController : ControllerBase
{

    private readonly IVillaNumberRepository _dbVillaNumber;
    private readonly IMapper _mapper;
    protected ApiResponse _response;
    private readonly IVillaRepository _dbVilla;


    public VillaNumberApiController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
    {
        _dbVillaNumber = dbVillaNumber;
        _mapper = mapper;
        this._response = new();
        _dbVilla = dbVilla;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetVillaNumbers()
    {
        try
        {
            IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();
            _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return _response;
    }

    [HttpGet("{id:int}", Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse>> GetVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var villaNumber = await _dbVillaNumber.GetAsync(s => s.VillaNo == id);
            if (villaNumber == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return _response;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createNumberDto)
    {
        try
        {
            if (await _dbVillaNumber.GetAsync(s => s.VillaNo == createNumberDto.VillaNo) != null)
            {
                ModelState.AddModelError("CustomError", "Villa number already exists");
                return BadRequest(ModelState);
            }
            
            if (await _dbVilla.GetAsync(u => u.Id == createNumberDto.VillaID) == null)
            {
                ModelState.AddModelError("CustomError", "VillaID is invalid");
                return BadRequest(ModelState);
            }

            if (createNumberDto == null)
            {
                return BadRequest(createNumberDto);
            }

            

            VillaNumber villaNumber = _mapper.Map<VillaNumber>(createNumberDto);


            await _dbVillaNumber.CreateAsync(villaNumber);

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.Created;

            //CreatedAtRoute gives a URL that contains created item.
            return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return _response;
    }

    [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> DeleteVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villaNumber = await _dbVillaNumber.GetAsync(s => s.VillaNo == id);

            if (villaNumber == null)
            {
                return NotFound();
            }

            await _dbVillaNumber.RemoveAsync(villaNumber);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> UpdateVillaNumber(int id, VillaNumberUpdateDTO updateDto)
    {
        try
        {
            if (updateDto == null || id != updateDto.VillaNo)
            {
                return BadRequest();
            }
            if (await _dbVilla.GetAsync(u => u.Id == updateDto.VillaID) == null)
            {
                ModelState.AddModelError("CustomError", "VillaID is invalid");
                return BadRequest(ModelState);
            }

            VillaNumber model = _mapper.Map<VillaNumber>(updateDto);

            await _dbVillaNumber.UpdateAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { e.ToString() };
        }

        return _response;
    }

}