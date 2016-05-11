using System.Collections.Generic;
using Microsoft.SharePoint.Client;

namespace chachiprofesMVCWeb.Models
{
    public class ProfesorModel
    {
        public int Id { get; set; }
        public string Profesor { get; set; }
        public int Valoracion  { get; set; }
        public List<ConocimientoModel> Conocimientos { get; set; }


    }
}