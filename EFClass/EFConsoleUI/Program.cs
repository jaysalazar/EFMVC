using System;
using System.Collections.Generic;
using EFBusinessLogic;
using EFEntities;

namespace EFConsoleUI
{
    class Program
    {
        static BusinessLogic businessLogic = new BusinessLogic();
        static SalesReason salesReason = new SalesReason();
        //asasdasddasd
        static void Main(string[] args)
        {
            while (true)
            {
                DisplayMenu();
                DisplayMessage(6);
                int choice = Convert.ToInt32(GetInput());

                switch (choice)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        RetrieveRecords(choice);
                        break;
                    case 2:
                        RetrieveRecords(choice);
                        break;
                    case 3:
                        AddRecord();
                        break;
                    case 4:
                        UpdateRecord();
                        break;
                    case 5:
                        DeleteRecord();
                        break;
                    case 6:
                        Count();
                        break;
                    case 7:
                        RetrieveBusinessEntity();
                        break;
                    default:
                        DisplayMessage(7);
                        break;
                }
            }
        }

        static string GetName()
        {
            DisplayMessage(1);
            string name = GetInput();
            return name;
        }

        static string GetReasonType()
        {
            DisplayMessage(2);
            string reasonType = GetInput();
            return reasonType;
        }

        static int GetID()
        {
            DisplayMessage(3);
            int id = Convert.ToInt32(GetInput());
            return id;
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n[1] - Retrieve All Records\n[2] - Retrieve Specific Record\n[3] - Add Record\n[4] - Update Record"
                            + "\n[5] - Delete Record\n[6] - Count rows\n[7] - Display Business Entity\n\nPress [0] to exit.\n");
        }

        static string GetInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        static void DisplayRowStatus(int rowStatus)
        {
            if (rowStatus == 1)
            {
                Console.WriteLine("\nSuccessful! {0} row(s) affected", rowStatus);
            }
            else
            {
                DisplayMessage(8);
            }
        }

        //optimize code
        static void RetrieveRecords(int choice)
        {
            if (choice == 1)
            {
                // RetrieveAllRecords
                List<SalesReason> salesReasonList = businessLogic.RetrieveAllRecords();

                if (salesReasonList.Count > 0)
                {
                    DisplayMessage(4);
                    foreach (var item in salesReasonList)
                    {
                        Console.WriteLine("{0} {1} {2}", item.SalesReasonID.ToString().PadLeft(2).PadRight(3), item.Name.PadRight(27), item.ReasonType);
                    }
                }
            }
            else
            {
                // RetrieveSpecificRecords
                salesReason = businessLogic.RetrieveSpecificRecord(GetID());

                if (salesReason != null)
                {
                    DisplayMessage(4);
                    Console.WriteLine("{0} {1} {2}", salesReason.SalesReasonID.ToString().PadLeft(2).PadRight(3), salesReason.Name.PadRight(27), salesReason.ReasonType);
                }
                else
                {
                    DisplayMessage(8);
                }
            }
        }

        static void AddRecord()
        {
            salesReason = new SalesReason() { Name = GetName(), ReasonType = GetReasonType(), ModifiedDate = DateTime.Now };
            int rowStatus = businessLogic.AddRecord(salesReason);
            DisplayRowStatus(rowStatus);
        }

        static void UpdateRecord()
        {
            salesReason = new SalesReason() { SalesReasonID = GetID(), Name = GetName(), ReasonType = GetReasonType(), ModifiedDate = DateTime.Now };
            int rowStatus = businessLogic.UpdateRecord(salesReason);
            DisplayRowStatus(rowStatus);
        }

        static void DeleteRecord()
        {
            int rowStatus = businessLogic.DeleteRecord(GetID());
            DisplayRowStatus(rowStatus);
        }

        static void Count()
        {
            Console.WriteLine("\nCurrent number of rows: {0}", businessLogic.Scalar());
        }

        static void RetrieveBusinessEntity()
        {
            BusinessEntity businessEntity = new BusinessEntity() { BusinessEntityID = GetID() };
            List<uspGetEmployeeManagers_Result> businessEntityList = businessLogic.StoredProcedure(businessEntity);
            if (businessEntityList.Count > 0)
            {
                DisplayMessage(5);
                foreach (var item in businessEntityList)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4}", item.BusinessEntityID.ToString().PadLeft(2).PadRight(3), item.FirstName.PadRight(7), item.LastName.PadRight(14),
                                                          item.ManagerFirstName, item.ManagerLastName);
                }
            }
            else
            {
                DisplayMessage(8);
            }
        }

        static void DisplayMessage(int message)
        {
            switch (message)
            {
                case 1:
                    Console.Write("\nEnter Name: ");
                    break;
                case 2:
                    Console.Write("\nEnter ReasonType: ");
                    break;
                case 3:
                    Console.Write("\nEnter ID: ");
                    break;
                case 4:
                    Console.WriteLine("{0} {1} {2}", "\nID".PadRight(4), "Name".PadRight(27), "ReasonType\n");
                    break;
                case 5:
                    Console.WriteLine("{0} {1} {2}", "\nID".PadRight(4), "Employee Name".PadRight(22), "Manager\n");
                    break;
                case 6:
                    Console.Write("Enter choice: ");
                    break;
                case 7:
                    Console.WriteLine("\nInput entered is not within the choices. Please try again.");
                    break;
                case 8:
                    Console.WriteLine("\nRecord not found.");
                    break;
            }
        }
    }
}
