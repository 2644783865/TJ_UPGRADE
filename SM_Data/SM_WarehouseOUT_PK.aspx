<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseOUT_PK.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_PK"
    Title="盘亏出库单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
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

    <script type="text/javascript" language="javascript">
        function checkLength(tb) {
            var length = parseFloat(tb.value);
            if (isNaN(length) || length < 0) {
                alert("请输入正确的长度！");
                tb.value = 0;
            }
        }

        function checkWidth(tb) {
            var width = parseFloat(tb.value);
            if (isNaN(width) || width < 0) {
                alert("请输入正确的长度！");
                tb.value = 0;
            }
        }
        
        function checkRN(tb) {
            var rn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var dn = parseFloat(par.getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML);
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            if (isNaN(rn)) {
                alert("请输入正确的数量！");
                tb.value = dn;
                Statistic();
            }
            if ((color == '0') && (rn > dn)) {
                alert("出库数量不能大于库存数量！");
                tb.value = dn;
                Statistic();
            }
            if (rn == 0) {
                tb.value = dn;
                Statistic();
            }
            if ((color == '0') && (rn < 0)) {
                tb.value = -rn;
                if (tb.value > dn) {
                    tb.value = dn;
                }
                Statistic();
            }
            if ((color == '1') && (rn > 0)) {
                tb.value = -rn;
                Statistic();
            }
             if((color == '1') && (parseFloat(tb.value) <dn)){
            
               alert("红单数量不能多于原单数量！");
               
               tb.value = dn;
               
               Statistic();
            }
             Statistic();
        }

        function checkRQN(tb) {
            var rqn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var dqn = Math.round(par.getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML);
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            if (isNaN(rqn)) {
                alert("请输入正确的数量！");
                tb.value = dqn;
                Statistic();
            }
            if ((color == '0') && (rqn > dqn)) {
                alert("出库数量不能大于库存数量！");                
                tb.value =dqn;
                Statistic();
            }            
            if ((color == '0') && (rqn < 0)) {
                tb.value = -rqn;
                if (tb.value > dqn) {
                    tb.value = dqn;
                }
                Statistic();
            }
            
           if ((color == '1') && (rqn > 0)) {
                tb.value = -rqn;
                Statistic();
            }
         
//            if ((color == '1') && (parseFloat(tb.value)<dqn)) {
//                alert("红单数量不能大于原单数量！");   
//                tb.value = dqn;
//                Statistic();
//            }
            
          
            
            Statistic();
        }
        
        function Statistic() {
            var tdn = 0;
            var trn = 0;
            var tdqn = 0;
            var trqn = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");

            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML;
                tdn += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                trn += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML;
                tdqn += parseFloat(val3);
                var val4 = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;
                trqn += parseFloat(val4);
            }
            var lbtdn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[14].getElementsByTagName("span")[0];
            lbtdn.innerHTML = tdn.toFixed(4);
            var lbtrn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[15].getElementsByTagName("span")[0];
            lbtrn.innerHTML = trn.toFixed(4);
            var lbtdqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[16].getElementsByTagName("span")[0];
            lbtdqn.innerHTML = tdqn.toFixed(4);
            var lbtrqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[17].getElementsByTagName("span")[0];
            lbtrqn.innerHTML = trqn.toFixed(4);
        }

        function checkPage() {
            var flag = true;
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
           
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
              
            if (color == 0) {
                for (i = 1; i < (gv1.rows.length - 1); i++) {
                    var val1 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;//实发数量
                    var val2 = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;//实发张支数
                    if (val1 <= 0) {
                        gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                    if (val2 <0) {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                }
                if (flag == false) {
                    alert("蓝字出库单数量必须大于0！");
                    return flag;
                } 
            }
            if (color == 1) {
                for (i = 1; i < (gv1.rows.length - 1); i++) {
                    var val1 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                    var val2 = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;
                    if (val1 >= 0) {
                        gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                    if (val2 >0) {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                }
                if (flag == false) {
                    alert("红字出库单数量必须小于0！");
                    return flag;
                } 
            }
             
            return flag;
        }

        function append() {
            var retVal = window.showModalDialog("SM_Warehouse_Query.aspx?FLAG=APPENDOUT", "", "dialogWidth=1200px;dialogHeight=650px;status=no;help=no;scroll=no");
            return retVal;
        }

        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
        } 
        
          function btnPrint_onclick() {
          
          var date=new Date();
        var time=date.getTime();
        window.showModalDialog('SM_WarehouseOUT_Print.aspx?outcode=<%=LabelCode.Text %>&&id='+time,'',"dialogWidth=780px;dialogHeight=600px;scroll:no");   
        }
        
    function tostorage() {
     var date=new Date();
        var time=date.getTime();
        window.open("SM_Warehouse_Query.aspx?FLAG=PUSHLLOUT&id="+time,"_self");
    }
    
//    其他退库
   function backstorage() {
    var date=new Date();
        var time=date.getTime();
        window.open("SM_WarehouseOUT_Red.aspx?FLAG=PUSHRED&id="+time);
    }
    
    
    
    
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            <input id="Close" type="button" value="关闭" onclick="closewin()" />&nbsp;|
                            <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" />&nbsp;|
                            <asp:Button ID="Verify" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="return checkPage()" />
                            &nbsp;|
                            <asp:Button ID="AntiVerify" runat="server" Text="反审核" OnClick="AntiVerify_Click" />
                            &nbsp;|
                            <asp:Button ID="DeleteBill" runat="server" Text="删单" OnClick="DeleteBill_Click" />
                            &nbsp;|
                            <input id="btnPrint" runat="server" type="button" value="打印" onclick="return btnPrint_onclick()" />
                        </td>
                        <td>
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
            <table width="100%">
                <tr>
                    <td>
                        系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="font-size: x-large; text-align: center;" colspan="2">
                        盘盈(亏)出库单<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                            Visible="false" />
                    </td>
                    <td>
                        <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false" />
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" style="white-space: nowrap; width: 25%">
                        &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号&nbsp;&nbsp;&nbsp;：<asp:Label
                            ID="LabelCode" runat="server"></asp:Label>
                        <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                        <input type="text" id="InputColour" style="display: none" runat="server" />
                        <asp:Label ID="LabelBillType" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                    <td align="left" style="white-space: nowrap; width: 25%">
                        &nbsp;&nbsp;&nbsp;日期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" style="white-space: nowrap; width: 25%">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                &nbsp;&nbsp;&nbsp;生产制号：<asp:TextBox ID="TextBoxSCZH" runat="server" Width="170px"
                                    OnTextChanged="TextBoxSCZH_TextChanged" AutoPostBack="true" onclick="this.select();"></asp:TextBox>
                                <cc1:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="TextBoxSCZH" FirstRowSelected="true"
                                    MinimumPrefixLength="1" ServiceMethod="getEngID" ServicePath="~/Ajax.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    CompletionSetCount="30">
                                </cc1:AutoCompleteExtender>
                                <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="TextBoxSCZH"
                                    PopupControlID="Panel1" OffsetX="0" OffsetY="-18">
                                </cc1:HoverMenuExtender>
                                <asp:Panel ID="Panel1" runat="server" Style="background-color: #79c8ef; visibility: hidden;">
                                    <asp:Label ID="LabelSCZH" runat="server" ForeColor="Black"></asp:Label>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="white-space: nowrap; width: 25%">
                        &nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="CheckBoxDate" runat="server" Text="会计期间" CssClass="checkBoxCss" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PanelBody" runat="server" Style="position: relative; overflow: auto;
            margin: 2px" margin="2px" Width="100%" Height="450px">
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                EmptyDataText="没有相应的记录！" EmptyDataRowStyle-HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound"
                OnDataBound="GridView1_DataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1%>
                            <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UniqueID")%>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            合计:
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料代码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>'
                                Visible="false"></asp:Label>
                            <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库存长(mm)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DueLength")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="长(mm)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBoxLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'
                                onblur="checkLength(this)" Width="60px" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="宽(mm)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBoxWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'
                                onblur="checkWidth(this)" Width="60px" Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="即时库存数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelDN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DN")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalDN" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实发数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'
                                onblur="checkRN(this)" Width="80px"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalRN" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="即时库存张(支)" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelDQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DQN")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalDQN" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实发张（支）数" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBoxRQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RQN")%>'
                                onblur="checkRQN(this)" Width="100px"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="LabelTotalRQN" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发料仓库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                            <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'></asp:Label>
                            <asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'
                                Width="100px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                <RowStyle BackColor="#EFF3FB" Wrap="false" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" Wrap="false" />
                <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />
            </yyc:SmartGridView>
        </asp:Panel>
        <asp:Panel ID="FooterPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
            <table width="100%">
                <tr>
                    <td align="left" style="width: 25%;">
                        &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                        <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td align="left" style="width: 25%;">
                        &nbsp;&nbsp;&nbsp;发&nbsp;&nbsp;&nbsp;料：<asp:DropDownList ID="DropDownListSender"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="width: 25%;">
                        &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server" Width="100px"></asp:Label>
                        <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 25%;">
                        审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
