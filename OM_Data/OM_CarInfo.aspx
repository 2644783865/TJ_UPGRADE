<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarInfo.aspx.cs" ValidateRequest="false" Inherits="ZCZJ_DPF.OM_Data.OM_CarInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%--<%@   Page      %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车辆详细信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript" language="javascript">
        window.onload = function() {
            //    debugger;
            var outputImage = document.getElementById('<%=ImageCar.ClientID%>').value;
            //       alert(outputImage);
            var num = document.getElementById('<%=imagenum.ClientID%>').value;

            document.getElementById('<%=panelimage.ClientID%>').innerHTML = outputImage;
        }
    </script>

    <div style="text-align: center; width: 1200px">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="98%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_OnClick" CausesValidation="False" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="width: 1000px; margin: auto; border: 1px dotted;">
            <div style="text-align: left">
                <asp:Panel ID="panelimage" runat="server" />
            </div>
        </div>
        <div style="width: 1200px; text-align: center">
            <table style="width: 1000px; margin: auto" class="toptable grid" border="1">
                <tr style="height: 40px">
                    <td colspan="1">
                        车牌号：
                    </td>
                    <td colspan="1">
                        <%--<asp:Label ID="lblCarNum" runat="server" Text=""></asp:Label>--%>
                        <input id="lblCarNum" runat="server" disabled="disabled" />
                    </td>
                    <td>
                        购买日期：
                    </td>
                    <td>
                        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <%--<asp:TextBox ID="ImageCar" runat="server" Text="" Visible="false"  />--%>
                        <%--<asp:TextBox ID="imagenum" runat="server" Text=""  />--%>
                        <input type="hidden" runat="server" id="ImageCar" />
                        <input type="hidden" runat="server" id="imagenum" />
                    </td>
                </tr>
                <tr id="trimage" style="height: 40px">
                    <td colspan="1">
                        车辆类型：
                    </td>
                    <td colspan="1">
                        <asp:Label ID="lblCarType" runat="server" Text=""></asp:Label>
                    </td>
                    <td colspan="1">
                        负责人：
                    </td>
                    <td colspan="1">
                        <%--<asp:DropDownList ID="ddl_fzr" runat="server" Width="200px"></asp:DropDownList>--%>
                      <%--  <asp:Label ID="txtfzr" runat="server"></asp:Label>--%>
                        <asp:HyperLink ID="hlkFzr" runat="server" Target="_blank" style="text-decoration:underline;color:#FF4400"></asp:HyperLink>
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td colspan="1">
                        加油卡：
                    </td>
                    <td colspan="1">
                        <asp:Label ID="txtcard" runat="server"></asp:Label>
                    </td>
                    <td colspan="1">
                        加油卡余额：
                    </td>
                    <td colspan="1">
                        <asp:Label ID="lbl_cardye" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td>
                        载客人数：
                    </td>
                    <td>
                        <asp:Label ID="lblCapacity" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 15%">
                        颜色：
                    </td>
                    <td>
                        <asp:Label ID="lblColor" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td style="width: 15%">
                        总里程数：
                    </td>
                    <td>
                        <asp:Label ID="lblMileage" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 20%">
                        总用油量：
                    </td>
                    <td>
                        <asp:Label ID="lblOil" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td>
                        生产厂商：
                    </td>
                    <td>
                        <asp:Label ID="lblManufacturer" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        额定吨数：
                    </td>
                    <td>
                        <asp:Label ID="lblTunnage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td>
                        备注：
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 1200px;">
         <div style="text-align: left">
            <strong style="padding-left: 100px">车辆保险：</strong>&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1" style="width: 980px; margin: auto">
                <asp:Repeater ID="Det_Repeater" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <td width="50px">
                                <strong>序号</strong>
                            </td>
                            <td>
                                <strong>名称</strong>
                            </td>
                            <td>
                                <strong>开始日期</strong>
                            </td>
                            <td>
                                <strong>结束日期</strong>
                            </td>
                            <td>
                                <strong>缴费金额</strong>
                            </td>
                            <td>
                                <strong>备注</strong>
                            </td>
                            <%-- <td>
                                                <strong>证明人</strong>
                                            </td>--%>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                <%--<asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                            </asp:CheckBox>--%>
                            </td>
                            <td>
                                <%# Eval("BXNAME")%>
                            </td>
                            <td>
                                <%# Eval("STARTDATE")%>
                            </td>
                            <td>
                                <%# Eval("ENDDATE")%>
                            </td>
                            <td>
                                <%# Eval("BXJE")%>
                            </td>
                            <td>
                                <%# Eval("NOTE")%>
                            </td>
                            <%-- <td>
                                                <asp:TextBox ID="ST_INDENTITY" runat="server" Text='<%# Eval("ST_INDENTITY")%>' Width="100px"></asp:TextBox>
                                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                没有记录!</asp:Panel>
            <br />
            <div>
            </div>
        </div>
        <div style="width: 1200px">
      <div style="text-align: left">
            <strong style="padding-left: 100px; ">维修保养加油信息：</strong>&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <table id="Table1" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1" style="width: 980px; margin: auto">
                <tr>
                    <td align="right">
                        维修信息
                    </td>
                    <td colspan="2" align="center">
                        <asp:HyperLink ID="link_wx" runat="server" Text="点击查看详细信息" Target="_blank" style="text-decoration:underline;color:#FF4400"></asp:HyperLink>
                        <%--<asp:LinkButton ID="link_wx" runat="server" Text="点击查看详细信息" PostBackUrl="~/OM_Data/OM_CarWeihu.aspx">
            </asp:LinkButton>--%>
                    </td>
                    <td align="right">
                        年度维修费用小计：
                    </td>
                    <td>
                        年份：<asp:DropDownList ID="ddl_wx" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_wx_changed">
                        </asp:DropDownList>
                    </td>
                    <td>
                        费用:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_wx" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" align="right">
                        保养信息
                    </td>
                    <td colspan="2" align="center">
                        <asp:HyperLink ID="link_by" runat="server" Text="点击查看详细信息" Target="_blank" style="text-decoration:underline;color:#FF4400"></asp:HyperLink>
                    </td>
                    <td align="right">
                        年度费用小计：
                    </td>
                    <td>
                        年份：<asp:DropDownList ID="ddl_by" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_by_changed">
                        </asp:DropDownList>
                    </td>
                    <td>
                        费用:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_by" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        上次保养公里数：
                    </td>
                    <td>
                        <asp:Label ID="lblLastBy" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        下次保养公里数：
                    </td>
                    <td>
                        <asp:Label ID="lblNextBy" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblYujing" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        加油信息
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="link_jy" runat="server" Text="点击查看详细信息" Target="_blank" style="text-decoration:underline;color:#FF4400"></asp:HyperLink>
                        <%--<asp:LinkButton ID="link_wx" runat="server" Text="点击查看详细信息" PostBackUrl="~/OM_Data/OM_CarWeihu.aspx">
            </asp:LinkButton>--%>
                    </td>
                    <td align="right">
                        年度加油费用小计：
                    </td>
                    <td>
                        年份：<asp:DropDownList ID="ddl_jy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_jy_changed">
                        </asp:DropDownList>
                        费用:&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_jy" runat="server"></asp:Label>
                    </td>
                    <td>
                        实时油耗:
                    </td>
                    <td>
                        <asp:Label ID="lblYouHao" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 1200px">
         <div style="text-align: left">
            <strong style="padding-left: 100px">车辆运行信息：</strong>&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <table id="Table2" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1" style="width: 980px; margin: auto">
                <tr>
                    <td align="right">
                        车辆目前状态
                    </td>
                    <td>
                        <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right">
                        (若外出)目的地：
                    </td>
                    <td>
                        <asp:Label ID="lblMudidi" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        出车司机：
                    </td>
                    <td>
                        <asp:Label ID="lblChuCheSJ" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        车辆运行详细信息
                    </td>
                    <td colspan="5">
                        <asp:HyperLink ID="link_CarYunXingInfo" runat="server" Text="点击查看详细信息" style="text-decoration:underline;color:#FF4400"  Target="_blank"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
</asp:Content>
