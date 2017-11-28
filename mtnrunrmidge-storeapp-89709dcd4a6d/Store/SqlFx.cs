using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    class SqlFx
    {
        #region Invoices
        /// <summary>
        /// This method returns the string of sql to return the next invoice number.
        /// </summary>
        /// <returns></returns>
        public static string SelectNextInvoiceNumber()
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices;";
                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);

            }
        }

        /// <summary>
        /// Returns a pretty join displaying ItemCode, ItemDesc, and Cost. Used with Invoice class.
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        public static string SelectItemsPretty(String InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT li.ItemCode AS Name, id.ItemDesc AS Description, id.Cost AS Cost, li.LineItemNum as LineItemNum FROM LineItems li INNER JOIN ItemDesc id ON li.ItemCode=id.ItemCode WHERE InvoiceNum=" + InvoiceNum + "; ";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When run will return all invoices in 3 columns
        /// InvoiceNum, InvoiceDate, TotalCharge
        /// </summary>
        /// <returns></returns>
        public static string SelectAllInvoice()
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region search
        /// <summary>
        /// This SQL gets all data on an invoice for a given InvoiceID.
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceData(string sInvoiceID)

        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + ex.Message);
            }

        }

        /// <summary>
        /// This SQL gets all invoice data and returns it in 3 columns: Invoice Number, Invoice Date, Total Charge
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string ReturnAllInvoices()

        {
            try
            {
                string sSQL = "SELECT * FROM Invoices";

                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);
            }

        }

        /// <summary>
        /// Method that creates an SQL query that gets the invoice number, Line Item Number, Item Code, Item Description and Item cost 
        /// </summary>
        /// <returns>string SQL qyery</returns>
        public string ReturnAllInvoiceItems()
        {
            string sSQL = "SELECT Invoices.InvoiceNum, LineItems.LineItemNum, LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost" +
                            " FROM ItemDesc INNER JOIN(Invoices INNER JOIN LineItems ON Invoices.InvoiceNum = LineItems.InvoiceNum) ON ItemDesc.ItemCode = LineItems.ItemCode ORDER BY Invoices.InvoiceNum;";

            return sSQL;
        }




        /// <summary>
        /// This SQL gets a inserts a new invoice
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string InsertInvoice(string InvoiceDate, double TotCharges)

        {
            if (TotCharges > 0)
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotCharges) Values (#" + InvoiceDate + "#, " + TotCharges + ");";

                return sSQL;
            }
            else
            {
                throw new Exception("There cannot be an invoice if there is no charge or the charge is less that 0.");
            }

        }
        /// <summary>
        /// This SQL updates an existing invoice
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string UpdateInvoice(string InvoiceDate, string InvoiceNum, double TotCharges)

        {
            if (TotCharges > 0)
            {
                string sSQL = "UPDATE Invoices SET InvoiceDate= #" + InvoiceDate + "#, TotalCharge = " + TotCharges + " WHERE InvoiceNum = " + InvoiceNum + ";";
                return sSQL;
            }
            else
            {
                throw new Exception("The total charges cannot be less than or equal to 0.");
            }

        }
        /// <summary>
        /// Deletes several lineItems at once, mostly used for when deleting a line item
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <returns></returns>
        public static string DeleteAllLineItemsForInvoice(String InvoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum=" + InvoiceNum + ";";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL deletes an existing invoice by invoice number.
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public static string DeleteInvoice(string InvoiceNum)
        {
            try
            {
                string sSQL = "DELETE FROM Invoices WHERE InvoiceNum =" + InvoiceNum + ";";
                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);
            }

        }
        #endregion

        #region Items
        /// <summary>
        /// Returns all the items available in 3 columns: ItemCode, ItemDesc, and Cost
        /// </summary>
        /// <returns></returns>
        public static string SelectAllItems()
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc;";
                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);

            }
        }
        /// <summary>
        /// Returns all data on a specific item by itemCode.
        /// </summary>
        /// <returns></returns>
        public static string SelectSpecificItem(string ItemCode)
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc WHERE ItemCode = " + ItemCode + ";";
                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);

            }
        }

        /// <summary>
        /// Insert an insert statement for an inventory item
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        public static string InsertSpecificItem(string ItemCode, string ItemDesc, double Cost)
        {
            try
            {
                string sSQL = "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) Values(" + ItemCode + ", " + ItemDesc + ", " + Cost + ");";
                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);

            }
        }
        /// <summary>
        /// Updates an item in inventory
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        public static string UpdateItem(string ItemCode, string ItemDesc, double Cost)
        {
            try
            {
                string sSQL = "UPDATE ItemDesc SET ItemCode = " + ItemCode + ", ItemDesc = " + ItemDesc + ", ItemCost = " + Cost + "WHERE ItemCode = " + ItemCode + ";";
                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);

            }
        }
        /// <summary>
        /// Remove an item from inventory
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        public static string DeleteItem(string ItemCode)
        {
            try
            {
                string sSQL = "DELETE * FROM ItemDesc WHERE ItemCode = " + ItemCode + ";";
                return sSQL;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + ": " + e.Message);

            }
        }

        /// <summary>
        /// SQL method to build a search query depending on search criteria set
        /// </summary>
        /// <param name="invoiceNum">string invoice number</param>
        /// <param name="invoiceDate">string invoice date</param>
        /// <param name="totalCharge">string total charge</param>
        /// <returns></returns>
        public string ReturnSearchResults(string invoiceNum, string invoiceDate, string totalCharge)
        {
            ///checks to see what criteria have been set and builds the query accordingly
            string sSQL = "SELECT * FROM Invoices";
            if (invoiceNum != null && invoiceDate == null && totalCharge == null)
            {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            if (invoiceNum == null && invoiceDate != null && totalCharge == null)
            {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = " + invoiceDate;
                return sSQL;
            }
            if (invoiceNum == null && invoiceDate == null && totalCharge != null)
            {
                sSQL = "SELECT * FROM Invoices WHERE TotalCharge = " + totalCharge;
                return sSQL;
            }
            ////// multiple parametes set in the queyr
            if (invoiceNum != null && invoiceDate != null && totalCharge == null)
            {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum + " AND InvoiceDate =" + invoiceDate;
                return sSQL;
            }
            if (invoiceNum == null && invoiceDate != null && totalCharge != null)
            {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = " + invoiceDate + " AND TotalCharge = " + totalCharge;
                return sSQL;
            }
            if (invoiceNum != null && invoiceDate == null && totalCharge != null)
            {
                sSQL = "SELECT * FROM Invoices WHERE TotalCharge = " + totalCharge + " AND InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            ///all three parameters set
            if (invoiceNum != null && invoiceDate != null && totalCharge != null)
            {
                sSQL = "SELECT * FROM Invoices WHERE TotalCharge = " + totalCharge + " AND InvoiceNum = " + invoiceNum + " AND InvoiceDate = " + invoiceDate;
                return sSQL;
            }

            return sSQL;
        }

        /// <summary>
        /// When run will return all ItemDesc in 3 columns
        /// ItemCode, ItemDesc, Cost
        /// </summary>
        /// <returns></returns>
        public static string SelectAllItemDesc()
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc;";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }

}
