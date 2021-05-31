using Hub.Tables;
using Hub.TablesRetriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Hub.Context
{
    //Embedded Class Email Settings in .NET
    //This Class is responible for sending emails both for the customers and admin of the website
    public class EmailSettings:OrderDetails
    {
        public string adminAddress = "trolll257@gmail.com";
        public string MailFromAddress = "noreplybotmaterials@gmail.com";
        public bool UseSsl = true;
        public string ServerName = "smtp.gmail.com";
        //public string Username= "noreplybotmaterials@gmail.com";
        //public string Password= "noreplybotmat123321";
        public string Username = "noreplybotstudmaterials@gmail.com";
        public string Password= "botmaterials123";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        // For Offline Storage 
        public string FileLocation = @"c:\University_Materials_Emails";
       
        
    }
    public class EmailProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        

        public EmailProcessor(EmailSettings settings)
        {
            emailSettings = settings;
            
        }
        public void ProcessOrder(ShoppingBasket basket, OrderDetails orderDetails)
        {
            using (var smtpClient = new SmtpClient())
            {

                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                // For Customer 
                StringBuilder body = new StringBuilder()
                .AppendLine("The Order Is Processed")
                .AppendLine("---")
                .AppendLine("Materials: ");
                // For Admin
                StringBuilder body_2 = new StringBuilder()
                 .AppendLine("The Order Is Processed")
                .AppendLine("---")
                .AppendLine("Materials: ");


                foreach (var item in basket.Procedures)
                {
                    var subtotal = item.Material.Cost * item.Amount;
                    body.AppendFormat("{0} x {1} (Overall: {2:c})\n", item.Amount, item.Material.Name, subtotal.ToString("# UAH"));
                    body_2.AppendFormat("{0} x {1} (Overall: {2:c})\n", item.Amount, item.Material.Name, subtotal.ToString("# UAH"));


                }


                body.AppendFormat("\nTotal Cost: {0:c}", basket.CountTotalCost().ToString("# UAH"))
                    .AppendLine("")
                    .AppendLine("---")
                    .AppendLine("Delivery Credentials:")
                    .AppendLine($"Name: {orderDetails.Name}")
                    .AppendLine($"Surname: {orderDetails.Surname}")
                    .AppendLine($"Address: {orderDetails.Address}")
                    .AppendLine($"Country: {orderDetails.Country}")
                    .AppendLine($"City: {orderDetails.City}")
                    .AppendLine("---")
                    .AppendFormat("Student Status Check: {0}", orderDetails.StudentCheck ? "Yes" : "No")
                    .AppendLine("")
                    .AppendLine("---")
                    .AppendLine("Administrator will reach You out and the materials will be sent as soon as possible - Payment Will be Requested After Delivery");

                body_2.AppendFormat("\nTotal Cost: {0:c}", basket.CountTotalCost().ToString("# UAH"))
                    .AppendLine("")
                    .AppendLine("---")
                    .AppendLine("Delivery Credentials:")
                     .AppendLine($"Name: {orderDetails.Name}")
                    .AppendLine($"Surname: {orderDetails.Surname}")
                    .AppendLine($"Address: {orderDetails.Address}")
                    .AppendLine($"Country: {orderDetails.Country}")
                    .AppendLine($"City: {orderDetails.City}")
                    .AppendLine($"Customer's Email: {orderDetails.Email}")
                    .AppendLine("---")
                    .AppendFormat("Student Status Check: {0}", orderDetails.StudentCheck ? "Yes" : "No")
                    .AppendLine("")
                    .AppendLine("---")
                    .AppendLine("Contact the client via email in order to make a final confirmation regarding the delivery and payment");

                // For Customer 
                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    orderDetails.Email,
                    "Univerisity Materials Order Confirmation",
                    body.ToString()
                    );
                //For Admin
                MailMessage adminMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.adminAddress,
                    "University Materials Order Information",
                    body_2.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

            

              
                    
                    smtpClient.Send(mailMessage);
                    smtpClient.Send(adminMessage);
            
            }


            }

        }
    }

