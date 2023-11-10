
//**************************************************************************
//		Copyright  Sybase, Inc. 1998-2006
//						 All Rights reserved.
//
//	Sybase, Inc. ("Sybase") claims copyright in this
//	program and documentation as an unpublished work, versions of
//	which were first licensed on the date indicated in the foregoing
//	notice.  Claim of copyright does not imply waiver of Sybase's
//	other rights.
//
//	 This code is generated by the PowerBuilder HTML DataWindow generator.
//	 It is provided subject to the terms of the Sybase License Agreement
//	 for use as is, without alteration or modification.  
//	 Sybase shall have no obligation to provide support or error correction 
//	 services with respect to any altered or modified versions of this code.  
//
//       ***********************************************************
//       **     DO NOT MODIFY OR ALTER THIS CODE IN ANY WAY       **
//       ***********************************************************
//
//       ***************************************************************
//       ** IMPLEMENTATION DETAILS SUBJECT TO CHANGE WITHOUT NOTICE.  **
//       **            DO NOT RELY ON IMPLEMENTATION!!!!		      **
//       ***************************************************************
//
// Use the public interface only.
//**************************************************************************


function DW_parseNumberString(inString, bFloat)
{
	var newString = DW_Trim(inString);		
	if (newString == "")		
		return 0;		
	else			
	{
		newString = DW_ChangeDecimalCharAndGroupCharToStd(newString);
		var n = 0;
		if (bFloat)
			n = parseFloat(newString);
		else
			n = parseInt(inString);
		if(isNaN(Number(inString)) || isNaN(n))
			return 0;		
		return n;
	}
}

function DW_ParseFloatString(inString)
{
	return DW_parseNumberString(inString, true);
}

function DW_ParseIntString(inString)
{
	return DW_parseNumberString(inString, false);
} function DW_FloatParse(inString)
{
    var result = new DW_NumberClass();
    var newString = DW_ChangeDecimalCharAndGroupCharToStd(inString);
    
    if (DW_parseNumberStringAgainstMask(newString, result, true))
    {
        return result.number;
    }
    else
        return null;
}

function DW_IntParse(inString)
{
    var result = new DW_NumberClass();
    
    var newString = DW_ChangeDecimalCharAndGroupCharToStd(inString);
    if (DW_parseNumberStringAgainstMask(newString, result, false))
    {
        return result.number;
    }
    else
        return null;
}

