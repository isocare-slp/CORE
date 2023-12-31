using System.ServiceModel; 
using System.Runtime.Serialization; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[DataContract]
	public struct str_lnatm
	{
		internal str_lnatm(c__str_lnatm __x__)
		{
			CopyFrom(out this, __x__);
		}
		internal void CopyFrom(c__str_lnatm __x__)
		{
			CopyFrom(out this, __x__);
		}
		[DataMember] 
		public string coop_id;
		[DataMember] 
		public string coop_code;
		[DataMember] 
		public string member_no;
		[DataMember] 
		public string loantype_code;
		[DataMember] 
		public string loancontract_no;
		[DataMember] 
		public System.DateTime operate_date;
		[DataMember] 
		public string system_cd;
		[DataMember] 
		public string operate_cd;
		[DataMember] 
		public string bank_cd;
		[DataMember] 
		public string branch_cd;
		[DataMember] 
		public string bank_accid;
		[DataMember] 
		public string atm_no;
		[DataMember] 
		public string atm_seqno;
		[DataMember] 
		public decimal item_amt;
		[DataMember] 
		public decimal fee_amt;
		[DataMember] 
		public System.Int16 feeinclude_status;
		[DataMember] 
		public System.Int16 action_status;
		[DataMember] 
		public System.Int16 post_status;
		[DataMember] 
		public string entry_id;
		[DataMember] 
		public string moneytype_code;
		[DataMember] 
		public string stmtitemtype_code;
		[DataMember] 
		public string slipitemtype_code;
		[DataMember] 
		public string account_id;
		[DataMember] 
		public string msg_status;
		[DataMember] 
		public string ref_slipno;
		[DataMember] 
		public decimal withdrawable_amt;
		[DataMember] 
		public decimal principal_amt;
		[DataMember] 
		public decimal approve_amt;
		[DataMember] 
		public string msg_output;
		internal void CopyTo(c__str_lnatm __x__)
		{
			__x__.coop_id = coop_id;
			__x__.coop_code = coop_code;
			__x__.member_no = member_no;
			__x__.loantype_code = loantype_code;
			__x__.loancontract_no = loancontract_no;
			__x__.operate_date = operate_date;
			__x__.system_cd = system_cd;
			__x__.operate_cd = operate_cd;
			__x__.bank_cd = bank_cd;
			__x__.branch_cd = branch_cd;
			__x__.bank_accid = bank_accid;
			__x__.atm_no = atm_no;
			__x__.atm_seqno = atm_seqno;
			__x__.item_amt = item_amt;
			__x__.fee_amt = fee_amt;
			__x__.feeinclude_status = feeinclude_status;
			__x__.action_status = action_status;
			__x__.post_status = post_status;
			__x__.entry_id = entry_id;
			__x__.moneytype_code = moneytype_code;
			__x__.stmtitemtype_code = stmtitemtype_code;
			__x__.slipitemtype_code = slipitemtype_code;
			__x__.account_id = account_id;
			__x__.msg_status = msg_status;
			__x__.ref_slipno = ref_slipno;
			__x__.withdrawable_amt = withdrawable_amt;
			__x__.principal_amt = principal_amt;
			__x__.approve_amt = approve_amt;
			__x__.msg_output = msg_output;
		}
		internal static void CopyFrom(out str_lnatm __this__, c__str_lnatm __x__)
		{
			__this__.coop_id = __x__.coop_id;
			__this__.coop_code = __x__.coop_code;
			__this__.member_no = __x__.member_no;
			__this__.loantype_code = __x__.loantype_code;
			__this__.loancontract_no = __x__.loancontract_no;
			__this__.operate_date = __x__.operate_date;
			__this__.system_cd = __x__.system_cd;
			__this__.operate_cd = __x__.operate_cd;
			__this__.bank_cd = __x__.bank_cd;
			__this__.branch_cd = __x__.branch_cd;
			__this__.bank_accid = __x__.bank_accid;
			__this__.atm_no = __x__.atm_no;
			__this__.atm_seqno = __x__.atm_seqno;
			__this__.item_amt = __x__.item_amt;
			__this__.fee_amt = __x__.fee_amt;
			__this__.feeinclude_status = __x__.feeinclude_status;
			__this__.action_status = __x__.action_status;
			__this__.post_status = __x__.post_status;
			__this__.entry_id = __x__.entry_id;
			__this__.moneytype_code = __x__.moneytype_code;
			__this__.stmtitemtype_code = __x__.stmtitemtype_code;
			__this__.slipitemtype_code = __x__.slipitemtype_code;
			__this__.account_id = __x__.account_id;
			__this__.msg_status = __x__.msg_status;
			__this__.ref_slipno = __x__.ref_slipno;
			__this__.withdrawable_amt = __x__.withdrawable_amt;
			__this__.principal_amt = __x__.principal_amt;
			__this__.approve_amt = __x__.approve_amt;
			__this__.msg_output = __x__.msg_output;
		}
		public static explicit operator object[](str_lnatm __this__)
		{
			return new object[] {
				__this__.coop_id
				,__this__.coop_code
				,__this__.member_no
				,__this__.loantype_code
				,__this__.loancontract_no
				,__this__.operate_date
				,__this__.system_cd
				,__this__.operate_cd
				,__this__.bank_cd
				,__this__.branch_cd
				,__this__.bank_accid
				,__this__.atm_no
				,__this__.atm_seqno
				,__this__.item_amt
				,__this__.fee_amt
				,__this__.feeinclude_status
				,__this__.action_status
				,__this__.post_status
				,__this__.entry_id
				,__this__.moneytype_code
				,__this__.stmtitemtype_code
				,__this__.slipitemtype_code
				,__this__.account_id
				,__this__.msg_status
				,__this__.ref_slipno
				,__this__.withdrawable_amt
				,__this__.principal_amt
				,__this__.approve_amt
				,__this__.msg_output
			};
		}
	}
} 