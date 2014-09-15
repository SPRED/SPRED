using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Net;
using System.Security;

namespace CSOM_CA
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientContext context = new ClientContext("http://sp.weiyun.com/sites/Doc");
            context.AuthenticationMode = ClientAuthenticationMode.FormsAuthentication;
            FormsAuthenticationLoginInfo formsAuthinfo = new FormsAuthenticationLoginInfo("DEL00001", "1234!qwer");
            context.FormsAuthenticationLoginInfo = formsAuthinfo;
            //context.Credentials = new NetworkCredential("DEL00001", "1234!qwer");
            Web web = context.Web;
            List list = web.Lists.GetByTitle("DocLib");
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml =
                @"<View>
                    <Query>
                        <Where>
                            <Eq>
                                <FieldRef Name = 'Title' />
                                <Value Type='Text'>71342245-c3c8-4094-a555-842a6763b201_081464afff6f457eac23e2168c82a974</Value>
                            </Eq>
                        </Where>
                    </Query>
                </View>";
            ListItemCollection items = list.GetItems(camlQuery);
            //context.Load(web,w=>w.Title,w=>w.Description);
            context.Load(items, s => s.Include(item => item["Title"]));
            //context.LoadQuery
            context.ExecuteQuery();
            //string Title = web.Title;
           //Console.WriteLine(string.Format("Web Title is {0}, Descript is {1}!",Title,web.Description));
            int i = items.Count;
            foreach (ListItem item in items)
            {
                Console.WriteLine("Title:{0}",item["Title"]);
            }
        }
    }
}