function DW_parseNumberStringAgainstMask(inString, outNumber, bFloat)
{
	var Mask = gMask;
    
	var format;
	var	bNegative = false;
	
	// try special case first
	if (Mask == "") // unformatted data
	{
        var newString = DW_ChangeDecimalCharAndGroupCharToStd2(inString);
		var n = 0;
		if (bFloat)
			n = parseFloat(newString);
		else
			n = parseInt(newString);
		
		if(isNaN(Number(inString)))
			return false;		

		if (isNaN(n))
			return false;

		if(outNumber != null)
			outNumber.number = n;
		
		return true;
	}		
	
	if (Mask.toLowerCase() == "[currency]")
	{
		bFloat = true; 
		if (inString.charAt(0) == DW_negCurrencyFormat.charAt(0))
		{
			format = new DW_NumberEncodingClass(DW_negCurrencyFormat, DWFMT_section_positive);
			bNegative = true;
		}
		else
			format = new DW_NumberEncodingClass(DW_posCurrencyFormat, DWFMT_section_positive);
	}
	else
    {
        var semiOffset;

        semiOffset = Mask.indexOf(";");
        if (semiOffset != -1)
            {
            format = new DW_NumberEncodingClass(Mask.substring(0, semiOffset), DWFMT_section_positive);
            Mask = Mask.substring(0, semiOffset);
            }
        else
           format = new DW_NumberEncodingClass(Mask, DWFMT_section_positive);
    }
    
    if(!format.bValid)
		return false;
	
	
	// Create a new number class
	var nm = new DW_NumberClass();

	var STATESECTION = 0;
	var STATENUMBER = 1;
	var STATEDECIMAL = 2;
	var STATEASIS = 3;

	var currChar;
	var charIndex = 0;
	var state;
	var prvState;
	var isInteger = false;

	var key;			// To hold number and strings
	var nVal;			// To store integer value
	
    var index = 0;
    var encodedFormat = format.encodedFormat;
    var action;
    
	while (charIndex < inString.length && index < encodedFormat.length)
	{

		// Initialize
		state = prvState = STATESECTION;

		key = "";
		nVal = 0;

		// Extract one token from inString
		do{
			currChar = inString.charAt (charIndex);
			if (state == STATESECTION)
			{
				if (currChar == '.')
					state = STATEDECIMAL;
				
				else if (allowInString(currChar, "1234567890+-"))
					state = STATENUMBER;
				
				else
					state = STATEASIS;
			}
			else if (state == STATEDECIMAL)
			{
				if(bFloat != true)
					isInteger = true;

				key += currChar;
				charIndex++;

				state = STATESECTION;       // Change state for next char
				prvState = STATEDECIMAL;
			}
			else if(state == STATENUMBER)
			{
				if((key == "") && ((currChar == '-') || (currChar == '+')))
				{
					if (currChar == '-')
						bNegative = true;

					charIndex++;
				}
				else
				{
					key += currChar;
					charIndex++;
					
					// Skip comma character
					if(inString.charAt (charIndex) == ',')
					{
						charIndex++;
						
						// Next character should be a digit
						if(!DW_parseIsDigit(inString.charAt (charIndex)))
							return false;
					}
					
					if (!DW_parseIsDigit(inString.charAt (charIndex)))
					{
						state = STATESECTION;       // Change state for next char
						prvState = STATENUMBER;
						nVal = key - 0;
					}
				}
			}
			else if (state == STATEASIS)
			{
				key += currChar;
				charIndex++;

				if (allowInString (inString.charAt (charIndex), "1234567890.+-"))
				{
					state = STATESECTION;       // Change state for next char
					prvState = STATEASIS;
				}
			}
			else
			{
				return false;        // Unspecified error
			}

		}while(charIndex < inString.length && state != STATESECTION)


		// Now extract one token from encode string
		
		action = 0;

		if( index < encodedFormat.length)
		{
            action = encodedFormat[index];
			index++;
		}
		
		if ((typeof action == "string") && (prvState != STATEASIS))
		{
			// Skip ASIS Format
            action = encodedFormat[index];
			index++;
		}
		
		if ((action == DWFMT_integer || action == DWFMT_integer_comma) && (prvState == STATEDECIMAL))
		{
			// Skip Integer Action
            action = encodedFormat[index+2];
			index+=3;

			if (action != DWFMT_decimal)
				return false;
		}

        if (typeof action == "string")
		{
			if (key != action)
				return false;
		}
		else if(action == DWFMT_decimal)
		{
            if (key != ".")
				return false;
		}
		else if(action == DWFMT_integer || action == DWFMT_integer_comma)
		{
            numReq = encodedFormat[index];	index ++;
            numOpt = encodedFormat[index];	index ++;
            
			if (!format.bMult100 && (key.length > numReq + numOpt)) 
				return false;

			nm.number = nVal;
		}
        else if (action == DWFMT_fraction)
		{
            numReq = encodedFormat[index];	index ++;
            numOpt = encodedFormat[index];	index ++;

            if (key.length > numReq + numOpt)
				return false;
				
			var tString = "." + key;
			
			nm.number += tString;
		}
	}

	if (bNegative)
		nm.number *= -1;

 	if(nm.number == 0)
		nm.number = "0.0"

	if (format.bMult100) 
		nm.number = nm.number/100.0;

	if (isInteger)
		nm.number = Math.floor(nm.number);

	if(outNumber != null)
		outNumber.number = nm.number;

	return true;
}


//
// Number formatting code
//
var DWFMT_integer_comma = 1;
var DWFMT_integer = 2;
var DWFMT_fraction = 3;
var DWFMT_decimal = 4;
var DWFMT_exp_integer_comma = 5;
var DWFMT_exp_integer = 6;
var DWFMT_exp_fraction = 7;
var DWFMT_exp_exp = 8;
var DWFMT_exp_sign = 9;
var DWFMT_exp_sign_opt = 10;
var DWFMT_general = 11;
var DWFMT_asis_digit = 12;

var DWFMT_type_normal = 1;
var DWFMT_type_exp = 2;
var DWFMT_type_percent = 3;
var DWFMT_type_asis = 4;

var DWFMT_section_positive = 0;
var DWFMT_section_negative = 1;
var DWFMT_section_zero = 2;
var DWFMT_section_null = 3;

