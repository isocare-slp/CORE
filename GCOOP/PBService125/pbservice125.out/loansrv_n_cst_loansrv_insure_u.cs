//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx\\n_cst_loansrv_insure.sru"
#line hidden
#line 1 "n_cst_loansrv_insure"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("n_cst_loansrv_insure",Sybase.PowerBuilder.PBGroupType.UserObject,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx",null,"pbservice125")]
internal class c__n_cst_loansrv_insure : Sybase.PowerBuilder.PBNonVisualObject
{
	#line hidden
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "itr_sqlca", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBTransaction itr_sqlca = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "ithw_exception", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBException ithw_exception = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "is_coopcontrol", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBString is_coopcontrol = Sybase.PowerBuilder.PBString.DefaultValue;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "is_coopid", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public Sybase.PowerBuilder.PBString is_coopid = Sybase.PowerBuilder.PBString.DefaultValue;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_transection", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_dbconnectservice inv_transection = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_dwxmliesrv", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_dwxmlieservice inv_dwxmliesrv = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_dwsrv", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_datawindowsservice inv_dwsrv = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_stringsrv", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_stringservice inv_stringsrv = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_datetime", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_datetimeservice inv_datetime = null;
	[Sybase.PowerBuilder.PBVariableAttribute(Sybase.PowerBuilder.VariableTypeKind.kInstanceVar, "inv_docsrv", null, "n_cst_loansrv_insure", null, null, Sybase.PowerBuilder.PBModifier.Public, "")]
	public c__n_cst_doccontrolservice inv_docsrv = null;

