//页面CB_ProNum_Cost.aspx

//单击某一行
function change(obj)  
 {   
  if(obj.tagName.toLowerCase()=="tr")   
  {  
    for(var   i=1;   i<=document.all.table1.rows.length-4;i++)  
    {   
      var row="";
      if(i<10)
      {
         row="0"+i;
      }
      else
      {
        row=i;
      }
      //其他行
      document.all.table1.rows[i+1].style.backgroundColor="";
      document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_cbxSelect").checked=false; 
      //其他控件
      CancelStyle(row);
    }//end for
    
    //行
    obj.style.backgroundColor="#CCCCFF";
    //该行控件
    var row="";
    if(obj.rowIndex<=10)
    {

       row="0"+(obj.rowIndex-2);
    }
    else
    {
  
       row=(obj.rowIndex-2);
    }
    document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_cbxSelect").checked=true;
    if(document.getElementById("ctl00_PrimaryContent_txtSCZH_Summary"))
    {
        document.getElementById("ctl00_PrimaryContent_txtSCZH_Summary").value=document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_lblTaskID").innerHTML;
    }
      
    //设置当前行控件的样式
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_BZJ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_BZJ").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_BZJ").title="点击进行修改";
	
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CNPJ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CNPJ").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DL").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_GJFM").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_GJFM").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HCL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HCL").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HGXJ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HGXJ").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJS").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJS").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJSSL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJSSL").style.borderStyle="Solid";
	
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_JGJ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_JGJ").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_MCKY").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_MCKY").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_RYL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_RYL").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WGJ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WGJ").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WJCL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WJCL").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_XFQC").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_XFQC").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YSJS").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YSJS").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YQTL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YQTL").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZP").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZP").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZZCL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZZCL").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DQDL").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DQDL").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CLGJ").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CLGJ").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QTGJ").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QTGJ").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DDGJ").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DDGJ").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HJGJ").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HJGJ").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_LBYP").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_LBYP").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QDGJ").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QDGJ").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QXGJ").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QXGJ").style.borderStyle="Solid";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QZYY").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QZYY").style.borderStyle="Solid";
//	
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_SDGJ").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_SDGJ").style.borderStyle="Solid";
//    document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_SDGJ").title="点击进行修改";
    
    
    document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_WWJGF").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_WWJGF").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_GZ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_GZ").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZGZ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZGZ").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_FLJBX").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_FLJBX").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZZFLJBX").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZZFLJBX").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_FTZZFY").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_FTZZFY").style.borderStyle="Solid";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_YF").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_YF").style.borderStyle="Solid";  
  } 
}


//双击取消
function changeback(obj)
{
  if(obj.tagName.toLowerCase()=="tr") 
  {
    obj.style.backgroundColor   =  "";
    var row="";
    if(obj.rowIndex<=10)
    {
       row="0"+(obj.rowIndex-2);
    }
    else
    {
       row=obj.rowIndex-2;
    }
    document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_cbxSelect").checked=false;  
    CancelStyle(row);
  }
} 

//取消选择
function CancelSelect(obj)
{
  if(obj.checked==true)
  {
    for(var   i=1;   i<=document.all.table1.rows.length-4;i++)
    {
      var row="";
      if(i<10)
      {
         row="0"+i;
      }
      else
      {
        row=i;
      }
      document.all.table1.rows[i+1].style.backgroundColor="";
      //CheckBox
      document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_cbxSelect").checked=true;
      CancelStyle(row);
    }
  }
  else
  {
    for(var   i=1;   i<=document.all.table1.rows.length-4;i++)
    {
      var row="";
      if(i<10)
      {
         row="0"+i;
      }
      else
      {
        row=i;
      }
      document.all.table1.rows[i+1].style.backgroundColor="";
      //CheckBox
      document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_cbxSelect").checked=false;
      CancelStyle(row);
    }
  }
}

//取消样式
function CancelStyle(row)
{
    //其他控件      
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_BZJ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_BZJ").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CNPJ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CNPJ").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DL").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_GJFM").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_GJFM").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HCL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HCL").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HGXJ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HGXJ").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJS").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJS").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJSSL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HSJSSL").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_JGJ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_JGJ").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_MCKY").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_MCKY").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_RYL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_RYL").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WGJ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WGJ").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WJCL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_WJCL").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_XFQC").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_XFQC").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YSJS").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YSJS").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YQTL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_YQTL").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZP").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZP").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZZCL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_ZZCL").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DQDL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DQDL").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CLGJ").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_CLGJ").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QTGJ").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QTGJ").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DDGJ").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_DDGJ").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HJGJ").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_HJGJ").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_LBYP").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_LBYP").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QDGJ").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QDGJ").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QXGJ").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QXGJ").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QZYY").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_QZYY").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_SDGJ").style.backgroundColor="Transparent";
//	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMS_SDGJ").style.borderStyle="None";
	
    document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_WWJGF").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_WWJGF").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_GZ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_GZ").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZGZ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZGZ").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_FLJBX").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_FLJBX").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZZFLJBX").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPPC_BZZFLJBX").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_FTZZFY").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_FTZZFY").style.borderStyle="None";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_YF").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumCost_ctl"+row+"_txtPMC_YF").style.borderStyle="None";
}

