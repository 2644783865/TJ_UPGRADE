<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_DriverInfo.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_DriverInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    司机详细信息
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

    <div  style="text-align:center;width:1200px">
        <div style=" width: 1000px;margin:auto;border:1px dotted;">
            <div style="text-align: left">
                <asp:Panel ID="panelimage" runat="server" />
            </div>
        </div>
        <div style="width: 1200px; text-align: center">
            <table style="width: 1000px; margin: auto" class="toptable grid" border="1">
                <tr style="height: 40px">
                    <td colspan="1">
                        姓名：
                    </td>
                    <td colspan="1">
                        <%--<asp:Label ID="lblCarNum" runat="server" Text=""></asp:Label>--%>
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        <input type="hidden" runat="server" id="ImageCar" />
                        <input type="hidden" runat="server" id="imagenum" />
                        <input type="hidden" runat="server" id="hidContext" />
                    </td>
                    <td>
                        工号：
                    </td>
                    <td>
                        <asp:Label ID="lblWorkNo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr id="trimage" style="height: 40px">
                    <td colspan="1">
                        部门：
                    </td>
                    <td colspan="1">
                        <asp:Label ID="lblDep" runat="server" Text=""></asp:Label>
                    </td>
                    <td colspan="1">
                        岗位：
                    </td>
                    <td colspan="1">
                        <asp:Label ID="lblPos" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 1200px;">
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
                                <strong>有效期</strong>
                            </td>
                            <td>
                                <strong>下次验证时间</strong>
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
                                <%# Eval("dNAME")%>
                            </td>
                            <td>
                                <%# Eval("dYOUXIAOQI")%>
                            </td>
                            <td>
                                <%# Eval("dENDDATE")%>
                            </td>
                            <td>
                                <%# Eval("dNOTE")%>
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
    </div>
</asp:Content>
