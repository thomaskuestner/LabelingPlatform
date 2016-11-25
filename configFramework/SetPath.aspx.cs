/*
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using LabelingFramework.DataAccess;
using LabelingFramework.Class;

namespace LabelingFramework.managementImage
{
    public partial class SetPath : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["User"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    DivError.Visible = false;
                    BindPaths();
                }
            }
        
        }

        public void BindPaths()
        {
            gvPathLabeling.DataSource = DataAccess.DataAccessPath.getPaths();
            gvPathLabeling.DataBind();
        }

        public void BindGv(string path)
        {
            try
            {
                DirectoryInfo[] DirArray = null;
                DirectoryInfo dir = new DirectoryInfo(path);
                DirArray = dir.GetDirectories();
                List<Class.Directory> listdir = new List<Class.Directory>();

                foreach (DirectoryInfo d in DirArray)
                {
                    Class.Directory classdir = new Class.Directory();
                    classdir.NameDirectory = d.Name;

                    foreach (DirectoryInfo subdirectory in d.GetDirectories())
                    {
                        Class.Directory subdir = new Class.Directory();
                        subdir.NameDirectory = subdirectory.Name;
                        classdir.SubDirectory.Add(subdir);
                    }

                    listdir.Add(classdir);
                }


                gvDirectory.DataSource = listdir;
                gvDirectory.DataBind();
                DivError.Visible = false;
            }
            catch (Exception ex) { 
                DivError.InnerHtml = "Database Path is invalid"; 
                DivError.Visible = true;
                fDatabaseFolders.Visible = false;
            }


        }


        protected void gvPathLabeling_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id_path = Convert.ToInt32(gvPathLabeling.DataKeys[e.RowIndex].Value.ToString());
            DataAccess.DataAccessPath.DeletePath(id_path);
            BindPaths();
        }

        protected void gvPathLabeling_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id_path = Convert.ToInt32(gvPathLabeling.DataKeys[e.RowIndex].Value.ToString());
            Class.Path dbpath = DataAccess.DataAccessPath.getPathById(id_path);
            BindGv(dbpath.DatabasePath);

            lDatabaseFolders.InnerText = "Database: " + dbpath.DatabasePath;
            gvDirectory.Visible = true;
            //fDatabaseFolders.Visible = true;
        }


        protected void gvPathLabeling_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            // get id user my datakey selected in the gridview
            int id_path = Convert.ToInt32(gvPathLabeling.DataKeys[e.NewSelectedIndex].Value);
            hddIdPath.Value = id_path.ToString();
            Class.Path dbpath = DataAccess.DataAccessPath.getPathById(id_path);

            txtPathLabelingName.Text = dbpath.DatabasePath;
            
            fEditPath.Visible = true;
            DivSuccess.Visible = false;
            DivError.Visible = false;

            addPath.Visible = false;
            fDatabaseFolders.Visible = false;
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Result res = DataAccess.DataAccessPath.UpdatePath(Convert.ToInt32(hddIdPath.Value), txtPathLabelingName.Text);
            
            if (res.result)
            {
                lblSuccess.Text = "Update complete!";
                DivSuccess.Visible = true;
                DivError.Visible = false;
                fEditPath.Visible = false;
            }
            else
            {
                DivSuccess.Visible = false;
                DivError.Visible = true;
                lblError.Text = res.Message;

            }
            BindPaths();
            addPath.Visible = true;
            fDatabaseFolders.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Result res = DataAccess.DataAccessPath.InsertPath(txtPathLabeling.Text);
            if (res.result)
            {
                lblSuccess.Text = "Database added!";
                DivSuccess.Visible = true;
                DivError.Visible = false;
            }
            else
            {
                DivSuccess.Visible = false;
                DivError.Visible = true;
                lblError.Text = res.Message;
            }
            BindPaths();
            addPath.Visible = true;
            fDatabaseFolders.Visible = false;
            txtPathLabelingName.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DivSuccess.Visible = false;
            DivError.Visible = false;
            fEditPath.Visible = false;

            addPath.Visible = true;
            fDatabaseFolders.Visible = false;
        }

        
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            addPath.Visible = false;
            fEditPath.Visible = false;
            fDatabaseFolders.Visible = true;
        }
        
       
    }
}