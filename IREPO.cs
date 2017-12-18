using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace RPS
{
    interface IREPO<T> where T : class
    {
        List<T> getall();
        
        bool getallbyid(T a);  // for login
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T insert(T a);
        T update(T a);
        T delete(T a);

    }
    class GRepo<T> : IREPO<T>, IDisposable where T : class
    {
        private Conn cc;
        private DbSet<T> entities;
        public GRepo()
        {
            cc = new Conn();
            entities = cc.Set<T>();
        }
        //public GRepo(Conn c)
        //{
        //    this.cc = c;
        //    entities = cc.Set<T>();
        //}
        public T delete(T a)
        {
            entities.Remove(a);
            cc.SaveChanges();
            return a;
        }


        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = cc.Set<T>().Where(predicate);
            return query;
        }

        public List<T> getall()
        {
            return entities.ToList();

        }

        public bool getallbyid(T a)
        {
            throw new NotImplementedException();
        }

        public T insert(T a)
        {
            entities.Add(a);
            try
            {
                cc.SaveChanges();
                return a;
            }
            catch (Exception)
            {
                
                throw new System.Data.Entity.Core.UpdateException();
            }
        }

        public T update(T a)
        {
            cc.Entry(a).State = EntityState.Modified;
            cc.SaveChanges();
            return a;
        }
        ~GRepo()
        {
            Dispose();
        }
        public void Dispose()
        {
            cc.Dispose();
        }
    }
    class LoginREpo : IREPO<BAL>// For login only
    {
        Conn cn = new Conn();

        public BAL delete(BAL a)
        {

            var v = cn.LoginBals.Find(a.Userid);
            cn.LoginBals.Remove(v);
            cn.SaveChanges();
            return a;
        }

        ~LoginREpo() { }

        public IQueryable<BAL> FindBy(Expression<Func<BAL, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<BAL> getall()
        {
            var x = cn.LoginBals.ToList();
            return x;
        }

        public bool getallbyid(BAL a)
        {
            try
            {
                var x = cn.LoginBals.Any(b => b.Userid == a.Userid && b.Pass == a.Pass);
                return x;
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                //return false;
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BAL insert(BAL a)
        {
            cn.LoginBals.Add(a);
            cn.SaveChanges();
            return a;

        }

        public BAL update(BAL a)
        {
            // var x = cn.LoginBals.Find(a);
            cn.Entry(a).State = EntityState.Modified;
            cn.SaveChanges();
            return a;
        }
    }
}
