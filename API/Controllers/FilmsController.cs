using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

[Route("api/[controller]")]
[ApiController]
public class FilmsController : ControllerBase
{
    private readonly FilmDbContext _context;

    public FilmsController(FilmDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Film>> GetProducts()
    {
        return _context.films.ToList();
    }

    [HttpGet("{title}")]
    public ActionResult<Film> GetFilm(string title)
    {
        var film = _context.films.Find(title);
        if (film == null)
        {
            return NotFound();
        }
        return film;
    }

    [HttpPost]
    public ActionResult<Film> PostFilm(Film film)
    {
        _context.films.Add(film);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetFilm), new { title = film.Title }, film);
    }

    [HttpPut("{title}")]
    public IActionResult PutFilm(string title, Film film)
    {
        if (title != film.Title)
        {
            return BadRequest();
        }

        _context.Entry(film).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{title}")]
    public IActionResult DeleteFilm(string title)
    {
        var film = _context.films.Find(title);
        if (film == null)
        {
            return NotFound();
        }

        _context.films.Remove(film);
        _context.SaveChanges();

        return NoContent();
    }
}
