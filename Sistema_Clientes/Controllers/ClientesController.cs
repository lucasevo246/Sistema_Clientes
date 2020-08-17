using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Clientes.Models;
using Sistema_Clientes.Models.Validation;

namespace Sistema_Clientes.Controllers
{
    public class ClientesController : Controller
    {
        private ClienteDbContext db = new ClienteDbContext();

        // GET: Clientes
        public ActionResult Index(string searchString,string Search2)
        {
            var clientes = db.Clientes.Where(x => x.Ativo == true);

            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.Nome.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Search2))
            {
                clientes = clientes.Where(s => s.Documento.Contains(Search2));

            }


            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Tipo,Documento,Tel")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                /* Validação CPF e CNPJ */
                if(cliente.Tipo == Models.Enums.Tipo.Fisica)
                {
                    var ok = Utils.IsCpf(cliente.Documento);

                    if (!ok)
                    {
                        ModelState.AddModelError("Documento", "CPF Inválido");
                        return View(cliente);
                    }
                }
                else
                {
                    var ok = Utils.IsCnpj(cliente.Documento);

                    if (!ok)
                    {
                        ModelState.AddModelError("Documento", "CNPJ Inválido");
                        return View(cliente);
                    }
                }

                cliente.Ativo = true;
                cliente.DataCadastro = DateTime.Today;

                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Tipo,DataCadastro,Documento,Tel,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                /* Validação CPF e CNPJ */
                if (cliente.Tipo == Models.Enums.Tipo.Fisica)
                {
                    var ok = Utils.IsCpf(cliente.Documento);

                    if (!ok)
                    {
                        ModelState.AddModelError("Documento", "CPF Inválido");
                        return View(cliente);
                    }
                }
                else
                {
                    var ok = Utils.IsCnpj(cliente.Documento);

                    if (!ok)
                    {
                        ModelState.AddModelError("Documento", "CNPJ Inválido");
                        return View(cliente);
                    }
                }
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            //db.Clientes.Remove(cliente);
            cliente.Ativo = false;
            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Clientes/DeletedList
        public ActionResult DeletedList(string searchString,string Search2)
        {
            var clientes = db.Clientes.Where(x => x.Ativo == false);

            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.Nome.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Search2))
            {
                clientes = clientes.Where(s => s.Documento.Contains(Search2));

            }

            return View(clientes);
        }


        // GET: Clientes/Delete/5
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Recover/5
        [HttpPost, ActionName("Recover")]
        [ValidateAntiForgeryToken]
        public ActionResult RecoverItem(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            //db.Clientes.Remove(cliente);
            cliente.Ativo = true;
            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