//页面CM_ProNum_ProfitLoss.aspx
//单击某一行
function change_prf(obj)  
 {   
  if(obj.tagName.toLowerCase()=="tr")   
  {  
    for(var i=0;i<document.all.table1.rows.length-2;i++)  
    {   
      var row="";
      var j=i+1;
      if(j<10)
      {
         row="0"+j;
      }
      else
      {
        row=j;
      }
      //其他行
      document.all.table1.rows[i+1].style.backgroundColor="";
      //其他控件
      CancelStyle_prf(row);
    }//end for
    
    //行
    obj.style.backgroundColor="#CCCCFF";
    //该行控件
    var row="";
    if(obj.rowIndex<10)
    {
       row="0"+obj.rowIndex;
    }
    else
    {
       row=obj.rowIndex;
    }
    document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_cbxSelect").checked=true;
    //设置当前行控件的样式
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTJE").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTJE").style.borderStyle="Solid";
//	
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_BHSHTJE").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_BHSHTJE").style.borderStyle="Solid";

//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTZL").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTZL").style.borderStyle="Solid";

//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_ZLGCZL").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_ZLGCZL").style.borderStyle="Solid";

//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_CPZCB").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_CPZCB").style.borderStyle="Solid";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTGLFY").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTGLFY").style.borderStyle="Solid";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTCWFY").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTCWFY").style.borderStyle="Solid";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTZYYWSJJFJ").style.backgroundColor="White";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTZYYWSJJFJ").style.borderStyle="Solid";

//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_JLR").style.backgroundColor="White";
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_JLR").style.borderStyle="Solid";
  } 
}


//双击取消
function changeback_prf(obj)
{
  if(obj.tagName.toLowerCase()=="tr") 
  {
    obj.style.backgroundColor   =  "";
    var row="";
    if(obj.rowIndex<10)
    {
       row="0"+obj.rowIndex;
    }
    else
    {
       row=obj.rowIndex;
    }
    document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_cbxSelect").checked=false;  
    CancelStyle_prf(row);
  }
} 

//取消选择
function CancelSelect_prf(obj)
{
  if(obj.checked==true)
  {
    for(var i=0;i<document.all.table1.rows.length-2;i++)
    {
      var row="";
      var j=i+1;
      if(j<10)
      {
         row="0"+j;
      }
      else
      {
        row=j;
      }
      document.all.table1.rows[i+1].style.backgroundColor="";
      //CheckBox
      document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_cbxSelect").checked=true;
    }
  }
  else
  {
    for(var i=0;i<document.all.table1.rows.length-2;i++)
    {
      var row="";
      var j=i+1;
      if(j<10)
      {
         row="0"+j;
      }
      else
      {
        row=j;
      }
      document.all.table1.rows[i+1].style.backgroundColor="";
      //CheckBox
      document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_cbxSelect").checked=false;
      CancelStyle_prf(row);
    }
  }
}

//取消样式
function CancelStyle_prf(row)
{
    //其他控件   
    document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTJE").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTJE").style.borderStyle="None";
//	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTJE").title="点击进行修改";
	
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_BHSHTJE").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_BHSHTJE").style.borderStyle="None";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTZL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_HTZL").style.borderStyle="None";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_ZLGCZL").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_ZLGCZL").style.borderStyle="None";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_CPZCB").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_CPZCB").style.borderStyle="None";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTGLFY").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTGLFY").style.borderStyle="None";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTCWFY").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTCWFY").style.borderStyle="None";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTZYYWSJJFJ").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_FTZYYWSJJFJ").style.borderStyle="None";

	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_JLR").style.backgroundColor="Transparent";
	document.getElementById("ctl00_PrimaryContent_rptProNumPL_ctl"+row+"_txtPPL_JLR").style.borderStyle="None";
   
}

//公共

//文本 获得焦点
function setToolTipGet(obj)
{
  obj.title="按Enter键保存";
  var t=obj.style.width.substring(0,obj.style.width.length-2);
  if(t<100)
  {
    obj.style.width="100px";
  }
  obj.style.borderWidth="medium";
  obj.style.borderColor="Red";
}
//文本 失去焦点
function setToolTipLost(obj)
{
  obj.title="点击进行修改";
  var t=obj.value.length*8;
  obj.style.width=t+"px";
  obj.style.backgroundColor="Transparent";
  obj.style.borderWidth="1px";
  obj.style.borderColor="#B3CDE8";
}