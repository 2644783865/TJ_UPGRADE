<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseOUT_QT_RED.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_QT_RED" 
     MasterPageFile="~/Masters/SMBaseMaster.Master"  Title="其他出库红联退库" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
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

    <script src="SM_JS/KeyControl.js" type="text/javascript"></script>

    <script src="SM_JS/QTSQL.js" type="text/javascript"></script>

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
                   
                par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= unitprice*realnum;//金额
            }
            else
            {
            
                  if(realnum>0)
                  
                  tb.value = -realnum;
                  
                  else
                  
                  tb.value = realnum;
            
                   var par = tb.parentNode.parentNode;
                   
                   var unitprice = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("input")[0].value);//单价
                   
                   if(isNaN(parseFloat(unitprice)))
                    
                    unitprice=0;
                   
                   par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= unitprice*parseFloat(tb.value);//金额
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
                   
                par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= unitprice*realnum;//金额
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
                   
                  par.getElementsByTagName("td")[18].getElementsByTagName("input")[0].value= unitprice*realnum;//金额
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
            val = window.showModalDialog("SM_WarehouseIN_PositionChoice.aspx?WSCODE=" + wscode+"&&id="+time, '', "dialogHeight: 550px; dialogWidth: 800px;  center: Yes; help: No; resizable: No; status: No;");
            if (val != null) 
            {
                tb.value = val[1];
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[16].getElementsByTagName("input")[1];
                positioncode.value = val[0];
            }
            else
            {
                tb.value = "待查";
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[16].getElementsByTagName("input")[1];
                positioncode.value = "0";
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
                    var dj = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;
                    
                    if(dj==""||dj==0)
                    {
                    
                      alert("单价不能为空或者为0!");
                      
                      return false;
                      
                    }
                   
                    var warehouse = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                        
                    if (warehouse == "-请选择-"||warehouse == "") 
                    {
                        alert("请选择仓库！");
                        return false;
                    }
                    
                    var position= gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;
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
//            生产制号
            var engid=document.getElementById("<%=TextBoxSCZH.ClientID %>");
            
            if(engid.value=="")
            {
              alert("请输入生产制号！");
              
              return false;
            }
//            退料部门
            var dep = document.getElementById('<%=DropDownListDep.ClientID %>');
            var depcode = "";
            for (var i = 0; i < dep.options.length; i++)
            {
                    if (dep.options[i].selected == true)
                    {
                        depcode = dep.options[i].value;
                        break;
                    }
            }
            
            if(depcode=="0"||depcode=="")
            {
              alert("请选择退料部门！");
              
              return false;
            }
            
            if(count==0)
            {
              alert("没有数据，不能保存或审核！");
              
              return false;
            }
            return flag;
        
        }
        
        
        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
        }
        
