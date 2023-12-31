//**************************************************************************
//
//                        Sybase Inc. 
//
//    THIS IS A TEMPORARY FILE GENERATED BY POWERBUILDER. IF YOU MODIFY 
//    THIS FILE, YOU DO SO AT YOUR OWN RISK. SYBASE DOES NOT PROVIDE 
//    SUPPORT FOR .NET ASSEMBLIES BUILT WITH USER-MODIFIED CS FILES. 
//
//**************************************************************************

#line 1 "c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx\\str_lncalperiod.srs"
#line hidden
[Sybase.PowerBuilder.PBGroupAttribute("str_lncalperiod",Sybase.PowerBuilder.PBGroupType.Structure,"","c:\\gcoop_all\\core\\gcoop\\pbservice125\\pbsrvbiz\\loansrv.pbl\\loansrv.pblx",null,"pbservice125")]
internal class c__str_lncalperiod : Sybase.PowerBuilder.PBStructure
{
	public Sybase.PowerBuilder.PBString loantype_code = Sybase.PowerBuilder.PBString.DefaultValue;
	public Sybase.PowerBuilder.PBInt loanpayment_type = Sybase.PowerBuilder.PBInt.DefaultValue;
	public Sybase.PowerBuilder.PBInt calperiod_maxinstallment = Sybase.PowerBuilder.PBInt.DefaultValue;
	public Sybase.PowerBuilder.PBDecimal calperiod_prnamt = new Sybase.PowerBuilder.PBDecimal(0m);
	public Sybase.PowerBuilder.PBDecimal calperiod_intrate = new Sybase.PowerBuilder.PBDecimal(0m);
	public Sybase.PowerBuilder.PBInt period_installment = Sybase.PowerBuilder.PBInt.DefaultValue;
	public Sybase.PowerBuilder.PBDecimal period_payment = new Sybase.PowerBuilder.PBDecimal(0m);
	public Sybase.PowerBuilder.PBDecimal period_lastpayment = new Sybase.PowerBuilder.PBDecimal(0m);

	public override object Clone()
	{
		c__str_lncalperiod vv = (c__str_lncalperiod)base.Clone();
 		vv.loantype_code = loantype_code;
		vv.loanpayment_type = loanpayment_type;
		vv.calperiod_maxinstallment = calperiod_maxinstallment;
		vv.calperiod_prnamt = calperiod_prnamt;
		vv.calperiod_intrate = calperiod_intrate;
		vv.period_installment = period_installment;
		vv.period_payment = period_payment;
		vv.period_lastpayment = period_lastpayment;
		return vv;
	}

	public static explicit operator c__str_lncalperiod(Sybase.PowerBuilder.PBAny v)
	{
		if (v.Value is Sybase.PowerBuilder.PBUnboundedArray)
		{
			Sybase.PowerBuilder.PBUnboundedArray a = (Sybase.PowerBuilder.PBUnboundedArray)v;
			c__str_lncalperiod vv = new c__str_lncalperiod();
			vv.loantype_code = (Sybase.PowerBuilder.PBString)((Sybase.PowerBuilder.PBAny)a[1]);
			vv.loanpayment_type = (Sybase.PowerBuilder.PBInt)((Sybase.PowerBuilder.PBAny)a[2]);
			vv.calperiod_maxinstallment = (Sybase.PowerBuilder.PBInt)((Sybase.PowerBuilder.PBAny)a[3]);
			vv.calperiod_prnamt = (Sybase.PowerBuilder.PBDecimal)((Sybase.PowerBuilder.PBAny)a[4]);
			vv.calperiod_intrate = (Sybase.PowerBuilder.PBDecimal)((Sybase.PowerBuilder.PBAny)a[5]);
			vv.period_installment = (Sybase.PowerBuilder.PBInt)((Sybase.PowerBuilder.PBAny)a[6]);
			vv.period_payment = (Sybase.PowerBuilder.PBDecimal)((Sybase.PowerBuilder.PBAny)a[7]);
			vv.period_lastpayment = (Sybase.PowerBuilder.PBDecimal)((Sybase.PowerBuilder.PBAny)a[8]);

			return vv;
		}
		else
		{
			return (c__str_lncalperiod) v.Value;
		}
	}

