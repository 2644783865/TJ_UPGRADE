<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="CM_AllBill.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_AllBill" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <table width="100%">
        <tr>
            <td style="width: 40%">
                发票单据
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript" language="javascript">
        function btnADDSWFP_onclick() {
            var sRet = window.showModalDialog("CM_AddBill.aspx", "obj", "dialogWidth=750px;dialogHeight=500px;status:no;");
            //  if(sRet=="refresh")
            //   {
            //     window.location.reload();
            //   }
        }

        //修改发票
        function FPEdit(i, ID) {

            var autonum = Math.round(10000 * Math.random());
            var sRet;
            if (i == 0) //商务发票
            {
                var sRet = window.showModalDialog("CM_Bill_SW.aspx?Action=Edit&BRid=" + ID + "&NoUse=" + autonum, obj, "dialogWidth=650px;dialogHeight=400px;");
            }
            else //其他发票
            {
                var sRet = window.showModalDialog("CM_Bill.aspx?Action=Edit&BillID=" + ID + "&NoUse=" + autonum, obj, "dialogWidth=700px;dialogHeight=400px;");
            }
            //    if(sRet=="refresh")
            //   {
            //     window.location.reload();
            //   }
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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                发票单据
                            </td>
                            <td align="right">
                                <asp:Button ID="btnExport" runat="server" Text="导 出" OnClick="btnExport_Click" />
                                &nbsp; &nbsp;&nbsp;
                                <asp:Button ID="btn_search" runat="server" Text="查 询" OnClick="btn_search_Click" />
                                &nbsp; &nbsp;&nbsp;<asp:Button ID="Btn_Reset" runat="server" Text="重 置" OnClick="Btn_Reset_Click" />&nbsp;
                                &nbsp;&nbsp;
                                <asp:HyperLink ID="hplAddFP" CssClass="hand" runat="server" onClick="javascript:return btnADDSWFP_onclick();">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Assets/icons/add.gif" />添加发票
                                </asp:HyperLink>
                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div class="box-outer" id="swfp" style="display: block">
                        <asp:Panel ID="Pal_Query" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        合同编号:
                                        <asp:TextBox ID="txt_HTBH" runat="server" Width="205px"></asp:TextBox>
                                    </td>
                                    <td>
                                        项目名称:
                                        <asp:TextBox ID="txt_PJNAME" runat="server" Width="205px"></asp:TextBox>
                                    </td>
                                    <td>
                                        开票单位:
                                        <asp:TextBox ID="txt_KPDW" runat="server"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txt_KPDW"
                                            ServicePath="~/Ajax.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                                            ServiceMethod="GetCompleteProvider" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        开票时间:
                                        <asp:TextBox ID="kpsta_time" runat="server" Width="90px"></asp:TextBox>
                                        至&nbsp;<asp:TextBox ID="kpend_time" runat="server" Width="90px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calender_kpsta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="kpsta_time">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="calender_kpend" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="kpend_time">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>
                                        收票时间:
                                        <asp:TextBox ID="spsta_time" runat="server" Width="90px"></asp:TextBox>
                                        至&nbsp;<asp:TextBox ID="spend_time" runat="server" Width="90px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calendar_spsta" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="spsta_time">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="calendar_spend" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                            TodaysDateFormat="yyyy年MM月dd日" TargetControlID="spend_time">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelFPJL" runat="server" Style="width: 100%; height: auto; overflow: auto;">
                            <asp:GridView ID="grvFPJL" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" ShowFooter="true">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                        ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <label>
                                                <%# Convert.ToInt32(Container.DataItemIndex +1) %></label>
                                            <asp:Label ID="lbl_kprq" runat="server" Text='<%#Eval("KPRQ") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="申请号" DataField="PZH" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="合同编号" DataField="HTBH" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="对方合同号" DataField="PCON_YZHTH" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="开票单位" DataField="KPDW" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="项目名称" DataField="PJNAME" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="设备名称" DataField="ENGNAME" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="开票金额" DataField="KPJE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  />
                                    <asp:BoundField HeaderText="数量" DataField="SL" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="单位" DataField="BR_DANWEI" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="重量" DataField="BR_WEIGHT" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="开票日期" DataField="KPRQ" DataFormatString="{0:d}" HtmlEncode="False"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="收票日期" DataField="SPRQ" DataFormatString="{0:d}" HtmlEncode="False"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField HeaderText="发票单号" DataField="FPDH" ItemStyle-HorizontalAlign="Left"
                                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Lbtn_Edit" runat="server" CommandArgument='<%#Eval("MAINID") %>'
                                                OnClick="Lbtn_Edit_OnClick">
                         编辑
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:Panel ID="pal_NoData" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                        </asp:Panel>
                        <table width="100%">
                            <tr>
                                <td style="text-align: right">
                                    筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
                                    合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                                <td>
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
