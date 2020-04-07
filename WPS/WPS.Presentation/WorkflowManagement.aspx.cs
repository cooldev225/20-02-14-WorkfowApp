using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WPS.WF;
using WPS.WF.Model;

namespace WPS.Presentation
{
    public partial class WorkflowManagement : System.Web.UI.Page
    {
        public int _stateless_id = -1;
        public List<Transition> _transitions=new List<Transition>();
        public List<WorkElement> statelesses;
        protected void Page_Load(object sender, EventArgs e)
        {
            statelesses = DBconn.GetStateless(-1);
            if (Session["_stateless_id"] != null)//if (ViewState["_stateless_id"] != null)
            {
                if (!int.TryParse(Session["_stateless_id"].ToString(), out _stateless_id)) _stateless_id = -1;
            }else if (Session["Stateless"] != null)
            {
                Stateless _stateless = (Stateless)Session["Stateless"];
                _stateless_id=_stateless.GetElementId();
            }else if (statelesses.Count > 0) _stateless_id = statelesses[0].Id;
            Select1.Items.Clear();
            Select1.Items.Add(new ListItem(" --ALL-- ", "-1"));
            foreach (WorkElement we in statelesses) {
                if (we.Id == _stateless_id)
                {
                    _transitions = we.Transition;
                    //break;
                    Select1.Items.Add(new ListItem(we.OrderTitle.ToString(),we.Id.ToString(),true));
                }
                else {
                    Select1.Items.Add(new ListItem(we.OrderTitle.ToString(),we.Id.ToString()));
                }
            }
            Select1.SelectedValue = _stateless_id.ToString();
            var sss = Select1.SelectedValue;
        }
        protected void Server_Change(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Form.Get("element_select"), out _stateless_id)) _stateless_id = -1;
            ViewState["_stateless_id"] = _stateless_id;
        }

        protected void Select1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!int.TryParse(Select1.SelectedValue, out _stateless_id)) _stateless_id = -1;
            if (!int.TryParse(Request.Form.Get("ctl00$MainContent$Select1"), out _stateless_id)) _stateless_id = -1;
            Session["_stateless_id"] = _stateless_id;
            Page.Response.Redirect(Page.Request.Url.ToString(), false);
        }
    }
}