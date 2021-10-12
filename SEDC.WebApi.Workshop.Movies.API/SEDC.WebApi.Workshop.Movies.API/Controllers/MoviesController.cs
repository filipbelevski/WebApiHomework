using DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SEDC.WebApi.Workshop.Movies.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [AllowAnonymous] // guest can search movies
        [HttpGet]
        public ActionResult<List<MovieDto>> GetAll([FromQuery] string genre)
        {
            try
            {
                if(genre != null)
                {
                    return Ok(_movieService.GetByGenre(genre));
                }
                return Ok(_movieService.GetAllMovies());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {
                return Ok(_movieService.GetById(id));
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                return Ok(_movieService.Delete(id));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPut]
        public ActionResult<string> Update([FromBody] MovieDto movie)
        {
            try
            {
                return Ok(_movieService.Update(movie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public ActionResult<string> Create([FromBody] MovieDto movie)
        {
            try
            {
                return Ok(_movieService.Create(movie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
