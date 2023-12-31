using System.ServiceModel; 
using System.Runtime.Serialization; 
using System.Net.Security; 
using System.ServiceModel.Web; 
using System.ServiceModel.Activation; 
using System.Transactions; 
using Sybase.PowerBuilder.WCFNVO; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[System.Diagnostics.DebuggerStepThrough]
	[ServiceContract(Name="n_cst_dbconnectservice",Namespace="http://tempurl.org")]
	public class n_cst_dbconnectservice : System.IDisposable 
	{
		internal c__n_cst_dbconnectservice __nvo__;
		private bool ____disposed____ = false;
		public void Dispose()
		{
			if (____disposed____)
				return;
			____disposed____ = true;
			c__pbservice125.InitSession(__nvo__.Session);
			Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(__nvo__);
			c__pbservice125.RestoreOldSession();
		}
		public n_cst_dbconnectservice()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (c__n_cst_dbconnectservice)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(c__n_cst_dbconnectservice));
			c__pbservice125.RestoreOldSession();
		}
		internal n_cst_dbconnectservice(c__n_cst_dbconnectservice nvo)
		{
			__nvo__ = nvo;
		}
		[OperationContract(Name="of_ondbconfig")]
		public virtual System.Int16 of_ondbconfig()
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_ondbconfig();
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_setconnectioninfo")]
		public virtual System.Int16 of_setconnectioninfo(string as_dbms, string as_database, string as_userid, string as_dbpass, string as_logid, string as_logpass, string as_server, string as_dbparm, string as_lock, bool ab_autocommit)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_dbms__temp__;
			as_dbms__temp__ = new Sybase.PowerBuilder.PBString((string)as_dbms);
			Sybase.PowerBuilder.PBString as_database__temp__;
			as_database__temp__ = new Sybase.PowerBuilder.PBString((string)as_database);
			Sybase.PowerBuilder.PBString as_userid__temp__;
			as_userid__temp__ = new Sybase.PowerBuilder.PBString((string)as_userid);
			Sybase.PowerBuilder.PBString as_dbpass__temp__;
			as_dbpass__temp__ = new Sybase.PowerBuilder.PBString((string)as_dbpass);
			Sybase.PowerBuilder.PBString as_logid__temp__;
			as_logid__temp__ = new Sybase.PowerBuilder.PBString((string)as_logid);
			Sybase.PowerBuilder.PBString as_logpass__temp__;
			as_logpass__temp__ = new Sybase.PowerBuilder.PBString((string)as_logpass);
			Sybase.PowerBuilder.PBString as_server__temp__;
			as_server__temp__ = new Sybase.PowerBuilder.PBString((string)as_server);
			Sybase.PowerBuilder.PBString as_dbparm__temp__;
			as_dbparm__temp__ = new Sybase.PowerBuilder.PBString((string)as_dbparm);
			Sybase.PowerBuilder.PBString as_lock__temp__;
			as_lock__temp__ = new Sybase.PowerBuilder.PBString((string)as_lock);
			Sybase.PowerBuilder.PBBoolean ab_autocommit__temp__;
			ab_autocommit__temp__ = new Sybase.PowerBuilder.PBBoolean((bool)ab_autocommit);
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_setconnectioninfo(as_dbms__temp__, as_database__temp__, as_userid__temp__, as_dbpass__temp__, as_logid__temp__, as_logpass__temp__, as_server__temp__, as_dbparm__temp__, as_lock__temp__, ab_autocommit__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getconnectioninfo")]
		public virtual System.Int16 of_getconnectioninfo(ref string as_dbms, ref string as_database, ref string as_userid, ref string as_dbpass, ref string as_logid, ref string as_logpass, ref string as_server, ref string as_dbparm, ref string as_lock, ref string as_autocommit)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_dbms__temp__ = as_dbms;
			Sybase.PowerBuilder.PBString as_database__temp__ = as_database;
			Sybase.PowerBuilder.PBString as_userid__temp__ = as_userid;
			Sybase.PowerBuilder.PBString as_dbpass__temp__ = as_dbpass;
			Sybase.PowerBuilder.PBString as_logid__temp__ = as_logid;
			Sybase.PowerBuilder.PBString as_logpass__temp__ = as_logpass;
			Sybase.PowerBuilder.PBString as_server__temp__ = as_server;
			Sybase.PowerBuilder.PBString as_dbparm__temp__ = as_dbparm;
			Sybase.PowerBuilder.PBString as_lock__temp__ = as_lock;
			Sybase.PowerBuilder.PBString as_autocommit__temp__ = as_autocommit;
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_getconnectioninfo(ref as_dbms__temp__, ref as_database__temp__, ref as_userid__temp__, ref as_dbpass__temp__, ref as_logid__temp__, ref as_logpass__temp__, ref as_server__temp__, ref as_dbparm__temp__, ref as_lock__temp__, ref as_autocommit__temp__);
			as_dbms = as_dbms__temp__;
			as_database = as_database__temp__;
			as_userid = as_userid__temp__;
			as_dbpass = as_dbpass__temp__;
			as_logid = as_logid__temp__;
			as_logpass = as_logpass__temp__;
			as_server = as_server__temp__;
			as_dbparm = as_dbparm__temp__;
			as_lock = as_lock__temp__;
			as_autocommit = as_autocommit__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_initconnection")]
		public virtual System.Int16 of_initconnection()
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_initconnection();
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getxml")]
		public virtual string of_getxml()
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_getxml();
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_disconnectdb")]
		public virtual System.Int16 of_disconnectdb()
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_disconnectdb();
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_connectdb")]
		public virtual System.Int16 of_connectdb()
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_connectdb();
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getelement")]
		public virtual string of_getelement(string as_connectionstring, string as_element_name)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_connectionstring__temp__;
			as_connectionstring__temp__ = new Sybase.PowerBuilder.PBString((string)as_connectionstring);
			Sybase.PowerBuilder.PBString as_element_name__temp__;
			as_element_name__temp__ = new Sybase.PowerBuilder.PBString((string)as_element_name);
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_getelement(as_connectionstring__temp__, as_element_name__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_connectdb_1")]
		public virtual System.Int16 of_connectdb_1(string as_connectionstring, string as_coopid, string as_coopcontrol, string as_gcoop_path)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_connectionstring__temp__;
			as_connectionstring__temp__ = new Sybase.PowerBuilder.PBString((string)as_connectionstring);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_coopcontrol__temp__;
			as_coopcontrol__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopcontrol);
			Sybase.PowerBuilder.PBString as_gcoop_path__temp__;
			as_gcoop_path__temp__ = new Sybase.PowerBuilder.PBString((string)as_gcoop_path);
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_connectdb(as_connectionstring__temp__, as_coopid__temp__, as_coopcontrol__temp__, as_gcoop_path__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_connectdb_2")]
		public virtual System.Int16 of_connectdb_2(string as_wspass)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_connectdb(as_wspass__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_password")]
		public virtual string of_password(string as_data_source, string as_user_id)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_data_source__temp__;
			as_data_source__temp__ = new Sybase.PowerBuilder.PBString((string)as_data_source);
			Sybase.PowerBuilder.PBString as_user_id__temp__;
			as_user_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_user_id);
			__retval__ = ((c__n_cst_dbconnectservice)__nvo__).of_password(as_data_source__temp__, as_user_id__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 