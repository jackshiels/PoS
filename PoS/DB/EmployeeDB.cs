﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.BusDomain;
using System.Data;
using System.Data.SqlClient;

namespace PoS.DB
{
    public class EmployeeDB : DB
    {
        #region Members
        private Collection<Employee> empList;
        private string sqlProd = "SELECT * FROM EmployeeRegister; SELECT * FROM Employee; SELECT * FROM Person";
        #endregion

        #region Constructors
        public EmployeeDB() : base()
        {
            empList = new Collection<Employee>();
            FillDataSet(sqlProd);
            ReadEmployees();
        }
        #endregion

        #region Methods - READ
        public void ReadEmployees()
        {
            Employee anEmp = new Employee();

            // Parses the table to get data for each customer
            foreach (DataRow dRow in dsMain.Tables["EmployeeRegister"].Rows)
            {
                if (!(dRow.RowState == DataRowState.Deleted))
                {
                    // Do the conversion stuff here.
                    anEmp.EmpID = Convert.ToString(dRow["EmployeeID"]);
                    anEmp.PersonID = Convert.ToString(dRow["PersonID"]);

                    // Creates a row from the Employee table that shares the same key from EmployeeRegister
                    DataRow tempEmpRow = dsMain.Tables["Employee"].Rows.Find(Convert.ToString(dRow["EmployeeID"]));

                    // Sets role
                    switch (Convert.ToString(tempEmpRow["Role"]))
                    {
                        case ("MarketingClerk"):
                            anEmp.Role = Employee.RoleType.MarketingClerk;
                            break;
                        case ("PickingClerk"):
                            anEmp.Role = Employee.RoleType.PickingClerk;
                            break;
                        case ("StockControlClerk"):
                            anEmp.Role = Employee.RoleType.StockControlClerk;
                            break;
                    }

                    // Gets the hash
                    anEmp.Hash = Convert.ToInt32(tempEmpRow["Password"]);

                    // Creates a row from the person table that shares the same key from CustomerRegister
                    DataRow temp = dsMain.Tables["Person"].Rows.Find(Convert.ToString(dRow["PersonID"]));
                    anEmp.Name = Convert.ToString(temp["Name"]);
                    anEmp.Address = Convert.ToString(temp["Address"]);

                    // Add to the list
                    empList.Add(anEmp);
                }
            }
        }
        #endregion

        #region Property Methods
        public Collection<Employee> EmpList
        {
            get { return empList; }
        }
        #endregion
    }
}
