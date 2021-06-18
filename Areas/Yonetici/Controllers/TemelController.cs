using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeYonetim.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
    [Authorize(Roles = "Admin")]
    public abstract class TemelController : Controller
    {

    }
}
