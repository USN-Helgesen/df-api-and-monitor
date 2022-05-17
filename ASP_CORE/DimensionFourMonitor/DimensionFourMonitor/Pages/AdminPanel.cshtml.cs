using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DimensionFourMonitor.Consumers;
using DimensionFourMonitor.Models;

namespace DimensionFourMonitor.Pages
{
    public class AdminPanelModel : PageModel
    {
        private readonly DFourConsumer _consumer;
        public List<Space> spaces = new List<Space>();
        public List<string> typeList = new List<string>();
        public Space space;
        public Point point;
        public AdminPanelModel(DFourConsumer consumer)
        {
            _consumer = consumer;
        }
        public void OnGet()
        {
            PrepSpaces();
        }
        public List<Space> PrepSpaces()
        {
            _consumer.EstablishCredentials();
            spaces = _consumer.GetTopLevelSpaces().Result;
            return spaces;
        }
        public JsonResult OnGetSpace(string id)
        {
            _consumer.EstablishCredentials();
            space = _consumer.GetSpace(id).Result;
            return new JsonResult(space);
        }
        public JsonResult OnGetCreateSpace(string spaceName, string parentId)
        {
            _consumer.EstablishCredentials();
            if(parentId == null)
            {
                parentId = "";
            }
            bool successState = _consumer.CreateSpace(spaceName, parentId).Result;
            return new JsonResult(successState);
        }
        public JsonResult OnGetCreatePoint(string pointName, string parentId)
        {
            _consumer.EstablishCredentials();
            bool successState = _consumer.CreatePoint(pointName, parentId).Result;
            return new JsonResult(successState);
        }
        public JsonResult OnGetAddPointTypes(string pointId, string type)
        {
            _consumer.EstablishCredentials();

            point = _consumer.GetPoint(pointId).Result;
            typeList = point.types;
            bool exists = typeList.Contains(type);
            if(exists != true)
            {
                typeList.Add(type);
            }

            bool successState = _consumer.UpdatePointTypes(pointId, typeList).Result;
            return new JsonResult(successState);
        }
        public JsonResult OnGetRemovePointTypes(string pointId, string type)
        {
            _consumer.EstablishCredentials();

            point = _consumer.GetPoint(pointId).Result;
            typeList = point.types;
            typeList.Remove(type);

            bool successState = _consumer.UpdatePointTypes(pointId, typeList).Result;
            return new JsonResult(successState);
        }
        public JsonResult OnGetDeleteSpace(string spaceId)
        {
            _consumer.EstablishCredentials();
            bool successState = _consumer.DeleteSpace(spaceId).Result;
            return new JsonResult(successState);
        }
        public JsonResult OnGetDeletePoint(string pointId)
        {
            _consumer.EstablishCredentials();
            bool successState = _consumer.DeletePoint(pointId).Result;
            return new JsonResult(successState);
        }
    }
}
