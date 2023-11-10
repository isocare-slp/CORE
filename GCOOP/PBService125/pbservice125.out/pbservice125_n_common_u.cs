using Sybase.PowerBuilder.WCFNVO;

//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbservice125.pbl\\pbservice125.pblx\\n_common.sru"
#line hidden
namespace pbservice125
{
	#line 1 "n_common"
	#line hidden
	[Sybase.PowerBuilder.PBGroupAttribute("n_common",Sybase.PowerBuilder.PBGroupType.UserObject,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbservice125.pbl\\pbservice125.pblx",null,"pbservice125")]
	internal class c__n_common : Sybase.PowerBuilder.PBNonVisualObject
	{
		#line hidden

		#line 1 "{pbservice125}n_common.of_getnextworkday(ISWRW)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_getnextworkday", new string[]{"string", "datetime", "ref datetime"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBInt of_getnextworkday(Sybase.PowerBuilder.PBString as_wspass, Sybase.PowerBuilder.PBDateTime adtm_fromdate, ref Sybase.PowerBuilder.PBDateTime adtm_nextworkdate)
		{
			#line hidden
			Sybase.PowerBuilder.PBInt result = Sybase.PowerBuilder.PBInt.DefaultValue;
			c__n_cst_dbconnectservice in_conn = null;
			c__n_cst_calendarservice ln_service = null;
			Sybase.PowerBuilder.PBThrowable ex = null;
			#line 3
			result = new Sybase.PowerBuilder.PBInt(0);
			#line hidden
			#line 5
			in_conn = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
			try
			{
				try
				{
					#line 7
					in_conn.of_connectdb(as_wspass);
					#line hidden
					#line 9
					ln_service = (c__n_cst_calendarservice)this.CreateInstance(typeof(c__n_cst_calendarservice), 0);
					#line hidden
					#line 10
					ln_service.of_initservice(in_conn);
					#line hidden
					#line 12
					result = ln_service.of_getnextworkday(adtm_fromdate, ref adtm_nextworkdate);
					#line hidden
					#line 13
					in_conn.of_disconnectdb();
					#line hidden
				}
				catch (System.DivideByZeroException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0001);
					throw new System.Exception();
				}
				catch (System.NullReferenceException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0002);
					throw new System.Exception();
				}
				catch (System.IndexOutOfRangeException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0003);
					throw new System.Exception();
				}
			}
			#line 14
			catch (Sybase.PowerBuilder.PBThrowableE __PB_TEMP_ex__temp)
			#line hidden
			{
				ex = __PB_TEMP_ex__temp.E;
				#line 15
				in_conn.of_disconnectdb();
				#line hidden
				#line 16
				throw new Sybase.PowerBuilder.PBThrowableE(ex);
				#line hidden
			}
			#line 18
			return result;
			#line hidden
		}

