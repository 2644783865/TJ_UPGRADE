<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_Contract_FB.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_Contract_FB" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="lblContractTypeBT" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
        //检验日期格式如：2012-01-01
        function dateCheck(obj) {
            var value = obj.value;
            if (value != "") {
                var re = new RegExp("^([0-9]{4})(-)([0-9]{2})(-)([0-9]{2})$");
                m = re.exec(value)
                if (m == null) {
                    obj.style.background = "yellow";
                    obj.value = "";
                    alert('请输入正确的时间格式如：2012-01-01');
                }
            }
        }
        //判断金额的格式 是否为最多两位小数的正数
        function check_num(obj) {
            var num = obj.value;
            var patten = /^[0-9][0-9]{0,9}(\.[0-9]{1,2})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0";
                obj.focus();
            }

        }
    </script>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-wrapper">
                    <div style="height: 8px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <%--操作按钮--%>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_search" runat="server" Text="查 询" OnClick="btn_search_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_reset" runat="server" Text="重 置" OnClick="btn_reset_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_ViewHT" runat="server" Text="查 看" OnClick="btn_ViewHT_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_EditHT" runat="server" Text="编 辑" OnClick="btn_EditHT_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_DelHT" runat="server" Text="删 除" OnClick="btn_DelHT_Click"
                                        OnClientClick="javascript:return confirm('确定删除吗？')" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_AddQK" runat="server" Text="添加请款" OnClick="btn_AddQK_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_AddHT" runat="server" Text="添加合同" OnClick="btn_AddHT_Click" />
                                    &nbsp;|&nbsp;<asp:Button ID="btn_Export" runat="server" Text="导 出" OnClick="btn_Export_Click"
                                        OnClientClick="javascript:return confirm('导出当前筛选记录吗？')" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Pal_Query" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        合同编号：<asp:TextBox ID="txtHTH" runat="server" Width="205px"></asp:TextBox>
                                    </td>
                                    <td>
                                        合同名称：<asp:TextBox ID="txt_HTNAME" runat="server" Width="205px"></asp:TextBox>
                                    </td>
                                    <td>
                                        责任部门：<asp:DropDownList ID="dplBM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplBM_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        项目名称：<asp:TextBox ID="txt_PJNAME" runat="server" Width="205px"></asp:TextBox>
                                    </td>
                                    <td>
                                        分 包 商：<asp:TextBox ID="txt_GHS" runat="server" Width="205px"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_GHS"
                                            ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                                            ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </asp:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        负 责 人：<asp:DropDownList ID="dplFZR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        签订时间：<asp:TextBox ID="sta_time" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox>
                                        至&nbsp;<asp:TextBox ID="end_time" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox>
                                        <asp:CalendarExtender ID="calender_sta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="sta_time">
                                        </asp:CalendarExtender>
                                        <asp:CalendarExtender ID="calender_end" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="end_time">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        付款比例：<asp:DropDownList ID="ddlfkbl" runat="server">
                                            <asp:ListItem Text="等于" Value="="></asp:ListItem>
                                            <asp:ListItem Text="大于" Value=">"></asp:ListItem>
                                            <asp:ListItem Text="小于" Value="<"></asp:ListItem>
                                            <asp:ListItem Text="大于等于" Value="">="></asp:ListItem>
                                            <asp:ListItem Text="小于等于" Value="<="></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="fkbl" runat="server" Width="30px" onchange="javascript:check_num(this);"></asp:TextBox>%
                                    </td>
                                    <td>
                                        每页显示：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;条记录&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        合同评审状态：<asp:DropDownList ID="htps_state" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                            <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="无" Value=""></asp:ListItem>
                                            <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="审批通过" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        合同年份：<asp:DropDownList ID="htnf" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto; position: relative;
                            margin: 2px" Width="100%">
                            <yyc:SmartGridView ID="GRV_CON" Width="100%" CssClass="toptable grid" runat="server"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true"
                                OnRowDataBound="grv_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                            <asp:Label ID="lbl_id" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbl_htbh" runat="server" Text='<%# Eval("PCON_BCODE") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_fzr" runat="server" Text='<%#Eval("PCON_RESPONSER")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbl_htlx" runat="server" Text='<%#Eval("PCON_FORM")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PCON_PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="PCON_BCODE" HeaderText="合同编号" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="PCON_NAME" HeaderText="合同名称" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="PCON_CUSTMNAME" HeaderText="分包商" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PCON_JINE" HeaderText="合同金额" DataFormatString="{0:c}"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PCON_BALANCEACNT" HeaderText="结算金额" DataFormatString="{0:c}"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                    <asp:TemplateField HeaderText="开票金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_kpje" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PCON_YFK" HeaderText="已付金额" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="付款比例" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_skbl" runat="server" Text='<%#string.Format("{0:N2}",(Convert.ToDouble(Eval("PCON_YFK"))/Convert.ToDouble(Convert.ToDouble(Eval("PCON_HTZJ"))==0?"1":Eval("PCON_HTZJ")))*100)+"%" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="请款金额" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_qkje" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="是否索赔" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_state" runat="server" Text='<%#(Eval("PCON_ERROR").ToString()=="0"?"正常":"索赔")%>'
                                                ForeColor='<%#Eval("PCON_ERROR").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.FromName("#333333") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="索赔金额" DataField="PCON_SPJE" DataFormatString="{0:c}"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                        ItemStyle-ForeColor="Red" />
                                    <asp:TemplateField HeaderText="合同变更" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_change" runat="server" Text='<%#"JE("+Eval("PCON_JECHG").ToString()+")"+"-"+"QT("+Eval("PCON_QTCHG").ToString()+")"%>'
                                                ForeColor='<%#Eval("PCON_QTCHG").ToString()!="0"?System.Drawing.Color.Red:Eval("PCON_JECHG").ToString()!="0"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="PCON_RESPONSER" HeaderText="负责人" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="合同评审状态" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="htps_state1" runat="server" Text='<%#(Eval("CR_PSZT").ToString()==""?"":Eval("CR_PSZT").ToString()=="1"?"审批中":Eval("CR_PSZT").ToString()=="2"?"审批通过":"已驳回") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#A8DCF8" Font-Bold="True" ForeColor="White" Wrap="False" />
                                <RowStyle BackColor="#ffffff" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                                <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4" />
                            </yyc:SmartGridView>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                没有记录!</asp:Panel>
                        </asp:Panel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: right">
                                    筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;
                                    合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp; 合计索赔：<asp:Label ID="lb_select_sp" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        //单击行变色
        function RowClick(obj) {
            //判断是否单击的已选择的行，如果是则取消该行选择
            if (obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked == false) {
                obj.style.backgroundColor = '#ffffff';
                obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
            }
            else {
                var table = obj.parentNode.parentNode;
                var tr = table.getElementsByTagName("tr");

                var ss = tr.length;
                for (var i = 1; i < ss - 1; i++) {
                    tr[i].style.backgroundColor = (tr[i].style.backgroundColor == '#87CEFF') ? '#ffffff' : '#ffffff';
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                }
                obj.style.backgroundColor = (obj.style.backgroundColor == '#ffffff') ? '#87CEFF' : '#ffffff';
                obj.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
            }
        }

        //查看合同
        function ViewHT(ID, conform) {
            var autonum = Math.round(10000 * Math.random());
            window.open("CM_Contract_addlot.aspx?Action=View&ConForm=" + conform + "&autonum=" + autonum + "&condetail_id=" + ID);
        }

        //编辑合同
        function EditHT(ID, conform) {
            var autonum = Math.round(10000 * Math.random());
            window.open("CM_Contract_addlot.aspx?Action=Edit&ConForm=" + conform + "&autonum=" + autonum + "&condetail_id=" + ID);
        }

        //添加请款
        function Add_QK(ID, conform) {
            var autonum = Math.round(10000 * Math.random());
            window.open("CM_Contract_addlot.aspx?Action=Add&ConForm=" + conform + "&autonum=" + autonum + "&condetail_id=" + ID);

        }

        //添加合同
        function Add_HT(conform) {
            var autonum = Math.round(10000 * Math.random());
            window.open("CM_Contract_addlot.aspx?Action=Add&autonum=" + autonum + "&ConForm=" + conform);
        }
    </script>

</asp:Content>
