using HotelAppLIbrary.Data;
using HotelAppLIbrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages
{
    public class RoomSearchModel : PageModel
    {
        private readonly IDatabaseData _db;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1); 

        public List<RoomTypeModel> AvailableRoomTypes { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false;

        public RoomSearchModel(IDatabaseData db)
        {
            _db = db;
        }
        public void OnGet()
        {
            if(SearchEnabled == true)
            {
                AvailableRoomTypes = _db.GetAvailableRoomsTypes(StartDate, EndDate);
            }
        }

        public IActionResult OnPost()
        {
            return RedirectToPage(new 
            {
                SearchEnabled = true,
                StartDate = StartDate.ToString("yyyy-MM-dd"),
                EndDate = EndDate.ToString("yyyy-MM-dd")
            });
        }
    }
}
