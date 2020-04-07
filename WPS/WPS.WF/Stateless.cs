using Stateless;
using System;
using System.Collections.Generic;
using WPS.Presentation;
using WPS.WF.Model;

namespace WPS.WF
{
    public class Stateless
    {
        enum Trigger
        {
            Assign,
            Create,
            Reject,
            Review,
            Approve
        }

        enum State
        {
            New,
            Designated,
            Completed,
            Rejected,
            Approved,
            Reviewed,
        }

        State _state = State.New;
        StateMachine<State, Trigger> _machine;
        StateMachine<State, Trigger>.TriggerWithParameters<int, string> _assignTrigger;
        StateMachine<State, Trigger>.TriggerWithParameters<int, string> _createTrigger;
        StateMachine<State, Trigger>.TriggerWithParameters<int, string> _rejectTrigger;
        StateMachine<State, Trigger>.TriggerWithParameters<int, string> _reviewTrigger;
        StateMachine<State, Trigger>.TriggerWithParameters<int, string> _approveTrigger;
        public WorkElement _element;//a request for creating a service order or work element release
        public Stateless(string requestedBy, string requestedMail, string orderTitle, string orderDesc)
        {
            int eid;
            var nowdate = DateTime.Now;
            int id = DBconn.insertAgent(requestedBy, requestedMail);
            var res = DBconn.excuteQuery($"insert into element (ordertitle,orderdesc,requestedby,laststate,requestdate,terminationdate) output Inserted.id values('{orderTitle}','{orderDesc}','{id}','{_state}','{nowdate}',null);");
            if (!int.TryParse(res.ToString(), out eid)) eid = 0;
            _element = new WorkElement
            {
                Id = eid,
                OrderTitle = orderTitle,
                OrderDescription = orderDesc,
                RequestedBy=requestedBy,
                EmailAddress=requestedMail,
                RequestDate = nowdate,
                Transition = new List<Transition>()
            };

            ConfigStateless();
        }

        public Stateless()
        {
            string orderTitle = "NewWorkElement"+DateTime.Now, orderDesc = "";
            int eid;
            var nowdate = DateTime.Now;
            var res = DBconn.excuteQuery($"insert into element (ordertitle,orderdesc,laststate,requestdate,terminationdate) output Inserted.id values('','','{_state}','{nowdate}',null);");
            if (!int.TryParse(res.ToString(), out eid)) eid = 0;
            _element = new WorkElement
            {
                Id = eid,
                OrderTitle = orderTitle,
                OrderDescription = orderDesc,
                RequestDate = nowdate,
                Transition = new List<Transition>()
            };

            ConfigStateless();
        }

        public Stateless(int eid)
        {
            List<WorkElement> cElement = DBconn.GetStateless(eid);
            if (cElement.Count == 0)
            {
                string orderTitle = "NewWorkElement" + DateTime.Now, orderDesc = "";
                var nowdate = DateTime.Now;
                var res = DBconn.excuteQuery($"insert into element (ordertitle,orderdesc,laststate,requestdate,terminationdate) output Inserted.id values('','','{_state}','{nowdate}',null);");
                if (!int.TryParse(res.ToString(), out eid)) eid = 0;
                _element = new WorkElement
                {
                    Id = eid,
                    OrderTitle = orderTitle,
                    RequestedBy="",
                    EmailAddress="",
                    OrderDescription = orderDesc,
                    RequestDate = nowdate,
                    Transition = new List<Transition>()
                };
            }
            else {
                _element = new WorkElement
                {
                    Id = eid,
                    OrderTitle = cElement[0].OrderTitle,
                    RequestedBy=cElement[0].RequestedBy,
                    EmailAddress=cElement[0].EmailAddress,
                    OrderDescription = cElement[0].OrderDescription,
                    RequestDate = cElement[0].RequestDate,
                    Transition = cElement[0].Transition
                };
            }
            ConfigStateless();
        }

        private void ConfigStateless() {
            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);
            _assignTrigger = _machine.SetTriggerParameters<int, string>(Trigger.Assign);
            _createTrigger = _machine.SetTriggerParameters<int, string>(Trigger.Create);
            _rejectTrigger = _machine.SetTriggerParameters<int, string>(Trigger.Reject);
            _reviewTrigger = _machine.SetTriggerParameters<int, string>(Trigger.Review);
            _approveTrigger = _machine.SetTriggerParameters<int, string>(Trigger.Approve);

            _machine.Configure(State.New)
                .Permit(Trigger.Assign, State.Designated);

            _machine.Configure(State.Designated)
                .Permit(Trigger.Create, State.Completed)
                .Permit(Trigger.Review, State.Reviewed)
                .Permit(Trigger.Reject, State.Rejected);

            _machine.Configure(State.Reviewed)
                .Permit(Trigger.Approve, State.Approved)
                .Permit(Trigger.Reject, State.Rejected);

            _machine.Configure(State.Approved)
                .Permit(Trigger.Assign, State.Designated);

            _machine.OnTransitioned(
                t =>
                {
                    var d = DateTime.Now;
                    DBconn.excuteQuery($"insert into statelog (elementid,triggerid,triggeredby,triggereddate,sourcestate,destinationstate,comment) output Inserted.id values('{_element.Id}','{t.Trigger}','" + (t.Parameters.Length > 0 ? t.Parameters[0] : "0") + "','" + d + "','" + t.Source + "','" + t.Destination + "','" + (t.Parameters.Length > 1 ? t.Parameters[1] : "") + "');");
                    DBconn.excuteQuery($"update element set laststate='{t.Destination}',terminationdate='{d}' where id='{_element.Id}';");
                    Console.WriteLine(
                        $"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}({string.Join(", ", t.Parameters)})"
                    );
                }
            );
        }
        public void Assign(string requestBy,string comment)
        {
            int id = DBconn.insertAgent(requestBy);
            _machine.Fire(_assignTrigger, id, comment);
        }

        public void Create(string createBy, string comment)
        {
            int id = DBconn.insertAgent(createBy);
            if (_machine.State == State.Designated)
                _machine.Fire(_createTrigger, id, comment);
        }

        public void Reject(string rejectBy, string comment)
        {
            int id = DBconn.insertAgent(rejectBy);
            if (_machine.State == State.Designated || _machine.State == State.Reviewed)
            {
                _machine.Fire(_rejectTrigger, id, comment);//This is reject action.//arup.mondal@arupmathnasium.com
                //Sorry, your request is rejected.
                EmailUtil.SendEmail("Sorry, this is only testing.", comment, _element.EmailAddress);
            }
        }

        public void Review(string reviewBy, string comment)
        {
            int id = DBconn.insertAgent(reviewBy);
            if(_machine.State == State.Designated)
                _machine.Fire(_reviewTrigger, id, comment);
        }

        public void Approve(string approveBy, string comment)
        {
            int id = DBconn.insertAgent(approveBy);
            if (_machine.State == State.Reviewed)
                _machine.Fire(_approveTrigger, id, comment);
        }

        public int GetElementId() {
            return _element.Id;
        }
    }
}