function DW_NumberEncodingClass(inString,section)
{
var STATE_START = 1;
var STATE_LEFTDEC = 2;
var STATE_RIGHTDEC = 3;
var STATE_TESTASIS = 4;
var STATE_ASIS = 5;

    var index;
    var currState = STATE_START;
    var currChar;
    var encodedFormat = new Array();
    var accum = "";
    var numInSection;
    var offset = 0;
    var bValid = true;
    // state flags
    var bOnLeft = true;
    var bDidLeft = false;
    var bOnExp = false;
    var bCommas = false;
    var reqDigits = 0;
    var optDigits = 0;
    var totDigits = 0;
    var numDecPlaces = 0;
    
    // misc variable
    var nextChar;
    var nextNextChar;
    
    this.encodedFormat = encodedFormat;
    this.color = "";
    this.bMult100 = false;
    this.maskType = DWFMT_type_normal;
    this.bGeneral = false;
    
    var strLen = inString.length;
    for (index=0; index <= strLen && bValid; )
        {
        if (index < strLen)
            currChar = inString.charAt(index);
        else
            currChar = "";
        if (currState == STATE_START)
            {
            // handle keywords
            if (currChar == "[")
                {
                if (accum != "")
                    encodedFormat[offset++] = accum;
                accum = "";
                index++;
                for (; inString.charAt(index) != "]"; index++)
                    accum += inString.charAt(index);
                index++; // skip ]
                var inlineEncoding = null;
                var keyword = accum.toUpperCase();
                if (keyword == "GENERAL")
                    {
                    encodedFormat[offset++] = DWFMT_general;
                    this.bGeneral = true;
                    }
                else if (keyword == "CURRENCY")
                    {
                    if (section == DWFMT_section_positive || section == DWFMT_section_null)
                        inlineEncoding = new DW_NumberEncodingClass(DW_posCurrencyFormat, section);
                    else if(section == DWFMT_section_negative)
                        inlineEncoding = new DW_NumberEncodingClass(DW_negCurrencyFormat, section);
                    else
                        bValid = false;
                    }
                else
                    {
					if (!parseInt(accum)) 
						this.color = accum.toLowerCase();
					else
						this.color = eval(accum);
                    this.keyword = accum;
                    }
                // if we build another format, inline it into current one
                if (inlineEncoding != null && inlineEncoding.bValid)
                    {
                    var innerFormat = inlineEncoding.encodedFormat;
                    for (var j=0; j<innerFormat.length; j++)
                        encodedFormat[offset++] = innerFormat[j];
                    }
                accum = "";
                }
            else if (currChar == "#")
                {
                if (accum != "")
                    encodedFormat[offset++] = accum;
                accum = "";
                if (bOnLeft && bDidLeft)
                    currState = STATE_TESTASIS;
                else if(bOnLeft)
                    currState = STATE_LEFTDEC;
                else
                    currState = STATE_RIGHTDEC;
                // reset accumulators
                reqDigits = 0;
                totDigits = 0;
                optDigits = 0;
                bCommas = false;
                }
            else if (currChar == "0")
                {
                if (accum != "")
                    encodedFormat[offset++] = accum;
                accum = "";
                if (bOnLeft && bDidLeft)
                    currState = STATE_TESTASIS;
                else if(bOnLeft)
                    currState = STATE_LEFTDEC;
                else
                    currState = STATE_RIGHTDEC;
                // reset accumulators
                reqDigits = 0;
                totDigits = 0;
                optDigits = 0;
                bCommas = false;
                }
            else if (currChar == ".")
                {
                if (accum != "")
                    encodedFormat[offset++] = accum;
                accum = "";
                index++;
                if (bOnLeft)
                    {
                    encodedFormat[offset++] = DWFMT_decimal;
                    bOnLeft = false;
                    }
                // we want only 1 period char!
                else
                    bValid = false;
                }
            else if (currChar == "e" || currChar == "E")
                {
                accum += currChar;
                index++;
                nextChar = inString.charAt(index);
                nextNextChar = inString.charAt(index+1);
                if ((nextChar == "-" || nextChar == "+") &&
                     (nextNextChar == "#" || nextNextChar == "0"))
                    {
                    bOnExp = true;
                    bOnLeft = false;
                    if (nextChar == "+")
                        encodedFormat[offset++] = DWFMT_exp_sign;
                    else
                        encodedFormat[offset++] = DWFMT_exp_sign_opt;
                    // correct prior encodings
                    for (i=0; i < encodedFormat.length;i++)
                        {
                        if (typeof encodedFormat[i] == "number")
                            {
                            var lastFormat = encodedFormat[i];
                            if (lastFormat == DWFMT_integer_comma)
                                {
                                encodedFormat[i] = DWFMT_exp_integer_comma;
                                i += 2;
                                }
                            else if (lastFormat == DWFMT_integer)
                                {
                                encodedFormat[i] = DWFMT_exp_integer;
                                i += 2;
                                }
                            else if (lastFormat == DWFMT_fraction)
                                {
                                encodedFormat[i] = DWFMT_exp_fraction;
                                i += 2;
                                }
                            }
                        }
                    }
                if (this.maskType == DWFMT_type_normal)
                    this.maskType = DWFMT_type_exp;
                else
                    bValid = false;
                }
            else if (currChar == "-")
                {
                // we will allow it to be the first char of the negative section
                if (section == DWFMT_section_negative && 
                        offset == 0 && accum == "")
                    {
                    accum += currChar;
                    index++;
                    }
                else
                    currState = STATE_TESTASIS;
                }
            else if (currChar == "%")
                {
                accum += currChar;
                index++;
                if (this.maskType == DWFMT_type_normal)
                    {
                    this.maskType = DWFMT_type_percent;
                    this.bMult100 = true;
                    }
                else
                    bValid = false;
                }
            else if (currChar == "\\")
                {
                index++;
                accum += inString.charAt(index++);
                }
            else if (currChar == "'")
                {
                index++;
                while (index < strLen)
                    {
                    currChar = inString.charAt(index);
                    if (currChar == "'")
                        break;
                    accum += currChar;
                    index++;
                    }
                // check if we fell off end before finding closing quotes
                if (index == strLen)
                    bValid = false;

                index++; // skip trailing '
                }
            else
                {
                accum += currChar;
                index++;
                }
            }
        else if (currState == STATE_LEFTDEC)
            {
            nextChar = inString.charAt(index+1);
            if (currChar == "#")
                {
                optDigits++;
                if(!bOnExp)
                    totDigits++;
                index++;
                }
            else if(currChar == "0")
                {
                reqDigits++;
                if(!bOnExp)
                    totDigits++;
                index++;
                }
            else if(currChar == "," &&
                    (nextChar == "#" || nextChar == "0"))
                {
                bCommas = true;
                index++;
                }
            else
                {
                if (bCommas)
                    encodedFormat[offset++] = DWFMT_integer_comma;
                else
                    encodedFormat[offset++] = DWFMT_integer;
                encodedFormat[offset++] = reqDigits;
                encodedFormat[offset++] = optDigits;
                currState = STATE_START;
                }
            }
        else if (currState == STATE_RIGHTDEC)
            {
            if (currChar == "#")
                {
                numDecPlaces++;
                optDigits++;
                if(!bOnExp)
                    totDigits++;
                index++;
                }
            else if(currChar == "0")
                {
                numDecPlaces++;
                reqDigits++;
                if(!bOnExp)
                    totDigits++;
                index++;
                }
            else
                {
                if (bOnExp)
                    encodedFormat[offset++] = DWFMT_exp_exp;
                else
                    encodedFormat[offset++] = DWFMT_fraction;
                encodedFormat[offset++] = reqDigits;
                encodedFormat[offset++] = optDigits;
                currState = STATE_START;
                }
            }
        else if (currState == STATE_TESTASIS)
            {
            // convert to asis form
            for (i=0; i < encodedFormat.length && bValid;i++)
                {
                if (typeof encodedFormat[i] == "number")
                    {
                    if (encodedFormat[i] == DWFMT_integer)
                        {
                        encodedFormat[i] = DWFMT_asis_digit;
                        i += 2;
                        }
                    else
                        bValid = false;
                    }
                }
            this.maskType = DWFMT_type_asis;
            currState = STATE_ASIS;
            }
        else if (currState == STATE_ASIS)
            {
            if (currChar == "#" || currChar == "0")
                {
                totDigits++;
                if (accum != "")
                    encodedFormat[offset++] = accum;
                accum = "";
                encodedFormat[offset++] = DWFMT_asis_digit;
                encodedFormat[offset++] = 1; // 1 required char
                encodedFormat[offset++] = 0; // no optional
                }
            else
                accum += currChar;
            index++;
            }
        }

    if (accum != "")
        encodedFormat[offset++] = accum;

    this.totalDigits = totDigits;

	if (encodedFormat.length == 0 ) 
		bValid = false;

    this.bValid = bValid;
    this.numDecPlaces = numDecPlaces;
}

