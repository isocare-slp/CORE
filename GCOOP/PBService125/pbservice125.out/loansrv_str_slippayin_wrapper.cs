using System.ServiceModel; 
using System.Runtime.Serialization; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[DataContract]
	public struct str_slippayin
	{
		internal str_slippayin(c__str_slippayin __x__)
		{
			CopyFrom(out this, __x__);
		}
		internal void CopyFrom(c__str_slippayin __x__)
		{
			CopyFrom(out this, __x__);
		}
		[DataMember] 
		public string memcoop_id;
		[DataMember] 
		public string member_no;
		[DataMember] 
		public string sliptype_code;
		[DataMember] 
		public System.DateTime slip_date;
		[DataMember] 
		public System.DateTime operate_date;
		[DataMember] 
		public string xml_sliphead;
		[DataMember] 
		public string xml_slipshr;
		[DataMember] 
		public string xml_sliplon;
		[DataMember] 
		public string xml_slipetc;
		[DataMember] 
		public string xml_expense;
		[DataMember] 
		public string entry_id;
		[DataMember] 
		public string coop_id;
		[DataMember] 
		public string payinorder_no;
		[DataMember] 
		public string payinslip_no;
		[DataMember] 
		public bool receiptno_flag;
		[DataMember] 
		public bool remark;
		[DataMember] 
		public string recv_period;
		[DataMember] 
		public string ref_slipno;
		internal void CopyTo(c__str_slippayin __x__)
		{
			__x__.memcoop_id = memcoop_id;
			__x__.member_no = member_no;
			__x__.sliptype_code = sliptype_code;
			__x__.slip_date = slip_date;
			__x__.operate_date = operate_date;
			__x__.xml_sliphead = xml_sliphead;
			__x__.xml_slipshr = xml_slipshr;
			__x__.xml_sliplon = xml_sliplon;
			__x__.xml_slipetc = xml_slipetc;
			__x__.xml_expense = xml_expense;
			__x__.entry_id = entry_id;
			__x__.coop_id = coop_id;
			__x__.payinorder_no = payinorder_no;
			__x__.payinslip_no = payinslip_no;
			__x__.receiptno_flag = receiptno_flag;
			__x__.remark = remark;
			__x__.recv_period = recv_period;
			__x__.ref_slipno = ref_slipno;
		}
		internal static void CopyFrom(out str_slippayin __this__, c__str_slippayin __x__)
		{
			__this__.memcoop_id = __x__.memcoop_id;
			__this__.member_no = __x__.member_no;
			__this__.sliptype_code = __x__.sliptype_code;
			__this__.slip_date = __x__.slip_date;
			__this__.operate_date = __x__.operate_date;
			__this__.xml_sliphead = __x__.xml_sliphead;
			__this__.xml_slipshr = __x__.xml_slipshr;
			__this__.xml_sliplon = __x__.xml_sliplon;
			__this__.xml_slipetc = __x__.xml_slipetc;
			__this__.xml_expense = __x__.xml_expense;
			__this__.entry_id = __x__.entry_id;
			__this__.coop_id = __x__.coop_id;
			__this__.payinorder_no = __x__.payinorder_no;
			__this__.payinslip_no = __x__.payinslip_no;
			__this__.receiptno_flag = __x__.receiptno_flag;
			__this__.remark = __x__.remark;
			__this__.recv_period = __x__.recv_period;
			__this__.ref_slipno = __x__.ref_slipno;
		}
		public static explicit operator object[](str_slippayin __this__)
		{
			return new object[] {
				__this__.memcoop_id
				,__this__.member_no
				,__this__.sliptype_code
				,__this__.slip_date
				,__this__.operate_date
				,__this__.xml_sliphead
				,__this__.xml_slipshr
				,__this__.xml_sliplon
				,__this__.xml_slipetc
				,__this__.xml_expense
				,__this__.entry_id
				,__this__.coop_id
				,__this__.payinorder_no
				,__this__.payinslip_no
				,__this__.receiptno_flag
				,__this__.remark
				,__this__.recv_period
				,__this__.ref_slipno
			};
		}
	}
} 