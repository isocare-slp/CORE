using System.ServiceModel; 
using System.Runtime.Serialization; 
using System.Net.Security; 
using System.ServiceModel.Web; 
using System.ServiceModel.Activation; 
using System.Transactions; 
using Sybase.PowerBuilder.WCFNVO; 
namespace pbservice125
{
	[System.Diagnostics.DebuggerStepThrough]
	[ServiceContract(Name="n_finance",Namespace="http://tempurl.org")]
	public class n_finance : System.IDisposable 
	{
		internal pbservice125.c__n_finance __nvo__;
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
		public n_finance()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (pbservice125.c__n_finance)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(pbservice125.c__n_finance));
			c__pbservice125.RestoreOldSession();
		}
		internal n_finance(pbservice125.c__n_finance nvo)
		{
			__nvo__ = nvo;
		}
		[OperationContract(Name="of_test")]
		public virtual string of_test(string as_test)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_test__temp__;
			as_test__temp__ = new Sybase.PowerBuilder.PBString((string)as_test);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_test(as_test__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postprocessotherto_fin")]
		public virtual System.Int16 of_postprocessotherto_fin(string as_wspass, string as_coopid, string as_entry_id, System.DateTime adtm_wdate, string as_machineid, string as_procxml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machineid__temp__;
			as_machineid__temp__ = new Sybase.PowerBuilder.PBString((string)as_machineid);
			Sybase.PowerBuilder.PBString as_procxml__temp__;
			as_procxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_procxml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postprocessotherto_fin(as_wspass__temp__, as_coopid__temp__, as_entry_id__temp__, adtm_wdate__temp__, as_machineid__temp__, as_procxml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_fincashcontrol_user")]
		public virtual System.Int16 of_init_fincashcontrol_user(string as_wspass, ref string as_fincashcontrol_xml, ref string as_fullname)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_fincashcontrol_xml__temp__ = as_fincashcontrol_xml;
			Sybase.PowerBuilder.PBString as_fullname__temp__ = as_fullname;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_fincashcontrol_user(as_wspass__temp__, ref as_fincashcontrol_xml__temp__, ref as_fullname__temp__);
			as_fincashcontrol_xml = as_fincashcontrol_xml__temp__;
			as_fullname = as_fullname__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_finquery")]
		public virtual System.Int16 of_finquery(string as_wspass, string as_appname, string as_user_xml, ref string as_userdetail_xml, ref string as_recv_xml, ref string as_pay_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			Sybase.PowerBuilder.PBString as_user_xml__temp__;
			as_user_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_user_xml);
			Sybase.PowerBuilder.PBString as_userdetail_xml__temp__ = as_userdetail_xml;
			Sybase.PowerBuilder.PBString as_recv_xml__temp__ = as_recv_xml;
			Sybase.PowerBuilder.PBString as_pay_xml__temp__ = as_pay_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_finquery(as_wspass__temp__, as_appname__temp__, as_user_xml__temp__, ref as_userdetail_xml__temp__, ref as_recv_xml__temp__, ref as_pay_xml__temp__);
			as_userdetail_xml = as_userdetail_xml__temp__;
			as_recv_xml = as_recv_xml__temp__;
			as_pay_xml = as_pay_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_close_day")]
		public virtual System.Int16 of_init_close_day(string as_wspass, string as_coopid, string as_entry_id, System.DateTime adtm_wdate, string as_appname, ref string as_closeday_xml, ref string as_chqwait_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			Sybase.PowerBuilder.PBString as_closeday_xml__temp__ = as_closeday_xml;
			Sybase.PowerBuilder.PBString as_chqwait_xml__temp__ = as_chqwait_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_close_day(as_wspass__temp__, as_coopid__temp__, as_entry_id__temp__, adtm_wdate__temp__, as_appname__temp__, ref as_closeday_xml__temp__, ref as_chqwait_xml__temp__);
			as_closeday_xml = as_closeday_xml__temp__;
			as_chqwait_xml = as_chqwait_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_payrecv_slip")]
		public virtual System.Int16 of_init_payrecv_slip(string wspass, string coopid, string entryid, string machineid, System.DateTime workdate, System.Int16 recv_pay_status)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString wspass__temp__;
			wspass__temp__ = new Sybase.PowerBuilder.PBString((string)wspass);
			Sybase.PowerBuilder.PBString coopid__temp__;
			coopid__temp__ = new Sybase.PowerBuilder.PBString((string)coopid);
			Sybase.PowerBuilder.PBString entryid__temp__;
			entryid__temp__ = new Sybase.PowerBuilder.PBString((string)entryid);
			Sybase.PowerBuilder.PBString machineid__temp__;
			machineid__temp__ = new Sybase.PowerBuilder.PBString((string)machineid);
			Sybase.PowerBuilder.PBDateTime workdate__temp__;
			workdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)workdate);
			Sybase.PowerBuilder.PBInt recv_pay_status__temp__;
			recv_pay_status__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)recv_pay_status);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_payrecv_slip(wspass__temp__, coopid__temp__, entryid__temp__, machineid__temp__, workdate__temp__, recv_pay_status__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_fincontact")]
		public virtual System.Int16 of_init_fincontact(string as_wspass, ref string as_contact_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_contact_xml__temp__ = as_contact_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_fincontact(as_wspass__temp__, ref as_contact_xml__temp__);
			as_contact_xml = as_contact_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_sendchq")]
		public virtual System.Int16 of_init_sendchq(string wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString wspass__temp__;
			wspass__temp__ = new Sybase.PowerBuilder.PBString((string)wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_sendchq(wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievecancel_sendchq")]
		public virtual System.Int16 of_retrievecancel_sendchq(string as_wspass, string as_coopid, System.DateTime adtm_wdate, ref string as_bank_xml, string as_cancellist)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_bank_xml__temp__ = as_bank_xml;
			Sybase.PowerBuilder.PBString as_cancellist__temp__;
			as_cancellist__temp__ = new Sybase.PowerBuilder.PBString((string)as_cancellist);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievecancel_sendchq(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, ref as_bank_xml__temp__, as_cancellist__temp__);
			as_bank_xml = as_bank_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_caltax")]
		public virtual System.Int16 of_caltax(string as_wspass, ref string as_main_xml, ref string as_taxdet_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__ = as_main_xml;
			Sybase.PowerBuilder.PBString as_taxdet_xml__temp__ = as_taxdet_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_caltax(as_wspass__temp__, ref as_main_xml__temp__, ref as_taxdet_xml__temp__);
			as_main_xml = as_main_xml__temp__;
			as_taxdet_xml = as_taxdet_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_close_day")]
		public virtual System.Int16 of_close_day(string as_wspass, string as_appname, string as_closeday_xml, string as_chqwait_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			Sybase.PowerBuilder.PBString as_closeday_xml__temp__;
			as_closeday_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_closeday_xml);
			Sybase.PowerBuilder.PBString as_chqwait_xml__temp__;
			as_chqwait_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqwait_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_close_day(as_wspass__temp__, as_appname__temp__, as_closeday_xml__temp__, as_chqwait_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_defaultaccid")]
		public virtual string of_defaultaccid(string as_wspass, string as_moneytype)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_moneytype__temp__;
			as_moneytype__temp__ = new Sybase.PowerBuilder.PBString((string)as_moneytype);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_defaultaccid(as_wspass__temp__, as_moneytype__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_fincashcontrol_process")]
		public virtual System.Int16 of_fincashcontrol_process(string as_wspass, string as_fincashcontrol_xml, string as_machined, string as_appname)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_fincashcontrol_xml__temp__;
			as_fincashcontrol_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_fincashcontrol_xml);
			Sybase.PowerBuilder.PBString as_machined__temp__;
			as_machined__temp__ = new Sybase.PowerBuilder.PBString((string)as_machined);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_fincashcontrol_process(as_wspass__temp__, as_fincashcontrol_xml__temp__, as_machined__temp__, as_appname__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getaddress")]
		public virtual System.Int16 of_getaddress(string as_wspass, ref string as_taxaddr, ref string as_taxid, string as_coopid, string as_memberno, System.Int16 ai_memberflag)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_taxaddr__temp__ = as_taxaddr;
			Sybase.PowerBuilder.PBString as_taxid__temp__ = as_taxid;
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_memberno__temp__;
			as_memberno__temp__ = new Sybase.PowerBuilder.PBString((string)as_memberno);
			Sybase.PowerBuilder.PBInt ai_memberflag__temp__;
			ai_memberflag__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_memberflag);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_getaddress(as_wspass__temp__, ref as_taxaddr__temp__, ref as_taxid__temp__, as_coopid__temp__, as_memberno__temp__, ai_memberflag__temp__);
			as_taxaddr = as_taxaddr__temp__;
			as_taxid = as_taxid__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_bankaccount_slip")]
		public virtual System.Int16 of_init_bankaccount_slip(string as_wspass, ref string as_main_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__ = as_main_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_bankaccount_slip(as_wspass__temp__, ref as_main_xml__temp__);
			as_main_xml = as_main_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_chq_bookno")]
		public virtual System.Int16 of_init_chq_bookno(string as_wspass, ref string as_chqbook_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_chqbook_xml__temp__ = as_chqbook_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_chq_bookno(as_wspass__temp__, ref as_chqbook_xml__temp__);
			as_chqbook_xml = as_chqbook_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_chqlistfrom_slip")]
		public virtual System.Int16 of_init_chqlistfrom_slip(string as_wspass, string as_coopid, System.DateTime adtm_wdate, ref string as_chqcond_xml, ref string as_cutbank_xml, ref string as_chqtype_xml, ref string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_chqcond_xml__temp__ = as_chqcond_xml;
			Sybase.PowerBuilder.PBString as_cutbank_xml__temp__ = as_cutbank_xml;
			Sybase.PowerBuilder.PBString as_chqtype_xml__temp__ = as_chqtype_xml;
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_chqlistfrom_slip(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, ref as_chqcond_xml__temp__, ref as_cutbank_xml__temp__, ref as_chqtype_xml__temp__, ref as_chqlist_xml__temp__);
			as_chqcond_xml = as_chqcond_xml__temp__;
			as_cutbank_xml = as_cutbank_xml__temp__;
			as_chqtype_xml = as_chqtype_xml__temp__;
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_chqnoandbank")]
		public virtual System.Int16 of_init_chqnoandbank(string as_wspass, string as_coopid, string as_bank, string as_bankbranch, string as_chqbookno, ref string as_accno, ref string as_startchqno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_bank__temp__;
			as_bank__temp__ = new Sybase.PowerBuilder.PBString((string)as_bank);
			Sybase.PowerBuilder.PBString as_bankbranch__temp__;
			as_bankbranch__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankbranch);
			Sybase.PowerBuilder.PBString as_chqbookno__temp__;
			as_chqbookno__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqbookno);
			Sybase.PowerBuilder.PBString as_accno__temp__ = as_accno;
			Sybase.PowerBuilder.PBString as_startchqno__temp__ = as_startchqno;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_chqnoandbank(as_wspass__temp__, as_coopid__temp__, as_bank__temp__, as_bankbranch__temp__, as_chqbookno__temp__, ref as_accno__temp__, ref as_startchqno__temp__);
			as_accno = as_accno__temp__;
			as_startchqno = as_startchqno__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_fincashcontrol")]
		public virtual System.Int16 of_init_fincashcontrol(string as_wspass, string as_coop_id, System.DateTime adtm_wdate, string as_permis_id, ref string as_fincashctl_info)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_permis_id__temp__;
			as_permis_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_permis_id);
			Sybase.PowerBuilder.PBString as_fincashctl_info__temp__ = as_fincashctl_info;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_fincashcontrol(as_wspass__temp__, as_coop_id__temp__, adtm_wdate__temp__, as_permis_id__temp__, ref as_fincashctl_info__temp__);
			as_fincashctl_info = as_fincashctl_info__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_openday")]
		public virtual System.Int16 of_init_openday(string as_wspass, string as_coop_id, string as_entry_id, System.DateTime adtm_wdate, string as_machine_id, ref string as_startday_info, ref string as_errmessage)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine_id__temp__;
			as_machine_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine_id);
			Sybase.PowerBuilder.PBString as_startday_info__temp__ = as_startday_info;
			Sybase.PowerBuilder.PBString as_errmessage__temp__ = as_errmessage;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_openday(as_wspass__temp__, as_coop_id__temp__, as_entry_id__temp__, adtm_wdate__temp__, as_machine_id__temp__, ref as_startday_info__temp__, ref as_errmessage__temp__);
			as_startday_info = as_startday_info__temp__;
			as_errmessage = as_errmessage__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_paychq")]
		public virtual System.Int16 of_init_paychq(string as_wspass, string as_coopid, System.DateTime adtm_wdate, ref string as_main_xml, ref string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_main_xml__temp__ = as_main_xml;
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_paychq(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, ref as_main_xml__temp__, ref as_chqlist_xml__temp__);
			as_main_xml = as_main_xml__temp__;
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_paychq_apvloancbt")]
		public virtual System.Int16 of_init_paychq_apvloancbt(string as_wspass, string as_coopid, System.DateTime adtm_wdate, ref string as_main_xml, ref string as_chqlist_xml, string as_cashtype)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_main_xml__temp__ = as_main_xml;
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			Sybase.PowerBuilder.PBString as_cashtype__temp__;
			as_cashtype__temp__ = new Sybase.PowerBuilder.PBString((string)as_cashtype);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_paychq_apvloancbt(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, ref as_main_xml__temp__, ref as_chqlist_xml__temp__, as_cashtype__temp__);
			as_main_xml = as_main_xml__temp__;
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_paychq_manual")]
		public virtual System.Int16 of_init_paychq_manual(string as_wspass, string as_coopid, System.DateTime adtm_wdate, ref string as_main_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_main_xml__temp__ = as_main_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_paychq_manual(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, ref as_main_xml__temp__);
			as_main_xml = as_main_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_paychq_split")]
		public virtual System.Int16 of_init_paychq_split(string as_wspass, string as_branch, System.DateTime adtm_wdate, ref string as_chqcond_xml, ref string as_cutbank_xml, ref string as_chqtype_xml, ref string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_branch__temp__;
			as_branch__temp__ = new Sybase.PowerBuilder.PBString((string)as_branch);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_chqcond_xml__temp__ = as_chqcond_xml;
			Sybase.PowerBuilder.PBString as_cutbank_xml__temp__ = as_cutbank_xml;
			Sybase.PowerBuilder.PBString as_chqtype_xml__temp__ = as_chqtype_xml;
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_paychq_split(as_wspass__temp__, as_branch__temp__, adtm_wdate__temp__, ref as_chqcond_xml__temp__, ref as_cutbank_xml__temp__, ref as_chqtype_xml__temp__, ref as_chqlist_xml__temp__);
			as_chqcond_xml = as_chqcond_xml__temp__;
			as_cutbank_xml = as_cutbank_xml__temp__;
			as_chqtype_xml = as_chqtype_xml__temp__;
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_payrecv_member")]
		public virtual System.Int16 of_init_payrecv_member(string as_wspass, ref string as_main_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__ = as_main_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_payrecv_member(as_wspass__temp__, ref as_main_xml__temp__);
			as_main_xml = as_main_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_payrecv_slip_1")]
		public virtual System.Int16 of_init_payrecv_slip_1(string as_wspass, string as_coop_id, string as_entry_id, string as_machine, System.DateTime adtm_wdate, System.Int16 as_recvpay_status, ref string as_slipmain_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBInt as_recvpay_status__temp__;
			as_recvpay_status__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)as_recvpay_status);
			Sybase.PowerBuilder.PBString as_slipmain_xml__temp__ = as_slipmain_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_payrecv_slip(as_wspass__temp__, as_coop_id__temp__, as_entry_id__temp__, as_machine__temp__, adtm_wdate__temp__, as_recvpay_status__temp__, ref as_slipmain_xml__temp__);
			as_slipmain_xml = as_slipmain_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_payrecv_slipcfm")]
		public virtual System.Int16 of_init_payrecv_slipcfm(string as_wspass, string as_coopid, string as_slipno, string as_entryid, string as_machine, System.DateTime adtm_wdate, ref string as_xmlfinslip, ref string as_xmlfinslipdet)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_slipno__temp__;
			as_slipno__temp__ = new Sybase.PowerBuilder.PBString((string)as_slipno);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_xmlfinslip__temp__ = as_xmlfinslip;
			Sybase.PowerBuilder.PBString as_xmlfinslipdet__temp__ = as_xmlfinslipdet;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_payrecv_slipcfm(as_wspass__temp__, as_coopid__temp__, as_slipno__temp__, as_entryid__temp__, as_machine__temp__, adtm_wdate__temp__, ref as_xmlfinslip__temp__, ref as_xmlfinslipdet__temp__);
			as_xmlfinslip = as_xmlfinslip__temp__;
			as_xmlfinslipdet = as_xmlfinslipdet__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_payrecv_slipdlg")]
		public virtual System.Int16 of_init_payrecv_slipdlg(string as_wspass, string as_coopid, string as_slipno, string as_entryid, string as_machine, System.DateTime adtm_wdate, ref string as_xmlfinslip, ref string as_xmlfinslipdet)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_slipno__temp__;
			as_slipno__temp__ = new Sybase.PowerBuilder.PBString((string)as_slipno);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_xmlfinslip__temp__ = as_xmlfinslip;
			Sybase.PowerBuilder.PBString as_xmlfinslipdet__temp__ = as_xmlfinslipdet;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_payrecv_slipdlg(as_wspass__temp__, as_coopid__temp__, as_slipno__temp__, as_entryid__temp__, as_machine__temp__, adtm_wdate__temp__, ref as_xmlfinslip__temp__, ref as_xmlfinslipdet__temp__);
			as_xmlfinslip = as_xmlfinslip__temp__;
			as_xmlfinslipdet = as_xmlfinslipdet__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_postotherto_fin")]
		public virtual System.Int16 of_init_postotherto_fin(string as_wspass, ref string as_memb_xml, ref string as_slipmain_xml, ref string as_slipcancel_xml, string as_appname)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_memb_xml__temp__ = as_memb_xml;
			Sybase.PowerBuilder.PBString as_slipmain_xml__temp__ = as_slipmain_xml;
			Sybase.PowerBuilder.PBString as_slipcancel_xml__temp__ = as_slipcancel_xml;
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_postotherto_fin(as_wspass__temp__, ref as_memb_xml__temp__, ref as_slipmain_xml__temp__, ref as_slipcancel_xml__temp__, as_appname__temp__);
			as_memb_xml = as_memb_xml__temp__;
			as_slipmain_xml = as_slipmain_xml__temp__;
			as_slipcancel_xml = as_slipcancel_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_posttobank")]
		public virtual System.Int16 of_init_posttobank(string as_wspass, string as_branch, System.DateTime adtm_wdate, ref string as_xmlinfo)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_branch__temp__;
			as_branch__temp__ = new Sybase.PowerBuilder.PBString((string)as_branch);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_xmlinfo__temp__ = as_xmlinfo;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_posttobank(as_wspass__temp__, as_branch__temp__, adtm_wdate__temp__, ref as_xmlinfo__temp__);
			as_xmlinfo = as_xmlinfo__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_printfinstatus")]
		public virtual string of_init_printfinstatus(string as_wspass, string as_branch, System.DateTime adtm_wdate)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_branch__temp__;
			as_branch__temp__ = new Sybase.PowerBuilder.PBString((string)as_branch);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_printfinstatus(as_wspass__temp__, as_branch__temp__, adtm_wdate__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_sendchq_1")]
		public virtual System.Int16 of_init_sendchq_1(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, ref string as_sendchq_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_sendchq_xml__temp__ = as_sendchq_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_sendchq(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, ref as_sendchq_xml__temp__);
			as_sendchq_xml = as_sendchq_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_itemcaltax")]
		public virtual System.Int16 of_itemcaltax(string as_wspass, string as_coopid, System.Int16 ai_recv_pay, System.Int16 ai_calvat, System.Int16 ai_taxcode, decimal adc_itemamt, ref decimal adc_taxamt, ref decimal adc_itemamt_net, ref decimal adc_vatamt)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBInt ai_recv_pay__temp__;
			ai_recv_pay__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_recv_pay);
			Sybase.PowerBuilder.PBInt ai_calvat__temp__;
			ai_calvat__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_calvat);
			Sybase.PowerBuilder.PBInt ai_taxcode__temp__;
			ai_taxcode__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_taxcode);
			Sybase.PowerBuilder.PBDecimal adc_itemamt__temp__;
			adc_itemamt__temp__ = new Sybase.PowerBuilder.PBDecimal((decimal)adc_itemamt);
			Sybase.PowerBuilder.PBDecimal adc_taxamt__temp__ = adc_taxamt;
			Sybase.PowerBuilder.PBDecimal adc_itemamt_net__temp__ = adc_itemamt_net;
			Sybase.PowerBuilder.PBDecimal adc_vatamt__temp__ = adc_vatamt;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_itemcaltax(as_wspass__temp__, as_coopid__temp__, ai_recv_pay__temp__, ai_calvat__temp__, ai_taxcode__temp__, adc_itemamt__temp__, ref adc_taxamt__temp__, ref adc_itemamt_net__temp__, ref adc_vatamt__temp__);
			adc_taxamt = adc_taxamt__temp__;
			adc_itemamt_net = adc_itemamt_net__temp__;
			adc_vatamt = adc_vatamt__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_open_day")]
		public virtual System.Int16 of_open_day(string as_wspass, string as_openday_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_openday_xml__temp__;
			as_openday_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_openday_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_open_day(as_wspass__temp__, as_openday_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_payslip")]
		public virtual string of_payslip(string as_wspass, string as_main_xml, string as_item_xml, string as_taxdetail_xml, string as_appname)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_item_xml__temp__;
			as_item_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_item_xml);
			Sybase.PowerBuilder.PBString as_taxdetail_xml__temp__;
			as_taxdetail_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_taxdetail_xml);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_payslip(as_wspass__temp__, as_main_xml__temp__, as_item_xml__temp__, as_taxdetail_xml__temp__, as_appname__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_payslip_pea")]
		public virtual string of_payslip_pea(string as_wspass, string as_main_xml, string as_item_xml, string as_taxdetail_xml, string as_appname)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_item_xml__temp__;
			as_item_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_item_xml);
			Sybase.PowerBuilder.PBString as_taxdetail_xml__temp__;
			as_taxdetail_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_taxdetail_xml);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_payslip_pea(as_wspass__temp__, as_main_xml__temp__, as_item_xml__temp__, as_taxdetail_xml__temp__, as_appname__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_payslip_pia")]
		public virtual string of_payslip_pia(string as_wspass, string as_main_xml, string as_item_xml, string as_taxdetail_xml, string as_appname)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_item_xml__temp__;
			as_item_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_item_xml);
			Sybase.PowerBuilder.PBString as_taxdetail_xml__temp__;
			as_taxdetail_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_taxdetail_xml);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_payslip_pia(as_wspass__temp__, as_main_xml__temp__, as_item_xml__temp__, as_taxdetail_xml__temp__, as_appname__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postcancel_sendchq")]
		public virtual System.Int16 of_postcancel_sendchq(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_bank_xml, string as_cancellist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_bank_xml__temp__;
			as_bank_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_bank_xml);
			Sybase.PowerBuilder.PBString as_cancellist_xml__temp__;
			as_cancellist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cancellist_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postcancel_sendchq(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_bank_xml__temp__, as_cancellist_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postcancelchq")]
		public virtual System.Int16 of_postcancelchq(string as_wspass, string as_coopid, System.DateTime adtm_wdate, string as_cancleid, string as_machine, string as_cancellist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_cancleid__temp__;
			as_cancleid__temp__ = new Sybase.PowerBuilder.PBString((string)as_cancleid);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_cancellist_xml__temp__;
			as_cancellist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cancellist_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postcancelchq(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, as_cancleid__temp__, as_machine__temp__, as_cancellist_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postcancelposttobank")]
		public virtual System.Int16 of_postcancelposttobank(string as_wspass, string as_branch, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_banklist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_branch__temp__;
			as_branch__temp__ = new Sybase.PowerBuilder.PBString((string)as_branch);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_banklist_xml__temp__;
			as_banklist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_banklist_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postcancelposttobank(as_wspass__temp__, as_branch__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_banklist_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postcancelsendchq")]
		public virtual System.Int16 of_postcancelsendchq(string as_wspass, string as_coopid, string as_chqno, string as_bank, string as_bankbranch, System.Int16 ai_seqno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_chqno__temp__;
			as_chqno__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqno);
			Sybase.PowerBuilder.PBString as_bank__temp__;
			as_bank__temp__ = new Sybase.PowerBuilder.PBString((string)as_bank);
			Sybase.PowerBuilder.PBString as_bankbranch__temp__;
			as_bankbranch__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankbranch);
			Sybase.PowerBuilder.PBInt ai_seqno__temp__;
			ai_seqno__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_seqno);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postcancelsendchq(as_wspass__temp__, as_coopid__temp__, as_chqno__temp__, as_bank__temp__, as_bankbranch__temp__, ai_seqno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postcancelslip")]
		public virtual System.Int16 of_postcancelslip(string as_wspass, string as_coopid, string as_entry_id, string as_head_xml, string as_cancle_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_head_xml__temp__;
			as_head_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_head_xml);
			Sybase.PowerBuilder.PBString as_cancle_xml__temp__;
			as_cancle_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cancle_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postcancelslip(as_wspass__temp__, as_coopid__temp__, as_entry_id__temp__, as_head_xml__temp__, as_cancle_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postchangedstatuschq")]
		public virtual System.Int16 of_postchangedstatuschq(string as_wspass, string as_coopid, string as_entry_id, System.DateTime adtm_wdate, string as_machine, string as_chqno, string as_chqbookno, string as_bank, string as_bankbranch, System.Int16 as_chqseq_no, System.Int16 ai_chqstatus)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_chqno__temp__;
			as_chqno__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqno);
			Sybase.PowerBuilder.PBString as_chqbookno__temp__;
			as_chqbookno__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqbookno);
			Sybase.PowerBuilder.PBString as_bank__temp__;
			as_bank__temp__ = new Sybase.PowerBuilder.PBString((string)as_bank);
			Sybase.PowerBuilder.PBString as_bankbranch__temp__;
			as_bankbranch__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankbranch);
			Sybase.PowerBuilder.PBInt as_chqseq_no__temp__;
			as_chqseq_no__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)as_chqseq_no);
			Sybase.PowerBuilder.PBInt ai_chqstatus__temp__;
			ai_chqstatus__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_chqstatus);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postchangedstatuschq(as_wspass__temp__, as_coopid__temp__, as_entry_id__temp__, adtm_wdate__temp__, as_machine__temp__, as_chqno__temp__, as_chqbookno__temp__, as_bank__temp__, as_bankbranch__temp__, as_chqseq_no__temp__, ai_chqstatus__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postchqmas")]
		public virtual System.Int16 of_postchqmas(string as_wspass, string as_chqbook_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_chqbook_xml__temp__;
			as_chqbook_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqbook_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postchqmas(as_wspass__temp__, as_chqbook_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postfincontact")]
		public virtual System.Int16 of_postfincontact(string as_wspass, string as_contact_xml, string as_action)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_contact_xml__temp__;
			as_contact_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_contact_xml);
			Sybase.PowerBuilder.PBString as_action__temp__;
			as_action__temp__ = new Sybase.PowerBuilder.PBString((string)as_action);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postfincontact(as_wspass__temp__, as_contact_xml__temp__, as_action__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postfinstatusexport")]
		public virtual System.Int16 of_postfinstatusexport(string as_wspass, string as_coopname, string as_mainxml, string as_path)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopname__temp__;
			as_coopname__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopname);
			Sybase.PowerBuilder.PBString as_mainxml__temp__;
			as_mainxml__temp__ = new Sybase.PowerBuilder.PBString((string)as_mainxml);
			Sybase.PowerBuilder.PBString as_path__temp__;
			as_path__temp__ = new Sybase.PowerBuilder.PBString((string)as_path);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postfinstatusexport(as_wspass__temp__, as_coopname__temp__, as_mainxml__temp__, as_path__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postotherto_fin")]
		public virtual string of_postotherto_fin(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_appname, string as_main_xml, string as_itemdet_xml, string as_cancelslip_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_appname__temp__;
			as_appname__temp__ = new Sybase.PowerBuilder.PBString((string)as_appname);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_itemdet_xml__temp__;
			as_itemdet_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_itemdet_xml);
			Sybase.PowerBuilder.PBString as_cancelslip_xml__temp__;
			as_cancelslip_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cancelslip_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postotherto_fin(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_appname__temp__, as_main_xml__temp__, as_itemdet_xml__temp__, as_cancelslip_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postpaychq")]
		public virtual string of_postpaychq(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_main_xml, string as_chqlist_xml, string as_formset)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__;
			as_chqlist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqlist_xml);
			Sybase.PowerBuilder.PBString as_formset__temp__;
			as_formset__temp__ = new Sybase.PowerBuilder.PBString((string)as_formset);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postpaychq(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_main_xml__temp__, as_chqlist_xml__temp__, as_formset__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postpaychq_fromapvloan")]
		public virtual string of_postpaychq_fromapvloan(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_cond_xml, string as_cutbank_xml, string as_chqtype_xml, string as_payoutslip, string as_formset)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_cutbank_xml__temp__;
			as_cutbank_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cutbank_xml);
			Sybase.PowerBuilder.PBString as_chqtype_xml__temp__;
			as_chqtype_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqtype_xml);
			Sybase.PowerBuilder.PBString as_payoutslip__temp__;
			as_payoutslip__temp__ = new Sybase.PowerBuilder.PBString((string)as_payoutslip);
			Sybase.PowerBuilder.PBString as_formset__temp__;
			as_formset__temp__ = new Sybase.PowerBuilder.PBString((string)as_formset);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postpaychq_fromapvloan(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_cond_xml__temp__, as_cutbank_xml__temp__, as_chqtype_xml__temp__, as_payoutslip__temp__, as_formset__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postpaychq_fromslip")]
		public virtual string of_postpaychq_fromslip(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_cond_xml, string as_cutbank_xml, string as_chqtype_xml, string as_chqllist_xml, string as_formset)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_cutbank_xml__temp__;
			as_cutbank_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cutbank_xml);
			Sybase.PowerBuilder.PBString as_chqtype_xml__temp__;
			as_chqtype_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqtype_xml);
			Sybase.PowerBuilder.PBString as_chqllist_xml__temp__;
			as_chqllist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqllist_xml);
			Sybase.PowerBuilder.PBString as_formset__temp__;
			as_formset__temp__ = new Sybase.PowerBuilder.PBString((string)as_formset);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postpaychq_fromslip(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_cond_xml__temp__, as_cutbank_xml__temp__, as_chqtype_xml__temp__, as_chqllist_xml__temp__, as_formset__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postpaychq_manual")]
		public virtual string of_postpaychq_manual(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_main_xml, string as_formset)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_formset__temp__;
			as_formset__temp__ = new Sybase.PowerBuilder.PBString((string)as_formset);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postpaychq_manual(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_main_xml__temp__, as_formset__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postreprintchq")]
		public virtual string of_postreprintchq(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_formset, string as_cond_xml, string as_retreive_xml, string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_formset__temp__;
			as_formset__temp__ = new Sybase.PowerBuilder.PBString((string)as_formset);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_retreive_xml__temp__;
			as_retreive_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_retreive_xml);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__;
			as_chqlist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqlist_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postreprintchq(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_formset__temp__, as_cond_xml__temp__, as_retreive_xml__temp__, as_chqlist_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postsavesendchq")]
		public virtual System.Int16 of_postsavesendchq(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_sendchq_xml, string as_waitchq_xml, string as_sendchqacc_xml, System.Int16 ai_accknow)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_sendchq_xml__temp__;
			as_sendchq_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_sendchq_xml);
			Sybase.PowerBuilder.PBString as_waitchq_xml__temp__;
			as_waitchq_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_waitchq_xml);
			Sybase.PowerBuilder.PBString as_sendchqacc_xml__temp__;
			as_sendchqacc_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_sendchqacc_xml);
			Sybase.PowerBuilder.PBInt ai_accknow__temp__;
			ai_accknow__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_accknow);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postsavesendchq(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_sendchq_xml__temp__, as_waitchq_xml__temp__, as_sendchqacc_xml__temp__, ai_accknow__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postslipbank")]
		public virtual System.Int16 of_postslipbank(string as_wspass, string as_main_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postslipbank(as_wspass__temp__, as_main_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_posttobank")]
		public virtual System.Int16 of_posttobank(string as_wspass, string as_coopid, string as_entryid, System.DateTime adtm_wdate, string as_machine, System.Int16 ai_seqno)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBInt ai_seqno__temp__;
			ai_seqno__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_seqno);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_posttobank(as_wspass__temp__, as_coopid__temp__, as_entryid__temp__, adtm_wdate__temp__, as_machine__temp__, ai_seqno__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postupdatebankaccount")]
		public virtual System.Int16 of_postupdatebankaccount(string as_wspass, string as_bank_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_bank_xml__temp__;
			as_bank_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_bank_xml);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postupdatebankaccount(as_wspass__temp__, as_bank_xml__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrieve_cancleslip")]
		public virtual System.Int16 of_retrieve_cancleslip(string as_wspass, string as_coopid, string as_head_xml, ref string as_itemlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_head_xml__temp__;
			as_head_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_head_xml);
			Sybase.PowerBuilder.PBString as_itemlist_xml__temp__ = as_itemlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrieve_cancleslip(as_wspass__temp__, as_coopid__temp__, as_head_xml__temp__, ref as_itemlist_xml__temp__);
			as_itemlist_xml = as_itemlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievebankaccount")]
		public virtual System.Int16 of_retrievebankaccount(string as_wspass, string as_coopid, ref string as_bank_xml, ref string as_bankstm_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_bank_xml__temp__ = as_bank_xml;
			Sybase.PowerBuilder.PBString as_bankstm_xml__temp__ = as_bankstm_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievebankaccount(as_wspass__temp__, as_coopid__temp__, ref as_bank_xml__temp__, ref as_bankstm_xml__temp__);
			as_bank_xml = as_bank_xml__temp__;
			as_bankstm_xml = as_bankstm_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievecancelchq")]
		public virtual System.Int16 of_retrievecancelchq(string as_wspass, string as_coopid, string as_cond_xml, ref string as_chqcancel_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_chqcancel_xml__temp__ = as_chqcancel_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievecancelchq(as_wspass__temp__, as_coopid__temp__, as_cond_xml__temp__, ref as_chqcancel_xml__temp__);
			as_chqcancel_xml = as_chqcancel_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievechangechqstatus")]
		public virtual System.Int16 of_retrievechangechqstatus(string as_wspass, string as_coopid, ref string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievechangechqstatus(as_wspass__temp__, as_coopid__temp__, ref as_chqlist_xml__temp__);
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievechqfrom_apvloancbt")]
		public virtual System.Int16 of_retrievechqfrom_apvloancbt(string as_wspass, string as_coopid, System.DateTime adtm_wdate, string as_bankcode, string as_lngroupcode, ref string as_chqlist_xml, string as_cashtype)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_bankcode__temp__;
			as_bankcode__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankcode);
			Sybase.PowerBuilder.PBString as_lngroupcode__temp__;
			as_lngroupcode__temp__ = new Sybase.PowerBuilder.PBString((string)as_lngroupcode);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			Sybase.PowerBuilder.PBString as_cashtype__temp__;
			as_cashtype__temp__ = new Sybase.PowerBuilder.PBString((string)as_cashtype);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievechqfrom_apvloancbt(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, as_bankcode__temp__, as_lngroupcode__temp__, ref as_chqlist_xml__temp__, as_cashtype__temp__);
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievechqfromapvloan")]
		public virtual System.Int16 of_retrievechqfromapvloan(string as_wspass, string as_coopcltr, System.DateTime adtm_wdate, string as_cashtype, ref string as_chqlist_xml, string as_bankcode, string as_bankbranch)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopcltr__temp__;
			as_coopcltr__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopcltr);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_cashtype__temp__;
			as_cashtype__temp__ = new Sybase.PowerBuilder.PBString((string)as_cashtype);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			Sybase.PowerBuilder.PBString as_bankcode__temp__;
			as_bankcode__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankcode);
			Sybase.PowerBuilder.PBString as_bankbranch__temp__;
			as_bankbranch__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankbranch);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievechqfromapvloan(as_wspass__temp__, as_coopcltr__temp__, adtm_wdate__temp__, as_cashtype__temp__, ref as_chqlist_xml__temp__, as_bankcode__temp__, as_bankbranch__temp__);
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievechqfromslip")]
		public virtual System.Int16 of_retrievechqfromslip(string as_wspass, string as_coopid, System.DateTime adtm_wdate, ref string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievechqfromslip(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, ref as_chqlist_xml__temp__);
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievefinslipdet")]
		public virtual System.Int16 of_retrievefinslipdet(string as_wspass, string as_coopid, string as_slipno, ref string as_slipdet_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_slipno__temp__;
			as_slipno__temp__ = new Sybase.PowerBuilder.PBString((string)as_slipno);
			Sybase.PowerBuilder.PBString as_slipdet_xml__temp__ = as_slipdet_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievefinslipdet(as_wspass__temp__, as_coopid__temp__, as_slipno__temp__, ref as_slipdet_xml__temp__);
			as_slipdet_xml = as_slipdet_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievepaychqlist")]
		public virtual System.Int16 of_retrievepaychqlist(string as_wspass, string as_coopid, System.DateTime adtm_wdate, ref string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievepaychqlist(as_wspass__temp__, as_coopid__temp__, adtm_wdate__temp__, ref as_chqlist_xml__temp__);
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievereprintchq")]
		public virtual System.Int16 of_retrievereprintchq(string as_wspass, string as_coopid, string as_retreive_xml, ref string as_chqlist_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_retreive_xml__temp__;
			as_retreive_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_retreive_xml);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__ = as_chqlist_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievereprintchq(as_wspass__temp__, as_coopid__temp__, as_retreive_xml__temp__, ref as_chqlist_xml__temp__);
			as_chqlist_xml = as_chqlist_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievereprintpayslip")]
		public virtual System.Int16 of_retrievereprintpayslip(string as_wspass, string as_coopid, string as_cond_xml, ref string as_slip_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_slip_xml__temp__ = as_slip_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievereprintpayslip(as_wspass__temp__, as_coopid__temp__, as_cond_xml__temp__, ref as_slip_xml__temp__);
			as_slip_xml = as_slip_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievereprintreceipt")]
		public virtual System.Int16 of_retrievereprintreceipt(string as_wspass, string as_coopid, string as_cond_xml, ref string as_list_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_list_xml__temp__ = as_list_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievereprintreceipt(as_wspass__temp__, as_coopid__temp__, as_cond_xml__temp__, ref as_list_xml__temp__);
			as_list_xml = as_list_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievetaxpay")]
		public virtual System.Int16 of_retrievetaxpay(string as_wspass, string as_coopid, string as_main_xml, ref string as_list_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_main_xml__temp__;
			as_main_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_main_xml);
			Sybase.PowerBuilder.PBString as_list_xml__temp__ = as_list_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievetaxpay(as_wspass__temp__, as_coopid__temp__, as_main_xml__temp__, ref as_list_xml__temp__);
			as_list_xml = as_list_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_dddwbankbranch")]
		public virtual System.Int16 of_dddwbankbranch(string as_wspass, string as_bank, ref string as_bankbranch)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_bank__temp__;
			as_bank__temp__ = new Sybase.PowerBuilder.PBString((string)as_bank);
			Sybase.PowerBuilder.PBString as_bankbranch__temp__ = as_bankbranch;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_dddwbankbranch(as_wspass__temp__, as_bank__temp__, ref as_bankbranch__temp__);
			as_bankbranch = as_bankbranch__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_dddwfinitemtype")]
		public virtual string of_dddwfinitemtype(string as_wspass)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_dddwfinitemtype(as_wspass__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_dddwbank")]
		public virtual string of_dddwbank(string as_wspass)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_dddwbank(as_wspass__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getlistreappr_moneyorder")]
		public virtual string of_getlistreappr_moneyorder(string as_wspass, System.DateTime adtm_trn)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_trn__temp__;
			adtm_trn__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_trn);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_getlistreappr_moneyorder(as_wspass__temp__, adtm_trn__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getlistcancel_moneyorder")]
		public virtual string of_getlistcancel_moneyorder(string as_wspass, System.DateTime adtm_trn)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_trn__temp__;
			adtm_trn__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_trn);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_getlistcancel_moneyorder(as_wspass__temp__, adtm_trn__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_cancel_moneyorder")]
		public virtual System.Int16 of_cancel_moneyorder(string as_wspass, string as_moneyorder_list_xml, string as_entryid, System.DateTime adtm_entry)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_moneyorder_list_xml__temp__;
			as_moneyorder_list_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_moneyorder_list_xml);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBDateTime adtm_entry__temp__;
			adtm_entry__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entry);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_cancel_moneyorder(as_wspass__temp__, as_moneyorder_list_xml__temp__, as_entryid__temp__, adtm_entry__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_cancelappr_moneyorder")]
		public virtual System.Int16 of_cancelappr_moneyorder(string as_wspass, string as_moneyorder_list_xml, string as_entryid, System.DateTime adtm_entry)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_moneyorder_list_xml__temp__;
			as_moneyorder_list_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_moneyorder_list_xml);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBDateTime adtm_entry__temp__;
			adtm_entry__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entry);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_cancelappr_moneyorder(as_wspass__temp__, as_moneyorder_list_xml__temp__, as_entryid__temp__, adtm_entry__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_approve_moneyorder")]
		public virtual System.Int16 of_approve_moneyorder(string as_wspass, string as_moneyorder_list_xml, string as_entryid, System.DateTime adtm_entry)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_moneyorder_list_xml__temp__;
			as_moneyorder_list_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_moneyorder_list_xml);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBDateTime adtm_entry__temp__;
			adtm_entry__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entry);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_approve_moneyorder(as_wspass__temp__, as_moneyorder_list_xml__temp__, as_entryid__temp__, adtm_entry__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getlistappr_moneyorder")]
		public virtual string of_getlistappr_moneyorder(string as_wspass, System.DateTime adtm_trn)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_trn__temp__;
			adtm_trn__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_trn);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_getlistappr_moneyorder(as_wspass__temp__, adtm_trn__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievereprintpayslip_ir")]
		public virtual System.Int16 of_retrievereprintpayslip_ir(string as_wspass, string as_coopid, string as_cond_xml, ref string as_slip_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_slip_xml__temp__ = as_slip_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievereprintpayslip_ir(as_wspass__temp__, as_coopid__temp__, as_cond_xml__temp__, ref as_slip_xml__temp__);
			as_slip_xml = as_slip_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_retrievechangchqdetail")]
		public virtual System.Int16 of_retrievechangchqdetail(string as_coopid, string as_chqno, string as_bookno, string as_bank, string as_bankbranch, System.Int16 ai_seqno, ref string as_chqdetail_xml, string as_wspass)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_chqno__temp__;
			as_chqno__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqno);
			Sybase.PowerBuilder.PBString as_bookno__temp__;
			as_bookno__temp__ = new Sybase.PowerBuilder.PBString((string)as_bookno);
			Sybase.PowerBuilder.PBString as_bank__temp__;
			as_bank__temp__ = new Sybase.PowerBuilder.PBString((string)as_bank);
			Sybase.PowerBuilder.PBString as_bankbranch__temp__;
			as_bankbranch__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankbranch);
			Sybase.PowerBuilder.PBInt ai_seqno__temp__;
			ai_seqno__temp__ = new Sybase.PowerBuilder.PBInt((System.Int16)ai_seqno);
			Sybase.PowerBuilder.PBString as_chqdetail_xml__temp__ = as_chqdetail_xml;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_retrievechangchqdetail(as_coopid__temp__, as_chqno__temp__, as_bookno__temp__, as_bank__temp__, as_bankbranch__temp__, ai_seqno__temp__, ref as_chqdetail_xml__temp__, as_wspass__temp__);
			as_chqdetail_xml = as_chqdetail_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_init_moneyorder")]
		public virtual string of_init_moneyorder(string as_wspass, string as_moneyorder_master_xml, string as_entryid, System.DateTime adtm_entry)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_moneyorder_master_xml__temp__;
			as_moneyorder_master_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_moneyorder_master_xml);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBDateTime adtm_entry__temp__;
			adtm_entry__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entry);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_init_moneyorder(as_wspass__temp__, as_moneyorder_master_xml__temp__, as_entryid__temp__, adtm_entry__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getlist_moneyorder")]
		public virtual string of_getlist_moneyorder(string as_wspass, System.DateTime adtm_trn)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBDateTime adtm_trn__temp__;
			adtm_trn__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_trn);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_getlist_moneyorder(as_wspass__temp__, adtm_trn__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getdata_moneyorder")]
		public virtual System.Int16 of_getdata_moneyorder(string as_wspass, string as_docno, ref string as_moneyorder_master_xml, ref string as_moneyorder_detail_xml)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_docno__temp__;
			as_docno__temp__ = new Sybase.PowerBuilder.PBString((string)as_docno);
			Sybase.PowerBuilder.PBString as_moneyorder_master_xml__temp__ = as_moneyorder_master_xml;
			Sybase.PowerBuilder.PBString as_moneyorder_detail_xml__temp__ = as_moneyorder_detail_xml;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_getdata_moneyorder(as_wspass__temp__, as_docno__temp__, ref as_moneyorder_master_xml__temp__, ref as_moneyorder_detail_xml__temp__);
			as_moneyorder_master_xml = as_moneyorder_master_xml__temp__;
			as_moneyorder_detail_xml = as_moneyorder_detail_xml__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_getchildbranch")]
		public virtual System.Int16 of_getchildbranch(string as_wspass, ref string as_xmlbank)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_xmlbank__temp__ = as_xmlbank;
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_getchildbranch(as_wspass__temp__, ref as_xmlbank__temp__);
			as_xmlbank = as_xmlbank__temp__;
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_fin_posttobank")]
		public virtual System.Int16 of_fin_posttobank(string as_wspass, string as_branch, string as_entry_id, System.DateTime adtm_wdate, string as_machine, string as_item_xmt)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_branch__temp__;
			as_branch__temp__ = new Sybase.PowerBuilder.PBString((string)as_branch);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_item_xmt__temp__;
			as_item_xmt__temp__ = new Sybase.PowerBuilder.PBString((string)as_item_xmt);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_fin_posttobank(as_wspass__temp__, as_branch__temp__, as_entry_id__temp__, adtm_wdate__temp__, as_machine__temp__, as_item_xmt__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_save_moneyorder")]
		public virtual System.Int16 of_save_moneyorder(string as_wspass, string as_moneyorder_master_xml, string as_moneyorder_detail_xml, string as_entryid, System.DateTime adtm_entry)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_moneyorder_master_xml__temp__;
			as_moneyorder_master_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_moneyorder_master_xml);
			Sybase.PowerBuilder.PBString as_moneyorder_detail_xml__temp__;
			as_moneyorder_detail_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_moneyorder_detail_xml);
			Sybase.PowerBuilder.PBString as_entryid__temp__;
			as_entryid__temp__ = new Sybase.PowerBuilder.PBString((string)as_entryid);
			Sybase.PowerBuilder.PBDateTime adtm_entry__temp__;
			adtm_entry__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_entry);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_save_moneyorder(as_wspass__temp__, as_moneyorder_master_xml__temp__, as_moneyorder_detail_xml__temp__, as_entryid__temp__, adtm_entry__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postfundto_fin")]
		public virtual System.Int16 of_postfundto_fin(string as_wspass, string as_postlist_xml, string as_entry_id, string as_coop_id, System.DateTime adtm_date)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_postlist_xml__temp__;
			as_postlist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_postlist_xml);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postfundto_fin(as_wspass__temp__, as_postlist_xml__temp__, as_entry_id__temp__, as_coop_id__temp__, adtm_date__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postfundto_depttran")]
		public virtual System.Int16 of_postfundto_depttran(string as_wspass, string as_postlist_xml, string as_entry_id, string as_coop_id, System.DateTime adtm_date)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_postlist_xml__temp__;
			as_postlist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_postlist_xml);
			Sybase.PowerBuilder.PBString as_entry_id__temp__;
			as_entry_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry_id);
			Sybase.PowerBuilder.PBString as_coop_id__temp__;
			as_coop_id__temp__ = new Sybase.PowerBuilder.PBString((string)as_coop_id);
			Sybase.PowerBuilder.PBDateTime adtm_date__temp__;
			adtm_date__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_date);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postfundto_depttran(as_wspass__temp__, as_postlist_xml__temp__, as_entry_id__temp__, as_coop_id__temp__, adtm_date__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
		[OperationContract(Name="of_postpaychq_split")]
		public virtual System.Int16 of_postpaychq_split(string as_wspass, string as_coopid, string as_entry, System.DateTime adtm_wdate, string as_machine, string as_cond_xml, string as_bankcut_xml, string as_chqtype_xml, string as_chqlist_xml, string as_chqsplit_xml, string as_formset)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			System.Int16 __retval__;
			Sybase.PowerBuilder.PBString as_wspass__temp__;
			as_wspass__temp__ = new Sybase.PowerBuilder.PBString((string)as_wspass);
			Sybase.PowerBuilder.PBString as_coopid__temp__;
			as_coopid__temp__ = new Sybase.PowerBuilder.PBString((string)as_coopid);
			Sybase.PowerBuilder.PBString as_entry__temp__;
			as_entry__temp__ = new Sybase.PowerBuilder.PBString((string)as_entry);
			Sybase.PowerBuilder.PBDateTime adtm_wdate__temp__;
			adtm_wdate__temp__ = new Sybase.PowerBuilder.PBDateTime((System.DateTime)adtm_wdate);
			Sybase.PowerBuilder.PBString as_machine__temp__;
			as_machine__temp__ = new Sybase.PowerBuilder.PBString((string)as_machine);
			Sybase.PowerBuilder.PBString as_cond_xml__temp__;
			as_cond_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_cond_xml);
			Sybase.PowerBuilder.PBString as_bankcut_xml__temp__;
			as_bankcut_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_bankcut_xml);
			Sybase.PowerBuilder.PBString as_chqtype_xml__temp__;
			as_chqtype_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqtype_xml);
			Sybase.PowerBuilder.PBString as_chqlist_xml__temp__;
			as_chqlist_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqlist_xml);
			Sybase.PowerBuilder.PBString as_chqsplit_xml__temp__;
			as_chqsplit_xml__temp__ = new Sybase.PowerBuilder.PBString((string)as_chqsplit_xml);
			Sybase.PowerBuilder.PBString as_formset__temp__;
			as_formset__temp__ = new Sybase.PowerBuilder.PBString((string)as_formset);
			__retval__ = ((pbservice125.c__n_finance)__nvo__).of_postpaychq_split(as_wspass__temp__, as_coopid__temp__, as_entry__temp__, adtm_wdate__temp__, as_machine__temp__, as_cond_xml__temp__, as_bankcut_xml__temp__, as_chqtype_xml__temp__, as_chqlist_xml__temp__, as_chqsplit_xml__temp__, as_formset__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 