//        function btnPrint_onclick() {
//        var date=new Date();
//        var time=date.getTime();
//        window.showModalDialog('SM_WarehouseOUT_Print.aspx?outcode=<%=LabelCode.Text %>&&id='+time,'',"dialogWidth=800px;dialogHeight=700px");   
//        } 

        ///*批号生成规则填写的批号生成标准批号*/
        function checkLotNum(tb) {
            if (tb.value == "") {return;}            
            var lotnum = parseFloat(tb.value);
            if (isNaN(lotnum)||lotnum<=0||lotnum>999) {
                alert("请正确输入批号！");
                tb.value = "";
                return;
            }
            var par = tb.parentNode.parentNode;
            var lotnum = par.getElementsByTagName("td")[7].getElementsByTagName("input")[0];
            var code = document.getElementById('<%= LabelCode.ClientID %>').innerHTML;
            if (lotnum.value != "") {
                var stringlist = lotnum.value.split('-');
                if ((stringlist[0].length == 1)) { stringlist[0] = "00" + stringlist[0]; }
                else if (stringlist[0].length == 2) { stringlist[0] = "0" + stringlist[0]; }
                else { }
                lotnum.value = stringlist[0] + "-" + code;
            }       
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
                <div style="margin: 0px 0px 0px 10px">
                    <table width="99%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                        <tr>
                            <td style="width: 20%" align="left">
                                &nbsp;&nbsp;
                                <asp:Button ID="btninsert" runat="server" Text="插 入" OnClick="btninsert_Click" CausesValidation="False" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnappend" runat="server" Text="追 加" CausesValidation="False" OnClick="btnadd_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btndelete" runat="server" Text="删 除" CausesValidation="False" OnClick="btndelete_Click"
                                    OnClientClick="return alert('确定删除此条目吗？')" />
                            </td>
                            <td align="right">
                                <input id="Close" type="button" value="关闭" onclick="closewin()" runat="server" />&nbsp;
                                <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" CausesValidation="False"
                                    OnClientClick="return checkPage();" />
                                &nbsp;
                                <asp:Button ID="btnverity" runat="server" Text="审 核" OnClick="Verify_Click" CausesValidation="False"
                                    OnClientClick="return checkPage();" />
                                &nbsp;
                                <asp:Button ID="btnantiverity" runat="server" Text="反 审" OnClick="AntiVerify_Click"
                                    Visible="false" />&nbsp;
                                <asp:Button ID="dj_delete" runat="server" Text="删 单" OnClick="DeleteBill_Click" Visible="false" />&nbsp;
                                <input id="btnPrint" runat="server" type="button" value="打印" onclick="return btnPrint_onclick()"
                                    visible="false" />&nbsp;
                                <asp:Button ID="tostorge" runat="server" Text="退 库" OnClick="backstorge_Click" CausesValidation="false"
                                    Visible="false" />
                                &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                    <table width="100%">
                        <tr>
                            <td colspan="3">
                                &nbsp;&nbsp;&nbsp;系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: x-large; text-align: center;" colspan="3">
                                红联退库
                                <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" />&nbsp;&nbsp;&nbsp;
                                <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                    Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="35%">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:Label
                                    ID="LabelCode" runat="server"></asp:Label>
                                <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                                <input type="text" id="InputColour" style="display: none" runat="server" />
                            </td>
                            <td align="center" width="30%">
                                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                    TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd" FirstDayOfWeek="Monday">
                                </asp:CalendarExtender>
                            </td>
                            <td align="left" width="35%">
                                退料部门：<asp:DropDownList ID="DropDownListDep" runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                </asp:DropDownList>
                                制作班组：
                                <asp:DropDownList ID="DropDownListBZ" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;生产制号：
                                <asp:TextBox ID="TextBoxSCZH" runat="server" Width="170px" ></asp:TextBox>
                                <asp:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="TextBoxSCZH" FirstRowSelected="true"
                                    MinimumPrefixLength="1" ServiceMethod="getEngID" ServicePath="~/Ajax.asmx">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td align="center">
                                &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="TextBoxComment" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                仓库：
                                <asp:DropDownList ID="DropDownListWarehouse" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="DropDownListWarehouse_SelecedIndexChanged">
                                    <asp:ListItem Value="0">-请选择-</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;仓位：
                                <asp:DropDownList ID="DropDownListPosition" runat="server" AutoPostBack="True" AppendDataBoundItems="true"
                                    OnSelectedIndexChanged="DropDownListPosition_SelecedIndexChanged">
                                    <asp:ListItem Value="0">待查</asp:ListItem>
                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="CheckBoxDate" runat="server" Text="会计期间" CssClass="checkBoxCss" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server" Style="height: 380px; margin: 2px; width: 100%;
                    overflow: scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                        CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                <ItemTemplate>
                                    <asp:TextBox ID="marid" runat="server" BorderStyle="None" Text='<%# DataBinder.Eval(Container.DataItem, "MaterialCode")%>'
                                        Width="120px" OnTextChanged="TextBoxMarid_TextChanged" AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionSetCount="10"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode"
                                        ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="marid" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="marnm" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("MaterialName") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="guige" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("MaterialStandard") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="caizhi" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("Attribute") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="gb" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("GB") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="ph" runat="server" style="border-style: none; width: 120px" type="text"
                                        value='<%#Eval("LotNumber") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checkLotNum(this)" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="unit" runat="server" style="border-style: none; width: 40px" type="text"
                                        value='<%#Eval("Unit") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                <ItemTemplate>
                                    <input id="num" runat="server" style="border-style: none; width: 40px" type="text"
                                        value='<%#Eval("RN") %>' onblur="checkRN(this)" onkeyup="grControlFocus(this)"
                                        onclick="getSelect(this)" />
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFFCC" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="长(mm)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                <ItemTemplate>
                                    <input id="length" runat="server" style="border-style: none; width: 50px" type="text"
                                        value='<%#Eval("Length") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFFCC" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="宽(mm)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                <ItemTemplate>
                                    <input id="width" runat="server" style="border-style: none; width: 50px" type="text"
                                        value='<%#Eval("Width") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFFCC" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="张（支）" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#FFFFCC">
                                <ItemTemplate>
                                    <input id="zhang" runat="server" style="border-style: none; width: 50px" type="text"
                                        value='<%#Eval("RQN") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFFCC" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="pt" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("PTC") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="beizhu" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("Comment") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="ck" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("Warehouse") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                    <input type="text" id="InputWarehouseCode" value='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                        style="display: none" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="cw" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("Position") %>' onclick="ChoosePosition(this)" onkeyup="grControlFocus(this)" />
                                    <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                        style="display: none" runat="server" />
                                    <%--<asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>' Visible="false"></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="dj" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("UnitPrice") %>' onblur="checkDJ(this)" onkeyup="grControlFocus(this)"
                                        onclick="getSelect(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="je" runat="server" style="border-style: none; width: 80px" type="text"
                                        value='<%#Eval("Amount") %>' onkeyup="grControlFocus(this)" onclick="getSelect(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="FooterPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                    <table width="100%">
                        <tr>
                            <td align="left" style="width: 25%;">
                                &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                                <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td align="left" style="width: 25%;">
                                &nbsp;&nbsp;&nbsp;收&nbsp;&nbsp;&nbsp;料：<asp:DropDownList ID="DropDownListSender"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="width: 25%;">
                                &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server" Width="100px"></asp:Label>
                                <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="left" style="width: 25%;">
                                审核日期：<asp:Label ID="LabelApproveDate" runat="server" Width="100px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

