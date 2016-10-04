using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using EFEntities;

namespace EFBusinessLogic
{
    public class BusinessLogic
    {
        List<Exception> exceptionList;

        //RetrieveAll
        public List<SalesReason> RetrieveAllRecords()
        {
            List<SalesReason> salesReasonList = new List<SalesReason>();

            try
            {
                using (var context = new AdventureWorks2008Entities())
                {
                    salesReasonList = (from salesRsn in context.SalesReasons
                                       orderby salesRsn.Name
                                       select salesRsn).ToList();

                    if (salesReasonList != null && salesReasonList.Count > 0)
                    {
                        return salesReasonList;
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionList = new List<Exception>();
                exceptionList.Add(ex);
            }
            return salesReasonList;
        }

        //Retrieve
        public SalesReason RetrieveSpecificRecord(int id)
        {
            SalesReason salesReason = new SalesReason();

            try
            {
                using (var context = new AdventureWorks2008Entities())
                {
                    salesReason = context.SalesReasons.Where(x => x.SalesReasonID == id).SingleOrDefault();

                    if (salesReason != null)
                    {
                        return salesReason;
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionList = new List<Exception>();
                exceptionList.Add(ex);
            }
            return salesReason;
        }

        //Add
        public int AddRecord(SalesReason salesReason)
        {
            int returnValue = 0;
            try
            {
                using (var context = new AdventureWorks2008Entities())
                {
                    if (salesReason != null)
                    {
                        context.SalesReasons.Add(salesReason);
                        returnValue = context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionList = new List<Exception>();
                exceptionList.Add(ex);
            }
            return returnValue;
        }

        //Update
        public int UpdateRecord(SalesReason salesReason)
        {
            int returnValue = 0;
            try
            {
                using (var context = new AdventureWorks2008Entities())
                {
                    //salesReason = (from salesRsn in context.SalesReasons
                    //                     where salesRsn.SalesReasonID == salesReason.SalesReasonID
                    //                     select salesRsn).SingleOrDefault();

                    if (salesReason != null)
                    {
                        context.Entry(salesReason).State = System.Data.Entity.EntityState.Modified;
                        returnValue = context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionList = new List<Exception>();
                exceptionList.Add(ex);
            }
            return returnValue;
        }

        //Delete
        public int DeleteRecord(int salesReasonID)
        {
            int returnValue = 0;
            try
            {
                using (var context = new AdventureWorks2008Entities())
                {
                    SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@id", salesReasonID) };
                    var item = context.Database.ExecuteSqlCommand("DELETE FROM Sales.SalesReason WHERE SalesReasonID = @id", parameters);
                    returnValue = item;
                }
            }
            catch (Exception ex)
            {
                exceptionList = new List<Exception>();
                exceptionList.Add(ex);
            }
            return returnValue;
        }

        //Scalar
        public int Scalar()
        {
            int output = 0;
            try
            {
                using (var context = new AdventureWorks2008Entities())
                {
                    output = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM HumanResources.Department").SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                exceptionList = new List<Exception>();
                exceptionList.Add(ex);
            }
            return output;
        }

        //StoredProcedure
        public List<uspGetEmployeeManagers_Result> StoredProcedure(BusinessEntity businessEntity)
        {
            List<uspGetEmployeeManagers_Result> businessEntityList = new List<uspGetEmployeeManagers_Result>();

            try
            {
                using (var context = new AdventureWorks2008Entities())
                {
                    SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@businessEntityID", businessEntity.BusinessEntityID) };

                    businessEntityList = context.Database.SqlQuery<uspGetEmployeeManagers_Result>("EXEC dbo.uspGetEmployeeManagers @BusinessEntityID = @businessEntityID", parameters).ToList();

                    if (businessEntityList.Count > 0)
                    {
                        return businessEntityList;
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionList = new List<Exception>();
                exceptionList.Add(ex);
            }
            return businessEntityList;
        }
    }
}
