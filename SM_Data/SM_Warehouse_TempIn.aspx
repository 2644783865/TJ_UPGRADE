<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_Warehouse_TempIn.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_TempIn"
    Title="结转备库" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <JSR:JSRegister ID="JSRegister1" runat="server" />

     <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    
    <script src="SM_JS/TEMPSQL.js" type="text/javascript"></script>

    <script src="SM_JS/KeyControl.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
  /*检验输入数量并统计*/
  
        function checkRN(tb)
        {           
            var realnum = parseFloat(tb.value);
            
            if (isNaN(realnum))
            {
                alert("请输入正确的数量！");
                
                tb.value = 0;
                
                realnum = parseFloat(tb.value);
                
                var par = tb.parentNode.parentNode;
                
                var unitprice = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value);//单价
                   
                par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= (unitprice*realnum).toFixed(2);//金额
            }
            else
            {
            
                  if(realnum<0)
                  
                  tb.value = -realnum;
                  
                  else
                  
                  tb.value = realnum;
            
                   var par = tb.parentNode.parentNode;
                   
                   var unitprice = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value);//单价
                   
                   if(isNaN(parseFloat(unitprice)))
                    
                    unitprice=0;
                   
                   par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= (unitprice*parseFloat(tb.value)).toFixed(2);//金额
                   
                   if(unitprice==0)
                   
                    par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value= 0;//单价
                    
                    //税率
                    
                    var taxrate=parseFloat(par.getElementsByTagName("td")[19].getElementsByTagName("input")[0].value);//税率
                    
                    if(isNaN(parseFloat(taxrate)))
                    
                    taxrate=17;
                    
                     //含税单价
                     
                    par.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=(unitprice*(1+taxrate/100)).toFixed(4);
                    
                    //含税金额
                    
                    par.getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=(unitprice*(1+taxrate/100)*parseFloat(tb.value)).toFixed(2);
                   
            }
        }
        
        function checkDJ(tb)
        {           
            var unitprice = parseFloat(tb.value);
            
            if (isNaN(unitprice))
            {
                alert("请输入正确的单价！");
                
                tb.value = 0;
                
                unitprice = parseFloat(tb.value);
                
                var par = tb.parentNode.parentNode;
                
                var realnum = parseFloat(par.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value);//数量
                   
                par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= (unitprice*realnum).toFixed(2);//金额
            }
            else 
            {
            
                  if(unitprice>0)
                  
                  tb.value = unitprice;
                  
                  else
                  
                  tb.value = -unitprice;
                
                  unitprice = parseFloat(tb.value);
                  
                  var par = tb.parentNode.parentNode;
                   
                  var realnum = parseFloat(par.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value);//数量
                   
                  par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= (unitprice*realnum).toFixed(2);//金额
                  
                   //税率
                    
                  var taxrate=parseFloat(par.getElementsByTagName("td")[19].getElementsByTagName("input")[0].value);//税率
                    
                  if(isNaN(parseFloat(taxrate)))
                    
                    taxrate=17;
                    
                     //含税单价
                     
                    par.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=(unitprice*(1+taxrate/100)).toFixed(4);
                    
                    //含税金额
                    
                    par.getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=(unitprice*(1+taxrate/100)*realnum).toFixed(2);
                  
            }
           
        }
        function checkJE(tb)
        {
           var cost = parseFloat(tb.value);
          if (isNaN(cost))
            {
                alert("请输入正确的金额！");
                
                tb.value = 0;
                
                cost = parseFloat(tb.value);
                
                var par = tb.parentNode.parentNode;
                   
                par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value= 0;//单价
            }
              else 
            {
            
                  if(cost>0)
                  
                  tb.value = cost;
                  
                  else
                  
                  tb.value = -cost;
                
                  cost = parseFloat(tb.value);
                  
                  var par = tb.parentNode.parentNode;
                   
                  var realnum = parseFloat(par.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value);//数量
                   
                  par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value= (cost/realnum).toFixed(4);//单价
                  
                   //税率
                    
                  var taxrate=parseFloat(par.getElementsByTagName("td")[19].getElementsByTagName("input")[0].value);//税率
                    
                  if(isNaN(parseFloat(taxrate)))
                    
                     taxrate=17;
                    
                     //含税单价
                     
                    par.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=((cost/realnum)*(1+taxrate/100)).toFixed(4);
                    
                    //含税金额
                    
                    par.getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=(cost*(1+taxrate/100)).toFixed(2);
                  
            }
        }
        function checkTaxRate(tb)
        { 
             var taxrate = parseFloat(tb.value); 
               
             if (isNaN(taxrate))
            {
                alert("请输入正确的税率！");
            }
           else
           {
                 if(taxrate>0)
                  
                  tb.value = taxrate;
                  
                  else
                  
                  tb.value = -taxrate;
                  
                  taxrate = parseFloat(tb.value); 
                  
                  var par = tb.parentNode.parentNode;
                  
                  var realnum = parseFloat(par.getElementsByTagName("td")[9].getElementsByTagName("input")[0].value);//数量
                  
                  var unitprice = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value);//单价
                  
                    //含税单价
                     
                  par.getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=(unitprice*(1+taxrate/100)).toFixed(4);
                    
                    //含税金额
                    
                  par.getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=(unitprice*(1+taxrate/100)*realnum).toFixed(2);
   
           }  
        }
        
           /*填充仓位*/
        function ChoosePosition(tb)
         {
            var ws = document.getElementById('<%= DropDownListWarehouse.ClientID %>');
            var wscode = "";
            for (var i = 0; i < ws.options.length; i++)
            {
                    if (ws.options[i].selected == true)
                    {
                        wscode = ws.options[i].value;
                        break;
                    }
            }
           var date=new Date();
           var time=date.getTime();
            var val = new Array(2);
             val = window.showModalDialog("SM_WarehouseIN_PositionChoice.aspx?WSCODE=" + wscode+"&&id="+time, '', "dialogHeight: 550px; dialogWidth: 800px; center: Yes; help: No; resizable: No; status: No;");
            if (val != null) 
            {
                tb.value = val[1];
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[16].getElementsByTagName("input")[1];
                positioncode.value = val[0];
            }
        }
        
        
        function checkPage()
        {
            var count=0;
            
            var flag = true; 
            
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            
            for (i =1; i < gv1.rows.length; i++)
             {
                var marid=gv1.rows[i].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value;
                
                if(marid!="")
                {
                
                     var num=gv1.rows[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value;
                     if(num==""||num==0)
                    {
                    
                      alert("数量不能为空或者为0!");
                      
                      return false;
                      
                    }
                
                    var dj = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;
                    
                    if(dj==""||dj==0)
                    {
                    
                      alert("单价不能为空或者为0!");
                      
                      return false;
                      
                    }
                    
                    var warehouse = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                        
                    if (warehouse == "-请选择-"||warehouse =="") 
                    {
                        alert("请选择仓库！");
                        return false;
                    }
                     var position = gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;
                        
                    if (position == "待查"||position == "") 
                    {
                        alert("请选择仓位！");
                        return false;
                    }

                    count++;
                }
                else
                {
                   continue;
                }
            }
            

       //仓库
            var ws = document.getElementById('<%= DropDownListWarehouse.ClientID %>');
            var wscode = "";
            for (var i = 0; i < ws.options.length; i++)
            {
                    if (ws.options[i].selected == true)
                    {
                        wscode = ws.options[i].value;
                        break;
                    }
            }
            if(wscode=="0"||wscode=="")
            {
              alert("请选择仓库！");
              
              return false;
            }
            
            if(count==0)
            {
              alert("没有数据，不能保存或审核！");
              
              return false;
            }
            return flag;
        
        }
  
function allsel()
  {
   var  table=document.getElementById("ctl00_PrimaryContent_GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
     if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
       }
    }
  }

