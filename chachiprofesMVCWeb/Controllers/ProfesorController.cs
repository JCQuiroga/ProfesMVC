using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chachiprofesMVCWeb.Models;
using Microsoft.SharePoint.Client;

namespace chachiprofesMVCWeb.Controllers
{
    public class ProfesorController : Controller
    {
        // GET: Profesor
        public ActionResult Index()
        {
            List<ProfesorModel> model = new List<ProfesorModel>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    Web web = clientContext.Web;
                    clientContext.Load(web);
                    clientContext.ExecuteQuery();

                    ListCollection lists = web.Lists;
                    clientContext.Load<ListCollection>(lists);
                    clientContext.ExecuteQuery();

                    var profesores = lists.GetByTitle("Profesores");
                    clientContext.Load(profesores);
                    clientContext.ExecuteQuery();

                    CamlQuery query = new CamlQuery();
                    ListItemCollection profesoresItems = profesores.GetItems(query);
                    clientContext.Load(profesoresItems);
                    clientContext.ExecuteQuery();

                    foreach (var item in profesoresItems)
                    {
                        FieldLookupValue lookup = item["Conocimientos"] as FieldLookupValue;
                        int lId = lookup.LookupId;
                        int val;
                        int.TryParse(item["Valoraciones"].ToString(), out val);
                        var pi = profesores.GetItemById(lId);
                        clientContext.Load(pi);
                        clientContext.ExecuteQuery();
                        model.Add(new ProfesorModel()
                        {
                            Id = lId,
                            Profesor = pi["Title"].ToString(),
                            Valoracion = val
                        });
                    }





                    return View(model);
                }
            }
            return HttpNotFound();
        }

        //public ActionResult TotalPedidos()
        //{
        //    var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
        //    using (var clientContext = spContext.CreateUserClientContextForSPHost())
        //    {
        //        if (clientContext != null)
        //        {
        //            Web web = clientContext.Web;
        //            clientContext.Load(web);
        //            clientContext.ExecuteQuery();

        //            ListCollection lists = web.Lists;
        //            clientContext.Load<ListCollection>(lists);
        //            clientContext.ExecuteQuery();

        //            var profesores = lists.GetByTitle("Profesores");
        //            clientContext.Load(profesores);

        //            var conocimientos = lists.GetByTitle("Conocimientos");
        //            clientContext.Load(profesores);
        //            clientContext.Load(conocimientos);
        //            clientContext.ExecuteQuery();

        //            CamlQuery query = new CamlQuery();
        //            ListItemCollection profesoresItems = profesores.GetItems(query);
        //            clientContext.Load(profesoresItems);
        //            clientContext.ExecuteQuery();

        //            foreach (var item in profesoresItems)
        //            {
        //                FieldLookupValue lookup = item["Conocimientos"] as FieldLookupValue;
        //                int lId = lookup.LookupId;
        //                var pi = profesores.GetItemById(lId);
        //                clientContext.Load(pi);
        //                clientContext.ExecuteQuery();
        //                var valoraciones = pi["Valoraciones"];
        //            }



        //            return View(model);
        //        }
        //    }
        //    return null;
        //}
    }
}