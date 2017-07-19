<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseIn_Other.aspx.cs" MasterPageFile="~/Masters/SMBaseMaster.Master" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIn_Other" Title="其他入库" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script src="SM_JS/KeyControl.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    
     function checknum(tb)
     {
        var objnum = parseFloat(tb.value);
        if (isNaN(objnum) || objnum < 0)
        {
            tb.value = 0;
            alert("请输入正确的数字！");
        }
        Statistic(tb);
      }
       /*数据统计函数*/
        function Statistic(tb) {
        
                   var par = tb.parentNode.parentNode;
                   
                   var num = parseFloat(par.getElementsByTagName("td")[12].getElementsByTagName("input")[0].value);//数量
                   var unitprice = parseFloat(par.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value);//单价
                   var je = parseFloat(par.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value);//金额
                   var sl = parseFloat(par.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value);//税率
                   var tdj = parseFloat(par.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value);//含税单价
                   var tje = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value);//含税金额
                   if(isNaN(num) || num < 0)
                   {
                     num=0;
                   }
                   if(isNaN(unitprice) || unitprice < 0)
                   {
                     unitprice=0;
                     par.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=0;
                   }
                   if(isNaN(je) || je < 0)
                   {
                     je=0;
                     par.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=0;
                   }
                   if(isNaN(sl) || sl < 0)
                   {
                     sl=17;
                     par.getElementsByTagName("td")[15].getElementsByTagName("input")[0].value=17;
                   }
                   if(isNaN(tdj) ||tdj < 0)
                   {
                     tdj=0;
                     par.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=0;
                   }
                   if(isNaN(tje) || tje < 0)
                   {
                     sl=0;
                     par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=0;
                   }
                   //数量
                   if(tb==par.getElementsByTagName("td")[12].getElementsByTagName("input")[0])
                   {
                      par.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=(num*unitprice).toFixed(2); //金额
                      
//                    par.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=(unitprice*(1+sl*0.01)).toFixed(4);//含税单价
                      
                      par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=(num*unitprice*(1+sl*0.01)).toFixed(2);//含税金额
                   }
                   //单价
                   if(tb==par.getElementsByTagName("td")[13].getElementsByTagName("input")[0])
                   {
                      par.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=(unitprice*num).toFixed(2); //金额
                      
                      par.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=(unitprice*(1+sl*0.01)).toFixed(4);//含税单价
                      
                      var obj3=par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=(unitprice*num*(1+sl*0.01)).toFixed(2);//含税金额
                   }
                   //金额
                   if(tb==par.getElementsByTagName("td")[14].getElementsByTagName("input")[0])
                   {
                      if(num!=0)
                      {
                          par.getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=(je/num).toFixed(4); //单价
                          par.getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=(je/num*(1+sl*0.01)).toFixed(4);//含税单价
                          par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=(je*(1+sl*0.01)).toFixed(2);//含税金额
                      }
                   }
        }
      
      
      
      
      function allsel()
      {
       var  table=document.getElementById('<%= GridView1.ClientID %>');
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
       var  table=document.getElementById('<%= GridView1.ClientID %>');
       var tr=table.getElementsByTagName("tr");
        for(var i=1;i<tr.length;i++)
        {
           if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
           {
            tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
            }
        }
    }

    function consel()
    {
        table=document.getElementById('<%= GridView1.ClientID %>');
        tr=table.getElementsByTagName("tr");
        for(var i=1;i<(tr.length-1);i++)
        {
            obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
            if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
            {
                if(obj.checked)
                {
                    obj.checked=true;
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
                                    }
                                }
                            }  
                        }
                    }
                }
            }
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
                var positioncode = par.getElementsByTagName("td")[19].getElementsByTagName("input")[1];
                positioncode.value = val[0];
            }
        }    
        
    function closewin() {
        window.opener.location = window.opener.location.href;
        window.close();
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
         function ClientSideClick(myButton) {
     
           // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            
            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.value = "加载中...";              
            }
            
           return true;
        }   
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                    <tr>
                        <td style="width: 50%" align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btninsert" runat="server" Text=" 插 入" OnClick="btninsert_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnappend" runat="server" Text="追 加" OnClick="btnadd_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btndelete" runat="server" Text="删 除" OnClick="btndelete_Click" OnClientClick="return alert('确定删除此条目吗？')" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" CausesValidation="false" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnverity" runat="server" Text="审 核" OnClick="Verify_Click" OnClientClick="ClientSideClick(this);" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="Close" type="button" value="关闭" onclick="closewin()" runat="server" />&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="HeadPanel" runat="server" Width="100%">
                <table width="100%">
                    <tr>
                        <td>
                            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="font-size: x-large; text-align: center;" colspan="2">
                            其他入库单
                            <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" />
                        </td>
                        <td>
                         <asp:CheckBox ID="CheckBoxDate" runat="server" Text="结转到下期" CssClass="checkBoxCss" onclick="clickalert(this);" />
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                        </td>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;库：
                            <asp:DropDownList ID="DropDownListWarehouse" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="DropDownListWarehouse_SelecedIndexChanged">
                                <asp:ListItem Value="0">-请选择-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;仓&nbsp;&nbsp;&nbsp;位：
                            <asp:DropDownList ID="DropDownListPosition" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPosition_SelecedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">待查</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%">
                            <asp:UpdatePanel ID="UpdatePanelCompany" runat="server">
                                <ContentTemplate>
                                    &nbsp;&nbsp;&nbsp;供应商：
                                    <asp:TextBox ID="TextBoxCompany" runat="server" OnTextChanged="TextBoxCompany_TextChanged"
                                        AutoPostBack="true" onclick="this.select();"></asp:TextBox>                                   
                                    <asp:AutoCompleteExtender ID="Company_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="getotherCompany"
                                        ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="TextBoxCompany"
                                        UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                            <td>
                                    &nbsp;&nbsp;&nbsp;任&nbsp;&nbsp;务&nbsp;号：
                             <asp:TextBox ID="DropDownListEng" runat="server"  OnTextChanged="DropDownListEng_Textchanged"  AutoPostBack="True"></asp:TextBox>
                                       <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionSetCount="10"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetTask"
                                        ServicePath="~/PC_Data/PC_Data_AutoComplete.asmx" FirstRowSelected="true" TargetControlID="DropDownListEng"
                                        UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </asp:AutoCompleteExtender>
                                </td>
                        <td width="25%">
                            &nbsp;&nbsp;&nbsp;合&nbsp;&nbsp;&nbsp;同：<asp:TextBox ID="TextBoxAbstract" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" >
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    &nbsp;&nbsp;&nbsp;项&nbsp;&nbsp;&nbsp;目：<asp:TextBox ID="TextBoxProject" runat="server"></asp:TextBox>
                                                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                                              
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            进行其他入库原因：<input id="ipt_reason" style="width:150px" type="text" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelBody" runat="server" Style="height: 350px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;
                        <input id="continue" type="button" value="连选" onclick="consel()" />&nbsp;&nbsp;&nbsp;
                        <input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
                        <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                            CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <asp:TextBox ID="marid" runat="server" BorderStyle="None" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>'
                                            Width="120px" OnTextChanged="TextBoxMarid_TextChanged" AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode"
                                            ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="marid" UseContextKey="True" CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </asp:AutoCompleteExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="marnm" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("MaterialName") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="guige" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("MaterialStandard") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="caizhi" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Attribute") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="gb" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("GB") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="ph" runat="server" style="border-style: none; width: 100px" type="text"
                                            value='<%#Eval("LotNumber") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="长(mm)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="length" runat="server" style="border-style: none; width: 50px" type="text"
                                            value='<%#Eval("Length") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="宽(mm)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="width" runat="server" style="border-style: none; width: 50px" type="text"
                                            value='<%#Eval("Width") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="张(支)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="zhang" runat="server" style="border-style: none; width: 50px" type="text"
                                            value='<%#Eval("RQN") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="unit" runat="server" style="border-style: none; width: 40px" type="text"
                                            value='<%#Eval("Unit") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                    <ItemTemplate>
                                        <input id="num" runat="server" style="border-style: none; width: 40px" type="text"
                                            value='<%#Eval("RN") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单价" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="dj" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("UnitPrice") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="je" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Amount") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="税率(%)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="tr" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("TAXRATE") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="含税单价" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="tdj" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("CTAXUPRICE") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="含税金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="tje" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("CTAMTMNY") %>' onkeyup="grControlFocus(this)" onblur="checknum(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="ck" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Warehouse") %>' onkeyup="grControlFocus(this)" />
                                        <input type="text" id="InputWarehouseCode" value='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                            style="display: none" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="cw" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Position") %>' onkeyup="grControlFocus(this)" onclick="ChoosePosition(this)" />
                                        <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                            style="display: none" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="ptc" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("PTC") %>' onkeyup="grControlFocus(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <input id="beizhu" runat="server" style="border-style: none; width: 80px" type="text"
                                            value='<%#Eval("Comment") %>' onkeyup="grControlFocus(this)" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <br />
            <asp:Panel ID="FooterPanel" runat="server" Width="100%">
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
</asp:Content>

