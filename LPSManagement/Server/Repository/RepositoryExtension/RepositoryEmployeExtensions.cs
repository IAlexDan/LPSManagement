using LPSManagement.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace LPSManagement.Server.Repository.RepositoryExtension
{
    public static class RepositoryEmployeExtensions
    {
        public static IQueryable<Employe> Search(this IQueryable<Employe> employes, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return employes;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return employes.Where(p => p.FirstName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Employe> ExpDate(this IQueryable<Employe> employes, string expDate)
        {
            if (!(expDate == "expdate"))
                return employes;

            return employes.Where(d => d.StartDate.Date < DateTime.Now.Date.AddDays(-336)
                                   && d.StartDate.Date > DateTime.Now.Date.AddDays(-396));
        }

        public static IQueryable<Employe> Sort(this IQueryable<Employe> employes, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return employes.OrderBy(e => e.FirstName);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Employe).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery))
                return employes.OrderBy(e => e.FirstName);

            return employes.OrderBy(orderQuery);
        }
    }
}
