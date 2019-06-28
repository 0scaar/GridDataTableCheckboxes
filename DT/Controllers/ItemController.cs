using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace DT.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetIds()
        //{
        //    var requestFormData = Request.Form;



        //    return Ok();
        //}

        public IActionResult ListItens()
        {
            //var requestFormData = Request.Form;

            var listaItens = GetItens();

            //var listaItensForm = ProcessarDadosForm(listaItens, requestFormData);

            dynamic response = new
            {
                Data = listaItens // listaItensForm,
                //Draw = requestFormData["draw"],
                //RecordsFiltered = listaItens.Count,
                //RecordsTotal = listaItens.Count
            };

            return Ok(response);
        }

        private List<Item> ProcessarDadosForm(List<Item> lstElements, IFormCollection requestFormData)
        {
            var skip = Convert.ToInt32(requestFormData["start"].ToString());
            var pageSize = Convert.ToInt32(requestFormData["length"].ToString());
            StringValues tempOrder = new[] { "" };
            
            if (requestFormData.TryGetValue("order[0][column]", out tempOrder))
            {
                var columnIndex = requestFormData["order[0][column]"].ToString();
                var sortDirection = requestFormData["order[0][dir]"].ToString();

                tempOrder = new[] { "" };

                if (requestFormData.TryGetValue($"columns[{columnIndex}][data]", out tempOrder))
                {
                    var columnName = requestFormData[$"columns[{columnIndex}][data]"].ToString();

                    if (pageSize > 0)
                    {
                        var prop = getProperty(columnName);
                        if (sortDirection == "asc")
                            return lstElements.OrderBy(prop.GetValue).Skip(skip).Take(pageSize).ToList();
                        else
                            return lstElements.OrderByDescending(prop.GetValue).Skip(skip).Take(pageSize).ToList();
                    }
                    else
                        return lstElements;
                }
            }
            return null;
        }

        private PropertyInfo getProperty(string name)
        {
            var properties = typeof(Models.Item).GetProperties();
            PropertyInfo prop = null;

            foreach(var item in properties)
            {
                if(item.Name.ToLower().Equals(name.ToLower()))
                {
                    prop = item;
                    break;
                }
            }

            return prop;
        }

        public List<Item> GetItens()
        {
            var listItens = new List<Item>()
            {
                new Item(){ ItemId=1, Nome="aaa", Descricao="AAA"},
                new Item(){ ItemId=2, Nome="bbb", Descricao="BBB"},
                new Item(){ ItemId=3, Nome="ccc", Descricao="CCC"},
                new Item(){ ItemId=4, Nome="ddd", Descricao="DDD"},
                new Item(){ ItemId=5, Nome="aaa", Descricao="AAA"},
                new Item(){ ItemId=6, Nome="bbb", Descricao="BBB"},
                new Item(){ ItemId=7, Nome="ccc", Descricao="CCC"},
                new Item(){ ItemId=8, Nome="ddd", Descricao="DDD"},
                new Item(){ ItemId=9, Nome="aaa", Descricao="AAA"},
                new Item(){ ItemId=10, Nome="bbb", Descricao="BBB"},
                new Item(){ ItemId=11, Nome="ccc", Descricao="CCC"},
                new Item(){ ItemId=12, Nome="ddd", Descricao="DDD"},
            };

            return listItens;
        }
    }
}