	#line 1 "n_cst_loansrv_insure.of_setsrvdwxmlie(IB)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_setsrvdwxmlie", new string[]{"boolean"}, Sybase.PowerBuilder.PBModifier.Private, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	private Sybase.PowerBuilder.PBInt of_setsrvdwxmlie(Sybase.PowerBuilder.PBBoolean ab_switch)
	{
		#line hidden
		#line 2
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ab_switch)))))
		#line hidden
		{
			#line 3
			return new Sybase.PowerBuilder.PBInt(-1);
			#line hidden
		}
		#line 6
		if (ab_switch)
		#line hidden
		{
			#line 7
			if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_dwxmlieservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_dwxmliesrv)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_dwxmliesrv)))))
			#line hidden
			{
				#line 8
				inv_dwxmliesrv = (c__n_cst_dwxmlieservice)this.CreateInstance(typeof(c__n_cst_dwxmlieservice), 0);
				#line hidden
				#line 9
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		else
		{
			#line 12
			if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_dwxmliesrv)))
			#line hidden
			{
				#line 13
				Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(inv_dwxmliesrv);
				#line hidden
				#line 14
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		#line 18
		return new Sybase.PowerBuilder.PBInt(0);
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.of_setsrvdw(IB)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_setsrvdw", new string[]{"boolean"}, Sybase.PowerBuilder.PBModifier.Private, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	private Sybase.PowerBuilder.PBInt of_setsrvdw(Sybase.PowerBuilder.PBBoolean ab_switch)
	{
		#line hidden
		#line 2
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ab_switch)))))
		#line hidden
		{
			#line 3
			return new Sybase.PowerBuilder.PBInt(-1);
			#line hidden
		}
		#line 6
		if (ab_switch)
		#line hidden
		{
			#line 7
			if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_datawindowsservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_dwsrv)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_dwsrv)))))
			#line hidden
			{
				#line 8
				inv_dwsrv = (c__n_cst_datawindowsservice)this.CreateInstance(typeof(c__n_cst_datawindowsservice), 0);
				#line hidden
				#line 9
				inv_dwsrv.of_initservice(inv_transection);
				#line hidden
				#line 10
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		else
		{
			#line 13
			if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_dwsrv)))
			#line hidden
			{
				#line 14
				Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(inv_dwsrv);
				#line hidden
				#line 15
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		#line 19
		return new Sybase.PowerBuilder.PBInt(0);
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.of_setsrvstring(IB)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_setsrvstring", new string[]{"boolean"}, Sybase.PowerBuilder.PBModifier.Private, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	private Sybase.PowerBuilder.PBInt of_setsrvstring(Sybase.PowerBuilder.PBBoolean ab_switch)
	{
		#line hidden
		#line 2
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ab_switch)))))
		#line hidden
		{
			#line 3
			return new Sybase.PowerBuilder.PBInt(-1);
			#line hidden
		}
		#line 6
		if (ab_switch)
		#line hidden
		{
			#line 7
			if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_stringservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_stringsrv)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_stringsrv)))))
			#line hidden
			{
				#line 8
				inv_stringsrv = (c__n_cst_stringservice)this.CreateInstance(typeof(c__n_cst_stringservice), 0);
				#line hidden
				#line 9
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		else
		{
			#line 12
			if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_stringsrv)))
			#line hidden
			{
				#line 13
				Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(inv_stringsrv);
				#line hidden
				#line 14
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		#line 18
		return new Sybase.PowerBuilder.PBInt(0);
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.of_parsetoarray(ISRS[])"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_parsetoarray", new string[]{"string", "ref string"}, Sybase.PowerBuilder.PBModifier.Private, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	private Sybase.PowerBuilder.PBInt of_parsetoarray_2_345252223(Sybase.PowerBuilder.PBString as_source, [Sybase.PowerBuilder.PBArrayAttribute(typeof(Sybase.PowerBuilder.PBString))] ref Sybase.PowerBuilder.PBArray as_contclr)
	{
		#line hidden
		#line 1
		this.of_setsrvstring(new Sybase.PowerBuilder.PBBoolean(true));
		#line hidden
		#line 2
		of_parsetoarray_3_345252223_3_509010394(inv_stringsrv, as_source, new Sybase.PowerBuilder.PBString(","), ref as_contclr);
		#line hidden
		#line 3
		this.of_setsrvstring(new Sybase.PowerBuilder.PBBoolean(false));
		#line hidden
		#line 5
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.of_setsrvdatetime(IB)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_setsrvdatetime", new string[]{"boolean"}, Sybase.PowerBuilder.PBModifier.Private, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	private Sybase.PowerBuilder.PBInt of_setsrvdatetime(Sybase.PowerBuilder.PBBoolean ab_switch)
	{
		#line hidden
		#line 2
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ab_switch)))))
		#line hidden
		{
			#line 3
			return new Sybase.PowerBuilder.PBInt(-1);
			#line hidden
		}
		#line 6
		if (ab_switch)
		#line hidden
		{
			#line 7
			if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_datetimeservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_datetime)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_datetime)))))
			#line hidden
			{
				#line 8
				inv_datetime = (c__n_cst_datetimeservice)this.CreateInstance(typeof(c__n_cst_datetimeservice), 0);
				#line hidden
				#line 9
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		else
		{
			#line 12
			if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_datetime)))
			#line hidden
			{
				#line 13
				Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(inv_datetime);
				#line hidden
				#line 14
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		#line 18
		return new Sybase.PowerBuilder.PBInt(0);
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.of_initservice(ICn_cst_dbconnectservice.)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_initservice", new string[]{"n_cst_dbconnectservice"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_initservice(c__n_cst_dbconnectservice anv_dbtrans)
	{
		#line hidden
		#line 2
		if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_dbconnectservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_transection)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_transection)))))
		#line hidden
		{
			#line 3
			inv_transection = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
		}
		#line 6
		inv_transection = anv_dbtrans;
		#line hidden
		#line 7
		itr_sqlca = inv_transection.itr_dbconnection;
		#line hidden
		#line 8
		is_coopcontrol = inv_transection.is_coopcontrol;
		#line hidden
		#line 9
		is_coopid = inv_transection.is_coopid;
		#line hidden
		#line 11
		this.of_setsrvdwxmlie(new Sybase.PowerBuilder.PBBoolean(true));
		#line hidden
		#line 13
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.of_setsrvdoccontrol(IB)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_setsrvdoccontrol", new string[]{"boolean"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_setsrvdoccontrol(Sybase.PowerBuilder.PBBoolean ab_switch)
	{
		#line hidden
		#line 2
		if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ab_switch)))))
		#line hidden
		{
			#line 3
			return new Sybase.PowerBuilder.PBInt(-1);
			#line hidden
		}
		#line 6
		if (ab_switch)
		#line hidden
		{
			#line 7
			if ((Sybase.PowerBuilder.PBBoolean)(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)((new Sybase.PowerBuilder.PBAny(((c__n_cst_doccontrolservice)(Sybase.PowerBuilder.PBSystemFunctions.PBCheckNull(inv_docsrv)))))))| !(Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_docsrv)))))
			#line hidden
			{
				#line 8
				inv_docsrv = (c__n_cst_doccontrolservice)this.CreateInstance(typeof(c__n_cst_doccontrolservice), 0);
				#line hidden
				#line 9
				inv_docsrv.of_settrans(inv_transection);
				#line hidden
				#line 10
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		else
		{
			#line 13
			if (Sybase.PowerBuilder.WPF.PBSystemFunctions.IsValid((Sybase.PowerBuilder.PBPowerObject)(inv_docsrv)))
			#line hidden
			{
				#line 14
				Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(inv_docsrv);
				#line hidden
				#line 15
				return new Sybase.PowerBuilder.PBInt(1);
				#line hidden
			}
		}
		#line 19
		return new Sybase.PowerBuilder.PBInt(0);
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.of_calinsurelnreq(IRCstr_lninsure.)"
	#line hidden
	[Sybase.PowerBuilder.PBFunctionAttribute("of_calinsurelnreq", new string[]{"ref str_lninsure"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
	public virtual Sybase.PowerBuilder.PBInt of_calinsurelnreq_1_1273706888<T0>(ref T0 astr_lninsure) where T0 : c__str_lninsure
	{
		#line hidden
		Sybase.PowerBuilder.PBString ls_memno = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_instype = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_contclr = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_sqlbal = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ls_sqlins = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBString ldc_sqlmain = Sybase.PowerBuilder.PBString.DefaultValue;
		Sybase.PowerBuilder.PBInt li_inscaltype = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_dayinsmain = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_dayprot = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBInt li_insrecal = Sybase.PowerBuilder.PBInt.DefaultValue;
		Sybase.PowerBuilder.PBDecimal ldc_stepprot = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_steprate = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_ratemin = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_ratemax = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_stepall = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_sumprn = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_protcur = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_protall = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_protadd = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_lnexcept = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_insamt = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_mod = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDecimal ldc_lnreq = new Sybase.PowerBuilder.PBDecimal(0m);
		Sybase.PowerBuilder.PBDateTime ldtm_protstart = Sybase.PowerBuilder.PBDateTime.DefaultValue;
		Sybase.PowerBuilder.PBDateTime ldtm_protend = Sybase.PowerBuilder.PBDateTime.DefaultValue;
		Sybase.PowerBuilder.PBDateTime ldtm_paydate = Sybase.PowerBuilder.PBDateTime.DefaultValue;

		Sybase.PowerBuilder.DB.DBCursor __DBVARPrefix_lncursor = null;

		Sybase.PowerBuilder.DB.DBCursor __DBVARPrefix_inscursor = null;

		Sybase.PowerBuilder.DB.DBCursor __DBVARPrefix_maincursor = null;
		#line 9
		ls_memno = astr_lninsure.member_no;
		#line hidden
		#line 10
		ls_instype = astr_lninsure.instype_code;
		#line hidden
		#line 11
		ls_contclr = astr_lninsure.lncont_clr;
		#line hidden
		#line 12
		ldc_lnreq.AssignFrom(astr_lninsure.loanrequest_amt);
		#line hidden
		#line 13
		ldtm_paydate = astr_lninsure.loanrcv_date;
		#line hidden
		#line 15
		if ((Sybase.PowerBuilder.PBBoolean)((ls_contclr == new Sybase.PowerBuilder.PBString(""))| Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ls_contclr))))))
		#line hidden
		{
			#line 16
			ls_contclr = new Sybase.PowerBuilder.PBString("''");
			#line hidden
		}
		#line 20
		ls_sqlbal = new Sybase.PowerBuilder.PBString("");
		#line hidden
		#line 21
		ls_sqlbal += new Sybase.PowerBuilder.PBString(" select sum( ln.withdrawable_amt + ln.principal_balance ) ");
		#line hidden
		#line 22
		ls_sqlbal += new Sybase.PowerBuilder.PBString(" from	lncontmaster ln ");
		#line hidden
		#line 23
		ls_sqlbal += new Sybase.PowerBuilder.PBString(" 		join lnloantype lt on ln.coop_id = lt.coop_id and ln.loantype_code = lt.loantype_code ");
		#line hidden
		#line 24
		ls_sqlbal += new Sybase.PowerBuilder.PBString(" where ln.contract_status > 0 ");
		#line hidden
		#line 25
		ls_sqlbal += new Sybase.PowerBuilder.PBString(" and lt.insman_status = 1 ");
		#line hidden
		#line 26
		ls_sqlbal += (new Sybase.PowerBuilder.PBString(" and lt.insmantype_code = '")+ ls_instype)+ new Sybase.PowerBuilder.PBString("' ");
		#line hidden
		#line 27
		ls_sqlbal += (new Sybase.PowerBuilder.PBString(" and ln.member_no = '")+ ls_memno)+ new Sybase.PowerBuilder.PBString("' ");
		#line hidden
		#line 28
		ls_sqlbal += (new Sybase.PowerBuilder.PBString(" and ln.loancontract_no not in (")+ ls_contclr)+ new Sybase.PowerBuilder.PBString(") ");
		#line hidden

		__DBVARPrefix_lncursor = new Sybase.PowerBuilder.DB.DBCursor(
			@"__DBVARPrefix_lncursor",
			c__pbservice125.GetCurrentApplication().sqlsa
		);

		#line 31
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Prepare(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			c__pbservice125.GetCurrentApplication().sqlsa,
			((Sybase.PowerBuilder.PBString)ls_sqlbal).Value,
			itr_sqlca
		);
		#line hidden

		if (__DBVARPrefix_lncursor == null)
		{
			__DBVARPrefix_lncursor = new Sybase.PowerBuilder.DB.DBCursor(
				@"__DBVARPrefix_lncursor",
				c__pbservice125.GetCurrentApplication().sqlsa
			);
		}

		#line 33
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.OpenDynamically3(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			__DBVARPrefix_lncursor,
			new Sybase.PowerBuilder.DB.DBStatement(@"__DBVARPrefix_lncursor"),
			null
		);
		#line hidden

		Sybase.PowerBuilder.IPBValue[] __PB_TEMP_DB__OutputVars0 = new Sybase.PowerBuilder.IPBValue[1];
		__PB_TEMP_DB__OutputVars0[0] = ldc_sumprn;
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Fetch(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			__DBVARPrefix_lncursor,
			new Sybase.PowerBuilder.DB.DBStatement(
				new Sybase.PowerBuilder.DB.DBOutputVarInfo[1] {
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false)
				},
				Sybase.PowerBuilder.DB.DBFetchDirection.Next
			),
			new Sybase.PowerBuilder.PBDataType[1] {
				Sybase.PowerBuilder.PBDataType.Decimal
			},
			__PB_TEMP_DB__OutputVars0
		);

		#line 34
		ldc_sumprn = (Sybase.PowerBuilder.PBDecimal) __PB_TEMP_DB__OutputVars0[0];
		#line hidden
		#line 35
		if ((Sybase.PowerBuilder.PBBoolean)((itr_sqlca.SQLCode != (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0)))| Sybase.PowerBuilder.WPF.PBSystemFunctions.IsNull((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldc_sumprn))))))
		#line hidden
		{
			#line 36
			ldc_sumprn.AssignFrom((Sybase.PowerBuilder.PBDecimal)(new Sybase.PowerBuilder.PBDecimal(0m)));
			#line hidden
		}

		#line 38
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Close(Sybase.PowerBuilder.WPF.PBSession.CurrentSession, __DBVARPrefix_lncursor);
		#line hidden
		#line 41
		ls_sqlins = new Sybase.PowerBuilder.PBString("");
		#line hidden
		#line 42
		ls_sqlins += new Sybase.PowerBuilder.PBString(" select protect_amt");
		#line hidden
		#line 43
		ls_sqlins += new Sybase.PowerBuilder.PBString(" from	isinsuremaster ");
		#line hidden
		#line 44
		ls_sqlins += new Sybase.PowerBuilder.PBString(" where insure_status = 1 ");
		#line hidden
		#line 45
		ls_sqlins += (new Sybase.PowerBuilder.PBString(" and instype_code = '")+ ls_instype)+ new Sybase.PowerBuilder.PBString("' ");
		#line hidden
		#line 46
		ls_sqlins += (new Sybase.PowerBuilder.PBString(" and member_no = '")+ ls_memno)+ new Sybase.PowerBuilder.PBString("' ");
		#line hidden
		#line 47
		ls_sqlins += new Sybase.PowerBuilder.PBString(" order by protectstart_date desc ");
		#line hidden

		__DBVARPrefix_inscursor = new Sybase.PowerBuilder.DB.DBCursor(
			@"__DBVARPrefix_inscursor",
			c__pbservice125.GetCurrentApplication().sqlsa
		);

		#line 50
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Prepare(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			c__pbservice125.GetCurrentApplication().sqlsa,
			((Sybase.PowerBuilder.PBString)ls_sqlins).Value,
			itr_sqlca
		);
		#line hidden

		if (__DBVARPrefix_inscursor == null)
		{
			__DBVARPrefix_inscursor = new Sybase.PowerBuilder.DB.DBCursor(
				@"__DBVARPrefix_inscursor",
				c__pbservice125.GetCurrentApplication().sqlsa
			);
		}

		#line 52
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.OpenDynamically3(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			__DBVARPrefix_inscursor,
			new Sybase.PowerBuilder.DB.DBStatement(@"__DBVARPrefix_inscursor"),
			null
		);
		#line hidden

		Sybase.PowerBuilder.IPBValue[] __PB_TEMP_DB__OutputVars1 = new Sybase.PowerBuilder.IPBValue[1];
		__PB_TEMP_DB__OutputVars1[0] = ldc_protcur;
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Fetch(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			__DBVARPrefix_inscursor,
			new Sybase.PowerBuilder.DB.DBStatement(
				new Sybase.PowerBuilder.DB.DBOutputVarInfo[1] {
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false)
				},
				Sybase.PowerBuilder.DB.DBFetchDirection.Next
			),
			new Sybase.PowerBuilder.PBDataType[1] {
				Sybase.PowerBuilder.PBDataType.Decimal
			},
			__PB_TEMP_DB__OutputVars1
		);

		#line 53
		ldc_protcur = (Sybase.PowerBuilder.PBDecimal) __PB_TEMP_DB__OutputVars1[0];
		#line hidden
		#line 54
		if ((Sybase.PowerBuilder.PBBoolean)(itr_sqlca.SQLCode != (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 55
			ldc_protcur.AssignFrom((Sybase.PowerBuilder.PBDecimal)(new Sybase.PowerBuilder.PBDecimal(0m)));
			#line hidden
		}

		#line 57
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Close(Sybase.PowerBuilder.WPF.PBSession.CurrentSession, __DBVARPrefix_inscursor);
		#line hidden

		Sybase.PowerBuilder.IPBValue[] __PB_TEMP_DB__OutputVars2 = new Sybase.PowerBuilder.IPBValue[3];
		__PB_TEMP_DB__OutputVars2[0] = li_inscaltype;
		__PB_TEMP_DB__OutputVars2[1] = ldc_lnexcept;
		__PB_TEMP_DB__OutputVars2[2] = li_insrecal;
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Select(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			new Sybase.PowerBuilder.DB.DBStatement(
				new Sybase.PowerBuilder.DB.DBStatement(
					new System.String[3] {
						"select inscal_type,lnexcept_amt,reins_type     from iscfinstype    where coop_id = ",
						"    and instype_code = ",
						"    "
					},
					new Sybase.PowerBuilder.IPBValue[2] {
						is_coopcontrol,
						ls_instype
					}
				),
				new Sybase.PowerBuilder.DB.DBOutputVarInfo[3] {
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false),
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false),
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false)
				}
			),
			new Sybase.PowerBuilder.IPBValue[2] {
				is_coopcontrol,
				ls_instype
			},
			__PB_TEMP_DB__OutputVars2,
			new Sybase.PowerBuilder.PBDataType[3] {
				Sybase.PowerBuilder.PBDataType.Int,
				Sybase.PowerBuilder.PBDataType.Decimal,
				Sybase.PowerBuilder.PBDataType.Int
			},
			itr_sqlca
		);
		li_inscaltype = (Sybase.PowerBuilder.PBInt) __PB_TEMP_DB__OutputVars2[0];
		ldc_lnexcept = (Sybase.PowerBuilder.PBDecimal) __PB_TEMP_DB__OutputVars2[1];

		#line 60
		li_insrecal = (Sybase.PowerBuilder.PBInt) __PB_TEMP_DB__OutputVars2[2];
		#line hidden
		#line 66
		if ((Sybase.PowerBuilder.PBBoolean)(itr_sqlca.SQLCode != (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 67
			li_inscaltype = new Sybase.PowerBuilder.PBInt(1);
			#line hidden
			#line 68
			ldc_lnexcept.AssignFrom((Sybase.PowerBuilder.PBDecimal)(new Sybase.PowerBuilder.PBDecimal(0m)));
			#line hidden
		}
		#line 72
		ldc_protall.AssignFrom((ldc_sumprn+ ldc_lnreq)- ldc_lnexcept);
		#line hidden
		#line 74
		if ((Sybase.PowerBuilder.PBBoolean)(ldc_protall< (Sybase.PowerBuilder.PBDecimal)(new Sybase.PowerBuilder.PBDecimal(0m))))
		#line hidden
		{
			#line 75
			ldc_protall.AssignFrom((Sybase.PowerBuilder.PBDecimal)(new Sybase.PowerBuilder.PBDecimal(0m)));
			#line hidden
		}
		#line 79
		if ((Sybase.PowerBuilder.PBBoolean)((li_insrecal == new Sybase.PowerBuilder.PBInt(1))& (ldc_protall<= ldc_protcur)))
		#line hidden
		{
			#line 80
			return new Sybase.PowerBuilder.PBInt(0);
			#line hidden
		}
		#line 84
		if ((Sybase.PowerBuilder.PBBoolean)(li_insrecal == new Sybase.PowerBuilder.PBInt(1)))
		#line hidden
		{
			#line 85
			ldc_protadd.AssignFrom(ldc_protall- ldc_protcur);
			#line hidden
		}
		else
		{
			#line 87
			ldc_protadd.AssignFrom(ldc_protall);
			#line hidden
		}
		#line 91
		ldc_sqlmain = new Sybase.PowerBuilder.PBString("");
		#line hidden
		#line 92
		ldc_sqlmain += new Sybase.PowerBuilder.PBString(" select	ins.protectstart_date, ins.protectend_date, insr.step_protect, insr.step_rate, insr.insure_minamt, insr.insure_maxamt ");
		#line hidden
		#line 93
		ldc_sqlmain += new Sybase.PowerBuilder.PBString(" from	isinsuremain ins ");
		#line hidden
		#line 94
		ldc_sqlmain += new Sybase.PowerBuilder.PBString(" 		join isinsuremainmap insm on ins.coop_id = insm.coop_id and ins.insmain_no = insm.insmain_no ");
		#line hidden
		#line 95
		ldc_sqlmain += new Sybase.PowerBuilder.PBString(" 		join isinsuremainrate insr on ins.coop_id = insr.coop_id and ins.insmain_no = insr.insmain_no ");
		#line hidden
		#line 96
		ldc_sqlmain += (new Sybase.PowerBuilder.PBString(" where insm.instype_code = '")+ ls_instype)+ new Sybase.PowerBuilder.PBString("' ");
		#line hidden
		#line 97
		ldc_sqlmain += (new Sybase.PowerBuilder.PBString(" and to_date('")+ Sybase.PowerBuilder.WPF.PBSystemFunctions.String((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldtm_paydate))), new Sybase.PowerBuilder.PBString("yyyymmdd")))+ new Sybase.PowerBuilder.PBString("','yyyymmdd') between ins.protectstart_date and ins.protectend_date ");
		#line hidden
		#line 98
		ldc_sqlmain += (new Sybase.PowerBuilder.PBString(" and ")+ Sybase.PowerBuilder.WPF.PBSystemFunctions.String((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldc_protall)))))+ new Sybase.PowerBuilder.PBString(" between insr.protect_from and insr.protect_to ");
		#line hidden

		__DBVARPrefix_maincursor = new Sybase.PowerBuilder.DB.DBCursor(
			@"__DBVARPrefix_maincursor",
			c__pbservice125.GetCurrentApplication().sqlsa
		);

		#line 101
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Prepare(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			c__pbservice125.GetCurrentApplication().sqlsa,
			((Sybase.PowerBuilder.PBString)ldc_sqlmain).Value,
			itr_sqlca
		);
		#line hidden

		if (__DBVARPrefix_maincursor == null)
		{
			__DBVARPrefix_maincursor = new Sybase.PowerBuilder.DB.DBCursor(
				@"__DBVARPrefix_maincursor",
				c__pbservice125.GetCurrentApplication().sqlsa
			);
		}

		#line 103
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.OpenDynamically3(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			__DBVARPrefix_maincursor,
			new Sybase.PowerBuilder.DB.DBStatement(@"__DBVARPrefix_maincursor"),
			null
		);
		#line hidden

		Sybase.PowerBuilder.IPBValue[] __PB_TEMP_DB__OutputVars3 = new Sybase.PowerBuilder.IPBValue[6];
		__PB_TEMP_DB__OutputVars3[0] = ldtm_protstart;
		__PB_TEMP_DB__OutputVars3[1] = ldtm_protend;
		__PB_TEMP_DB__OutputVars3[2] = ldc_stepprot;
		__PB_TEMP_DB__OutputVars3[3] = ldc_steprate;
		__PB_TEMP_DB__OutputVars3[4] = ldc_ratemin;
		__PB_TEMP_DB__OutputVars3[5] = ldc_ratemax;
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Fetch(Sybase.PowerBuilder.WPF.PBSession.CurrentSession,
			__DBVARPrefix_maincursor,
			new Sybase.PowerBuilder.DB.DBStatement(
				new Sybase.PowerBuilder.DB.DBOutputVarInfo[6] {
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false),
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false),
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false),
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false),
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false),
					new Sybase.PowerBuilder.DB.DBOutputVarInfo(false)
				},
				Sybase.PowerBuilder.DB.DBFetchDirection.Next
			),
			new Sybase.PowerBuilder.PBDataType[6] {
				Sybase.PowerBuilder.PBDataType.DateTime,
				Sybase.PowerBuilder.PBDataType.DateTime,
				Sybase.PowerBuilder.PBDataType.Decimal,
				Sybase.PowerBuilder.PBDataType.Decimal,
				Sybase.PowerBuilder.PBDataType.Decimal,
				Sybase.PowerBuilder.PBDataType.Decimal
			},
			__PB_TEMP_DB__OutputVars3
		);
		ldtm_protstart = (Sybase.PowerBuilder.PBDateTime) __PB_TEMP_DB__OutputVars3[0];
		ldtm_protend = (Sybase.PowerBuilder.PBDateTime) __PB_TEMP_DB__OutputVars3[1];
		ldc_stepprot = (Sybase.PowerBuilder.PBDecimal) __PB_TEMP_DB__OutputVars3[2];
		ldc_steprate = (Sybase.PowerBuilder.PBDecimal) __PB_TEMP_DB__OutputVars3[3];
		ldc_ratemin = (Sybase.PowerBuilder.PBDecimal) __PB_TEMP_DB__OutputVars3[4];

		#line 104
		ldc_ratemax = (Sybase.PowerBuilder.PBDecimal) __PB_TEMP_DB__OutputVars3[5];
		#line hidden
		#line 105
		if ((Sybase.PowerBuilder.PBBoolean)(itr_sqlca.SQLCode != (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(0))))
		#line hidden
		{
			#line 106
			ithw_exception.Text = ((((new Sybase.PowerBuilder.PBString("ไม่พบข้อมูลอัตราเบี้ยประกันประเภท ")+ ls_instype)+ new Sybase.PowerBuilder.PBString(" ที่อยู่ในช่วงวันที่ "))+ Sybase.PowerBuilder.WPF.PBSystemFunctions.String((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldtm_paydate))), new Sybase.PowerBuilder.PBString("dd/mm/yyyy")))+ new Sybase.PowerBuilder.PBString(" ช่วงทุนประกัน "))+ Sybase.PowerBuilder.WPF.PBSystemFunctions.String((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldc_protall))));
			#line hidden
			#line 107
			throw new Sybase.PowerBuilder.PBExceptionE(ithw_exception);
			#line hidden
		}

		#line 109
		Sybase.PowerBuilder.DSI.PBSQL.DSISQLFunc.Close(Sybase.PowerBuilder.WPF.PBSession.CurrentSession, __DBVARPrefix_maincursor);
		#line hidden
		#line 112
		ldc_mod.AssignFrom((Sybase.PowerBuilder.PBDecimal)(Sybase.PowerBuilder.WPF.PBSystemFunctions.Mod((Sybase.PowerBuilder.PBDouble)(ldc_protadd), (Sybase.PowerBuilder.PBDouble)(ldc_stepprot))));
		#line hidden
		#line 114
		if ((Sybase.PowerBuilder.PBBoolean)(ldc_mod> (Sybase.PowerBuilder.PBDecimal)(new Sybase.PowerBuilder.PBDecimal(0m))))
		#line hidden
		{
			#line 115
			ldc_protadd.AssignFrom((ldc_protadd- ldc_mod)+ ldc_stepprot);
			#line hidden
		}
		#line 119
		ldc_stepall.AssignFrom(Sybase.PowerBuilder.WPF.PBSystemFunctions.Truncate((ldc_protadd/ ldc_stepprot).ToPBDecimal(-1), new Sybase.PowerBuilder.PBInt(0)));
		#line hidden
		#line 122
		ldc_insamt.AssignFrom(ldc_stepall* ldc_steprate);
		#line hidden
		#line 125
		if ((Sybase.PowerBuilder.PBBoolean)(li_inscaltype == new Sybase.PowerBuilder.PBInt(2)))
		#line hidden
		{
			#line 126
			li_dayinsmain = (Sybase.PowerBuilder.PBInt)(Sybase.PowerBuilder.WPF.PBSystemFunctions.DaysAfter(Sybase.PowerBuilder.WPF.PBSystemFunctions.Date((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldtm_protstart)))), Sybase.PowerBuilder.WPF.PBSystemFunctions.Date((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldtm_protend)))))+ (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)));
			#line hidden
			#line 127
			li_dayprot = (Sybase.PowerBuilder.PBInt)(Sybase.PowerBuilder.WPF.PBSystemFunctions.DaysAfter(Sybase.PowerBuilder.WPF.PBSystemFunctions.Date((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldtm_paydate)))), Sybase.PowerBuilder.WPF.PBSystemFunctions.Date((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(ldtm_protend)))))+ (Sybase.PowerBuilder.PBLong)(new Sybase.PowerBuilder.PBInt(1)));
			#line hidden
			#line 129
			ldc_insamt.AssignFrom(ldc_insamt* (Sybase.PowerBuilder.WPF.PBSystemFunctions.Dec((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(li_dayprot))))/ Sybase.PowerBuilder.WPF.PBSystemFunctions.Dec((Sybase.PowerBuilder.PBAny)(((Sybase.PowerBuilder.PBAny)(li_dayinsmain))))));
			#line hidden
		}
		#line 132
		astr_lninsure.insprotect_amt.AssignFrom(ldc_protadd);
		#line hidden
		#line 133
		astr_lninsure.ins_amt.AssignFrom(ldc_insamt);
		#line hidden
		#line 135
		return new Sybase.PowerBuilder.PBInt(1);
		#line hidden
	}

	#line hidden
	[Sybase.PowerBuilder.PBEventAttribute("create")]
	public override void create()
	{
		#line hidden
		#line hidden
		base.create();
		#line hidden
		#line hidden
		;
		#line hidden
	}

	#line hidden
	[Sybase.PowerBuilder.PBEventAttribute("destroy")]
	public override void destroy()
	{
		#line hidden
		#line hidden
		this.TriggerEvent(new Sybase.PowerBuilder.PBString("destructor"));
		#line hidden
		#line hidden
		base.destroy();
		#line hidden
	}

	#line 1 "n_cst_loansrv_insure.constructor"
	#line hidden
	[Sybase.PowerBuilder.PBEventAttribute("constructor")]
	[Sybase.PowerBuilder.PBEventToken(Sybase.PowerBuilder.PBEventTokens.pbm_constructor)]
	public override Sybase.PowerBuilder.PBLong constructor()
	{
		#line hidden
		Sybase.PowerBuilder.PBLong ancestorreturnvalue = Sybase.PowerBuilder.PBLong.DefaultValue;
		#line 3
		ithw_exception = (Sybase.PowerBuilder.PBException)this.CreateInstance(typeof(Sybase.PowerBuilder.PBException), 0);
		#line hidden
		return new Sybase.PowerBuilder.PBLong(0);
	}

	public Sybase.PowerBuilder.PBLong of_parsetoarray_3_345252223_3_509010394(c__n_cst_stringservice this__object, Sybase.PowerBuilder.PBString as_source, Sybase.PowerBuilder.PBString as_delimiter, ref Sybase.PowerBuilder.PBArray as_array)
	{
		Sybase.PowerBuilder.PBLong return_value = this__object.of_parsetoarray_3_345252223(as_source, as_delimiter, ref as_array);
		return return_value;
	}


	void _init()
	{
		this.CreateEvent += new Sybase.PowerBuilder.PBEventHandler(this.create);
		this.DestroyEvent += new Sybase.PowerBuilder.PBEventHandler(this.destroy);
		this.ConstructorEvent += new Sybase.PowerBuilder.PBM_EventHandler(this.constructor);

		if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
		{
		}
	}

	public c__n_cst_loansrv_insure()
	{
		_init();
	}


	public static explicit operator c__n_cst_loansrv_insure(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedAnyArray)
		{
			Sybase.PowerBuilder.PBUnboundedAnyArray a = (Sybase.PowerBuilder.PBUnboundedAnyArray)v.Value;
			c__n_cst_loansrv_insure vv = new c__n_cst_loansrv_insure();
			if (vv.FromUnboundedAnyArray(a) > 0)
			{
				return vv;
			}
			else
			{
				return null;
			}
		}
		else
		{
			return (c__n_cst_loansrv_insure) v.Value;
		}
	}
}
 