using BayraktarlarWebsite.Entities.Entities;
using BayraktarlarWebsite.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.DAL.Interfaces
{
    //Tabela sınıfımızla ilgili generic işlemleri yapabilmek için bir interface oluşturduk bu interface i IEntityrepository den inherit ettik interfaceler interfacelerden inherit edilir
    public interface ITabelaRepository : IEntityRepository<Tabela>
    {

    }
}