		#line 1 "{pbservice125}n_common.of_getnewdocno(SSSS)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_getnewdocno", new string[]{"string", "string", "string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBString of_getnewdocno(Sybase.PowerBuilder.PBString as_wspass, Sybase.PowerBuilder.PBString as_coopid, Sybase.PowerBuilder.PBString doccode)
		{
			#line hidden
			Sybase.PowerBuilder.PBString result = Sybase.PowerBuilder.PBString.DefaultValue;
			c__n_cst_dbconnectservice in_conn = null;
			c__n_cst_doccontrolservice ln_service = null;
			Sybase.PowerBuilder.PBThrowable ex = null;
			#line 3
			result = new Sybase.PowerBuilder.PBString("");
			#line hidden
			#line 5
			in_conn = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
			try
			{
				try
				{
					#line 7
					in_conn.of_connectdb(as_wspass);
					#line hidden
					#line 9
					ln_service = (c__n_cst_doccontrolservice)this.CreateInstance(typeof(c__n_cst_doccontrolservice), 0);
					#line hidden
					#line 10
					ln_service.of_settrans(in_conn);
					#line hidden
					#line 12
					result = ln_service.of_getnewdocno(as_coopid, doccode);
					#line hidden
					#line 13
					in_conn.of_disconnectdb();
					#line hidden
				}
				catch (System.DivideByZeroException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0001);
					throw new System.Exception();
				}
				catch (System.NullReferenceException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0002);
					throw new System.Exception();
				}
				catch (System.IndexOutOfRangeException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0003);
					throw new System.Exception();
				}
			}
			#line 14
			catch (Sybase.PowerBuilder.PBThrowableE __PB_TEMP_ex__temp)
			#line hidden
			{
				ex = __PB_TEMP_ex__temp.E;
				#line 15
				in_conn.of_disconnectdb();
				#line hidden
				#line 16
				throw new Sybase.PowerBuilder.PBThrowableE(ex);
				#line hidden
			}
			#line 18
			return result;
			#line hidden
		}

		#line 1 "{pbservice125}n_common.of_isworkingdate(BSW)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_isworkingdate", new string[]{"string", "datetime"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBBoolean of_isworkingdate(Sybase.PowerBuilder.PBString as_wspass, Sybase.PowerBuilder.PBDateTime adtm_checkdate)
		{
			#line hidden
			Sybase.PowerBuilder.PBBoolean result = Sybase.PowerBuilder.PBBoolean.DefaultValue;
			c__n_cst_dbconnectservice in_conn = null;
			c__n_cst_calendarservice ln_service = null;
			Sybase.PowerBuilder.PBThrowable ex = null;
			#line 3
			result = new Sybase.PowerBuilder.PBBoolean(false);
			#line hidden
			#line 5
			in_conn = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
			try
			{
				try
				{
					#line 7
					in_conn.of_connectdb(as_wspass);
					#line hidden
					#line 9
					ln_service = (c__n_cst_calendarservice)this.CreateInstance(typeof(c__n_cst_calendarservice), 0);
					#line hidden
					#line 10
					ln_service.of_initservice(in_conn);
					#line hidden
					#line 12
					result = ln_service.of_isworkingdate(adtm_checkdate);
					#line hidden
					#line 13
					in_conn.of_disconnectdb();
					#line hidden
				}
				catch (System.DivideByZeroException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0001);
					throw new System.Exception();
				}
				catch (System.NullReferenceException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0002);
					throw new System.Exception();
				}
				catch (System.IndexOutOfRangeException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0003);
					throw new System.Exception();
				}
			}
			#line 14
			catch (Sybase.PowerBuilder.PBThrowableE __PB_TEMP_ex__temp)
			#line hidden
			{
				ex = __PB_TEMP_ex__temp.E;
				#line 15
				in_conn.of_disconnectdb();
				#line hidden
				#line 16
				throw new Sybase.PowerBuilder.PBThrowableE(ex);
				#line hidden
			}
			#line 18
			return result;
			#line hidden
		}

		#line 1 "{pbservice125}n_common.of_getdddwxml(SSS)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_getdddwxml", new string[]{"string", "string"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBString of_getdddwxml(Sybase.PowerBuilder.PBString as_wspass, Sybase.PowerBuilder.PBString as_dddwobj)
		{
			#line hidden
			Sybase.PowerBuilder.PBString result = Sybase.PowerBuilder.PBString.DefaultValue;
			c__n_cst_dbconnectservice in_conn = null;
			c__n_cst_utility ln_service = null;
			Sybase.PowerBuilder.PBThrowable ex = null;
			#line 3
			in_conn = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
			try
			{
				try
				{
					#line 5
					in_conn.of_connectdb(as_wspass);
					#line hidden
					#line 7
					ln_service = (c__n_cst_utility)this.CreateInstance(typeof(c__n_cst_utility), 0);
					#line hidden
					#line 9
					result = ln_service.of_getdddwxml(as_dddwobj);
					#line hidden
					#line 10
					in_conn.of_disconnectdb();
					#line hidden
				}
				catch (System.DivideByZeroException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0001);
					throw new System.Exception();
				}
				catch (System.NullReferenceException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0002);
					throw new System.Exception();
				}
				catch (System.IndexOutOfRangeException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0003);
					throw new System.Exception();
				}
			}
			#line 11
			catch (Sybase.PowerBuilder.PBThrowableE __PB_TEMP_ex__temp)
			#line hidden
			{
				ex = __PB_TEMP_ex__temp.E;
				#line 12
				in_conn.of_disconnectdb();
				#line hidden
				#line 13
				throw new Sybase.PowerBuilder.PBThrowableE(ex);
				#line hidden
			}
			#line 15
			return result;
			#line hidden
		}

		#line 1 "{pbservice125}n_common.of_dwexportexcel_etn(ISCstr_rptexcel.)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_dwexportexcel_etn", new string[]{"string", "str_rptexcel"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBInt of_dwexportexcel_etn(Sybase.PowerBuilder.PBString as_wspass, c__str_rptexcel astr_rptexcel)
		{
			#line hidden
			Sybase.PowerBuilder.PBInt result = Sybase.PowerBuilder.PBInt.DefaultValue;
			c__n_cst_dbconnectservice in_conn = null;
			c__n_cst_dwexportexcel ln_service = null;
			Sybase.PowerBuilder.PBException ex = null;
			#line 3
			result = new Sybase.PowerBuilder.PBInt(0);
			#line hidden
			#line 5
			in_conn = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
			try
			{
				try
				{
					#line 7
					in_conn.of_connectdb(as_wspass);
					#line hidden
					#line 9
					ln_service = (c__n_cst_dwexportexcel)this.CreateInstance(typeof(c__n_cst_dwexportexcel), 0);
					#line hidden
					#line 12
					result = ln_service.of_dwexportexcel_etn(((c__str_rptexcel)(Sybase.PowerBuilder.PBSystemFunctions.PBClone(astr_rptexcel))));
					#line hidden
					#line 13
					in_conn.of_disconnectdb();
					#line hidden
				}
				catch (System.DivideByZeroException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0001);
					throw new System.Exception();
				}
				catch (System.NullReferenceException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0002);
					throw new System.Exception();
				}
				catch (System.IndexOutOfRangeException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0003);
					throw new System.Exception();
				}
			}
			#line 14
			catch (Sybase.PowerBuilder.PBExceptionE __PB_TEMP_ex__temp)
			#line hidden
			{
				ex = __PB_TEMP_ex__temp.E;
				#line 15
				in_conn.of_disconnectdb();
				#line hidden
				#line 16
				throw new Sybase.PowerBuilder.PBExceptionE(ex);
				#line hidden
			}
			#line 18
			return result;
			#line hidden
		}

		#line 1 "{pbservice125}n_common.of_dwexportexcel_rpt(ISRCstr_rptexcel.)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_dwexportexcel_rpt", new string[]{"string", "ref str_rptexcel"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBInt of_dwexportexcel_rpt_2_2005728673<T1>(Sybase.PowerBuilder.PBString as_wspass, ref T1 astr_rptexcel) where T1 : c__str_rptexcel
		{
			#line hidden
			Sybase.PowerBuilder.PBInt result = Sybase.PowerBuilder.PBInt.DefaultValue;
			c__n_cst_dbconnectservice in_conn = null;
			c__n_cst_dwexportexcel ln_service = null;
			Sybase.PowerBuilder.PBException ex = null;
			#line 3
			in_conn = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
			try
			{
				try
				{
					#line 5
					in_conn.of_connectdb(as_wspass);
					#line hidden
					#line 7
					ln_service = (c__n_cst_dwexportexcel)this.CreateInstance(typeof(c__n_cst_dwexportexcel), 0);
					#line hidden
					#line 9
					result = ln_service.of_dwexportexcel_rpt((c__str_rptexcel)(c__str_rptexcel)(((c__str_rptexcel)(Sybase.PowerBuilder.PBSystemFunctions.PBClone(astr_rptexcel)))));
					#line hidden
					#line 10
					in_conn.of_disconnectdb();
					#line hidden
				}
				catch (System.DivideByZeroException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0001);
					throw new System.Exception();
				}
				catch (System.NullReferenceException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0002);
					throw new System.Exception();
				}
				catch (System.IndexOutOfRangeException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0003);
					throw new System.Exception();
				}
			}
			#line 11
			catch (Sybase.PowerBuilder.PBExceptionE __PB_TEMP_ex__temp)
			#line hidden
			{
				ex = __PB_TEMP_ex__temp.E;
				#line 12
				in_conn.of_disconnectdb();
				#line hidden
				#line 13
				throw new Sybase.PowerBuilder.PBExceptionE(ex);
				#line hidden
			}
			#line 15
			return result;
			#line hidden
		}

		#line 1 "{pbservice125}n_common.of_lastdayofmonth(YSY)"
		#line hidden
		[Sybase.PowerBuilder.PBFunctionAttribute("of_lastdayofmonth", new string[]{"string", "date"}, Sybase.PowerBuilder.PBModifier.Public, Sybase.PowerBuilder.PBFunctionType.kPowerscriptFunction)]
		public virtual Sybase.PowerBuilder.PBDate of_lastdayofmonth(Sybase.PowerBuilder.PBString as_wspass, Sybase.PowerBuilder.PBDate ad_source)
		{
			#line hidden
			Sybase.PowerBuilder.PBDate result = Sybase.PowerBuilder.PBDate.DefaultValue;
			c__n_cst_dbconnectservice in_conn = null;
			c__n_cst_datetimeservice ln_service = null;
			Sybase.PowerBuilder.PBException ex = null;
			#line 4
			in_conn = (c__n_cst_dbconnectservice)this.CreateInstance(typeof(c__n_cst_dbconnectservice), 0);
			#line hidden
			try
			{
				try
				{
					#line 6
					in_conn.of_connectdb(as_wspass);
					#line hidden
					#line 8
					ln_service = (c__n_cst_datetimeservice)this.CreateInstance(typeof(c__n_cst_datetimeservice), 0);
					#line hidden
					#line 11
					result = ln_service.of_lastdayofmonth(ad_source);
					#line hidden
					#line 12
					in_conn.of_disconnectdb();
					#line hidden
				}
				catch (System.DivideByZeroException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0001);
					throw new System.Exception();
				}
				catch (System.NullReferenceException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0002);
					throw new System.Exception();
				}
				catch (System.IndexOutOfRangeException)
				{
					Sybase.PowerBuilder.PBRuntimeError.Throw(Sybase.PowerBuilder.RuntimeErrorCode.RT_R0003);
					throw new System.Exception();
				}
			}
			#line 13
			catch (Sybase.PowerBuilder.PBExceptionE __PB_TEMP_ex__temp)
			#line hidden
			{
				ex = __PB_TEMP_ex__temp.E;
				#line 14
				in_conn.of_disconnectdb();
				#line hidden
				#line 15
				throw new Sybase.PowerBuilder.PBExceptionE(ex);
				#line hidden
			}
			#line 17
			return result;
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

		void _init()
		{
			this.CreateEvent += new Sybase.PowerBuilder.PBEventHandler(this.create);
			this.DestroyEvent += new Sybase.PowerBuilder.PBEventHandler(this.destroy);

			if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
			{
			}
		}

		public c__n_common()
		{
			_init();
		}


		public static explicit operator c__n_common(Sybase.PowerBuilder.PBAny v)
		{
			if (v.Value is Sybase.PowerBuilder.PBUnboundedAnyArray)
			{
				Sybase.PowerBuilder.PBUnboundedAnyArray a = (Sybase.PowerBuilder.PBUnboundedAnyArray)v.Value;
				c__n_common vv = new c__n_common();
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
				return (c__n_common) v.Value;
			}
		}
	}
}
 