<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseOUT_OPSplit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_OPSplit"
    Title="出库单据拆分" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="0">
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
        
        function checkSN(tb) {
       
             var sn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var rn = par.getElementsByTagName("td")[12].getElementsByTagName("span")[0].innerHTML;
            var quantity = parseFloat(par.getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML);
            var sq = par.getElementsByTagName("td")[15].getElementsByTagName("input")[0];
            var unitprice = parseFloat(par.getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML);
            var ctup = parseFloat(par.getElementsByTagName("td")[18].getElementsByTagName("span")[0].innerHTML);
            var samount = par.getElementsByTagName("td")[20].getElementsByTagName("input")[0];
            var scta = par.getElementsByTagName("td")[22].getElementsByTagName("input")[0]; 
            if (isNaN(sn)) {
                alert("请正确输入拆分数量！");
                tb.value = rn;
                sn = tb.value;
 //             sq.value = Math.round((sn / rn) * quantity); 
                samount.value = (sn * unitprice).toFixed(2);
                scta.value = (sn * ctup).toFixed(2);
                Statistic();
                return;
            }
            if (sn > rn || sn <= 0 ) {
                alert("拆分数量必须大于0并且不大于实收数量！");
                tb.value = rn;
                sn = tb.value;
//              sq.value = Math.round((sn / rn) * quantity);
                samount.value = (sn * unitprice).toFixed(2);
                scta.value = (sn * ctup).toFixed(2);
                Statistic();
                return;
            }
            if(sn.toFixed(4)==0&&sn>0)
            {
                alert("拆分数量必须是四位有效数字！");
                tb.value = rn;
                sn = tb.value;
//              sq.value = Math.round((sn / rn) * quantity);
                samount.value = (sn * unitprice).toFixed(2);
                scta.value = (sn * ctup).toFixed(2);
                Statistic();
                return;
            }
//            sq.value = Math.round((sn / rn) * quantity);
            samount.value = (sn * unitprice).toFixed(2);
            scta.value = (sn * ctup).toFixed(2);
            Statistic();     
        }
         
          function checkSN1(tb) {
         
            var par = tb.parentNode.parentNode;

//         总数

            var sq = parseFloat(tb.value);
              
            var quantity = parseFloat(par.childNodes[14].getElementsByTagName("span")[0].innerHTML);
            
//         拆分数量

            if (isNaN(sq)) {
                alert("请正确输入拆分张（支）数！");
                tb.value = quantity;
                return;
            }
            if (sq > quantity || sq <0 ) {
                alert("拆分张（支）数必须大于0并且不大于实收张（支）数！");
                tb.value =quantity;
                return;
            }
            Statistic(); 
        }
         
        function Statistic() 
        {
            var tsn = 0;
            var tsqn = 0;
            var tsa = 0;
            var tscta = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++)
             {
                 var val1 = gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;
                tsn += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                tsqn += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
                tsa += parseFloat(val3);
                var val4 = gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;
                tscta += parseFloat(val4);
            }
            var lbtsn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[13].getElementsByTagName("span")[0];
            lbtsn.innerHTML = tsn.toFixed(4);
            var lbtsqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[15].getElementsByTagName("span")[0];
            lbtsqn.innerHTML =Math.round(tsqn);
            var lbtsa = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[20].getElementsByTagName("span")[0];
            lbtsa.innerHTML = tsa.toFixed(2);
            var lbtscta = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[22].getElementsByTagName("span")[0];
            lbtscta.innerHTML = tscta.toFixed(2);
        }
        
        function ConfirmTip() {
            var retVal = confirm("确定进行拆分？");
            return retVal;
        }
           
        function CancelTip() {
            var retVal = confirm("放弃本次拆分并返回？");
            return retVal;
        }

        /*检查是否所有条目都被完全拆分，暂时不阻止这种情况 */
        function pagecheck() {
            var flag = false;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[11].getElementsByTagName("span")[0].innerHTML;
                var val2 = gv1.rows[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
                if (val1 != val2) {
                    flag = true;
                }
            }
            if (flag == false) {
                alert("不能将数据完全拆分到子单！");
            }
            return flag;
        }     
    </script>
    
    
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Width="300px"></asp:Label>
                        </td>
                        <td align="right" style="width: 100%">
                            <asp:Button ID="Confirm" runat="server" Text="确定" OnClick="Confirm_Click"
                                OnClientClick="return ConfirmTip()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Cancel" runat="server" Text="取消" CausesValidation="False" OnClick="Cancel_Click"
                                OnClientClick="return CancelTip()" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="HeadPanel" runat="server" Width="100%">
                <table width="100%">
                    <tr>
                        <td>
                            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="font-size: x-large; text-align: center;" colspan="2">
                            <asp:Label runat="server" ID="LabelTittle" Font-Size="Larger" ></asp:Label>
                            <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                        </td>
                        <td>
                            <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false" />
                            &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号&nbsp;&nbsp;&nbsp;：
                            <asp:Label ID="LabelCode" runat="server"  style="display: none" ></asp:Label>
                            <asp:Label ID="LabelNewCode" runat="server"  style="display: none"></asp:Label>
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                            <asp:Label ID="LabelBillType" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;日期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
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
                            领料部门：<asp:DropDownList ID="DropDownListDep" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;制作班组:
                            <asp:DropDownList ID="DropDownListBZ" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextBoxComment" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="CheckBoxDate" runat="server" Text="会计期间" CssClass="checkBoxCss" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;子项名称：
                            <asp:DropDownList ID="DropDownListZXMC" runat="server">
                                <asp:ListItem Value="--">-请选择-</asp:ListItem>
                                <asp:ListItem Value="包装">--包装--</asp:ListItem>
                                <asp:ListItem Value="支撑">--支撑--</asp:ListItem>
                                <asp:ListItem Value="窑托">--窑托--</asp:ListItem>
                                <asp:ListItem Value="磨托">--磨托--</asp:ListItem>
                                <asp:ListItem Value="备料">--备料--</asp:ListItem>
                                <asp:ListItem Value="底座">--底座--</asp:ListItem>
                                <asp:ListItem Value="预组装">--预组装--</asp:ListItem>
                                
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;张数:<asp:TextBox ID="TextBoxPageNum" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    
                    <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注:
                            
                            <asp:TextBox ID="TextBoxNOTE1" runat="server"></asp:TextBox>
                        </td>
                    <td colspan="3"></td>
                    </tr>         
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelBody" runat="server" Style="position: relative; overflow: auto;
                margin: 2px" margin="2px" Width="100%" Height="450px">
                <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    EmptyDataText="没有相应的记录！" EmptyDataRowStyle-HorizontalAlign="Center">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                                <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UniqueID")%>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'
                                    Width="100%"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                合计:
                            </FooterTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料代码" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="库存长(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DueLength")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="长(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'
                                     Width="60px" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="宽(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'
                                     Width="60px" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'
                                    Width="30px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实发数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'
                                    Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalRN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="拆分数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxSN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SN")%>'
                                    Style="width: 60px" ></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalSN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实发张（支）数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxRQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RQN")%>'
                                     Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalRQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="拆分张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <input type="text" id="InputSQN" runat="server" value='<%#DataBinder.Eval(Container.DataItem, "SQN")%>'
                                    style="width: 60px"  />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalSQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发料仓库" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                    Style="display: none"></asp:Label>
                                <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'
                                    Width="60px" onclick="ChoosePosition(this)" Enabled="false"></asp:TextBox>
                                <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                    style="display: none" runat="server" />
                                <%-- <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'></asp:Label>
                            <asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                Visible="false"></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标识号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelBSH" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BSH")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'
                                    Width="100px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <RowStyle BackColor="#EFF3FB" Wrap="false" />
                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" Wrap="false" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <FixRowColumn FixRowType="Header,Pager" TableHeight="450px" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />
                </yyc:SmartGridView>
            </asp:Panel>
            <asp:Panel ID="FooterPanel" runat="server" Width="100%">
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
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