function DW_NumberClass(number)
{
    if (arguments.length == 0)
        number = 0.0;
        
    this.number = number;
}

function DW_NumberFormatClass(formatString)
{
    this.negativeFormat = null;
    this.zeroFormat = null;
    this.nullFormat = null;

    // try special case first
    if (formatString.toLowerCase() == "[currency]")
        {
        this.positiveFormat = new DW_NumberEncodingClass(DW_posCurrencyFormat, DWFMT_section_positive);
        this.negativeFormat = new DW_NumberEncodingClass(DW_negCurrencyFormat, DWFMT_section_negative);
        this.bValid = this.positiveFormat.bValid && this.negativeFormat.bValid;
        }
    else
        {
        var bValid = true;
        var semiOffset;

        semiOffset = formatString.indexOf(";");
        if (semiOffset != -1)
            {
            this.positiveFormat = new DW_NumberEncodingClass(formatString.substring(0, semiOffset), DWFMT_section_positive);
            formatString = formatString.substring(semiOffset+1, formatString.length);
            semiOffset = formatString.indexOf(";");
            if (semiOffset != -1)
                {
                this.negativeFormat = new DW_NumberEncodingClass(formatString.substring(0, semiOffset), DWFMT_section_negative);
                formatString = formatString.substring(semiOffset+1, formatString.length);
                semiOffset = formatString.indexOf(";");
                if (semiOffset != -1)
                    {
                    this.zeroFormat = new DW_NumberEncodingClass(formatString.substring(0, semiOffset), DWFMT_section_zero);
                    this.nullFormat = new DW_NumberEncodingClass(formatString.substring(semiOffset+1, formatString.length), DWFMT_section_null);
                    bValid = bValid && this.nullFormat.bValid;
                    }
                else
                    this.zeroFormat = new DW_NumberEncodingClass(formatString, DWFMT_section_zero);
                bValid = bValid && this.zeroFormat.bValid;
                }
            else
                this.negativeFormat = new DW_NumberEncodingClass(formatString, DWFMT_section_negative);

            bValid = bValid && this.negativeFormat.bValid
            }
        else
            this.positiveFormat = new DW_NumberEncodingClass(formatString, DWFMT_section_positive);
        this.bValid = bValid && this.positiveFormat.bValid;
        }
}

