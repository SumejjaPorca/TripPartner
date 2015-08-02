using System.Web;
using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

namespace TripPartner
{
    public class BundleConfig
    {
        
   
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bundle consisting of everything needed for the AngularJS app
           
            
 
            bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory(
                        "~/Scripts/app", "*.js", true));

               }
    }
}
