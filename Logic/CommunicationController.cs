using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class CommunicationController : MainController
    {
        public List<Communication> GetCommunicationsNeeded(int id, EClass className)
        {
            List<Communication> communications = entities.RelCommunicationClasses.Where(x => x.ClassId == id && x.Class == className.ToString()).
                                                    Select(c => c.Communication).ToList();
            return communications;
        }
    }
}