function DW_FormatNumber(formatString, value, control)
{
    var numberFormat = new DW_NumberFormatClass(formatString);
    var result = "";
    var strValue, dotOffset, exponentOffset;
    var bRequireNegative = false;
    var bNegative = false;
    var format;
    

    if (numberFormat.bValid)
        {

        if (value == null)
            {
            if (numberFormat.nullFormat != null)
                format = numberFormat.nullFormat;
            else
                format = numberFormat.positiveFormat;
            }
        else if (value < 0)
            {
            bNegative = true;
            if (numberFormat.negativeFormat != null)
                format = numberFormat.negativeFormat;
            else
                {
                format = numberFormat.positiveFormat;
                bRequireNegative = true; // mask will not contain negative
                }
            }
        else if (value > 0)
            format = numberFormat.positiveFormat;
        else // == 0
            {
            if (numberFormat.zeroFormat != null)
                format = numberFormat.zeroFormat;
            else
                format = numberFormat.positiveFormat;
            }

        // if not format, we just return ""
        if (format != null)
            {
            var index;
            var encodedFormat = format.encodedFormat;
            var action;
            var numReq, numOpt, numDigitsLeft, numDigitsRight, i, commaDigit;
            var accum;
            var asIsOffset = 0;
            
            if (format.bMult100 && value != null)
                value *= 100;

            // round to proper number of decimal places
            if (value != null && ! format.bGeneral)
                value = DW_Round (value, format.numDecPlaces);

            // convert the value to a string
            if (value != null && value !=0)
                strValue = value.toString();
            else
                strValue = "";

            // strip off leading minus
            if (strValue.charAt(0) == "-")
                strValue = strValue.substring(1,strValue.length);
                
            dotOffset = strValue.indexOf(".");
            if (dotOffset == -1)
                {
                numDigitsLeft = strValue.length;
                numDigitsRight = 0;
                }
            else
                {
                numDigitsLeft = dotOffset;
                numDigitsRight = strValue.length - dotOffset - 1;
                }
            
            for (index=0; index < encodedFormat.length ; index++)
                {
                action = encodedFormat[index];
                if (typeof action == "string")
                    result += action;
                else if (action == DWFMT_integer_comma)
                    {
                    numReq = encodedFormat[index + 1];
                    numOpt = encodedFormat[index + 2];
                    index += 2;
                    if (bRequireNegative)
                        result += "-";

                    if (numDigitsLeft < numReq)
                        {
                        commaDigit = numReq - 1;
                        for (i=0;i< numReq-numDigitsLeft;i++,commaDigit--)
                            {
                            result += "0";
                            if (commaDigit % 3 == 0 && commaDigit != 0)
                                result += DW_thousandsChar;
                            }
                        }
                    else
                        commaDigit = numDigitsLeft - 1;
                                                    
                    for (i=0; i < numDigitsLeft; i++,commaDigit--)
                        {
                        result += strValue.charAt(i);
                        if (commaDigit % 3 == 0 && commaDigit != 0)
                            result += DW_thousandsChar;
                        }
                        
                    }
                else if (action == DWFMT_integer)
                    {
                    numReq = encodedFormat[index + 1];
                    numOpt = encodedFormat[index + 2];
                    index += 2;
                    if (numDigitsLeft < numReq)
                        {
                        for (i=0;i< numReq-numDigitsLeft;i++)
                            result += "0";
                        }
                    for (i=0; i < numDigitsLeft; i++)
                        result += strValue.charAt(i);
                    }
                else if (action == DWFMT_fraction)
                    {
                    numReq = encodedFormat[index + 1];
                    numOpt = encodedFormat[index + 2];
                    index += 2;
                    for (i=0; i < numDigitsRight; i++)
                        result += strValue.charAt(dotOffset + 1 + i);
                    if (numDigitsRight < numReq)
                        {
                        for (i=0; i < numReq - numDigitsRight; i++)
                            result += "0";
                        }
                    }
                else if (action == DWFMT_asis_digit)
                    {
                    numReq = encodedFormat[index + 1];
                    numReq += encodedFormat[index + 2];
                    index += 2;
                    for (i=0; i< numReq; i++)
                        {
                        result += strValue.charAt (asIsOffset);
                        asIsOffset++;
                        }
                    }
                else if (action == DWFMT_decimal)
                    {
                    result += DW_decimalChar;
                    }
                else if (action == DWFMT_general)
                    {
                    if (value != null)
                        result += DW_ChangeDecimalCharAndGroupCharToCurrent(value.toString());
                    }

                // won't deal with exponents for now
                else if (action == DWFMT_exp_integer_comma)
                    {
                    }
                else if (action == DWFMT_exp_integer)
                    {
                    }
                else if (action == DWFMT_exp_fraction)
                    {
                    }
                else if (action == DWFMT_exp_exp)
                    {
                    }
                else if (action == DWFMT_exp_sign)
                    {
                    }
                else if (action == DWFMT_exp_sign_opt)
                    {
                    }
                }
            }
        }
    else if ( value != null )
	{
		// Simulating a [general] format.
        //result = value.toString();
        
        result += DW_ChangeDecimalCharAndGroupCharToCurrent(value.toString());
    }
	else
		result = "";

	if (this.bStylePositioning && format && format.bValid)  
        if ( format.color == "" || typeof format.color == "string" )
            control.style.color = format.color;
        else
            control.style.color = convertToRGB( format.color );

    return result;
}

function DW_Fact(n)
{
    var result = 1;

    if (n == null)
        result = null;
    else
        {
        var intN = Math.floor(n);
        // must be an integer and in range
        if (intN != n || intN <= 0 || intN >= 171)
            result = null;
        else
            {
            for (var i = 1; i <= n ; i++)
                result *= i;
            }
        }

    return result;
}

function DW_Sign(n)
{
    var result;
    if (n < 0) 
        result = -1;
    else if (n == 0)
        result = 0;
    else
        result = 1;

    return result;
}

function DW_Truncate(num, decPlaces)
{
	var powTen = Math.pow(10.0,decPlaces);
	num *= powTen;
	if (num >= 0)
	    num = Math.floor(num + 0.000000000000005);
	else
	    num = Math.ceil(num - 0.000000000000005);

    return num / powTen;
}