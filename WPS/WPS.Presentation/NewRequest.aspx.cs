using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WPS.WF;
using WPS.WF.Model;

namespace WPS.Presentation
{
    public partial class NewRequest : System.Web.UI.Page
    {
        private Stateless _stateless = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var user = Request.Form.Get("username");
            if (!user.Equals(""))
            {
                _stateless = new Stateless(user, Request.Form.Get("emailaddress"),Request.Form.Get("ordertitle"), Request.Form.Get("orderdesc"));
                _stateless.Assign(user, "");
                Session["Stateless"] = _stateless;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Session["Stateless"] != null) _stateless = (Stateless)Session["Stateless"];
            if (_stateless == null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('You need to request a new element at first.');</script>");
                return;
            }
            var user = Request.Form.Get("designator");
            if (!user.Equals(""))
            {
                _stateless.Create(user, Request.Form.Get("designatiedcomment"));
            }
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            if (Session["Stateless"] != null) _stateless = (Stateless)Session["Stateless"];
            if (_stateless == null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('You need to request a new element at first.');</script>");
                return;
            }
            var user = Request.Form.Get("designator");
            if (!user.Equals(""))
            {
                _stateless.Review(user, Request.Form.Get("designatiedcomment"));
            }
        }


        protected void btnRejectFromDesignated_Click(object sender, EventArgs e)
        {
            if (Session["Stateless"] != null) _stateless = (Stateless)Session["Stateless"];
            if (_stateless == null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('You need to request a new element at first.');</script>");
                return;
            }
            var user = Request.Form.Get("designator");
            if (!user.Equals(""))
            {
                _stateless.Reject(user, Request.Form.Get("designatiedcomment"));
            }
        }


        protected void btnApproveFromReviewed_Click(object sender, EventArgs e)
        {
            if (Session["Stateless"] != null) _stateless = (Stateless)Session["Stateless"];
            if (_stateless == null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('You need to request a new element at first.');</script>");
                return;
            }
            var user = Request.Form.Get("revieweduser");
            if (!user.Equals(""))
            {
                _stateless.Approve(user, Request.Form.Get("reviewedcomment"));
            }
        }


        protected void btnRejectFromReviewed_Click(object sender, EventArgs e)
        {
            if (Session["Stateless"] != null) _stateless = (Stateless)Session["Stateless"];
            if (_stateless == null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('You need to request a new element at first.');</script>");
                return;
            }
            var user = Request.Form.Get("revieweduser");
            if (!user.Equals(""))
            {
                _stateless.Reject(user, Request.Form.Get("reviewedcomment"));
            }
        }


        protected void btnAssignFromApprove_Click(object sender, EventArgs e)
        {
            if (Session["Stateless"] != null) _stateless = (Stateless)Session["Stateless"];
            if (_stateless == null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "<script>alert('You need to request a new element at first.');</script>");
                return;
            }
            var user = Request.Form.Get("approveduser");
            if (!user.Equals(""))
            {
                _stateless.Assign(user, Request.Form.Get("approvedcomment"));
            }
        }

    }
}