	public override Sybase.PowerBuilder.PBUnboundedAnyArray ToUnboundedAnyArray()
	{
		Sybase.PowerBuilder.PBUnboundedAnyArray a = new Sybase.PowerBuilder.PBUnboundedAnyArray();
		a.Add(new Sybase.PowerBuilder.PBAny(this.loantype_code));
		a.Add(new Sybase.PowerBuilder.PBAny(this.loanpayment_type));
		a.Add(new Sybase.PowerBuilder.PBAny(this.calperiod_maxinstallment));
		a.Add(new Sybase.PowerBuilder.PBAny(this.calperiod_prnamt));
		a.Add(new Sybase.PowerBuilder.PBAny(this.calperiod_intrate));
		a.Add(new Sybase.PowerBuilder.PBAny(this.period_installment));
		a.Add(new Sybase.PowerBuilder.PBAny(this.period_payment));
		a.Add(new Sybase.PowerBuilder.PBAny(this.period_lastpayment));

		return a;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_lncalperiod")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Ansi )]
internal struct c__str_lncalperiod_ansi
{
	public string loantype_code;
	public short loanpayment_type;
	public short calperiod_maxinstallment;
	public decimal calperiod_prnamt;
	public decimal calperiod_intrate;
	public short period_installment;
	public decimal period_payment;
	public decimal period_lastpayment;

	public static implicit operator c__str_lncalperiod_ansi(c__str_lncalperiod other)
	{

		c__str_lncalperiod_ansi ret = new c__str_lncalperiod_ansi();

		ret.loantype_code = other.loantype_code;

		ret.loanpayment_type = other.loanpayment_type;

		ret.calperiod_maxinstallment = other.calperiod_maxinstallment;

		ret.calperiod_prnamt = other.calperiod_prnamt;

		ret.calperiod_intrate = other.calperiod_intrate;

		ret.period_installment = other.period_installment;

		ret.period_payment = other.period_payment;

		ret.period_lastpayment = other.period_lastpayment;

		return ret;
	}

	public static implicit operator c__str_lncalperiod(c__str_lncalperiod_ansi other)
	{

		c__str_lncalperiod ret = new c__str_lncalperiod();

		ret.loantype_code = other.loantype_code;

		ret.loanpayment_type = other.loanpayment_type;

		ret.calperiod_maxinstallment = other.calperiod_maxinstallment;

		ret.calperiod_prnamt = other.calperiod_prnamt;

		ret.calperiod_intrate = other.calperiod_intrate;

		ret.period_installment = other.period_installment;

		ret.period_payment = other.period_payment;

		ret.period_lastpayment = other.period_lastpayment;

		return ret;
	}
}


[Sybase.PowerBuilder.PBStructureLayoutAttribute("str_lncalperiod")]
[ System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1, CharSet = System.Runtime.InteropServices.CharSet.Unicode )]
internal struct c__str_lncalperiod_unicode
{
	public string loantype_code;
	public short loanpayment_type;
	public short calperiod_maxinstallment;
	public decimal calperiod_prnamt;
	public decimal calperiod_intrate;
	public short period_installment;
	public decimal period_payment;
	public decimal period_lastpayment;

	public static implicit operator c__str_lncalperiod_unicode(c__str_lncalperiod other)
	{

		c__str_lncalperiod_unicode ret = new c__str_lncalperiod_unicode();

		ret.loantype_code = other.loantype_code;

		ret.loanpayment_type = other.loanpayment_type;

		ret.calperiod_maxinstallment = other.calperiod_maxinstallment;

		ret.calperiod_prnamt = other.calperiod_prnamt;

		ret.calperiod_intrate = other.calperiod_intrate;

		ret.period_installment = other.period_installment;

		ret.period_payment = other.period_payment;

		ret.period_lastpayment = other.period_lastpayment;

		return ret;
	}

	public static implicit operator c__str_lncalperiod(c__str_lncalperiod_unicode other)
	{

		c__str_lncalperiod ret = new c__str_lncalperiod();

		ret.loantype_code = other.loantype_code;

		ret.loanpayment_type = other.loanpayment_type;

		ret.calperiod_maxinstallment = other.calperiod_maxinstallment;

		ret.calperiod_prnamt = other.calperiod_prnamt;

		ret.calperiod_intrate = other.calperiod_intrate;

		ret.period_installment = other.period_installment;

		ret.period_payment = other.period_payment;

		ret.period_lastpayment = other.period_lastpayment;

		return ret;
	}
}
 