function cancelsel()
{
   var  table=document.getElementById("ctl00_PrimaryContent_GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
       if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//        tr[i].style.backgroundColor='#EFF3FB'; 
        }
    }
}

function consel()
{
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");//这里的tr是一个数组
    for(var i=1;i<(tr.length-1);i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
        {
            if(obj.checked)
            {
                obj.checked=true;
//                obj.parentNode.parentNode.style.backgroundColor ='#55DF55'; 
                for(var j=i+1;j<tr.length;j++)
                {
                    var nextobj=tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if(nextobj!=null)
                    {
                    
                        if(nextobj.type.toLowerCase()=="checkbox" && nextobj.value!="")
                        {
                            if(nextobj.checked)
                            {
                                for(var k=i+1;k<j+1;k++)
                                {
                                    tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
//                                    tr[k].style.backgroundColor ='#55DF55'; 
                                }
                            }
                        }  
                    }
                }
            }
        }
    }
}
  
        function clickalert(tb){
           var ischeck=confirm("单据将会结转到下期，这个功能只有当系统处于结账状态时，才能使用！请慎用！");
           if(ischeck)
           {
           tb.checked=true;
           }
           else
           {
           tb.checked=false;
           }
}     
        
        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
        }
      function tostorage() {
 
        window.open("SM_Warehouse_TempIn.aspx?FLAG=PUSHIN");
       }    
        
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div style="margin: 0px 0px 0px 10px">
                        <table width="99%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                            <tr>
                                <td style="width: 50%" align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btninsert" runat="server" Text=" 插 入" OnClick="btninsert_Click" CausesValidation="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnappend" runat="server" Text="追 加" OnClick="btnadd_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btndelete" runat="server" Text="删 除" OnClick="btndelete_Click" OnClientClick="return alert('确定删除此条目吗？')" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" OnClientClick="return checkPage();" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnverity" runat="server" Text="审 核" OnClick="Verify_Click" OnClientClick="return checkPage();" />
                                    <input id="Close" type="button" value="关闭" onclick="closewin()" runat="server" />&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                        <table width="100%">
                            <tr>
                                <td>
                                    系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="font-size: x-large; text-align: center;" colspan="2">
                                    备库入库单
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxDate" runat="server" Text="结转到下期" CssClass="checkBoxCss"
                                        onclick="clickalert(this);" />
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode"
                                        runat="server"></asp:Label>
                                    <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                                    <input type="text" id="InputColour" style="display: none" runat="server" />
                                </td>
                                <td width="25%">
                                    &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                        TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd" FirstDayOfWeek="Monday">
                                    </asp:CalendarExtender>
                                </td>
                                <td width="25%">
                                    &nbsp;&nbsp;&nbsp;仓库：
                                    <asp:DropDownList ID="DropDownListWarehouse" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="DropDownListWarehouse_SelecedIndexChanged">
                                        <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="25%">
                                    &nbsp;&nbsp;&nbsp;仓位：
                                    <asp:DropDownList ID="DropDownListPosition" runat="server" AutoPostBack="True" AppendDataBoundItems="true"
                                        OnSelectedIndexChanged="DropDownListPosition_SelecedIndexChanged">
                                        <asp:ListItem Value="0">待查</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextBoxAbstract" runat="server" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div>
                        &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                            id="continue" type="button" value="连选" onclick="consel()" />
                        &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
                    </div>
                    <asp:Panel ID="PanelBody" runat="server" Style="height: 350px;">
                        <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                            CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="15px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Width="15px" CssClass="checkBoxCss" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物料编码" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <asp:TextBox ID="marid" runat="server" BorderStyle="None"  AutoPostBack="true" 
                                            Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>'  OnTextChanged="marid_TextChanged"  Width="120px"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode"
                                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="marid" UseContextKey="True"
                                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物料名称">
                                    <ItemTemplate>
                                        <input id="marnm" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("MaterialName") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="规格型号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="guige" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("MaterialStandard") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="材质">
                                    <ItemTemplate>
                                        <input id="caizhi" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Attribute") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="国标" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="gb" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("GB") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="批号">
                                    <ItemTemplate>
                                        <input id="ph" runat="server" style="border-style: none; width: 100px" type="text"
                                            value='<%#Eval("LotNumber") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="unit" runat="server" style="border-style: none; width: 40px" type="text"
                                            value='<%#Eval("Unit") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="数量" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="num" runat="server" style="border-style: none; width: 40px" type="text"
                                            value='<%#Eval("RN") %>' onkeyup="grControlFocus(this)" onblur="checkRN(this)" />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#FFFFCC" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="长(mm)" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="length" runat="server" style="border-style: none; width: 50px" type="text"
                                            value='<%#Eval("Length") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#FFFFCC" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="宽(mm)" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="width" runat="server" style="border-style: none; width: 50px" type="text"
                                            value='<%#Eval("Width") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#FFFFCC" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="张(支)" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="zhang" runat="server" style="border-style: none; width: 50px" type="text"
                                            value='<%#Eval("RQN") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle BackColor="#FFFFCC" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="计划跟踪号" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="pt" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("PTC") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <input id="beizhu" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Comment") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="仓库">
                                    <ItemTemplate>
                                        <input id="ck" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Warehouse") %>' onkeyup="grControlFocus(this)" />
                                        <input type="text" id="InputWarehouseCode" value='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                            style="display: none" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="仓位">
                                    <ItemTemplate>
                                        <input id="cw" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Position") %>' onkeyup="grControlFocus(this)" onclick="ChoosePosition(this)" />
                                        <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                            style="display: none" runat="server" />
                                        <%--<asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>' Visible="false"></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单价">
                                    <ItemTemplate>
                                        <input id="dj" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("UnitPrice") %>' onkeyup="grControlFocus(this)" onblur="checkDJ(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额">
                                    <ItemTemplate>
                                        <input id="je" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Amount") %>' onkeyup="grControlFocus(this)" onblur="checkJE(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="税率(%)">
                                    <ItemTemplate>
                                        <input id="tr" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("TAXRATE") %>' onkeyup="grControlFocus(this)" onblur="checkTaxRate(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="含税单价">
                                    <ItemTemplate>
                                        <input id="tdj" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("CTAXUPRICE") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="含税金额">
                                    <ItemTemplate>
                                        <input id="tje" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("CTAMTMNY") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1" />
                        </yyc:SmartGridView>
                    </asp:Panel>
                    <asp:Panel ID="FooterPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                        <table width="100%">
                            <tr>
                                <td align="center" style="width: 25%;">
                                    &nbsp;&nbsp;&nbsp;收&nbsp;&nbsp;&nbsp;料：
                                    <asp:DropDownList ID="DropDownListAccepter" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" style="width: 25%;">
                                    &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                                    <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 25%;">
                                    &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server" Width="100px"></asp:Label>
                                    <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="center" style="width: 25%;">
                                    审核日期：<asp:Label ID="LabelApproveDate" runat="server" Width="100px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
