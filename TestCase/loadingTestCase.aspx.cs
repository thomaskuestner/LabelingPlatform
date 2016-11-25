/*
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
*/
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LabelingFramework.Class;
using LabelingFramework.DataAccess;

namespace LabelingFramework
{
    public partial class loadingTestCase : System.Web.UI.Page
    {
       
        Thread task;
        Class.manageImg managerView;
		protected void Page_Load(object sender, EventArgs e)
		{
            Class.User user = Session["User"] as Class.User;
            // - Martin
            long idTestCase = 0;
            if (HttpContext.Current.Session["IdTestCase"] != null)
                idTestCase = Convert.ToInt64(HttpContext.Current.Session["IdTestCase"]);
            // infos die immer da sind
            Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase().Where(p => p.IDTestcase == idTestCase).FirstOrDefault();  /* original:  Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase(Convert.ToInt64(Session["IdTestCase"]));*/
            // initialize manage object with all needed content
            managerView = new manageImg(tc, user);

            // spawn a thread to create the image packages
            ThreadStart thread = new ThreadStart(managerView.ThreadRun);
            task = new Thread(thread);

           task.Start();

           if (task.IsAlive) // creation of everything finished => all necessary information are in Session["managerView"]
           {
               Session["managerView"] = managerView;
               Response.Redirect("~/managementImage/ViewImage_.aspx");
           }

		}



        protected void Timer_Tick(object sender, EventArgs e)
        {
            // - Martin !task.Alive -> task.Alive
            //if (task.IsAlive) // creation of everything finished => all necessary information are in Session["managerView"]
            //{
            //    Session["managerView"] = managerView;
            //    Response.Redirect("~/managementImage/ViewImage_.aspx");
            //}
        }
		
    }
}